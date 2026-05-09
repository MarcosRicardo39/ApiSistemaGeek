namespace ApiSistemaGeek.Model
{
    public class ProdutosGeek
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Categoria { get; set; }   // jogo, anime, filme, HQ...

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public int AnoLancamento { get; set; }

        public string? Plataforma { get; set; }   // PS5, PC, Xbox, etc.

        public string? ImagemUrl { get; set; }

        public double Nota { get; set; }    // avaliação tipo 0 a 10
    }
}
