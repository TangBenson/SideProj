using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRMap.Hubs
{
    public class MotorHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            //發送給所有client端
            string conid = Context.ConnectionId;
            Console.WriteLine($"{user}-{conid}--{message}");
            await Clients.All.SendAsync("ReceiveMessage", $"{user}-{conid}", message);
        }
    }
}