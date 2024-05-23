using Microsoft.AspNetCore.Mvc;
using TechChallenge.API.Services;
using TechChallenge.Core.Models;

namespace TechChallenge.API.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class ContatoController(IContatoService _service) : ControllerBase
    {
        /// <summary>
        /// Retorna todos contatos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() => Ok(_service.GetAll());

        /// <summary>
        /// Pesquisa contato pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(_service.Get(id));

        /// <summary>
        /// Pesquisa contatos de acordo com o DDD
        /// </summary>
        /// <param name="ddd">Código da região (DDD)</param>
        /// <returns></returns>
        [HttpGet("[action]/{ddd:int}")]
        public IActionResult GetByDDD(int ddd) => Ok(_service.GetByDDD(ddd));

        /// <summary>
        /// Insere um contato
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ContatoInsertModel model)
        {
            _service.Insert(model);
            return Ok();
        }

        /// <summary>
        /// Atualiza um contato
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(ContatoUpdateModel model)
        {
            _service.Update(model);
            return Ok();
        }

        /// <summary>
        /// Deleta um contato
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
