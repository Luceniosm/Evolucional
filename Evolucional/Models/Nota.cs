using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Evolucional.Models
{
    public class Nota
    {
        public Nota(int alunoId, int disciplinaId, decimal notaAluno)
        {
            AlunoId = alunoId;
            DisciplinaId = disciplinaId;
            NotaAluno = notaAluno;
        }

        public int NotaId { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        
        [Range(1,10,ErrorMessage ="A nota deve estar entre 0,00 e 10,00")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal NotaAluno { get; set; }
        public virtual Aluno Aluno { get; set; }
        public virtual Disciplina Disciplina { get; set; }
    }
}
