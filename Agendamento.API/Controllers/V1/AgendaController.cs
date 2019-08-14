using Agendamento.API.Contracts.V1;
using Agendamento.API.Contracts.V1.Requests;
using Agendamento.API.Contracts.V1.Responses;
using Agendamento.API.Domain.Interfaces.Services;
using Agendamento.API.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendamento.API.Controllers.V1
{
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaService agendaService;
        private readonly IMapper mapper;

        public AgendaController(IAgendaService agendaService, IMapper mapper)
        {
            this.agendaService = agendaService;
            this.mapper = mapper;
        }

        [HttpGet(ApiRoutes.Agenda.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            var agendas = await agendaService.GetAllAsync();
            return Ok(mapper.Map<List<AgendaResponse>>(agendas));
        }

        [HttpGet(ApiRoutes.Agenda.Get)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var agenda = await agendaService.GetAsync(id);
            return Ok(mapper.Map<AgendaResponse>(agenda));
        }

        [HttpPost(ApiRoutes.Agenda.Create)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateAgendaRequest agendaRequest)
        {
            var agenda = await agendaService.AddAsync(mapper.Map<Agenda>(agendaRequest));

            if (agenda == null)
            {
                var errorResponse = new ErrorResponse();
                errorResponse.Errors.Add(new ErrorData { Message = "Não foi possivel criar um agendamento" });
                return BadRequest(errorResponse);
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Agenda.Get.Replace("{id}", agenda.Id.ToString());

            return Created(locationUri, new { });
        }

        [HttpPut(ApiRoutes.Agenda.Update)]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateSalaRequest agendaRequest)
        {
            var agenda = await agendaService.GetAsync(agendaRequest.Id);
            await agendaService.UpdateAsync(mapper.Map(agendaRequest, agenda));

            return Ok(mapper.Map<AgendaResponse>(agenda));
        }

        [HttpDelete(ApiRoutes.Agenda.Delete)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();

            if (await agendaService.RemoveAsync(id))
                deleteResponse.Removed = true;
            else
                deleteResponse.Removed = false;

            return Ok(deleteResponse);
        }
    }
}
