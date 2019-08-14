using System;

namespace Agendamento.API.Contracts.V1.Requests
{
    public class CreateAgendaRequest
    {
        public string Titulo { get; set; }
        public DateTime HorarioInicio { get; set; }
        public DateTime HorarioFim { get; set; }
        public int SalaId { get; set; }
    }
}
