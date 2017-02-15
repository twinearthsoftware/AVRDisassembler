using System;
using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AVRDisassembler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
            .WithParsed(options =>
            {
                var disassemblerOptions = new DisassemblerOptions
                {
                    File = options.InputFile,
                    JsonOutput = options.Json
                };

                var disassembler = new Disassembler(disassemblerOptions);
                try
                {
                    var disassembledStmts = disassembler.Disassemble();

                    if (!options.Json)
                    {
                        foreach (var assemblyStatement in disassembledStmts)
                            Console.WriteLine(assemblyStatement);
                    }
                    else
                    {
                        var serializerSettings = new JsonSerializerSettings()
                        {
                            Formatting = Formatting.Indented
                        };
                        serializerSettings.Converters.Add(new StringEnumConverter());
                        var json = JsonConvert.SerializeObject(disassembledStmts, serializerSettings);
                        Console.WriteLine(json);
                    }
                    Environment.Exit((int) ExitCode.Success);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The following error occurred:");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Environment.Exit((int) ExitCode.GeneralFailure);
                }
            })
            .WithNotParsed(options => 
                Environment.Exit((int) ExitCode.FailedToParseCommandLineArgs));
        }
    }
}