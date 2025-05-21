
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF           =  0, // (EOF)
        SYMBOL_ERROR         =  1, // (Error)
        SYMBOL_WHITESPACE    =  2, // Whitespace
        SYMBOL_AND           =  3, // And
        SYMBOL_ASSIGN        =  4, // Assign
        SYMBOL_DEF           =  5, // Def
        SYMBOL_DIGIT         =  6, // Digit
        SYMBOL_DIGITS        =  7, // Digits
        SYMBOL_DIV           =  8, // Div
        SYMBOL_ELSE          =  9, // Else
        SYMBOL_EQ            = 10, // Eq
        SYMBOL_EXP           = 11, // Exp
        SYMBOL_GE            = 12, // Ge
        SYMBOL_GT            = 13, // Gt
        SYMBOL_IDENTIFIER    = 14, // Identifier
        SYMBOL_IF            = 15, // If
        SYMBOL_LBRACE        = 16, // LBrace
        SYMBOL_LE            = 17, // Le
        SYMBOL_LPAREN        = 18, // LParen
        SYMBOL_LT            = 19, // Lt
        SYMBOL_MINUS         = 20, // Minus
        SYMBOL_MOD           = 21, // Mod
        SYMBOL_MUL           = 22, // Mul
        SYMBOL_NE            = 23, // Ne
        SYMBOL_OR            = 24, // Or
        SYMBOL_PLUS          = 25, // Plus
        SYMBOL_RBRACE        = 26, // RBrace
        SYMBOL_RPAREN        = 27, // RParen
        SYMBOL_SEMICOLON     = 28, // SemiColon
        SYMBOL_STRINGLITERAL = 29, // StringLiteral
        SYMBOL_THEN          = 30, // Then
        SYMBOL_WHILE         = 31, // While
        SYMBOL_ADDEXP        = 32, // <Add Exp>
        SYMBOL_ASSIGN2       = 33, // <assign>
        SYMBOL_ELSE2         = 34, // <else>
        SYMBOL_ELSEIF        = 35, // <else if>
        SYMBOL_EXPRESSION    = 36, // <Expression>
        SYMBOL_FACTOR        = 37, // <factor>
        SYMBOL_IF2           = 38, // <if>
        SYMBOL_LOGICALANDEXP = 39, // <LogicalAndExp>
        SYMBOL_MULTEXP       = 40, // <Mult Exp>
        SYMBOL_NEGATEEXP     = 41, // <Negate Exp>
        SYMBOL_PROGRAM       = 42, // <Program>
        SYMBOL_RELATIONALEXP = 43, // <RelationalExp>
        SYMBOL_START         = 44, // <Start>
        SYMBOL_STMT          = 45, // <stmt>
        SYMBOL_STMT_LIST     = 46, // <stmt_list>
        SYMBOL_VALUE         = 47, // <Value>
        SYMBOL_WHILE2        = 48  // <while>
    };

    enum RuleConstants : int
    {
        RULE_START                                            =  0, // <Start> ::= <Program>
        RULE_PROGRAM_LBRACE_RBRACE                            =  1, // <Program> ::= LBrace <stmt_list> RBrace
        RULE_STMT_LIST                                        =  2, // <stmt_list> ::= <stmt> <stmt_list>
        RULE_STMT_LIST2                                       =  3, // <stmt_list> ::= <stmt>
        RULE_STMT                                             =  4, // <stmt> ::= <assign>
        RULE_STMT2                                            =  5, // <stmt> ::= <if>
        RULE_STMT3                                            =  6, // <stmt> ::= <while>
        RULE_ASSIGN_DEF_IDENTIFIER_ASSIGN_SEMICOLON           =  7, // <assign> ::= Def Identifier Assign <Expression> SemiColon
        RULE_ASSIGN_IDENTIFIER_ASSIGN_IDENTIFIER_SEMICOLON    =  8, // <assign> ::= Identifier Assign Identifier SemiColon
        RULE_ASSIGN_IDENTIFIER_ASSIGN_SEMICOLON               =  9, // <assign> ::= Identifier Assign <Expression> SemiColon
        RULE_EXPRESSION_OR                                    = 10, // <Expression> ::= <Expression> Or <LogicalAndExp>
        RULE_EXPRESSION                                       = 11, // <Expression> ::= <LogicalAndExp>
        RULE_LOGICALANDEXP_AND                                = 12, // <LogicalAndExp> ::= <LogicalAndExp> And <RelationalExp>
        RULE_LOGICALANDEXP                                    = 13, // <LogicalAndExp> ::= <RelationalExp>
        RULE_RELATIONALEXP_GT                                 = 14, // <RelationalExp> ::= <RelationalExp> Gt <Add Exp>
        RULE_RELATIONALEXP_LT                                 = 15, // <RelationalExp> ::= <RelationalExp> Lt <Add Exp>
        RULE_RELATIONALEXP_LE                                 = 16, // <RelationalExp> ::= <RelationalExp> Le <Add Exp>
        RULE_RELATIONALEXP_GE                                 = 17, // <RelationalExp> ::= <RelationalExp> Ge <Add Exp>
        RULE_RELATIONALEXP_EQ                                 = 18, // <RelationalExp> ::= <RelationalExp> Eq <Add Exp>
        RULE_RELATIONALEXP_NE                                 = 19, // <RelationalExp> ::= <RelationalExp> Ne <Add Exp>
        RULE_RELATIONALEXP                                    = 20, // <RelationalExp> ::= <Add Exp>
        RULE_ADDEXP_PLUS                                      = 21, // <Add Exp> ::= <Add Exp> Plus <Mult Exp>
        RULE_ADDEXP_MINUS                                     = 22, // <Add Exp> ::= <Add Exp> Minus <Mult Exp>
        RULE_ADDEXP                                           = 23, // <Add Exp> ::= <Mult Exp>
        RULE_MULTEXP_MUL                                      = 24, // <Mult Exp> ::= <Mult Exp> Mul <factor>
        RULE_MULTEXP_DIV                                      = 25, // <Mult Exp> ::= <Mult Exp> Div <factor>
        RULE_MULTEXP_MOD                                      = 26, // <Mult Exp> ::= <Mult Exp> Mod <factor>
        RULE_MULTEXP                                          = 27, // <Mult Exp> ::= <factor>
        RULE_FACTOR_EXP                                       = 28, // <factor> ::= <factor> Exp <Negate Exp>
        RULE_FACTOR                                           = 29, // <factor> ::= <Negate Exp>
        RULE_NEGATEEXP_MINUS                                  = 30, // <Negate Exp> ::= Minus <Value>
        RULE_NEGATEEXP                                        = 31, // <Negate Exp> ::= <Value>
        RULE_NEGATEEXP_DIGIT                                  = 32, // <Negate Exp> ::= Digit
        RULE_VALUE_IDENTIFIER                                 = 33, // <Value> ::= Identifier
        RULE_VALUE_LPAREN_RPAREN                              = 34, // <Value> ::= LParen <Expression> RParen
        RULE_VALUE_DIGITS                                     = 35, // <Value> ::= Digits
        RULE_VALUE_STRINGLITERAL                              = 36, // <Value> ::= StringLiteral
        RULE_IF_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE           = 37, // <if> ::= If LParen <Expression> RParen Then LBrace <stmt_list> RBrace
        RULE_IF_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE2          = 38, // <if> ::= If LParen <Expression> RParen Then LBrace <stmt_list> RBrace <else if>
        RULE_IF_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE3          = 39, // <if> ::= If LParen <Expression> RParen Then LBrace <stmt_list> RBrace <else>
        RULE_ELSEIF_ELSE_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE  = 40, // <else if> ::= Else If LParen <Expression> RParen Then LBrace <stmt_list> RBrace
        RULE_ELSEIF_ELSE_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE2 = 41, // <else if> ::= Else If LParen <Expression> RParen Then LBrace <stmt_list> RBrace <else if>
        RULE_ELSEIF_ELSE_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE3 = 42, // <else if> ::= Else If LParen <Expression> RParen Then LBrace <stmt_list> RBrace <else>
        RULE_ELSE_LBRACE_RBRACE                               = 43, // <else> ::= LBrace <stmt_list> RBrace
        RULE_WHILE_WHILE_LPAREN_RPAREN_THEN_LBRACE_RBRACE     = 44  // <while> ::= While LParen <Expression> RParen Then LBrace <stmt_list> RBrace
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox syntaxErrorBox;
        ListBox lexicalErrorBox;
        public MyParser(string filename, ListBox syntaxErrorBox, ListBox lexicalErrorBox)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
            this.syntaxErrorBox = syntaxErrorBox;
            this.lexicalErrorBox = lexicalErrorBox;
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            string info = args.Token.Text + "\t \t" + (SymbolConstants)args.Token.Symbol.Id;
            lexicalErrorBox.Items.Add(info);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AND :
                //And
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //Assign
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEF :
                //Def
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGITS :
                //Digits
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //Div
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //Else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //Eq
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //Exp
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GE :
                //Ge
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //Gt
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //Identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //If
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //LBrace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LE :
                //Le
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //LParen
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //Lt
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //Minus
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MOD :
                //Mod
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MUL :
                //Mul
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NE :
                //Ne
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OR :
                //Or
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //Plus
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //RBrace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //RParen
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMICOLON :
                //SemiColon
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL :
                //StringLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_THEN :
                //Then
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //While
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ADDEXP :
                //<Add Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN2 :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE2 :
                //<else>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSEIF :
                //<else if>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<Expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF2 :
                //<if>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOGICALANDEXP :
                //<LogicalAndExp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MULTEXP :
                //<Mult Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEGATEEXP :
                //<Negate Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<Program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RELATIONALEXP :
                //<RelationalExp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //<Start>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT :
                //<stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VALUE :
                //<Value>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE2 :
                //<while>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_START :
                //<Start> ::= <Program>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PROGRAM_LBRACE_RBRACE :
                //<Program> ::= LBrace <stmt_list> RBrace
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_list> ::= <stmt> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST2 :
                //<stmt_list> ::= <stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT :
                //<stmt> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT2 :
                //<stmt> ::= <if>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT3 :
                //<stmt> ::= <while>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_DEF_IDENTIFIER_ASSIGN_SEMICOLON :
                //<assign> ::= Def Identifier Assign <Expression> SemiColon
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_IDENTIFIER_ASSIGN_IDENTIFIER_SEMICOLON :
                //<assign> ::= Identifier Assign Identifier SemiColon
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_IDENTIFIER_ASSIGN_SEMICOLON :
                //<assign> ::= Identifier Assign <Expression> SemiColon
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_OR :
                //<Expression> ::= <Expression> Or <LogicalAndExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<Expression> ::= <LogicalAndExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOGICALANDEXP_AND :
                //<LogicalAndExp> ::= <LogicalAndExp> And <RelationalExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOGICALANDEXP :
                //<LogicalAndExp> ::= <RelationalExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXP_GT :
                //<RelationalExp> ::= <RelationalExp> Gt <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXP_LT :
                //<RelationalExp> ::= <RelationalExp> Lt <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXP_LE :
                //<RelationalExp> ::= <RelationalExp> Le <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXP_GE :
                //<RelationalExp> ::= <RelationalExp> Ge <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXP_EQ :
                //<RelationalExp> ::= <RelationalExp> Eq <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXP_NE :
                //<RelationalExp> ::= <RelationalExp> Ne <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RELATIONALEXP :
                //<RelationalExp> ::= <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_PLUS :
                //<Add Exp> ::= <Add Exp> Plus <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_MINUS :
                //<Add Exp> ::= <Add Exp> Minus <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP :
                //<Add Exp> ::= <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_MUL :
                //<Mult Exp> ::= <Mult Exp> Mul <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_DIV :
                //<Mult Exp> ::= <Mult Exp> Div <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_MOD :
                //<Mult Exp> ::= <Mult Exp> Mod <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP :
                //<Mult Exp> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_EXP :
                //<factor> ::= <factor> Exp <Negate Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <Negate Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NEGATEEXP_MINUS :
                //<Negate Exp> ::= Minus <Value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NEGATEEXP :
                //<Negate Exp> ::= <Value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NEGATEEXP_DIGIT :
                //<Negate Exp> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_IDENTIFIER :
                //<Value> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_LPAREN_RPAREN :
                //<Value> ::= LParen <Expression> RParen
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_DIGITS :
                //<Value> ::= Digits
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_STRINGLITERAL :
                //<Value> ::= StringLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE :
                //<if> ::= If LParen <Expression> RParen Then LBrace <stmt_list> RBrace
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE2 :
                //<if> ::= If LParen <Expression> RParen Then LBrace <stmt_list> RBrace <else if>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE3 :
                //<if> ::= If LParen <Expression> RParen Then LBrace <stmt_list> RBrace <else>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSEIF_ELSE_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE :
                //<else if> ::= Else If LParen <Expression> RParen Then LBrace <stmt_list> RBrace
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSEIF_ELSE_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE2 :
                //<else if> ::= Else If LParen <Expression> RParen Then LBrace <stmt_list> RBrace <else if>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSEIF_ELSE_IF_LPAREN_RPAREN_THEN_LBRACE_RBRACE3 :
                //<else if> ::= Else If LParen <Expression> RParen Then LBrace <stmt_list> RBrace <else>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSE_LBRACE_RBRACE :
                //<else> ::= LBrace <stmt_list> RBrace
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILE_WHILE_LPAREN_RPAREN_THEN_LBRACE_RBRACE :
                //<while> ::= While LParen <Expression> RParen Then LBrace <stmt_list> RBrace
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"' in line: " + args.UnexpectedToken.Location.LineNr;
            //todo: Report message to UI?
            syntaxErrorBox.Items.Add(message);
            message = "Expected "+args.ExpectedTokens.ToString();
            syntaxErrorBox.Items.Add(message);
        }

    }
}
