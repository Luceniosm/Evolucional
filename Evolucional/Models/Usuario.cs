using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolucional.Models
{
    public class Usuario
    {
        public Usuario(string nome, string password, string role)
        {
            Nome = nome;
            Password = password;
            Role = role;
        }

        public int UsuarioId { get; set; }
        public string Nome { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }
    }
}
