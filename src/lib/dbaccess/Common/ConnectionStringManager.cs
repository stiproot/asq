using Microsoft.Extensions.Configuration;

namespace dbaccess.Common
{
    public class ConnectionStringManager : IConnectionStringManager
    {
        private readonly IConfiguration _config;

        public ConnectionStringManager(IConfiguration config)
            => this._config = config ?? throw new System.ArgumentNullException(nameof(config));

        public string GetConnectionString()
        {
            if(this._config == null) throw new System.InvalidOperationException("IConfiguration implementation not provided");

            string connectionString = this._config["Database:ConnectionString"];

            if(string.IsNullOrEmpty(connectionString)) throw new System.InvalidOperationException("Connection string is null or empty");

            return this._config["Database:ConnectionString"];
        }
    }
}