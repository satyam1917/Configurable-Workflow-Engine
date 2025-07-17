using ConfigurableWorkflowEngine.Models;

namespace ConfigurableWorkflowEngine.Storage
{
    public class InMemoryRepository
    {
        private static readonly Lazy<InMemoryRepository> _instance = new(() => new InMemoryRepository());
        public static InMemoryRepository Instance => _instance.Value;

        public List<WorkflowDefinition> Definitions { get; } = new();
        public List<WorkflowInstance> Instances { get; } = new();

        private InMemoryRepository() { }
    }
}
    