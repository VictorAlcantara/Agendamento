using Agendamento.API.Contracts.V1.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Agendamento.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(w => w.Value.Errors.Count > 0)
                    .ToDictionary(pair => pair.Key, pair => pair.Value.Errors.Select(s => s.ErrorMessage)).ToList();

                var errorResponse = new ErrorResponse();

                foreach (var fieldWithError in errors)
                {
                    foreach (var error in fieldWithError.Value)
                    {
                        errorResponse.Errors.Add(new ErrorData
                        {
                            FieldName = fieldWithError.Key,
                            Message = error
                        });
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}
