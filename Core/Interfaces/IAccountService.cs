using Bank.Data.Dtos;

namespace Bank.Core.Interfaces
{
    public interface IAccountService
    {

        Task<List<ShowAccountsResponseDto>> GetCustomerAccountAsync(int customerId);

        Task<CreateAccountResponseDto> CreateAccountAsync(CreateAccountRequestDto dto, int customerId, string accountFrequency);
    }
}