﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DDivyansh_Project1.Models
{
    [ModelMetadataType(typeof(AthleteMetaData))]
    public class AthleteDTO : IValidatableObject
    {
        public int ID { get; set; }

        public string FirstName { get; set; } = "";

        public string? MiddleName { get; set; }


        public string LastName { get; set; } = "";

        public string AthleteCode { get; set; } = "0000000";


        public DateTime DOB { get; set; }

        public int Height { get; set; }

        public double Weight { get; set; }


        public string Gender { get; set; } = "";

        public string Affiliation { get; set; } = "";

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Test date range for DOB - Allowed age range for Summer 2025
            if (DOB < Convert.ToDateTime("1995-08-22") || DOB >= Convert.ToDateTime("2013-08-07"))
            {
                yield return new ValidationResult("DOB must be between 1995-08-22 and 2013-08-06.", new[] { "DOB" });
            }
            //Test BMI Value
            double BMI = Weight / Math.Pow(Height / 100d, 2);
            if (BMI < 15 || BMI >= 40)
            {
                yield return new ValidationResult("BMI of " + BMI.ToString("n1")
                    + " is outside the allowable range of 15 to 40", new[] { "Weight" });
            }
        }

       
        public Byte[]? RowVersion { get; set; }//Added for concurrency

        // Foreign Key for Contingent
        public int ContingentID { get; set; }
        public ContingentDTO? Contingent { get; set; } = null!; 

        // Foreign Key for Sport
        public int SportID { get; set; }
        public SportDTO? Sport { get; set; } = null!; 
    }

}
