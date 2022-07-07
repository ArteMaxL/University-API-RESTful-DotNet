using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContext: DbContext 
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options): base(options) { }

        // TODO: Add DbSets (Tables of our Data base)

        public DbSet<User>? Users { get; set; }

        public DbSet<Curso>? Cursos { get; set; }
    }
}
