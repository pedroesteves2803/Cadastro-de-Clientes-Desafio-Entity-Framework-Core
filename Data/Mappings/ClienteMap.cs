using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroDeClientesDesafioEntityFrameworkCore.Models.Mappings;

public class ClienteMap : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Cliente");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.Nome)
            .HasColumnName("Nome")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasColumnName("Email")
            .HasMaxLength(150)
            .IsRequired();
        
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.Telefone)
            .HasColumnName("Telefone")
            .HasMaxLength(20)
            .HasDefaultValue(null);
        
        builder.Property(x => x.DataDeCadastro)
            .HasColumnName("DataCadastro")
            .HasColumnType("datetime2")
            .IsRequired();
    }
}