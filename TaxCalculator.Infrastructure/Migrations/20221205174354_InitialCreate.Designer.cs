// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaxCalculator.Infrastructure;

#nullable disable

namespace TaxCalculator.Infrastructure.Migrations
{
    [DbContext(typeof(TaxDbContext))]
    [Migration("20221205174354_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TaxCalculator.Infrastructure.Entities.TaxBandEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LowerLimit")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<int>("UpperLimit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TaxBands");
                });
#pragma warning restore 612, 618
        }
    }
}
