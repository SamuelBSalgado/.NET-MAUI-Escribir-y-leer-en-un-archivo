using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escribir_leer_enArchivo.Models
{
    public class User
    {
        public string nombre { get; set; }
        public string password { get; set; }
        public string direccion { get; set; }
        public int telefono { get; set; }
        public string correo { get; set; }

        public User(string nombre, string direccion = null,
            int telefono = 0, string correo = null,
            string password = null)
        {
            this.nombre = nombre;
            this.password = password;
            this.direccion = direccion;
            this.correo = correo;
            this.telefono = telefono;
        }
    }
}
