using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TargetDDContext.Models;

namespace TargetDDContext.Data;

public partial class TargetDDDbContext : DbContext
{
    public TargetDDDbContext()
    {
    }

    public TargetDDDbContext(DbContextOptions<TargetDDDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Column> Columns { get; set; }

    public virtual DbSet<Config> Configs { get; set; }

    public virtual DbSet<FamilyPath> FamilyPaths { get; set; }

    public virtual DbSet<ForeignKey> ForeignKeys { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS22;Initial Catalog=AttractDD;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Column>(entity =>
        {
            entity.HasKey(e => new { e.TableSchema, e.TableName, e.ColumnName }).HasName("PK_Schema_Table_Column");

            entity.Property(e => e.TableSchema)
                .HasMaxLength(128)
                .HasColumnName("TABLE_SCHEMA");
            entity.Property(e => e.TableName)
                .HasMaxLength(128)
                .HasColumnName("TABLE_NAME");
            entity.Property(e => e.ColumnName)
                .HasMaxLength(128)
                .HasColumnName("COLUMN_NAME");
            entity.Property(e => e.CharacterMaximumLength).HasColumnName("CHARACTER_MAXIMUM_LENGTH");
            entity.Property(e => e.CharacterOctetLength).HasColumnName("CHARACTER_OCTET_LENGTH");
            entity.Property(e => e.CharacterSetCatalog)
                .HasMaxLength(128)
                .HasColumnName("CHARACTER_SET_CATALOG");
            entity.Property(e => e.CharacterSetName)
                .HasMaxLength(128)
                .HasColumnName("CHARACTER_SET_NAME");
            entity.Property(e => e.CharacterSetSchema)
                .HasMaxLength(128)
                .HasColumnName("CHARACTER_SET_SCHEMA");
            entity.Property(e => e.CollationCatalog)
                .HasMaxLength(128)
                .HasColumnName("COLLATION_CATALOG");
            entity.Property(e => e.CollationName)
                .HasMaxLength(128)
                .HasColumnName("COLLATION_NAME");
            entity.Property(e => e.CollationSchema)
                .HasMaxLength(128)
                .HasColumnName("COLLATION_SCHEMA");
            entity.Property(e => e.ColumnDefault)
                .HasMaxLength(4000)
                .HasColumnName("COLUMN_DEFAULT");
            entity.Property(e => e.DataType)
                .HasMaxLength(128)
                .HasColumnName("DATA_TYPE");
            entity.Property(e => e.DatetimePrecision).HasColumnName("DATETIME_PRECISION");
            entity.Property(e => e.DistinctValues).HasColumnName("DISTINCT_VALUES");
            entity.Property(e => e.DomainCatalog)
                .HasMaxLength(128)
                .HasColumnName("DOMAIN_CATALOG");
            entity.Property(e => e.DomainName)
                .HasMaxLength(128)
                .HasColumnName("DOMAIN_NAME");
            entity.Property(e => e.DomainSchema)
                .HasMaxLength(128)
                .HasColumnName("DOMAIN_SCHEMA");
            entity.Property(e => e.IsNullable)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("IS_NULLABLE");
            entity.Property(e => e.NonNulls).HasColumnName("NON_NULLS");
            entity.Property(e => e.NumericPrecision).HasColumnName("NUMERIC_PRECISION");
            entity.Property(e => e.NumericPrecisionRadix).HasColumnName("NUMERIC_PRECISION_RADIX");
            entity.Property(e => e.NumericScale).HasColumnName("NUMERIC_SCALE");
            entity.Property(e => e.OrdinalPosition).HasColumnName("ORDINAL_POSITION");
            entity.Property(e => e.SourceColumn)
                .HasMaxLength(128)
                .HasColumnName("Source_Column");
            entity.Property(e => e.SourceTable)
                .HasMaxLength(128)
                .HasColumnName("Source_Table");
            entity.Property(e => e.TableCatalog)
                .HasMaxLength(128)
                .HasColumnName("TABLE_CATALOG");

            entity.HasOne(d => d.Table).WithMany(p => p.Columns)
                .HasForeignKey(d => new { d.TableSchema, d.TableName })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schema_Table");
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Config");

            entity.Property(e => e.Databasename).HasMaxLength(70);
        });

        modelBuilder.Entity<FamilyPath>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FktableName)
                .HasMaxLength(128)
                .HasColumnName("FKTABLE_NAME");
            entity.Property(e => e.FktableOwner)
                .HasMaxLength(128)
                .HasColumnName("FKTABLE_OWNER");
            entity.Property(e => e.Fullpath)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("FULLPATH");
            entity.Property(e => e.Parentpath)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PARENTPATH");
            entity.Property(e => e.PktableName)
                .HasMaxLength(128)
                .HasColumnName("PKTABLE_NAME");
            entity.Property(e => e.PktableOwner)
                .HasMaxLength(128)
                .HasColumnName("PKTABLE_OWNER");

            entity.HasOne(d => d.Table).WithMany(t => t.ParentPaths)
                .HasForeignKey(d => new { d.FktableOwner, d.FktableName })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Table");

            entity.HasOne(d => d.TableNavigation).WithMany(t => t.ChildPaths)
                .HasForeignKey(d => new { d.PktableOwner, d.PktableName })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_Table");
        });

        modelBuilder.Entity<ForeignKey>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Deferrability).HasColumnName("DEFERRABILITY");
            entity.Property(e => e.DeleteRule).HasColumnName("DELETE_RULE");
            entity.Property(e => e.FkName)
                .HasMaxLength(128)
                .HasColumnName("FK_NAME");
            entity.Property(e => e.FkcolumnName)
                .HasMaxLength(128)
                .HasColumnName("FKCOLUMN_NAME");
            entity.Property(e => e.FktableName)
                .HasMaxLength(128)
                .HasColumnName("FKTABLE_NAME");
            entity.Property(e => e.FktableOwner)
                .HasMaxLength(128)
                .HasColumnName("FKTABLE_OWNER");
            entity.Property(e => e.FktableQualifier)
                .HasMaxLength(128)
                .HasColumnName("FKTABLE_QUALIFIER");
            entity.Property(e => e.KeySeq).HasColumnName("KEY_SEQ");
            entity.Property(e => e.PkName)
                .HasMaxLength(128)
                .HasColumnName("PK_NAME");
            entity.Property(e => e.PkcolumnName)
                .HasMaxLength(128)
                .HasColumnName("PKCOLUMN_NAME");
            entity.Property(e => e.PktableName)
                .HasMaxLength(128)
                .HasColumnName("PKTABLE_NAME");
            entity.Property(e => e.PktableOwner)
                .HasMaxLength(128)
                .HasColumnName("PKTABLE_OWNER");
            entity.Property(e => e.PktableQualifier)
                .HasMaxLength(128)
                .HasColumnName("PKTABLE_QUALIFIER");
            entity.Property(e => e.UpdateRule).HasColumnName("UPDATE_RULE");

            entity.HasOne(d => d.ForeignKeyColumn).WithMany(c => c.ForeignKeys)
                .HasForeignKey(d => new { d.FktableOwner, d.FktableName, d.FkcolumnName })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Column");

            entity.HasOne(d => d.PrimaryKeyColumn).WithMany(c => c.PrimaryKeys)
                .HasForeignKey(d => new { d.PktableOwner, d.PktableName, d.PkcolumnName })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_Column");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => new { e.TableSchema, e.TableName }).HasName("PK_Schema_Table");

            entity.Property(e => e.TableSchema)
                .HasMaxLength(128)
                .HasColumnName("TABLE_SCHEMA");
            entity.Property(e => e.TableName)
                .HasMaxLength(128)
                .HasColumnName("TABLE_NAME");
            entity.Property(e => e.ColCount).HasColumnName("COL_COUNT");
            entity.Property(e => e.RowCount).HasColumnName("ROW_COUNT");
            entity.Property(e => e.SourceTable)
                .HasMaxLength(500)
                .HasColumnName("Source_Table");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
