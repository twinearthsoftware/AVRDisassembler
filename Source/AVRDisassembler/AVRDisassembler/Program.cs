using System;
using System.CommandLine;

namespace AVRDisassembler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fileArg = string.Empty;

            ArgumentSyntax.Parse(args, syntax =>
            {
                Func<Argument<string>,string> formatErrorMessage = 
                    (x) => $"Mandatory option '{x.Help}' ({string.Join("|", x.Names)}) not specified!";

                var fileOption = syntax.DefineOption("f|file", ref fileArg, true, "File to parse");

                if (fileOption.IsRequired && !fileOption.IsSpecified)
                    syntax.ReportError(formatErrorMessage(fileOption));
            });

            var options = new DisassemblerOptions
            {
                File = fileArg
            };
            var disassembler = new Disassembler(options);
            try
            {
                foreach (var assemblyStatement in disassembler.Disassemble())
                    Console.WriteLine(assemblyStatement);

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error occurred:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
        }
    }
}