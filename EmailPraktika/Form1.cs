using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Net.Mime;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.IO;

namespace EmailPraktika
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.ScrollBars = ScrollBars.Vertical;

            //      sander5ashley@yandex.ru
        }

        LinkedResource linkedResource1 = new LinkedResource(@"D:\Projects\aaaaaaaaaaaaaaaaaaaaaaaaaa\1.jpeg", MediaTypeNames.Image.Jpeg);
        LinkedResource linkedResource2 = new LinkedResource(@"D:\Projects\aaaaaaaaaaaaaaaaaaaaaaaaaa\2.jpeg", MediaTypeNames.Image.Jpeg);
        LinkedResource linkedResource3 = new LinkedResource(@"D:\Projects\aaaaaaaaaaaaaaaaaaaaaaaaaa\3.jpeg", MediaTypeNames.Image.Jpeg);
        LinkedResource linkedResource4 = new LinkedResource(@"D:\Projects\aaaaaaaaaaaaaaaaaaaaaaaaaa\4.jpeg", MediaTypeNames.Image.Jpeg);
        LinkedResource linkedResource5 = new LinkedResource(@"D:\Projects\aaaaaaaaaaaaaaaaaaaaaaaaaa\5.jpeg", MediaTypeNames.Image.Jpeg);
        LinkedResource linkedResource6 = new LinkedResource(@"D:\Projects\aaaaaaaaaaaaaaaaaaaaaaaaaa\6.jpeg", MediaTypeNames.Image.Jpeg);

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = $"cid:{ linkedResource1.ContentId}";
            textBox3.Text = $"cid:{ linkedResource2.ContentId}";
            textBox5.Text = $"cid:{ linkedResource3.ContentId}";
            textBox4.Text = $"cid:{ linkedResource4.ContentId}";
            textBox7.Text = $"cid:{ linkedResource5.ContentId}";
            textBox6.Text = $"cid:{ linkedResource6.ContentId}";
        }

        private void puskButton_Click(object sender, EventArgs e)
        {
            int successEm = 0;
            var thread = new Thread (p =>
            {
                for (int i = 0; i < recievers.Lines.Count(); i++)
                {
                    string to, from, pass, messageBody;
                    to = recievers.Lines[i].ToString();
                    from = emailSender.Text;
                    pass = password.Text;

                    messageBody = textBox1.Text;
                    var alternateView = AlternateView.CreateAlternateViewFromString(messageBody, null, MediaTypeNames.Text.Html);
                    alternateView.LinkedResources.Add(linkedResource1);
                    alternateView.LinkedResources.Add(linkedResource2);
                    alternateView.LinkedResources.Add(linkedResource3);
                    alternateView.LinkedResources.Add(linkedResource4);
                    alternateView.LinkedResources.Add(linkedResource5);
                    alternateView.LinkedResources.Add(linkedResource6);

                    MailMessage mailMessage = new MailMessage
                    {
                        To = { to },
                        From = new MailAddress(from),
                        Body = messageBody,
                        Subject = "Услуги ПОЛИГРАФИИ по лучшим ценам !",
                        IsBodyHtml = true,
                        AlternateViews = { alternateView }
                    };

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                    {
                        EnableSsl = true,
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(from, pass)
                    };

                    try
                    {
                        successEm++;
                        smtp.Send(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    if (i + 1 != recievers.Lines.Count())
                    {
                        Thread.Sleep(int.Parse(delay.Text) * 1000);
                    }
                }
                MessageBox.Show($"Отправлено {successEm} писем за {int.Parse(delay.Text) * recievers.Lines.Count()} секунд!", "Готово!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            });
            thread.Start();
        }

        public void SMTPCheck()
        {
            
        }
        bool IsValidEmail1(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public bool IsValidEmail2(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}