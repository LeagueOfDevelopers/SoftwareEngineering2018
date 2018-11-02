using LoDSprintApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LoDSprintApi.Filters
{
    public class ExceptionsFilterAttribute : System.Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is PermissionDeniedException)
            {
                context.Result = new ContentResult
                {
                    Content = context.Exception.Message,
                    StatusCode = 403
                };
                context.ExceptionHandled = true;
            }

            else if (context.Exception is NotFoundException)
            {
                context.Result = new ContentResult
                {
                    Content = context.Exception.Message,
                    StatusCode = 404
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
