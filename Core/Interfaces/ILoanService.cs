using Bank.Data.Dtos;

namespace Bank.Core.Interfaces
{
    public interface ILoanService
    {
        Task<AdminCreateCustomerLoanResponseDto> CreateLoanAsync(AdminCreateCustomerLoanRequestDto dto);
    }
}