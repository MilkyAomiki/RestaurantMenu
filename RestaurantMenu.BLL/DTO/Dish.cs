using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMenu.BLL.DTO
{
    public class Dish
    {

        [Key]
        [Column("ID")]
        public short Id { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Consist { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Gram { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Calorific { get; set; }
        [Range(0, int.MaxValue)]
        public int CookTime { get; set; }
    }
}
