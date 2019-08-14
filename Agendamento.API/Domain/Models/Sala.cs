using Agendamento.API.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agendamento.API.Domain
{
    [Table("Sala")]
    public class Sala : EntityBase
    {
        public string Descricao { get; set; }
        public IList<Agenda> Agendas { get; set; }
    }
}
