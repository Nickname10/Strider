using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameServerServices.DataBase.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        [Required]
        public int Points { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
        public User()
        {
            Messages = new List<Message>();
            Characters = new List<Character>();
        }
    }
}