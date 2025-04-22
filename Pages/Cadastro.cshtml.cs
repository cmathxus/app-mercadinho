using System.Globalization;
using Mercadinho.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query.Internal;

public class CadastroModel : PageModel // Fazer historico de baixas e inclusões no sistema, fazer um carrinho e uma lista para filtrar o produto pelo ID no banco de dados
{
    [BindProperty]

    public Produto Produto { get; set; } = new Produto(); // objeto que vai receber os dados do form

    [TempData]
    public string Mensagem { get; set; }
    public IActionResult OnPost()
    {
        Produto.Nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Produto.Nome.ToLower());

        using (var db = new AppDbContext())
        {
            db.Database.EnsureCreated();
            var produtoExistente = db.Produtos.FirstOrDefault(p => p.Nome == Produto.Nome);

            if (produtoExistente != null)
            {
                produtoExistente.Quantidade += Produto.Quantidade;

                Mensagem = $"Produto {Produto.Nome} já existente. Quantidade atualizada";
                System.Console.WriteLine($"Produto {Produto.Nome} já existente");
            }
            else
            {
                db.Produtos.Add(Produto);

                Mensagem = $"Produto {Produto.Nome} cadastrado com sucesso";

                System.Console.WriteLine($"Produto {Produto.Nome} cadastrado com sucesso!");

            }

            db.SaveChanges();

            var quantidadeAnterior = produtoExistente.Quantidade;
            produtoExistente.Quantidade += Produto.Quantidade;

            string tipoOperacao;

            if (Produto.Quantidade > 0) {
                tipoOperacao = produtoExistente != null ? "Inclusão" : "Cadastro";
            } 
            else if (Produto.Quantidade < 0) {
                tipoOperacao = "Baixa";
            } 
            else {
                tipoOperacao = "Sem alteração";
            }

            Console.WriteLine("Produto recebido:");
            Console.WriteLine($"Nome: {Produto?.Nome}");
            Console.WriteLine($"Quantidade: {Produto?.Quantidade}");
            
            var log = new Log
            {
                NomeProduto = Produto.Nome,
                TipoOperacao = tipoOperacao,
                Quantidade = Produto.Quantidade,
                DataOperacao = DateTime.Now
            };

            db.Logs.Add(log);
            db.SaveChanges();

            Console.WriteLine($"Log: {log.NomeProduto} - {log.TipoOperacao} - {log.Quantidade} - {log.DataOperacao}");
        }

        return RedirectToPage("Cadastro");
    }

    public List<string> ProdutosCadastrados { get; set; } = new List<string>();

    public void OnGet()
    {
        using (var db = new AppDbContext())
        {
            db.Database.EnsureCreated();
            ProdutosCadastrados = db.Produtos
                .Select(p => p.Nome)
                .Distinct()
                .OrderBy(n => n)
                .ToList();
        }
    }
}

