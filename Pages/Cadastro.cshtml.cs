using System.Globalization;
using Mercadinho.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query.Internal;

public class CadastroModel : PageModel
{
    [BindProperty]

    public Produto Produto {get; set;} = new Produto(); // objeto que vai receber os dados do form
    public string Mensagem { get;set;}
    public void OnPost()
    {
            using (var db = new AppDbContext()) {
                var produtoExistente = db.Produtos.FirstOrDefault(p => p.Nome == Produto.Nome);

                if (produtoExistente != null)
                {
                    produtoExistente.Quantidade += Produto.Quantidade;

                    Mensagem = $"Produto {Produto.Nome} já existente. Quantidade atualizada";
                } else {
                    db.Produtos.Add(Produto);

                    Mensagem = $"Produto {Produto.Nome} cadastrado com sucesso";
                    
                    System.Console.WriteLine("Produto cadastrado!");
                }
                
            db.SaveChanges();
        }
    }
}
