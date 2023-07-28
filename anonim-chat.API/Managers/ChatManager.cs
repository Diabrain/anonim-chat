using anonim_chat.API.Context;
using anonim_chat.API.Entities;
using anonim_chat.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace anonim_chat.API.Managers;

public class ChatManager : IChatManager
{
    private readonly AppDbContext _context;
    private readonly IUserProvider _userProvider;
    public ChatManager(AppDbContext context, IUserProvider userProvider)
    {
        _context = context;
        _userProvider = userProvider;
    }

    public async Task<List<ChatModel>> GetChatsAsync()
    {
        var userId =  _userProvider.UserId;
        var chats = await _context.Chats.Where(u => u.CreatedUserId == userId || u.JoinedUserId != null || u.JoinedUserId == userId).ToListAsync();
        if(chats is null)
        {
            return new List<ChatModel>();
        }
        var chatModels = new List<ChatModel>();
        foreach(var chat in chats)
        {
            chatModels.Add(chat.ToModel());
        }
        return chatModels;
        
    }

    public async Task<ChatModel> CreateChat(CreateChatModel model)
    {
        var isKey = await _context.Chats.AnyAsync(u => u.Key == model.Key);
        if (isKey)
        {
            throw new Exception("Key already exists");
        }
        var chat = new Chat() {
            Key = model.Key,
            CreatedUserId = _userProvider.UserId,
            Messages = new List<Message>()
        };
        _context.Chats.Add(chat);
        await _context.SaveChangesAsync();
        return chat.ToModel();
    }

   
    public async Task<ChatModel> JoinChatModel( JoinChatModel model)
    {
        var chat = await _context.Chats.FirstOrDefaultAsync(u => u.Key == model.Key);
        
        if(chat is null)
        {
            throw new Exception("Key xato kiritildi");
        }
        if (chat.CreatedUserId == _userProvider.UserId)
        {
            throw new Exception("Siz bu chatda creatorsiz!!!");
        }
        chat.JoinedUserId = _userProvider.UserId;
        chat.Messages = new List<Message>();
        await _context.SaveChangesAsync();
        return chat.ToModel();
    }
}
