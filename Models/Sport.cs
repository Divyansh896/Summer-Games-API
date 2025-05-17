using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DDivyansh_Project1.Models
{
    [ModelMetadataType(typeof(SportMetaData))]
    public class Sport : Auditable
    {
        public int ID { get; set; }

       
        public string Code { get; set; } = "";

        public string Name { get; set; } = "";
        
        //public Byte[]? RowVersion { get; set; }//Added for concurrency 
        public ICollection<Athlete> Athletes { get; set; } = new List<Athlete>();
    }


}
