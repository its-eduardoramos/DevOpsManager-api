using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
  public class Resource
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "Virtual Machine";
    public string IpAddress { get; set; } = "0.0.0.0";
    public string Status { get; set; } = "Active";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; } = string.Empty;
    [ForeignKey("UserId")]
    public virtual AppUser User { get; set; } = null!;
  }
}