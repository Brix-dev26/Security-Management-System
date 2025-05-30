﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDataContext))]
    [Migration("20250409071116_Newmigrations")]
    partial class Newmigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Campus", b =>
                {
                    b.Property<int>("CampusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CampusId"), 1L, 1);

                    b.Property<string>("CampusLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("campus_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CampusId");

                    b.ToTable("Campus");
                });

            modelBuilder.Entity("Domain.Entities.EmergencyEvent", b =>
                {
                    b.Property<int>("Emerg_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Emerg_ID"), 1L, 1);

                    b.Property<string>("ActionTaken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CampusId")
                        .HasColumnType("int");

                    b.Property<string>("CauseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfInvolvedPeople")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResolutionMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ResolutionStatus")
                        .HasColumnType("bit");

                    b.Property<int>("Sec_ID")
                        .HasColumnType("int");

                    b.Property<string>("Secutity_names")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("involvedpeople_names")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Emerg_ID");

                    b.HasIndex("CampusId");

                    b.HasIndex("Sec_ID");

                    b.ToTable("EmergencyEvents");
                });

            modelBuilder.Entity("Domain.Entities.Gate", b =>
                {
                    b.Property<int>("GateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GateId"), 1L, 1);

                    b.Property<int>("CampusId")
                        .HasColumnType("int");

                    b.Property<string>("gate_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GateId");

                    b.HasIndex("CampusId");

                    b.ToTable("Gate");
                });

            modelBuilder.Entity("Domain.Entities.InvolvedParty", b =>
                {
                    b.Property<int>("InvolvedPartyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvolvedPartyId"), 1L, 1);

                    b.Property<int>("Emerg_ID")
                        .HasColumnType("int");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonalId")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Sec_ID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("InvolvedPartyId");

                    b.HasIndex("Emerg_ID");

                    b.HasIndex("Sec_ID");

                    b.ToTable("InvolvedParties");
                });

            modelBuilder.Entity("Domain.Entities.LogEntry", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogId"), 1L, 1);

                    b.Property<int>("CampusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EntryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExitTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GateId")
                        .HasColumnType("int");

                    b.Property<int?>("GateId_exit")
                        .HasColumnType("int");

                    b.Property<int>("Sec_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Sec_ID_Exit")
                        .HasColumnType("int");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("int");

                    b.Property<string>("Visit_reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VisitorId")
                        .HasColumnType("int");

                    b.HasKey("LogId");

                    b.HasIndex("CampusId");

                    b.HasIndex("GateId");

                    b.HasIndex("Sec_ID");

                    b.HasIndex("VehicleId");

                    b.HasIndex("VisitorId");

                    b.ToTable("LogEntries");
                });

            modelBuilder.Entity("Domain.Entities.Permit", b =>
                {
                    b.Property<int>("PermitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermitId"), 1L, 1);

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PermitType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sec_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VehicleId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("VisitorId")
                        .HasColumnType("int");

                    b.HasKey("PermitId");

                    b.HasIndex("Sec_ID");

                    b.HasIndex("VehicleId");

                    b.HasIndex("VisitorId");

                    b.ToTable("Permits");
                });

            modelBuilder.Entity("Domain.Entities.SecurityStaff", b =>
                {
                    b.Property<int>("Sec_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Sec_ID"), 1L, 1);

                    b.Property<int>("CampusId")
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GateId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("NationalIdCard")
                        .HasColumnType("bigint");

                    b.Property<long>("Password")
                        .HasColumnType("bigint");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShiftTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Sec_ID");

                    b.HasIndex("CampusId");

                    b.HasIndex("GateId");

                    b.ToTable("SecurityStaffs");
                });

            modelBuilder.Entity("Domain.Entities.StaffManager", b =>
                {
                    b.Property<int>("StaffManagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffManagerId"), 1L, 1);

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Password")
                        .HasColumnType("bigint");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("StaffManagerId");

                    b.ToTable("StaffManagers");
                });

            modelBuilder.Entity("Domain.Entities.StaffManagerCampus", b =>
                {
                    b.Property<int>("StaffManagerId")
                        .HasColumnType("int");

                    b.Property<int>("CampusId")
                        .HasColumnType("int");

                    b.HasKey("StaffManagerId", "CampusId");

                    b.HasIndex("CampusId");

                    b.ToTable("StaffManagerCampuses");
                });

            modelBuilder.Entity("Domain.Entities.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"), 1L, 1);

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VisitorId")
                        .HasColumnType("int");

                    b.HasKey("VehicleId");

                    b.HasIndex("VisitorId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Domain.Entities.Visitor", b =>
                {
                    b.Property<int>("VisitorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VisitorId"), 1L, 1);

                    b.Property<string>("E_mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBlacklisted")
                        .HasColumnType("bit");

                    b.Property<long?>("NationalIdCard")
                        .HasColumnType("bigint");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("passport_no")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone_no")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VisitorId");

                    b.ToTable("Visitors");
                });

            modelBuilder.Entity("Domain.Entities.EmergencyEvent", b =>
                {
                    b.HasOne("Domain.Entities.Campus", "Campus")
                        .WithMany()
                        .HasForeignKey("CampusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.SecurityStaff", "SecurityStaff")
                        .WithMany("EmergencyEvents")
                        .HasForeignKey("Sec_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campus");

                    b.Navigation("SecurityStaff");
                });

            modelBuilder.Entity("Domain.Entities.Gate", b =>
                {
                    b.HasOne("Domain.Entities.Campus", "Campus")
                        .WithMany("Gates")
                        .HasForeignKey("CampusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campus");
                });

            modelBuilder.Entity("Domain.Entities.InvolvedParty", b =>
                {
                    b.HasOne("Domain.Entities.EmergencyEvent", "EmergencyEvent")
                        .WithMany("InvolvedParties")
                        .HasForeignKey("Emerg_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.SecurityStaff", "SecurityStaff")
                        .WithMany("InvolvedParties")
                        .HasForeignKey("Sec_ID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("EmergencyEvent");

                    b.Navigation("SecurityStaff");
                });

            modelBuilder.Entity("Domain.Entities.LogEntry", b =>
                {
                    b.HasOne("Domain.Entities.Campus", "Campus")
                        .WithMany()
                        .HasForeignKey("CampusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Gate", "Gate")
                        .WithMany()
                        .HasForeignKey("GateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.SecurityStaff", "SecurityStaff")
                        .WithMany()
                        .HasForeignKey("Sec_ID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.Visitor", "Visitor")
                        .WithMany()
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Campus");

                    b.Navigation("Gate");

                    b.Navigation("SecurityStaff");

                    b.Navigation("Vehicle");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("Domain.Entities.Permit", b =>
                {
                    b.HasOne("Domain.Entities.SecurityStaff", "SecurityStaff")
                        .WithMany("PermitsGranted")
                        .HasForeignKey("Sec_ID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Visitor", "Visitor")
                        .WithMany("Permits")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SecurityStaff");

                    b.Navigation("Vehicle");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("Domain.Entities.SecurityStaff", b =>
                {
                    b.HasOne("Domain.Entities.Campus", "Campus")
                        .WithMany()
                        .HasForeignKey("CampusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Gate", "Gate")
                        .WithMany()
                        .HasForeignKey("GateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Campus");

                    b.Navigation("Gate");
                });

            modelBuilder.Entity("Domain.Entities.StaffManagerCampus", b =>
                {
                    b.HasOne("Domain.Entities.Campus", "Campus")
                        .WithMany()
                        .HasForeignKey("CampusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.StaffManager", "StaffManager")
                        .WithMany("StaffManagerCampuses")
                        .HasForeignKey("StaffManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campus");

                    b.Navigation("StaffManager");
                });

            modelBuilder.Entity("Domain.Entities.Vehicle", b =>
                {
                    b.HasOne("Domain.Entities.Visitor", "Visitor")
                        .WithMany("Vehicles")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("Domain.Entities.Campus", b =>
                {
                    b.Navigation("Gates");
                });

            modelBuilder.Entity("Domain.Entities.EmergencyEvent", b =>
                {
                    b.Navigation("InvolvedParties");
                });

            modelBuilder.Entity("Domain.Entities.SecurityStaff", b =>
                {
                    b.Navigation("EmergencyEvents");

                    b.Navigation("InvolvedParties");

                    b.Navigation("PermitsGranted");
                });

            modelBuilder.Entity("Domain.Entities.StaffManager", b =>
                {
                    b.Navigation("StaffManagerCampuses");
                });

            modelBuilder.Entity("Domain.Entities.Visitor", b =>
                {
                    b.Navigation("Permits");

                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
