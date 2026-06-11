using api.Helpers;
using api.Inetraces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Authorize]
  [Route("api/auditLog")]
  [ApiController]
  public class AuditLogController : ControllerBase
  {
    private readonly IAuditLogRepository _auditLogRepository;
    public AuditLogController(IAuditLogRepository auditLogRepository)
    {
      _auditLogRepository = auditLogRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] AuditLogQueryObject query)
    {
      var auditLogs = await _auditLogRepository.GetAllAsync(query);
      return Ok(auditLogs);
    }
  }
}