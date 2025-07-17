using ConfigurableWorkflowEngine.Models;
using ConfigurableWorkflowEngine.Storage;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurableWorkflowEngine.Controllers
{
    [ApiController]
    [Route("workflows")]
    public class WorkflowsController : ControllerBase
    {
        private readonly InMemoryRepository _repo = InMemoryRepository.Instance;

        [HttpPost]
        public IActionResult CreateWorkflow([FromBody] WorkflowDefinition def)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(def.Id) || string.IsNullOrWhiteSpace(def.Name))
                return BadRequest("Workflow must have an id and name.");

            if (_repo.Definitions.Any(d => d.Id == def.Id))
                return BadRequest("Duplicate workflow id.");

            var initialStates = def.States.Where(s => s.IsInitial).ToList();
            if (initialStates.Count != 1)
                return BadRequest("Workflow must have exactly one initial state.");

            if (def.States.GroupBy(s => s.Id).Any(g => g.Count() > 1))
                return BadRequest("Duplicate state id detected.");

            if (def.Actions.GroupBy(a => a.Id).Any(g => g.Count() > 1))
                return BadRequest("Duplicate action id detected.");

            if (def.Actions.Any(a => !def.States.Any(s => s.Id == a.ToState)))
                return BadRequest("Action references unknown toState.");

            if (def.Actions.Any(a => a.FromStates.Any(fid => !def.States.Any(s => s.Id == fid))))
                return BadRequest("Action references unknown fromStates.");

            _repo.Definitions.Add(def);
            return Created($"/workflows/{def.Id}", def);
        }

        [HttpGet("{id}")]
        public IActionResult GetWorkflow(string id)
        {
            var wf = _repo.Definitions.FirstOrDefault(d => d.Id == id);
            if (wf == null) return NotFound();
            return Ok(wf);
        }

        [HttpGet]
        public IActionResult ListWorkflows() => Ok(_repo.Definitions);
    }
}
