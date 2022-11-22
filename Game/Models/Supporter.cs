using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game.Models
{
    public class Supporter
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("ReferencedPlayer")]
        public int HeroID { get; set; }

        [Required]
        [Display(Name = "Név")]
        public string? Name { get; set; }

        public virtual Player? ReferencedPlayer { get; set; }

        
    }
}
