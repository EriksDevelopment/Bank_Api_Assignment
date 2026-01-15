namespace Bank.Data.Dtos
{
    public class ShowAllTransactionsResponseDto
    {
        public string AccountNumber { get; set; } = null!;
        public DateOnly Date { get; set; }

        public string Type { get; set; } = null!;

        public string Operation { get; set; } = null!;

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public string? Symbol { get; set; }

        public string? Bank { get; set; }

        public string? Account { get; set; }
    }

    public class CreateTransactionRequestDto
    {
        public string FromAccountNumber { get; set; } = null!;
        public string ToAccountNumber { get; set; } = null!;
        public decimal Amount { get; set; }
    }

    public class CreateTransactionResponseDto
    {
        public string Message { get; set; } = null!;
        public string FromAccountNumber { get; set; } = null!;
        public string ToAccountNumber { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
    }
}