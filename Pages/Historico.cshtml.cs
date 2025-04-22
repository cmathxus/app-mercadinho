using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class HistoricoModel : PageModel
    {
    public List<Log> Logs { get; set; }

    public void OnGet()
    {
        using (var db = new AppDbContext())
        {
            Logs = db.Logs.ToList();
        }
    }
    }
}
