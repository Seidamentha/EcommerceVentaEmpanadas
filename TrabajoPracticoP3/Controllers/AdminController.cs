using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Security.Claims;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.Data.Models;
using TrabajoPracticoP3.Services.Implementations;
using TrabajoPracticoP3.Services.Interfaces;

namespace TrabajoPracticoP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminService;

        public AdminController(IAdminServices adminService)
        {
            _adminService = adminService;
        }


        [HttpGet("GetProductId")]
        public IActionResult GetProductById(int ProductId) 
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                Product ProdId = _adminService.GetProductById(ProductId);
                return Ok(ProdId);

            }
            return Forbid();
        }


        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProduct()
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                var products = _adminService.GetAllProduct();
                if (products != null)
                {
                    return Ok(products);
                }
                return NotFound("No hay nada que mostrar");
            }
            return Forbid();
        } 


        [HttpPost("NewClient")] 
        public IActionResult AddProduct([FromBody] ProductPostDto dto)  
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")

            {
                var product = new Product()
                {
                    Name = dto.Name,
                    Price = dto.Price,

                };
                int id = _adminService.AddProduct(product);

                return Ok(id);
            }
            return Forbid();

        }


        [HttpPut("UpdateProduct")]
        public IActionResult EditProduct(int productId, [FromBody] ProductUpdateDto updateProduct)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == "Admin")
            {
                Product existingProduct = _adminService.GetProductById(productId);

                if (existingProduct != null)
                {
                    existingProduct.Name = updateProduct.Name;
                    existingProduct.Price = updateProduct.Price;

                    _adminService.EditProduct(existingProduct);
                    return Ok();
                }
                else
                {
                    return NotFound("Producto no encontrado");
                }
            }
            return Forbid();
        }


        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int productId)   
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                Product productToDelete = _adminService.GetProductById(productId);

                if (productToDelete != null)
                {
                    _adminService.DeleteProduct(productToDelete);
                }
                else
                {
                    return NotFound("producto no encontrado");
                }
            }
            return NoContent();
        }


        [HttpPut("HighLogic")]
        public IActionResult HighLogicProduct(int productId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                Product logicPutProduct = _adminService.GetProductById(productId);
                logicPutProduct.State = true;
                _adminService.HighLogicProduct(logicPutProduct);
            }
            return NoContent();
        }


        [HttpDelete("LowLogic")]
        public IActionResult DeleteLogicProduct(int productId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                Product logicDeleteProduct = _adminService.GetProductById(productId);
                logicDeleteProduct.State = false;
                _adminService.DeleteLogicProduct(logicDeleteProduct);
            }
            return NoContent();
        }
    }
}

