using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CarRent.Middleware
{
    public class ErrorHandlingMiddleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context,RequestDelegate next)
        {
			try
			{
				await next.Invoke(context);
			}
			catch (InvalidOperationException e)
			{
				await context.Response.WriteAsync("Nie można wypożyczyć auta o danym Id");
			}
			catch(InvalidEnumArgumentException e)
			{
                await context.Response.WriteAsync("Zła klasa");
            }
        }
    }
}
