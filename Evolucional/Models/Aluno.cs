using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Evolucional.Models
{
    public class Aluno
    {
        public Aluno(string nome, DateTime dataNascimento)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
        }

        [Key]
        public int AlunoId { get; set; }
        [Required(ErrorMessage = "Este campo é Obrigátorio")]
        [MinLength(3,ErrorMessage = "Este campo deve conter no minimo 3 caracteres")]
        public string  Nome { get; set; }
        [Required(ErrorMessage = "Este campo é Obrigátorio")]
        public DateTime DataNascimento { get; set; }
        public virtual ICollection<Nota> Notas { get; set; }
    }
}
