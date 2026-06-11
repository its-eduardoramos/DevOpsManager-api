namespace api.Models
{
  public class AuditLog
  {
    public int Id { get; set; }
    public string Action { get; set; } = string.Empty;
    public string ResourceName { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string UserEmail { get; set; } = string.Empty;
  }
}