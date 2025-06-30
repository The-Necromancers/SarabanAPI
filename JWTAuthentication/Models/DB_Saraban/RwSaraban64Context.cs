using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class RwSaraban64Context : DbContext
    {
        public RwSaraban64Context()
        {
        }

        public RwSaraban64Context(DbContextOptions<RwSaraban64Context> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionCommand> ActionCommands { get; set; } = null!;
        public virtual DbSet<ActionMessage> ActionMessages { get; set; } = null!;
        public virtual DbSet<Actioninfo> Actioninfos { get; set; } = null!;
        public virtual DbSet<Actiontemplate> Actiontemplates { get; set; } = null!;
        public virtual DbSet<Basketinfo> Basketinfos { get; set; } = null!;
        public virtual DbSet<Bookgroup> Bookgroups { get; set; } = null!;
        public virtual DbSet<Borrow> Borrows { get; set; } = null!;
        public virtual DbSet<Bukeystore> Bukeystores { get; set; } = null!;
        public virtual DbSet<CcmailTemplate> CcmailTemplates { get; set; } = null!;
        public virtual DbSet<Clientinfo> Clientinfos { get; set; } = null!;
        public virtual DbSet<Clusterconnect> Clusterconnects { get; set; } = null!;
        public virtual DbSet<Constdate> Constdates { get; set; } = null!;
        public virtual DbSet<Controltable> Controltables { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<DefineEform> DefineEforms { get; set; } = null!;
        public virtual DbSet<Deligation> Deligations { get; set; } = null!;
        public virtual DbSet<DocSign> DocSigns { get; set; } = null!;
        public virtual DbSet<DocVer> DocVers { get; set; } = null!;
        public virtual DbSet<Docattach> Docattaches { get; set; } = null!;
        public virtual DbSet<Docfiling> Docfilings { get; set; } = null!;
        public virtual DbSet<Docfulltext> Docfulltexts { get; set; } = null!;
        public virtual DbSet<Docgroup> Docgroups { get; set; } = null!;
        public virtual DbSet<DraftMsg> DraftMsgs { get; set; } = null!;
        public virtual DbSet<DraftType> DraftTypes { get; set; } = null!;
        public virtual DbSet<Eform> Eforms { get; set; } = null!;
        public virtual DbSet<EformDataVer> EformDataVers { get; set; } = null!;
        public virtual DbSet<EformData> EformDatas { get; set; } = null!;
        public virtual DbSet<Emailtemplate> Emailtemplates { get; set; } = null!;
        public virtual DbSet<Followup> Followups { get; set; } = null!;
        public virtual DbSet<Followupdraftmsg> Followupdraftmsgs { get; set; } = null!;
        public virtual DbSet<GroupDesc> GroupDescs { get; set; } = null!;
        public virtual DbSet<GroupDescJson> GroupDescJsons { get; set; } = null!;
        public virtual DbSet<GroupOutBusiness> GroupOutBusinesses { get; set; } = null!;
        public virtual DbSet<Holiday> Holidays { get; set; } = null!;
        public virtual DbSet<IfmflowDepartment> IfmflowDepartments { get; set; } = null!;
        public virtual DbSet<Ifmtemplate> Ifmtemplates { get; set; } = null!;
        public virtual DbSet<InternalAction> InternalActions { get; set; } = null!;
        public virtual DbSet<Keystore> Keystores { get; set; } = null!;
        public virtual DbSet<LinkRouteflow> LinkRouteflows { get; set; } = null!;
        public virtual DbSet<Linkinfo> Linkinfos { get; set; } = null!;
        public virtual DbSet<ListEform> ListEforms { get; set; } = null!;
        public virtual DbSet<ListRouteflow> ListRouteflows { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Matchword> Matchwords { get; set; } = null!;
        public virtual DbSet<Metadatum> Metadata { get; set; } = null!;
        public virtual DbSet<OtherSiteFollowup> OtherSiteFollowups { get; set; } = null!;
        public virtual DbSet<Otherbid> Otherbids { get; set; } = null!;
        public virtual DbSet<Othersizefollowup> Othersizefollowups { get; set; } = null!;
        public virtual DbSet<Priorityinfo> Priorityinfos { get; set; } = null!;
        public virtual DbSet<QrcodeGen> QrcodeGens { get; set; } = null!;
        public virtual DbSet<Receivedoc> Receivedocs { get; set; } = null!;
        public virtual DbSet<Secretlev> Secretlevs { get; set; } = null!;
        public virtual DbSet<SendPerson> SendPeople { get; set; } = null!;
        public virtual DbSet<SignatureInfo> SignatureInfos { get; set; } = null!;
        public virtual DbSet<Specialcommand> Specialcommands { get; set; } = null!;
        public virtual DbSet<Stateinfo> Stateinfos { get; set; } = null!;
        public virtual DbSet<TempGroupdesc> TempGroupdescs { get; set; } = null!;
        public virtual DbSet<TmpDocattachEform> TmpDocattachEforms { get; set; } = null!;
        public virtual DbSet<TmpIfmeformused> TmpIfmeformuseds { get; set; } = null!;
        public virtual DbSet<TmpRegisterNoUsed> TmpRegisterNoUseds { get; set; } = null!;
        public virtual DbSet<UserCertPwd> UserCertPwds { get; set; } = null!;
        public virtual DbSet<Userinfo> Userinfos { get; set; } = null!;
        public virtual DbSet<Userpwd2> Userpwd2s { get; set; } = null!;
        public virtual DbSet<Userstatus> Userstatuses { get; set; } = null!;
        public virtual DbSet<ViewBasketinfo> ViewBasketinfos { get; set; } = null!;
        public virtual DbSet<ViewNewdocument> ViewNewdocuments { get; set; } = null!;
        public virtual DbSet<ViewNotworkinfo> ViewNotworkinfos { get; set; } = null!;
        public virtual DbSet<ViewReadaliasbasket> ViewReadaliasbaskets { get; set; } = null!;
        public virtual DbSet<ViewTransferpea> ViewTransferpeas { get; set; } = null!;
        public virtual DbSet<ViewUserinfo> ViewUserinfos { get; set; } = null!;
        public virtual DbSet<ViewWsubtype> ViewWsubtypes { get; set; } = null!;
        public virtual DbSet<Waitforsign> Waitforsigns { get; set; } = null!;
        public virtual DbSet<WorkEform> WorkEforms { get; set; } = null!;
        public virtual DbSet<Workinfo> Workinfos { get; set; } = null!;
        public virtual DbSet<Workinprocess> Workinprocesses { get; set; } = null!;
        public virtual DbSet<Worktype> Worktypes { get; set; } = null!;
        public virtual DbSet<Wsubtype> Wsubtypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=172.29.11.30;Database=RwSaraban64;user id=ifmsa; Password=infoma");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionCommand>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ActionCommand");

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.CommandBy)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("commandBy");

                entity.Property(e => e.Commanddate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("commanddate");

                entity.Property(e => e.Commandmessage)
                    .HasColumnType("text")
                    .HasColumnName("commandmessage");

                entity.Property(e => e.Commandtowho)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("commandtowho");

                entity.Property(e => e.Registerdte)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Registerno)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Registertime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ActionMessage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ActionMessage");

                entity.Property(e => e.Actiondate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("actiondate");

                entity.Property(e => e.Actionmsg)
                    .HasColumnType("text")
                    .HasColumnName("actionmsg");

                entity.Property(e => e.Actiontime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("actiontime");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("bid");

                entity.Property(e => e.Commandcode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("commandcode");

                entity.Property(e => e.Imagefile)
                    .IsUnicode(false)
                    .HasColumnName("imagefile");

                entity.Property(e => e.Presentto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("presentto");

                entity.Property(e => e.Signature).HasColumnType("text");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usrid");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wid");
            });

            modelBuilder.Entity<Actioninfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACTIONINFO");

                entity.HasIndex(e => e.Code, "IX_ACTIONINFO")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Dsc)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.OrderAction)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Actiontemplate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACTIONTEMPLATE");

                entity.Property(e => e.TmpBid)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.TmpDsc).HasColumnType("text");

                entity.Property(e => e.TmpUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Basketinfo>(entity =>
            {
                entity.HasKey(e => e.Bid);

                entity.ToTable("BASKETINFO");

                entity.HasIndex(e => e.Bid, "Basket_ID")
                    .IsUnique();

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Class)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CLASS");

                entity.Property(e => e.Deptcode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DocuName)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.HomeDir)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ItemNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RegisterNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SendNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Wfdsc)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Wfid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WFID");
            });

            modelBuilder.Entity<Bookgroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BOOKGROUP");

                entity.HasIndex(e => e.Code, "code")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Dsc)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Borrow>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BORROW");

                entity.HasIndex(e => new { e.Wid, e.Borrowdate, e.Bid }, "BORROW");

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Borrowdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("borrowdate");

                entity.Property(e => e.Borrowdept)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("borrowdept");

                entity.Property(e => e.Borrowname)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("borrowname");

                entity.Property(e => e.Itemno)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("remark");

                entity.Property(e => e.Returndate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("returndate");

                entity.Property(e => e.UserName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wid");

                entity.Property(e => e.Willreturndate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("willreturndate");
            });

            modelBuilder.Entity<Bukeystore>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BUKEYSTORE");

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Enddate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ENDDATE");

                entity.Property(e => e.Gencode).HasColumnName("GENCODE");

                entity.Property(e => e.Pwdencrypt)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PWDENCRYPT");

                entity.Property(e => e.Pwdread)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PWDREAD");

                entity.Property(e => e.Startdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STARTDATE");
            });

            modelBuilder.Entity<CcmailTemplate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CCMAIL_TEMPLATE");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.Bidcc)
                    .HasColumnType("ntext")
                    .HasColumnName("BIDCC");

                entity.Property(e => e.Bidccnon)
                    .HasColumnType("ntext")
                    .HasColumnName("BIDCCNON");

                entity.Property(e => e.Bidto)
                    .HasColumnType("ntext")
                    .HasColumnName("BIDTO");

                entity.Property(e => e.Bidtonon)
                    .HasColumnType("ntext")
                    .HasColumnName("BIDTONON");

                entity.Property(e => e.Ccname)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("CCNAME");

                entity.Property(e => e.Itemno).HasColumnName("ITEMNO");
            });

            modelBuilder.Entity<Clientinfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("clientinfo");

                entity.Property(e => e.ClientAlias)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("client_alias");

                entity.Property(e => e.ClientFullnameEn)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("client_fullname_en");

                entity.Property(e => e.ClientFullnameTh)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("client_fullname_th");
            });

            modelBuilder.Entity<Clusterconnect>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CLUSTERCONNECT");

                entity.Property(e => e.Bidclass)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("BIDCLASS");

                entity.Property(e => e.Bidconnect)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("BIDCONNECT");

                entity.Property(e => e.Senderclass)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("SENDERCLASS");

                entity.Property(e => e.Senderconnect)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("SENDERCONNECT");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WID");
            });

            modelBuilder.Entity<Constdate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CONSTDATE");

                entity.Property(e => e.Tdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TDATE");

                entity.Property(e => e.Tno).HasColumnName("TNO");

                entity.Property(e => e.Ttime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TTIME");
            });

            modelBuilder.Entity<Controltable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CONTROLTABLE");

                entity.Property(e => e.DeptOutNo)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DeptRegNo)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.LastWserialNo)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("COUNTRY");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_NAME");
            });

            modelBuilder.Entity<DefineEform>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DEFINE_EFORM");

                entity.Property(e => e.Category)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY")
                    .IsFixedLength();

                entity.Property(e => e.Deptcode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("DEPTCODE");

                entity.Property(e => e.EformStatus)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("EFORM_STATUS")
                    .IsFixedLength();

                entity.Property(e => e.Eformname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EFORMNAME");

                entity.Property(e => e.Eformno)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("EFORMNO")
                    .IsFixedLength();

                entity.Property(e => e.TargetPath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TARGET_PATH");
            });

            modelBuilder.Entity<Deligation>(entity =>
            {
                entity.HasKey(e => new { e.Usrid, e.Bid, e.ToUsrid });

                entity.ToTable("Deligation");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USRID");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.ToUsrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ToUSRID");

                entity.Property(e => e.FromDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ToDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocSign>(entity =>
            {
                entity.HasKey(e => new { e.Wid, e.Bid, e.Itemno, e.Usrid });

                entity.ToTable("DocSign");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("wid");

                entity.Property(e => e.Bid)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("bid");

                entity.Property(e => e.Itemno)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("itemno");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usrid");

                entity.Property(e => e.Filepath)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("filepath");

                entity.Property(e => e.Signature)
                    .HasColumnType("text")
                    .HasColumnName("signature");
            });

            modelBuilder.Entity<DocVer>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DOC_VER");

                entity.Property(e => e.DocvContenttype)
                    .HasMaxLength(200)
                    .HasColumnName("docv_contenttype");

                entity.Property(e => e.DocvData).HasColumnName("docv_data");

                entity.Property(e => e.DocvDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("docv_date");

                entity.Property(e => e.DocvId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("docv_id");

                entity.Property(e => e.DocvName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("docv_name");

                entity.Property(e => e.DocvTime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("docv_time");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("wid");
            });

            modelBuilder.Entity<Docattach>(entity =>
            {
                entity.HasKey(e => new { e.Wid, e.Bid, e.Itemno });

                entity.ToTable("DOCATTACH");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("wid");

                entity.Property(e => e.Bid)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("bid");

                entity.Property(e => e.Itemno)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("itemno");

                entity.Property(e => e.Actionmsg)
                    .HasColumnType("text")
                    .HasColumnName("actionmsg");

                entity.Property(e => e.Allowupdate)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("allowupdate");

                entity.Property(e => e.Attachdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("attachdate");

                entity.Property(e => e.Attachname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("attachname");

                entity.Property(e => e.Attachtime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("attachtime");

                entity.Property(e => e.Contextattach)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("contextattach");

                entity.Property(e => e.Linkwid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("linkwid");

                entity.Property(e => e.Userattach)
                    .HasMaxLength(180)
                    .IsUnicode(false)
                    .HasColumnName("userattach");
            });

            modelBuilder.Entity<Docfiling>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DOCFILING");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.FileDesc)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FILE_DESC");

                entity.Property(e => e.FileId)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("FILE_ID");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.Prioritycode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PRIORITYCODE");

                entity.Property(e => e.Status)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.YearEnd)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("YEAR_END");

                entity.Property(e => e.YearStart)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("YEAR_START");
            });

            modelBuilder.Entity<Docfulltext>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DOCFULLTEXT");

                entity.Property(e => e.Actionmsg)
                    .HasColumnType("text")
                    .HasColumnName("actionmsg");

                entity.Property(e => e.Attachname)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("attachname");

                entity.Property(e => e.Bid)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("bid");

                entity.Property(e => e.Itemno)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("itemno");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wid");
            });

            modelBuilder.Entity<Docgroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DOCGROUP");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("GROUP_ID");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GROUP_NAME");
            });

            modelBuilder.Entity<DraftMsg>(entity =>
            {
                entity.HasKey(e => e.DraftId);

                entity.ToTable("DraftMSG");

                entity.Property(e => e.DraftId).HasColumnName("Draft_ID");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Command).HasColumnType("text");

                entity.Property(e => e.CommandDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SignStatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SugCommand).HasColumnType("text");

                entity.Property(e => e.Thread)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Usrid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USRID");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DraftType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DraftType");

                entity.Property(e => e.Granted)
                    .HasMaxLength(140)
                    .IsUnicode(false)
                    .HasColumnName("granted");

                entity.Property(e => e.Prefix)
                    .HasMaxLength(140)
                    .IsUnicode(false)
                    .HasColumnName("prefix");

                entity.Property(e => e.Wid)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("wid");

                entity.Property(e => e.Wsubtype)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("wsubtype");

                entity.Property(e => e.Wtype)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("wtype");
            });

            modelBuilder.Entity<Eform>(entity =>
            {
                entity.ToTable("EFORM");

                entity.Property(e => e.EformId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("eform_id")
                    .IsFixedLength();

                entity.Property(e => e.Draft)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("draft");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("dsc");

                entity.Property(e => e.Granted)
                    .HasMaxLength(140)
                    .IsUnicode(false)
                    .HasColumnName("granted");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Pdffile)
                    .IsUnicode(false)
                    .HasColumnName("pdffile");

                entity.Property(e => e.Wsubtype)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("wsubtype");

                entity.Property(e => e.Wtype)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("wtype");

                entity.Property(e => e.Xml)
                    .HasColumnType("text")
                    .HasColumnName("xml");
            });

            modelBuilder.Entity<EformDataVer>(entity =>
            {
                entity.HasKey(e => e.EvId);

                entity.ToTable("EFORM_DATA_VER");

                entity.Property(e => e.EvId).HasColumnName("ev_id");

                entity.Property(e => e.EdData).HasColumnName("ed_data");

                entity.Property(e => e.EdId).HasColumnName("ed_id");

                entity.Property(e => e.EformId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("eform_id")
                    .IsFixedLength();

                entity.Property(e => e.EformXml).HasColumnName("eform_xml");

                entity.Property(e => e.EvDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ev_date");

                entity.Property(e => e.EvTime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ev_time");

                entity.Property(e => e.Pdffile)
                    .IsUnicode(false)
                    .HasColumnName("pdffile");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("wid");
            });

            modelBuilder.Entity<EformData>(entity =>
            {
                entity.HasKey(e => e.Wid);

                entity.ToTable("EFORM_DATA");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("wid");

                entity.Property(e => e.EdData).HasColumnName("ed_data");

                entity.Property(e => e.EdId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ed_id");

                entity.Property(e => e.EformId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("eform_id")
                    .IsFixedLength();

                entity.Property(e => e.EformXml).HasColumnName("eform_xml");

                entity.Property(e => e.Pdffile)
                    .IsUnicode(false)
                    .HasColumnName("pdffile");
            });

            modelBuilder.Entity<Emailtemplate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EMAILTEMPLATE");

                entity.Property(e => e.TmpBid).HasMaxLength(13);

                entity.Property(e => e.TmpCode).HasMaxLength(10);

                entity.Property(e => e.TmpUser).HasMaxLength(40);

                entity.Property(e => e.Tmpbcc).HasColumnType("text");

                entity.Property(e => e.Tmpcc).HasColumnType("text");

                entity.Property(e => e.Tmpname).HasMaxLength(100);

                entity.Property(e => e.Tmpto).HasColumnType("text");
            });

            modelBuilder.Entity<Followup>(entity =>
            {
                entity.HasKey(e => e.Wid);

                entity.ToTable("FOLLOWUP");

                entity.HasIndex(e => e.Wid, "followup_id")
                    .IsUnique();

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WID");

                entity.Property(e => e.ActionMsg).HasColumnType("text");

                entity.Property(e => e.Wserial)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Followupdraftmsg>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FOLLOWUPDRAFTMSG");

                entity.Property(e => e.Actionmsg).HasColumnType("text");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("bid");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wid");
            });

            modelBuilder.Entity<GroupDesc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GroupDesc");

                entity.Property(e => e.Bdsc).HasMaxLength(100);

                entity.Property(e => e.BeginNode).HasMaxLength(13);

                entity.Property(e => e.Gindent).HasColumnName("GIndent");

                entity.Property(e => e.Gorder)
                    .HasMaxLength(5)
                    .HasColumnName("GOrder");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(13)
                    .HasColumnName("GroupID");

                entity.Property(e => e.GroupName).HasMaxLength(100);

                entity.Property(e => e.NextNode).HasMaxLength(13);

                entity.Property(e => e.PrevNode).HasMaxLength(13);

                entity.Property(e => e.SubGroup).HasMaxLength(13);

                entity.Property(e => e.SubLev).HasMaxLength(13);
            });

            modelBuilder.Entity<GroupDescJson>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GroupDesc_Json");

                entity.Property(e => e.JsonText)
                    .IsUnicode(false)
                    .HasColumnName("json_text");
            });

            modelBuilder.Entity<GroupOutBusiness>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GroupOutBusiness");

                entity.Property(e => e.Bdsc).HasMaxLength(100);

                entity.Property(e => e.BeginNode).HasMaxLength(13);

                entity.Property(e => e.Gindent).HasColumnName("GIndent");

                entity.Property(e => e.Gorder)
                    .HasMaxLength(5)
                    .HasColumnName("GOrder");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(13)
                    .HasColumnName("GroupID");

                entity.Property(e => e.GroupName).HasMaxLength(60);

                entity.Property(e => e.NextNode).HasMaxLength(13);

                entity.Property(e => e.PrevNode).HasMaxLength(13);

                entity.Property(e => e.SubGroup).HasMaxLength(13);

                entity.Property(e => e.SubLev).HasMaxLength(13);
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("holiday");

                entity.Property(e => e.HDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("h_date");

                entity.Property(e => e.HDesc)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("h_desc");
            });

            modelBuilder.Entity<IfmflowDepartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("IFMFLOW_DEPARTMENT");

                entity.HasIndex(e => e.Deptcode, "DEPARTMENT_CODE")
                    .IsUnique();

                entity.Property(e => e.Deptcode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("deptcode");

                entity.Property(e => e.Deptname)
                    .HasMaxLength(105)
                    .IsUnicode(false)
                    .HasColumnName("deptname");

                entity.Property(e => e.Outnumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("outnumber");

                entity.Property(e => e.Password)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Prefixdept)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("prefixdept");
            });

            modelBuilder.Entity<Ifmtemplate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("IFMTEMPLATE");

                entity.HasIndex(e => e.TmpCode, "IFMTEMPLATE_KEY")
                    .IsUnique();

                entity.Property(e => e.TmpAttach).HasMaxLength(100);

                entity.Property(e => e.TmpBid).HasMaxLength(13);

                entity.Property(e => e.TmpCode).HasMaxLength(10);

                entity.Property(e => e.TmpDsc).HasColumnType("ntext");

                entity.Property(e => e.TmpOrigin).HasMaxLength(100);

                entity.Property(e => e.TmpOwner).HasMaxLength(100);

                entity.Property(e => e.TmpRefwid).HasMaxLength(100);

                entity.Property(e => e.TmpSubj).HasMaxLength(255);

                entity.Property(e => e.TmpUser).HasMaxLength(60);

                entity.Property(e => e.TmpWid).HasMaxLength(100);
            });

            modelBuilder.Entity<InternalAction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("InternalAction");

                entity.Property(e => e.Code).HasMaxLength(2);

                entity.Property(e => e.Dsc).HasMaxLength(50);
            });

            modelBuilder.Entity<Keystore>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("KEYSTORE");

                entity.Property(e => e.EffectiveDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("effectiveDate")
                    .IsFixedLength();

                entity.Property(e => e.ExpireDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("expireDate")
                    .IsFixedLength();

                entity.Property(e => e.Issuer)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("issuer");

                entity.Property(e => e.Privatekey)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("PRIVATEKEY");

                entity.Property(e => e.Publickey)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("PUBLICKEY");

                entity.Property(e => e.RequestDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("requestDate")
                    .IsFixedLength();

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("serial_number");

                entity.Property(e => e.SignatureAlgorithm)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("signature_algorithm");

                entity.Property(e => e.Subject)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("subject");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USRID")
                    .IsFixedLength();

                entity.Property(e => e.Version)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("version");
            });

            modelBuilder.Entity<LinkRouteflow>(entity =>
            {
                entity.HasKey(e => e.Linkid)
                    .HasName("PK_link_outside_app");

                entity.ToTable("link_routeflow");

                entity.Property(e => e.Linkid).HasColumnName("linkid");

                entity.Property(e => e.Actiondate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("actiondate");

                entity.Property(e => e.Actiontime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("actiontime");

                entity.Property(e => e.Appid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("appid");

                entity.Property(e => e.Refid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("refid");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wid");
            });

            modelBuilder.Entity<Linkinfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LINKINFO");

                entity.HasIndex(e => new { e.Sourcewid, e.Linkwid }, "LINKINFO_KEY")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Createdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CREATEDATE");

                entity.Property(e => e.Initdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("INITDATE");

                entity.Property(e => e.Inittime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("INITTIME");

                entity.Property(e => e.Itemno)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("ITEMNO");

                entity.Property(e => e.Linkbid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("LINKBID");

                entity.Property(e => e.Linkwid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LINKWID");

                entity.Property(e => e.Registerno)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("REGISTERNO");

                entity.Property(e => e.Senderbid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("SENDERBID");

                entity.Property(e => e.Sourcewid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SOURCEWID");
            });

            modelBuilder.Entity<ListEform>(entity =>
            {
                entity.HasKey(e => e.ListId);

                entity.ToTable("List_EFORM");

                entity.Property(e => e.ListId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("listID");

                entity.Property(e => e.Active)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("active");

                entity.Property(e => e.EformId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("eform_id")
                    .IsFixedLength();

                entity.Property(e => e.Rid)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("rid");
            });

            modelBuilder.Entity<ListRouteflow>(entity =>
            {
                entity.HasKey(e => e.Rid);

                entity.ToTable("list_routeflow");

                entity.Property(e => e.Rid)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("rid");

                entity.Property(e => e.Rdsc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("rdsc");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LOCATION");

                entity.HasIndex(e => new { e.Wid, e.Bid }, "LOCATION_KEY")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.Location1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WID");
            });

            modelBuilder.Entity<Matchword>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MATCHWORDS");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("bid");

                entity.Property(e => e.Category)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("category")
                    .IsFixedLength();

                entity.Property(e => e.Code).HasColumnName("CODE");

                entity.Property(e => e.Detailwords)
                    .HasColumnType("text")
                    .HasColumnName("detailwords");

                entity.Property(e => e.Mainwords)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mainwords");
            });

            modelBuilder.Entity<Metadatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("metadata");

                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("date_time");

                entity.Property(e => e.TableName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("table_name");

                entity.Property(e => e.Wid)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("wid");
            });

            modelBuilder.Entity<OtherSiteFollowup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OtherSiteFollowup");

                entity.Property(e => e.Actionmsg)
                    .HasColumnType("text")
                    .HasColumnName("actionmsg");

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bdsc");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("bid");

                entity.Property(e => e.Bidclass)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("bidclass");

                entity.Property(e => e.FlagDelete)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Itemno)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("itemno");

                entity.Property(e => e.Maxtime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maxtime");

                entity.Property(e => e.Receivedate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("receivedate");

                entity.Property(e => e.Receivetime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("receivetime");

                entity.Property(e => e.Registerdate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Registerno)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("registerno");

                entity.Property(e => e.Registertime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Statecode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Viewstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("viewstatus");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WID");
            });

            modelBuilder.Entity<Otherbid>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OTHERBID");

                entity.HasIndex(e => e.Code, "OTHERBID_key")
                    .IsUnique();

                entity.Property(e => e.Address1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address2");

                entity.Property(e => e.Address3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address3");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("bid");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Contect)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contect");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DSC");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tel");
            });

            modelBuilder.Entity<Othersizefollowup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Othersizefollowup");

                entity.Property(e => e.Actionmsg)
                    .HasColumnType("ntext")
                    .HasColumnName("actionmsg");

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bdsc");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("bid");

                entity.Property(e => e.Class)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("class")
                    .IsFixedLength();

                entity.Property(e => e.Itemno)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("itemno");

                entity.Property(e => e.Maxtime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maxtime");

                entity.Property(e => e.Receivedate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("receivedate");

                entity.Property(e => e.Receivetime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("receivetime");

                entity.Property(e => e.Registerdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("registerdate");

                entity.Property(e => e.Registerno)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("registerno");

                entity.Property(e => e.Statecode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("statecode");

                entity.Property(e => e.Viewstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("viewstatus");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WID");
            });

            modelBuilder.Entity<Priorityinfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PRIORITYINFO");

                entity.HasIndex(e => e.Code, "PRIORITYINFO_key")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Dsc)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QrcodeGen>(entity =>
            {
                entity.HasKey(e => e.Qrcode);

                entity.ToTable("QRCodeGen");

                entity.Property(e => e.Qrcode)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("qrcode");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("wid");

                entity.HasOne(d => d.WidNavigation)
                    .WithMany(p => p.QrcodeGens)
                    .HasForeignKey(d => d.Wid)
                    .HasConstraintName("FK_QRCodeGen_WORKINFO");
            });

            modelBuilder.Entity<Receivedoc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RECEIVEDOC");

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Dsc)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Secretlev>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SECRETLEV");

                entity.HasIndex(e => e.Code, "SECRETLEV_key")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Code)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Dsc)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SendPerson>(entity =>
            {
                entity.HasKey(e => e.SpId);

                entity.ToTable("Send_Person");

                entity.Property(e => e.SpId).HasColumnName("sp_id");

                entity.Property(e => e.FlagUpdate)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("flagUpdate");

                entity.Property(e => e.Msg)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("msg");

                entity.Property(e => e.ReciverBid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("reciver_bid");

                entity.Property(e => e.ReciverUid)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("reciver_uid");

                entity.Property(e => e.SendDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("send_date");

                entity.Property(e => e.SendTime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("send_time");

                entity.Property(e => e.SenderBid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("sender_bid");

                entity.Property(e => e.SenderUid)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("sender_uid");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wid");
            });

            modelBuilder.Entity<SignatureInfo>(entity =>
            {
                entity.HasKey(e => new { e.Wid, e.Usrid });

                entity.ToTable("SignatureInfo");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wid");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usrid");

                entity.Property(e => e.Filepath)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("filepath");

                entity.Property(e => e.Imagesfile)
                    .HasColumnType("text")
                    .HasColumnName("imagesfile");

                entity.Property(e => e.Signature)
                    .HasColumnType("text")
                    .HasColumnName("signature");
            });

            modelBuilder.Entity<Specialcommand>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SPECIALCOMMAND");

                entity.HasIndex(e => e.Code, "SPECIALCOMMAND_key")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DSC");
            });

            modelBuilder.Entity<Stateinfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("STATEINFO");

                entity.HasIndex(e => e.Code, "STATEINFO_key")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Dsc)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TempGroupdesc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TempGroupdesc");

                entity.Property(e => e.CenterId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("centerID");

                entity.Property(e => e.CenterName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("centerName");

                entity.Property(e => e.DeptId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("deptID");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("deptName");

                entity.Property(e => e.GIndent)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("gIndent");

                entity.Property(e => e.ItemNo)
                    .HasMaxLength(50)
                    .HasColumnName("itemNo");

                entity.Property(e => e.ItemSort)
                    .HasMaxLength(50)
                    .HasColumnName("itemSort");

                entity.Property(e => e.ItemUsed)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("itemUsed")
                    .IsFixedLength();

                entity.Property(e => e.NextNode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nextNode");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(10)
                    .HasColumnName("parentID")
                    .IsFixedLength();
            });

            modelBuilder.Entity<TmpDocattachEform>(entity =>
            {
                entity.HasKey(e => new { e.Wid, e.Bid, e.Itemno });

                entity.ToTable("TmpDocattachEform");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wid");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("bid");

                entity.Property(e => e.Itemno)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("itemno");

                entity.Property(e => e.EformNo)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TmpIfmeformused>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TmpIFMEFORMUsed");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.EformNo)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Registerno)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("registerno");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usrid");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wid");

                entity.Property(e => e.Wsubtype)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("wsubtype");

                entity.Property(e => e.Wtype)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("wtype");
            });

            modelBuilder.Entity<TmpRegisterNoUsed>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TmpRegisterNoUsed");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Createdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("createdate");

                entity.Property(e => e.Createtime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("createtime");

                entity.Property(e => e.Registerno)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("registerno");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("usrid");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("wid");

                entity.Property(e => e.Wsubtype)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("wsubtype");

                entity.Property(e => e.Wtype)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("wtype");
            });

            modelBuilder.Entity<UserCertPwd>(entity =>
            {
                entity.HasKey(e => e.Usrid);

                entity.ToTable("UserCertPWD");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("USRID");

                entity.Property(e => e.CertPwd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CertPWD");
            });

            modelBuilder.Entity<Userinfo>(entity =>
            {
                entity.HasKey(e => new { e.Usrid, e.Bid, e.Mainbid });

                entity.ToTable("USERINFO");

                entity.HasIndex(e => new { e.Usrid, e.Bid, e.Mainbid }, "KEYUSER_BID")
                    .IsUnique();

                entity.HasIndex(e => e.Bid, "USER_BID");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USRID");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.Mainbid)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("mainbid");

                entity.Property(e => e.Computername)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Emailaddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emailaddress");

                entity.Property(e => e.ExpireDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Icqaddress)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("icqaddress");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.SecureBasketinfo)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("secure_basketinfo");

                entity.Property(e => e.SecureIfmflowDepartment)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Secure_ifmflow_department");

                entity.Property(e => e.SecureSignature)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Secure_Signature");

                entity.Property(e => e.SignaturePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Signature_path");

                entity.Property(e => e.StartDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UsedInbound)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Used_Inbound");

                entity.Property(e => e.UsedOutbound)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Used_Outbound");

                entity.Property(e => e.Usedencrypt)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("usedencrypt");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Userpwd2>(entity =>
            {
                entity.HasKey(e => e.Usrid);

                entity.ToTable("userpwd2");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usrid");

                entity.Property(e => e.Pwd2)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("pwd2");
            });

            modelBuilder.Entity<Userstatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("USERSTATUS");

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DSC");
            });

            modelBuilder.Entity<ViewBasketinfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_basketinfo");

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.Class)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CLASS");

                entity.Property(e => e.Deptcode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Deptname)
                    .HasMaxLength(105)
                    .IsUnicode(false)
                    .HasColumnName("deptname");

                entity.Property(e => e.DocuName)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.HomeDir)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ItemNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RegisterNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SendNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Wfdsc)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Wfid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WFID");
            });

            modelBuilder.Entity<ViewNewdocument>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_newdocument");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.Docuname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Expr1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PriorityCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveTime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Viewstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WID");
            });

            modelBuilder.Entity<ViewNotworkinfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VIEW_NOTWORKINFO");

                entity.Property(e => e.ReceiveDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WID");

                entity.Property(e => e.Workfowid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WORKFOWID");
            });

            modelBuilder.Entity<ViewReadaliasbasket>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VIEW_readaliasbasket");

                entity.Property(e => e.ActionCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.Expr1)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Expr2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlagDelete)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SenderBdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SenderBDsc");

                entity.Property(e => e.SenderBid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("SenderBID");

                entity.Property(e => e.Wfid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WFID");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WID");
            });

            modelBuilder.Entity<ViewTransferpea>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_transferpea");

                entity.Property(e => e.ActionCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ActionMsg).HasColumnType("ntext");

                entity.Property(e => e.ActionMsgflw).HasColumnType("text");

                entity.Property(e => e.Actionfollowup)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("actionfollowup");

                entity.Property(e => e.Agewid)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Attach1)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Attach2)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Autodelete)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("autodelete");

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BDsc");

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.Bidclass)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("bidclass");

                entity.Property(e => e.Bookgroup)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.CompleteDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompleteDatepro)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompleteTimepro)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Completetime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Docuname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dsc).HasColumnType("text");

                entity.Property(e => e.Expr1)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Expr2)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Expr3)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Expr4)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FlagDelete)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.InitDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.InitTime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ItemNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Maxtime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OtherAttach)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PriorityCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.PriorityCodepro)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveTime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Refwid)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterBdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterBid)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterUid)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Registernopro)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("registernopro");

                entity.Property(e => e.Registertime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SecretLevCodepro)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Secretlevcode)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SenderBdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SenderBDsc");

                entity.Property(e => e.SenderBid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("SenderBID");

                entity.Property(e => e.SenderClass)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("senderCLASS");

                entity.Property(e => e.SenderMsg)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SenderRegisterNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SenderUid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SenderUID");

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StateCodepro)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TakeActionname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TakeRemark)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USRID");

                entity.Property(e => e.Viewstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Wdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("WDATE");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WID");

                entity.Property(e => e.Widflw)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WIDflw");

                entity.Property(e => e.Widpro)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("widpro");

                entity.Property(e => e.Worigin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WownerBdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Wserial)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Wsubject)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Wsubtype)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Wtype)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewUserinfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VIEW_userinfo");

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.Code)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Deptcode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Deptname)
                    .HasMaxLength(105)
                    .IsUnicode(false)
                    .HasColumnName("deptname");

                entity.Property(e => e.Emailaddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emailaddress");

                entity.Property(e => e.Expr1)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Expr2)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Expr3)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Mainbid)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("mainbid");

                entity.Property(e => e.Prefixdept)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("prefixdept");

                entity.Property(e => e.SecretDsc)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("secret_dsc");

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StatusDsc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status_dsc");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USRID");

                entity.Property(e => e.Wfid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WFID");
            });

            modelBuilder.Entity<ViewWsubtype>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_wsubtype");

                entity.Property(e => e.AliasPosition)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Dsc)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Granted)
                    .HasMaxLength(140)
                    .IsUnicode(false);

                entity.Property(e => e.SubWtypeNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UsedCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Used_code");

                entity.Property(e => e.Wtype)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Waitforsign>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("waitforsign");

                entity.Property(e => e.Status)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Wid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wid");
            });

            modelBuilder.Entity<WorkEform>(entity =>
            {
                entity.HasKey(e => e.Wid);

                entity.ToTable("work_eform");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("wid");

                entity.Property(e => e.EformData)
                    .HasColumnType("text")
                    .HasColumnName("eform_data");

                entity.Property(e => e.EformId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("eform_id");
            });

            modelBuilder.Entity<Workinfo>(entity =>
            {
                entity.HasKey(e => e.Wid);

                entity.ToTable("WORKINFO");

                entity.HasIndex(e => e.Wid, "WINFO_ID")
                    .IsUnique();

                entity.HasIndex(e => new { e.Wid, e.Wtype }, "workinfo21");

                entity.HasIndex(e => new { e.Wid, e.Wtype, e.RegisterBid }, "workinfo22");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WID");

                entity.Property(e => e.Agewid)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Autodelete)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("autodelete");

                entity.Property(e => e.CompleteDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Completetime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Docuname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dsc).HasColumnType("text");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Maxtime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OtherAttach)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PriorityCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Refwid)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterBdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterBid)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterUid)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Registertime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Secretlevcode)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Wdate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("WDATE");

                entity.Property(e => e.Worigin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WownerBdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Wsubject)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Wsubtype)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Wtype)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Workinprocess>(entity =>
            {
                entity.HasKey(e => new { e.Wid, e.ItemNo, e.Bid })
                    .IsClustered(false);

                entity.ToTable("WORKINPROCESS");

                entity.HasIndex(e => new { e.Wid, e.Bid, e.ItemNo }, "WINPROCESS_ID")
                    .IsUnique();

                entity.HasIndex(e => e.Bid, "WINPROCESS_ID1");

                entity.HasIndex(e => e.SenderBid, "WINPROCESS_ID2");

                entity.HasIndex(e => new { e.Bid, e.Wid }, "workinprocess25")
                    .IsClustered();

                entity.HasIndex(e => new { e.Bid, e.Wid, e.RegisterNo, e.StateCode }, "workinprocess27");

                entity.Property(e => e.Wid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WID");

                entity.Property(e => e.ItemNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("BID");

                entity.Property(e => e.ActionCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ActionMsg).HasColumnType("ntext");

                entity.Property(e => e.Actionfollowup)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("actionfollowup");

                entity.Property(e => e.Attach1)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Attach2)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Bdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BDsc");

                entity.Property(e => e.Bookgroup)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.CompleteDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompleteTime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FlagDelete)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.InitDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.InitTime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Maxtime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maxtime");

                entity.Property(e => e.PriorityCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveTime)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SecretLevCode)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SenderBdsc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SenderBDsc");

                entity.Property(e => e.SenderBid)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("SenderBID");

                entity.Property(e => e.SenderMsg)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SenderRegisterNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SenderUid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SenderUID");

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TakeActionname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TakeRemark)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Usrid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USRID");

                entity.Property(e => e.Viewstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Worktype>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WORKTYPE");

                entity.HasIndex(e => e.Code, "code")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DSC");
            });

            modelBuilder.Entity<Wsubtype>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WSUBTYPE");

                entity.HasIndex(e => new { e.Wtype, e.Code, e.Bid, e.Granted }, "WSubtype_KEY")
                    .IsUnique();

                entity.Property(e => e.AliasPosition)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Dsc)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Granted)
                    .HasMaxLength(140)
                    .IsUnicode(false);

                entity.Property(e => e.SubWtypeNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UsedCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Used_code");

                entity.Property(e => e.Wtype)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
