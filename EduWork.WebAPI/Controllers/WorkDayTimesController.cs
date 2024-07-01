﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduWork.Data;
using EduWork.Data.Entities;

namespace EduWork.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkDayTimesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkDayTimesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkDayTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkDayTime>>> GetWorkDayTimeRecords()
        {
            return await _context.WorkDayTimeRecords.ToListAsync();
        }

        // GET: api/WorkDayTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkDayTime>> GetWorkDayTime(int id)
        {
            var workDayTime = await _context.WorkDayTimeRecords.FindAsync(id);

            if (workDayTime == null)
            {
                return NotFound();
            }

            return workDayTime;
        }

        // PUT: api/WorkDayTimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkDayTime(int id, WorkDayTime workDayTime)
        {
            if (id != workDayTime.Id)
            {
                return BadRequest();
            }

            _context.Entry(workDayTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkDayTimeExists(id))
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

        // POST: api/WorkDayTimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkDayTime>> PostWorkDayTime(WorkDayTime workDayTime)
        {
            _context.WorkDayTimeRecords.Add(workDayTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkDayTime", new { id = workDayTime.Id }, workDayTime);
        }

        // DELETE: api/WorkDayTimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkDayTime(int id)
        {
            var workDayTime = await _context.WorkDayTimeRecords.FindAsync(id);
            if (workDayTime == null)
            {
                return NotFound();
            }

            _context.WorkDayTimeRecords.Remove(workDayTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkDayTimeExists(int id)
        {
            return _context.WorkDayTimeRecords.Any(e => e.Id == id);
        }
    }
}
