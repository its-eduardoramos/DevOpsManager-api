using api.Dtos;
using api.Models;

namespace api.Mappers
{
  public static class ResourceMappers
  {
    public static ResourceResponse ToResponse(this Resource resourceModel, Random rnd)
    {
      return new ResourceResponse
      {
        Id = resourceModel.Id,
        Name = resourceModel.Name,
        Type = resourceModel.Type,
        IpAddress = resourceModel.IpAddress,
        Status = resourceModel.Status,
        CreatedAt = resourceModel.CreatedAt,
        UserId = resourceModel.UserId,
        CpuPercentage = resourceModel.Status == "active" ? rnd.Next(5,40) : 0,
        RamPercentage = resourceModel.Status == "active" ? rnd.Next(20, 75) : 0,
        DiskPercentage = resourceModel.Status == "active" ? rnd.Next(30, 60) : 0
      };
    }
    public static Resource ToEntity(this CreateResourceRequest resourceDto, string userId)
    {
      return new Resource
      {
        Name = resourceDto.Name,
        Type = resourceDto.Type,
        IpAddress = resourceDto.IpAddress,
        Status = resourceDto.Status,
        UserId = userId
      };
    }
  }
}