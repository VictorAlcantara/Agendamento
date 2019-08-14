using Agendamento.API.Contracts.V1;
using Agendamento.API.Contracts.V1.Requests;
using Agendamento.API.Contracts.V1.Responses;
using Agendamento.API.Domain;
using Agendamento.API.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendamento.API.Controllers
{
    public class SalaController : ControllerBase
    {
        private readonly ISalaService salaService;
        private readonly IMapper mapper;

        public SalaController(ISalaService salaService, IMapper mapper)
        {
            this.salaService = salaService;
            this.mapper = mapper;
        }

        [HttpGet(ApiRoutes.Sala.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var salas = await salaService.GetAllAsync();
            return Ok(mapper.Map<List<SalaResponse>>(salas));
        }

        [HttpGet(ApiRoutes.Sala.Get)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var sala = await salaService.GetAsync(id);
            return Ok(mapper.Map<SalaResponse>(sala));
        }

        [HttpPost(ApiRoutes.Sala.Create)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateSalaRequest salaRequest)
        {
            var sala = await salaService.AddAsync(mapper.Map<Sala>(salaRequest));

            if (sala == null)
            {
                var errorResponse = new ErrorResponse();
                errorResponse.Errors.Add(new ErrorData { Message = "Não foi possivel criar a sala" });
                return BadRequest(errorResponse);
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Sala.Get.Replace("{id}", sala.Id.ToString());

            return Created(locationUri, new { });
        }

        [HttpPut(ApiRoutes.Sala.Update)]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateSalaRequest salaRequest)
        {
            var sala = await salaService.GetAsync(salaRequest.Id);
            await salaService.UpdateAsync(mapper.Map(salaRequest, sala));

            return Ok(mapper.Map<SalaResponse>(sala));
        }

        [HttpDelete(ApiRoutes.Sala.Delete)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();

            if (await salaService.RemoveAsync(id))
                deleteResponse.Removed = true;
            else
                deleteResponse.Removed = false;

            return Ok(deleteResponse);
        }
    }
}
