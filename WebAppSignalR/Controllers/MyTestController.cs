using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class MyTestController : ControllerBase
    {
        Mydata mydata;

        private readonly IHubContext<NotificationHub> _hubContext;

        public MyTestController(IHubContext<NotificationHub> hubContext)
        {
            mydata = new Mydata();
            _hubContext = hubContext;
        }
        
        [HttpGet]
        [Route("~/mytest/getmsg")]
        public ActionResult<Mydata> Getmsg()
        {
            return mydata;
        }

        [HttpGet]
        [Route("~/mytest/notify")]
        public async Task NotifyAll(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", $"Time loaded at:{message}:-> {DateTime.Now}");
        }

    }

    public class Mydata
    {
        public string myname { get; set; } = "Hello Vijay Pitroda";

       
    }
}
