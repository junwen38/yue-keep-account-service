using System.Collections.Generic;
using accountbook.Db;
using accountbook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;

namespace accountbook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        public ItemController(AccountBookDb db)
        {
            Db = db;
        }

        public AccountBookDb Db { get; }

        [HttpGet]
        public IEnumerable<Item> GetItems([FromQuery] int skip, [FromQuery] int take, [FromQuery] int? categoryId)
        {
            IQueryable<Item> query = Db.Items;
            if (categoryId != null)
            {
                var category = Db.Categories.Find(categoryId.Value);
                if (category != null)
                {
                    if (category.ParentId != null)
                    {
                        query = query.Where(r => r.Category2.Id == category.Id);
                    }
                    else
                    {
                        query = query.Where(r => r.Category1.Id == category.Id);
                    }
                }
                else
                {
                    return new List<Item>();
                }
            }
            return query.OrderByDescending(r=>r.Date).OrderByDescending(r=>r.Id).Include(r=>r.Category1).Include(r=>r.Category2).AsNoTracking().Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public object GetItem(int id)
        {
            var item = (from x in Db.Items where x.Id == id select x).Include(r=>r.Category1).Include(r=>r.Category2).AsNoTracking().FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] Item item)
        {
            item.Id = 0;
            Db.Items.Add(item);
            Db.SaveChanges();
            return CreatedAtAction("GetItem", new
            {
                id = item.Id
            }, item);
        }

        [HttpPut("{id}")]
        public IActionResult EditCategory([FromRoute] int id, [FromBody] Item item)
        {
            if (!(from x in Db.Items where x.Id == id select x).Any())
            {
                return NotFound();
            }
            item.Id = id;
            Db.Attach<Item>(item);
            Db.Entry<Item>(item).State = EntityState.Modified;
            Db.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCatgory([FromRoute] int id)
        {
            var item = Db.Items.AsNoTracking().FirstOrDefault(r => r.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            Db.Items.Remove(item);
            Db.SaveChanges();
            return NoContent();
        }
    }
}