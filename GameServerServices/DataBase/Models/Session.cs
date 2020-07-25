using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace GameServerServices.DataBase.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int MaxPlayers { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required]
        [ForeignKey("Creator")]
        public int UserId { get; set; }
        [Required]
        public User Creator { get; set; }
        public virtual  ICollection<Message> Messages { get; set; }
        public Session()
        {
            Messages = new List<Message>();
        }
    }
}