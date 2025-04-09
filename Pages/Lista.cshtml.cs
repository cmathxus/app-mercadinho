using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Mercadinho.models;

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
}