using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agendamento.API.Domain.Models
{
    [Table("Agenda")]
    public class Agenda : EntityBase
    {
        public string Titulo { get; set; }
        public DateTime HorarioInicio { get; set; }
        public DateTime HorarioFim { get; set; }
        public int SalaId { get; set; }
        public Sala Sala { get; set; }
    }
}
