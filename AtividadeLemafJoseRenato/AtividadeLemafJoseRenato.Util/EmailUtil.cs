using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeLemafJoseRenato.Util
{
    public static class EmailUtil
    {
        private const string SMTP_GMAIL = "smtp.gmail.com";
        private const string USUARIO_GMAIL = "sistemaenvio9791@gmail.com";
        private const string SENHA_GMAIL = "dti@1406";

        public static SmtpClient ObterClienteSmtp()
        {
            SmtpClient smtp;
            smtp = new SmtpClient(SMTP_GMAIL, 587) { UseDefaultCredentials = false };
            smtp.Credentials = new System.Net.NetworkCredential(USUARIO_GMAIL, SENHA_GMAIL);
            smtp.EnableSsl = true;
            return smtp;
        }
    }
}
