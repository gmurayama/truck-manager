using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace TruckManager.Api.Filters
{
    public class ErrorFormatResult : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult result &&
                result.StatusCode.HasValue &&
                result.StatusCode.Value != (int)HttpStatusCode.OK)
            {
                LogRequestBody(context.HttpContext.Request);

                if (result.Value is Exception exception)
                {
                    result.Value = new List<ResponseObject>
                    {
                        new ResponseObject
                        {
                            fieldName = "Global",
                            errorMessages = new List<string>(){ exception.Message }
                        }
                    };
                }
                else if (result.Value is IEnumerable<Exception> exceptions)
                {
                    result.Value = new List<ResponseObject>
                    {
                        new ResponseObject
                        {
                            fieldName = "Global",
                            errorMessages = exceptions.Select(e => e.Message)
                        }
                    };
                }
                else if (result.Value is ValidationResult validationResult)
                {
                    result.Value = validationResult.Errors
                        .Select(error => new ResponseObject
                        {
                            fieldName = error.PropertyName,
                            errorMessages = validationResult.Errors
                                .Where(e => e.PropertyName.Equals(e.PropertyName))
                                .Select(e => e.ErrorMessage)
                        });
                }
            }
        }

        private void LogRequestBody(HttpRequest request)
        {
            string body = "";

            request.EnableRewind();
            request.Body.Position = 0;

            using (var reader = new StreamReader(stream: request.Body,
                                                          encoding: Encoding.UTF8,
                                                          detectEncodingFromByteOrderMarks: true,
                                                          bufferSize: 2048,
                                                          leaveOpen: true))
            {
                body = reader.ReadToEnd().Trim();
            }

            request.Body.Position = 0;

            body = string.IsNullOrEmpty(body) ? "(empty body)" : body;

            Log.Information($"Request body: {body}");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
