using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Mercadinho.models;
using Microsoft.AspNetCore.Mvc;

public class ListaModel : PageModel
{
    public List<Produto> Produtos { get; set; }

    public void OnGet()
    {
        using (var db = new AppDbContext())
        {
            Produtos = db.Produtos.ToList();
        }
    }

    public async Task<IActionResult> OnPostExcluirAsync(int id)
    {
        using (var db = new AppDbContext())
        {
            var produto = await db.Produtos.FindAsync(id);
            if (produto != null)
            {
                db.Produtos.Remove(produto);
                await db.SaveChangesAsync();
            }
        }

        return RedirectToPage(); 
    }
}