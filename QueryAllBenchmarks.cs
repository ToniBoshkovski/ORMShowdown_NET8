using BenchmarkDotNet.Attributes;
using Dapper;
using Microsoft.EntityFrameworkCore;
using ORMShowdown_NET8.Models;

namespace ORMShowdown_NET8
{
    [MemoryDiagnoser(false)]
    public class QueryAllBenchmarks
    {
        private EFCoreDbContext _efContext = null!;
        private DapperContext _dapperContext = null!;

        [GlobalSetup]
        public void Setup()
        {
            _efContext = new();
            _dapperContext = new();
        }

        [Benchmark]
        public async Task<List<Product>> EF_ToList()
        {
            var products = await _efContext.Products.ToListAsync();
            return products;
        }

        [Benchmark]
        public async Task<List<ProductDto>> EF_SqlQuery_ToList()
        {
            var products = await _efContext.Database.SqlQueryRaw<ProductDto>($"SELECT * FROM Products").ToListAsync();
            return products;
        }

        [Benchmark]
        public async Task<List<Product>> Dapper_ToList()
        {
            var products = (await _dapperContext.CreateConnection().QueryAsync<Product>("SELECT * FROM Products")).ToList();
            return products;
        }
    }
}
