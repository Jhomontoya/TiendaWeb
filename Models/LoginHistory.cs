using System;
namespace WebAppTienda.Models
{
    public class LoginHistory
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Exitoso { get; set; }
    }
}
