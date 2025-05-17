using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DDivyansh_Project1.Models;

namespace DDivyansh_Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        private readonly SummerGamesContext _context;

        public SportsController(SummerGamesContext context)
        {
            _context = context;
        }

        // GET: api/Sports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportDTO>>> GetSports()
        {
            var SportDTOs = await _context.Sports.Select(s => new SportDTO
            {
                ID = s.ID,
                Code = s.Code,
                Name = s.Name
            }).ToListAsync();

            if (SportDTOs.Count() > 0)
            {
                return SportDTOs;
            }
            else
            {
                return NotFound(new { message = "Error: No Sports records found in the database." });
            }

        }

        [HttpGet("inc")]
        public async Task<ActionResult<IEnumerable<SportDTO>>> GetSportsWithIncluded()
        {
            var SportDTOs = await _context.Sports
                                          .Include(a => a.Athletes)
                                          .Select(s => new SportDTO
                                          {
                                              ID = s.ID,
                                              Code = s.Code,
                                              Name = s.Name,
                                              Athletes = s.Athletes.Select(sAthlete => new AthleteDTO
                                              {
                                                  ID = sAthlete.ID,
                                                  FirstName = sAthlete.FirstName,
                                                  MiddleName = sAthlete.MiddleName,
                                                  LastName = sAthlete.LastName,
                                                  AthleteCode = sAthlete.AthleteCode,
                                                  DOB = sAthlete.DOB,
                                                  Height = sAthlete.Height,
                                                  Weight = sAthlete.Weight,
                                                  Gender = sAthlete.Gender,
                                                  Affiliation = sAthlete.Affiliation,
                                                  ContingentID = sAthlete.ContingentID,
                                                  Contingent = new ContingentDTO
                                                  {
                                                      ID = sAthlete.Contingent.ID,
                                                      Code = sAthlete.Contingent.Code,
                                                      Name = sAthlete.Contingent.Name,
                                                  },
                                                  SportID = sAthlete.SportID
                                                 
                                              }).ToList()
                                          }).ToListAsync();

            if (SportDTOs.Count() > 0)
            {
                return SportDTOs;
            }
            else
            {
                return NotFound(new { message = "Error: No Sports records found in the database." });
            }

        }

        // GET: api/Sports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SportDTO>> GetSport(int id)
        {
            var SportDTO = await _context.Sports.Select(s => new SportDTO
            {
                ID = s.ID,
                Code = s.Code,
                Name = s.Name
            }).FirstOrDefaultAsync(a => a.ID == id);

            if (SportDTO == null)
            {
                return NotFound(new { message = $"No Sport found for Sport ID {id}." });
            }

            return SportDTO;
        }

        // GET: api/Sports/5
        [HttpGet("inc/{id}")]
        public async Task<ActionResult<SportDTO>> GetSportWithInc(int id)
        {
            var SportDTO = await _context.Sports.Select(s => new SportDTO
            {
                ID = s.ID,
                Code = s.Code,
                Name = s.Name,
                Athletes = s.Athletes.Select(sAthlete => new AthleteDTO
                {
                    ID = sAthlete.ID,
                    FirstName = sAthlete.FirstName,
                    MiddleName = sAthlete.MiddleName,
                    LastName = sAthlete.LastName,
                    AthleteCode = sAthlete.AthleteCode,
                    DOB = sAthlete.DOB,
                    Height = sAthlete.Height,
                    Weight = sAthlete.Weight,
                    Gender = sAthlete.Gender,
                    Affiliation = sAthlete.Affiliation,
                    ContingentID = sAthlete.ContingentID,
                    Contingent = new ContingentDTO
                    {
                        ID = sAthlete.Contingent.ID,
                        Code = sAthlete.Contingent.Code,
                        Name = sAthlete.Contingent.Name,
                    },
                    SportID = sAthlete.SportID,
                    
                }).ToList()
            }).FirstOrDefaultAsync(a => a.ID == id);

            if (SportDTO == null)
            {
                return NotFound(new { message = $"No Sport found for Sport ID {id}." });
            }

            return SportDTO;
        }

        
        private bool SportExists(int id)
        {
            return _context.Sports.Any(e => e.ID == id);
        }
    }
}
