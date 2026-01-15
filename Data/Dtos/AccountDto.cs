namespace Bank.Data.Dtos
{
    public class ShowAccountsResponseDto
    {
        public string AccountNumber { get; set; } = null!;
        public string Account { get; set; } = null!;
        public decimal Balance { get; set; }
    }

    public class CreateAccountRequestDto
    {
        public string AccountFrequency { get; set; } = "Monthly";
        public int AccountTypeId { get; set; } = 1;
    }

    public class CreateAccountResponseDto
    {
        public string Message { get; set; } = null!;
        public string Frequency { get; set; } = "Monthly";
        public string Account { get; set; } = "Standard transaction account";
        public decimal Balance { get; set; } = 0;
        public string AccountNumber { get; set; } = null!;
        public DateOnly Created { get; set; }
    }
}