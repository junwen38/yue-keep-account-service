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
    public class CategoryController : ControllerBase {
        public CategoryController(AccountBookDb db) {
            Db = db;
        }

        public AccountBookDb Db { get; }

        [HttpGet]
        public IEnumerable<dynamic> GetCategories([FromQuery]int? parentId) {
            var categories = (from x in Db.Categories where x.ParentId == parentId select x).Include(x=>x.Children).ToList();
            return categories.Select((v, i) => {
                return new {
                    v.Id,
                    v.Name,
                    v.Icon,
                    v.Type,
                    v.ParentId,
                    haveChildren = v.Children.Count() > 0
                };
            });
        }

        [HttpGet("{id}")]
        public object GetCategory(int id) {
            var category = (from x in Db.Categories where x.Id == id select x).Include(r=>r.Book).AsNoTracking().FirstOrDefault();
            if (category == null) {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody]Category category) {
            category.Id = 0;
            Db.Categories.Add(category);
            Db.SaveChanges();
            return CreatedAtAction("GetCategory", new {
                id = category.Id
            }, category);
        }

        [HttpPut("{id}")]
        public IActionResult EditCategory([FromRoute]int id, [FromBody]Category category) {
            if (!(from x in Db.Categories where x.Id == id select x).Any()){
                return NotFound();
            }
            category.Id = id;
            Db.Attach<Category>(category);
            Db.Entry<Category>(category).State = EntityState.Modified;
            Db.SaveChanges();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCatgory([FromRoute]int id) {
            var category = Db.Categories.AsNoTracking().FirstOrDefault(r=>r.Id == id);
            if (category == null) {
                return NotFound();
            }
            Db.Categories.Remove(category);
            Db.SaveChanges();
            return NoContent();
        }
    }
}