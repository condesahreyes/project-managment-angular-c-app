﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Filters
{
    [ExcludeFromCodeCoverage]
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        private const string errorGeneric = "An unexpected error occurred";

        public ExceptionFilter() { }

        public void OnException(ExceptionContext context)
        {
            try
            {
                throw context.Exception;
            }
            catch (ExistingObjectException exception)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    Content = exception.Message
                };
            }
            catch (InvalidDataObjException exception)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    Content = exception.Message
                };
            }
            catch (NoObjectException exception)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 404,
                    Content = exception.Message
                };
            }
            catch (Exception)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 404,
                    Content = errorGeneric
                };
            }
        }
    }
}
