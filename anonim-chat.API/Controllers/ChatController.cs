using anonim_chat.API.Managers;
using anonim_chat.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace anonim_chat.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatManager _chatManager;
        public ChatController(IChatManager chatManager)
        {
            _chatManager = chatManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetChats() 
        { 
            return Ok(await _chatManager.GetChatsAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CreateChat(CreateChatModel model)
        {
            var chatModel = await _chatManager.CreateChat(model);
            return Ok(chatModel);
        }
        [HttpPost("Joinchat")]
        public async Task<IActionResult> JoinChat(JoinChatModel model)
        {
            var chatModel = await _chatManager.JoinChatModel(model);
            return Ok(chatModel);
        }

    }
}
