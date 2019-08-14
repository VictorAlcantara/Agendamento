using System;

namespace Agendamento.API.Contracts.V1.Responses
{
    public class AgendaResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime HorarioInicio { get; set; }
        public DateTime HorarioFim { get; set; }
        public SalaResponse Sala { get; set; }
    }
}
