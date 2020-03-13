using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckManager.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Select(x =>
                    new ResponseObject
                    {
                        fieldName = x.Key,
                        errorMessages = x.Value.Errors
                            .Select(e => e.ErrorMessage)
                    });

                context.Result = new BadRequestObjectResult(errors);
            }
        }
    }
}
