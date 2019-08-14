using System.Collections.Generic;

namespace Agendamento.API.Contracts.V1.Responses
{
    public class ErrorResponse
    {
        public List<ErrorData> Errors { get; set; } = new List<ErrorData>();
    }
}
