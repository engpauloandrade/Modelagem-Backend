using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelagem.src.Database;
using Modelagem.src.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Modelagem.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly MySqlDBContext _context;

        public PessoaController(MySqlDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pessoa>> GetPessoas()
        {
            return _context.Pessoa.Include(p => p.Cidade).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Pessoa> GetPessoa(int id)
        {
            var pessoa = _context.Pessoa.Find(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }

        /// <summary>
        /// Cadastro de uma pessoa
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Pessoa cadastrada com sucesso</response>
        /// <response code="400">Valores inválidos</response>
        /// <response code="500">Oops! Estamos com problema!</response>
        [HttpPost]
        [SwaggerResponse(200, "Pessoa cadastrada com sucesso")]
        [SwaggerResponse(400, "Valores inválidos")]
        [SwaggerResponse(500, "Oops! Estamos com problema!")]
        public ActionResult<Pessoa> AddPessoa(Pessoa pessoa)
        {
            try
            {
                _context.Pessoa.Add(pessoa);
                _context.SaveChanges();

                return Ok("Pessoa cadastrada com sucesso");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        /// <summary>
        /// Atualização de uma pessoa
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Pessoa cadastrada com sucesso</response>
        /// <response code="400">Valores inválidos</response>
        /// <response code="500">Oops! Estamos com problema!</response>

        [HttpPut("{id}")]

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UpdatePessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoa).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Excluir uma pessoa cadastrada na base de dados
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Pessoa excluída com sucesso</response>
        /// <response code="400">Valores inválidos</response>
        /// <response code="500">Oops! Estamos com problema!</response>
        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Pessoa excluída com sucesso")]
        [SwaggerResponse(400, "Valores inválidos")]
        [SwaggerResponse(500, "Oops! Estamos com problema!")]
        public ActionResult<Pessoa> DeletePessoa(int id)
        {
            try
            {
                var pessoa = _context.Pessoa.Find(id);

                _context.Pessoa.Remove(pessoa);
                _context.SaveChanges();

                return Ok("Pessoa excluída com sucesso!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
