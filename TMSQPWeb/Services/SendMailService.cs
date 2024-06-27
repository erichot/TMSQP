using Microsoft.Extensions.Options;

using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using NPOI.XWPF.UserModel;


namespace TMSQPWeb.Services
{
    public class SendMailService
    {
        private IWebHostEnvironment m_Environment;
        private readonly SmtpSettingsModel m_SmtpSettings;
        private readonly NotificationService m_NotificationService;



        public SendMailService(IWebHostEnvironment environment
            , Microsoft.Extensions.Options.IOptions<SmtpSettingsModel> _smtpSettingsModel
            , NotificationService notificationService)
        {
            m_Environment = environment;
            m_SmtpSettings = _smtpSettingsModel.Value;
            m_NotificationService = notificationService;
        }




        public SendMailService(IWebHostEnvironment environment
            , SmtpSettingsModel _smtpSettingsInfo
            , NotificationService notificationService)
        {
            m_Environment = environment;
            m_SmtpSettings = _smtpSettingsInfo;
            m_NotificationService = notificationService;
        }






        private async Task<bool> SendMailAsync(MimeMessage _mimeMessage)
        {
            using (SmtpClient _smtpClient = new SmtpClient())
            {
                //await _smtpClient.ConnectAsync(m_SmtpSettingsOptions.SmtpServer,
                //    m_SmtpSettingsOptions.Port, false);
                await _smtpClient.ConnectAsync(m_SmtpSettings.SmtpServer,
                    m_SmtpSettings.Port, false);

                //await _smtpClient.AuthenticateAsync(m_NotificationMetadata.UserName,
                //   m_NotificationMetadata.Password);

                await _smtpClient.SendAsync(_mimeMessage);
                await _smtpClient.DisconnectAsync(true);
            }

            return true;
        }




        //public async Task<BusinessProcessResult> SendMailGeneralAsync(MailMessageModel _mailMessage)
        //{
        //    var result = new BusinessProcessResult();

        //    var _mimeEmailMessage = ConvertFromMailMessage(_mailMessage);
        //    var _mimeMessage = CreateMimeMessageFromEmailMessage(_mimeEmailMessage);

        //    try
        //    {
        //        await SendMail(_mimeMessage);
        //        result.ResultNo = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.ExceptionInfo = ex;                
        //    }


        //    return result;
        //}

        //public async Task<BusinessProcessResult> SendMailGeneralAsync(int _notificationNo)
        //{
        //    var result = new BusinessProcessResult();

        //    var notificationHead = await m_NotificationService.GetEntityAsync(_notificationNo, _enableTracking: true, _includeDetails: true);
        //    if (notificationHead == null)
        //        return null;


        //    // ===============================================================
        //    var _mimeEmailMessage = ConvertFromMailMessage(notificationHead);
        //    var _mimeMessage = CreateMimeMessageFromEmailMessage(_mimeEmailMessage);

        //    try
        //    {
        //        await SendMail(_mimeMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.ExceptionInfo = ex;
        //        return result;
        //    }


        //    // =============================================            
        //    // 回寫 SMTP Setting 內的 Sender資訊
        //    notificationHead.Notified();

        //    try
        //    {
        //        await m_NotificationService.UpdateEntityAsync(notificationHead);

        //    }
        //    catch (Exception ex)
        //    {
        //        result.ExceptionInfo = ex;
        //        return result;
        //    }


        //    return result;
        //}



















        /// <summary>
        /// 
        /// </summary>
        /// <param name="_info"></param>
        /// <remarks>
        /// 附件：實體檔案要先存在於指定路徑，並傳入檔名給MimeMessage
        /// </remarks>
        /// <returns></returns>
        public async Task<List<NotificationDetail>> SendMailAsync(NotificationHead _head)
        {
            var subject = _head.Subject;
            var mailContent = _head.MailContent;
            var senderName = _head.SenderName;
            var senderAddress = _head.SenderAddress;
            var salaryYear = _head.PayslipImport.SalaryYear;
            var salaryMonth = _head.PayslipImport.SalaryMonth;

            // 需要與 WebSystemService 的 GetPayslipPdfFileName 保持一致
            var physicalFullPath = Path.Combine(
                   m_Environment.ContentRootPath
                   , AppSystem.GetRelativePathPayslipPdfRepository(salaryYear, salaryMonth)
                   );


            if (string.IsNullOrEmpty(_head.SenderAddress))
            {
                senderName = m_SmtpSettings.SenderName;
                senderAddress = m_SmtpSettings.SenderAddress;
            }


            // ==============================================================
            var resultDetails = new List<NotificationDetail>();
            foreach (var item in _head.NotificationDetails)
            {
                // 該通知內容的，被通知收件者（一筆or多筆收件人）
                var receivers =
                    item
                        .NotificationRecipients
                        .Select(c =>
                            new MailboxAddress(c.RecipientName, c.Email)
                        )
                        .ToList();

                

                MimeEmailMessage emailMessage = new MimeEmailMessage
                {
                    Sender = new MailboxAddress(senderName, senderAddress),
                    Receivers = receivers,
                    Subject = subject,
                    Content = mailContent,
                    FileName = item.Attachment,
                    PathFileName = Path.Combine(physicalFullPath, item.Attachment)
                };


                var mimeMessage = CreateMimeMessageFromEmailMessage(emailMessage);
                await SendMailAsync(mimeMessage);
                try
                {
                    
                    item.Notified();
                }
                catch (Exception ex)
                {
                    item.HasError(ex);
                }


                resultDetails.Add(item);
            }



            // =============================================================
            return resultDetails;
        }




        private MimeMessage CreateMimeMessageFromEmailMessage(MimeEmailMessage _message)
        {
            var pathFileName = _message.PathFileName;
            var fileName = _message.FileName;
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(_message.Sender);
            //mimeMessage.To.Add(message.Receiver);
            mimeMessage.To.AddRange(_message.Receivers);
            mimeMessage.Subject = _message.Subject;


            var contentBody = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = _message.Content
            };
            var multipart = new Multipart("mixed");
            multipart.Add(contentBody);


            if (string.IsNullOrEmpty(pathFileName) == false)
            {
                var attachment = new MimePart("application/pdf", "pdf")
                {
                    Content = new MimeContent(File.OpenRead(pathFileName), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(pathFileName)
                };

                multipart.Add(attachment);
            }


            // now set the multipart/mixed as the message body
            mimeMessage.Body = multipart;

            return mimeMessage;
        }



















        private class MimeEmailMessage
        {
            public MailboxAddress Sender { get; set; }
            public List<MailboxAddress> Receivers { get; set; }
            public string Subject { get; set; }
            public string Content { get; set; }


            public string PathFileName { get; set; }
            public string FileName { get; set; }
        }



    }
}
