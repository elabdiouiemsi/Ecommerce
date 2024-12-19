﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ecommerce.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241219102004_haitham3")]
    partial class haitham3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Commande", b =>
                {
                    b.Property<int>("CommandeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommandeId"));

                    b.Property<string>("CommandeStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCommande")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPayer")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommandeId");

                    b.HasIndex("UserId");

                    b.ToTable("Commandes");
                });

            modelBuilder.Entity("CommandeItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CommandeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Prix")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ProduitId")
                        .HasColumnType("int");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommandeId");

                    b.HasIndex("ProduitId");

                    b.ToTable("CommandeItems");
                });

            modelBuilder.Entity("Consomateur", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Consomateurs");
                });

            modelBuilder.Entity("Panier", b =>
                {
                    b.Property<int>("PanierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PanierId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PanierId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Paniers");
                });

            modelBuilder.Entity("PanierItem", b =>
                {
                    b.Property<int>("PanierItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PanierItemId"));

                    b.Property<int>("PanierId")
                        .HasColumnType("int");

                    b.Property<decimal>("Prix")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ProduitId")
                        .HasColumnType("int");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.HasKey("PanierItemId");

                    b.HasIndex("PanierId");

                    b.HasIndex("ProduitId");

                    b.ToTable("PanierItems");
                });

            modelBuilder.Entity("Produit", b =>
                {
                    b.Property<int>("ProduitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProduitId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Prix")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.HasKey("ProduitId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("Commande", b =>
                {
                    b.HasOne("Consomateur", "Consomateur")
                        .WithMany("Commandes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consomateur");
                });

            modelBuilder.Entity("CommandeItem", b =>
                {
                    b.HasOne("Commande", "Commande")
                        .WithMany("CommandeItems")
                        .HasForeignKey("CommandeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Produit", "Produit")
                        .WithMany("CommandeItems")
                        .HasForeignKey("ProduitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Commande");

                    b.Navigation("Produit");
                });

            modelBuilder.Entity("Panier", b =>
                {
                    b.HasOne("Consomateur", "Consomateur")
                        .WithOne("Panier")
                        .HasForeignKey("Panier", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consomateur");
                });

            modelBuilder.Entity("PanierItem", b =>
                {
                    b.HasOne("Panier", "Panier")
                        .WithMany("PanierItems")
                        .HasForeignKey("PanierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Produit", "Produit")
                        .WithMany("PanierItems")
                        .HasForeignKey("ProduitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Panier");

                    b.Navigation("Produit");
                });

            modelBuilder.Entity("Produit", b =>
                {
                    b.HasOne("Category", "Category")
                        .WithMany("Produits")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Category", b =>
                {
                    b.Navigation("Produits");
                });

            modelBuilder.Entity("Commande", b =>
                {
                    b.Navigation("CommandeItems");
                });

            modelBuilder.Entity("Consomateur", b =>
                {
                    b.Navigation("Commandes");

                    b.Navigation("Panier")
                        .IsRequired();
                });

            modelBuilder.Entity("Panier", b =>
                {
                    b.Navigation("PanierItems");
                });

            modelBuilder.Entity("Produit", b =>
                {
                    b.Navigation("CommandeItems");

                    b.Navigation("PanierItems");
                });
#pragma warning restore 612, 618
        }
    }
}
