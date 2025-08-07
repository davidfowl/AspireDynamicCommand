#pragma warning disable ASPIREINTERACTION001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

string[] projectArgs = [];

builder.AddProject<Projects.MyApp>("myapp")
       .WithExplicitStart()
       .WithArgs(context =>
       {
           context.Args.Clear();
           foreach (var arg in projectArgs)
           {
               context.Args.Add(arg);
           }
       })
       .WithCommand("run", "Run with args", async context =>
       {
           var commandService = context.ServiceProvider.GetRequiredService<ResourceCommandService>();
           var interactionService = context.ServiceProvider.GetRequiredService<IInteractionService>();
           var rns = context.ServiceProvider.GetRequiredService<ResourceNotificationService>();

           var result = await interactionService.PromptInputAsync("Enter the arguments",
            "Enter the arguments for the command to run",
            new()
            {
                InputType = InputType.Text,
                Label = "Arguments",
                Placeholder = "arg1 arg2 arg3",
            });

           if (result.Canceled)
           {
               return CommandResults.Success();
           }

           // This parsing could be improved to handle quoted arguments, etc.

           // Set the project arguments based on user input
           // This will be used on the next run
           projectArgs = result.Data.Value?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? [];

           if (rns.TryGetCurrentState(context.ResourceName, out var state)
                && state.Snapshot.State?.Text == KnownResourceStates.NotStarted)
           {
              return await commandService.ExecuteCommandAsync(context.ResourceName, "resource-start");
           }

           return await commandService.ExecuteCommandAsync(context.ResourceName, "resource-restart");
       },
       new()
       {
           IconName = "Play",
           IconVariant = IconVariant.Regular,
           IsHighlighted = true
       });

builder.Build().Run();

#pragma warning restore ASPIREINTERACTION001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
