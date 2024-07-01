using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduWork.Data;
using EduWork.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace EduWork.WebAPI.Controllers
{
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTimesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectTimesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTime>>> GetProjectTimeRecords()
        {
            return await _context.ProjectTimeRecords.ToListAsync();
        }

        // GET: api/ProjectTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTime>> GetProjectTime(int id)
        {
            var projectTime = await _context.ProjectTimeRecords.FindAsync(id);

            if (projectTime == null)
            {
                return NotFound();
            }

            return projectTime;
        }

        // PUT: api/ProjectTimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTime(int id, ProjectTime projectTime)
        {
            if (id != projectTime.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTimeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProjectTimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectTime>> PostProjectTime(ProjectTime projectTime)
        {
            _context.ProjectTimeRecords.Add(projectTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectTime", new { id = projectTime.Id }, projectTime);
        }

        // DELETE: api/ProjectTimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTime(int id)
        {
            var projectTime = await _context.ProjectTimeRecords.FindAsync(id);
            if (projectTime == null)
            {
                return NotFound();
            }

            _context.ProjectTimeRecords.Remove(projectTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectTimeExists(int id)
        {
            return _context.ProjectTimeRecords.Any(e => e.Id == id);
        }
    }
}
