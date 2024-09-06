using AlunosAPI.Models;
using AlunosAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlunosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        // função assincrona
        // ActionResult == usamos para retornar um valor equivalente ao response, componente do request de uma api
        // IAsyncEnumerable == mesma coisa o IEnumerable, única diferença é que não é uma interação sincrona 
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                // armazena a lista dos alunos, se atente a ver se é assincrono ou não
                return Ok(alunos);
                // retorna um status 200 com a lista dos alunos, essa função de Ok() é do tipo ActionResult
            }
            catch
            {
                // return BadRequest("Request inválido"); == forma mais simples de se fazer o tratamento de erro
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
                // forma mais personalizada
            }
        }

        [HttpGet("AlunosPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByName([FromQuery] string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunoByNome(nome);
                if (alunos == null)
                {
                    return NotFound($"Não existem alunos com o critério {nome}");
                }
                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpGet("{id:int}", Name ="GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if(aluno == null)
                {
                    return NotFound($"Não existe aluno com o id={id}");
                }
                return Ok(aluno); 
            } catch 
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAluno(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
                // retornar informando que o aluno foi criado com sucesso e passando ele mesmo no corpo da response
            } catch
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAluno(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if(aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return Ok($"Aluno com o id={id} foi atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if(aluno == null)
                {
                    return NotFound($"Não existe aluno com o id={id}");
                } else
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"Aluno de id={id} foi excluido com sucesso");
                }
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
