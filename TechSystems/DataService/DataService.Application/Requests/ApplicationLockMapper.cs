namespace DataService.Application.Requests
{
    public class CreateApplicationLockRequest
    {
        public string LockId { get; set; }
        public long ApplicationId { get; set; }
        public int LockedBy { get; set; }
        public DateTime? LockedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}