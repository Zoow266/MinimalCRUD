using Microsoft.AspNetCore.Mvc;
using MinimalCrud.Models;

namespace MinimalCrud.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // Список пользователей
        private static List<User> users = new List<User>();

        // Получить всех пользователей
        [HttpGet]
        public IActionResult GetUsers() => Ok(users);

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var searchUser = users.FirstOrDefault(x => x.Id == id);
            if(searchUser is null) return NotFound();
            return Ok(searchUser);
        }
    }
}