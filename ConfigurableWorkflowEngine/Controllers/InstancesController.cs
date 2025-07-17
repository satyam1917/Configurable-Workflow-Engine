using ConfigurableWorkflowEngine.Models;
using ConfigurableWorkflowEngine.Storage;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurableWorkflowEngine.Controllers
{
    [ApiController]
    [Route("instances")]
    public class InstancesController : ControllerBase
    {
        private readonly InMemoryRepository _repo = InMemoryRepository.Instance;

        [HttpPost("/workflows/{defId}/instances")]
        public IActionResult StartInstance(string defId)
        {
            var def = _repo.Definitions.FirstOrDefault(d => d.Id == defId);
            if (def == null) return NotFound("Workflow definition not found.");

            var initialState = def.States.FirstOrDefault(s => s.IsInitial && s.Enabled);
            if (initialState == null)
                return BadRequest("No enabled initial state found.");

            var instance = new WorkflowInstance
            {
                Id = Guid.NewGuid().ToString(),
                WorkflowDefId = defId,
                CurrentStateId = initialState.Id,
                History = new List<HistoryEntry>()
            };
            _repo.Instances.Add(instance);
            return Created($"/instances/{instance.Id}", instance);
        }

        [HttpGet("{instanceId}")]
        public IActionResult GetInstance(string instanceId)
        {
            var inst = _repo.Instances.FirstOrDefault(i => i.Id == instanceId);
            if (inst == null) return NotFound();
            return Ok(inst);
        }

        [HttpGet]
        public IActionResult ListInstances() => Ok(_repo.Instances);

        public class ExecuteActionRequest
        {
            public string ActionId { get; set; } = default!;
        }

        [HttpPost("{instanceId}/actions")]
        public IActionResult ExecuteAction(string instanceId, [FromBody] ExecuteActionRequest req)
        {
            var inst = _repo.Instances.FirstOrDefault(i => i.Id == instanceId);
            if (inst == null) return NotFound("Instance not found.");

            var def = _repo.Definitions.FirstOrDefault(d => d.Id == inst.WorkflowDefId);
            if (def == null) return StatusCode(500, "Definition not found for this instance.");

            var currState = def.States.FirstOrDefault(s => s.Id == inst.CurrentStateId);
            if (currState == null || !currState.Enabled)
                return BadRequest("Current state is invalid or disabled.");

            if (currState.IsFinal)
                return BadRequest("Cannot execute action: instance is in a final state.");

            var action = def.Actions.FirstOrDefault(a => a.Id == req.ActionId);
            if (action == null)
                return BadRequest("Action does not exist in workflow definition.");

            if (!action.Enabled)
                return BadRequest("Action is disabled.");

            if (!action.FromStates.Contains(currState.Id))
                return BadRequest("Action is not valid from current state.");

            var toState = def.States.FirstOrDefault(s => s.Id == action.ToState && s.Enabled);
            if (toState == null)
                return BadRequest("Target state invalid or disabled.");

            inst.CurrentStateId = toState.Id;
            inst.History.Add(new HistoryEntry
            {
                ActionId = action.Id,
                Timestamp = DateTime.UtcNow
            });

            return Ok(inst);
        }
    }
}
