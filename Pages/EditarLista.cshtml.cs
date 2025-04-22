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

                var log = new Log
                {
                    NomeProduto = Produto.Nome,
                    TipoOperacao = "Atualização",
                    Quantidade = Produto.Quantidade,
                    DataOperacao = DateTime.Now
                };
                db.Logs.Add(log);
            }
        }
        return RedirectToPage("/Lista");
    }
}
