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
        public TokenType Code {get;}
        public string Value {get;}

        public Token(TokenType c, string v)
        {
            Code=c;
            Value=v;
        }
    }

    class TokenScanner(string input)
    {   
        private readonly string input;
        private int position;

        public TokenScanner(string input)
        {
            this.input=input;
            position=0;
        }

        public Token GetNextToken()
        {
            SkipWhiteSpace();
            
            if(position >= input.Length)
                return new Token(TokenType.EndOfFile, "");
            

        }
    }

    public static void Main()
    {


    }
}