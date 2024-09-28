﻿using Hospital.Web.Core;

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
    }
}
