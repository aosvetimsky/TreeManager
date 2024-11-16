using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TreeManager.Contracts.ErrorJournal;

namespace TreeManager.Services.Api.Infrastructure.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorJournalService = httpContext.RequestServices.GetRequiredService<IErrorJournalAppService>();

            string type = exception.GetType().Name;
            string stackTrace= exception.StackTrace;
            string message = exception switch
            {
                SecureException => type,
                _ => "Internal Server Error"
            };

            int id = await errorJournalService.CreateErrorEntry(new ErrorEntryDto
            {
                CreatedAt = DateTime.UtcNow,
                RequestDetails = await ReadHttpRequestDetails(httpContext.Request),
                StackTrace = stackTrace,
                ErrorType = type,
            });

            var createdErrorEntry = await errorJournalService.GetErrorEntryById(id);

            string details = exception switch
            {
                SecureException => exception.Message,
                _ => $"{message} EventID = {createdErrorEntry.EventId}"
            };

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = type,
                Detail = details
            });

            return await ValueTask.FromResult(true);
        }

        private async Task<string> ReadHttpRequestDetails(HttpRequest request)
        {
            string details = string.Empty;
            var body = await request.BodyReader.ReadAsync();
            details += $"Query: {JsonConvert.SerializeObject(request.Query)}";
            details += $"Body: {JsonConvert.SerializeObject(body)}";
            return details;
        }
    }
}
