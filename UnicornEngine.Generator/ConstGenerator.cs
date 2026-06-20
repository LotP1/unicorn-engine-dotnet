using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;

namespace UnicornEngine.Generator;

[Generator]
public class SourceGeneratorWithAdditionalFiles : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.AdditionalTextsProvider
            .Where(f => Path.GetExtension(f.Path) == ".dat")
            .Collect();

        context.RegisterSourceOutput(provider, GenerateCode);
    }

    private void GenerateCode(SourceProductionContext context, ImmutableArray<AdditionalText> files)
    {
        foreach (AdditionalText file in files)
        {
            
            
            // Get the text of the file.
            string[] lines = file.GetText(context.CancellationToken)?.ToString().Split('\n');
            if (lines == null)
                continue;

            // var className = line.Trim();
            string fileName = Path.GetFileNameWithoutExtension(file.Path);

            string source = fileName is "Common" ? GenerateSource(lines) : GenerateCpuConst(lines);

            // Add the source code to the compilation.
            context.AddSource($"{fileName}.g.cs", source);
        }
    }

    private string GenerateCpuConst(Span<string> lines)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("namespace UnicornEngine.Const;");
        builder.AppendLine("");

        Dictionary<string, List<string>> enums = new();
        
        foreach (var line in lines[4..])
        {
            string[] segments = line.Split('_');
            
            if (segments.Length > 1)
            {
                if (segments[0] is "CPU")
                {
                    string archName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segments[1].ToLower());
                    string archTypeName = $"Cpu{archName}";
                    
                    enums.TryAdd(archTypeName, new List<string>());

                    string cpuName = archName;
                    
                    foreach (string segment in segments[2..])
                    {
                        cpuName += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums[archTypeName].Add(cpuName);
                }
                
                if (segments[1] is "REG")
                {
                    string archName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segments[0].ToLower());
                    string archTypeName = $"Reg{archName}";
                    
                    enums.TryAdd(archTypeName, new List<string>());
                    
                    string regName = "";
                    
                    foreach (string segment in segments[2..])
                    {
                        regName += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }

                    if (char.IsDigit(regName[0]))
                    {
                        regName = $"r{regName}";
                    }
                    
                    enums[archTypeName].Add(regName);
                }
                
                if (segments[1] is "INS")
                {
                    string archName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segments[0].ToLower());
                    string archTypeName = $"Instruction{archName}";
                    
                    enums.TryAdd(archTypeName, new List<string>());
                    
                    string insName = "";
                    
                    foreach (string segment in segments[2..])
                    {
                        insName += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums[archTypeName].Add(insName);
                }
            }
        }

        foreach (KeyValuePair<string, List<string>> e in enums)
        {
            builder.AppendLine($"public enum {e.Key}");
            builder.AppendLine("{");
            foreach (string line in e.Value)
            {
                builder.AppendLine($"    {line},");
            }
            builder.AppendLine("}");
            builder.AppendLine("");
        }
        
        return builder.ToString();
    }

    private string GenerateSource(Span<string> lines)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("namespace UnicornEngine.Const;");
        builder.AppendLine("");
        builder.AppendLine("public static class UcCommon");
        builder.AppendLine("{");
        
        Dictionary<string, List<string>> enums = new();
        
        foreach (var line in lines[1..])
        {
            string[] segments = line.Split('_');
            
            if (segments.Length > 1)
            {
                if (segments[0] is "ARCH")
                {
                    enums.TryAdd("UcArch", new List<string>());
                    
                    string archName = "";
                    
                    foreach (string segment in segments[1..])
                    {
                        archName += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcArch"].Add(archName);
                }
                else if (segments[0] is "MODE")
                {
                    enums.TryAdd("UcMode", new List<string>());
                    
                    string mode = "";
                    
                    foreach (string segment in segments[1..])
                    {
                        mode += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    if (char.IsDigit(mode[0]))
                    {
                        mode = $"X64_{mode}";
                    }
                    
                    enums["UcMode"].Add(mode);
                }
                else if (segments[0] is "ERR")
                {
                    enums.TryAdd("UcError", new List<string>());
                    
                    string error = "";
                    
                    foreach (string segment in segments[1..])
                    {
                        error += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcError"].Add(error);
                }
                else if (segments[0] is "PROT")
                {
                    enums.TryAdd("UcProtection", new List<string>());
                    
                    string prot = "";
                    
                    foreach (string segment in segments[1..])
                    {
                        prot += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcProtection"].Add(prot);
                }
                else if (segments[0] is "MEM")
                {
                    enums.TryAdd("UcMemoryType", new List<string>());
                    
                    string mem = "";
                    
                    foreach (string segment in segments[1..])
                    {
                        mem += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcMemoryType"].Add(mem);
                }
                else if (segments.Length > 2 && segments[0] is "TCG" && segments[1] is "OP" && segments[2] is not "FLAG")
                {
                    enums.TryAdd("UcTcgOpcode", new List<string>());
                    
                    string op = "";
                    
                    foreach (string segment in segments[2..])
                    {
                        op += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcTcgOpcode"].Add(op);
                }
                else if (segments.Length > 3 && segments[0] is "TCG" && segments[1] is "OP" && segments[2] is "FLAG")
                {
                    enums.TryAdd("UcTcgOpcodeFlag", new List<string>());
                    
                    string flag = "";
                    
                    foreach (string segment in segments[2..])
                    {
                        flag += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcTcgOpcodeFlag"].Add(flag);
                }
                else if (segments[0] is "HOOK")
                {
                    enums.TryAdd("UcHookType", new List<string>());
                    
                    string hook = "";
                    
                    foreach (string segment in segments[1..])
                    {
                        hook += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcHookType"].Add(hook);
                }
                else if (segments[0] is "QUERY")
                {
                    enums.TryAdd("UcQueryType", new List<string>());
                    
                    string query = "";
                    
                    foreach (string segment in segments[1..])
                    {
                        query += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcQueryType"].Add(query);
                }
                else if (segments.Length > 2 && segments[0] is "CTL" && segments[1] is "IO")
                {
                    enums.TryAdd("UcCtlIoType", new List<string>());
                    
                    string ctlIo = "";
                    
                    foreach (string segment in segments[2..])
                    {
                        ctlIo += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcCtlIoType"].Add(ctlIo);
                }
                else if (segments[0] is "TLB")
                {
                    enums.TryAdd("UcTlbType", new List<string>());
                    
                    string tlb = "";
                    
                    foreach (string segment in segments[1..])
                    {
                        tlb += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcTlbType"].Add(tlb);
                }
                else if (segments.Length > 2 && segments[0] is "CTL" && segments[1] is "CONTEXT" &&  !segments[2].Contains("MODE"))
                {
                    enums.TryAdd("UcControlContentType", new List<string>());
                    
                    string ctl = "";
                    
                    foreach (string segment in segments[1..])
                    {
                        ctl += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcControlContentType"].Add(ctl);
                }
                else if (segments[0] is "CTL")
                {
                    enums.TryAdd("UcControlType", new List<string>());
                    
                    string ctl = "";
                    
                    foreach (string segment in segments[1..])
                    {
                        ctl += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    }
                    
                    enums["UcControlType"].Add(ctl);
                }
                else
                {
                    string formattedLine = "";
                    
                    foreach (string segment in segments)
                    {
                        formattedLine += CultureInfo.InvariantCulture.TextInfo.ToTitleCase(segment.ToLower());
                    
                    }
                
                    builder.AppendLine($"    const uint {formattedLine};");
                }
            }
        }
        
        builder.AppendLine("}");
        builder.AppendLine("");
        
        foreach (KeyValuePair<string, List<string>> e in enums)
        {
            if (e.Key.Contains("UcMode") || e.Key.Contains("UcProtection") || e.Key.Contains("UcHookType") || e.Key.Contains("UcCtlIoType") || e.Key.Contains("UcControlContentType"))
            {
                builder.AppendLine("[Flags]");
            }
            
            builder.AppendLine($"public enum {e.Key} : uint");
            builder.AppendLine("{");
            foreach (string line in e.Value)
            {
                builder.AppendLine($"    {line},");
            }
            builder.AppendLine("}");
            builder.AppendLine("");
        }
        
        return builder.ToString();
    }
}