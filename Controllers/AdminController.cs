using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bank.Data.Dtos;
using Bank.Core.Interfaces;

namespace Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController
        (
            IAdminService adminService,
            ILogger<AdminController> logger
        )
        {
            _adminService = adminService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("admin-login")]
        public async Task<ActionResult<AdminLoginResponseDto>> AdminLogin([FromBody] AdminLoginRequestDto request)
        {
            try
            {
                var result = await _adminService.AdminLoginAsync(request);

                _logger.LogInformation("Login successfull (Admin)");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while login (Admin).");
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}