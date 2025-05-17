using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DDivyansh_Project1.Models
{

    public class SportMetaData 
    {

        [Required(ErrorMessage = "You cannot leave the sport code blank.")]
        [RegularExpression("^[A-Z]{3}$", ErrorMessage = "The sport code must be exactly 3 capital letters.")]
        [StringLength(3)]
        public string Code { get; set; } = "";

        [Required(ErrorMessage = "You cannot leave the sport name blank.")]
        [StringLength(50, ErrorMessage = "Sport name cannot be more than 50 characters long.")]
        public string Name { get; set; } = "";

        //[ScaffoldColumn(false)]
        //[Timestamp]
        //public Byte[]? RowVersion { get; set; }//Added for concurrency 

        //public ICollection<Athlete> Athletes { get; set; } =new List<Athlete>();
    }


}
