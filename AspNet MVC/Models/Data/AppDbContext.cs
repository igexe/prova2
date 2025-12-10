using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace Models.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cotacao> Cotacoes { get; set; }
    public DbSet<CotacaoItem> CotacaoItens { get; set; }
    public DbSet<CotacaoParticipante> CotacaoParticipantes { get; set; }
    public DbSet<CotacaoPreco> CotacaoPrecos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

// Mapeamento das entidades para as tabelas singulares
modelBuilder.Entity<Usuario>().ToTable("Usuario");
modelBuilder.Entity<Fornecedor>().ToTable("Fornecedor");
modelBuilder.Entity<Produto>().ToTable("Produto");
modelBuilder.Entity<Cotacao>().ToTable("Cotacao");
modelBuilder.Entity<CotacaoItem>().ToTable("CotacaoItem");
modelBuilder.Entity<CotacaoParticipante>().ToTable("CotacaoParticipante");
modelBuilder.Entity<CotacaoPreco>().ToTable("CotacaoPreco");


        // Restrição: um preço por item + fornecedor
        modelBuilder.Entity<CotacaoPreco>()
            .HasIndex(p => new { p.CotacaoItemId, p.FornecedorId })
            .IsUnique();
    }
}