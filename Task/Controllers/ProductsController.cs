using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Task.Models;

namespace Task.Controllers
{
    [Route("/api/[controller]")]
    public class ProductsController : Controller
    {
        private static List<ProductModel> products = new List<ProductModel>();

        [HttpGet]
        public IEnumerable<ProductModel> Get() => products;

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            var product = products.SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            products.Remove(products.SingleOrDefault(p => p.Id == id));
            return Ok();
        }

        private int NextProductId => products.Count() == 0 ? 1 : products.Max(x => x.Id) + 1;

        [HttpGet("GetNextProductId")]
        public int GetNextProductId()
        {
            return NextProductId;
        }

        [HttpPost]
        public IActionResult Post(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Id = NextProductId;
            products.Add(model);
            return CreatedAtAction(nameof(Get), new {id = model.Id}, model);
        }

        [HttpPost("AddProduct")]
        public IActionResult PostBody([FromBody] ProductModel product) => Post(product);

        [HttpPut]
        public IActionResult Put(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storedProduct = products.SingleOrDefault(p => p.Id == model.Id);

            if (storedProduct == null)
                return NotFound();

            storedProduct.Name = model.Name;
            storedProduct.Description = model.Description;

            return Ok(storedProduct);
        }
    }
}
