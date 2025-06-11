using Microsoft.AspNetCore.Mvc;
using ProjetoAsp.Models;
using ProjetoAsp.Repositorio;

namespace ProjetoAsp.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ChecarProdutos()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ChecarProdutos(string Nome, string Descricao)
        {
            /* Chama o método ObterUsuario do _usuarioRepositorio, passando o email fornecido pelo usuário.
            Isso buscará um usuário no banco de dados com o email correspondente.*/

            var produto = _produtoRepositorio.ObterProduto(Nome);

            // Verifica se um usuário foi encontrado for diferente de vazio e se a senha fornecida corresponde à senha do usuário encontrado.

            if (produto != null && produto.Descricao == Descricao)
            {
                // Autenticação bem-sucedida
                // Redireciona o usuário para a action "Index" do Controller "Cliente".

                return RedirectToAction("Home", "Home");
            }

            /* Se a autenticação falhar (usuário não encontrado ou senha incorreta):
            Adiciona um erro ao ModelState. ModelState armazena o estado do modelo e erros de validação.
            O primeiro argumento ("") indica um erro
            O segundo argumento é a mensagem de erro que será exibida ao usuário.*/

            ModelState.AddModelError("", "Produto inválido.");

            // retorna view login

            return View();
        }
        // Define uma action chamada Cadastro que retorna um IActionResult.
        public IActionResult CadastroProdutos()
        {
            // Retorna a View  Cadastro.
            return View();

        }
        [HttpPost]
        public IActionResult CadastroProdutos(Produto produto)
        {
            // Verifica se o ModelState é válido. O ModelState é considerado válido se não houver erros de validação.
            if (ModelState.IsValid)
            {
                /* Se o modelo for válido:
                 Chama o método AdicionarUsuario do _usuarioRepositorio, passando o objeto Usuario recebido.
                 Isso  salvará as informações do novo usuário no banco de dados.*/

                _produtoRepositorio.AdicionarProduto(produto);

                /* Redireciona o usuário para a action "Login" deste mesmo Controller (LoginController).
                  após um cadastro bem-sucedido, redirecionará à página de login.*/
                return RedirectToAction("Login");
            }

            /* Se o ModelState não for válido (houver erros de validação):
             Retorna a View de Cadastro novamente, passando o objeto Usuario com os erros de validação.
             Isso permite que a View exiba os erros para o usuário corrigir o formulário.*/
            return View(produto);


        }
    }
}
