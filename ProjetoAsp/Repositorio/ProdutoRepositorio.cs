using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using ProjetoAsp.Models;
using System.Data;
namespace ProjetoAsp.Repositorio
{
    // Define a classe UsuarioRepositorio, responsável por operações de acesso a dados para a entidade Usuario.
    public class ProdutoRepositorio(IConfiguration configuration)
    {

        // Declara um campo privado somente leitura para armazenar a string de conexão com o MySQL.
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        // Define um método público para adicionar um novo usuário ao banco de dados. Recebe um objeto 'Usuario' como parâmetro.
        public void AdicionarProduto(Produto produto)
        {
            /* Cria uma nova instância da conexão MySQL dentro de um bloco 'using'.
             Isso garante que a conexão será fechada e descartada corretamente após o uso, mesmo em caso de erro.*/
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL.

                conexao.Open();
                /* Cria um novo comando SQL para inserir dados na tabela 'Usuario'. Os valores para Nome, Email e Senha são passados como parâmetros
                 (@Nome, @Email, @Senha) para evitar SQL Injection.*/

                MySqlCommand cmd = new("INSERT INTO tbProduct (Nome, Descricao, Preco, Quant) VALUES (@Nome, @Descricao, @Preco, @Quant)", conexao);
                // Adiciona um parâmetro ao comando SQL para o campo 'Nome', utilizando o valor da propriedade 'Nome' do objeto 'usuario'.
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                // Adiciona um parâmetro ao comando SQL para o campo 'Email', utilizando o valor da propriedade 'Email' do objeto 'usuario'.
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                // Adiciona um parâmetro ao comando SQL para o campo 'Senha', utilizando o valor da propriedade 'Senha' do objeto 'usuario'.
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                // Executa o comando SQL INSERT no banco de dados. Retorna o número de linhas afetadas.
                cmd.Parameters.AddWithValue("@Quant", produto.quantidade);
                cmd.ExecuteNonQuery();
                // Fecha a conexão com o banco de dados (embora o 'using' já faria isso, só garante o fechamento).
                conexao.Close();
            }
        }
        public Produto ObterProduto(int Id)
        {
            // Cria uma nova instância da conexão MySQL dentro de um bloco 'using'.
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL.
                conexao.Open();

                // Cria um novo comando SQL para selecionar todos os campos da tabela 'Usuario' onde o campo 'Email' corresponde ao parâmetro fornecido.
                MySqlCommand cmd = new("SELECT * FROM tbProduct WHERE Id = @Id", conexao);

                // Adiciona um parâmetro ao comando SQL para o campo 'Email', especificando o tipo como VarChar e utilizando o valor do parâmetro 'email'.
                cmd.Parameters.AddWithValue("@Id", Id);

                // Executa o comando SQL SELECT e obtém um leitor de dados (MySqlDataReader). O CommandBehavior.CloseConnection garante que a conexão
                // será fechada automaticamente quando o leitor for fechado.

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Produto produto = new Produto();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                

                    // Inicializa uma variável 'usuario' como null. Ela será preenchida se um usuário for encontrado.

                    

                    // Lê a próxima linha do resultado da consulta. Retorna true se houver uma linha e false caso contrário.

                    while (dr.Read())
                    {
                        // Cria uma nova instância do objeto 'Usuario'.
                        produto = new Produto
                        {
                            // Lê o valor da coluna "Id" da linha atual do resultado, converte para inteiro e atribui à propriedade 'Id' do objeto 'usuario'.
                            Id = Convert.ToInt32(dr["Id"]),
                            // Lê o valor da coluna "Nome" da linha atual do resultado, converte para string e atribui à propriedade 'Nome' do objeto 'usuario'.
                            Nome = dr["Nome"].ToString(),
                            // Lê o valor da coluna "Email" da linha atual do resultado, converte para string e atribui à propriedade 'Email' do objeto 'usuario'.
                            Descricao = dr["Descricao"].ToString(),
                            // Lê o valor da coluna "Senha" da linha atual do resultado, converte para string e atribui à propriedade 'Senha' do objeto 'usuario'.
                            Preco = Convert.ToDecimal(dr["Preco"]),
                            quantidade = Convert.ToInt32(dr["Quant"])
                        };
                    }
                    /* Retorna o objeto 'usuario'. Se nenhum usuário foi encontrado com o email fornecido, a variável 'usuario'
                     permanecerá null e será retornado.*/
                    return produto;
                
            }
        }


        // Método para listar todos os clientes do banco de dados
        public IEnumerable<Produto> TodosOsProdutos()
        {
            // Cria uma nova lista para armazenar os objetos Cliente
            List<Produto> TodosOsProdutos = new List<Produto>();

            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar todos os registros da tabela 'cliente'
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbProduct", conexao);

                // Cria um adaptador de dados para preencher um DataTable com os resultados da consulta
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                // Cria um novo DataTable
                DataTable dt = new DataTable();
                // metodo fill- Preenche o DataTable com os dados retornados pela consulta
                da.Fill(dt);
                // Fecha explicitamente a conexão com o banco de dados 
                conexao.Close();

                // interage sobre cada linha (DataRow) do DataTable
                foreach (DataRow dr in dt.Rows)
                {
                    // Cria um novo objeto Cliente e preenche suas propriedades com os valores da linha atual
                    TodosOsProdutos.Add(
                                new Produto
                                {
                                    Id = Convert.ToInt32(dr["IdProd"]), // Converte o valor da coluna "codigo" para inteiro
                                    Nome = dr["Nome"].ToString(),
                                    // Lê o valor da coluna "Email" da linha atual do resultado, converte para string e atribui à propriedade 'Email' do objeto 'usuario'.
                                    Descricao = dr["Descricao"].ToString(),
                                    // Lê o valor da coluna "Senha" da linha atual do resultado, converte para string e atribui à propriedade 'Senha' do objeto 'usuario'.
                                    Preco = Convert.ToDecimal(dr["Preco"]),
                                    quantidade = Convert.ToInt32(dr["Quant"])
                                });
                }
                // Retorna a lista de todos os clientes
                return TodosOsProdutos;
            }
        }


    }
}