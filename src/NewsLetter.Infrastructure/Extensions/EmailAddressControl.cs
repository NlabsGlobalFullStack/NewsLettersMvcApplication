using System.Net.Mail;

namespace NewsLetter.Infrastructure.Extensions;
public static class EmailAddressControl
{
    public static bool IsValidEmail(this string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return mailAddress.Address == email;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
