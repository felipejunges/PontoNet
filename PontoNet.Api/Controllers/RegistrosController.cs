using MediatR;
using Microsoft.AspNetCore.Mvc;
using PontoNet.Api.Models.Registros.Mappers;
using PontoNet.Api.Models.Registros.Requests;
using PontoNet.Api.Models.Registros.Responses;
using PontoNet.Domain.Commands.Registros.AlterarRegistro;
using PontoNet.Domain.Commands.Registros.ExcluirRegistro;
using PontoNet.Domain.Commands.Registros.IncluirRegistro;
using PontoNet.Domain.Commands.Registros.ListarRegistrosData;
using PontoNet.Domain.Commands.Registros.ObterResumoDiario;
using PontoNet.Domain.Commands.Registros.RegistrarHorarioData;
using System.Net;

namespace PontoNet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegistrosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{data}")]
        [ProducesResponseType(typeof(IEnumerable<RegistroResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IEnumerable<RegistroResponse>> ListarRegistrosDataAsync([FromRoute] DateTime data)
        {
            var registros = await _mediator.Send(new ListarRegistrosDataCommand(data));

            return RegistroResponseMapper.Map(registros);
        }

        [HttpGet("resumo-diario")]
        [ProducesResponseType(typeof(ResumoDiarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ResumoDiarioResponse> ObterResumoDiarioAsync()
        {
            var resumo = await _mediator.Send(new ObterResumoDiarioCommand());

            if (resumo == null)
                return null!;

            return ResumoDiarioResponseMapper.Map(resumo);
        }

        [HttpPut("registrar-horario")]
        [ProducesResponseType(typeof(IEnumerable<RegistroResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> RegistrarHorarioDataAsync([FromQuery] DateTime data)
        {
            var registro = await _mediator.Send(new RegistrarHorarioDataCommand(data));

            return Ok(RegistroResponseMapper.Map(registro));
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegistroResponse), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<RegistroResponse>> IncluirRegistroAsync([FromBody] IncluirRegistroRequest request)
        {
            var registro = await _mediator.Send(new IncluirRegistroCommand(request.Data, request.Hora));

            return Created("[controller]/{id}", RegistroResponseMapper.Map(registro));
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<RegistroResponse>> AlterarRegistroAsync([FromRoute] long id, [FromBody] AlterarRegistroRequest request)
        {
            await _mediator.Send(new AlterarRegistroCommand(id, request.Data, request.HoraInicial, request.HoraFinal));

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<RegistroResponse>> ExcluirRegistroAsync([FromRoute] long id)
        {
            await _mediator.Send(new ExcluirRegistroCommand(id));

            return Ok();
        }
    }
}