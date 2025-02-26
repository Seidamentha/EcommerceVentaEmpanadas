using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.Data.Models;
using TrabajoPracticoP3.Services.Implementations;
using TrabajoPracticoP3.Services.Interfaces;

namespace TrabajoPracticoP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly IClientServices _clientServices;
        private readonly IOrderServices _orderServices;
        private readonly ISellerServices _sellerServices;

        public SellerController(IProductServices productServices, IClientServices clientServices, IOrderServices orderServices, ISellerServices sellerServices)
        {
            _productServices = productServices;
            _clientServices = clientServices;
            _orderServices = orderServices;
            _sellerServices = sellerServices;
        }

        [HttpGet("GetSellers")]
        public IActionResult GetSellers()
        {
            try
            {
                var sellers = _sellerServices.GetSellers();
                return Ok(sellers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpGet("GetSellerById/{sellerId}")]
        public IActionResult GetSellerById(int sellerId)
        {
            try
            {
                var seller = _sellerServices.GetSellerById(sellerId);
                return seller != null ? Ok(seller) : NotFound($"Vendedor con ID {sellerId} no encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        // ✅ Agregar un producto
        [HttpPost("AgregarProducto")]
        public IActionResult AgregarProducto([FromBody] ProductDto productDto)
        {
            try
            {
                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role != "Seller") return Forbid();

                var producto = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Stock = productDto.Stock
                };

                int productId = _productServices.AddProduct(producto);
                return Ok(productId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // ✅ Obtener productos
        [HttpGet("ObtenerProductos")]
        public IActionResult ObtenerProductos()
        {
            try
            {
                var productos = _productServices.GetAllProducts();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // ✅ Modificar producto
        [HttpPut("ModificarProducto/{productId}")]
        public IActionResult ModificarProducto(int productId, [FromBody] ProductDto productDto)
        {
            try
            {
                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role != "Seller") return Forbid();

                var producto = new Product
                {
                    Id = productId,
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Stock = productDto.Stock
                };

                int actualizado = _productServices.UpdateProduct(producto);
                if (actualizado == 0) return NotFound($"Producto con ID {productId} no encontrado");

                return Ok("Producto actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // ✅ Eliminar producto
        [HttpDelete("EliminarProducto/{productId}")]
        public IActionResult EliminarProducto(int productId)
        {
            try
            {
                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role != "Seller") return Forbid();

                bool eliminado = _productServices.DeleteProduct(productId);
                if (!eliminado) return NotFound($"Producto con ID {productId} no encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // ✅ Agregar cliente
        [HttpPost("AgregarCliente")]
        public IActionResult AgregarCliente([FromBody] ClientPostDto clientDto)
        {
            try
            {
                var cliente = new Client
                {
                    Name = clientDto.Name,
                    Email = clientDto.Email
                };

                int clientId = _clientServices.AddClient(cliente);
                return Ok(clientId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // ✅ Modificar cliente
        [HttpPut("ModificarCliente/{clientId}")]
        public IActionResult ModificarCliente(int clientId, [FromBody] ClientPostDto clientDto)
        {
            try
            {
                var cliente = new Client
                {
                    Id = clientId,
                    Name = clientDto.Name,
                    Email = clientDto.Email
                };

                bool actualizado = _clientServices.UpdateClient(cliente);
                if (!actualizado) return NotFound($"Cliente con ID {clientId} no encontrado");

                return Ok("Cliente actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // ✅ Crear pedido
        [HttpPost("CrearPedido")]
        public IActionResult CrearPedido([FromBody] OrderPostDto orderDto)
        {
            try
            {
                var pedido = new Order
                {
                    ClientId = orderDto.ClientId,
                    ProductId = orderDto.ProductId,
                    Quantity = orderDto.Quantity,
                    TotalPrice = orderDto.TotalPrice
                };

                int orderId = _orderServices.AddOrder(pedido);
                return Ok(orderId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // ✅ Modificar pedido
        [HttpPut("ModificarPedido/{orderId}")]
        public IActionResult ModificarPedido(int orderId, [FromBody] OrderPostDto orderDto)
        {
            try
            {
                var pedido = new Order
                {
                    Id = orderId,
                    ClientId = orderDto.ClientId,
                    ProductId = orderDto.ProductId,
                    Quantity = orderDto.Quantity,
                    TotalPrice = orderDto.TotalPrice
                };

                bool actualizado = _orderServices.UpdateOrder(pedido);
                if (!actualizado) return NotFound($"Pedido con ID {orderId} no encontrado");

                return Ok("Pedido actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }
    }
}
