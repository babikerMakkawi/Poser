using Microsoft.AspNetCore.SignalR;

namespace Poser.SignalR
{
    public class MyHub : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }

        public async Task RefreshBookDataAction()
        {
            await Clients.All.SendAsync("RefreshBookData");
        }

    }
}