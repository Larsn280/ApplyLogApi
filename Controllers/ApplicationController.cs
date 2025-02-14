using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("applications")]
public class ApplicationController : ControllerBase
{
    private readonly IDynamoDBContext _dynamoDbContext;

    public ApplicationController(IDynamoDBContext context)
    {
        _dynamoDbContext = context;
    }

    // ✅ Create Application
    [HttpPost]
    public async Task<ActionResult<ApplicationData>> CreateApplication([FromBody] ApplicationData applicationData)
    {
        try
        {
            if (applicationData == null)
            {
                return BadRequest("Invalid data.");
            }

            // Set Partition Key & Unique Sort Key
            applicationData.PK = "APPLICATION";
            applicationData.SK = $"APPLICATION#{Guid.NewGuid()}";

            await _dynamoDbContext.SaveAsync(applicationData);
            return CreatedAtAction(nameof(GetApplicationById), new { sk = applicationData.SK }, applicationData);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // ✅ Update Application
    [HttpPut("{sk}")]
    public async Task<ActionResult<ApplicationData>> UpdateApplication(string sk, [FromBody] ApplicationData applicationData)
    {
        try
        {
            if (applicationData == null)
            {
                return BadRequest("Invalid data.");
            }

            // Fetch existing record
            var existingApplication = await _dynamoDbContext.LoadAsync<ApplicationData>("APPLICATION", sk);
            if (existingApplication == null)
            {
                return NotFound($"Application with SK {sk} not found.");
            }

            // Update fields
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

            await _dynamoDbContext.SaveAsync(existingApplication);
            return Ok(existingApplication);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // ✅ Fetch All Applications
    [HttpGet]
    public async Task<ActionResult<List<ApplicationData>>> FetchAllApplications()
    {
        try
        {
            var scanConditions = new List<ScanCondition>(); // Empty scan gets all items
            var allApplications = await _dynamoDbContext.ScanAsync<ApplicationData>(scanConditions).GetRemainingAsync();
            return Ok(allApplications);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // ✅ Get Single Application by SK
    [HttpGet("{sk}")]
    public async Task<ActionResult<ApplicationData>> GetApplicationById(string sk)
    {
        try
        {
            var application = await _dynamoDbContext.LoadAsync<ApplicationData>("APPLICATION", sk);
            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // ✅ Delete Application
    [HttpDelete("{sk}")]
    public async Task<ActionResult> RemoveApplication(string sk)
    {
        try
        {
            var application = await _dynamoDbContext.LoadAsync<ApplicationData>("APPLICATION", sk);
            if (application == null)
            {
                return NotFound();
            }

            await _dynamoDbContext.DeleteAsync<ApplicationData>("APPLICATION", sk);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
