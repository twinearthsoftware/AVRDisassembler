using CommandLine;

namespace AVRDisassembler
{
    public class CommandLineOptions
    {
        [Option('i', "input", Required = true, HelpText = "Input file (HEX) to be disassembled.")]
        public string InputFile { get; set; }

        [Option(Required = false, Default = false, HelpText = "Format output as JSON.")]
        public bool Json { get; set; }
    }
}
