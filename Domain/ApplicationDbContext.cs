using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, String>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //User
        public virtual DbSet<ApplicationUser> AppUsers { get; set; }

        //Post
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Style> Styles { get; set; }
        public virtual DbSet<TasteNote> TasteNotes { get; set; }
        public virtual DbSet<TechnicalData> TechnicalDatas { get; set; }

        //Ingredients
        public virtual DbSet<Ingredient> Ingredients { get; set; }

        //Recipes
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<MashStep> MashSteps { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(p => p.Posts)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>()
                .HasOne(t=>t.TasteNote)
                .WithOne(ad => ad.Post)
                .HasForeignKey<TasteNote>(ad => ad.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>()
                .HasOne(r => r.Recipe)
                .WithOne(p => p.Post)
                .HasForeignKey<Recipe>(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>()
                .HasOne(r => r.TechnicalData)
                .WithOne(p => p.Post)
                .HasForeignKey<TechnicalData>(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Recipe>()
                .HasMany(c => c.MashSteps)
                .WithOne(e => e.Recipe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Recipe>()
                .HasMany(c => c.Ingredients)
                .WithOne(e => e.Recipe)
                .OnDelete(DeleteBehavior.Cascade);     

        }
    }
}
