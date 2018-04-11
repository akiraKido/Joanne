using System;

namespace Joanne.Core
{
    public static class JoanneCompiler
    {
        public static string Compile(string code)
        {
            ILexer lexer = new JoanneLexer(new StringSrounce(code));
            IJoanneCodeEmitable emitter = new JoanneCodeEmitter();

            while(lexer.Token.TokenType != TokenType.EndOfFile)
            {
                switch(lexer.Token.TokenType)
                {
                    case TokenType.Class:
                        return emitter.EmitCode(JoanneParser.ParseClass(lexer));
                    default:
                        throw new NotImplementedException();
                }
            }
            
            throw new NotImplementedException();
        }
    }
}
