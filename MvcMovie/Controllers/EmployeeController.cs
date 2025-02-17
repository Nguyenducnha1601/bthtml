using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace MvcMovie.Controllers
{
    public class Employee : Controller
    { 
        public IActionResult Index()
        {
            return View();
        } 
    }    
}
