using System.ComponentModel.DataAnnotations;

namespace Game.Models
{
    public class Player
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "Név")]
        public string? Name { get; set; }
        
        [Required]
        [Display(Name = "Felhasználónév")]
        public string? UserName { get; set; }
        
        [Required]
        [Display(Name = "Szint")]
        public int Level { get; set; }
    }
}
