using Microsoft.AspNetCore.Mvc;
using DoestaWeb.Data;

public class TestController : Controller
{
    private readonly AppDbContext _context;

    public TestController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return Content("AppDbContext başarıyla inject edildi!");
    }
}
