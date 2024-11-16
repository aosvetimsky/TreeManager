using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreeManager.Contracts.Common;
using TreeManager.Contracts.Tree;
using TreeManager.Services.Api.Infrastructure.ExceptionHandling;

namespace TreeManager.Services.Api.Controllers
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IResult HandleOperationInvalidResult(OperationError error)
        {
            // just to somehow throw SecureException
            throw new SecureException(error.Message);

            // should be something like this
            if (error == TreeErrors.TreeNotFoundError)
            {
                return Results.NotFound();
            }
            // else sections with other error result to http codes mapping
        }
    }
}
