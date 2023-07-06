using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Xml;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var createAtCollmn = GetMemberName((TimestampEntity<int> t) => t.CreatedAt);
            var lastUpdateCollmn = GetMemberName((TimestampEntity<int> t) => t.LastUpdated);

            //foreach (var et in modelBuilder.Model.GetEntityTypes())
            //{
            //    if (et.ClrType.IsSubclassOf(typeof(TimestampEntity<>)))
            //    {
            //        var createAt = et.FindProperty(createAtCollmn)!;
            //        var lastUpdate = et.FindProperty(lastUpdateCollmn)!;

            //        createAt.SetDefaultValueSql("GETDATE()");
            //        createAt.ValueGenerated = ValueGenerated.OnAdd;

            //        lastUpdate.SetDefaultValueSql("GETDATE()");
            //        lastUpdate.ValueGenerated = ValueGenerated.OnAddOrUpdate;

            //        Console.WriteLine($"In loop : {createAtCollmn}, {lastUpdate}");

            //    }
            //}

            modelBuilder.Entity<Comment>()
                .Property(e => e.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Comment>()
                .Property(e => e.LastUpdated)
                //.HasComputedColumnSql("GETDATE()");
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Post>()
                .Property(e => e.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Post>()
                .Property(e => e.LastUpdated)
                .ValueGeneratedOnAddOrUpdate()
                //.HasComputedColumnSql("GETDATE()");
                .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
        }

        private static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }


    }

}
