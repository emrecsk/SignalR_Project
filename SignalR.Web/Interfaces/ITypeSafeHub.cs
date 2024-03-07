namespace SignalR.Web.Interfaces
{
    public interface ITypeSafeHub
    {
        Task SendtoAllClientAsync(string message);
        Task OnlineClients(int count);
    }
}
