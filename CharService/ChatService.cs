using Sing_Char_Zubakov.Data;
using System.Data;

namespace CharService
{

    public class ChatService
    {
        private readonly ChatingContext _context;
        public ChatService(ChatingContext context)
        {
            _context = context;
        }

        public async Task DeleteOldMessages()
        {

            var messages = _context.Mess.Where(x => x.Date < DateTime.Now.AddMinutes(-5)). ToList();

            _context.Mess.RemoveRange(messages);    
            _context.SaveChanges();
        }
    }
}
