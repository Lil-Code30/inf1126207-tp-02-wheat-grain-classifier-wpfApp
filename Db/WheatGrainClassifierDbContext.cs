using WheatGrainClassifierWpfApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WheatGrainClassifierWpfApp.Db
{
    public class WheatGrainClassifierDbContext : DbContext
    {
        public DbSet<Experiment> Experiments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=DESKTOP-LDNM8KV\\SQLEXPRESS;Initial Catalog=WheatGrainClassifierDB;Integrated Security=True;Encrypt=False";
            string dataBaseName = "WheatGrainClassifierDB";

            optionsBuilder.UseSqlServer($"{connectionString};Database={dataBaseName}");
        }
    }
}
