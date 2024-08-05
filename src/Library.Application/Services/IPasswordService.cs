namespace Library.Application.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string inputPassword, string storedPassword);
    }
}