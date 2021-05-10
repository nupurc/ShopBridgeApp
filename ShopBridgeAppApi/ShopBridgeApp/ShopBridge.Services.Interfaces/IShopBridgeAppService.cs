using ShopBridgeApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Services.Interfaces
{
    public interface IShopBridgeAppService
    {
        Task<List<ShopBridgeItemProduct>> GetShopBridgeItems();
    }
}
