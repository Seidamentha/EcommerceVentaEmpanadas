using System;
using System.Linq;
using System.Security.Claims;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.Data.Models;
using TrabajoPracticoP3.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrabajoPracticoP3.Controllers
{
    [Route("api/client")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        private readonly IUserServices _userServices;
        private readonly IOrderServices _orderServices;
        private readonly IClientServices _clientServices;
        private readonly IProductServices _productServices;

        public ClientController(IUserServices userServices, IAdminServices adminServices, IOrderServices orderServices, IClientServices clientServices, IProductServices productServices)
        {
            _adminServices = adminServices;
            _userServices = userServices;
            _orderServices = orderServices;
            _clientServices = clientServices;
            _productServices = productServices;
        }

        [HttpGet("GetClients")]
        public IActionResult GetClients()
        {
            try
            {
                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                if (role == "Admin")
                {
                    var res = _adminServices.GetClients();
                    if (res == null)
                    {
                        return BadRequest(res);
                    }
                    return Ok(res);
                }
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpPost("CreateClient")]
        public IActionResult CreateClient([FromBody] AdminPostDto adminDto)
        {
            try
            {
                var client = new Client()
                {
                    Email = adminDto.Email,
                    Name = adminDto.Name,
                    Password = adminDto.Password,
                };

                int id = _userServices.CreateUser(client);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                if (userId == 0)
                {
                    return NotFound("Usuario no encontrado");
                }
                _userServices.DeleteUser(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpPut("UpdateClient/{ClientId}")]
        public IActionResult UpdateClient([FromRoute] int ClientId, [FromBody] UserPutDto userDto)
        {
            try
            {
                var clientToUpdate = new Client()
                {
                    Id = ClientId,
                    Email = userDto.Email,
                    Name = userDto.Name,
                    Password = userDto.Password,
                };

                int updatedClientId = _userServices.UpdateUser(clientToUpdate);

                if (updatedClientId == 0)
                {
                    return NotFound($"Usuario con ID {ClientId} no encontrado");
                }

                return Ok(updatedClientId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }
    }
}
