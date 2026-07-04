using System.Security.Claims;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
  [Route("api/account")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenService)
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CreateLoginRequest loginDto)
    {
      var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName.Equals(loginDto.UserName.ToLower()));
      if (existingUser is null) return Unauthorized("User/password invalid");

      var result = await _signInManager.CheckPasswordSignInAsync(existingUser, loginDto.Password, false);
      if (!result.Succeeded) return Unauthorized("User/password invalid");

      var roles = await _userManager.GetRolesAsync(existingUser);
      var token = _tokenService.CreateToken(existingUser, roles.FirstOrDefault() ?? "user");
      return Ok(existingUser.ToResponse(token));
    }

    [Authorize]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateAccountRequest accountDto)
    {
      try
      {
        if (!User.IsInRole("Admin")) return Forbid();
        var appUser = accountDto.ToEntity();
        var createdUser = await _userManager.CreateAsync(appUser, accountDto.Password);
        if (createdUser.Succeeded)
        {
          var result = await _userManager.AddToRoleAsync(appUser, "User");

          if (result.Succeeded)
          {
            var token = _tokenService.CreateToken(appUser, "user");
            return Ok(appUser.ToResponse(token));
          }
          else
          {
            return StatusCode(500, result.Errors);
          }
        }
        else
        {
          return StatusCode(500, createdUser.Errors);
        }
      }
      catch (Exception e)
      {
        return StatusCode(500, e);
      }
    }

    [Authorize]
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
      if (!User.IsInRole("Admin")) return Forbid();

      var users = await _userManager.Users.ToListAsync();
      var userList = new List<UserListResponse>();

      foreach(var user in users)
      {
        var roles = await _userManager.GetRolesAsync(user);
        userList.Add(user.ToListResponse(roles.FirstOrDefault() ?? "userrr"));

      }

      return Ok(userList);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateAccountRequest updateDto)
    {
      if (!User.IsInRole("Admin")) return Forbid();
      var existingUser = await _userManager.FindByIdAsync(id);
      if (existingUser is null) return NotFound();

      existingUser.UserName = updateDto.UserName;

      var updatedUser = await _userManager.UpdateAsync(existingUser);
      var roles = await _userManager.GetRolesAsync(existingUser);
      return Ok(existingUser.ToListResponse(roles.FirstOrDefault() ?? "userrr"));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
      if (!User.IsInRole("Admin")) return Forbid();
      var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
      if(existingUser is null) NotFound();
      await _userManager.DeleteAsync(existingUser);
      return NoContent();
    }
  }
}