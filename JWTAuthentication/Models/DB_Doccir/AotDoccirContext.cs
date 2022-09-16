using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JWTAuthentication.Models.DB_Doccir
{
    public partial class AotDoccirContext : DbContext
    {
        public AotDoccirContext()
        {
        }

        public AotDoccirContext(DbContextOptions<AotDoccirContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auditlog> Auditlogs { get; set; }
        public virtual DbSet<Dbuser> Dbusers { get; set; }
        public virtual DbSet<Deptment> Deptments { get; set; }
        public virtual DbSet<DoccirDetail> DoccirDetails { get; set; }
        public virtual DbSet<Docdept> Docdepts { get; set; }
        public virtual DbSet<Docgroup> Docgroups { get; set; }
        public virtual DbSet<Docgrp> Docgrps { get; set; }
        public virtual DbSet<Docpriority> Docpriorities { get; set; }
        public virtual DbSet<Docstatus> Docstatuses { get; set; }
        public virtual DbSet<Docsubgroup> Docsubgroups { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Drawer> Drawers { get; set; }
        public virtual DbSet<Filetype> Filetypes { get; set; }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<Regbook> Regbooks { get; set; }
        public virtual DbSet<Sysinfo> Sysinfos { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Viewer> Viewers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=172.29.11.30;Database=AotDoccir;user id=ifmsa; Password=infoma");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auditlog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("auditlog");

                entity.Property(e => e.Auditdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("auditdate");

                entity.Property(e => e.Audittime)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("audittime");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.Dept)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Docuname)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("docuname");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.Entrydate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("entrydate");

                entity.Property(e => e.Extension)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("extension");

                entity.Property(e => e.Filetype).HasColumnName("filetype");

                entity.Property(e => e.FoldCode)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("fold_code");

                entity.Property(e => e.Granted)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("granted");

                entity.Property(e => e.Grp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("grp");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Nossobj).HasColumnName("nossobj");

                entity.Property(e => e.Originator)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("originator");

                entity.Property(e => e.Origname)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("origname");

                entity.Property(e => e.Pages).HasColumnName("pages");

                entity.Property(e => e.Seclev).HasColumnName("seclev");

                entity.Property(e => e.Storage)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("storage");

                entity.Property(e => e.Usraction)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("usraction");

                entity.Property(e => e.Usrdept)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("usrdept");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usrid");

                entity.Property(e => e.Usrscaned)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("usrscaned");

                entity.Property(e => e.Usrtype)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("usrtype");
            });

            modelBuilder.Entity<Dbuser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbuser");

                entity.Property(e => e.Delnote)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("delnote");

                entity.Property(e => e.Dept)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Filingop)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("filingop");

                entity.Property(e => e.Folderop)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("folderop");

                entity.Property(e => e.Grantdoc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("grantdoc");

                entity.Property(e => e.Grantnew)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("grantnew");

                entity.Property(e => e.Importdoc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("importdoc");

                entity.Property(e => e.Loggedin).HasColumnName("loggedin");

                entity.Property(e => e.Movenoss)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("movenoss");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Otherdept)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("otherdept");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Printdoc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("printdoc");

                entity.Property(e => e.Printrep)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("printrep");

                entity.Property(e => e.Scandoc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("scandoc");

                entity.Property(e => e.Seclev).HasColumnName("seclev");

                entity.Property(e => e.Updnote)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("updnote");

                entity.Property(e => e.Viewall)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("viewall");

                entity.Property(e => e.Viewdoc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("viewdoc");

                entity.Property(e => e.Viewlev).HasColumnName("viewlev");
            });

            modelBuilder.Entity<Deptment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("deptment");

                entity.Property(e => e.Dept)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DoccirDetail>(entity =>
            {
                entity.HasKey(e => e.DoccirIfmid);

                entity.ToTable("doccir_detail");

                entity.Property(e => e.DoccirIfmid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("doccir_ifmid");

                entity.Property(e => e.DoccirCategory)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("doccir_category");

                entity.Property(e => e.DoccirDep)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("doccir_dep");

                entity.Property(e => e.DoccirDocdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("doccir_docdate");

                entity.Property(e => e.DoccirDocid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("doccir_docid");

                entity.Property(e => e.DoccirDsc)
                    .HasColumnType("text")
                    .HasColumnName("doccir_dsc");

                entity.Property(e => e.DoccirEnd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("doccir_end");

                entity.Property(e => e.DoccirExpire)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("doccir_expire");

                entity.Property(e => e.DoccirGrp)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("doccir_grp");

                entity.Property(e => e.DoccirInput)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("doccir_input");

                entity.Property(e => e.DoccirLevel)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("doccir_level");

                entity.Property(e => e.DoccirLocation)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("doccir_location");

                entity.Property(e => e.DoccirOrgdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("doccir_orgdate");

                entity.Property(e => e.DoccirOrgid)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("doccir_orgid");

                entity.Property(e => e.DoccirOrgname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("doccir_orgname");

                entity.Property(e => e.DoccirOwner)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("doccir_owner");

                entity.Property(e => e.DoccirPriority)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("doccir_priority");

                entity.Property(e => e.DoccirRemark)
                    .HasColumnType("text")
                    .HasColumnName("doccir_remark");

                entity.Property(e => e.DoccirSecret)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("doccir_secret");

                entity.Property(e => e.DoccirStart)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("doccir_start");

                entity.Property(e => e.DoccirStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("doccir_status");

                entity.Property(e => e.DoccirSubgrp)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("doccir_subgrp");

                entity.Property(e => e.DoccirSubject)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("doccir_subject");
            });

            modelBuilder.Entity<Docdept>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("docdept");

                entity.Property(e => e.Dsc)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.Id)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Docgroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("docgroup");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Docgrp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("docgrp");

                entity.Property(e => e.Drawer)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("drawer");

                entity.Property(e => e.Grp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("grp");
            });

            modelBuilder.Entity<Docpriority>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("docpriority");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Docstatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("docstatus");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Docsubgroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("docsubgroup");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Iddocgp)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("iddocgp")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("document");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.Dept)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Docuname)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("docuname");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.Entrydate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("entrydate");

                entity.Property(e => e.Extension)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("extension");

                entity.Property(e => e.Filetype).HasColumnName("filetype");

                entity.Property(e => e.FoldCode)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("fold_code");

                entity.Property(e => e.Granted)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("granted");

                entity.Property(e => e.Grp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("grp");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Nossobj).HasColumnName("nossobj");

                entity.Property(e => e.Originator)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("originator");

                entity.Property(e => e.Origname)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("origname");

                entity.Property(e => e.Pages).HasColumnName("pages");

                entity.Property(e => e.Seclev).HasColumnName("seclev");

                entity.Property(e => e.Storage)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("storage");
            });

            modelBuilder.Entity<Drawer>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("drawer");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.Dbconnect)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("dbconnect");

                entity.Property(e => e.Dbname)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("dbname");

                entity.Property(e => e.Dept)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.GrantedTo)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("granted_to");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Nossclass).HasColumnName("nossclass");

                entity.Property(e => e.Pathimage)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("pathimage");

                entity.Property(e => e.Seclev).HasColumnName("seclev");
            });

            modelBuilder.Entity<Filetype>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("filetype");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.Ext)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ext");

                entity.Property(e => e.Filetype1).HasColumnName("filetype");
            });

            modelBuilder.Entity<Folder>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("folder");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.FoldCode)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("fold_code");

                entity.Property(e => e.Seclev).HasColumnName("seclev");

                entity.Property(e => e.ViewCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("view_code");
            });

            modelBuilder.Entity<Regbook>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("regbook");

                entity.Property(e => e.Dept)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.Entrydate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("entrydate");

                entity.Property(e => e.Grp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("grp");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Originator)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("originator");

                entity.Property(e => e.Times)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("times");
            });

            modelBuilder.Entity<Sysinfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("sysinfo");

                entity.Property(e => e.CatId)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("cat_id");

                entity.Property(e => e.Category)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("category");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("topics");

                entity.Property(e => e.Dept)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Entrydate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("entrydate");

                entity.Property(e => e.FoldCode)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("fold_code");

                entity.Property(e => e.Granted)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("granted");

                entity.Property(e => e.Query)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("query");

                entity.Property(e => e.Seclev).HasColumnName("seclev");

                entity.Property(e => e.Title)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Viewer>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("viewer");

                entity.Property(e => e.Editor)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("editor");

                entity.Property(e => e.Ext)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ext");

                entity.Property(e => e.Filetype).HasColumnName("filetype");

                entity.Property(e => e.Progname)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("progname");

                entity.Property(e => e.Security).HasColumnName("security");

                entity.Property(e => e.Switches)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("switches");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
