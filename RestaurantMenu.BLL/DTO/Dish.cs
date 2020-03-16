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
        [Required]
        public string Consist { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal Price { get; set; }
        public int Gram { get; set; }
        public double Calorific { get; set; }
        public int CookTime { get; set; }
    }
}
