using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularAspShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AngularAspShop.Controllers
{
    public class AppController : Controller
    {
        private readonly IShopRepository shopRepository;

        public AppController(IShopRepository shopRepository)
        {
            this.shopRepository = shopRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }
    }
}