using DDivyansh_Project1.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DDivyansh_Project1.Models
{
    [ModelMetadataType(typeof(ContingentMetaData))]
    public class ContingentDTO
    {
        public int ID { get; set; }

       
        public string Code { get; set; } = "";

       
        public string Name { get; set; } = "";

        // Navigation Property
        public ICollection<AthleteDTO>? Athletes { get; set; }

    }

}