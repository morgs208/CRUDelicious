using System.ComponentModel.DataAnnotations;
using System;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]

            public int DishId {get; set;}
            
            [Required(ErrorMessage="Required")]
            [Display(Name = "Chef's Name")]
            public string ChefName {get; set;}

            [Required(ErrorMessage="Required")]
            [Display(Name = "Dish Name")]
            public string DishName {get; set;}

            [Required(ErrorMessage="Required")]
            [Display(Name = "Calories")]
            [Range(1, int.MaxValue, ErrorMessage = "Value must be bigger then 0")]
            public int Calories {get; set;}

            [Required(ErrorMessage="Required")]
            [Display(Name = "Toastiness")]
            [Range(1,5)]
            public int Toastiness {get; set;}

            [Required(ErrorMessage="Required")]
            [Display(Name = "Description")]
            public string Description {get; set;}

            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;
}
}