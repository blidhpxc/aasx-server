﻿// <auto-generated />
using AasxServerDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AasxServerStandardBib.Migrations.Postgres
{
    [DbContext(typeof(PostgreAasContext))]
    [Migration("20230904082927_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AasxServer.AASXSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AASX")
                        .HasColumnType("text");

                    b.Property<long>("AASXNum")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AASXNum");

                    b.ToTable("AASXSets");
                });

            modelBuilder.Entity("AasxServer.AASSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("AASXNum")
                        .HasColumnType("bigint");

                    b.Property<string>("AASId")
                        .HasColumnType("text");

                    b.Property<long>("AASNum")
                        .HasColumnType("bigint");

                    b.Property<string>("GlobalAssetId")
                        .HasColumnType("text");

                    b.Property<string>("AssetKind")
                        .HasColumnType("text");

                    b.Property<string>("IdShort")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AASNum");

                    b.ToTable("AASSets");
                });

            modelBuilder.Entity("AasxServer.DBConfigSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("AASXCount")
                        .HasColumnType("bigint");

                    b.Property<long>("AASCount")
                        .HasColumnType("bigint");

                    b.Property<long>("SMECount")
                        .HasColumnType("bigint");

                    b.Property<long>("SubmodelCount")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("DBConfigSets");
                });

            modelBuilder.Entity("AasxServer.DoubleValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Annotation")
                        .HasColumnType("text");

                    b.Property<long>("ParentSMENum")
                        .HasColumnType("bigint");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ParentSMENum");

                    b.ToTable("DValueSets");
                });

            modelBuilder.Entity("AasxServer.IntValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Annotation")
                        .HasColumnType("text");

                    b.Property<long>("ParentSMENum")
                        .HasColumnType("bigint");

                    b.Property<long>("Value")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ParentSMENum");

                    b.ToTable("IValueSets");
                });

            modelBuilder.Entity("AasxServer.SMESet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("IdShort")
                        .HasColumnType("text");

                    b.Property<long>("ParentSMENum")
                        .HasColumnType("bigint");

                    b.Property<long>("SMENum")
                        .HasColumnType("bigint");

                    b.Property<string>("SMEType")
                        .HasColumnType("text");

                    b.Property<string>("SemanticId")
                        .HasColumnType("text");

                    b.Property<long>("SMNum")
                        .HasColumnType("bigint");

                    b.Property<string>("ValueType")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SMENum");

                    b.HasIndex("SMNum");

                    b.ToTable("SMESets");
                });

            modelBuilder.Entity("AasxServer.StringValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Annotation")
                        .HasColumnType("text");

                    b.Property<long>("ParentSMENum")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentSMENum");

                    b.ToTable("SValueSets");
                });

            modelBuilder.Entity("AasxServer.SMSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("AASXNum")
                        .HasColumnType("bigint");

                    b.Property<long>("AASNum")
                        .HasColumnType("bigint");

                    b.Property<string>("IdShort")
                        .HasColumnType("text");

                    b.Property<string>("SemanticId")
                        .HasColumnType("text");

                    b.Property<string>("SMId")
                        .HasColumnType("text");

                    b.Property<long>("SMNum")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SMNum");

                    b.ToTable("SMSets");
                });
#pragma warning restore 612, 618
        }
    }
}
