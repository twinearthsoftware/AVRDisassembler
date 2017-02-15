using System;
using CommandLine;

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
                    File = options.InputFile
                };

                var disassembler = new Disassembler(disassemblerOptions);
                try
                {
                    foreach (var assemblyStatement in disassembler.Disassemble())
                        Console.WriteLine(assemblyStatement);

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