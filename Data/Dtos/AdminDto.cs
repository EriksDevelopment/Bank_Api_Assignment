namespace Bank.Data.Dtos
{
    public class AdminLoginRequestDto
    {
        public string User_Name { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class AdminLoginResponseDto
    {
        public string AccessToken { get; set; } = null!;
    }
}