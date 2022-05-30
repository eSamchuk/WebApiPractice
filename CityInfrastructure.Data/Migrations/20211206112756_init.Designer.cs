﻿// <auto-generated />
using System;
using CityInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CityInfrastructure.Data.Migrations
{
    [DbContext(typeof(InfrastructureDbContext))]
    [Migration("20211206112756_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CityInfrastructure.Data.Model.Common.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.Common.ScheduleRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ArriveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("StationId");

                    b.ToTable("ScheduleRecords");
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.MunicipalTransport.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("RouteTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int?>("StationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteTypeId");

                    b.HasIndex("ScheduleId")
                        .IsUnique();

                    b.HasIndex("StationId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.MunicipalTransport.RouteType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RouteTypes");
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.MunicipalTransport.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longtitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RouteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Station");
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.SubwayLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SubwayLines");
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.SubwayStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ConstructionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Depth")
                        .HasColumnType("int");

                    b.Property<int?>("IntersectingStationId")
                        .HasColumnType("int");

                    b.Property<int>("LineId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IntersectingStationId")
                        .IsUnique()
                        .HasFilter("[IntersectingStationId] IS NOT NULL");

                    b.HasIndex("LineId");

                    b.ToTable("SubwayStations");
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.SubwayStationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubwayStationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubwayStationId");

                    b.ToTable("SubwayStationStatuses");
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.Common.ScheduleRecord", b =>
                {
                    b.HasOne("CityInfrastructure.Data.Model.MunicipalTransport.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CityInfrastructure.Data.Model.Common.Schedule", "Schedule")
                        .WithMany("ScheduleRecords")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CityInfrastructure.Data.Model.MunicipalTransport.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.MunicipalTransport.Route", b =>
                {
                    b.HasOne("CityInfrastructure.Data.Model.MunicipalTransport.RouteType", "RouteType")
                        .WithMany("Routes")
                        .HasForeignKey("RouteTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CityInfrastructure.Data.Model.Common.Schedule", "Schedule")
                        .WithOne()
                        .HasForeignKey("CityInfrastructure.Data.Model.MunicipalTransport.Route", "ScheduleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CityInfrastructure.Data.Model.MunicipalTransport.Station", null)
                        .WithMany("PassingRoutes")
                        .HasForeignKey("StationId");
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.MunicipalTransport.Station", b =>
                {
                    b.HasOne("CityInfrastructure.Data.Model.MunicipalTransport.Route", null)
                        .WithMany("Stations")
                        .HasForeignKey("RouteId");
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.SubwayStation", b =>
                {
                    b.HasOne("CityInfrastructure.Data.Model.SubwayStation", "IntersectingStation")
                        .WithOne()
                        .HasForeignKey("CityInfrastructure.Data.Model.SubwayStation", "IntersectingStationId");

                    b.HasOne("CityInfrastructure.Data.Model.SubwayLine", "Line")
                        .WithMany("Stations")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CityInfrastructure.Data.Model.SubwayStationStatus", b =>
                {
                    b.HasOne("CityInfrastructure.Data.Model.SubwayStation", "SubwayStation")
                        .WithMany("Statuses")
                        .HasForeignKey("SubwayStationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
