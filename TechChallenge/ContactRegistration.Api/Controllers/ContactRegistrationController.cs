using ContactRegistration.Domain.Usecases;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Models;

namespace ContactRegistration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactRegistrationController(IContactRegistrationUsecase _usecase) : ControllerBase
    {
        /// <summary>
        /// Insere um contato
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(InsertContactModel model)
        {
            _usecase.Insert(model);
            return Ok();
        }

        /// <summary>
        /// Atualiza um contato
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(UpdateContactModel model)
        {
            _usecase.Update(model);
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
            _usecase.Delete(id);
            return Ok();
        }
    }
}
