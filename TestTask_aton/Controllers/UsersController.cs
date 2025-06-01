using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using TestTask_aton.Application.Services;
using TestTask_aton.Contracts;
using TestTask_aton.Core.Models;

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

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] UsersRequestCreate request)
        {
            var (user, error) = Core.Models.User.Create(
                Guid.NewGuid(),
                request.Login,
                request.Password,
                request.Name,
                request.Gender,
                request.BirthDay,
                request.IsAdmin,
                DateTime.UtcNow,
                "SLOWY",
                null,
                "",
                null,
                "");

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var userId = await _usersService.CreateUser(user);

            return Ok(userId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> Update1(Guid id, [FromBody] UsersRequestUpdate request)
        {
            var userId = await _usersService.UpdateUser(
                id, 
                request.Login, 
                request.Password, 
                request.Name, 
                request.Gender, 
                request.BirthDay,
                DateTime.UtcNow,
                "SLOWY");

            return Ok(userId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> HardDelete(Guid id)
        {
            return Ok(await _usersService.HardDelete(id));
        }
    }
}
