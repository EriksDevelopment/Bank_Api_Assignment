using Bank.Data.Interfaces;
using Bank.Data.Repositories;
using Bank.Core.Interfaces;
using Bank.Core.Services;
using Bank.Core.Services.JwtServices;

namespace Bank.Extensions
{
    public static class AddScopedCollection
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ILoanService, LoanService>();

            services.AddScoped<IDispositionRepository, DispositionRepository>();

            services.AddScoped<JwtService>();

            return services;
        }
    }
}