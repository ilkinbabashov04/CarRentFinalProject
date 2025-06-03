using MailKit.Net.Smtp;
using MimeKit;
using MimeKit;

namespace Ui.Helper
{
    public class MailManager 
    {
        private readonly IConfiguration _configuration;
        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendReservationConfirmationAsync(string toEmail, string carModel, decimal price, DateTime pickUp, DateTime dropOff)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = "✅ Reservation Confirmed";

            email.Body = new TextPart("html")
            {
                Text = $@"
                <h2>Your Reservation is Confirmed</h2>
                <p><strong>Car:</strong> {carModel}</p>
                <p><strong>Pick-Up Date:</strong> {pickUp:dd MMM yyyy}</p>
                <p><strong>Drop-Off Date:</strong> {dropOff:dd MMM yyyy}</p>
                <p><strong>Total Price:</strong> ${price}</p>
                <p>Thank you for choosing us!</p>"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:Port"]), true);
            await smtp.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
