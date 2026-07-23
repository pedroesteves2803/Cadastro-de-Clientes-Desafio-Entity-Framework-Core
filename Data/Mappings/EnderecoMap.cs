using CadastroDeClientesDesafioEntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class EnderecoMap : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.Cep)
            .HasColumnName("Cep")
            .HasMaxLength(8)
            .IsRequired();
        
        builder.Property(x => x.Rua)
            .HasColumnName("Rua")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Numero)
            .HasColumnName("Numero")
            .IsRequired();
        
        builder.Property(x => x.Bairro)
            .HasColumnName("Bairro")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Cidade)
            .HasColumnName("Cidade")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Estado)
            .HasColumnName("Estado")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.Enderecos);
    }
}