using System;

namespace Joanne.Core
{
    internal static class AccessibilityExtensions
    {
        internal static Accesibility ToAccesibility(this TokenType tokenType)
        {
            switch(tokenType)
            {
                case TokenType.Public:
                    return Accesibility.Public;
                case TokenType.Private:
                    return Accesibility.Private;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}