using AtividadeLemafJoseRenato.Fronteiras.Repositorios;
using AtividadeLemafJoseRenato.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Repositorios
{
    public class EnviarEmailRepositorio : IEnviarEmailRepositorio
    {
        public bool EnviarEmail(string remetente, string destinatario, string assunto, string corpoEmail)
        {
            try
            {
                using (System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage(remetente, destinatario))
                {
                    using (System.Net.Mail.SmtpClient cliente = EmailUtil.ObterClienteSmtp())
                    {
                        email.Subject = assunto;
                        email.Body = corpoEmail;
                        cliente.Send(email);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
