# Dynamic Command Sample

This sample demonstrates how to create custom commands in .NET Aspire that can dynamically pass arguments to resources using the new **Interaction Service** and **Resource Command Service** features introduced in .NET Aspire 9.4.


https://github.com/user-attachments/assets/411bf5df-304d-429c-b08c-30a209ee18d4


## Scenario

This addresses the common need to:

- Run console applications with different command-line arguments from the Aspire dashboard
- Prompt users for input before starting a resource
- Dynamically configure resources based on user interaction

Common use cases include:

- Database migrations with different parameters
- Test runners with different filters
- Data seeding with various dataset sizes

## How it Works

The sample includes:

1. **MyApp** - A simple console application that prints its command-line arguments
2. **AppHost** - Configures the MyApp resource with:
   - `WithExplicitStart()` - Prevents automatic startup
   - `WithArgs()` - Dynamically sets arguments based on user input
   - `WithCommand()` - Creates a custom "Run with args" command

When you click the "Run with args" command in the Aspire dashboard:

1. A prompt appears asking for command-line arguments
2. The arguments are parsed and stored
3. The resource is started (or restarted) with the new arguments

## Running the Sample

1. Run `aspire run`
2. Open the Aspire dashboard
3. Find the "myapp" resource and click the "Run with args" command (play icon)
4. Enter some arguments in the prompt (e.g., `--migrate --verbose`)
5. The console app will start and display the provided arguments

## Related Discussion

This sample was created in response to [dotnet/aspire#8502](https://github.com/dotnet/aspire/discussions/8502), which explored various approaches for dynamic resource configuration before the official Interaction Service was available.
