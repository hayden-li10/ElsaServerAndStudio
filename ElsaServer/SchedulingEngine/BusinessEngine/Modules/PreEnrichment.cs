using Elsa.Workflows;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using SchedulingEngine.BusinessEngine.Models;

namespace SchedulingEngine.BusinessEngine.Modules;

[Activity(Type = "PreEnrichment", Category = "SchedulingEngine", Description = "Add 2 to each number.")]
public class PreEnrichment : CodeActivity
{
    [Input] public Input<List<ModulePayload>> Inputs { get; set; } = default!;
    [Output] public Output<List<ModulePayload>> Outputs { get; set; } = new();

    protected override ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var list = context.Get(Inputs) ?? new List<ModulePayload>();

        foreach (var item in list)
        {
            item.TestNumber += 2;
        }

        context.Set(Outputs, list);
        return ValueTask.CompletedTask;
    }
}
