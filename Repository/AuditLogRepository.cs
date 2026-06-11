using api.Data;
using api.Helpers;
using api.Inetraces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
  public class AuditLogRepository : IAuditLogRepository
  {
    private readonly ApplicationDbContext _context;
    public AuditLogRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<AuditLog>> GetAllAsync(AuditLogQueryObject query)
    {
      var auditLogs = _context.AuditLogs.AsQueryable();
      var skipNumber = (query.PageNumber - 1) * query.PageSize;
      return await auditLogs.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<AuditLog> CreateAsync(AuditLog audit)
    {
      await _context.AuditLogs.AddAsync(audit);
      await _context.SaveChangesAsync();
      return audit;
    }
  }
}