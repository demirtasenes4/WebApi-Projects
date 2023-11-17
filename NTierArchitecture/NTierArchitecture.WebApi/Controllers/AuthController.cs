using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NTierArchitecture.WebApi.DTOs;
using NTierArchitecture.WebApi.Exceptions;
using NTierArchitecture.WebApi.Models;
using NTierArchitecture.WebApi.Services;
using System.ComponentModel.DataAnnotations;

namespace NTierArchitecture.WebApi.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly IValidator<RegisterDto> _registerDtoValidator;

    public AuthController(IValidator<RegisterDto> registerDtoValidator)
    {
        _registerDtoValidator = registerDtoValidator;
    }

    [HttpPost]
    public IActionResult Register(RegisterDto request)
    {
        var result = _registerDtoValidator.Validate(request);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors[0].ErrorMessage);
        }

        byte[] PasswordHash;
        byte[] PasswordSalt;

        PasswordService.CreatePassword(request.Password, out PasswordHash, out PasswordSalt);

        User user = new()
        {
            Email = request.Email,
            LastName = request.LastName,
            PasswordHash = PasswordHash,
            Name = request.Name,
            PasswordSalt = PasswordSalt
        };

        return Ok(new { Message = "User registration is successful!" });

    }
}