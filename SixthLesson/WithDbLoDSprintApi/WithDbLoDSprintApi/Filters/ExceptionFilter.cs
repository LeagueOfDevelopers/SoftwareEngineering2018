using BusinessServices.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WithDbLoDSprintApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case PermissionDeniedException _:
                    context.Result = new ContentResult
                    {
                        Content = context.Exception.Message,
                        StatusCode = 403
                    };
                    context.ExceptionHandled = true;
                    break;
                case NotFoundException _:
                    context.Result = new ContentResult
                    {
                        Content = context.Exception.Message,
                        StatusCode = 404
                    };
                    context.ExceptionHandled = true;
                    break;
                case IncorrectAnswersException _:
                    context.Result = new ContentResult
                    {
                        Content = context.Exception.Message,
                        StatusCode = 400
                    };
                    context.ExceptionHandled = true;
                    break;
            }
        }
    }
}
