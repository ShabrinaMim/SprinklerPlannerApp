# Sprinkler Planner

Sprinkler Planner is a modular C# .NET 8.0 application that calculates optimal sprinkler placements in a 3D-defined room and connects each sprinkler to its nearest pipe segment. It is built using clean, industry-standard layered architecture with a strong focus on separation of concerns, dependency injection, and structured logging via Serilog.

Designed for extensibility and testability, the project includes full unit test coverage using xUnit, structured console and file logging, and CSV export functionality. Ideal for demonstrating geometry processing, service-oriented design, and clean .NET practices.


---

## Key Features

- 3D modeling of room geometry and pipe layouts using precise spatial representations
- Automated sprinkler placement with configurable minimum spacing (default: 2.5 meters)
- Accurate nearest pipe point computation using 3D vector projection on line segments
- Structured and extensible logging using Serilog (console + rolling file output)
- CSV export and formatted console output of all sprinkler-to-pipe connections
- Clean separation of concerns using layered architecture:
  - Domain (core logic and models)
  - Infrastructure (service and I/O implementations)
  - Application (runner and configuration)
  - Test (unit tests for core and infrastructure layers)
- Full dependency injection setup via `Microsoft.Extensions.DependencyInjection`
- Unit tested with `xUnit` to ensure correctness of placement, geometry, and data loading logic

---

## Technology Stack

- .NET 8.0 Console Application (C# 12)
- Dependency Injection using `Microsoft.Extensions.DependencyInjection`
- Structured logging with Serilog (console and file sinks)
- Logging abstraction via `ILogger<T>` for flexibility and testability
- CSV export using built-in file I/O operations
- Unit testing with xUnit for core and infrastructure layers

## Architecture

This project follows a **layered architecture** separating concerns into:

1. **Core Layer**: Domain models and interfaces
2. **Infrastructure Layer**: Implementations (seeding, output, planning logic)
3. **Application Layer**: Entry point, dependency injection, orchestration
4. **Test Layer**: Unit tests for each layer

---

## Project Structure

```
SprinklerPlannerApp.sln
│
├── SprinklerPlannerApp/                 # Main application entry point
│   ├── Config/
│   │   └── ServiceRegistration.cs       # DI + Serilog setup
│   ├── Resources/
│   │   ├── output.csv
│   │   └── pipe.csv
│   │   ├── room.csv
│   │   └── sprinkler_distribution.png
│   ├── Runner/
│   │   └── AppRunner.cs
│   ├── Visualization/
│   │   └── visualize.py
│   └── Program.cs
│
├── SprinklerPlannerApp.Core/
│   ├── Domain/
│   │   ├── Point3D.cs
│   │   ├── LineSegment.cs
│   │   └── Room.cs
│   └── Interfaces/
│       ├── IOutputPrinter.cs
│       ├── IPipeDataSeeder.cs
│       ├── IRoomDataSeeder.cs
│       └── ISprinklerPlannerService.cs
│
├── SprinklerPlannerApp.Infrastructure/
│   ├── Data/
│   │   ├── PipeDataSeeder.cs
│   │   └── RoomDataSeeder.cs
│   ├── Output/
│   │   └── ConsoleOutputPrinter.cs
│   └── Services/
│       └── SprinklerPlannerService.cs
│
├── SprinklerPlannerApp.Tests/
│   ├── Core/Domain/
│   │   ├── Point3DTests.cs
│   │   ├── LineSegmentTests.cs
│   │   └── RoomTests.cs
│   └── Infrastructure/
│       ├── Data/
│       │   ├── PipeDataSeederTests.cs
│       │   └── RoomDataSeederTests.cs
│       ├── Output/
│       │   └── ConsoleOutputPrinterTests.cs
│       └── Services/
│           └── SprinklerPlannerServiceTests.cs
│
├── .gitignore
├── README.md
└── LICENSE (optional)
```

---

## Logging with Serilog

Logging is implemented using Serilog with sinks for:

- Console (custom template)
- File output (`Logs/sprinklers.log`, daily rolling)

Configured in `ServiceRegistration.cs`:

```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}")
    .WriteTo.File("Logs/sprinklers.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```

Logs are injected using `ILogger<T>` and handled via DI.

---

## Dependency Injection

All services are registered in `ServiceRegistration.cs` using the Microsoft DI container:

- `ISprinklerPlannerService`
- `IOutputPrinter`
- `IRoomDataSeeder`
- `IPipeDataSeeder`
- Concrete `Room` and `List<LineSegment>` instances

---

## CSV Output

CSV file is generated at:
```
SprinklerPlannerApp/Resources/output.csv
```

Format:
```
SprinklerX,SprinklerY,SprinklerZ,PipeX,PipeY,PipeZ
90647.67,44000.00,2500.00,91425.36,45039.84,3093.60
...
```

---

## Visualization (Optional)

Sprinkler distribution can be visualized using the included Python script:

```bash
python3 SprinklerPlannerApp/Visualization/visualize.py
```

---

## How to Run

```bash
# Clone the repo
git clone https://github.com/ShabrinaMim/SprinklerPlannerApp.git
cd SprinklerPlannerApp

# Run the application
dotnet run --project SprinklerPlannerApp/SprinklerPlannerApp.csproj
```

---

## Running Tests

```bash
dotnet test
```

All domain and infrastructure components are tested with `xUnit`.

---

## License

This project is licensed under the MIT License.

---

## Author

**Shabrina Mim**  
GitHub: [https://github.com/ShabrinaMim/SprinklerPlannerApp.git](https://github.com/ShabrinaMim)