﻿// <auto-generated />
using System;
using DevagramCSharp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DevagramCSharp.Migrations
{
    [DbContext(typeof(DevagramCSharpContext))]
    [Migration("20220919192709_Publicacao")]
    partial class Publicacao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DevagramCSharp.Models.Publicacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Publicacaos");
                });

            modelBuilder.Entity("DevagramCSharp.Models.Seguidor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("IdUsuarioSeguido")
                        .HasColumnType("int");

                    b.Property<int?>("IdUsuarioSeguidor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuarioSeguido");

                    b.HasIndex("IdUsuarioSeguidor");

                    b.ToTable("Seguidores");
                });

            modelBuilder.Entity("DevagramCSharp.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FotoPerfil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("DevagramCSharp.Models.Publicacao", b =>
                {
                    b.HasOne("DevagramCSharp.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("DevagramCSharp.Models.Seguidor", b =>
                {
                    b.HasOne("DevagramCSharp.Models.Usuario", "UsuarioSeguido")
                        .WithMany()
                        .HasForeignKey("IdUsuarioSeguido");

                    b.HasOne("DevagramCSharp.Models.Usuario", "UsuarioSeguidor")
                        .WithMany()
                        .HasForeignKey("IdUsuarioSeguidor");

                    b.Navigation("UsuarioSeguido");

                    b.Navigation("UsuarioSeguidor");
                });
#pragma warning restore 612, 618
        }
    }
}
