using BenchmarkDotNet.Attributes;
using Dapper;
using Microsoft.EntityFrameworkCore;
using ORMShowdown_NET8.Models;

namespace ORMShowdown_NET8
{
    [MemoryDiagnoser(false)]
    public class QueryAllBenchmarks
    {
        [Benchmark]
        public async Task<List<Product>> EF_ToList()
        {
            var context = new EFCoreDbContext();
            var products = await context.Products.ToListAsync();
            return products;
        }

        [Benchmark]
        public async Task<List<Product>> EF_ToList_AsNoTracking()
        {
            var context = new EFCoreDbContext();
            var products = await context.Products.AsNoTracking().ToListAsync();
            return products;
        }

        [Benchmark]
        public async Task<List<ProductDto>> EF_SqlQuery_ToList()
        {
            var context = new EFCoreDbContext();
            var products = await context.Database.SqlQueryRaw<ProductDto>($"SELECT * FROM Products").ToListAsync();
            return products;
        }

        [Benchmark]
        public async Task<List<Product>> Dapper_ToList()
        {
            var context = new DapperContext();
            var products = (await context.CreateConnection().QueryAsync<Product>("SELECT * FROM Products")).ToList();
            return products;
        }
    }
}
