namespace ApiSistemaGeek.Model
{
    public class ProdutosGeek
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Categoria { get; set; }

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public int AnoLancamento { get; set; }

        public string? Plataforma { get; set; }
        public string? ImagemUrl { get; set; }

        public double Nota { get; set; }
    }
}
