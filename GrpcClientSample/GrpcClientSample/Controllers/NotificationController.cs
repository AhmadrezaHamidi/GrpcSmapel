using Grpc.Net.Client;
using GrpcClientSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GrpcClientSample.Protos.SMS;

namespace GrpcClientSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly SMSClient sMSClient;

        public NotificationController(SMSClient sMSClient)
        {
            this.sMSClient = sMSClient;
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(SMSInfoDto model)
        {
            //GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7044");
            //var client = new SMSClient(channel);
            var smsResult = await sMSClient.SendAsync(
                 new Protos.SendSMSRequest { Mobile = model.Mobile, Message = model.Message }
                );

            if (!smsResult.IsSent)
            {
                return BadRequest("is not send sms!!!");
            }

            return Ok("Send sms succ...");

        }
    }
}
