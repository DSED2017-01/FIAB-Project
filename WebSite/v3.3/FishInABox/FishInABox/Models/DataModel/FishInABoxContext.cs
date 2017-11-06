using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FishInABox.Models.DataModel
{
    public partial class FishInABoxContext : DbContext
    {
        public virtual DbSet<MARINE_CLASS> MARINE_CLASS { get; set; }
        public virtual DbSet<MARINE_FAMILY> MARINE_FAMILY { get; set; }
        public virtual DbSet<MARINE_SPECIES> MARINE_SPECIES { get; set; }
        public virtual DbSet<MOVEMENT> MOVEMENT { get; set; }
        public virtual DbSet<MOVEMENT_BATCH> MOVEMENT_BATCH { get; set; }
        public virtual DbSet<MOVEMENT_PERIOD> MOVEMENT_PERIOD { get; set; }
        public virtual DbSet<REASON_MORTALITY> REASON_MORTALITY { get; set; }
        public virtual DbSet<RECORD_GROUP> RECORD_GROUP { get; set; }
        public virtual DbSet<RECORD_PACKING> RECORD_PACKING { get; set; }
        public virtual DbSet<RECORD_PET> RECORD_PET { get; set; }
        public virtual DbSet<RECORD_PET_SIZE> RECORD_PET_SIZE { get; set; }
        public virtual DbSet<SHIPMENT> SHIPMENT { get; set; }
        public virtual DbSet<SHIPMENT_ITEM> SHIPMENT_ITEM { get; set; }
        public virtual DbSet<SHIPMENT_ORDER> SHIPMENT_ORDER { get; set; }
        public virtual DbSet<SYS_STUFF> SYS_STUFF { get; set; }
        public virtual DbSet<TANK> TANK { get; set; }
        public virtual DbSet<TANK_BAY> TANK_BAY { get; set; }
        public virtual DbSet<TANK_LOG> TANK_LOG { get; set; }
        public virtual DbSet<TANK_LOG_DAILY> TANK_LOG_DAILY { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=tcp:fishinabox.database.windows.net;Initial Catalog=fishinabox;Persist Security Info=True;User ID=FIAB;Password=M0r7w1b3");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MARINE_CLASS>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Flag)
                    .HasColumnName("FLAG")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Schedule4)
                    .IsRequired()
                    .HasColumnName("SCHEDULE4")
                    .HasMaxLength(40);

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<MARINE_FAMILY>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Flag)
                    .HasColumnName("FLAG")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Schedule3)
                    .IsRequired()
                    .HasColumnName("SCHEDULE3")
                    .HasMaxLength(25);

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<MARINE_SPECIES>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.ClassFk).HasColumnName("CLASS_FK");

                entity.Property(e => e.Common)
                    .IsRequired()
                    .HasColumnName("COMMON")
                    .HasMaxLength(80);

                entity.Property(e => e.FamilyFk).HasColumnName("FAMILY_FK");

                entity.Property(e => e.Flag)
                    .HasColumnName("FLAG")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Scientific)
                    .IsRequired()
                    .HasColumnName("SCIENTIFIC")
                    .HasMaxLength(40);

                entity.Property(e => e.SpeciesFk).HasColumnName("SPECIES_FK");

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(50);

                entity.HasOne(d => d.ClassFkNavigation)
                    .WithMany(p => p.MARINE_SPECIES)
                    .HasForeignKey(d => d.ClassFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MARINE_SPECIES_MARINE_CLASS");

                entity.HasOne(d => d.FamilyFkNavigation)
                    .WithMany(p => p.MARINE_SPECIES)
                    .HasForeignKey(d => d.FamilyFk)
                    .HasConstraintName("FK_MARINE_SPECIES_MARINE_FAMILY");
            });

            modelBuilder.Entity<MOVEMENT>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.BatchFk).HasColumnName("BATCH_FK");

                entity.Property(e => e.Comment)
                    .HasColumnName("COMMENT")
                    .HasMaxLength(100);

                entity.Property(e => e.Day).HasColumnName("DAY");

                entity.Property(e => e.FromFk).HasColumnName("FROM_FK");

                entity.Property(e => e.IntialQty).HasColumnName("INTIAL_QTY");

                entity.Property(e => e.PeriodFk).HasColumnName("PERIOD_FK");

                entity.Property(e => e.QtyMoved).HasColumnName("QTY_MOVED");

                entity.Property(e => e.TankFk).HasColumnName("TANK_FK");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("TIMESTAMP")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MOVEMENT_BATCH>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.ItemFk).HasColumnName("ITEM_FK");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            });

            modelBuilder.Entity<MOVEMENT_PERIOD>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.ClosedDate)
                    .HasColumnName("CLOSED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.ClosedFlag)
                    .HasColumnName("CLOSED_FLAG")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StartDate)
                    .HasColumnName("START_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<REASON_MORTALITY>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.IdCode)
                    .HasColumnName("ID_CODE")
                    .HasMaxLength(10);

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<RECORD_GROUP>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<RECORD_PACKING>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<RECORD_PET>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("CODE")
                    .HasMaxLength(10);

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(100);

                entity.Property(e => e.GroupFk).HasColumnName("GROUP_FK");

                entity.Property(e => e.SizeFk).HasColumnName("SIZE_FK");

                entity.Property(e => e.SpeciesFk).HasColumnName("SPECIES_FK");

                entity.HasOne(d => d.GroupFkNavigation)
                    .WithMany(p => p.RECORD_PET)
                    .HasForeignKey(d => d.GroupFk)
                    .HasConstraintName("FK_RECORD_PET_RECORD_GROUP");

                entity.HasOne(d => d.SizeFkNavigation)
                    .WithMany(p => p.RECORD_PET)
                    .HasForeignKey(d => d.SizeFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RECORD_PET_RECORD_PET_SIZE");

                entity.HasOne(d => d.SpeciesFkNavigation)
                    .WithMany(p => p.RECORD_PET)
                    .HasForeignKey(d => d.SpeciesFk)
                    .HasConstraintName("FK_RECORD_PET_MARINE_SPECIES");
            });

            modelBuilder.Entity<RECORD_PET_SIZE>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<SHIPMENT>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Comment)
                    .HasColumnName("COMMENT")
                    .HasMaxLength(100);

                entity.Property(e => e.Eta)
                    .HasColumnName("ETA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Etd)
                    .HasColumnName("ETD")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExportFk).HasColumnName("EXPORT_FK");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("TIMESTAMP")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SHIPMENT_ITEM>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.Property(e => e.QuarantineFk).HasColumnName("QUARANTINE_FK");

                entity.Property(e => e.RecordFk).HasColumnName("RECORD_FK");

                entity.Property(e => e.SizeFk).HasColumnName("SIZE_FK");

                entity.Property(e => e.SpeciesFk).HasColumnName("SPECIES_FK");

                entity.Property(e => e.SpeciesText)
                    .HasColumnName("SPECIES_TEXT")
                    .HasMaxLength(100);

                entity.Property(e => e.SpeciesText2)
                    .HasColumnName("SPECIES_TEXT_2")
                    .HasMaxLength(100);

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(20);

                entity.HasOne(d => d.RecordFkNavigation)
                    .WithMany(p => p.SHIPMENT_ITEM)
                    .HasForeignKey(d => d.RecordFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_ITEM_SHIPMENT_ORDER");

                entity.HasOne(d => d.SizeFkNavigation)
                    .WithMany(p => p.SHIPMENT_ITEM)
                    .HasForeignKey(d => d.SizeFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_ITEM_RECORD_PET_SIZE");

                entity.HasOne(d => d.SpeciesFkNavigation)
                    .WithMany(p => p.SHIPMENT_ITEM)
                    .HasForeignKey(d => d.SpeciesFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_ITEM_MARINE_SPECIES");
            });

            modelBuilder.Entity<SHIPMENT_ORDER>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.ShipmentFk).HasColumnName("SHIPMENT_FK");

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(20);

                entity.Property(e => e.Timestamp)
                    .HasColumnName("TIMESTAMP")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.ShipmentFkNavigation)
                    .WithMany(p => p.SHIPMENT_ORDER)
                    .HasForeignKey(d => d.ShipmentFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_ORDER_SHIPMENT");
            });

            modelBuilder.Entity<SYS_STUFF>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.FamilyName)
                    .HasColumnName("FAMILY_NAME")
                    .HasMaxLength(25);

                entity.Property(e => e.FirstName)
                    .HasColumnName("FIRST_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.IdCode)
                    .IsRequired()
                    .HasColumnName("ID_CODE")
                    .HasMaxLength(10);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("MIDDLE_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TANK>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.BayFk).HasColumnName("BAY_FK");

                entity.Property(e => e.IdCode)
                    .IsRequired()
                    .HasColumnName("ID_CODE")
                    .HasMaxLength(10);

                entity.Property(e => e.Rfid)
                    .HasColumnName("RFID")
                    .HasMaxLength(20);

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(100);

                entity.HasOne(d => d.BayFkNavigation)
                    .WithMany(p => p.TANK)
                    .HasForeignKey(d => d.BayFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TANK_TANK_BAY");
            });

            modelBuilder.Entity<TANK_BAY>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.IdCode)
                    .IsRequired()
                    .HasColumnName("ID_CODE")
                    .HasMaxLength(3);

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TANK_LOG>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Comment)
                    .HasColumnName("COMMENT")
                    .HasMaxLength(100);

                entity.Property(e => e.OrderFk).HasColumnName("ORDER_FK");

                entity.Property(e => e.PeriodFk).HasColumnName("PERIOD_FK");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.SizeFk).HasColumnName("SIZE_FK");

                entity.Property(e => e.SpeciesFk).HasColumnName("SPECIES_FK");

                entity.Property(e => e.SpeciesText)
                    .HasColumnName("SPECIES_TEXT")
                    .HasMaxLength(50);

                entity.Property(e => e.SpeciesText2)
                    .HasColumnName("SPECIES_TEXT_2")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('VARIANT/MALE/FEMALE')");

                entity.Property(e => e.StuffFk).HasColumnName("STUFF_FK");

                entity.Property(e => e.TankFk).HasColumnName("TANK_FK");

                entity.HasOne(d => d.PeriodFkNavigation)
                    .WithMany(p => p.TANK_LOG)
                    .HasForeignKey(d => d.PeriodFk)
                    .HasConstraintName("FK_TANK_LOG_MOVEMENT_PERIOD");

                entity.HasOne(d => d.SizeFkNavigation)
                    .WithMany(p => p.TANK_LOG)
                    .HasForeignKey(d => d.SizeFk)
                    .HasConstraintName("FK_TANK_LOG_RECORD_PET_SIZE");

                entity.HasOne(d => d.SpeciesFkNavigation)
                    .WithMany(p => p.TANK_LOG)
                    .HasForeignKey(d => d.SpeciesFk)
                    .HasConstraintName("FK_TANK_LOG_MARINE_SPECIES");

                entity.HasOne(d => d.StuffFkNavigation)
                    .WithMany(p => p.TANK_LOG)
                    .HasForeignKey(d => d.StuffFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TANK_LOG_SYS_STUFF");

                entity.HasOne(d => d.TankFkNavigation)
                    .WithMany(p => p.TANK_LOG)
                    .HasForeignKey(d => d.TankFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TANK_LOG_TANK");
            });

            modelBuilder.Entity<TANK_LOG_DAILY>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Comment)
                    .HasColumnName("COMMENT")
                    .HasMaxLength(100);

                entity.Property(e => e.LogDate)
                    .HasColumnName("LOG_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LogFk).HasColumnName("LOG_FK");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.ReasonFk).HasColumnName("REASON_FK");

                entity.Property(e => e.StuffFk).HasColumnName("STUFF_FK");

                entity.HasOne(d => d.LogFkNavigation)
                    .WithMany(p => p.TANK_LOG_DAILY)
                    .HasForeignKey(d => d.LogFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TANK_LOG_DAILY_TANK_LOG");

                entity.HasOne(d => d.ReasonFkNavigation)
                    .WithMany(p => p.TANK_LOG_DAILY)
                    .HasForeignKey(d => d.ReasonFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TANK_LOG_DAILY_REASON_MORTALITY");

                entity.HasOne(d => d.StuffFkNavigation)
                    .WithMany(p => p.TANK_LOG_DAILY)
                    .HasForeignKey(d => d.StuffFk)
                    .HasConstraintName("FK_TANK_LOG_DAILY_SYS_STUFF");
            });
        }
    }
}
