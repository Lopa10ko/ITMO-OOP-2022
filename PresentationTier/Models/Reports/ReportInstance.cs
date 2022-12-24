namespace PresentationTier.Models.Reports;

public record ReportInstance(Guid Id, Guid EmployeeId, DateTime FormedDate, string FullText);