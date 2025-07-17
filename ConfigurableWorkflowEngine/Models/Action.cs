﻿namespace ConfigurableWorkflowEngine.Models
{
    public class Action
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool Enabled { get; set; }
        public List<string> FromStates { get; set; } = new();
        public string ToState { get; set; } = default!;
    }
}
