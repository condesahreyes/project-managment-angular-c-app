using System;
using System.Collections.Generic;
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
        private List<string> codeRol;

        public AuthorizationFilter(string codeRol)
        {
            AddDiferentsCodeRoles(codeRol);
        }

        private void AddDiferentsCodeRoles(string code) {

            codeRol = new List<string>();
            string[] ret = code.Split(",");
            for (int i = 0; i < ret.Length; i++)
            {
                codeRol.Add(ret[i].ToLower());
            }
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
                string rol = (token.Split("-"))[0].ToString().ToLower();
                if (!sessionLogic.IsCorrectToken(token))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 403,
                        Content = "You aren't logued correctly."
                    };
                }
                else if(!codeRol.Contains(rol))
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
