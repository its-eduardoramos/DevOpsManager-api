using System.Security.Claims;
using api.Dtos;
using api.Inetraces;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Authorize]
  [Route("api/resource")]
  [ApiController]
  public class ResourceController : ControllerBase
  {
    private readonly IResourceRepository _resourceRepository;
    public ResourceController(IResourceRepository resourceRepository)
    {
      _resourceRepository = resourceRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetResources()
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      if(userId is null) return Unauthorized();
      var resources = await _resourceRepository.GetResourcesAsync(userId);

      var rnd = new Random();

      return Ok(resources.Select(r => r.ToResponse(rnd)));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      var resource = await _resourceRepository.GetAsyncById(id);
      if (resource is null) return NotFound();
      var rnd = new Random();
      return Ok(resource.ToResponse(rnd));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResourceRequest resourceDto)
    { 
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //Id del usuario actual

      var userEmail = User.FindFirstValue(ClaimTypes.Email);

      var resource = resourceDto.ToEntity(userId);
      var auditLog = new AuditLog
      {
        Action = "Resource creation",
        ResourceName = resourceDto.Name,
        UserEmail = userEmail
      };

      var createdResource = await _resourceRepository.CreateAsync(resource, auditLog);
      var rnd = new Random();

      return CreatedAtAction(
        nameof(GetById),
        new { id = createdResource.Id},
        createdResource.ToResponse(rnd)
      );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateResourceRequest updateDto)
    {
      var userEmail = User.FindFirstValue(ClaimTypes.Email);

      var auditLog = new AuditLog
      {
        Action = "Resource update",
        ResourceName = updateDto.Name,
        UserEmail = userEmail
      };

      var updatedResource = await _resourceRepository.UpdateAsync(id, updateDto, auditLog);
      if(updatedResource is null) return NotFound();
      var rnd = new Random();
      return Ok(updatedResource.ToResponse(rnd));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute]  int id)
    {
      var userEmail = User.FindFirstValue(ClaimTypes.Email);
      var existingResource = await _resourceRepository.GetAsyncById(id);
      if(existingResource is null) return NotFound();

      var auditLog = new AuditLog
      {
        Action = "Resource deletion",
        ResourceName = existingResource.Name,
        UserEmail = userEmail
      };
      
      var deletedResource = await _resourceRepository.DeleteAsync(existingResource, auditLog);
      return NoContent();
    }
  }
}