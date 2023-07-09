using System.Security.Cryptography;
using System.Text;
using SocialMedia.Services.IServices;

namespace SocialMedia.Services;

public class PasswordService: IPasswordService
{
    public string EncodePassword(string password)
    {
        string base64HashedPasswordBytes;
        using (var sha256 = SHA256.Create())
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
            base64HashedPasswordBytes = Convert.ToBase64String(hashedPasswordBytes);
        }

        return base64HashedPasswordBytes;
    }
}