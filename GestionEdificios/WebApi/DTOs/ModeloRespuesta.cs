namespace GestionEdificios.WebApi.DTOs
{
    public class ModeloRespuesta<T>
    {
        public int Codigo { get; set; }
        public T Contenido { get; set; }
        public string Mensaje { get; set; }
    }
}
