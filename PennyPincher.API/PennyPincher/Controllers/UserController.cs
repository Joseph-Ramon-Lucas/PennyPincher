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
            if (allUsers == null || allUsers.Count() <= 0) {
                return NotFound();
            }

            foreach (var item in allUsers)
            {
                UserDto userDto = _userService.UserToDto(item);
                allUserDtos.Add(userDto);
            }
            return Ok(allUserDtos);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>>GertUserById(int userId)
        {
            User foundUser = await _userService.GetUserByIdAsync(userId);
            if (foundUser == null) { return BadRequest($"Cannot find user id {userId}"); }

            UserDto userDto = _userService.UserToDto(foundUser);

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateUserAsync(UserForCreationDto newUser)
        {
            if (newUser == null) { return BadRequest("User data missing"); }

            //Users should not share the same email
            ValidationResponseDto validationResponseDto = await _validationRepository.checkUserExistsByEmail(newUser.Email);
            if (validationResponseDto.IsSuccess) { return BadRequest(validationResponseDto.ResponseMessage); }

            var newUserId = await _userService.CreatUserAsync(newUser);
            if (newUserId == null) { return BadRequest(); }

            return Ok(newUserId);
        }

        [HttpDelete("delete_user/{userId}")]
        public async Task<ActionResult>DeleteUser(int userId)
        {
            User foundUser = await _userService.GetUserByIdAsync(userId);
            if (foundUser == null) { return BadRequest($"Cannot find user id {userId}"); }

            bool userDeletedSuccessfully = await _userService.DeleteUserByIdAsync(userId);

            return Ok(userDeletedSuccessfully);
        }
    }
}
