using System.Collections.Generic;

namespace Joanne.Core
{
    internal static partial class JoanneParser
    {
        internal static Class ParseClass(ILexer lexer)
        {
            if(lexer.Token.TokenType != TokenType.Class)
            {
                // TODO
            }
            lexer = lexer.Next; // eat class
            if(lexer.Token.TokenType != TokenType.Identifier)
            {
                // TODO
            }
            var name = lexer.Token.Value;
            lexer = lexer.Next;

            // TODO Inheritence

            if(lexer.Token.TokenType != TokenType.L_Brace)
            {
                // TODO
            }

            lexer = lexer.Next;

            var functions = new List<Function> { ParseFunction(lexer) };

            var class_ = new Class(name, functions);
            return class_;
        }
    }
}