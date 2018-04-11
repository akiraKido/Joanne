using System;
using System.Collections.Generic;

namespace Joanne.Core
{
    internal interface ISource
    {
        int GetCountWhere(int startPos, Func<char, bool> predicate);
        char this[int index] { get; }
        int Length { get; }
        string Substring(int startPos, int length);
    }

    internal class StringSrounce : ISource
    {
        private readonly string _code;

        public StringSrounce(string code)
        {
            _code = code ?? throw new ArgumentNullException(nameof(code));
        }

        public int GetCountWhere(int startPos, Func<char, bool> predicate)
        {
            int offset = startPos;
            int cnt = 0;
            while(offset < _code.Length && predicate(_code[offset]))
            {
                offset++;
                cnt++;
            }
            return cnt;
        }

        public char this[int index] => _code[index];

        public int Length => _code.Length;

        public string Substring(int startPos, int length) => _code.Substring(startPos, length);
    }

    internal enum TokenType
    {
        None,
        EndOfFile,

        Class,
        Public,
        Private,
        Static,
        Void,

        Identifier,
        L_Brace,
        R_Brace,
        L_Paren,
        R_Paren
    }

    internal struct Token
    {
        public Token(TokenType tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }

        internal TokenType TokenType { get; }
        internal string Value { get; }

        public static bool operator ==(Token a, Token b) => a.Equals(b);

        public static bool operator !=(Token a, Token b) => !(a == b);

        public override bool Equals(object obj) => obj is Token token && Equals(token);

        public bool Equals(Token other) => TokenType == other.TokenType
                                           && Value == other.Value;

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)TokenType * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }
    }
    internal interface ILexer
    {
        Token Token { get; }
        ILexer Next { get; }
    }

    internal struct JoanneLexer : ILexer
    {
        private static readonly IReadOnlyDictionary<char, Token> SingleCharTokens = new Dictionary<char, Token>()
        {
            { '{', new Token(TokenType.L_Brace, "{") },
            { '}', new Token(TokenType.R_Brace, "}") },
            { '(', new Token(TokenType.L_Paren, "(") },
            { ')', new Token(TokenType.R_Paren, ")") },
        };

        private static readonly IReadOnlyDictionary<string, Token> PredefinedIdentifiers = new Dictionary<string, Token>()
        {
            { "class", new Token(TokenType.Class, "class") },
            { "public", new Token(TokenType.Public, "public") },
            { "private", new Token(TokenType.Private, "private") },
            { "static", new Token(TokenType.Static, "static") },
            { "void", new Token(TokenType.Void, "void") },
        };

        private readonly ISource _source;
        private readonly int _offset;

        internal JoanneLexer(ISource source)
        {
            _source = source;
            _offset = _source.GetCountWhere(0, char.IsWhiteSpace); ;
            _token = default;
        }

        private JoanneLexer(ISource source, int offset)
        {
            _source = source;
            _offset = offset;
            _token = default;
        }

        public Token Token
        {
            get
            {
                if(_token == default)
                {
                    _token = GetNextToken();
                }
                return _token;
            }
        }
        private Token _token;

        public ILexer Next
        {
            get
            {
                var offset = _offset + Token.Value.Length;
                offset += _source.GetCountWhere(offset, char.IsWhiteSpace);
                return new JoanneLexer(_source, offset);
            }
        }

        private Token GetNextToken()
        {
            var offset = _offset;

            if(offset >= _source.Length)
            {
                return new Token(TokenType.EndOfFile, "\0");
            }

            var current = _source[_offset];

            if(SingleCharTokens.ContainsKey(current))
            {
                return SingleCharTokens[current];
            }

            if(current == '_' || char.IsLetter(current))
            {
                var startPos = offset;
                offset++;
                offset += _source.GetCountWhere(offset, c => c == '_' || char.IsLetterOrDigit(c));
                var result = _source.Substring(startPos, offset - startPos);
                return PredefinedIdentifiers.ContainsKey(result)
                    ? PredefinedIdentifiers[result]
                    : new Token(TokenType.Identifier, result);
            }

            {
                var startPos = offset;
                offset += _source.GetCountWhere(offset, c => char.IsWhiteSpace(c) == false);
                var possibleToken = _source.Substring(startPos, offset - startPos);
                throw new NotImplementedException(possibleToken);
            }
        }
    }
}