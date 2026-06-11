using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
  public class UpdateResourceRequest
  {
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Type { get; set; } = "Virtual Machine";
    [Required]
    public string IpAddress { get; set; } = "0.0.0.0";
    [Required]
    public string Status { get; set; } = "Active";
  }
}