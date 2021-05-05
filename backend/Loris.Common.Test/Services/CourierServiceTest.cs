using System;
using System.Collections.Generic;
using System.Linq;
using Loris.Services;
using Loris.Entities;
using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Domain.Services;
using Loris.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loris.Common.Test.Services
{
    [TestClass]
    public class CourierServiceTest
    {
        /*
        select * from public.courier_template;
        select * from public.courier_message;
        select * from public.courier_to;
        select * from public.courier_attachment;
         */

        private Database Database { get; } = DatabaseHelper.RecoverDatabase();

        [TestMethod]
        public void TestAddTemplateSendToken()
        {
            try
            {
                var template = new CourierTemplate()
                {
                    ExternalId = CourierTemplateService.SEND_TOKEN,
                    TemplateName = "Auth-Send new token to e-mail",
                    Title = "Auth-Send new token to e-mail",
                    Template = "Hi, your token is: {0}",
                    SystemSenderIdent = "system@system.com.br",
                    System = CourierSystem.Email,                    
                };

                var service = new CourierTemplateService(null, Database);
                service.Add(template).Wait();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestAddMessage()
        {
            try
            {
                var cTemplService = new CourierTemplateService(null, Database);

                var message = new CourierMessage()
                {
                    Action = CourierAction.ToSend,
                    Generator = InternalSystem.PlatformBase,
                    DtInclusion = DateTime.Now,
                    CourierTemplateId = 0,
                    Title = "Title",
                    Message = "Message",
                    From = "system@system.com.br",
                    To = new List<CourierTo>()
                    {
                        new CourierTo(){
                            SystemUserIdent = "teste@teste.com.br",
                            Status = CourierStatus.ToProcess,
                            System = CourierSystem.Email,
                        }
                    },
                    Attachments = new List<CourierAttachment>()
                    {
                        new CourierAttachment()
                        {
                            FileName = "arquivo teste",
                            FileType = FileType.Document,
                            File = new byte[] { 208, 207, 17, 224, 161, 177, 26, 225 },
                        }
                    },
                };

                var service = new CourierMessageService(null, Database);
                service.Add(message).Wait();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
