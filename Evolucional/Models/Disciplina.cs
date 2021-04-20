using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolucional.Models
{
    public class Disciplina
    {
        public Disciplina(string nome)
        {            
            Nome = nome;
        }

        public int DisciplinaId { get; private set; }
        public string Nome { get; private set; }
        public virtual ICollection<Nota> Notas { get; set; }

    }
}
