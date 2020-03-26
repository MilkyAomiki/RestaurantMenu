using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMenu.BLL.DTO
{
    /// <summary>
    /// DTO for main model
    /// </summary>
    public class DishDTO
    {

        [Key]
        [Column("ID")]
        public short Id { get; set; }
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public string Consist { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        
        [Range(0, Int32.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Gram { get; set; }
        [Range(0, Int32.MaxValue)]
        public decimal Calorific { get; set; }
        public decimal TotalCalorific { get;  set; }

        
        [Range(0, int.MaxValue)]
        public int CookTime { get; set; }
    }
}
