using System.Globalization;
using Mercadinho.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query.Internal;

public class CadastroModel : PageModel
{
    [BindProperty]

    public Produto Produto { get; set; } = new Produto(); // objeto que vai receber os dados do form
    public string Mensagem { get; set; }
    public void OnPost()
    {
        Produto.Nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Produto.Nome.ToLower());

        // amanha tentar colocar o campo de "nome do produto" como uma lista de produtos já cadastrados no sistema, e o usuario vai ter a opçao de adicionar um novo produto na lista, 
        // assim posso evitar o caso de produtos duplicados no banco
        using (var db = new AppDbContext())
        {
            var produtoExistente = db.Produtos.FirstOrDefault(p => p.Nome == Produto.Nome);

            if (produtoExistente != null)
            {
                produtoExistente.Quantidade += Produto.Quantidade;

                Mensagem = $"Produto {Produto.Nome} já existente. Quantidade atualizada";
            }
            else
            {
                db.Produtos.Add(Produto);

                Mensagem = $"Produto {Produto.Nome} cadastrado com sucesso";

                System.Console.WriteLine("Produto cadastrado!");
            }

            db.SaveChanges();
        }
    }

    public List<string> ProdutosCadastrados { get; set; } = new List<string>();

    public void OnGet()
    {
        using (var db = new AppDbContext())
        {
            ProdutosCadastrados = db.Produtos
                .Select(p => p.Nome)
                .Distinct()
                .OrderBy(n => n)
                .ToList();
        }
    }
}

