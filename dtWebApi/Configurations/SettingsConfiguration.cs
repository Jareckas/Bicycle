using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dtWebApi.Models;

public class SettingsConfiguration : IEntityTypeConfiguration<Settings>
{
    public void Configure(EntityTypeBuilder<Settings> builder)
    {
        builder.ToTable("Settings");

        builder.HasKey(s => s.UserId);

        builder.Property(s => s.UserId)
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(s => s.UserName)
            .HasMaxLength(256)
            .HasColumnType("varchar(256)");

        // Define foreign key relationship
        builder.HasOne(s => s.User)
            .WithOne()
            .HasForeignKey<Settings>(s => s.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}