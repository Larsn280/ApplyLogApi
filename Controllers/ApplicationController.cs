using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("Application")]
public class ApplicationController : ControllerBase
{
    private readonly AppDbContext _context;

    public ApplicationController(AppDbContext context)
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

            return StatusCode(201);

        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<ApplicationData>> UpdateApplication(ApplicationData applicationData)
    {
        try
        {
            if (applicationData == null)
            {
                return BadRequest("Invalid data or mismatched ID.");
            }

            // Check if the application exists
            var existingApplication = await _context.ApplicationDatas.FindAsync(applicationData.Id);
            if (existingApplication == null)
            {
                return NotFound($"Application with ID {applicationData.Id} not found.");
            }

            // Update the application data
            existingApplication.AdSource = applicationData.AdSource;
            existingApplication.Company = applicationData.Company;
            existingApplication.AppliedJob = applicationData.AppliedJob;
            existingApplication.Location = applicationData.Location;
            existingApplication.Contact = applicationData.Contact;
            existingApplication.Phone = applicationData.Phone;
            existingApplication.Email = applicationData.Email;
            existingApplication.Date = applicationData.Date;
            existingApplication.Reference = applicationData.Reference;
            existingApplication.ApplyStatus = applicationData.ApplyStatus;
            existingApplication.AdLink = applicationData.AdLink;
            existingApplication.CompanySite = applicationData.CompanySite;
            existingApplication.Comments = applicationData.Comments;

            // Save the changes
            await _context.SaveChangesAsync();

            // Return the updated application data
            return Ok(existingApplication); // Return updated application data
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

    [HttpGet("{applicationId}")]
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

    [HttpDelete("{applicationId}")]
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