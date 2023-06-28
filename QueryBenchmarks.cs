using BenchmarkDotNet.Attributes;
using Dapper;
using Microsoft.EntityFrameworkCore;
using ORMShowdown_NET8.Models;

namespace ORMShowdown_NET8
{
    [MemoryDiagnoser(false)]
    public class QueryBenchmarks
    {
        private EFCoreDbContext _efContext = null!;
        private Product _product = null!;

        [GlobalSetup]
        public void Setup()
        {
            _efContext = new();
            var randomNum = new Random().Next(1, 99);
            _product = _efContext.Products.First(x => x.Id == randomNum);
        }

        [Benchmark]
        public async Task<Product?> EF_FirstOrDefault()
        {
            var context = new EFCoreDbContext();
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == _product.Id);
            return product;
        }

        [Benchmark]
        public async Task<Product?> EF_AsNoTracking_FirstOrDefault()
        {
            var context = new EFCoreDbContext();
            var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == _product.Id);
            return product;
        }

        [Benchmark]
        public async Task<ProductDto?> EF_SqlQuery_FirstOrDefault()
        {
            var context = new EFCoreDbContext();
            var product = await context.Database.SqlQueryRaw<ProductDto>($"SELECT * FROM Products WHERE Id = {_product.Id}").FirstOrDefaultAsync();
            return product;
        }

        [Benchmark]
        public async Task<Product> Dapper_FirstOrDefault()
        {
            var context = new DapperContext();
            var product = await context.CreateConnection().QueryFirstOrDefaultAsync<Product>($"SELECT * FROM Products WHERE Id = {_product.Id}");
            return product;
        }
    }
}
