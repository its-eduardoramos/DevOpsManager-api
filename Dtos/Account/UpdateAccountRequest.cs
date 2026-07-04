using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
  public class UpdateAccountRequest
  {
    [Required]
    public string UserName { get; set; } = string.Empty;
  }
}