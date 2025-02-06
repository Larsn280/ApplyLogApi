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
    public async Task<ActionResult<ApplicationData>> PostApplication(ApplicationData applicationData)
    {

        try
        {
            if (applicationData == null)
            {
                return BadRequest("Invalid data.");
            }

            // Add the new ApplyLogEntry to the database
            _context.ApplicationDatas.Add(applicationData);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a response with the created log entry
            return CreatedAtAction(nameof(GetApplicationById), new { id = applicationData.Id }, applicationData);

        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ApplicationData>> FetchAllApplications()
    {

        try
        {
            var allApplications = await _context.ApplicationDatas.ToListAsync();
            return Ok(allApplications);
        }
        catch (Exception ex)
        {

            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationData>> GetApplicationById(int applicationId)
    {

        try
        {
            var application = await _context.ApplicationDatas.FindAsync(applicationId);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }
        catch (Exception ex)
        {

            return StatusCode(500, "Internal server errors: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveApplication(int applicationId)
    {

        try
        {
            var application = await _context.ApplicationDatas.FindAsync(applicationId);

            if (application == null)
            {
                return NotFound();
            }

            _context.ApplicationDatas.Remove(application);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }

    }
}