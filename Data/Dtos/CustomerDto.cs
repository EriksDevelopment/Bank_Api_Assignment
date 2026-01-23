namespace Bank.Data.Dtos
{
    public class AdminCreateCustomerRequestDto
    {
        public string Gender { get; set; } = null!;
        public string Givenname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Streetaddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Zipcode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public DateOnly? Birthday { get; set; }
        public string? Telephonecountrycode { get; set; }
        public string? Telephonenumber { get; set; }
        public string Emailaddress { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class AdminCreateCustomerResponseDto
    {
        public string Message { get; set; } = null!;
        public int CustomerId { get; set; }
        public string Email { get; set; } = null!;

        public string Frequency { get; set; } = null!;
        public string Account { get; set; } = null!;
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; } = null!;
        public DateOnly Created { get; set; }
    }

    public class CustomerLoginRequestDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class CustomerLoginResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }
}