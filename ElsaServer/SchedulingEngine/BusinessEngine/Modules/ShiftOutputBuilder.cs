using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using SchedulingEngine.BusinessEngine.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingEngine.BusinessEngine.Modules;

[Activity(Type = "ShiftOutputBuilder", Category = "SchedulingEngine", Description = "Build final output.")]
public class ShiftOutputBuilder : CodeActivity
{
    [Input] public Input<List<ModulePayload>> Inputs { get; set; } = default!;
    [Output] public Output<List<ModulePayload>> Outputs { get; set; } = new();

    protected override ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var list = context.Get(Inputs) ?? new List<ModulePayload>();

        foreach (var item in list)
        {
            item.TestNumber += 5;
        }

        context.Set(Outputs, list);
        return ValueTask.CompletedTask;
    }
}



