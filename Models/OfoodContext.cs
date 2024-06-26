using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OFoodBackendTask.Models;

public partial class OfoodContext : DbContext
{
    public OfoodContext()
    {
    }

    public OfoodContext(DbContextOptions<OfoodContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DeliveryFeedback> DeliveryFeedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderFeedback> OrderFeedbacks { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user id=amir;password=amir;database=Ofood", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeliveryFeedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("DeliveryFeedback", "ofood");

            entity.HasIndex(e => e.OrderRef, "order_ref");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FeedbackComment)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("feedback_comment");
            entity.Property(e => e.OrderRef)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("order_ref");
            entity.Property(e => e.Rating)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("rating");

            entity.HasOne(d => d.OrderRefNavigation).WithMany(p => p.DeliveryFeedbacks)
                .HasForeignKey(d => d.OrderRef)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("deliveryfeedback_ibfk_1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("Orders", "ofood");

            entity.HasIndex(e => e.StoreRef, "store_ref");

            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("order_id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("order_date");
            entity.Property(e => e.StoreRef)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("store_ref");

            entity.HasOne(d => d.StoreRefNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreRef)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("orders_ibfk_1");
        });

        modelBuilder.Entity<OrderFeedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("OrderFeedback", "ofood");

            entity.HasIndex(e => e.OrderRef, "order_ref");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FeedbackComment)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("feedback_comment");
            entity.Property(e => e.OrderRef)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("order_ref");
            entity.Property(e => e.Rating)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("rating");

            entity.HasOne(d => d.OrderRefNavigation).WithMany(p => p.OrderFeedbacks)
                .HasForeignKey(d => d.OrderRef)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("orderfeedback_ibfk_1");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PRIMARY");

            entity.ToTable("Store", "ofood");

            entity.Property(e => e.StoreId)
                .HasColumnType("int(11)")
                .HasColumnName("store_id");
            entity.Property(e => e.StoreName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("store_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
