using api.Helpers;
using api.Models;

namespace api.Inetraces
{
  public interface IAuditLogRepository
  {
    public Task<List<AuditLog>> GetAllAsync(AuditLogQueryObject auditQuery);
    public Task<AuditLog> CreateAsync(AuditLog audit);
  }
}