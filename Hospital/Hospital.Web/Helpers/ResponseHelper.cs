﻿using Hospital.Web.Core;
using Hospital.Web.Data.Entities;

namespace Hospital.Web.Helpers
{
    public static class ResponseHelper<T>
    {

        public static Response<T> MakeResponseSuccess(T model, string message = "Tarea realizada con exito")
        {
            return new Response<T>
            {
                IsSuccess = true,
                Message = message,
                Result = model
            };
        }
        public static Response<T> MakeResponseFail(Exception ex, string message = "Error al generar la solucitud")
        {
            return new Response<T>
            {
                Errors = new List<string>
                {
                    ex.Message,
                },
                IsSuccess = false,
                Message = message        
            };
        }

        public static Response<T> MakeResponseFail(string message)
        {
            return new Response<T>
            {
                Errors = new List<string>
                {
                    message
                },
                IsSuccess = false,
                Message = message
            };
        }

        internal static Response<IEnumerable<Permission>> MakeResponseSuccess(IEnumerable<Permission> permissions)
        {
            throw new NotImplementedException();
        }
    }
}
