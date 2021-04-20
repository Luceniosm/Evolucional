using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolucional.Data;
using Evolucional.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evolucional.Controllers
{
    [ApiController]
    [Route("v1/notas")]
    public class NotaController : ControllerBase
    {
        [HttpGet]
        [Route("gerarNotas")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<string>> GerarNotas([FromServices] EvolucionalContext context)
        {
            var alunos = await context.Alunos.ToListAsync();
            var disciplinas = await context.Disciplinas.ToListAsync();
            
            foreach (var aluno in alunos)
            {
                foreach (var disciplina in disciplinas)
                {                    
                    var nota = new Nota(aluno.AlunoId, disciplina.DisciplinaId, GetNotaAleatoria());
                    context.Notas.Add(nota);
                }
            }

            context.SaveChanges();
            return "Notas Geradas com Sucesso";
        }

        private decimal GetNotaAleatoria()
        {
            Random random = new Random();
            return Convert.ToDecimal(random.NextDouble() * (10.00 - 0.00) + 0.00);
        }

        [HttpGet]
        [Route("gerarRelatorio")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GerarRelatorio([FromServices] EvolucionalContext context)
        {
            var alunos = await context.Alunos.ToListAsync();
            var disciplinas = await context.Disciplinas.ToListAsync();
            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Aluno");
                var currentRow = 1;
                var coluna = 1;
                worksheet.Cell(currentRow, 1).Value = "Aluno";
                worksheet.Cell(currentRow, 2).Value = "Matemática";
                worksheet.Cell(currentRow, 3).Value = "Português";
                worksheet.Cell(currentRow, 4).Value = "História";
                worksheet.Cell(currentRow, 5).Value = "Geografica";
                worksheet.Cell(currentRow, 6).Value = "Inglês";
                worksheet.Cell(currentRow, 7).Value = "Biologia";
                worksheet.Cell(currentRow, 8).Value = "FIlosofia";
                worksheet.Cell(currentRow, 9).Value = "Física";
                worksheet.Cell(currentRow, 10).Value = "Química";
                worksheet.Cell(currentRow, 11).Value = "Media";

                foreach (var aluno in alunos.OrderBy(el => el.Nome))
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = aluno.Nome;
                    worksheet.Cell(currentRow, 2).Value = aluno.Notas.Where(_ => _.Disciplina.Nome == "Matemática").FirstOrDefault().NotaAluno;
                    worksheet.Cell(currentRow, 3).Value = aluno.Notas.Where(_ => _.Disciplina.Nome == "Português").FirstOrDefault().NotaAluno;
                    worksheet.Cell(currentRow, 4).Value = aluno.Notas.Where(_ => _.Disciplina.Nome == "História").FirstOrDefault().NotaAluno;
                    worksheet.Cell(currentRow, 5).Value = aluno.Notas.Where(_ => _.Disciplina.Nome == "Geografica").FirstOrDefault().NotaAluno;
                    worksheet.Cell(currentRow, 6).Value = aluno.Notas.Where(_ => _.Disciplina.Nome == "Inglês").FirstOrDefault().NotaAluno;
                    worksheet.Cell(currentRow, 7).Value = aluno.Notas.Where(_ => _.Disciplina.Nome == "Biologia").FirstOrDefault().NotaAluno;
                    worksheet.Cell(currentRow, 8).Value = aluno.Notas.Where(_ => _.Disciplina.Nome == "FIlosofia").FirstOrDefault().NotaAluno;
                    worksheet.Cell(currentRow, 9).Value = aluno.Notas.Where(_ => _.Disciplina.Nome == "Física").FirstOrDefault().NotaAluno;
                    worksheet.Cell(currentRow, 10).Value = aluno.Notas.Where(_ => _.Disciplina.Nome == "Química").FirstOrDefault().NotaAluno;
                    worksheet.Cell(currentRow, 11).Value = (aluno.Notas.Sum(_ => _.NotaAluno) / disciplinas.Count());
                }

                var stream = new System.IO.MemoryStream();
                workbook.SaveAs(stream);                
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Faturamento.xls");
            }
        }
    }
}
