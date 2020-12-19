using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Areas.Administration.Controllers
{
    public class DashboardController:AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
