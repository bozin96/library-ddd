﻿using System;
using System.Net;
using System.Threading.Tasks;
using Library.Web.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Library.Web.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; 

            if (ex is HttpException) code = ((HttpException)ex).HttpStatusCode;

            string result;
            if (ex.InnerException != null)
            {
                result = JsonConvert.SerializeObject(new { error = ex.Message, innerError = ex.InnerException.Message });
            }
            else
            {
                result = JsonConvert.SerializeObject(new { error = ex.Message });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
