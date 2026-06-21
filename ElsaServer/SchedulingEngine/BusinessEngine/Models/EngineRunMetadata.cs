namespace SchedulingEngine.BusinessEngine.Models
{
    public class EngineRunMetadata
    {
        public Guid RunId { get; init; } = Guid.NewGuid();
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public string? TriggeredBy { get; init; }
        public string? Notes { get; init; }


        public EngineRunMetadata(string? triggeredBy = null, string? notes = null)
        {
            TriggeredBy = triggeredBy;
            Notes = notes;
        }
    }
}
