public class Log {
    public int Id { get; set; }
    public string NomeProduto { get; set; } = "";
    
    public string TipoOperacao { get; set; } = "";
    public int Quantidade { get; set; } 
    public DateTime DataOperacao { get; set; } = DateTime.Now;
}