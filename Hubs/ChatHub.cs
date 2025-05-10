using Microsoft.AspNetCore.SignalR;
using SocialMediaChatApp.Data;
using SocialMediaChatApp.Models;

namespace SocialMediaChatApp.Hubs
{
    public class ChatHub : Hub
{
    private readonly ApplicationDbContext _db;

    public ChatHub(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task SendMessage(string user, string message)
    {
        var msg = new Message { User = user, Content = message, Timestamp = DateTime.UtcNow };
        _db.Messages.Add(msg);
        await _db.SaveChangesAsync(); // ← This must be present

        await Clients.All.SendAsync("ReceiveMessage", user, message);
        Console.WriteLine($"Received message from {user}: {message}");

    }
}

}
