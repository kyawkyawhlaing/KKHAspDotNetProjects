using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KKH.AuthCookie.Controllers;

[Authorize]
public class UsersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
