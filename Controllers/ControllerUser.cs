using Microsoft.AspNetCore.Mvc;
using MinimalCrud.Models;

namespace MinimalCrud.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // Список пользователей
        private static List<User> Users = new List<User>();

        [HttpGet]
        
    }
}