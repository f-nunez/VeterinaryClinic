﻿// <auto-generated />
using System;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230320201806_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate.AppNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("NotificationId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ReadOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("NotificationId");

                    b.ToTable("AppNotifications");
                });

            modelBuilder.Entity("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationRoleAggregate.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationRoles");
                });

            modelBuilder.Entity("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserAggregate.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserRoleAggregate.ApplicationUserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("ApplicationUserRoles");
                });

            modelBuilder.Entity("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("NotificationEvent")
                        .HasColumnType("int");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("TriggeredByUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TriggeredByUserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate.AppNotification", b =>
                {
                    b.HasOne("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Notification", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Notification");
                });

            modelBuilder.Entity("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserRoleAggregate.ApplicationUserRole", b =>
                {
                    b.HasOne("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationRoleAggregate.ApplicationRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserAggregate.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Notification", b =>
                {
                    b.HasOne("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserAggregate.ApplicationUser", "TriggeredByUser")
                        .WithMany()
                        .HasForeignKey("TriggeredByUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TriggeredByUser");
                });

            modelBuilder.Entity("Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserAggregate.ApplicationUser", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
