using API.Repos;
using Microsoft.Data.SqlClient;
using SqlKata.Execution;

namespace AffiGive_API_V1.Registers
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<QueryFactory>(sp =>
            {
                var env = sp.GetRequiredService<IWebHostEnvironment>();
                string connStr = configuration.GetConnectionString("Default");
                var conn = new SqlConnection(connStr);
                var compiler = new SqlKata.Compilers.SqlServerCompiler();
                return new QueryFactory(conn, compiler);
            });
            //Auth
            services.AddHttpContextAccessor();

            //Repos
            services.AddScoped<IUserRepository, UserRepository>();


            // Services

        }
    }
}
