using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FishInABox.Models;

namespace FishInABox.Controllers
{
    /*
    [Produces("application/json")]
    [Route("api/Shipping")]
    */
    public class ShippingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PurchaseOrder()
        {
            return View();
        }
    }
}