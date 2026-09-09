namespace ApiSistemaGeek.Model
{
    public class PedidoItem
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }

        public int ProdutoGeekId { get; set; }

        public int Quantidade { get; set; }

        public decimal Preco { get; set; }
    }
}
