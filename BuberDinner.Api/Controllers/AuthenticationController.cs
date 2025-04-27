using Microsoft.AspNetCore.Mvc;

using BuberDinner.Contracts.Authentication;
using BuberDinner.Application.Services.Authentication;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]

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
        AuthenticationResult result;
        try {
            result = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
        var response = new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.Token);
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        AuthenticationResult result;
        try {
            result = _authenticationService.Login(request.Email, request.Password);
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
        var response = new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.Token);
        return Ok(response);
    }
}