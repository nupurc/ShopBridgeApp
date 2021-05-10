using ShopBridgeApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridgeApp.Repository.Interface
{
    public interface IShopBridgeItemRepository
    {
        Task<List<ShopBridgeItemProduct>> GetShopBridgeItems();
    }
}
