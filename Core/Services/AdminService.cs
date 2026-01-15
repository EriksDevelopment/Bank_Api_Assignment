using Bank.Core.Interfaces;
using Bank.Data.Dtos;
using Bank.Core.Services.JwtServices;
using Bank.Data.Interfaces;

namespace Bank.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepo;
        private readonly JwtService _jwtService;

        public AdminService
        (
            IAdminRepository adminRepo,
            JwtService jwtService
        )
        {
            _adminRepo = adminRepo;
            _jwtService = jwtService;
        }

        public async Task<AdminLoginResponseDto?> AdminLoginAsync(AdminLoginRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.User_Name) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Email and password are required");

            var admin = await _adminRepo.GetByUserNameAsync(dto.User_Name);
            if (admin == null || admin.Password != dto.Password)
                throw new UnauthorizedAccessException("Invalid credentials");


            var token = _jwtService.GenerateToken(admin.Admin_Id, "Admin");

            return new AdminLoginResponseDto
            {
                AccessToken = token
            };
        }
    }
}