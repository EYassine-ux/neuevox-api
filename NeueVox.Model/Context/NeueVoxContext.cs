using Microsoft.EntityFrameworkCore;

namespace NeueVox.Model.NeuevoxModel.Context;

public class NeueVoxContext : DbContext
{
    public DbSet<User> Users {get; set;}
    public DbSet<Student> Students {get; set;}
    public DbSet<Professor> Professors {get; set;}
    public DbSet<Class> Classes {get; set;}
    public DbSet<StudentClass> StudentClasses {get; set;}
    public DbSet<Evaluation> Evaluations {get; set;}
    public DbSet<Grade> Grades {get; set;}
    public DbSet<Course> Courses {get; set;}
    public DbSet<Program> Programs {get; set;}
    public DbSet<StudentSubmission> StudentSubmissions {get; set;}
    public DbSet<Document> Documents {get; set;}



    public NeueVoxContext(DbContextOptions<NeueVoxContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().Property(user => user.Role).HasConversion<string>();
        modelBuilder.Entity<Professor>().Property(p => p.Role).HasConversion<string>();
        modelBuilder.Entity<Student>().Property(s => s.Role).HasConversion<string>();
        modelBuilder.Entity<Evaluation>().Property(e=>e.EvaluationType).HasConversion<string>();

        modelBuilder.Entity<Professor>().ToTable("Professors");
        modelBuilder.Entity<Student>().ToTable("Students");

        // Program -> Student (One to Many)
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Program)
            .WithMany(p => p.Students)
            .HasForeignKey(s => s.ProgramId);

        //Professor -> Class (One to Many)
        modelBuilder.Entity<Class>()
            .HasOne(c => c.Professor)
            .WithMany(p => p.Classes)
            .HasForeignKey(c => c.ProfessorId);

       //Course -> Class (One to Many)
        modelBuilder.Entity<Class>()
            .HasOne(c=>c.Course)
            .WithMany(p => p.Classes)
            .HasForeignKey(c=>c.CourseId);

        //Class -> StudentClass (One to Many)
        modelBuilder.Entity<StudentClass>()
            .HasOne(c=>c.Class)
            .WithMany(p => p.StudentClasses)
            .HasForeignKey(f=>f.ClassId);

        //Student -> StudentClass (One to Many)
        modelBuilder.Entity<StudentClass>()
            .HasOne(c=>c.Student)
            .WithMany(p => p.StudentClasses)
            .HasForeignKey(f=>f.StudentId);

        //Class -> Evaluation (One to Many)
        modelBuilder.Entity<Evaluation>()
            .HasOne(e=>e.Class)
            .WithMany(c=>c.Evaluations)
            .HasForeignKey(e=>e.ClassId);

        //Class -> Document (One to Many)
        modelBuilder.Entity<Document>()
          .HasOne(d=>d.Class)
          .WithMany(d=>d.Documents)
          .HasForeignKey(d=>d.ClassId);

        //Student -> Grade (One to Many)
        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Student)
            .WithMany( s=>s.Grades)
            .HasForeignKey(g=>g.StudentId);

        //Student -> StudentSubmission (One to Many)
        modelBuilder.Entity<StudentSubmission>()
          .HasOne(s=>s.Student)
          .WithMany(s=>s.StudentSubmissions)
          .HasForeignKey(s=>s.StudentId);

        //Evaluation -> Grade (One to Many)
        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Evaluation)
            .WithMany( e=>e.Grades)
            .HasForeignKey(g=>g.EvaluationId);

        //Evaluation -> StudentSubmission (One to Many)
        modelBuilder.Entity<StudentSubmission>()
          .HasOne(s=>s.Evaluation)
          .WithMany(s=>s.StudentSubmissions)
          .HasForeignKey(s=>s.EvaluationId);




    }
}
