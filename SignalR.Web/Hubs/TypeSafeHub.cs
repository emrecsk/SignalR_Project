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
        public async Task SendingToGroup(string message, string groupName)
        {
            await Clients.Group(groupName).SendtoGroup(message);
        }
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Caller.SendtoCallingClient($"You are now in the group {groupName}.");
                        
            await Clients.Group(groupName).SendtoGroup($"{Context.ConnectionId} has joined the group {groupName}.");
            //await Clients.Others.SendtoOtherClients($"{Context.ConnectionId} has joined the group {groupName}.");
        }
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Caller.SendtoCallingClient($"You left the group {groupName}.");
            
            await Clients.Group(groupName).SendtoGroup($"{Context.ConnectionId} has left the group {groupName}.");
            //await Clients.Others.SendtoOtherClients($"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}
