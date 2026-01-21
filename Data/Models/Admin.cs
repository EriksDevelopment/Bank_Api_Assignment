namespace Bank.Data.Models
{
    public class Admin
    {
        public int Admin_Id { get; set; }
        public string User_Name { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}