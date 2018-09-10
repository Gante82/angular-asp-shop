using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularAspShop.Data;
using AngularAspShop.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AngularAspShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IShopRepository shopRepository;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IShopRepository shopRepository, ILogger<ProductsController> logger)
        {
            this.shopRepository = shopRepository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(shopRepository.GetAllProducts());
            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products");
            }
        }

    }
}