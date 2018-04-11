namespace Joanne.Core
{
    internal static partial class JoanneParser
    {
        internal static Function ParseFunction(ILexer lexer)
        {
            var functionDeclaration = ParseFuncitonDeclaration(lexer);
            return new Function(functionDeclaration);
        }
    }
}