using System;
using System.Net;
using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private ISessionLogic sessionLogic;
        private string codeRol;

        public AuthorizationFilter( string codeRol)
        {
            this.codeRol = codeRol;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            this.sessionLogic = context.HttpContext.RequestServices.GetService<ISessionLogic>();

            string token = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "You aren't logued."
                };
            }
            else
            {
                if (!sessionLogic.IsCorrectToken(token))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 403,
                        Content = "You aren't logued correctly."
                    };
                }
                else if(token[0].ToString().ToLower() != codeRol.ToLower())
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden,
                    Content = "Not enough permissions."
                    };
                }
            }
            
        }
    }
}
