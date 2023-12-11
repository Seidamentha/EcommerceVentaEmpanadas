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
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        private readonly IUserServices _userServices;
        public AdminController(IAdminServices adminServices, IUserServices userServices)
        {
            _adminServices = adminServices;
            _userServices = userServices;
        }

        [HttpGet("GetAdmin")]
        public IActionResult GetAdmins()
        {

            try
            {
                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();

                var res = _adminServices.GetAdmins();
                if (role == "Admin")
                {

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
                return StatusCode(500, "Error interno del servidor" + ex.Message);
            }
        }


        [HttpPost("CreateAdmin")]
        public IActionResult CreateAdmin([FromBody] AdminPostDto adminDto)
        {
            try
            {
                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
                if (role == "Admin")
                {

                    var res = new Admin()
                    {
                        Email = adminDto.Email,
                        Name = adminDto.Name,
                        Password = adminDto.Password,
                    };
                    if (res == null)
                    {
                        return BadRequest(res);
                    }
                    int id = _userServices.CreateUser(res);
                    return Ok(id);
                }
                return Forbid();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor" + ex.Message);
            }

        }


        [HttpPut("UpdateAdmin/{AdminId}")]
        public IActionResult UpdateAdmin([FromRoute] int AdminId, [FromBody] UserPutDto userDto)
        {
            try
            {
                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
                if (role == "Admin")
                {
                    var userToUpdate = new Admin()
                    {
                        Id = AdminId,
                        Email = userDto.Email,
                        Name = userDto.Name,
                        Password = userDto.Password,
                    };

                    int updatedUserId = _userServices.UpdateUser(userToUpdate);

                    if (updatedUserId == 0)
                    {
                        return NotFound($"Usuario con ID {AdminId} no encontrado");
                    }

                    return Ok(updatedUserId);
                }
                return Forbid();
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
                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
                if (role == "Admin")
                {
                    if (userId == 0)
                    {
                        return NotFound();
                    }
                    _userServices.DeleteUser(userId);
                    return NoContent();
                }
                return Forbid();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor" + ex.Message);
            }
        }

    }
}