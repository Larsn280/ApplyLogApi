public class ApplyLogEntry
{
    public int Id { get; set; }
    public string? AdSource { get; set; }
    public string? Company { get; set; }
    public string? AppliedJob { get; set; }
    public string? Location { get; set; }
    public string? Contact {get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Date { get; set; }
    public string? Reference { get; set; }
    public string? ApplyStatus { get; set; }
    public string? AdLink { get; set; }
    public string? CompanySite { get; set; }
    public string? Comments { get; set; }

    public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
}