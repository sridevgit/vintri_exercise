using Exercises.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Text.RegularExpressions;

namespace Exercises.Filters
{
    public class EmailActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is BeerUserRatings);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Provided Beer User Rating is null");
                return;
            }

            var payload = (BeerUserRatings)param.Value;

            if (string.IsNullOrEmpty(payload.UserName) || IsEmailValid(payload.UserName) == false)
            {
                context.Result = new BadRequestObjectResult("Provided username is not a email address");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public bool IsEmailValid(string emailAddress)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(emailAddress);
            if (match.Success)
                return true;
            else
                return false;
        }
    }
}
