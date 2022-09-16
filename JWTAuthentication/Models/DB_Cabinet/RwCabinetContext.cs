using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JWTAuthentication.Models.DB_Cabinet
{
    public partial class RwCabinetContext : DbContext
    {
        public RwCabinetContext()
        {
        }

        public RwCabinetContext(DbContextOptions<RwCabinetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cabinet> Cabinets { get; set; } = null!;
        public virtual DbSet<EcmsDept> EcmsDepts { get; set; } = null!;
        public virtual DbSet<EcmsPriority> EcmsPriorities { get; set; } = null!;
        public virtual DbSet<EcmsSecret> EcmsSecrets { get; set; } = null!;
        public virtual DbSet<EcmsStatus> EcmsStatuses { get; set; } = null!;
        public virtual DbSet<FlwCabinet> FlwCabinets { get; set; } = null!;
        public virtual DbSet<Positionpeadb> Positionpeadbs { get; set; } = null!;
        public virtual DbSet<TinyUrl> TinyUrls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=172.29.11.30;Database=RwCabinet;user id=ifmsa; Password=infoma");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabinet>(entity =>
            {
                entity.HasKey(e => e.RdbmsDatasource);

                entity.ToTable("cabinet");

                entity.Property(e => e.RdbmsDatasource)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_datasource")
                    .IsFixedLength();

                entity.Property(e => e.CabName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cab_name")
                    .IsFixedLength();

                entity.Property(e => e.NossDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("noss_desc")
                    .IsFixedLength();

                entity.Property(e => e.NossServername)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("noss_servername")
                    .IsFixedLength();

                entity.Property(e => e.RdbmsDatabasename)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_databasename")
                    .IsFixedLength();

                entity.Property(e => e.RdbmsDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_desc")
                    .IsFixedLength();

                entity.Property(e => e.RdbmsMainconnect)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_mainconnect")
                    .IsFixedLength();

                entity.Property(e => e.RdbmsPassword)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_password")
                    .IsFixedLength();

                entity.Property(e => e.RdbmsUsername)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_username")
                    .IsFixedLength();
            });

            modelBuilder.Entity<EcmsDept>(entity =>
            {
                entity.HasKey(e => e.DeptCode);

                entity.ToTable("ECMS_DEPT");

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .HasColumnName("Dept_Code");

                entity.Property(e => e.Active).HasMaxLength(1);

                entity.Property(e => e.DeptName)
                    .HasMaxLength(100)
                    .HasColumnName("Dept_Name");

                entity.Property(e => e.MatchBid)
                    .HasMaxLength(13)
                    .HasColumnName("Match_Bid");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Uri)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("uri");
            });

            modelBuilder.Entity<EcmsPriority>(entity =>
            {
                entity.HasKey(e => e.PriorityCode);

                entity.ToTable("ECMS_PRIORITY");

                entity.Property(e => e.PriorityCode)
                    .HasMaxLength(3)
                    .HasColumnName("PRIORITY_CODE");

                entity.Property(e => e.Active).HasMaxLength(1);

                entity.Property(e => e.MatchPriority)
                    .HasMaxLength(2)
                    .HasColumnName("MATCH_PRIORITY");

                entity.Property(e => e.PriorityName)
                    .HasMaxLength(50)
                    .HasColumnName("PRIORITY_NAME");
            });

            modelBuilder.Entity<EcmsSecret>(entity =>
            {
                entity.HasKey(e => e.SecretCode);

                entity.ToTable("ECMS_SECRET");

                entity.Property(e => e.SecretCode)
                    .HasMaxLength(3)
                    .HasColumnName("SECRET_CODE");

                entity.Property(e => e.Active).HasMaxLength(1);

                entity.Property(e => e.MatchSecret)
                    .HasMaxLength(2)
                    .HasColumnName("MATCH_SECRET");

                entity.Property(e => e.SecretName)
                    .HasMaxLength(50)
                    .HasColumnName("SECRET_NAME");
            });

            modelBuilder.Entity<EcmsStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ECMS_STATUS");

                entity.Property(e => e.Active).HasMaxLength(1);

                entity.Property(e => e.MatchStatus)
                    .HasMaxLength(2)
                    .HasColumnName("MATCH_STATUS");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(3)
                    .HasColumnName("STATUS_CODE");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .HasColumnName("STATUS_NAME");
            });

            modelBuilder.Entity<FlwCabinet>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("flwCabinet");

                entity.Property(e => e.CabName)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("cab_name");

                entity.Property(e => e.RdbmsDatabasename)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_databasename");

                entity.Property(e => e.RdbmsDatasource)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_datasource");

                entity.Property(e => e.RdbmsDesc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_desc");

                entity.Property(e => e.RdbmsMainconnect)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_mainconnect");

                entity.Property(e => e.RdbmsPassword)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_password");

                entity.Property(e => e.RdbmsStatus)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_status");

                entity.Property(e => e.RdbmsUsername)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("rdbms_username");
            });

            modelBuilder.Entity<Positionpeadb>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("POSITIONPEADB");

                entity.HasIndex(e => new { e.Bid, e.BidClass }, "IX_POSITIONPEADB")
                    .IsUnique();

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BDSC");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.BidClass)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Databasename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DATABASENAME");

                entity.Property(e => e.Ipdatabase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IPDATABASE");

                entity.Property(e => e.Ipserver)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IPSERVER");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Pathimage)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PATHIMAGE");

                entity.Property(e => e.Pathtemp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PATHTEMP");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USRID");
            });

            modelBuilder.Entity<TinyUrl>(entity =>
            {
                entity.HasKey(e => e.Tinyurl1)
                    .HasName("PK_tinyurl");

                entity.ToTable("tinyURL");

                entity.Property(e => e.Tinyurl1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tinyurl")
                    .IsFixedLength();

                entity.Property(e => e.Cabinet)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cabinet");

                entity.Property(e => e.Createdate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("createdate")
                    .IsFixedLength();

                entity.Property(e => e.Expiredate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("expiredate")
                    .IsFixedLength();

                entity.Property(e => e.Flag)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("flag");

                entity.Property(e => e.Ifmid)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("ifmid")
                    .IsFixedLength();

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("url");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
