using anonim_chat.API.Entities;
using anonim_chat.API.Exceptions;
using anonim_chat.API.Managers;
using anonim_chat.API.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = anonim_chat.API.Models.Error;

namespace anonim_chat.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{


    private readonly IUserManager _userManager;

    public UserController(
        IUserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> GetUserAsync()
    {
        try
        {
            var user = await _userManager.GetUserAsync();
            return Ok(user);
        }
        catch (RecordNotFoundException e)
        {
            return BadRequest(new Error(e.Message));
        }
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> RegisterAsync(
        [FromBody] CreateUserModel createUserModel,
        [FromServices] IValidator<CreateUserModel> validator)
    {
        var validationResult = await validator.ValidateAsync(createUserModel);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var user = await _userManager.RegisterAsync(createUserModel);
            return Ok(user);
        }
        catch (Exception e) when (e is RecordNotFoundException or UsernameExistsException)
        {
            return BadRequest(new Error(e.Message));
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> LoginAsync([FromBody] LoginUserModel loginUserModel)
    {
        try
        {
            var tokenResult = await _userManager.LoginAsync(loginUserModel);
            return Ok(tokenResult);
        }
        catch (LoginValidationException e)
        {
            return BadRequest(new Error(e.Message));
        }
    }
} 