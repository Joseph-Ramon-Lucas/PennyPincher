using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;
using PennyPincher.Models.DtoModels;
using PennyPincher.Repositories;

namespace PennyPincher.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IValidationRepository _validationRepository;
        private readonly IUserService _userService;


        public UserController(IValidationRepository validationRepository, IUserService userService)
        {
            _validationRepository = validationRepository;
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>>GetAllUsers()
        {
            IEnumerable<User> allUsers = await _userService.GetAllUsersAsync();
            List<UserDto> allUserDtos = new List<UserDto>();
            if (allUsers != null && allUsers.Count() > 0) {
                foreach (var item in allUsers)
                {
                    allUserDtos.Add(new UserDto()
                    {
                        Name = item.Name,
                        Email = item.Email,
                    });
                }
                return Ok(allUserDtos);
            }
            return NotFound();
        }
    }
}
