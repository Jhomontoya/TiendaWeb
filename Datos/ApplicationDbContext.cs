namespace WebAppTienda.Datos
{
    using global::WebAppTienda.Models;
    using Microsoft.EntityFrameworkCore;
    //using WebAppTienda.Models;

    namespace WebAppTienda.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<LoginRegistro> LoginRegistros { get; set; }
            public ApplicationDbContext(DbSet<LoginRegistro> loginRegistros)
            {
                LoginRegistros = loginRegistros;
            }
        }
    }

}
