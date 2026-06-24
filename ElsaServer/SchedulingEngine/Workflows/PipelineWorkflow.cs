using Elsa.Workflows;
using Elsa.Workflows.Activities.Flowchart.Activities;
using Elsa.Workflows.Models;
using SchedulingEngine.BusinessEngine.Modules;
//using Elsa.Scheduling.Activities.Timer;
using SchedulingEngine.BusinessEngine.Models;
namespace ElsaServer.SchedulingEngine.Workflows
{
    public class PipelineWorkflow : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {
            builder.Name = "Scheduling RPP Pipeline";
            var timerTrigger = new Elsa.Scheduling.Activities.Timer(TimeSpan.FromHours(1))
            {
                CanStartWorkflow = true
            };

            var payloadList = builder.WithVariable<List<ModulePayload>>("payloadList", new List<ModulePayload>());

            var shiftBaselineGeneration = new ShiftBaselineGeneration()
            {
                Outputs = new Output<List<ModulePayload>>(payloadList)
            };

            var assetAllocation = new AssetAllocation()
            {
                Inputs = new Input<List<ModulePayload>>(payloadList),
                Outputs = new Output<List<ModulePayload>>(payloadList)
            };

            var shiftOutputBuilder = new ShiftOutputBuilder()
            {
                Inputs = new Input<List<ModulePayload>>(payloadList),
                Outputs = new Output<List<ModulePayload>>(payloadList)
            };

            var enrichmentA = new Enrichment()
            {
                Inputs = new Input<List<ModulePayload>>(payloadList),
                Outputs = new Output<List<ModulePayload>>(payloadList)
            };

            var enrichmentB = new Enrichment()
            {
                Inputs = new Input<List<ModulePayload>>(payloadList),
                Outputs = new Output<List<ModulePayload>>(payloadList)
            };

            builder.Root = new Flowchart
            {
                Activities =
            {
                timerTrigger,
                shiftBaselineGeneration,
                assetAllocation,
                enrichmentA,
                enrichmentB,
                shiftOutputBuilder
            },
                Connections =
            {
                new(timerTrigger, shiftBaselineGeneration),
                new(shiftBaselineGeneration, enrichmentA),
                new(enrichmentA, enrichmentB),
                new(enrichmentB, assetAllocation),
                new(assetAllocation, shiftOutputBuilder)
            }
            };
        }
    }
}
