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
        public DateOnly Date { get; set; }
        public string ToAccountNumber { get; set; } = null!;
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public decimal PaymentPerMonth { get; set; }
        public DateOnly EndDate { get; set; }
        public string Status { get; set; } = null!;
    }
}