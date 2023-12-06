using System.ComponentModel.DataAnnotations.Schema;

namespace Sing_Char_Zubakov.Data
{
    public class Mess
    {
        public int Id { get; set; }

        [ForeignKey (nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
