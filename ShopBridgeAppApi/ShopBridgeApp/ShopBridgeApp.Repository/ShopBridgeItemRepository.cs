using Dapper;
using ShopBridgeApp.Models;
using ShopBridgeApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ShopBridgeApp.Repository
{
    public class ShopBridgeItemRepository : IShopBridgeItemRepository
    {
        private string sqlConnectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=ShopBridgeDb;Integrated Security=True;Pooling=False;Connect Timeout=30";
        public async Task<List<ShopBridgeItemProduct>> GetShopBridgeItems()
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                var ShopBridgeProducts = (await connection.QueryAsync<ShopBridgeItemProduct>("select Name from ProductDetails", commandType: CommandType.Text)).AsList();
                return ShopBridgeProducts;
            }
        }
    }
}
