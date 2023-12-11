using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.Data.Models;
using TrabajoPracticoP3.Services.Interfaces;

namespace TrabajoPracticoP3.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllProductos()
        {
            try
            {
                var products = _productServices.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpGet("{idProducto}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProduct([FromRoute] int idProducto)
        {
            try
            {
                var product = _productServices.GetProductById(idProducto);
                if (product == null)
                {
                    return NotFound($"El producto con el id {idProducto} no se ha encontrado");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpPost("CreateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateProducts([FromBody] ProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
                if (role != "Admin")
                {
                    return Forbid();
                }

                var newProduct = new Product
                {
                    NameProduct = productDto.NameProduct,
                    Price = productDto.Price,
                };

                int id = _productServices.CreateProduct(newProduct);
                return Ok(new { ProductId = id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpPut("UpdateProduct/{idProduct}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateProduct([FromRoute] int idProduct, [FromBody] ProductDto product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
                if (role != "Admin")
                {
                    return Forbid();
                }

                var productToUpdate = new Product
                {
                    IdProduct = idProduct,
                    NameProduct = product.NameProduct,
                    Price = product.Price,
                };

                int updatedProductId = _productServices.UpdateProduct(productToUpdate);

                if (updatedProductId == 0)
                {
                    return NotFound($"Producto con ID {idProduct} no encontrado");
                }

                return Ok($"Producto actualizado exitosamente, ID del producto: {updatedProductId}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpDelete("DeleteProduct/{idProduct}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProduct([FromRoute] int idProduct)
        {
            try
            {
                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
                if (role != "Admin")
                {
                    return Forbid();
                }

                var product = _productServices.GetProductById(idProduct);
                if (product == null)
                {
                    return NotFound($"El producto con el ID: {idProduct} no fue encontrado");
                }

                _productServices.DeleteProduct(idProduct);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor: " + ex.Message);
            }
        }
    }

}