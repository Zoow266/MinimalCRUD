using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalCrud.DATA;
using MinimalCrud.DTO;
using MinimalCrud.Models;
using MinimalCrud.Services;

namespace MinimalCrud.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service) => _userService = service;

        // Получить всех пользователей
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersList = await _userService.GetAllAsync();
            return Ok(usersList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var searchUser = await _userService.GetByIdAsync(id);
            if(searchUser is null) return NotFound();
            return Ok(searchUser);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO userDTO)
        {
            if(userDTO is null) return BadRequest();

            var user = await _userService.CreateAsync(userDTO);

            return CreatedAtAction(nameof(GetUserById), new {id = user.Id}, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDTO updateUser)
        {
            if (updateUser is null) return BadRequest();

            var searchUpdateUser = await _userService.UpdateAsync(id, updateUser);
            
            if(searchUpdateUser is null) return NotFound();

            return Ok(searchUpdateUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleteUser = await _userService.DeleteAsync(id);

            if(!deleteUser) return NotFound();

            return NoContent();
        }
    }
}