using Microsoft.Extensions.Logging;
using SprinklerPlannerApp.Core.Domain;
using SprinklerPlannerApp.Core.Interfaces;
using System.Collections.Generic;

namespace SprinklerPlannerApp.Runner
{
    public class AppRunner
    {
        private readonly ISprinklerPlannerService _plannerService;
        private readonly IOutputPrinter _outputPrinter;
        private readonly ILogger<AppRunner> _logger;

        public AppRunner(
            ISprinklerPlannerService plannerService,
            IOutputPrinter outputPrinter,
            ILogger<AppRunner> logger)
        {
            _plannerService = plannerService;
            _outputPrinter = outputPrinter;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation("Sprinkler Planner App Started");
            List<(Point3D Sprinkler, Point3D ClosestPipePoint)> results = _plannerService.PlanSprinklers();
            _outputPrinter.PrintSprinklerResults(results);
            _outputPrinter.ExportToCsv(results, "Resources/output.csv");
            _logger.LogInformation("Sprinkler planning complete. Results exported.");
        }
    }
}
