namespace WebAppTienda.Datos
{
    //using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    //using WebAppTienda.Models;
    using global::WebAppTienda.Models;
    using Microsoft.EntityFrameworkCore;

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
            public DbSet<LoginHistory> LoginHistories { get; set; }
            public ApplicationDbContext(DbSet<LoginRegistro> loginRegistros)
            {
                LoginRegistros = loginRegistros;
            }
        }
    }

}
