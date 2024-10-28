using Microsoft.AspNetCore.Mvc;
using Blog.Models; // Zmień na odpowiednią przestrzeń nazw
using System.Linq;
using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

public class BlogController : Controller
{
    private readonly BlogContext _context;

    public BlogController(BlogContext context)
    {
        _context = context; // Przechowuj kontekst w polu
    }

    public IActionResult Index()
    {
        var posts = _context.BlogPosts.ToList();
        return View(posts);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(BlogPost post)
    {
        if (ModelState.IsValid)
        {
            post.CreatedAt = DateTime.Now;
            _context.BlogPosts.Add(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(post);
    }
}
