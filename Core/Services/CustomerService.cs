using Bank.Core.Interfaces;
using Bank.Core.Services.JwtServices;
using Bank.Data.Dtos;
using Bank.Data.Models;
using Bank.Data.Interfaces;

namespace Bank.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly IDispositionRepository _dispositionRepo;
        private readonly JwtService _jwtService;

        public CustomerService
        (
            ICustomerRepository customerRepo,
            IAccountRepository accountRepo,
            IDispositionRepository dispositionRepo,
            JwtService jwtService
        )
        {
            _customerRepo = customerRepo;
            _accountRepo = accountRepo;
            _dispositionRepo = dispositionRepo;
            _jwtService = jwtService;
        }

        public async Task<CustomerLoginResponseDto?> CustomerLoginAsync(CustomerLoginRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Email and password are required.");

            var customer = await _customerRepo.GetByEmailAsync(dto.Email);
            if (customer == null || !BCrypt.Net.BCrypt.Verify(dto.Password, customer.Password))
                throw new UnauthorizedAccessException("Invalid credentials.");

            var token = _jwtService.GenerateToken(customer.CustomerId, "Customer");

            return new CustomerLoginResponseDto
            {
                AccessToken = token
            };
        }

        public async Task<AdminCreateCustomerResponseDto> CreateCustomerWithAccount(AdminCreateCustomerRequestDto dto)
        {

            if (string.IsNullOrWhiteSpace(dto.Gender) ||
                string.IsNullOrWhiteSpace(dto.Givenname) ||
                string.IsNullOrWhiteSpace(dto.Surname) ||
                string.IsNullOrWhiteSpace(dto.Streetaddress) ||
                string.IsNullOrWhiteSpace(dto.City) ||
                string.IsNullOrWhiteSpace(dto.Zipcode) ||
                string.IsNullOrWhiteSpace(dto.Country) ||
                string.IsNullOrWhiteSpace(dto.CountryCode) ||
                string.IsNullOrWhiteSpace(dto.Emailaddress) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("All required fields must be filled in. The following fields are optional: Birthday, TelephoneCountryCode, TelephoneNumber.");

            var emailExists = await _customerRepo.GetByEmailAsync(dto.Emailaddress);
            if (emailExists != null)
                throw new ArgumentException("Email already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);


            var customer = new Customer
            {
                Gender = dto.Gender,
                Givenname = dto.Givenname,
                Surname = dto.Surname,
                Streetaddress = dto.Streetaddress,
                City = dto.City,
                Zipcode = dto.Zipcode,
                Country = dto.Country,
                CountryCode = dto.CountryCode,
                Birthday = dto.Birthday,
                Telephonecountrycode = dto.Telephonecountrycode,
                Telephonenumber = dto.Telephonenumber,
                Emailaddress = dto.Emailaddress,
                Password = hashedPassword
            };

            customer = await _customerRepo.CreateCustomerAsync(customer);

            var account = new Account
            {
                Balance = 0,
                Created = DateOnly.FromDateTime(DateTime.UtcNow),
                Frequency = "Monthly",
                AccountTypesId = 1
            };

            account = await _accountRepo.CreateAccountAsync(account);

            var disposition = new Disposition
            {
                CustomerId = customer.CustomerId,
                AccountId = account.AccountId,
                Type = "OWNER"
            };

            var accountType = await _accountRepo.GetAccountTypeAsync(account.AccountTypesId);

            await _dispositionRepo.CreateDispositionAsync(disposition);

            return new AdminCreateCustomerResponseDto
            {
                Message = "Customer and related account created successfully.",
                CustomerId = customer.CustomerId,
                Email = customer.Emailaddress,
                Balance = account.Balance,
                Frequency = account.Frequency,
                Account = accountType!.TypeName,
                AccountNumber = account.AccountNumber,
                Created = account.Created
            };
        }
    }
}