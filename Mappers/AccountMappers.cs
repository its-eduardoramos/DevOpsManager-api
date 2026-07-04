using api.Dtos;
using api.Models;

namespace api.Mappers
{
  public static class AccountMappers
  {
    public static AccountResponse ToResponse(this AppUser appUser, string token)
    {
      return new AccountResponse
      {
        UserName = appUser.UserName,
        Email = appUser.Email,
        Token = token
      };
    }

    public static UserListResponse ToListResponse(this AppUser appUser, string roles)
    {
      return new UserListResponse
      {
        Id = appUser.Id,
        UserName = appUser.UserName,
        Email = appUser.Email,
        Roles = roles,
      };
    }

    public static AppUser ToEntity(this CreateAccountRequest accountDto)
    {
      return new AppUser
      {
        UserName = accountDto.UserName,
        Email = accountDto.Email
      };
    }
  }
}