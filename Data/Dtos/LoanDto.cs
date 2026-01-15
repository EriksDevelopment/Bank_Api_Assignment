namespace Bank.Data.Dtos
{
    public class AdminCreateCustomerLoanRequestDto
    {
        public string ToAccountNumber { get; set; } = null!;

        public decimal Amount { get; set; }

        public int Duration { get; set; }
    }

    public class AdminCreateCustomerLoanResponseDto
    {
        public string Message { get; set; } = null!;
        public string ToAccountNumber { get; set; } = null!;

        public decimal Amount { get; set; }

        public int Duration { get; set; }

        public decimal Payments { get; set; }
        public string Status { get; set; } = null!;
        public DateOnly Date { get; set; }
    }
}