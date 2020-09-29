using System;
using Microsoft.AspNetCore.Authorization;

namespace CRMSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class ContactsController:Controller
    {

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            //TODO: Make Create Input Model and so on

            throw new NotImplementedException();
        }
    }
}
