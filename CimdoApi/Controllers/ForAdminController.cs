using Cimdo2._0.Context;
using Cimdo2._0.InnerClass;
using Cimdo2._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CimdoApi.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "UserIdPolicy")]
public class ForAdminController : ControllerBase
{
    private readonly SbcpksmyContext _context; //подключение к бд

    public ForAdminController(SbcpksmyContext context) //конструктор контроллера
    {
        _context = context;
    }
    
    //добавление нового автоора
    [HttpPost("addauthor")]
    public IActionResult AddAuthor(Author author)
    {
        Author authorModel = new Author(); //экземпляр класса автора

        authorModel.Author1 = author.Author1;

        _context.Authors.Add(authorModel); //добавдение нового автора
        _context.SaveChanges(); //сохранение

        return Ok("Author add.");
    }
    
    //добавление новой книги
    [HttpPost("addbook")]
    public IActionResult AddBook(Book book)
    {
        Book booknModel = new Book(); //экземпляр класса книги

        booknModel.Title = book.Title;
        booknModel.Author = book.Author;
        booknModel.Description = book.Description;

        _context.Books.Add(booknModel); //добавление новой книги
        _context.SaveChanges(); //сохранение 

        return Ok("Book add.");
    }
    //добавление жанров к книге
    [HttpPost("addgenerstobooks")]
    public IActionResult AddGenersToBooks(BooksGener booksgeners)
    {
        BooksGener modelbooksgeners = new BooksGener(); //экземпляр класса жанр-книга

        modelbooksgeners.IdBook = booksgeners.IdBook;
        modelbooksgeners.IdGener = booksgeners.IdGener;
        
        _context.BooksGeners.Add(modelbooksgeners); //добавление жанра к книге
        _context.SaveChanges(); //сохранение

        return Ok("Genre added to book.");
    }
    //создание списка пользователей
    private List<User> GetUsers() //подключение к базе данных
    {
        using (var context = new SbcpksmyContext())
        {
            return context.Users.ToList();
        }
    }
    //вывод всех пользователей
    [HttpGet("getusers")]
    public ActionResult GetDataApi()
    {
        var usersData = GetUsers(); //вывод данных в api
        return Ok(usersData);
    }
    private static IEnumerable<Author> GetAuthors() //подключение к базе данных
    {
        using (var context = new SbcpksmyContext())
        {
            return context.Authors
                .ToList();
        }
    }
    //запрос на вывод всех авторов
    [HttpGet("getauthors")]
    [Authorize]
    public ActionResult GetDataAuthors()
    {
        var authorsData = GetAuthors(); //вывод данных в api
        return Ok(authorsData);
    }
    
    //удаление пользователя
    [HttpDelete("deliteuser")]
    public async Task<IActionResult> DeleteUser(int id_user)
    {
        var user = await _context.Users.FindAsync(id_user); //находим пользователя по id

        if (user == null)
        {
            return NotFound("User not found"); // Если пользователь с указанным id не найден, возвращаем 404 Not Found
        }

        _context.Users.Remove(user); //удаляем пользователя
        await _context.SaveChangesAsync(); //сохраняем

        return Ok("User delited"); 
    }
}