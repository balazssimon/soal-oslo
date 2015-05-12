module OsloExtensions
{
    language OsloCodeGenerator
    {
        syntax Main 
            = WSCS? decls:Declarations WSCS? => Program { Declarations => decls };
        
        syntax Declarations
        = uds:UsingDeclarations WSCS? cfg:ConfigurationDeclaration? WSCS? fds:FunctionDeclarations => [valuesof(uds), cfg, valuesof(fds)];

        syntax UsingDeclarations
            = empty => []
            | uds:UsingDeclarations WSCS? ud:UsingDeclaration => [valuesof(uds), ud];
        syntax UsingDeclaration
            = d:Reference => d
            | d:Using => d
            | d:Include => d
            | d:Import => d
            ;
        
        syntax ConfigurationDeclaration
            = TConfiguration WSCS cp:ConfigurationContent? WSCS? TEnd WSCS TConfiguration => Configuration { Content => cp };
        
        syntax FunctionDeclarations
            = empty => []
            | fds:FunctionDeclarations WSCS? fd:FunctionDeclaration => [valuesof(fds), fd];
        syntax FunctionDeclaration
            = d:Function => d
            | d:Template => d
            ;
        
        syntax Reference
            = TReference WSCS fn:TextLiteral WSCS? ";" => Reference { Assembly => fn };
        syntax Using
            = TUsing WSCS ns:NamespaceReference WSCS? ";" => Using { Namespace => ns };
        syntax Include
            = TInclude WSCS fn:TextLiteral WSCS? ";" => Include { FileName => fn };
        syntax Import
            = TImport WSCS name:Identifier WSCS? ";" => Import { GeneratorName => name };
        syntax NamespaceReference
            = ns:Identifier => [ns]
            | nss:NamespaceReference WSCS? "." WSCS? ns:Identifier => [valuesof(nss), ns];

        syntax ConfigurationContent
            = cp:ConfigurationProperties? WSCS? upg:UserConfigurationPropertyGroup? => ConfigurationContent { ConfigurationProperties => cp, UserProperties => upg };
        syntax UserConfigurationPropertyGroup
            = TProperties WSCS name:Identifier WSCS ucp:UserConfigurationProperties? WSCS? TEnd WSCS TProperties => UserPropertyGroup { Name => name, Properties => ucp }
            ;
        syntax ConfigurationProperties
            = nnp:NamespaceNameProperty? WSCS? cnp:ClassNameProperty? WSCS? itp:InstancesTypeProperty? WSCS? ctp:ContextTypeProperty?  WSCS? umln:UseMcgLineNumbersProperty? => ConfigurationProperties { NamespaceName => nnp, ClassName => cnp, InstancesType => itp, ContextType => ctp, UseMcgLineNumbers => umln };
        syntax UserConfigurationProperties
            = cp:UserConfigurationProperty => [cp]
            | cps:UserConfigurationProperties WSCS? cp:UserConfigurationProperty => [valuesof(cps), cp]
            ;
        syntax UserConfigurationProperty
            = type:TypeReference WSCS name:Identifier WSCS? ";" => UserProperty { Name => name, Type => type, Default => null }
            | type:TypeReference WSCS name:Identifier WSCS? "=" WSCS? exp:Expression ";" => UserProperty { Name => name, Type => type, Default => exp }
            | upg:UserConfigurationPropertyGroup => upg;
        syntax NamespaceNameProperty
            = TString WSCS "NamespaceName" WSCS? "=" WSCS? ns:TextLiteral ";" => ConfigurationProperty { Name => "NamespaceName", Type => "string", Default => ns };
        syntax ClassNameProperty
            = TString WSCS "ClassName" WSCS? "=" WSCS? ns:TextLiteral ";" => ConfigurationProperty { Name => "ClassName", Type => "string", Default => ns };
        syntax InstancesTypeProperty
            = "Type" WSCS "InstancesType" WSCS? "=" WSCS? TTypeOf WSCS? "(" WSCS? type:TypeReference WSCS? ")" WSCS? ";" => ConfigurationProperty { Name => "InstancesType", Type => "Type", Default => type };
        syntax ContextTypeProperty
            = "Type" WSCS "ContextType" WSCS? "=" WSCS? TTypeOf WSCS? "(" WSCS? type:TypeReference WSCS? ")" WSCS? ";" => ConfigurationProperty { Name => "ContextType", Type => "Type", Default => type };
        syntax UseMcgLineNumbersProperty
            = TBool WSCS "UseMcgLineNumbers" WSCS? "=" WSCS? bl:LogicalLiteral WSCS? ";" => ConfigurationProperty { Name => "UseMcgLineNumbers", Type => "bool", Default => bl };

        syntax Function 
            = TFunction WSCS type:TypeReference WSCS name:Identifier WSCS? "(" WSCS? params:Parameters? WSCS? ")" WSCS? statements:Statements? WSCS? TEnd WSCS TFunction => Function { Name => name, ReturnType => type, Parameters => params, Statements => statements };
        syntax Parameters
            = p:Parameter => [p]
            | ps:Parameters WSCS? "," WSCS? p:Parameter => [valuesof(ps), p];
        syntax Parameter = type:TypeReference WSCS name:Identifier => Parameter { Name => name, Type => type };
        
        syntax Template
            = TTemplate WSCS name:Identifier WSCS? "(" WSCS? params:Parameters? WSCS? ")" NL content:TemplateContent? TEnd SP TTemplate => Template { Name => name, Parameters => params, Content => content };

        syntax TemplateContent
            = tcl:TemplateContentLine+ => tcl;
        
        syntax TypeReference
            = str:SimpleTypeReference => str
            | atr:ArrayTypeReference => atr
            | atr:TemplateTypeReference => atr;
        syntax SimpleTypeReference
            = bit:BuiltInTypes => SimpleType { Name => bit, IsBuiltInType => true, IsNullable => false }
            | bit:BuiltInTypes WSCS? "?" => SimpleType { Name => bit, IsBuiltInType => true, IsNullable => true }
            | nbit:NonBuiltInTypes => SimpleType { Name => nbit, IsBuiltInType => true, IsNullable => false }
            | nbit:NonBuiltInTypes WSCS? "?" => SimpleType { Name => nbit, IsBuiltInType => true, IsNullable => true };
        syntax ArrayTypeReference
            = str:SimpleTypeReference WSCS? "[" WSCS? "]" => ArrayType { ItemType => str };
        syntax TemplateTypeReference
            = name:Identifier WSCS? "<" WSCS? params:TemplateParamList WSCS? ">" => TemplateType { Name => name, Params => params };
        syntax TemplateParamList
            = tr:TypeReference => [tr]
            | tpl:TemplateParamList WSCS? "," WSCS? tr:TypeReference => [valuesof(tpl), tr];
        token BuiltInTypes
            = TBool | TByte | TInt | TLong | TFloat | TDouble | TString;
        token NonBuiltInTypes
            = Identifier - BuiltInTypes;
            
        syntax Statements 
            = s:Statement => [s]
            | ss:Statements WSCS? s:Statement => [valuesof(ss), s];
        syntax Statement 
            = s:VariableDeclarationStatement => s
            | s:ReturnStatement => s
            | s:AssignmentStatement => s
            | s:CallStatement => s
            | s:IfStatement => s
            | s:LoopStatement => s
            ;
        syntax VariableDeclarationStatement
            = e:VariableDeclarationExpression WSCS? ";" => e;
        syntax ReturnStatement
            = TReturn WSCS? result:Expression? WSCS? ";" => ReturnStatement { Result => result };
        syntax AssignmentStatement
            = e:AssignmentExpression WSCS? ";" => e;
        syntax CallStatement
            = e:CallExpression WSCS? ";" => e; 
        syntax IfStatement
            = precedence 1: isb:IfStatementBegin WSCS tss:Statements? WSCS? ise:IfStatementEnd => IfStatement { Begin => isb, ThenStatements => tss, ElseIfs => null, Else => null, ElseStatements => null, End => ise }
            | precedence 1: isb:IfStatementBegin WSCS tss:Statements? WSCS? es:ElseStatement WSCS ess:Statements? WSCS? ise:IfStatementEnd => IfStatement { Begin => isb, ThenStatements => tss, ElseIfs => null, Else => es, ElseStatements => ess, End => ise }
            | precedence 2: isb:IfStatementBegin WSCS tss:Statements? WSCS? eisl:ElseIfStatementList WSCS? ise:IfStatementEnd => IfStatement { Begin => isb, ThenStatements => tss, ElseIfs => eisl, Else => null, ElseStatements => null, End => ise }
            | precedence 2: isb:IfStatementBegin WSCS tss:Statements? WSCS? eisl:ElseIfStatementList WSCS? es:ElseStatement WSCS ess:Statements? WSCS? ise:IfStatementEnd => IfStatement { Begin => isb, ThenStatements => tss, ElseIfs => eisl, Else => es, ElseStatements => ess, End => ise }
            ;
        syntax LoopStatement
            = lsb:LoopStatementBegin WSCS ss:Statements? WSCS? lse:LoopStatementEnd => LoopStatement { Begin => lsb, Statements => ss, Else => null, ElseStatements => null, End => lse }
            | lsb:LoopStatementBegin WSCS ss:Statements? WSCS? es:ElseStatement WSCS ess:Statements? WSCS? lse:LoopStatementEnd => LoopStatement { Begin => lsb, Statements => ss, Else => es, ElseStatements => ess, End => lse };
        syntax LoopRunExpressionsList
            = ";" WSCS? lres:LoopRunExpressions => [lres]
            | lresl:LoopRunExpressionsList WSCS? ";" WSCS? lres:LoopRunExpressions => [valuesof(lresl), lres]
            ;  
        syntax LoopRunExpressions
            = lre:LoopRunExpression => [lre]
            | lres:LoopRunExpressions WSCS? "," WSCS? lre:LoopRunExpression => [valuesof(lres), lre]
            ;  
        syntax LoopRunExpression
            = e:VariableDeclarationExpression => e
            | e:AssignmentExpression => e
            | e:CallExpression => e
            ;
        syntax VariableDeclarationExpression
            = type:TypeReference WSCS name:Identifier => VariableDeclarationStatement { Name => name, Type => type, Default => null }
            | type:TypeReference WSCS name:Identifier WSCS? "=" WSCS? default:Expression => VariableDeclarationStatement { Name => name, Type => type, Default => default }
            ;
        syntax AssignmentExpression
            = lhs:VariableExpression WSCS? "=" WSCS? rhs:Expression => AssignmentStatement { Left => lhs, Right => rhs }
            | lhs:PropertyAccessExpression WSCS? "=" WSCS? rhs:Expression => AssignmentStatement { Left => lhs, Right => rhs }
            ;
        syntax CallExpression
            = call:FunctionCallExpression => CallStatement { Call => call }
            | call:MethodCallExpression => CallStatement { Call => call }
            ;

        syntax Expression
            = exp:PostfixExpression => exp
            | exp:InfixExpression => exp;

        syntax FunctionCallExpression
            = name:Identifier WSCS? "(" WSCS? params:PassedParams? WSCS? ")" => FunctionCallExpression { Name => name, TemplateParams => null, Params => params }
            | name:Identifier WSCS? "<" WSCS? templParams:TemplateParamList WSCS? ">" WSCS? "(" WSCS? params:PassedParams? WSCS? ")" => FunctionCallExpression { Name => name, TemplateParams => templParams, Params => params };
        syntax NewExpression
            = "new" WSCS name:Identifier WSCS? "(" WSCS? params:PassedParams? WSCS? ")" => NewExpression { Name => name, TemplateParams => null, Params => params }
            | "new" WSCS name:Identifier WSCS? "<" WSCS? templParams:TemplateParamList WSCS? ">" WSCS? "(" WSCS? params:PassedParams? WSCS? ")" => NewExpression { Name => name, TemplateParams => templParams, Params => params };
        syntax MemberFunctionCallExpression
            = name:Identifier WSCS? "(" WSCS? params:PassedParams? WSCS? ")" => MemberFunctionCallExpression { Name => name, TemplateParams => null, Params => params }
            | name:Identifier WSCS? "<" WSCS? templParams:TemplateParamList WSCS? ">" WSCS? "(" WSCS? params:PassedParams? WSCS? ")" => MemberFunctionCallExpression { Name => name, TemplateParams => templParams, Params => params };
        syntax PassedParams
            = pp:PassedParam => [pp]
            | pps:PassedParams WSCS? "," WSCS? pp:PassedParam => [valuesof(pps), pp];
        syntax PassedParam 
            = exp:Expression => exp;
        syntax MethodCallExpression
            = object:PostfixExpression WSCS? "." WSCS? fce:MemberFunctionCallExpression => MethodCallExpression { Object => object, FunctionCall => fce };
        syntax PropertyAccessExpression
            = object:PostfixExpression WSCS? "." WSCS? pn:Identifier => PropertyAccessExpression { Object => object, PropertyName => pn };
        syntax VariableExpression
            = name:Identifier => VariableExpression { Name => name };
        syntax BracketExpression
            = "(" WSCS? exp:Expression WSCS? ")" => BracketExpression { Expression => exp };
        syntax TypeCastExpression
            = "(" WSCS? type:TypeReference WSCS? ")" WSCS? exp:PostfixExpression => TypeCastExpression { Type => type, Expression => exp };
        syntax InfixExpression
            = pe:PrefixExpression => pe
            | lhs:InfixExpression WSCS? op:InfixOp WSCS? rhs:PrefixExpression => InfixExpression { Left => lhs, Operator => op, Right => rhs };
        syntax PrefixExpression
            = op:PrefixOp WSCS? exp:PrefixExpression => PrefixExpression { Operator => op, Expression => exp }
            | pe:PostfixExpression => pe
            ;
        syntax PostfixExpression
            = precedence 3: exp:FunctionCallExpression => exp
            | precedence 3: exp:MethodCallExpression => exp
            | precedence 3: exp:NewExpression => exp
            | precedence 3: exp:PropertyAccessExpression => exp
            | precedence 3: exp:VariableExpression => exp
            | precedence 1: exp:Literal => exp
            | precedence 3: exp:BracketExpression => exp
            | precedence 2: exp:TypeCastExpression => exp
            ;
        token InfixOp
            = "||"
            | "&&"
            | "=="
            | "!="
            | "<"
            | ">"
            | "<="
            | ">="
            | "+"
            | "-"
            | "*"
            | "/"
            | "is"
            ;
        token PrefixOp
            = "!" 
            | "-"
            | "++"
            ;
        
        syntax LoopStatementBegin
            = TLoop WSCS? "(" WSCS? le:LoopExpression WSCS? ")" => LoopStatementBegin { Loop => le };
        syntax IfStatementBegin
            = TIf WSCS? "(" WSCS? condition:Expression WSCS? ")" => IfStatementBegin { Condition => condition };
        syntax ElseIfStatementBegin
            = TElseIf WSCS? "(" WSCS? condition:Expression WSCS? ")" => ElseIfStatementBegin { Condition => condition };
        syntax ElseIfStatement
            = eisb:ElseIfStatementBegin WSCS? tss:Statements? => ElseIfStatement { Begin => eisb, Statements => tss };
        syntax ElseIfStatementList
            = eis:ElseIfStatement => [eis]
            | eisl:ElseIfStatementList WSCS? eis:ElseIfStatement => [valuesof(eisl), eis]
            ;
        syntax ElseStatement
            = TElse => ElseStatement {};

        syntax IfStatementEnd
            = TEnd WSCS TIf => IfStatementEnd {};
        syntax LoopStatementEnd
            = TEnd WSCS TLoop => LoopStatementEnd {};

        syntax LoopExpression
            = lce:LoopChainExpression WSCS? lrel:LoopRunExpressionsList? => LoopExpression { LoopChain => lce, Where => null, OrderBy => null, Runs => lrel }
            | lce:LoopChainExpression WSCS TWhere WSCS we:Expression WSCS? lrel:LoopRunExpressionsList? => LoopExpression { LoopChain => lce, Where => we, OrderBy => null, Runs => lrel }
            | lce:LoopChainExpression WSCS TOrderBy WSCS obl:OrderByList WSCS? lrel:LoopRunExpressionsList? => LoopExpression { LoopChain => lce, Where => null, OrderBy => obl, Runs => lrel }
            | lce:LoopChainExpression WSCS TWhere WSCS we:Expression WSCS TOrderBy WSCS obl:OrderByList? WSCS? lrel:LoopRunExpressionsList? => LoopExpression { LoopChain => lce, Where => we, OrderBy => obl, Runs => lrel };
        syntax LoopChainExpression
            = exp:PostfixExpression => [LoopItem { Item => exp, Alias => null }]
            | alias:Identifier WSCS? ":" WSCS? exp:PostfixExpression => [LoopItem { Item => exp, Alias => alias }]
            | exp:PostfixExpression "->" lcae:LoopChainArrowsExpression => [LoopItem { Item => exp, Alias => null }, valuesof(lcae)]
            | alias:Identifier WSCS? ":" WSCS? exp:PostfixExpression "->" lcae:LoopChainArrowsExpression => [LoopItem { Item => exp, Alias => alias }, valuesof(lcae)];
        syntax LoopChainArrowsExpression
            = li:LoopItem => [li]
            | lc:LoopChainArrowsExpression "->" li:LoopItem => [valuesof(lc), li];
        syntax LoopItem
            = item:MemberFunctionCallExpression => LoopItem { Item => item, Alias => null }
            | alias:Identifier WSCS? ":" WSCS? item:MemberFunctionCallExpression => LoopItem { Item => item, Alias => alias }
            | item:MethodCallExpression => LoopItem { Item => item, Alias => null }
            | alias:Identifier WSCS? ":" WSCS? item:MethodCallExpression => LoopItem { Item => item, Alias => alias }
            | item:PropertyAccessExpression => LoopItem { Item => item, Alias => null }
            | alias:Identifier WSCS? ":" WSCS? item:PropertyAccessExpression => LoopItem { Item => item, Alias => alias }
            | item:Identifier => LoopItem { Item => item, Alias => null }
            | alias:Identifier WSCS? ":" WSCS? item:Identifier=> LoopItem { Item => item, Alias => alias };
        syntax WhereExpression
            = TWhere exp:Expression => WhereExpression { Expression => exp };
        syntax OrderByList
            = ob:OrderBy => [ob]
            | obl:OrderByList WSCS? "," WSCS? ob:OrderBy => [valuesof(obl),ob];
        syntax OrderBy 
            = exp:Expression => OrderBy { Expression => exp, Descending => false }
            | exp:Expression WSCS TDescending => OrderBy { Expression => exp, Descending => true };
        
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
        token TElseIf = "else if";
        @{Classification["Keyword"]}
        token TLoop = "loop";
        @{Classification["Keyword"]}
        token TWhere = "where";
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
        token TObject = "object";
        @{Classification["Keyword"]}
        token TDynamic = "dynamic";
        @{Classification["Keyword"]}
        token TTypeOf = "typeof";
     
        token NonTemplateOutputCharacter
            = NewLineCharacter;
        token TemplateOutputCharacter
            = !NonTemplateOutputCharacter;
        token TemplateOutputCharacters
            = TemplateOutputCharacter+;
        
        token TemplateContentLine
            = SpaceCharacter* NonSpaceCharacterNonE NonNewLineCharacter* NL
            | SpaceCharacter* "e" !("n") NonNewLineCharacter* NL
            | SpaceCharacter* "en" !("d") NonNewLineCharacter* NL
            | SpaceCharacter* "end" !SP NonNewLineCharacter* NL
            | SpaceCharacter* "end" SP NonSpaceCharacterNonT NonNewLineCharacter* NL
            | SpaceCharacter* "end" SP "t" !("e") NonNewLineCharacter* NL
            | SpaceCharacter* "end" SP "te" !("m") NonNewLineCharacter* NL
            | SpaceCharacter* "end" SP "tem" !("p") NonNewLineCharacter* NL
            | SpaceCharacter* "end" SP "temp" !("l") NonNewLineCharacter* NL
            | SpaceCharacter* "end" SP "templ" !("a") NonNewLineCharacter* NL
            | SpaceCharacter* "end" SP "templa" !("t") NonNewLineCharacter* NL
            | SpaceCharacter* "end" SP "templat" !("e") NonNewLineCharacter* NL
            ; 
        token NonNewLineCharacter
            = !NewLineCharacter;
        token NonWhitespaceCharacter
            = !WhitespaceCharacter;
        token NonSpaceCharacterNonE
            = ! ("e" | SpaceCharacter);
        token NonSpaceCharacterNonT
            = ! ("t" | SpaceCharacter);
        // General syntax elements:
        //interleave Whitespace
        //    = WhitespaceCharacter+;
        token WS = WhitespaceCharacter+;
        token SP = SpaceCharacter+;
        token SpaceCharacter 
            = "\u0009" // Horizontal Tab
            | "\u000B" // Vertical Tab
            | "\u000C" // Form Feed
            | "\u0020"; // Space
        token NL = NewLineCharacter+;
        token WhitespaceCharacter
            = SpaceCharacter
            | NewLineCharacter;
        token NewLineCharacter
            = "\u000A" // New Line
            | "\u000D" // Carriage Return
            | "\u0085" // Next Line
            | "\u2028" // Line Separator
            | "\u2029"; // Paragraph Separator
        token WSC 
            = WhitespaceCharacter
            | CommentDelimited
            | CommentLine;
        token WSCS
            = WSC+;
        token NLC
            = NewLineCharacter
            | CommentLine;
        token NLCS
            = NLC+;
        token SPC
            = SpaceCharacter
            | CommentDelimited;
        token SPCS
            = SPC+;
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
        token LogicalLiteral
            = TTrue
            | TFalse;
        token NullLiteral
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
    
    language OsloCodeGeneratorTemplate
    {
        syntax Main 
            = tcl:TemplateContentList => Main { Content => tcl };
        syntax TemplateContentList
            = tc:TemplateContent* => tc;
        syntax TemplateContent
            = tc:TemplateControl => tc
            | to:TemplateOutput => to;
    
        syntax Control
            = s:OsloCodeGenerator.LoopStatementBegin => TemplateControl { Statement => s, Expression => null }
            | s:OsloCodeGenerator.LoopStatementEnd => TemplateControl { Statement => s, Expression => null }
            | s:OsloCodeGenerator.IfStatementBegin => TemplateControl { Statement => s, Expression => null }
            | s:OsloCodeGenerator.ElseIfStatementBegin => TemplateControl { Statement => s, Expression => null }
            | s:OsloCodeGenerator.ElseStatement => TemplateControl { Statement => s, Expression => null }
            | s:OsloCodeGenerator.IfStatementEnd => TemplateControl { Statement => s, Expression => null }
            | e:OsloCodeGenerator.Expression => TemplateControl { Statement => null, Expression => e };
    
        syntax TemplateControl
            = "[" c:Control "]" => c;
        syntax TemplateOutput
            = o:NonBrackets => TemplateOutput { Output => o };
        token NonBrackets
            = NonBracket+;
    
        token NonBracket = !("[" | "]");
    }
}
