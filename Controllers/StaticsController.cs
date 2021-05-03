using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using accountbook.Db;
using accountbook.Models;
using Microsoft.AspNetCore.Mvc;

namespace accountbook.Controllers {
  [ApiController]
  [Route("api/[controller]")]
  public class StaticsController : ControllerBase {
    public StaticsController(AccountBookDb db) {
      Db = db;
    }
    public AccountBookDb Db { get; }

    [HttpGet("[action]")]
    public object MonthlyIncome([FromQuery]DateTime? beginDate, [FromQuery]DateTime? endDate) {
      if (beginDate == null || endDate == null) {
        return BadRequest(new {
          Message = "必须输入开始日期和结束日期"
        });
      }
      Dictionary<string, object> data = new Dictionary<string, object>();
      var currentDate = new DateTime(beginDate.Value.Year, beginDate.Value.Month, 1);
      do
      {
        var income = Db.Items.Where(r=>r.Type == TransactionType.Income && r.Date >= currentDate && r.Date < currentDate.AddMonths(1)).Sum(r=>r.Cash);
        data.Add(currentDate.ToString("yyyyMM"), income);
        currentDate = currentDate.AddMonths(1);
      } while (currentDate <= endDate);
      return data;
    }

    [HttpGet("[action]")]
    public object MonthlyPayout([FromQuery]DateTime? beginDate, [FromQuery]DateTime? endDate) {
      if (beginDate == null || endDate == null) {
        return BadRequest(new {
          Message = "必须输入开始日期和结束日期"
        });
      }

      Dictionary<string, object> data = new Dictionary<string, object>();
      var currentDate = new DateTime(beginDate.Value.Year, beginDate.Value.Month, 1);
      do
      {
        var payout = Db.Items.Where(r=>r.Type == TransactionType.Payout && r.Date >= currentDate && r.Date < currentDate.AddMonths(1)).Sum(r=>r.Cash);
        data.Add(currentDate.ToString("yyyyMM"), payout);
        currentDate = currentDate.AddMonths(1);
      } while (currentDate <= endDate);
      return data;
    }
  }
}