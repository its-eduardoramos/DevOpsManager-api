using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
  public class UpdateAuditLogRequest
  {
    [Required]
    public string Action { get; set; } = string.Empty;
    [Required]
    public string ResourceName { get; set; } = string.Empty;
  }
}