namespace SignalR.Web.Interfaces
{
    public interface ITypeSafeHub
    {
        Task SendtoAllClientAsync(string message);
        Task OnlineClients(int count);
        Task SendtoCallingClient(string message);
        Task SendtoOtherClients(string message);
        Task SendtoSpecificClient(string message);
    }
}
