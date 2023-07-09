namespace SocialMedia.Services.IServices;

public interface IPasswordService
{
    public string EncodePassword(string password);
}