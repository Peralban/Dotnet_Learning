using Microsoft.AspNetCore.Mvc;

using BuberDinner.Contracts.Authentication;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Api.Filters;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
[ErrorHandlingFilter]

public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        AuthenticationResult result = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        var response = new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.Token);
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        AuthenticationResult result = _authenticationService.Login(request.Email, request.Password);
        var response = new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.Token);
        return Ok(response);
    }
}