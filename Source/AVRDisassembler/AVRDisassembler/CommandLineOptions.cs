using CommandLine;

namespace AVRDisassembler
{
    public class CommandLineOptions
    {
        [Option('i', "input", Required = true, HelpText = "Input file (HEX) to be disassembled.")]
        public string InputFile { get; set; }
    }
}
