module OsloExtensions
{
    language OsloCodeGeneratorScanner
    {
        syntax Main 
            = ts:Tokens => Line { Tokens => ts };
        
        syntax Tokens
            = t:Token => [t]
            | ts:Tokens t:Token => [valuesof(ts), t];
        
        syntax Token
            = s:SKeyword => s
            | s:SIdentifier => s
            | s:SNumberLiteral => s
            | s:STextLiteral => s
            | s:SPunctuation => s
            | s:SSpace => s
            | s:SCommentLine => s
            | s:SCommentStart => s
            ;

        syntax SKeyword
            = TReference | TUsing | TInclude | TImport | TTemplate | TFunction | TEnd | TReturn 
            | TIf | TElse | TLoop | TWhere | TTypeOf | TOrderBy | TDescending 
            | TTrue | TFalse | TNull | TVoid | TBool | TInt | TLong | TFloat | TDouble | TString
            | TObject | TDynamic | TConfiguration | TProperties;
        
        syntax SPunctuation
            = "." | ";" | "=" | "==" | "(" | ")" | "[" | "]" | "<" | ">" | "{" | "}" 
            | "->" | "+" | "-" | "*" | "/" | "!" | "&&" | "||" | "^" | ":" | "%" | "?" | "@" 
            | "," | "\\" | "#" | "&";
        
        syntax SNumberLiteral
            = IntegerLiteral
            | ScientificLiteral
            ;

        syntax STextLiteral
            = TextLiteral;
            
        syntax SIdentifier
            = Identifier;
        
        syntax SSpace
            = SP;
        
        syntax SNewLine
            = NL;
            
        syntax SCommentLine
            = CommentLine;
            
        syntax SCommentStart
            = "/*";
            
        @{Classification["Keyword"]}
        token TReference = "reference";
        @{Classification["Keyword"]}
        token TUsing = "using";
        @{Classification["Keyword"]}
        token TInclude = "include";
        @{Classification["Keyword"]}
        token TImport = "import";
        @{Classification["Keyword"]}
        token TConfiguration = "configuration";
        @{Classification["Keyword"]}
        token TProperties = "properties";
        @{Classification["Keyword"]}
        token TTemplate = "template";
        @{Classification["Keyword"]}
        token TFunction = "function";
        @{Classification["Keyword"]}
        token TEnd = "end";
        @{Classification["Keyword"]}
        token TReturn = "return";
        @{Classification["Keyword"]}
        token TIf = "if";
        @{Classification["Keyword"]}
        token TElse = "else";
        @{Classification["Keyword"]}
        token TLoop = "loop";
        @{Classification["Keyword"]}
        token TWhere = "where";
        @{Classification["Keyword"]}
        token TTypeOf = "typeof";
        @{Classification["Keyword"]}
        token TOrderBy = "orderby";
        @{Classification["Keyword"]}
        token TDescending = "descending";
        
        @{Classification["Keyword"]}
        token TTrue = "true";
        @{Classification["Keyword"]}
        token TFalse = "false";
        @{Classification["Keyword"]}
        token TNull = "null";
        @{Classification["Keyword"]}
        token TVoid = "void";
        @{Classification["Keyword"]}
        token TBool = "bool";
        @{Classification["Keyword"]}
        token TByte = "byte";
        @{Classification["Keyword"]}
        token TInt = "int";
        @{Classification["Keyword"]}
        token TLong = "long";
        @{Classification["Keyword"]}
        token TFloat = "float";
        @{Classification["Keyword"]}
        token TDouble = "double";
        @{Classification["Keyword"]}
        token TString = "string";
        @{Classification["Keyword"]}
        token TDynamic = "dynamic";
        @{Classification["Keyword"]}
        token TObject = "object";
     
        token SP = SpaceCharacter+;
        token NL 
            = "\u000D" "\u000A"
            | NewLineCharacter;    
        token WhitespaceCharacter
            = SpaceCharacter
            | NewLineCharacter;
        token SpaceCharacter 
            = "\u0009" // Horizontal Tab
            | "\u000B" // Vertical Tab
            | "\u000C" // Form Feed
            | "\u0020"; // Space
        token NewLineCharacter
            = "\u000A" // New Line
            | "\u000D" // Carriage Return
            | "\u0085" // Next Line
            | "\u2028" // Line Separator
            | "\u2029"; // Paragraph Separator
        //interleave Comment
        //    = CommentDelimited
        //    | CommentLine;
        @{Classification["Comment"]}
        token CommentDelimited
            = "/*" CommentDelimitedContent* "*/";
        token CommentDelimitedContent
            = !("*")
            | "*" !("/");
        @{Classification["Comment"]}
        token CommentLine
            = "//" CommentLineContent*;
        token CommentLineContent
            = !NewLineCharacter;
        @{Classification["Identifier"]}
        syntax IdentifierAny
            = id:IdentifierVerbatim
            | id:Identifier;
        @{Classification["Identifier"]}
        token Identifier
            = IdentifierBegin IdentifierCharacter*;
        token IdentifierBegin = "_" | Letter;
        token Letter = "a".."z" | "A".."Z";
        token IdentifierCharacter
            = IdentifierBegin
            | "$"
            | DecimalDigit;
        token IdentifierCharacters
            = IdentifierCharacter+;
        @{Classification["Identifier"]}
        token IdentifierVerbatim
            = "@[" IdentifierVerbatimCharacters "]";
        token IdentifierVerbatimCharacter
            = !( "]" )
            | IdentifierVerbatimEscape;
        token IdentifierVerbatimCharacters
            = IdentifierVerbatimCharacter+;
        token IdentifierVerbatimEscape
            = "\\\\" | "\\]";
        syntax Literal
            = DateLiteral
            | DateTimeLiteral
            | DateTimeOffsetLiteral
            | DecimalLiteral
            | GuidLiteral
            | IntegerLiteral
            | LogicalLiteral
            | NullLiteral
            | ScientificLiteral
            | TextLiteral
            | TimeLiteral;
        @{Classification["Number"]}
        token DateLiteral
            = Sign? DateYear "-" DateMonth "-" DateDay;
        token DateDay
            = "01" | "02" | "03" | "04" | "05" | "06" | "07" | "08" | "09" | "10"
            | "11" | "12" | "13" | "14" | "15"
            | "16" | "17" | "18" | "19" | "20" | "21" | "22" | "23" | "24" | "25"
            | "26" | "27" | "28" | "29" | "30"
            | "31";
        token DateMonth
            = "01" | "02" | "03" | "04" | "05" | "06" | "07" | "08"
            | "09" | "10" | "11" | "12";
        token DateYear
            = DecimalDigit DecimalDigit DecimalDigit DecimalDigit;
        @{Classification["Number"]}
        token DateTimeLiteral
            = DateLiteral "T" TimeLiteral;
        @{Classification["Number"]}
        token DateTimeOffsetLiteral = DateLiteral "T" TimeLiteral TimeZone;
        token TimeZone
            = Sign OffsetTimeHourMinute
            | "Z";
        token OffsetTimeHour
            = "00" | "01" | "02" | "03" | "04" | "05" | "06" | "07" | "08" | "09"
            | "10" | "11" | "12" | "13" | "14";
        token OffsetTimeHourMinute
            = OffsetTimeHour ":" TimeMinute;
        @{Classification["Number"]}
        token DecimalLiteral = DecimalDigit+ "." DecimalDigit+;
        @{Classification["Number"]}
        token GuidLiteral
            = "#" "[" HexDigit HexDigit HexDigit HexDigit HexDigit HexDigit HexDigit
            HexDigit "-" HexDigit HexDigit HexDigit HexDigit "-"
            HexDigit HexDigit HexDigit HexDigit "-"
            HexDigit HexDigit HexDigit HexDigit "-"
            HexDigit HexDigit HexDigit HexDigit HexDigit HexDigit HexDigit
            HexDigit HexDigit HexDigit HexDigit HexDigit "]";
        token HexDigit = "0".."9" | "a".."f" | "A".."F";
        @{Classification["Number"]}
        token IntegerLiteral
            = DecimalDigits
            | Hexadecimal;
        token DecimalDigits = DecimalDigit+;
        token DecimalDigit = "0".."9";
        token Hexadecimal
            = "0x" HexDigit*
            | "0X" HexDigit*;
        syntax LogicalLiteral
            = TTrue
            | TFalse;
        syntax NullLiteral
            = TNull;
        @{Classification["Number"]}
        token ScientificLiteral
            = DecimalLiteral "e" Sign? DecimalDigit+
            | DecimalLiteral "E" Sign? DecimalDigit+;
        token Sign
            = "+"
            | "-";
        @{Classification["Text"]}
        token TextLiteral
            = RegularStringLiteral
            | VerbatimStringLiteral;
        token RegularStringLiteral
            = '"' DoubleQuoteTextCharacter* '"'
            | "'" SingleQuoteTextCharacter* "'";
        token VerbatimStringLiteral
            = '@' '"' DoubleQuoteTextVerbatimCharacter* '"'
            | '@' "'" SingleQuoteTextVerbatimCharacter* "'";
        token SingleQuoteTextCharacter
            = SingleQuoteTextSimple
            | CharacterEscapeSimple
            | CharacterEscapeUnicode ;
        token SingleQuoteTextSimple
            = !( '\u0027' // Single Quote
            | '\u005C' // Backslash
            | NewLineCharacter) ;
        token SingleQuoteTextVerbatimCharacter
            = !('\u0027') // SingleQuote
            | SingleQuoteTextVerbatimCharacterEscape ;
        token SingleQuoteTextVerbatimCharacterEscape = '\u0027' '\u0027';
        token SingleQuoteTextVerbatimCharacters = SingleQuoteTextVerbatimCharacter+;
        token DoubleQuoteTextCharacter
            = DoubleQuoteTextSimple
            | CharacterEscapeSimple
            | CharacterEscapeUnicode ;
        token DoubleQuoteTextSimple
            = !( '\u0022' // DoubleQuote
            | '\u005C' // Backslash
            | NewLineCharacter) ;
        token DoubleQuoteTextVerbatimCharacter
            = !('\u0022') // DoubleQuote
            | DoubleQuoteTextVerbatimCharacterEscape ;
        token DoubleQuoteTextVerbatimCharacterEscape = '\u0022' '\u0022';
        token DoubleQuoteTextVerbatimCharacters = DoubleQuoteTextVerbatimCharacter+;
        token CharacterEscapeSimple = '\u005C' CharacterEscapeSimpleCharacter;
        token CharacterEscapeSimpleCharacter
        = "'" // Single Quote
        | '"' // Double Quote
        | '\u005C' // Backslash
        | '0' // Null
        | 'a' // Alert
        | 'b' // Backspace
        | 'f' // Form Feed
        | 'n' // New Line
        | 'r' // Carriage Return
        | 't' // Horizontal Tab
        | 'v'; // Vertical Tab
        token CharacterEscapeUnicode
        = "\\u" HexDigit#4
        | "\\U" HexDigit#8;
        @{Classification["Number"]}
        token TimeLiteral
        = TimeHourMinute ":" TimeSecond;
        token TimeHour
        = "00" | "01" | "02" | "03" | "04" | "05" | "06" | "07" | "08" | "09"
        | "10" | "11"
        | "12" | "13" | "14" | "15" | "16" | "17" | "18" | "19" | "20" | "21"
        | "22" | "23";
        token TimeHourMinute
        = TimeHour ":" TimeMinute;
        token TimeMinute
        = "0" DecimalDigit
        | "1" DecimalDigit
        | "2" DecimalDigit
        | "3" DecimalDigit
        | "4" DecimalDigit
        | "5" DecimalDigit;
        token TimeSecond
        = "0" DecimalDigit TimeSecondDecimalPart?
        | "1" DecimalDigit TimeSecondDecimalPart?
        | "2" DecimalDigit TimeSecondDecimalPart?
        | "3" DecimalDigit TimeSecondDecimalPart?
        | "4" DecimalDigit TimeSecondDecimalPart?
        | "5" DecimalDigit TimeSecondDecimalPart?;
        token TimeSecondDecimalPart = "." DecimalDigits;
    }

    language OsloCodeGeneratorTemplateScanner
    {
        syntax Main 
            = ts:Tokens => Line { Tokens => ts };

        syntax Tokens
            = t:Token => [t]
            | ts:Tokens t:Token => [valuesof(ts), t];
    
        syntax Token
            = s:STemplateOutput => s
            | s:STemplateControl => s
            ;
        
        syntax STemplateControl
            = "[" control:OsloCodeGeneratorScanner.Main "]" => STemplateControl { Control => control };
        syntax STemplateOutput
            = NonBrackets;
    
        token NonBrackets
            = NonBracket+;
        token NonBracket = !("[" | "]");
    }
}
