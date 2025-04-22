using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mercadinho.models;
using System.Dynamic;

public class EditarListaModel : PageModel
{
    [BindProperty]
    public Produto Produto { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        using (var db = new AppDbContext())
        {
            Produto = await db.Produtos.FindAsync(id);
            if (Produto == null)
            {
                return RedirectToPage("/Lista");
            }
        }

        return Page();
    }



    public async Task<IActionResult> OnPostAsync()
    {
        using (var db = new AppDbContext())
        {
            var produtoNoBanco = await db.Produtos.FindAsync(Produto.Id);

            if (produtoNoBanco != null)
            {
                produtoNoBanco.Nome = Produto.Nome;
                produtoNoBanco.Preco = Produto.Preco;
                produtoNoBanco.Quantidade = Produto.Quantidade;

                await db.SaveChangesAsync();
                var quantidadeAnterior = produtoNoBanco.Quantidade;
                produtoNoBanco.Quantidade = Produto.Quantidade;

                string tipoOperacao;

                if (Produto.Quantidade > 0)
                {
                    tipoOperacao = produtoNoBanco != null ? "Inclusão" : "Cadastro";
                }
                else if (Produto.Quantidade < 0)
                {
                    tipoOperacao = "Baixa";
                }
                else
                {
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
        }
        return RedirectToPage("/Lista");
    }
}
