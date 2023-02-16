using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JWTAuthentication.Models.DB_User
{
    public partial class RwUserContext : DbContext
    {
        public RwUserContext()
        {
        }

        public RwUserContext(DbContextOptions<RwUserContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DepartmentDetail> DepartmentDetails { get; set; }
        public virtual DbSet<DeptCompare> DeptCompares { get; set; }
        public virtual DbSet<GmemberDetail> GmemberDetails { get; set; }
        public virtual DbSet<GrantDetail> GrantDetails { get; set; }
        public virtual DbSet<Granttsy> Granttsys { get; set; }
        public virtual DbSet<Granttsysdept> Granttsysdepts { get; set; }
        public virtual DbSet<GroupDesc> GroupDescs { get; set; }
        public virtual DbSet<GroupDetail> GroupDetails { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<IfmsysDetail> IfmsysDetails { get; set; }
        public virtual DbSet<Ipgroup> Ipgroups { get; set; }
        public virtual DbSet<Ipsyswork> Ipsysworks { get; set; }
        public virtual DbSet<Ipuser> Ipusers { get; set; }
        public virtual DbSet<Levelunit> Levelunits { get; set; }
        public virtual DbSet<MainGroup> MainGroups { get; set; }
        public virtual DbSet<MdeptDetail> MdeptDetails { get; set; }
        public virtual DbSet<PosSeclev> PosSeclevs { get; set; }
        public virtual DbSet<PosadminDetail> PosadminDetails { get; set; }
        public virtual DbSet<PosunderDetail> PosunderDetails { get; set; }
        public virtual DbSet<PosworkDetail> PosworkDetails { get; set; }
        public virtual DbSet<PwpolicyDetail> PwpolicyDetails { get; set; }
        public virtual DbSet<SdeptDetail> SdeptDetails { get; set; }
        public virtual DbSet<SdpetDetailBk> SdpetDetailBks { get; set; }
        public virtual DbSet<SpolicyDetail> SpolicyDetails { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UstatusDetail> UstatusDetails { get; set; }
        public virtual DbSet<UtemplateDetail> UtemplateDetails { get; set; }
        public virtual DbSet<Viewsysall> Viewsysalls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=172.29.11.30;user=ifmsa;password=infoma;database=RwUser");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("department_detail");

                entity.Property(e => e.Id)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Mid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("mid")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DeptCompare>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Dept_Compare");

                entity.Property(e => e.Bdsc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.Id)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Linkid)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("linkid")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<GmemberDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("gmember_detail");

                entity.Property(e => e.Delnote)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("delnote");

                entity.Property(e => e.Dept)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Filingop)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("filingop");

                entity.Property(e => e.Folderop)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("folderop");

                entity.Property(e => e.Grantdoc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("grantdoc");

                entity.Property(e => e.Grantnew)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("grantnew");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Importdoc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("importdoc");

                entity.Property(e => e.Loggedin).HasColumnName("loggedin");

                entity.Property(e => e.Movenoss)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("movenoss");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Otherdept)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("otherdept");

                entity.Property(e => e.Printdoc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("printdoc");

                entity.Property(e => e.Printrep)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("printrep");

                entity.Property(e => e.Scandoc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("scandoc");

                entity.Property(e => e.Seclev).HasColumnName("seclev");

                entity.Property(e => e.Updnote)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("updnote");

                entity.Property(e => e.Viewall)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("viewall");

                entity.Property(e => e.Viewdoc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("viewdoc");

                entity.Property(e => e.Viewlev).HasColumnName("viewlev");
            });

            modelBuilder.Entity<GrantDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("grant_detail");

                entity.HasIndex(e => e.Name, "IX_grant_detail")
                    .IsUnique();

                entity.Property(e => e.Granted)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("granted");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Granttsy>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("granttsys");

                entity.Property(e => e.DeptId)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("dept_id");

                entity.Property(e => e.GranttId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("grantt_id");

                entity.Property(e => e.SysId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("sys_id")
                    .IsFixedLength();

                entity.Property(e => e.UserLname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("user_lname");
            });

            modelBuilder.Entity<Granttsysdept>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("granttsysdept");

                entity.Property(e => e.DeptId)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("dept_id");

                entity.Property(e => e.GranttdeptId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("granttdept_id");

                entity.Property(e => e.SysId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("sys_id")
                    .IsFixedLength();
            });

            modelBuilder.Entity<GroupDesc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GroupDesc");

                entity.Property(e => e.Bdsc)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.BeginNode)
                    .IsRequired()
                    .HasMaxLength(13);

                entity.Property(e => e.Gorder)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("GOrder");

                entity.Property(e => e.GroupId)
                    .IsRequired()
                    .HasMaxLength(13)
                    .HasColumnName("GroupID");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.NextNode)
                    .IsRequired()
                    .HasMaxLength(13);

                entity.Property(e => e.PrevNode)
                    .IsRequired()
                    .HasMaxLength(13);

                entity.Property(e => e.SubGroup).HasMaxLength(13);

                entity.Property(e => e.SubLev)
                    .IsRequired()
                    .HasMaxLength(13);
            });

            modelBuilder.Entity<GroupDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("group_detail");

                entity.Property(e => e.Delnote)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("delnote");

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

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Importdoc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("importdoc");

                entity.Property(e => e.Loggedin)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("loggedin");

                entity.Property(e => e.Movenoss)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("movenoss");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Otherdept)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("otherdept");

                entity.Property(e => e.Printdoc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("printdoc");

                entity.Property(e => e.Printrep)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("printrep");

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("remark");

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

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Group_member");

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.FromBid)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Mgroupid)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Mgroupname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NextNode)
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IfmsysDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ifmsys_detail");

                entity.Property(e => e.AuditA)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("audit_a")
                    .IsFixedLength();

                entity.Property(e => e.AuditD)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("audit_d")
                    .IsFixedLength();

                entity.Property(e => e.AuditE)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("audit_e")
                    .IsFixedLength();

                entity.Property(e => e.AuditG)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("audit_g")
                    .IsFixedLength();

                entity.Property(e => e.AuditL)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("audit_l")
                    .IsFixedLength();

                entity.Property(e => e.AuditP)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("audit_p")
                    .IsFixedLength();

                entity.Property(e => e.AuditV)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("audit_v")
                    .IsFixedLength();

                entity.Property(e => e.Dbsize).HasColumnName("dbsize");

                entity.Property(e => e.Dsn)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dsn");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pwd");

                entity.Property(e => e.Status)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.Uid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("uid");
            });

            modelBuilder.Entity<Ipgroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("IPgroup");

                entity.Property(e => e.Dept)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Flag)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("flag");

                entity.Property(e => e.Ipaddress)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("IPaddress");
            });

            modelBuilder.Entity<Ipsyswork>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("IPsyswork");

                entity.Property(e => e.Ipaddress)
                    .HasColumnType("text")
                    .HasColumnName("ipaddress");

                entity.Property(e => e.Syswork)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("syswork");
            });

            modelBuilder.Entity<Ipuser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("IPuser");

                entity.Property(e => e.Compname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("compname");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.Ipaddress)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("IPaddress");
            });

            modelBuilder.Entity<Levelunit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("levelunit");

                entity.Property(e => e.Childid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("childid");

                entity.Property(e => e.Parentid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("parentid");
            });

            modelBuilder.Entity<MainGroup>(entity =>
            {
                entity.HasKey(e => e.Groupid)
                    .HasName("PK_HeaderGroup");

                entity.ToTable("MainGroup");

                entity.Property(e => e.Groupid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("groupid");

                entity.Property(e => e.Groupname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("groupname");
            });

            modelBuilder.Entity<MdeptDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("mdept_detail");

                entity.Property(e => e.Id)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Pid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("pid")
                    .IsFixedLength();
            });

            modelBuilder.Entity<PosSeclev>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pos_seclev");

                entity.Property(e => e.PosLevel)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("pos_level")
                    .IsFixedLength();

                entity.Property(e => e.Seclev).HasColumnName("seclev");
            });

            modelBuilder.Entity<PosadminDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("posadmin_detail");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PosunderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("posunder_detail");

                entity.Property(e => e.Id)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PosworkDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("poswork_detail");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PwpolicyDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pwpolicy_detail");

                entity.Property(e => e.Pwmode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("pwmode");

                entity.Property(e => e.Pwvalue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pwvalue");
            });

            modelBuilder.Entity<SdeptDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("sdept_detail");

                entity.HasIndex(e => e.Id, "sdept_code")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Linkid)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("linkid")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SdpetDetailBk>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("sdpet_detail_bk");

                entity.Property(e => e.Id)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Linkid)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("linkid")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SpolicyDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("spolicy_detail");

                entity.Property(e => e.Badlogattm)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("badlogattm")
                    .IsFixedLength();

                entity.Property(e => e.Pwdage)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("pwdage")
                    .IsFixedLength();
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_detail");

                entity.HasIndex(e => new { e.Id, e.Lname }, "user_code")
                    .IsUnique();

                entity.Property(e => e.Ename)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ename");

                entity.Property(e => e.Esurname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("esurname");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Lname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lname");

                entity.Property(e => e.Lpwd)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lpwd");

                entity.Property(e => e.MDept)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("m_dept")
                    .IsFixedLength();

                entity.Property(e => e.Pname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("pname")
                    .IsFixedLength();

                entity.Property(e => e.PosAdmin)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("pos_admin")
                    .IsFixedLength();

                entity.Property(e => e.PosLevel)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("pos_level")
                    .IsFixedLength();

                entity.Property(e => e.PosReserve)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("pos_reserve")
                    .IsFixedLength();

                entity.Property(e => e.PosUnder)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("pos_under")
                    .IsFixedLength();

                entity.Property(e => e.PosWork)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("pos_work")
                    .IsFixedLength();

                entity.Property(e => e.SDept)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("s_dept")
                    .IsFixedLength();

                entity.Property(e => e.Tname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Tsurname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tsurname");

                entity.Property(e => e.UAddress)
                    .HasMaxLength(110)
                    .IsUnicode(false)
                    .HasColumnName("u_address");

                entity.Property(e => e.UAlwchgpwd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("u_alwchgpwd")
                    .IsFixedLength();

                entity.Property(e => e.UChgpwdnext)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("u_chgpwdnext")
                    .IsFixedLength();

                entity.Property(e => e.UEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("u_email")
                    .IsFixedLength();

                entity.Property(e => e.UFax)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("u_fax")
                    .IsFixedLength();

                entity.Property(e => e.UId1)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("u_id1")
                    .IsFixedLength();

                entity.Property(e => e.UId2)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("u_id2")
                    .IsFixedLength();

                entity.Property(e => e.UPause)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("u_pause")
                    .IsFixedLength();

                entity.Property(e => e.UPwdexpdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("u_pwdexpdate")
                    .IsFixedLength();

                entity.Property(e => e.UPwdexpday)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("u_pwdexpday")
                    .IsFixedLength();

                entity.Property(e => e.UStatus)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("u_status")
                    .IsFixedLength();

                entity.Property(e => e.UTel1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("u_tel1")
                    .IsFixedLength();

                entity.Property(e => e.UTel2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("u_tel2")
                    .IsFixedLength();

                entity.Property(e => e.UTime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("u_time")
                    .IsFixedLength();

                entity.Property(e => e.UTrack)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("u_track")
                    .IsFixedLength();
            });

            modelBuilder.Entity<UstatusDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ustatus_detail");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<UtemplateDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("utemplate_detail");

                entity.Property(e => e.Delnote)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("delnote");

                entity.Property(e => e.Filingop)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("filingop");

                entity.Property(e => e.Folderop)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("folderop");

                entity.Property(e => e.Grantdoc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("grantdoc");

                entity.Property(e => e.Grantnew)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("grantnew");

                entity.Property(e => e.Importdoc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("importdoc");

                entity.Property(e => e.Movenoss)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("movenoss");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Otherdept)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("otherdept");

                entity.Property(e => e.Printdoc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("printdoc");

                entity.Property(e => e.Printrep)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("printrep");

                entity.Property(e => e.Scandoc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("scandoc");

                entity.Property(e => e.Seclev).HasColumnName("seclev");

                entity.Property(e => e.Updnote)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("updnote");

                entity.Property(e => e.Viewall)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("viewall");

                entity.Property(e => e.Viewdoc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("viewdoc");

                entity.Property(e => e.Viewlev).HasColumnName("viewlev");
            });

            modelBuilder.Entity<Viewsysall>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("viewsysall");

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

                entity.Property(e => e.Fname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fname");

                entity.Property(e => e.Folderop)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("folderop");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("fullname");

                entity.Property(e => e.Grantdoc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("grantdoc");

                entity.Property(e => e.Grantnew)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("grantnew");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Importdoc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("importdoc");

                entity.Property(e => e.Lname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lname");

                entity.Property(e => e.Loggedin).HasColumnName("loggedin");

                entity.Property(e => e.Movenoss)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("movenoss");

                entity.Property(e => e.Otherdept)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("otherdept");

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Pname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("pname")
                    .IsFixedLength();

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

                entity.Property(e => e.Surname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("surname");

                entity.Property(e => e.Sysdsn)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sysdsn");

                entity.Property(e => e.Sysmainconnect)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("sysmainconnect")
                    .IsFixedLength();

                entity.Property(e => e.Sysname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("sysname");

                entity.Property(e => e.Syspwd)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("syspwd");

                entity.Property(e => e.Sysstatus)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("sysstatus")
                    .IsFixedLength();

                entity.Property(e => e.Sysuid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sysuid");

                entity.Property(e => e.Updnote)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("updnote");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usrid");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
