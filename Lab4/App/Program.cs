using Labs;
using McMaster.Extensions.CommandLineUtils;

namespace App;


internal class Program
{
    private static readonly Dictionary<string, Func<string, string>> _solveMethods = new()
    {
        ["lab1"] = Lab1.Solve,
        ["lab2"] = Lab2.Solve,
        ["lab3"] = Lab3.Solve
    };


    private static int Main(string[] args)
    {
        var app = new CommandLineApplication
        {
            UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.Throw
        };

        app.Command("version", versionCmd =>
        {
            versionCmd.OnExecute(() =>
            {
                Console.WriteLine("Author: Vadym Bahrii");
                Console.WriteLine("Version: 1.0");
                return 0;
            });
        });


        app.Command("run", runCmd =>
        {
            runCmd.OnExecute(() =>
            {
                Console.WriteLine("Specify a subcommand: lab1, lab2, or lab3");
                runCmd.ShowHelp();
                return 1;
            });

            foreach (var labCommandName in _solveMethods.Keys)
            {
                runCmd.Command(labCommandName, labCmd =>
                {
                    var inputOption = labCmd.Option(
                        "-i|--input",
                        "Input file",
                        CommandOptionType.SingleValue
                    );
                    var outputOption = labCmd.Option(
                        "-o|--output",
                        "Output file",
                        CommandOptionType.SingleValue
                    );

                    labCmd.OnExecute(() =>
                    {
                        var inputPath = GetInputPath(inputOption.Value());
                        var outputPath = GetOutputPath(outputOption.Value());

                        Console.WriteLine(
                            $"{labCommandName} is running with input file: {inputPath}"
                        );
                        Console.WriteLine($"and output file: {outputPath}");

                        string text;
                        try
                        {
                            text = File.ReadAllText(inputPath);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine($"File reading error: {exception.Message}");
                            return 1;
                        }

                        string result;
                        try
                        {
                            result = _solveMethods[labCommandName](text);
                        }
                        catch (Exception exception)
                        {
                            result = exception.Message;
                        }

                        try
                        {
                            File.WriteAllLines(outputPath, [result]);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine($"File writing error: {exception.Message}");
                            return 1;
                        }

                        return 0;
                    });
                });
            }
        });

        app.Command("set-path", setPathCmd =>
        {
            var pathOption = setPathCmd.Option(
                "-p|--path",
                "Path to input and output files",
                CommandOptionType.SingleValue
            )
                .IsRequired();

            setPathCmd.OnExecute(() =>
            {
                SetLabPath(pathOption.Value()!);
                return 0;
            });
        });


        app.OnExecute(() =>
        {
            Console.WriteLine("Invalid command. Use 'version', 'run', or 'set-path'");
            app.ShowHelp();
            return 1;
        });


        try
        {
            return app.Execute(args);
        }
        catch (CommandParsingException exception)
        {
            Console.WriteLine(exception.Message);
            app.ShowHelp();
        }
        catch
        {
            app.ShowHelp();
        }
        return 1;
    }


    private static string GetInputPath(string? userInputPath)
    {
        if (userInputPath is not null)
        {
            return userInputPath;
        }

        var directoryPath =
            Environment.GetEnvironmentVariable("LAB_PATH")
            ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        return Path.Join(directoryPath, "INPUT.TXT");
    }


    private static string GetOutputPath(string? userOutputPath)
    {
        if (userOutputPath is not null)
        {
            return userOutputPath;
        }

        var directoryPath =
            Environment.GetEnvironmentVariable("LAB_PATH")
            ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        return Path.Join(directoryPath, "OUTPUT.TXT");
    }


    private static void SetLabPath(string labPath)
    {
        Environment.SetEnvironmentVariable(
            "LAB_PATH",
            labPath,
            EnvironmentVariableTarget.User
        );
        Console.WriteLine($"LAB_PATH set to: {labPath}");
    }
}