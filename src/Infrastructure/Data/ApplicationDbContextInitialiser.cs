using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();
    }

    public static async Task SeedDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary

        if (!_context.Snippets.Any())
        {
            // tags
            var tags = new List<Tag>
            {
                new Tag { Name = "c#" }, 
                new Tag { Name = "java" }, 
                new Tag { Name = "html" },
                new Tag { Name = "go" }
            };

            await _context.Tags.AddRangeAsync(tags);

            await _context.SaveChangesAsync();

            // Snippets to add
            var snippets = new List<Snippet>
            {
                new()
                {
                    Code = "Console.Writeline(\"Hello world\")",
                    Description = "Print new line from console",
                    Docs = "Use this in C# console app",
                    IsPinned = false,
                    Language = "C#",
                    Title = "Writeline",
                    Tags = new Tag[] { tags[0], tags[1], tags[2] }
                },
                new()
                {
                    Code = "System.out.println(\"Hello world\")",
                    Description = "Prints a new line in the console",
                    Docs = "Use it in a Java console application",
                    IsPinned = false,
                    Language = "Java",
                    Title = "println",
                    Tags = new Tag[] { tags[0], tags[1], tags[2] }
                },
                new()
                {
                    Code = "print(\"Hello world\")",
                    Description = "Prints a new line in the console",
                    Docs = "Use it in a Python console application",
                    IsPinned = false,
                    Language = "Python",
                    Title = "print",
                    Tags = new Tag[] { tags[3] }
                },
                new()
                {
                    Code = "fmt.Println(\"Hello world\")",
                    Description = "Prints a new line in the console",
                    Docs = "Use it in a Go console application",
                    IsPinned = false,
                    Language = "Go",
                    Title = "Println",
                    Tags = new Tag[] { tags[3] }
                },
                new()
                {
                    Code = "console.log(\"Hello world\")",
                    Description = "Prints a new line in the console",
                    Docs = "Use it in a JavaScript console application",
                    IsPinned = false,
                    Language = "JavaScript",
                    Title = "log",
                    Tags = new Tag[] { tags[0] }
                },
                new()
                {
                    Code = "fmt.Println(\"Hello world\")",
                    Description = "Prints a new line in the console",
                    Docs = "Use it in a Go console application",
                    IsPinned = false,
                    Language = "Go",
                    Title = "Println",
                    Tags = new Tag[] { tags[0], tags[1], tags[2] }
                }
            };

            await _context.Snippets.AddRangeAsync(snippets);

            await _context.SaveChangesAsync();
        }
    }
}
