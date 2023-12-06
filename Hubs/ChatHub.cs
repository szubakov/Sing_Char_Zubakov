using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Sing_Char_Zubakov.Data;
using Sing_Char_Zubakov.Models;
using System.Runtime.CompilerServices;

namespace Sing_Char_Zubakov.Hubs
{
    public class ChatHub : Hub

    {
        private readonly ChatingContext _context;
        public ChatHub(ChatingContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task Send(string message)
        {

         var newMessage = new Mess
            {
                UserId = GetUserId(),
                Text = message,
                Date = DateTime.Now
            };

            await _context.Mess.AddAsync(newMessage);
            await _context.SaveChangesAsync();

           

            var messageDto = new MessageDto
            {
                Id = newMessage.Id,
                Name = Context.User.Identity.Name,
                Date = DateTime.Now.ToString("dd.MM HH:mm"),
                Message = message
            };
           


            await Clients.All.SendAsync("Receive", messageDto);
        }


      
        [Authorize]
        public async Task DeleteMessage(int id)
        {
            var message = _context.Mess.FirstOrDefault(x => x.Id == id);


            if (message != null)
            {
               

                if (message.UserId == GetUserId() || Context.User.IsInRole(UserRole.Admin.ToString()))
                    {
                    _context.Mess.Remove(message);
                    _context.SaveChanges();

                }
              
            }

            await Clients.All.SendAsync("HideMessage", message.Id);
        }
        private int GetUserId()
        {
            var userIdSrt = Context.User.FindFirst("Id")?.Value;
            var userId = Convert.ToInt32(userIdSrt);
            
            return userId;
        }
    }
}
