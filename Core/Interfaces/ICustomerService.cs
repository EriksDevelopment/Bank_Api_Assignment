using Bank.Data.Dtos;

namespace Bank.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerLoginResponseDto?> CustomerLoginAsync(CustomerLoginRequestDto dto);
        Task<AdminCreateCustomerResponseDto> CreateCustomerWithAccount(AdminCreateCustomerRequestDto dto);
    }
}