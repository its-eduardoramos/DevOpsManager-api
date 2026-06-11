using api.Dtos;
using api.Models;

namespace api.Interfaces
{
  public interface IResourceRepository
  {
    public Task<List<Resource>> GetResourcesAsync(string userId);
    public Task<Resource?> GetAsyncById(int id);
    public Task<Resource> CreateAsync(Resource resourceModel, AuditLog auditLog);
    public Task<Resource?> UpdateAsync(int id, UpdateResourceRequest resourceDto, AuditLog audit);
    public Task<Resource?> DeleteAsync(Resource resource, AuditLog auditLog);
  }
}