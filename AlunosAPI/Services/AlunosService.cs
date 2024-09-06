using AlunosAPI.Context;
using AlunosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosAPI.Services
{
    public class AlunosService : IAlunoService
    {
        private readonly AppDbContext _context;

        public AlunosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Aluno>> GetAlunoByNome(string nome)
        {
            try
            {
                IEnumerable<Aluno> alunos;
                // variável para guardar a nova lista de alunos
                if (!string.IsNullOrWhiteSpace(nome))
                // verifica se NÃO é vazio ou espaço em branco
                {
                    return alunos = await _context.Alunos.Where(aluno => aluno.Nome.Contains(nome)).ToListAsync();
                    // retorna a lista de alunos de acordo com o que vai ser digitado na instância de nome da função
                }
                else
                {
                    return alunos = await GetAlunos();
                    // se não der certo só retorna todos os alunos
                }
            }
            catch (Exception ex)
            {
               throw;
            }
        }

        public async Task<Aluno> GetAluno(int id)
        {
            try
            {
                var aluno = await _context.Alunos.FindAsync(id);
                return aluno;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CreateAluno(Aluno aluno)
        {
            try
            {
                _context.Alunos.Add(aluno);
                // primeiro faz a alteração no contexto
                await _context.SaveChangesAsync();
                // depois salva no banco de dados
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAluno(Aluno aluno)
        {
            try
            {
                _context.Entry(aluno).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            try
            {
                _context.Alunos.Remove(aluno);
                // primeiro faz a alteração no contexto
                await _context.SaveChangesAsync();
                // depois salva no banco de dados
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
