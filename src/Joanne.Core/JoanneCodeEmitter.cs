using System;
using System.Collections.Generic;
using System.Linq;

namespace Joanne.Core
{
    internal interface IJoanneCodeEmitable
    {
        string EmitCode(Class class_);
        IEnumerable<string> EmitCode(Class class_, Function function);
    }

    internal class JoanneCodeEmitter : IJoanneCodeEmitable
    {
        public string EmitCode(Class class_)
        {
            var funcString = class_.Functions
                .Select(function => EmitCode(class_, function))
                .Select(compiledLines =>
                    compiledLines.Aggregate(string.Empty, (current, line) => current + $"    {line}{Environment.NewLine}"))
                .Aggregate(string.Empty, (current, funcCode) => current + funcCode);

            var entryPoint = class_.Functions.SingleOrDefault(f =>
            {
                var decl = f.FunctionDeclaration;
                return decl.Name == "Main" && decl.IsStatic;
            });
            var entryPointCode = string.Empty;
            if(entryPoint != null)
            {
                entryPointCode = $"{class_.Name}.{entryPoint.FunctionDeclaration.Name}();";
            }

            return $"var Program = (function() {{ // @class{Environment.NewLine}" +
                   $"    function Program() {{{Environment.NewLine}" +
                   $"    }}{Environment.NewLine}" +
                   funcString +
                   $"    return Program;{Environment.NewLine}" +
                   $"}}());{Environment.NewLine}" +
                   $"{entryPointCode}{Environment.NewLine}";
        }

        public IEnumerable<string> EmitCode(Class class_, Function function)
        {
            var lines = new List<string>();
            var decl = function.FunctionDeclaration;
            lines.Add($"{class_.Name}.{decl.Name} = function() {{");
            lines.Add($"}};");
            return lines;
        }
    }
}