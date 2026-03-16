using System.Net;
using System.Net.Mail;

namespace AutoService.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task TrimiteNotificareFinalizare(string emailClient, string numeClient, string masina, string nrInmatriculare)
        {
            try
            {
                // NOTĂ: Aici trebuie să pui datele tale de SMTP în appsettings.json
                var smtpServer = "smtp.gmail.com"; 
                var port = 587;
                var emailSursa = "lucianjuganaru85@gmail.com"; 
                var parolaSursa = "kybd anmm xepw heis"; 

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailSursa, "CROX Service Auto"),
                    Subject = $"🔧 Mașina dvs. {nrInmatriculare} este gata!",
                    Body = $@"Salutare, {numeClient},

Vă informăm că reparațiile pentru autovehiculul {masina} ({nrInmatriculare}) au fost finalizate cu succes.
Mașina este pregătită pentru ridicare.

Vă așteptăm la service!
Echipa CROX Service.",
                    IsBodyHtml = false
                };
                mailMessage.To.Add(emailClient);

                using var smtpClient = new SmtpClient(smtpServer, port);
                smtpClient.Credentials = new NetworkCredential(emailSursa, parolaSursa);
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Dacă eșuează email-ul, nu vrem să blocăm restul aplicației
                Console.WriteLine($"Eroare trimitere email: {ex.Message}");
            }
        }
    }
}