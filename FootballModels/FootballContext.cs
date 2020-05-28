using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FootballModels
{
    public partial class FootballContext : DbContext
    {
        public FootballContext()
        {
        }

        public FootballContext(DbContextOptions<FootballContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DefDetail> DefDetail { get; set; }
        public virtual DbSet<DriDetail> DriDetail { get; set; }
        public virtual DbSet<PacDetail> PacDetail { get; set; }
        public virtual DbSet<PasDetail> PasDetail { get; set; }
        public virtual DbSet<PhyDetail> PhyDetail { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<ShoDetail> ShoDetail { get; set; }
        public virtual DbSet<SkillDetail> SkillDetail { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TradingMarket> TradingMarket { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=PHATLTSE62882;Database=Football;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DefDetail>(entity =>
            {
                entity.Property(e => e.Da).HasColumnName("DA");

                entity.Property(e => e.Ha).HasColumnName("HA");

                entity.HasOne(d => d.PlayerNavigation)
                    .WithMany(p => p.DefDetail)
                    .HasForeignKey(d => d.Player)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DefDetail_Player");
            });

            modelBuilder.Entity<DriDetail>(entity =>
            {
                entity.HasOne(d => d.PlayerNavigation)
                    .WithMany(p => p.DriDetail)
                    .HasForeignKey(d => d.Player)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriDetail_Player");
            });

            modelBuilder.Entity<PacDetail>(entity =>
            {
                entity.HasOne(d => d.PlayerNavigation)
                    .WithMany(p => p.PacDetail)
                    .HasForeignKey(d => d.Player)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PacDetail_Player");
            });

            modelBuilder.Entity<PasDetail>(entity =>
            {
                entity.Property(e => e.Fk).HasColumnName("FK");

                entity.Property(e => e.Lp).HasColumnName("LP");

                entity.Property(e => e.Sp).HasColumnName("SP");

                entity.HasOne(d => d.PlayerNavigation)
                    .WithMany(p => p.PasDetail)
                    .HasForeignKey(d => d.Player)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PasDetail_Player");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Def).HasColumnName("DEF");

                entity.Property(e => e.Dri).HasColumnName("DRI");

                entity.Property(e => e.Foot).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Nation).HasMaxLength(25);

                entity.Property(e => e.Ovr).HasColumnName("OVR");

                entity.Property(e => e.Pac).HasColumnName("PAC");

                entity.Property(e => e.Pas).HasColumnName("PAS");

                entity.Property(e => e.Phy).HasColumnName("PHY");

                entity.Property(e => e.Pos)
                    .HasColumnName("POS")
                    .HasMaxLength(10);

                entity.Property(e => e.Season).HasMaxLength(50);

                entity.Property(e => e.Sho).HasColumnName("SHO");

                entity.Property(e => e.Sm).HasColumnName("SM");

                entity.Property(e => e.Wf).HasColumnName("WF");

                entity.Property(e => e.Wrs)
                    .HasColumnName("WRS")
                    .HasMaxLength(50);

                entity.HasOne(d => d.TeamNavigation)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.Team)
                    .HasConstraintName("FK_Player_Team");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.Cam).HasColumnName("CAM");

                entity.Property(e => e.Cb).HasColumnName("CB");

                entity.Property(e => e.Cdm).HasColumnName("CDM");

                entity.Property(e => e.Cf).HasColumnName("CF");

                entity.Property(e => e.Cm).HasColumnName("CM");

                entity.Property(e => e.Lb).HasColumnName("LB");

                entity.Property(e => e.Lf).HasColumnName("LF");

                entity.Property(e => e.Lm).HasColumnName("LM");

                entity.Property(e => e.Lw).HasColumnName("LW");

                entity.Property(e => e.Lwb).HasColumnName("LWB");

                entity.Property(e => e.Rb).HasColumnName("RB");

                entity.Property(e => e.Rf).HasColumnName("RF");

                entity.Property(e => e.Rm).HasColumnName("RM");

                entity.Property(e => e.Rw).HasColumnName("RW");

                entity.Property(e => e.Rwb).HasColumnName("RWB");

                entity.Property(e => e.St).HasColumnName("ST");

                entity.HasOne(d => d.PlayerNavigation)
                    .WithMany(p => p.Position)
                    .HasForeignKey(d => d.Player)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Position_Player");
            });

            modelBuilder.Entity<ShoDetail>(entity =>
            {
                entity.Property(e => e.Ls).HasColumnName("LS");

                entity.Property(e => e.Sp).HasColumnName("SP");

                entity.HasOne(d => d.PlayerNavigation)
                    .WithMany(p => p.ShoDetail)
                    .HasForeignKey(d => d.Player)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoDetail_Player");
            });

            modelBuilder.Entity<SkillDetail>(entity =>
            {
                entity.Property(e => e.Footed).HasMaxLength(50);

                entity.Property(e => e.Height).HasMaxLength(50);

                entity.Property(e => e.Weight).HasMaxLength(50);

                entity.Property(e => e.Workrates).HasMaxLength(50);

                entity.HasOne(d => d.PlayerNavigation)
                    .WithMany(p => p.SkillDetail)
                    .HasForeignKey(d => d.Player)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SkillDetail_Player");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.League).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TradingMarket>(entity =>
            {
                entity.Property(e => e.Ps4price).HasColumnName("PS4Price");

                entity.Property(e => e.Ps4time)
                    .HasColumnName("PS4Time")
                    .HasMaxLength(50);

                entity.Property(e => e.Xprice).HasColumnName("XPrice");

                entity.Property(e => e.Xtime)
                    .HasColumnName("XTime")
                    .HasMaxLength(50);

                entity.HasOne(d => d.PlayerNavigation)
                    .WithMany(p => p.TradingMarket)
                    .HasForeignKey(d => d.Player)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TradingMarket_Player");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
