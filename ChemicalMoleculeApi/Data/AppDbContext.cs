using ChemicalMoleculeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChemicalMoleculeApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Molecule> Molecules => Set<Molecule>();
    }
}
