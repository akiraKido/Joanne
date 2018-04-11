namespace Joanne.Core
{
    internal static partial class JoanneParser
    {
        internal static FunctionDeclaration ParseFuncitonDeclaration(ILexer lexer)
        {
            Accesibility accesibility = Accesibility.None;
            bool isStatic = false;
            string returnType = null;
            string name = null;

            while(lexer.Token.TokenType != TokenType.L_Paren)
            {
                switch(lexer.Token.TokenType)
                {
                    case TokenType.Public:
                    case TokenType.Private:
                        if(accesibility != Accesibility.None)
                        {
                            // TODO
                        }
                        accesibility = lexer.Token.TokenType.ToAccesibility();
                        break;
                    case TokenType.Static:
                        if(isStatic == true)
                        {
                            // TODO
                        }
                        isStatic = true;
                        break;
                    case TokenType.Void:
                        if(returnType != null)
                        {
                            // TODO
                        }
                        returnType = "void";
                        break;
                    default:
                        if(name != null)
                        {
                            // TODO
                        }
                        name = lexer.Token.Value;
                        break;
                }

                lexer = lexer.Next;
            }

            lexer = lexer.Next; // eat L_Paren
            // TODO
            lexer = lexer.Next; // eat R_Paren

            return new FunctionDeclaration(accesibility, isStatic, name);
        }
    }
}