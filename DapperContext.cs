using Microsoft.Data.SqlClient;
using System.Data;

namespace ORMShowdown_NET8
{
    public class DapperContext
    {
        public DapperContext()
        {
        }

        public IDbConnection CreateConnection() => new SqlConnection("Data Source=TONI-BOSHKOVSKI; Initial Catalog=ORMShowdown; MultipleActiveResultSets=true; Integrated Security=true; TrustServerCertificate=true;");
    }
}
