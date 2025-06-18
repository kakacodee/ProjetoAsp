using MySqlX.XDevAPI;

namespace ProjetoAsp.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public decimal quantidade { get; set; }
        public List<Produto>? TodosOsProdutos { get; set; }

    }
}
