using Microsoft.AspNetCore.Mvc;
using Blog.Models; 
using System.Linq;
using Blog.Data;

using Microsoft.EntityFrameworkCore;

public class BlogController : Controller
{
    private readonly BlogContext _context;

    public BlogController(BlogContext context)
    {
        _context = context; 
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
    [HttpPost]
    public IActionResult AddComment(int id, string author, string content)
    {
        var blogPost = _context.BlogPosts.FirstOrDefault(p => p.Id == id);
        if (blogPost == null)
        {
            return NotFound();
        }

        var comment = new Comment
        {
            BlogPostId = id,
            Author = author,
            Content = content,
            CreatedAt = DateTime.Now
        };

        _context.Comments.Add(comment);
        _context.SaveChanges();

        return RedirectToAction("Details", new { id = id });
    }
    public IActionResult Details(int id)
    {
        var blogPost = _context.BlogPosts
            .Include(p => p.Comments)
            .FirstOrDefault(p => p.Id == id);

        if (blogPost == null)
        {
            return NotFound();
        }

        return View(blogPost);
    }


}
