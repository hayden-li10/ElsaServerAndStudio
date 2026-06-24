using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Activities.Flowchart.Activities;
using Elsa.Workflows.Activities.Flowchart.Models;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime.Activities;
using SchedulingEngine.BusinessEngine.Models;
using SchedulingEngine.BusinessEngine.Modules;

namespace ElsaServer.SchedulingEngine.Workflows
{
    public class ParentWorkflow : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {
            builder.Name = "Parent Workflow";
            builder.Id = "Parent Workflow";

            var childOutput = builder.WithVariable<IDictionary<string, object>>();
            var fullPayloadList = builder.WithVariable<List<ModulePayload>>("fullPayloadList", new List<ModulePayload>());

            var start = new WriteLine("Starting Parent Workflow");

            var dispatchWorkflow1 = new DispatchWorkflow
            {
                WorkflowDefinitionId = new("ChildWorkflow"),
                Input = new(new Dictionary<string, object>
                {
                    ["ParentMessage"] = new List<ModulePayload>(){
                        new ModulePayload { TestNumber = 1},
                        new ModulePayload { TestNumber = 2},
                        new ModulePayload { TestNumber = 3},
                    }
                }),
                WaitForCompletion = new(true),
                Result = new(childOutput)
            };

            var dispatchWorkflow2 = new DispatchWorkflow
            {
                WorkflowDefinitionId = new("ChildWorkflowDelay"),
                Input = new(new Dictionary<string, object>
                {
                    ["ParentMessage"] = new List<ModulePayload>(){
                        new ModulePayload { TestNumber = 100},
                        new ModulePayload { TestNumber = 200},
                        new ModulePayload { TestNumber = 300},
                    }
                }),
                WaitForCompletion = new(true),
                Result = new(childOutput)
            };

            var joinParallel = new FlowJoin { Mode = new(FlowJoinMode.WaitAll) };
            var logEnd = new WriteLine("Parent Workflow completed");

            builder.Root = new Flowchart
            {
                    Activities =
                {
                    start,
                    dispatchWorkflow1,
                    dispatchWorkflow2,
                    joinParallel,
                    logEnd
                },
                    Connections =
                {
                    // True Fan-Out (Parallel)
                    new(start, dispatchWorkflow1),
                    new(start, dispatchWorkflow2),
                    
                    // True Fan-In. Both connect to the FlowJoin block.
                    new(dispatchWorkflow1, joinParallel),
                    new(dispatchWorkflow2, joinParallel),
                    
                    // Finish the workflow
                    new(joinParallel, logEnd)
                }
            };
        }
    }
}
