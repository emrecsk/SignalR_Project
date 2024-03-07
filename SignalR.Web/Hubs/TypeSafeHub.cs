using Microsoft.AspNetCore.SignalR;
using SignalR.Web.Interfaces;

namespace SignalR.Web.Hubs
{
    public class TypeSafeHub : Hub<ITypeSafeHub>
    {
        private static int _counter = 0;
        public override async Task OnConnectedAsync()
        {
            _counter++;
            await Clients.All.OnlineClients(_counter);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _counter--;
            await Clients.All.OnlineClients(_counter);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task GettingFromClient(string message)
        {
            await Clients.All.SendtoAllClientAsync(message);
        }
        public async Task CallingByClient(string message)
        {
            await Clients.Caller.SendtoCallingClient(message);
        }
        public async Task SendingToOthers(string message)
        {
            await Clients.Others.SendtoOtherClients(message);
        }
        public async Task SendingToSpecific(string message, string connectionId)
        {
            await Clients.Client(connectionId).SendtoSpecificClient(message);
        }
    }
}
