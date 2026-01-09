using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PedidosProcessamento.Infrastructure.Persistence
{
    public class PedidoDbContextFactory : IDesignTimeDbContextFactory<PedidoDbContext>
    {
        public PedidoDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<PedidoDbContext>()
                .UseNpgsql(config.GetConnectionString("DefaultConnection"))
                .Options;

            return new PedidoDbContext(options);
        }
    }
}