using Bank.Data.Dtos;

namespace Bank.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<List<ShowAllTransactionsResponseDto>> GetAccountTransactionsAsync(int customerId, string accountNumber);

        Task<CreateTransactionResponseDto> CreateTransactionAsync(int customerId, CreateTransactionRequestDto dto);
    }
}