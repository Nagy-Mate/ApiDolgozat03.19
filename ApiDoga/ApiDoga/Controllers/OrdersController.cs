using ApiDoga.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDoga.Controllers;

[Route("[controller]")]
[ApiController]
public class OrdersController(ApplicationDbContext db) : ControllerBase
{
    [HttpGet]
    public ActionResult GetOrders()
    {
        return Ok(db.Orders.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult GetOrder(int id)
    {
        var res = db.Orders.FirstOrDefault(x => x.Id == id);
        if (res == null)
            return NotFound(); 

        return Ok(res);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteOrder(int id)
    {
        var res = db.Orders.FirstOrDefault(x => x.Id == id);
        if (res == null)
            return NotFound();
        try
        {
            db.Orders.Remove(res);
            db.SaveChanges();
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
        
    }

    [HttpPost]
    public ActionResult CreateOrder(ProductDto product)
    {
        if(product == null)
            return BadRequest();

        try
        {
            db.Orders.Add(new Product
            {
                ProductName = product.ProductName,
                Created = DateTime.Now,
                Price = product.Price,
                ProductType = product.ProductType,
                Quantity = product.Quantity,
            });
            db.SaveChanges();
            return Created() ;
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public ActionResult UpdateOrder (UpdateProductDto product)
    {
        var order = db.Orders.FirstOrDefault(o => o.Id == product.Id);
        if(order == null)
            return NotFound();

        if (product == null)
            return BadRequest();

        try
        {
            db.Orders.Where(o => o.Id == product.Id)
                .ExecuteUpdate(o => o
                    .SetProperty(p => p.ProductName, product.ProductName)
                    .SetProperty(p => p.ProductType, product.ProductType)
                    .SetProperty(p => p.Quantity, product.Quantity)
                    .SetProperty(p => p.Price, product.Price)
                );
            
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet("MostSold")]
    public ActionResult GetMostSoldByDay()
    {
        var res = db.Orders.GroupBy(o => o.Created.Date).Select(x => new
        {
            Day = x.Key,
            Value = x.Sum(o => o.Price * o.Quantity)

        }).OrderByDescending(d => d.Value).First();
        return Ok(res);
    }


}
