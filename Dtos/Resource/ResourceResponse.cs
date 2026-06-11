namespace api.Dtos
{
  public class ResourceResponse
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int CpuPercentage { get; set; }
    public int RamPercentage { get; set; }
    public int DiskPercentage { get; set; }
  }
}