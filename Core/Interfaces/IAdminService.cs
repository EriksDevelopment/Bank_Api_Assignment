using Bank.Data.Dtos;

namespace Bank.Core.Interfaces
{
    public interface IAdminService
    {
        Task<AdminLoginResponseDto?> AdminLoginAsync(AdminLoginRequestDto dto);
    }
}