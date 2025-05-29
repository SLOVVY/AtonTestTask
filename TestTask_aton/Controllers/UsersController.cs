using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Xml.Linq;
using TestTask_aton.Application.Services;
using TestTask_aton.Contracts;

namespace TestTask_aton.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<UsersResponse>>> GetUsers()
        {
            var users = await _usersService.GetAllUsers();

            var response = users.Select(u => new UsersResponse(
                u.Id,
                u.Login,
                u.Password,
                u.Name,
                u.Gender,
                u.BirthDay,
                u.IsAdmin,
                u.CreatedAt,
                u.CreatedBy,
                u.ModifiedAt,
                u.ModifiedBy,
                u.RevokedAt,
                u.RevokeddBy));

            return Ok(response);
        }
    }
}
