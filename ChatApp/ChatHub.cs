using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> _userConnections = new Dictionary<string, string>();

        public async Task SendMessage(string user, string message)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveMessage", user, message, Context.ConnectionId);
            } catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
                throw;
            }
        }

        public async Task StoreConnectionId(string username)
        {
            _userConnections[Context.ConnectionId] = username;

            await Clients.Caller.SendAsync("ReceiveConnectionId", Context.ConnectionId);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Connection {Context.ConnectionId} connected.");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _userConnections.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
