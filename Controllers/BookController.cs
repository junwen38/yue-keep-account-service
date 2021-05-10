using System.Collections.Generic;
using YueKeepAccountService.Db;
using YueKeepAccountService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace YueKeepAccountService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase {
        public BookController(AccountBookDb db) {
            Db = db;
        }

        public AccountBookDb Db { get; }

        [HttpGet]
        public IEnumerable<Book> GetBooks() {
            var books = (from x in Db.Books select x).ToList();
            return books;
        }

        [HttpGet("{id}")]
        public object GetBook(int id) {
            var book = (from x in Db.Books where x.Id == id select x).AsNoTracking().FirstOrDefault();
            if (book == null) {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody]Book book) {
            book.Id = 0;
            Db.Books.Add(book);
            Db.SaveChanges();
            return CreatedAtAction("GetBook", new {
                id = book.Id
            }, book);
        }

        [HttpPut("{id}")]
        public IActionResult EditBook([FromRoute]int id, [FromBody]Book book) {
            if (!(from x in Db.Books where x.Id == id select x).Any()){
                return NotFound();
            }
            book.Id = id;
            Db.Attach<Book>(book);
            Db.Entry<Book>(book).State = EntityState.Modified;
            Db.SaveChanges();
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook([FromRoute]int id) {
            var book = Db.Books.AsNoTracking().FirstOrDefault(r=>r.Id == id);
            if (book == null) {
                return NotFound();
            }
            Db.Books.Remove(book);
            Db.SaveChanges();
            return NoContent();
        }
    }
}