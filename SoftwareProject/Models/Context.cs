using Microsoft.EntityFrameworkCore;

namespace SoftwareProject.Models
{

    public class Context :DbContext
    {
        public Context()
        {

        }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public virtual DbSet<user> Users { get; set; }
        public virtual DbSet<admin> Admins { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
    }
}
