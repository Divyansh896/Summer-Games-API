using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DDivyansh_Project1.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace DDivyansh_Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthletesController : ControllerBase
    {
        private readonly SummerGamesContext _context;

        public AthletesController(SummerGamesContext context)
        {
            _context = context;
        }

        // GET: api/Athletes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AthleteDTO>>> GetAthletes()
        {
            var athleteDTOs = await _context.Athletes
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Gender = a.Gender,
                    Affiliation = a.Affiliation,
                    ContingentID = a.ContingentID,
                    Contingent = new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name,
                       
                    },
                    SportID = a.SportID,
                    Sport = new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name
                    },
                    RowVersion = a.RowVersion
                }).ToListAsync();
            if (athleteDTOs.Count() > 0)
            {
                return athleteDTOs;
            }
            else
            {
                return NotFound(new { message = "Error: No athlete records found in the database." });
            }

        }




        // GET: api/Athletes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AthleteDTO>> GetAthlete(int id)
        {
            var athleteDTO = await _context.Athletes
                .Include(a => a.Contingent)
                .Include(a => a.Sport)
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Gender = a.Gender,
                    Affiliation = a.Affiliation,
                    ContingentID = a.ContingentID,
                    Contingent = a.Contingent != null ? new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name
                    } : null,

                    SportID = a.SportID,
                    Sport = a.Sport != null ? new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name
                    } : null,

                    RowVersion = a.RowVersion
                })
                .FirstOrDefaultAsync(p => p.ID == id);  // Get only one record

            if (athleteDTO == null)
            {
                return NotFound(new { message = "Error: That Athlete was not found in the database." });
            }

            return athleteDTO;
        }


        // PUT: api/Athletes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAthlete(int id, AthleteDTO athleteDTO)
        {
            if (id != athleteDTO.ID)
            {
                return BadRequest(new { message = "Error: ID does not match Athlete" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var athleteToUpdate = await _context.Athletes.FindAsync(id);

            //Check that you got it
            if (athleteToUpdate == null)
            {
                return NotFound(new { message = "Error: Athlete record not found." });
            }

            //Wow, we have a chance to check for concurrency even before bothering
            //the database!  Of course, it will get checked again in the database just in case
            //it changes after we pulled the record.  
            //Note using SequenceEqual becuase it is an array after all.
            if (athleteDTO.RowVersion != null)
            {
                if (!athleteToUpdate.RowVersion.SequenceEqual(athleteDTO.RowVersion))
                {
                    return Conflict(new { message = "Concurrency Error: Athlete has been changed by another user.  Try editing the record again." });
                }
            }

            athleteToUpdate.ID = athleteDTO.ID;
            athleteToUpdate.FirstName = athleteDTO.FirstName;
            athleteToUpdate.MiddleName = athleteDTO.MiddleName;
            athleteToUpdate.LastName = athleteDTO.LastName;
            athleteToUpdate.AthleteCode = athleteDTO.AthleteCode;
            athleteToUpdate.DOB = athleteDTO.DOB;
            athleteToUpdate.Height = athleteDTO.Height;
            athleteToUpdate.Weight = athleteDTO.Weight;
            athleteToUpdate.Gender = athleteDTO.Gender;
            athleteToUpdate.Affiliation = athleteDTO.Affiliation;
            athleteToUpdate.RowVersion = athleteDTO.RowVersion;
            athleteToUpdate.ContingentID = athleteDTO.ContingentID;
            athleteToUpdate.SportID = athleteDTO.SportID;

            //Put the original RowVersion value in the OriginalValues collection for the entity
            _context.Entry(athleteToUpdate).Property("RowVersion").OriginalValue = athleteDTO.RowVersion;


            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AthleteExists(id))
                {
                    return Conflict(new { message = "Concurrency Error: Athlete has been Removed." });
                }
                else
                {
                    return Conflict(new { message = "Concurrency Error: Athlete has been updated by another user.  Back out and try editing the record again." });
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE"))
                {
                    return BadRequest(new { message = "Unable to save: Duplicate Athlete Code." });
                }
                else
                {
                    return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
                }
            }

        }

        // POST: api/Athletes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AthleteDTO>> PostAthlete(AthleteDTO athleteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Athlete athlete = new Athlete
            {
                ID = athleteDTO.ID,
                FirstName = athleteDTO.FirstName,
                MiddleName = athleteDTO.MiddleName,
                LastName = athleteDTO.LastName,
                AthleteCode = athleteDTO.AthleteCode,
                DOB = athleteDTO.DOB,
                Height = athleteDTO.Height,
                Weight = athleteDTO.Weight,
                Gender = athleteDTO.Gender,
                Affiliation = athleteDTO.Affiliation,
                RowVersion = athleteDTO.RowVersion,
                ContingentID = athleteDTO.ContingentID,
                SportID = athleteDTO.SportID
                
            };

            try
            {
                _context.Athletes.Add(athlete);
                await _context.SaveChangesAsync();

                //Assign Database Generated values back into the DTO
                athleteDTO.ID = athlete.ID;
                athleteDTO.RowVersion = athlete.RowVersion;

                return CreatedAtAction(nameof(GetAthlete), new { id = athlete.ID }, athleteDTO);
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE"))
                {
                    return BadRequest(new { message = "Unable to save: Duplicate Athlete Code." });
                }
                else
                {
                    return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
                }
            }


        }

        // DELETE: api/Athletes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAthlete(int id)
        {
            var athlete = await _context.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return NotFound(new { message = "Delete Error: Athlete has already been removed." });
            }
            try
            {
                _context.Athletes.Remove(athlete);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Delete Error: Unable to delete Athlete." });
            }
        }

        // Get: api/GetAthleteBySportID
        [HttpGet("BySport/{id}")]
        public async Task<ActionResult<IEnumerable<AthleteDTO>>> GetAthleteBySportID(int id)
        {
            var athleteDTOs = await _context.Athletes
                .Include(a => a.Sport)
                .Include(a => a.Contingent)
                .Where(a => a.SportID == id)
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Gender = a.Gender,
                    Affiliation = a.Affiliation,

                    ContingentID = a.ContingentID,
                    Contingent = a.Contingent != null ? new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name
                    } : null,

                    SportID = a.SportID,
                    Sport = a.Sport != null ? new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name
                    } : null
                })
                .ToListAsync();

            if (!athleteDTOs.Any())
            {
                return NotFound(new { message = $"No athletes found for Sport ID {id}." });
            }

            return athleteDTOs;
        }

        // Get: api/GetAthleteByContingentID
        [HttpGet("ByContingent/{id}")]
        public async Task<ActionResult<IEnumerable<AthleteDTO>>> GetAthleteByContingentID(int id)
        {
            var athleteDTOs = await _context.Athletes
                .Include(a => a.Sport)
                .Include(a => a.Contingent)
                .Where(a => a.ContingentID == id)
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Gender = a.Gender,
                    Affiliation = a.Affiliation,

                    ContingentID = a.ContingentID,
                    Contingent = a.Contingent != null ? new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name
                    } : null,

                    SportID = a.SportID,
                    Sport = a.Sport != null ? new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name
                    } : null
                })
                .ToListAsync();

            if (!athleteDTOs.Any())
            {
                return NotFound(new { message = $"No athletes found for Contingent ID {id}." });
            }

            return athleteDTOs;
        }

        private bool AthleteExists(int id)
        {
            return _context.Athletes.Any(e => e.ID == id);
        }
    }
}
