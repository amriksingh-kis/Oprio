using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using RazorEngine;
using System.Threading.Tasks;

namespace JetWeb
{
    public class Email
    {
        public string ReceiverEmail { get; set; }
        public EmailTemplates Template { get; set; }
        public dynamic Model { get; set; }
        
        public Email()
        {
        }
        public Email(string toEmail, EmailTemplates template, dynamic model)
        {
            ReceiverEmail = toEmail;
            Template = template;
            Model = model;
        }
        public void Send()
        {
            SmtpClient client = new SmtpClient();
            MailMessage msg = new MailMessage();
            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.To.Add(ReceiverEmail);
            EmailTemplate emailTempl = ParseEmailTemplate(Template, Model);
            if (emailTempl != null)
            {
                msg.Subject = emailTempl.Subject;
                msg.Body = emailTempl.Body;
               // Task.Factory.StartNew(()=> client.Send(msg));
            }
        }

        public EmailTemplate ParseEmailTemplate(EmailTemplates template, dynamic model)
        {
            var templateRaw = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplates/" + template.ToString()));
            
            if (templateRaw == null)
                return null;

            string[] sb = templateRaw.Split(new char[]{';'},2);
            EmailTemplate email = new EmailTemplate();

            //subject
            email.Subject = Razor.Parse(sb[0], model);
            //body
            email.Body = Razor.Parse(sb[1], model);
            
            return email;
        }
    }
    public class EmailTemplate
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailTemplate() { }
        public EmailTemplate(string subject, string body) 
        {
            Subject = subject;
            Body = body;
        }
    }
    public enum EmailTemplates
    {
        Welcome, Invite, PwdReset, Rejection
    }
}