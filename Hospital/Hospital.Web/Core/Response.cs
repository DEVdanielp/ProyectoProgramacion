using Hospital.Web.Data.Entities;

namespace Hospital.Web.Core
{

    //Se crea la clase respuesta para tener una respuesta predefinida a las consultas
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<String> Errors { get; set; }
        public T Result { get; set; }

    }
}
