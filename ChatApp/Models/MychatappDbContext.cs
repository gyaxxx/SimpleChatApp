using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Models;

public partial class MychatappDbContext : DbContext
{
    public MychatappDbContext()
    {
    }

    public MychatappDbContext(DbContextOptions<MychatappDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblGroup> TblGroups { get; set; }

    public virtual DbSet<TblMessage> TblMessages { get; set; }

    public virtual DbSet<TblPrivateChat> TblPrivateChats { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserGroup> TblUserGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=GYAXXX;Initial Catalog=mychatappDB;Persist Security Info=True;User ID=sa; Password=nghia123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__tblGroup__149AF30A13DDB970");

            entity.ToTable("tblGroup");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.GroupDescription).HasMaxLength(100);
            entity.Property(e => e.GroupName).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasColumnName("isActive");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.TblGroups)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblGroup__isActi__398D8EEE");
        });

        modelBuilder.Entity<TblMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__tblMessa__C87C037C51E8FD35");

            entity.ToTable("tblMessage");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.ChatId).HasColumnName("ChatID");
            entity.Property(e => e.MessageText).HasMaxLength(200);
            entity.Property(e => e.SenderUserId).HasColumnName("SenderUserID");
            entity.Property(e => e.Timestampt).HasColumnType("datetime");

            entity.HasOne(d => d.Chat).WithMany(p => p.TblMessages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblMessag__ChatI__4CA06362");

            entity.HasOne(d => d.SenderUser).WithMany(p => p.TblMessages)
                .HasForeignKey(d => d.SenderUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblMessag__Times__4BAC3F29");
        });

        modelBuilder.Entity<TblPrivateChat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__tblPriva__A9FBE626F2E39D89");

            entity.ToTable("tblPrivateChat");

            entity.Property(e => e.ChatId).HasColumnName("ChatID");
            entity.Property(e => e.LastMessageId).HasColumnName("LastMessageID");
            entity.Property(e => e.User1Id).HasColumnName("User1ID");
            entity.Property(e => e.User2Id).HasColumnName("User2ID");

            entity.HasOne(d => d.LastMessage).WithMany(p => p.TblPrivateChats)
                .HasForeignKey(d => d.LastMessageId)
                .HasConstraintName("FK_MessageID");

            entity.HasOne(d => d.User1).WithMany(p => p.TblPrivateChatUser1s)
                .HasForeignKey(d => d.User1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblPrivat__LastM__47DBAE45");

            entity.HasOne(d => d.User2).WithMany(p => p.TblPrivateChatUser2s)
                .HasForeignKey(d => d.User2Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblPrivat__User2__48CFD27E");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tblUser__1788CCAC5EE38EAE");

            entity.ToTable("tblUser");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.HashedPassword).HasMaxLength(100);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.ProfilePictureUrl).HasColumnName("ProfilePictureURL");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        modelBuilder.Entity<TblUserGroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblUserGroup");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.JoinedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserRole).HasMaxLength(20);

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblUserGr__UserR__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
