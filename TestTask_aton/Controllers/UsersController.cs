using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask_aton.Core.Abstractions;
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

        [HttpGet("getallusers")]
        [Authorize(Roles = "Admin")]
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

        [HttpGet("getuserbylogin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<GetByLoginResponse>>> GetUserByLogin(string login)
        {
            var user = await _usersService.GetUserByLogin(login);

            var isActive = "Неактивен";

            if (user.RevokedAt == null) isActive = "Активен";

            var response = new GetByLoginResponse(
                user.Name,
                user.Gender,
                user.BirthDay,
                isActive);

            return Ok(response);
        }

        [HttpGet("getyourself")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<List<UsersResponse>>> GetYourself(string login, string password)
        {
            var user = await _usersService.GetYourself(login, password);

            if (User.FindFirst("userId").Value != user.Id.ToString()) return BadRequest("Вы не имеете право доступа к этой информации");
            if (user.RevokedAt != null) return BadRequest("Пользователь не может быть активен на данный момент");

            var response = new UsersResponse(
                user.Id,
                user.Login,
                user.Password,
                user.Name,
                user.Gender,
                user.BirthDay,
                user.IsAdmin,
                user.CreatedAt,
                user.CreatedBy,
                user.ModifiedAt,
                user.ModifiedBy,
                user.RevokedAt,
                user.RevokeddBy);

            return Ok(response);
        }

        [HttpGet("getallusersolderthan")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UsersResponse>>> GetAllUsersOlderThan(int age)
        {
            var users = await _usersService.GetAllUsersOlderThan(age);

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

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] UsersRequestCreate request)
        {
            var creator = User.FindFirst("userLogin").Value;

            var (user, error) = Core.Models.User.Create(
                Guid.NewGuid(),
                request.Login,
                request.Password,
                request.Name,
                request.Gender,
                request.BirthDay,
                request.IsAdmin,
                DateTime.UtcNow,
                creator,
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] UsersRequestLogin request)
        {
            var userToken = await _usersService.Login(request.Login, request.Password); // в настоящем приложении пароль не будет храниться явно,
                                                                                        // пароль будет шифроваться и дешифроваться по одной из схем

            HttpContext.Response.Cookies.Append("JWTtoken", userToken); // в настоящем приложении не будет называться так открыто

            return Ok();
        }

        [HttpPut("updateuser")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid id, [FromBody] UsersRequestUpdate request)
        {
            var userId = await _usersService.UpdateUser(
                id, 
                request.Name, 
                request.Gender, 
                request.BirthDay,
                DateTime.UtcNow,
                User.FindFirst("userLogin").Value);

            return Ok(userId);
        }

        [HttpPut("updateuseryourself")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Guid>> UpdateUserYourself([FromBody] UsersRequestUpdate request)
        {
            var userId = await _usersService.UpdateUser(
                Guid.Parse(User.FindFirst("userId").Value),
                request.Name,
                request.Gender,
                request.BirthDay,
                DateTime.UtcNow,
                User.FindFirst("userLogin").Value);

            return Ok(userId);
        }

        [HttpPut("updateuserspassword")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> UpdateUserPassword(Guid id, [FromBody] UsersRequestUpdatePassword request)
        {
            var userId = await _usersService.UpdateUsersPassword(
                id,
                request.Password,
                DateTime.UtcNow,
                User.FindFirst("userLogin").Value);

            return Ok(userId);
        }

        [HttpPut("updateyourselfsuserspassword")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Guid>> UpdateYorselfsUserPassword([FromBody] UsersRequestUpdatePassword request)
        {
            var userId = await _usersService.UpdateUsersPassword(
                Guid.Parse(User.FindFirst("userLogin").Value),
                request.Password,
                DateTime.UtcNow,
                User.FindFirst("userLogin").Value);

            return Ok(userId);
        }

        [HttpPut("updateuserslogin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> UpdateUserLogin(Guid id, [FromBody] UsersRequestUpdateLogin request)
        {
            var userId = await _usersService.UpdateUsersLogin(
                id,
                request.Login,
                DateTime.UtcNow,
                User.FindFirst("userLogin").Value);

            return Ok(userId);
        }

        [HttpPut("updateyourselfsuserslogin")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Guid>> UpdateYorselfsUsersLogin([FromBody] UsersRequestUpdateLogin request)
        {
            var userId = await _usersService.UpdateUsersLogin(
                Guid.Parse(User.FindFirst("userLogin").Value),
                request.Login,
                DateTime.UtcNow,
                User.FindFirst("userLogin").Value);

            return Ok(userId);
        }

        [HttpPut("update2")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> Update2(Guid id)
        {
            var userId = await _usersService.Update2(
                id
                );

            return Ok(userId);
        }

        [HttpDelete("softdelete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> SoftDelete(Guid id)
        {
            var userId = await _usersService.SoftDelete(
                id,
                DateTime.UtcNow,
                User.FindFirst("userLogin").Value);

            return Ok(userId);
        }

        [HttpDelete("harddelete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> HardDelete(Guid id)
        {
            return Ok(await _usersService.HardDelete(id));
        }
    }
}
