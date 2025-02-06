using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/ApplyLogEntry")]
public class ApplyLogEntryController : ControllerBase
{
    private readonly AppDbContext _context;

    public ApplyLogEntryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<ApplyLogEntry>> PostApplyLogEntry(ApplyLogEntry logEntry)
    {
        if (logEntry == null)
        {
            return BadRequest("Invalid data.");
        }

        // Add the new ApplyLogEntry to the database
        _context.ApplyLogEntries.Add(logEntry);

        // Save the changes
        await _context.SaveChangesAsync();

        // Return a response with the created log entry
        return CreatedAtAction(nameof(GetApplyLogEntry), new { id = logEntry.Id }, logEntry);
    }
    
    [HttpGet]
    public async Task<ActionResult<ApplyLogEntry>> FetchAllLoggedEntries() {
        
        var allLoggedEntries = await _context.ApplyLogEntries.ToListAsync();

        return Ok(allLoggedEntries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplyLogEntry>> GetApplyLogEntry(int id)
    {
        var logEntry = await _context.ApplyLogEntries.FindAsync(id);

        if (logEntry == null)
        {
            return NotFound();
        }

        return logEntry;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveApplyLogEntry(int id) {

        var logEntry = await _context.ApplyLogEntries.FindAsync(id);

        if(logEntry == null) {
            return NotFound();
        }

        _context.ApplyLogEntries.Remove(logEntry);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}