﻿using System;
using System.Collections.Generic;
using CRMSystem.Web.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CRMSystem.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Contacts;
    using CRMSystem.Web.ViewModels.Emails;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : Controller
    {
        private readonly IContactsService contactsService;
        private readonly IEmailsService emailsService;
        private readonly IOrganizationsService organizationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContactsController(
            IContactsService contactsService,
            IEmailsService emailsService,
            IOrganizationsService organizationsService,
            UserManager<ApplicationUser> userManager)
        {
            this.contactsService = contactsService;
            this.emailsService = emailsService;
            this.organizationsService = organizationsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var allContacts = this.contactsService.GetAllUserContacts<ContactViewModel>(user.Id);
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SortByName"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["SortByOrganization"] = String.IsNullOrEmpty(sortOrder) ? "organ_desc" : "";
            ViewData["SortByIndustry"] = String.IsNullOrEmpty(sortOrder) ? "industry_desc" : "";
            ViewData["SortByDate"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var contacts = from c in allContacts
                           select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                contacts = contacts.Where(s => s.LastName.Contains(searchString)
                                               || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "organ_desc":
                    contacts = contacts
                        .OrderByDescending(c => c.Organization.Name);
                    break;
                case "name_desc":
                    contacts = contacts
                        .OrderByDescending(c => c.FirstName)
                        .ThenByDescending(c => c.LastName)
                        .ThenByDescending(c => c.MiddleName);
                    break;
                case "industry_desc":
                    contacts = contacts
                        .OrderByDescending(c => c.Industry);
                    break;
                case "Date":
                    contacts = contacts.OrderBy(c => c.CreatedOn);
                    break;
                case "date_desc":
                    contacts = contacts.OrderByDescending(c => c.CreatedOn);
                    break;
                default:
                    contacts = contacts.OrderBy(c => c.LastName);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<ContactViewModel>.CreateAsync(contacts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ContactCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var contactId = await this.contactsService.CreateContactAsync(input, user.Id);

            return this.RedirectToAction("ConnectToOrganization", "Organizations", new { contactId = contactId });
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var course = await _context.Courses
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(m => m.CourseID == id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }
        //    PopulateDepartmentsDropDownList(course.DepartmentID);
        //    return View(course);
        //}

        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditPost(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var courseToUpdate = await _context.Courses
        //        .FirstOrDefaultAsync(c => c.CourseID == id);

        //    if (await TryUpdateModelAsync<Course>(courseToUpdate,
        //        "",
        //        c => c.Credits, c => c.DepartmentID, c => c.Title))
        //    {
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException /* ex */)
        //        {
        //            //Log the error (uncomment ex variable name and write a log.)
        //            ModelState.AddModelError("", "Unable to save changes. " +
        //                                         "Try again, and if the problem persists, " +
        //                                         "see your system administrator.");
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);
        //    return View(courseToUpdate);
        //}

        public async Task<IActionResult> Details(int id)
        {
            //todo make method async

            var contact = this.contactsService.GetContactById<ContactViewModel>(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await this.contactsService.DeleteContactAsync(id);

            //todo for this user check..
            if (contact == null)
            {
                return NotFound();
            }

            return this.RedirectToAction("Index");
        }


        [Authorize]
        public IActionResult GetDetails(int contactId)
        {
            var viewModel = this.contactsService.GetContactById<GetDetailsViewModel>(contactId);
            return this.View(viewModel);
        }
    }
}
