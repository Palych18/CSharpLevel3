using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Model
{
    class EmailSendServiceClass
    {
        #region vars
        private string strLogin;         // email, c которого будет рассылаться почта
        private string strPassword;  // пароль к email, с которого будет рассылаться почта
        private string strSmtp = "smtp.yandex.ru"; // smtp-server
        private int iSmtpPort = 25;                // порт для smtp-server
        #endregion
        public EmailSendServiceClass(string sLogin, string sPassword)
        {
            strLogin = sLogin;
            strPassword = sPassword;
        }

        private void SendMail(string mail, string name)
        {
            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = "none";//strSubject;
                mm.Body = "Hello world!";
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(strSmtp, iSmtpPort);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, strPassword);
                try
                {
                    sc.Send(mm);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Невозможно отправить письмо " + ex.ToString());
                }
            }
        }

    }

    
    static class ServiceData
    {
        static public List<SmtpClient> SmtpClients { get; } = new List<SmtpClient>() { new SmtpClient("smtp.yandex.ru", 25), new SmtpClient("smtp.gmail.com", 58), new SmtpClient("smtp.mail.ru", 25) };

    }


    class EMailSendServiceClass
    {

        static public event Action<string> Action;
        public void Send(MailMessage message, string password, int port)
        {
            try
            {
                
                string subject = message.Subject;
                
                password = System.IO.File.ReadAllText("C:\\temp\\1.txt");
                string body = message.Body;
                var smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("pavel@gmail.com", password)
                };
                Action?.Invoke("Message is sending");
                smtp.Send(message);                
                Action?.Invoke("Message has sent");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Action?.Invoke(ex.Message);
            }


        }
    }
}
