using JIWGrandAlpha.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Object = JIWGrandAlpha.Domain.Entity.Object;

namespace JIWGrandAlpha.DAL
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cell> Cells { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupObject> GroupObjects { get; set; } = null!;
        public virtual DbSet<Object> Objects { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<TeacherObject> TeacherObjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=jiwgrand;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.33-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Cell>(entity =>
            {
                entity.ToTable("cell");

                entity.HasIndex(e => e.IdClass, "idClass");

                entity.HasIndex(e => e.IdStudent, "idStudent");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.IdClass)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("idClass");

                entity.Property(e => e.IdStudent)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("idStudent");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("type");

                entity.Property(e => e.Value)
                    .HasColumnType("tinyint(89)")
                    .HasColumnName("value");

                entity.HasOne(d => d.Class)
                    .WithOne(p => p.Cell)
                    .HasForeignKey<Cell>(d => d.IdClass)
                    .HasConstraintName("cell_ibfk_1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Cells)
                    .HasForeignKey(d => d.IdStudent)
                    .HasConstraintName("cell_ibfk_2");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("class");

                entity.HasIndex(e => e.IdGroup, "idGroup");

                entity.HasIndex(e => e.IdObject, "idObject");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.IdGroup)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("idGroup");

                entity.Property(e => e.IdObject)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("idObject");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint(3)")
                    .HasColumnName("type");

                entity.Property(e => e.Date1)
                    .HasColumnName("date");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("class_ibfk_2");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.IdObject)
                    .HasConstraintName("class_ibfk_3");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("groups");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(8)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<GroupObject>(entity =>
            {
                entity.ToTable("groupsobject");

                entity.HasIndex(e => e.IdGroup, "idGroup");

                entity.HasIndex(e => e.IdObject, "idObject");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.IdGroup)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("idGroup");

                entity.Property(e => e.IdObject)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("idObject");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupsObjects)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("groupsobject_ibfk_1");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.GroupsObjects)
                    .HasForeignKey(d => d.IdObject)
                    .HasConstraintName("groupsobject_ibfk_2");
            });

            modelBuilder.Entity<Object>(entity =>
            {
                entity.ToTable("object");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(32)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.HasIndex(e => e.IdGroup, "idGroup");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(32)
                    .HasColumnName("firstname");

                entity.Property(e => e.IdGroup)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("idGroup");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(32)
                    .HasColumnName("lastname");

                entity.Property(e => e.Middlename)
                    .HasMaxLength(32)
                    .HasColumnName("middlename");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("student_ibfk_1");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teacher");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(32)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(32)
                    .HasColumnName("lastname");

                entity.Property(e => e.Middlename)
                    .HasMaxLength(32)
                    .HasColumnName("middlename");

                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<TeacherObject>(entity =>
            {
                entity.ToTable("teacherobject");

                entity.HasIndex(e => e.IdObject, "idObject");

                entity.HasIndex(e => e.IdTeacher, "idTeacher");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.IdObject)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("idObject");

                entity.Property(e => e.IdTeacher)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("idTeacher");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.TeacherObjects)
                    .HasForeignKey(d => d.IdObject)
                    .HasConstraintName("teacherobject_ibfk_1");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherObjects)
                    .HasForeignKey(d => d.IdTeacher)
                    .HasConstraintName("teacherobject_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
