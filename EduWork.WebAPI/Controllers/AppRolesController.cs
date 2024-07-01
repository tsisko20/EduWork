﻿using System;
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
    public class AppRolesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AppRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppRole>>> GetAppRoles()
        {
            return await _context.AppRoles.ToListAsync();
        }

        // GET: api/AppRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppRole>> GetAppRole(int id)
        {
            var appRole = await _context.AppRoles.FindAsync(id);

            if (appRole == null)
            {
                return NotFound();
            }

            return appRole;
        }

        // PUT: api/AppRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppRole(int id, AppRole appRole)
        {
            if (id != appRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(appRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppRoleExists(id))
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

        // POST: api/AppRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppRole>> PostAppRole(AppRole appRole)
        {
            _context.AppRoles.Add(appRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppRole", new { id = appRole.Id }, appRole);
        }

        // DELETE: api/AppRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppRole(int id)
        {
            var appRole = await _context.AppRoles.FindAsync(id);
            if (appRole == null)
            {
                return NotFound();
            }

            _context.AppRoles.Remove(appRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppRoleExists(int id)
        {
            return _context.AppRoles.Any(e => e.Id == id);
        }
    }
}
