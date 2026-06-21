using Elsa.Workflows;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using SchedulingEngine.BusinessEngine.Models;

namespace SchedulingEngine.BusinessEngine.Modules;

[Activity(Type = "Enrichment", Category = "SchedulingEngine", Description = "Enrich data.")]
public class Enrichment : CodeActivity
{
    [Input] public Input<List<ModulePayload>> Inputs { get; set; } = default!;
    [Output] public Output<List<ModulePayload>> Outputs { get; set; } = new();

    protected override ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var list = context.Get(Inputs) ?? new List<ModulePayload>();

        foreach (var item in list)
        {
            item.TestNumber += 4;
        }

        context.Set(Outputs, list);
        return ValueTask.CompletedTask;
    }
}

