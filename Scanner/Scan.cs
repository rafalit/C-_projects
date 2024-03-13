
using System;

class Program
{
    enum TokenType
    {
        Integer,
        Identifier,
        Plus,
        Minus,
        Multiply,
        Divide,
        LeftParenthesis,
        RightParenthesis,
        EndOfFile,
        Error
    }


    class Token
    {
        public TokenType Code { get; }
        public string Value { get; }

        public Token(TokenType c, string v)
        {
            Code = c;
            Value = v;
        }
    }

    class TokenScanner
    {
        private readonly string input;
        private int position;

        public TokenScanner(string input)
        {
            this.input = input;
            position = 0;
        }

        public Token GetNextToken()
        {
            SkipWhiteSpace();

            if (position >= input.Length)
                return new Token(TokenType.EndOfFile, "");

            char currentChar = input[position];

            if (char.IsDigit(currentChar))
            {
                return ScanInteger();
            }
            else if (char.IsLetter(currentChar))
            {
                return ScanIdentifier();
            }
            else
            {
                position++;
                switch (currentChar)
                {
                    case '+':
                        return new Token(TokenType.Plus, "+");
                    case '-':
                        return new Token(TokenType.Minus, "-");
                    case '*':
                        return new Token(TokenType.Multiply, "*");
                    case '/':
                        return new Token(TokenType.Divide, "/");
                    case '(':
                        return new Token(TokenType.LeftParenthesis, "(");
                    case ')':
                        return new Token(TokenType.RightParenthesis, ")");
                    default:
                        ThrowError($"Nieznany token {currentChar}");
                        return new Token(TokenType.Error, "");
                }
            }
        }

        private Token ScanInteger()
        {
            int start = position;
            while (position < input.Length && char.IsDigit(input[position]))
            {
                position++;
            }
            return new Token(TokenType.Integer, input.Substring(start, position - start));
        }

        private Token ScanIdentifier()
        {
            int start = position;
            while (position < input.Length && (char.IsLetterOrDigit(input[position]) || input[position] == '_'))
            {
                position++;
            }
            return new Token(TokenType.Identifier, input.Substring(start, position - start));
        }

        private void SkipWhiteSpace()
        {
            while (position < input.Length && char.IsWhiteSpace(input[position]))
            {
                position++;
            }
        }

        private void ThrowError(string message)
        {
            throw new Exception($"{message}, (kolumna {position + 1})");
        }
    }

    public static void Main()
    {
        Console.WriteLine("Podaj wyrażenie matematyczne:");
        string input = Console.ReadLine();

        try
        {
            TokenScanner scanner = new TokenScanner(input);
            Token token;

            do
            {
                token = scanner.GetNextToken();
                Console.WriteLine($"({token.Code}, {token.Value})");
            } while (token.Code != TokenType.EndOfFile);

            Console.WriteLine("Skanowanie zakończone.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd skanera: {ex.Message}");
        }
    }

}
