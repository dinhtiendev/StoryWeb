﻿using System;
using DataAccess.Models;
using DataAccess.Seeding;
using Microsoft.EntityFrameworkCore;
using StoryAPI.Models;

namespace DataAccess.DbContexts
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryCategory> StoryCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoryCategory>()
            .HasKey(sc => new { sc.CategoryId, sc.StoryId });
            modelBuilder.Entity<Favourite>()
            .HasKey(sc => new { sc.UserId, sc.StoryId });

            SeedingData.Seeding(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}

