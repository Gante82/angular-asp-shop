using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularAspShop.Data;
using AngularAspShop.Data.Entities;
using AngularAspShop.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AngularAspShop.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/orders/{orderId}/items")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IShopRepository repository;
        private readonly ILogger<OrderItemsController> logger;
        private readonly IMapper mapper;

        public OrderItemsController(IShopRepository repository, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = repository.GetOrderById(User.Identity.Name,orderId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = repository.GetOrderById(User.Identity.Name, orderId);

            if (order == null)
            {
                return NotFound();
            }

            var item = order.Items.Where(i => i.Id == id).FirstOrDefault();

            if (item == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<OrderItem, OrderItemViewModel>(item));
        }


    }
}