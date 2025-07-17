namespace ConfigurableWorkflowEngine.Models
{
    public class HistoryEntry
    {
        public string ActionId { get; set; } = default!;
        public DateTime Timestamp { get; set; }
    }

    public class WorkflowInstance
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string WorkflowDefId { get; set; } = default!;
        public string CurrentStateId { get; set; } = default!;
        public List<HistoryEntry> History { get; set; } = new();
    }
}
