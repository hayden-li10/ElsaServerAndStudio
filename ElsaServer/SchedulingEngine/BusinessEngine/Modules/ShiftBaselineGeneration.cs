using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using SchedulingEngine.BusinessEngine.Models;

namespace SchedulingEngine.BusinessEngine.Modules;

[Activity(Type = "ShiftBaselineGeneration", Category = "SchedulingEngine", Description = "Generate shift baseline.")]
public class ShiftBaselineGeneration : CodeActivity
{
    [Output]
    public Output<List<ModulePayload>> Outputs { get; set; } = new();

    protected override ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var result = new List<ModulePayload>
        {
            new ModulePayload { TestNumber = 1},
            new ModulePayload { TestNumber = 2},
            new ModulePayload { TestNumber = 3},
        };
        context.Set(Outputs, result);
        return ValueTask.CompletedTask;
    }
}



