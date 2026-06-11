using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
  public class ResourceRepository : IResourceRepository
  {
    private readonly ApplicationDbContext _context;
    public ResourceRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<Resource>> GetResourcesAsync(string userId)
    {
      return await _context.Resources.Where(r => r.UserId.Equals(userId)).ToListAsync();
    }

    public async Task<Resource?> GetAsyncById(int id)
    {
      return await _context.Resources.FindAsync(id);
    }

    public async Task<Resource> CreateAsync(Resource resource, AuditLog audit)
    {
      await _context.Resources.AddAsync(resource);
      await _context.AuditLogs.AddAsync(audit);
      await _context.SaveChangesAsync();
      return resource;
    }

    public async Task<Resource?> UpdateAsync(int id, UpdateResourceRequest resourceDto, AuditLog audit)
    {
      var existingResource = await _context.Resources.FindAsync(id);
      if(existingResource is null) return null;
      
      existingResource.Name = resourceDto.Name;
      existingResource.Type = resourceDto.Type;
      existingResource.IpAddress = resourceDto.IpAddress;
      existingResource.Status = resourceDto.Status;

      await _context.AuditLogs.AddAsync(audit);
      await _context.SaveChangesAsync();
      return existingResource;
    }

    public async Task<Resource?> DeleteAsync(Resource resource, AuditLog audit)
    {
      await _context.AuditLogs.AddAsync(audit);
      _context.Resources.Remove(resource);

      await _context.SaveChangesAsync();
      return resource;
    }
  }
}