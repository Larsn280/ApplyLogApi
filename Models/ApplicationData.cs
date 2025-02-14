using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("Applications")]
public class ApplicationData
{
    [DynamoDBHashKey] // Primary Key (Partition Key)
    public string PK { get; set; } = "APPLICATION"; // Static prefix for grouping

    [DynamoDBRangeKey] // Sort Key (SK)
    public string? SK { get; set; }

    [DynamoDBProperty]
    public string? AdSource { get; set; }

    [DynamoDBProperty]
    public string? Company { get; set; }

    [DynamoDBProperty]
    public string? AppliedJob { get; set; }

    [DynamoDBProperty]
    public string? Location { get; set; }

    [DynamoDBProperty]
    public string? Contact { get; set; }

    [DynamoDBProperty]
    public string? Phone { get; set; }

    [DynamoDBProperty]
    public string? Email { get; set; }

    [DynamoDBProperty]
    public string? Date { get; set; }

    [DynamoDBProperty]
    public string? Reference { get; set; }

    [DynamoDBProperty]
    public string? ApplyStatus { get; set; }

    [DynamoDBProperty]
    public string? AdLink { get; set; }

    [DynamoDBProperty]
    public string? CompanySite { get; set; }

    [DynamoDBProperty]
    public string? Comments { get; set; }

    [DynamoDBProperty]
    public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
}
