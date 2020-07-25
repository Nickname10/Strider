using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameServerServices.DataBase.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public byte MainRedColor { get; set; }
        [Required]
        public byte MainGreenColor { get; set; }
        [Required]
        public byte MainBlueColor { get; set; }
        [Required]
        public byte StrokeRedColor { get; set; }
        [Required]
        public byte StrokeGreenColor { get; set; }
        [Required]
        public byte StrokeBlueColor { get; set; }
        [Required]
        public byte StrokeLength { get; set; }
        [Required]
        public byte StrokeSpace { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
        public Character() { }
        public Character(ClientServerModels.Character character, User user)
        {
            Name = character.Name;
            MainRedColor = character.MainRedColor;
            MainGreenColor = character.MainGreenColor;
            MainBlueColor = character.MainBlueColor;
            StrokeRedColor = character.StrokeRedColor;
            StrokeGreenColor = character.StrokeGreenColor;
            StrokeBlueColor = character.StrokeBlueColor;
            StrokeLength = character.StrokeLength;
            StrokeSpace = character.StrokeSpace;
            UserId = user.Id;
            User = user;
        }
    }
}