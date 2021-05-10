using ShopBridge.Services.Interfaces;
using ShopBridgeApp.Models;
using ShopBridgeApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridgeApp.Services
{
    public class ShopBridgeAppService : IShopBridgeAppService
    {
        
        private readonly IShopBridgeItemRepository shopBridgeItemRepository;

        public ShopBridgeAppService(IShopBridgeItemRepository shopBridgeItemRepository)
        {
            this.shopBridgeItemRepository = shopBridgeItemRepository;
        }
        public async Task<List<ShopBridgeItemProduct>> GetShopBridgeItems()
        {
            return await shopBridgeItemRepository.GetShopBridgeItems();
        }
    }
}
