namespace API.Dtos
{
    public class ResponseDto
    {
        public bool IsExitoso { get; set; }

        public object Resultado { get; set; }
        public string Mensaje { get; set; }
    }
}
