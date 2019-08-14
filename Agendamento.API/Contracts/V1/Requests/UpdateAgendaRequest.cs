using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendamento.API.Contracts.V1.Requests
{
    public class UpdateAgendaRequest
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime HorarioInicio { get; set; }
        public DateTime HorarioFim { get; set; }
        public int SalaId { get; set; }
    }
}
