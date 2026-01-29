namespace DataService.Domain.Entities;

public class ApplicationLock
{
    public string LockId { get; set; } = string.Empty;
    public long ApplicationId { get; set; }
    public int LockedBy { get; set; }
    public DateTime? LockedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}
