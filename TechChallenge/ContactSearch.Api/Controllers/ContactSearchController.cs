using ContactSearch.Domain.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace ContactSearch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactSearchController(IContactUsecase _usecase) : ControllerBase
    {
        /// <summary>
        /// Retorna todos contatos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() => Ok(_usecase.GetAll());

        /// <summary>
        /// Pesquisa contato pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var model = _usecase.Get(id);
            return model != null ? Ok(model) : NotFound();
        }

        /// <summary>
        /// Pesquisa contatos de acordo com o DDD
        /// </summary>
        /// <param name="ddd">Código da região (DDD)</param>
        /// <returns></returns>
        [HttpGet("[action]/{ddd:int}")]
        public IActionResult GetByDDD(int ddd) => Ok(_usecase.GetByDDD(ddd));
    }
}
