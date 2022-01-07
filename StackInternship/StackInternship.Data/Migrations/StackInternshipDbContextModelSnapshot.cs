﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StackInternship.Data.Entities;

namespace StackInternship.Data.Migrations
{
    [DbContext(typeof(StackInternshipDbContext))]
    partial class StackInternshipDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StackInternship.Data.Entities.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ResourceId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "prvi",
                            CreatedAt = new DateTime(2022, 1, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            ResourceId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Content = "drugi",
                            CreatedAt = new DateTime(2022, 1, 7, 10, 44, 3, 674, DateTimeKind.Local).AddTicks(8677),
                            ParentId = 1,
                            ResourceId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            Content = "treci",
                            CreatedAt = new DateTime(2022, 1, 7, 10, 44, 3, 674, DateTimeKind.Local).AddTicks(9701),
                            ParentId = 2,
                            ResourceId = 1,
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            Content = "super post!",
                            CreatedAt = new DateTime(2022, 1, 7, 10, 44, 3, 674, DateTimeKind.Local).AddTicks(9771),
                            ResourceId = 1,
                            UserId = 3
                        });
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.Downvote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ResourceId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("ResourceId");

                    b.HasIndex("UserId");

                    b.ToTable("Downvotes");
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Resources");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = 0,
                            Content = "prvi post",
                            CreatedAt = new DateTime(2022, 1, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Title = "post 1",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Category = 0,
                            Content = "drugi\n post",
                            CreatedAt = new DateTime(2022, 1, 7, 10, 44, 3, 674, DateTimeKind.Local).AddTicks(98),
                            Title = "post 2",
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            Category = 1,
                            Content = "t\nr\ne\nc\ni\npost",
                            CreatedAt = new DateTime(2022, 1, 7, 10, 44, 3, 674, DateTimeKind.Local).AddTicks(396),
                            Title = "dizajn radionica",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.ResourceView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.HasIndex("UserId");

                    b.ToTable("ResourceViews");
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.Upvote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ResourceId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("ResourceId");

                    b.HasIndex("UserId");

                    b.ToTable("Upvotes");
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeactivatedUntil")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("HashedPassword")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsOrganizer")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2022, 1, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            HashedPassword = new byte[] { 177, 9, 243, 187, 188, 36, 78, 184, 36, 65, 145, 126, 208, 109, 97, 139, 144, 8, 221, 9, 179, 190, 253, 27, 94, 7, 57, 76, 112, 106, 139, 185, 128, 177, 215, 120, 94, 89, 118, 236, 4, 155, 70, 223, 95, 19, 38, 175, 90, 46, 166, 209, 3, 253, 7, 201, 83, 133, 255, 171, 12, 172, 188, 134 },
                            IsOrganizer = false,
                            Username = "ivan"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2022, 1, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            HashedPassword = new byte[] { 177, 9, 243, 187, 188, 36, 78, 184, 36, 65, 145, 126, 208, 109, 97, 139, 144, 8, 221, 9, 179, 190, 253, 27, 94, 7, 57, 76, 112, 106, 139, 185, 128, 177, 215, 120, 94, 89, 118, 236, 4, 155, 70, 223, 95, 19, 38, 175, 90, 46, 166, 209, 3, 253, 7, 201, 83, 133, 255, 171, 12, 172, 188, 134 },
                            IsOrganizer = true,
                            Username = "marko"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2022, 1, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            HashedPassword = new byte[] { 177, 9, 243, 187, 188, 36, 78, 184, 36, 65, 145, 126, 208, 109, 97, 139, 144, 8, 221, 9, 179, 190, 253, 27, 94, 7, 57, 76, 112, 106, 139, 185, 128, 177, 215, 120, 94, 89, 118, 236, 4, 155, 70, 223, 95, 19, 38, 175, 90, 46, 166, 209, 3, 253, 7, 201, 83, 133, 255, 171, 12, 172, 188, 134 },
                            IsOrganizer = true,
                            Username = "ante"
                        });
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.Comment", b =>
                {
                    b.HasOne("StackInternship.Data.Entities.Models.Comment", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("StackInternship.Data.Entities.Models.Resource", "Resource")
                        .WithMany("Comments")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StackInternship.Data.Entities.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Resource");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.Downvote", b =>
                {
                    b.HasOne("StackInternship.Data.Entities.Models.Comment", "Comment")
                        .WithMany("Downvotes")
                        .HasForeignKey("CommentId");

                    b.HasOne("StackInternship.Data.Entities.Models.Resource", "Resource")
                        .WithMany("Downvotes")
                        .HasForeignKey("ResourceId");

                    b.HasOne("StackInternship.Data.Entities.Models.User", "User")
                        .WithMany("Downvotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("Resource");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.Resource", b =>
                {
                    b.HasOne("StackInternship.Data.Entities.Models.User", "User")
                        .WithMany("Resources")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.ResourceView", b =>
                {
                    b.HasOne("StackInternship.Data.Entities.Models.Resource", "Resource")
                        .WithMany("Views")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StackInternship.Data.Entities.Models.User", "User")
                        .WithMany("Views")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Resource");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.Upvote", b =>
                {
                    b.HasOne("StackInternship.Data.Entities.Models.Comment", "Comment")
                        .WithMany("Upvotes")
                        .HasForeignKey("CommentId");

                    b.HasOne("StackInternship.Data.Entities.Models.Resource", "Resource")
                        .WithMany("Upvotes")
                        .HasForeignKey("ResourceId");

                    b.HasOne("StackInternship.Data.Entities.Models.User", "User")
                        .WithMany("Upvotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("Resource");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.Comment", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Downvotes");

                    b.Navigation("Upvotes");
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.Resource", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Downvotes");

                    b.Navigation("Upvotes");

                    b.Navigation("Views");
                });

            modelBuilder.Entity("StackInternship.Data.Entities.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Downvotes");

                    b.Navigation("Resources");

                    b.Navigation("Upvotes");

                    b.Navigation("Views");
                });
#pragma warning restore 612, 618
        }
    }
}
