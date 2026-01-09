using Microsoft.EntityFrameworkCore;
using PedidosProcessamento.Domain.Entities;

namespace PedidosProcessamento.Infrastructure.Persistence;

public class PedidoDbContext : DbContext
{
    public PedidoDbContext(DbContextOptions<PedidoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pedido> Pedidos => Set<Pedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(o => o.Id);

            entity.Property(o => o.ClienteId)
                  .IsRequired();

            entity.Property(o => o.ValorTotal)
                  .HasPrecision(18, 2)
                  .IsRequired();

            entity.Property(o => o.Status)
                  .IsRequired();

            entity.Property(o => o.DataCriacao)
                  .IsRequired();
        });
    }
}
