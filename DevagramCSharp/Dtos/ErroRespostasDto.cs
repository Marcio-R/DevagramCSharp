namespace DevagramCSharp.Dtos
{
    public class ErroRespostasDto
    {
        public int Status { get; set; }
        public string Descricao { get; set; }
        public List<string> Erros { get; set; }
    }
}
