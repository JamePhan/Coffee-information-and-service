using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library.Models;

public partial class CoffeehouseSystemContext : DbContext
{
    public CoffeehouseSystemContext()
    {
    }

    public CoffeehouseSystemContext(DbContextOptions<CoffeehouseSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Following> Followings { get; set; }

    public virtual DbSet<GroupImage> GroupImages { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Waiting> Waitings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__46A222CD1DDA5097");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.ForgetCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("forget_code");
            entity.Property(e => e.IsBanned).HasColumnName("is_banned");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__43AA4141EDEB149C");

            entity.ToTable("Admin");

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.Account).WithMany(p => p.Admins)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Admin_Account");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.BannerId).HasName("PK__Banner__10373C34A38BEEDF");

            entity.ToTable("Banner");

            entity.Property(e => e.BannerId).HasColumnName("banner_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Banners)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Banner_User");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB850CB01850");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Avatar)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.Account).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Customer_Account");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__2370F727E96E7387");

            entity.ToTable("Event");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.GroupImageId).HasColumnName("groupImage_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.SeatCount).HasColumnName("seat_count");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.GroupImage).WithMany(p => p.Events)
                .HasForeignKey(d => d.GroupImageId)
                .HasConstraintName("FK_Event_GroupImage");

            entity.HasOne(d => d.Location).WithMany(p => p.Events)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Event_Location");

            entity.HasOne(d => d.User).WithMany(p => p.Events)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Event_User");
        });

        modelBuilder.Entity<Following>(entity =>
        {
            entity.HasKey(e => e.FollowingId).HasName("PK__Followin__E8FB4889A55E7282");

            entity.ToTable("Following");

            entity.Property(e => e.FollowingId).HasColumnName("following_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Followings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Following_Customer");

            entity.HasOne(d => d.User).WithMany(p => p.Followings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Following_User");
        });

        modelBuilder.Entity<GroupImage>(entity =>
        {
            entity.HasKey(e => e.GroupImageId).HasName("PK__GroupIma__62AFADC703E1D60A");

            entity.ToTable("GroupImage");

            entity.Property(e => e.GroupImageId).HasColumnName("groupImage_id");
            entity.Property(e => e.ImageId).HasColumnName("image_id");

            entity.HasOne(d => d.Image).WithMany(p => p.GroupImages)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK_GroupImage_Image");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Image__DC9AC95548552B02");

            entity.ToTable("Image");

            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.Image1)
                .HasMaxLength(255)
                .HasColumnName("image");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__771831EA46AF5A98");

            entity.ToTable("Location");

            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Locations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Location_User");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__4C27CCD88491AB97");

            entity.Property(e => e.NewsId).HasColumnName("news_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("date")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.GroupImageId).HasColumnName("groupImage_id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.GroupImage).WithMany(p => p.News)
                .HasForeignKey(d => d.GroupImageId)
                .HasConstraintName("FK_News_GroupImage");

            entity.HasOne(d => d.User).WithMany(p => p.News)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_News_User");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__C46A8A6F55858489");

            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.TicketCount).HasColumnName("ticket_count");

            entity.HasOne(d => d.Customer).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Schedule_Customer");

            entity.HasOne(d => d.Event).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_Schedule_Event");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__3E0DB8AF3148D4F5");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.GroupImageId).HasColumnName("groupImage_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.GroupImage).WithMany(p => p.Services)
                .HasForeignKey(d => d.GroupImageId)
                .HasConstraintName("FK_Service_GroupImage");

            entity.HasOne(d => d.User).WithMany(p => p.Services)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Service_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__B9BE370FAE54FE41");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Avatar)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.CoffeeShopName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("coffeeShopName");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.Account).WithMany(p => p.Users)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_User_Account");
        });

        modelBuilder.Entity<Waiting>(entity =>
        {
            entity.HasKey(e => e.WaitingId).HasName("PK__Waiting__24A0A3A35B8B9057");

            entity.ToTable("Waiting");

            entity.Property(e => e.WaitingId).HasColumnName("waiting_id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.CoffeeShopName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("coffeeShopName");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.Customer).WithMany(p => p.Waitings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Waiting_Customer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}