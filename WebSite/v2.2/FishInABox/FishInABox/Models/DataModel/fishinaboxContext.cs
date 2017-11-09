using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FishInABox.Models.DataModel
{
    public partial class fishinaboxContext : DbContext
    {
        public fishinaboxContext(DbContextOptions<fishinaboxContext> opts) : base(opts) { }

        public virtual DbSet<MarineClass> MarineClass { get; set; }
        public virtual DbSet<MarineFamily> MarineFamily { get; set; }
        public virtual DbSet<MarineSpecies> MarineSpecies { get; set; }
        public virtual DbSet<Movement> Movement { get; set; }
        public virtual DbSet<MovementBatch> MovementBatch { get; set; }
        public virtual DbSet<MovementPeriod> MovementPeriod { get; set; }
        public virtual DbSet<ReasonMortality> ReasonMortality { get; set; }
        public virtual DbSet<RecordGroup> RecordGroup { get; set; }
        public virtual DbSet<RecordPacking> RecordPacking { get; set; }
        public virtual DbSet<RecordPet> RecordPet { get; set; }
        public virtual DbSet<RecordPetSize> RecordPetSize { get; set; }
        public virtual DbSet<Shipment> Shipment { get; set; }
        public virtual DbSet<ShipmentItem> ShipmentItem { get; set; }
        public virtual DbSet<ShipmentOrder> ShipmentOrder { get; set; }
        public virtual DbSet<SysStuff> SysStuff { get; set; }
        public virtual DbSet<Tank> Tank { get; set; }
        public virtual DbSet<TankBay> TankBay { get; set; }
        public virtual DbSet<TankLog> TankLog { get; set; }
        public virtual DbSet<TankLogDaily> TankLogDaily { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarineClass>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("MARINE_CLASS");

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

            modelBuilder.Entity<MarineFamily>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("MARINE_FAMILY");

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

            modelBuilder.Entity<MarineSpecies>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("MARINE_SPECIES");

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
                    .WithMany(p => p.MarineSpecies)
                    .HasForeignKey(d => d.ClassFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MARINE_SPECIES_MARINE_CLASS");

                entity.HasOne(d => d.FamilyFkNavigation)
                    .WithMany(p => p.MarineSpecies)
                    .HasForeignKey(d => d.FamilyFk)
                    .HasConstraintName("FK_MARINE_SPECIES_MARINE_FAMILY");
            });

            modelBuilder.Entity<Movement>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("MOVEMENT");

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

            modelBuilder.Entity<MovementBatch>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("MOVEMENT_BATCH");

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.ItemFk).HasColumnName("ITEM_FK");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            });

            modelBuilder.Entity<MovementPeriod>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("MOVEMENT_PERIOD");

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

            modelBuilder.Entity<ReasonMortality>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("REASON_MORTALITY");

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.IdCode)
                    .HasColumnName("ID_CODE")
                    .HasMaxLength(10);

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<RecordGroup>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("RECORD_GROUP");

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<RecordPacking>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("RECORD_PACKING");

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<RecordPet>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("RECORD_PET");

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
                    .WithMany(p => p.RecordPet)
                    .HasForeignKey(d => d.GroupFk)
                    .HasConstraintName("FK_RECORD_PET_RECORD_GROUP");

                entity.HasOne(d => d.SizeFkNavigation)
                    .WithMany(p => p.RecordPet)
                    .HasForeignKey(d => d.SizeFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RECORD_PET_RECORD_PET_SIZE");

                entity.HasOne(d => d.SpeciesFkNavigation)
                    .WithMany(p => p.RecordPet)
                    .HasForeignKey(d => d.SpeciesFk)
                    .HasConstraintName("FK_RECORD_PET_MARINE_SPECIES");
            });

            modelBuilder.Entity<RecordPetSize>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("RECORD_PET_SIZE");

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("SHIPMENT");

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

            modelBuilder.Entity<ShipmentItem>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("SHIPMENT_ITEM");

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
                    .WithMany(p => p.ShipmentItem)
                    .HasForeignKey(d => d.RecordFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_ITEM_SHIPMENT_ORDER");

                entity.HasOne(d => d.SizeFkNavigation)
                    .WithMany(p => p.ShipmentItem)
                    .HasForeignKey(d => d.SizeFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_ITEM_RECORD_PET_SIZE");

                entity.HasOne(d => d.SpeciesFkNavigation)
                    .WithMany(p => p.ShipmentItem)
                    .HasForeignKey(d => d.SpeciesFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_ITEM_MARINE_SPECIES");
            });

            modelBuilder.Entity<ShipmentOrder>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("SHIPMENT_ORDER");

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.ShipmentFk).HasColumnName("SHIPMENT_FK");

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(20);

                entity.Property(e => e.Timestamp)
                    .HasColumnName("TIMESTAMP")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.ShipmentFkNavigation)
                    .WithMany(p => p.ShipmentOrder)
                    .HasForeignKey(d => d.ShipmentFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_ORDER_SHIPMENT");
            });

            modelBuilder.Entity<SysStuff>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("SYS_STUFF");

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

            modelBuilder.Entity<Tank>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("TANK");

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
                    .WithMany(p => p.Tank)
                    .HasForeignKey(d => d.BayFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TANK_TANK_BAY");
            });

            modelBuilder.Entity<TankBay>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("TANK_BAY");

                entity.Property(e => e.IdPk).HasColumnName("ID_PK");

                entity.Property(e => e.IdCode)
                    .IsRequired()
                    .HasColumnName("ID_CODE")
                    .HasMaxLength(3);

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TankLog>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("TANK_LOG");

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
                    .WithMany(p => p.TankLog)
                    .HasForeignKey(d => d.PeriodFk)
                    .HasConstraintName("FK_TANK_LOG_MOVEMENT_PERIOD");

                entity.HasOne(d => d.SizeFkNavigation)
                    .WithMany(p => p.TankLog)
                    .HasForeignKey(d => d.SizeFk)
                    .HasConstraintName("FK_TANK_LOG_RECORD_PET_SIZE");

                entity.HasOne(d => d.SpeciesFkNavigation)
                    .WithMany(p => p.TankLog)
                    .HasForeignKey(d => d.SpeciesFk)
                    .HasConstraintName("FK_TANK_LOG_MARINE_SPECIES");

                entity.HasOne(d => d.StuffFkNavigation)
                    .WithMany(p => p.TankLog)
                    .HasForeignKey(d => d.StuffFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TANK_LOG_SYS_STUFF");

                entity.HasOne(d => d.TankFkNavigation)
                    .WithMany(p => p.TankLog)
                    .HasForeignKey(d => d.TankFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TANK_LOG_TANK");
            });

            modelBuilder.Entity<TankLogDaily>(entity =>
            {
                entity.HasKey(e => e.IdPk);

                entity.ToTable("TANK_LOG_DAILY");

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
                    .WithMany(p => p.TankLogDaily)
                    .HasForeignKey(d => d.LogFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TANK_LOG_DAILY_TANK_LOG");

                entity.HasOne(d => d.ReasonFkNavigation)
                    .WithMany(p => p.TankLogDaily)
                    .HasForeignKey(d => d.ReasonFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TANK_LOG_DAILY_REASON_MORTALITY");

                entity.HasOne(d => d.StuffFkNavigation)
                    .WithMany(p => p.TankLogDaily)
                    .HasForeignKey(d => d.StuffFk)
                    .HasConstraintName("FK_TANK_LOG_DAILY_SYS_STUFF");
            });
        }
    }
}
