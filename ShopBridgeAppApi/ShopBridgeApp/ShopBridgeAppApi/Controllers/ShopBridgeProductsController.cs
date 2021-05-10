using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Services.Interfaces;
using ShopBridgeApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopBridgeAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopBridgeProductsController : ControllerBase
    {
        private readonly IShopBridgeAppService _shopBridgeAppService;
        public ShopBridgeProductsController(IShopBridgeAppService shopBridgeAppService)
        {
            _shopBridgeAppService = shopBridgeAppService;
        }
        

        // GET api/<ValuesController>/
        [HttpGet]
        public List<ShopBridgeItemProduct> Get()
        {
            return _shopBridgeAppService.GetShopBridgeItems().Result;
        }
       
    }
}
