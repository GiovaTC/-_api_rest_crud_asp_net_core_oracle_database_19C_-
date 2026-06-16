using Oracle.ManagedDataAccess.Client;

namespace ApiOracle19c.Data
{
    public class OracleContext
    {
        private readonly IConfiguration _configuration;

        public OracleContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public OracleConnection GetConnection()
        {
            return new OracleConnection(
                _configuration.GetConnectionString("OracleConnection"));
        }   
    }
}
