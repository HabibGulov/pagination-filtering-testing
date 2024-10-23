using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/users")]
public sealed class UserController(IUserRepository userRepository) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllUsers([FromQuery] UserFilter filter)
    {
        var users = userRepository.GetAllUsers(filter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<UserReadDTO>>>.Success(null!, users));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetUserById(int id)
    {
        var user = userRepository.GetUserById(id);
        return user != null
            ? Ok(ApiResponse<UserReadDTO?>.Success(null!, user))
            : NotFound(ApiResponse<UserReadDTO?>.Fail(null!, null));
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreateDTO userCreateDTO)
    {
        bool result = userRepository.CreateUser(userCreateDTO);
        if(result==true) return Ok(ApiResponse<bool>.Success(null!, result));
        return BadRequest(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpPut]
    public IActionResult UpdateUser([FromBody] UserUpdateDTO userUpdateDTO)
    {
        var result = userRepository.UpdateUser(userUpdateDTO);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : NotFound(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteUser(int id)
    {
        var result = userRepository.DeleteUser(id);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : NotFound(ApiResponse<bool>.Fail(null!, result));
    }
}
