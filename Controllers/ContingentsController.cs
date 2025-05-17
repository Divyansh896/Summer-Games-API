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
    public class ContingentsController : ControllerBase
    {
        private readonly SummerGamesContext _context;

        public ContingentsController(SummerGamesContext context)
        {
            _context = context;
        }

        // GET: api/Contingents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContingentDTO>>> GetContingents()
        {
            var contingentDTOs = await _context.Contingents
                                               .Select(a => new ContingentDTO
                                               {
                                                   ID = a.ID,
                                                   Code = a.Code,
                                                   Name = a.Name
                                                   
                                               }).ToListAsync();
            if (contingentDTOs.Count() > 0)
            {
                return contingentDTOs;
            }
            else
            {
                return NotFound(new { message = "Error: No Contingent records found in the database." });
            }
        }

        // GET: api/Contingents/inc
        [HttpGet("inc")]
        public async Task<ActionResult<IEnumerable<ContingentDTO>>> GetContingentWithAthletes()
        {
            var contingentDTOs = await _context.Contingents
                                               .Include(a => a.Athletes)
                                               .Select(a => new ContingentDTO
                                               {
                                                   ID = a.ID,
                                                   Code = a.Code,
                                                   Name = a.Name,
                                                   Athletes = a.Athletes.Select(aAthlete => new AthleteDTO
                                                   {
                                                       ID = aAthlete.ID,
                                                       FirstName = aAthlete.FirstName,
                                                       MiddleName = aAthlete.MiddleName,
                                                       LastName = aAthlete.LastName,
                                                       AthleteCode = aAthlete.AthleteCode,
                                                       DOB = aAthlete.DOB,
                                                       Height = aAthlete.Height,
                                                       Weight = aAthlete.Weight,
                                                       Gender = aAthlete.Gender,
                                                       Affiliation = aAthlete.Affiliation,
                                                       
                                                       SportID = aAthlete.SportID,
                                                       Sport = new SportDTO
                                                       {
                                                           ID = aAthlete.Sport.ID,
                                                           Code = aAthlete.Sport.Code,
                                                           Name = aAthlete.Sport.Name,
                                                       }
                                                   }).ToList()
                                               }).ToListAsync();
            if (contingentDTOs.Count() > 0)
            {
                return contingentDTOs;
            }
            else
            {
                return NotFound(new { message = "Error: No Contingent records found in the database." });
            }
        }

        // GET: api/Contingents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContingentDTO>> GetContingent(int id)
        {
            var contingentDTO = await _context.Contingents
                                               .Where(a => a.ID == id)
                                               .Select(a => new ContingentDTO
                                               {
                                                   ID = a.ID,
                                                   Code = a.Code,
                                                   Name = a.Name
                                               })
                                               .FirstOrDefaultAsync();

            if (contingentDTO == null)
            {
                return NotFound(new { message = "Error: No Contingent records found in the database." });
            }

            return contingentDTO;
        }

        // GET: api/Contingents/5
        [HttpGet("inc/{id}")]
        public async Task<ActionResult<ContingentDTO>> GetContingentWithAthletes(int id)
        {
            var contingentDTO = await _context.Contingents
                                               .Where(a => a.ID == id)
                                               .Select(a => new ContingentDTO
                                               {
                                                   ID = a.ID,
                                                   Code = a.Code,
                                                   Name = a.Name,
                                                   Athletes = a.Athletes.Select(aAthlete => new AthleteDTO
                                                   {
                                                       ID = aAthlete.ID,
                                                       FirstName = aAthlete.FirstName,
                                                       MiddleName = aAthlete.MiddleName,
                                                       LastName = aAthlete.LastName,
                                                       AthleteCode = aAthlete.AthleteCode,
                                                       DOB = aAthlete.DOB,
                                                       Height = aAthlete.Height,
                                                       Weight = aAthlete.Weight,
                                                       Gender = aAthlete.Gender,
                                                       Affiliation = aAthlete.Affiliation,
                                                       
                                                       SportID = aAthlete.SportID,
                                                       Sport = new SportDTO
                                                       {
                                                           ID = aAthlete.Sport.ID,
                                                           Code = aAthlete.Sport.Code,
                                                           Name = aAthlete.Sport.Name,
                                                       }
                                                   }).ToList()
                                               })
                                               .FirstOrDefaultAsync();

            if (contingentDTO == null)
            {
                return NotFound(new { message = "Error: No Contingent records found in the database." });
            }

            return contingentDTO;
        }


       

       

        private bool ContingentExists(int id)
        {
            return _context.Contingents.Any(e => e.ID == id);
        }
    }
}
