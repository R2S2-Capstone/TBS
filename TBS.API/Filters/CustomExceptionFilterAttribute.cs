using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using TBS.Data.Exceptions.Posts;
using TBS.Data.Exceptions.Posts.Carrier;
using TBS.Data.Exceptions.Posts.Shipper;

namespace TBS.API.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute, IActionFilter
    {
        public override void OnException(ExceptionContext context)
        {
            var code = HttpStatusCode.InternalServerError;

            switch (context.Exception)
            {
                case InvalidCarrierPostException  _:
                    code = HttpStatusCode.NoContent;
                    break;
                case InvalidShipperPostException _:
                    code = HttpStatusCode.NoContent;
                    break;
                case FailedToUpdatePostException _:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message }
            });
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new
            {
                errors = context.ModelState
                    .Select(x => x.Value.Errors.Select(m => m.ErrorMessage))

            });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
