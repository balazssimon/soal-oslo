
module Soa
{
    language Services
    {
        syntax Main = NamespaceDeclarations?;

		syntax UriAttribute
			= "[" TUri "(" uri:TextLiteral ")" "]" => uri;

        syntax NamespaceDeclarations
            = nd:NamespaceDeclaration => Namespaces [nd]
            | nds:NamespaceDeclarations nd:NamespaceDeclaration => NamespaceDeclarations [nds, nd];
        syntax NamespaceDeclaration 
            = TNamespace name:IdentifierList "{" imports:NamespaceImports? decls:Declarations? "}" => Namespace { Name => name, Uri => null, Imports => imports, Declarations => decls }
            | uri:UriAttribute TNamespace name:IdentifierList "{" imports:NamespaceImports? decls:Declarations? "}" => Namespace { Name => name, Uri => uri, Imports => imports, Declarations => decls };
        
        syntax NamespaceImports 
            = ni:NamespaceImport => [ni]
            | nis:NamespaceImports ni:NamespaceImport => [valuesof(nis), ni];
        syntax NamespaceImport
            = TUsing name:IdentifierList ";" => NamespaceImport { Name => name};
        
        syntax Declarations
            = d:Declaration => [d]
            | ds:Declarations d:Declaration => [valuesof(ds), d];            
        syntax Declaration
            = d:NamespaceDeclaration => d
            | d:SequenceTypeDeclaration => d
            | d:EnumTypeDeclaration => d
            | d:ExceptionTypeDeclaration => d
            | d:InterfaceDeclaration => d
            | d:BindingDeclaration => d
            | d:EndpointDeclaration => d
            | d:ContractDeclaration => d
            | d:ClaimsetDeclaration => d
            | d:AuthorizationDeclaration => d
            | d:BpelDeclaration => d
            ;

        syntax SequenceTypeDeclaration 
            = TStruct name:Identifier "{" fields:FieldDeclarations? "}" => StructType { Name => name, SuperType => null, Fields => fields}
            | TStruct name:Identifier ":" super:NamespacedTypeReference "{" fields:FieldDeclarations? "}" => StructType { Name => name, SuperType => super, Fields => fields};
        syntax FieldDeclarations
            = f:FieldDeclaration => [f]
            | fs:FieldDeclarations f:FieldDeclaration => [valuesof(fs), f];
        syntax FieldDeclaration
            = type:TypeReference name:Identifier ";" => Field { Name => name, Type => type };
        
        syntax ExceptionTypeDeclaration
            = TException name:Identifier "{" fields:FieldDeclarations? "}" => ExceptionType { Name => name, SuperType => null, Fields => fields}
            | TException name:Identifier ":" super:NamespacedTypeReference "{" fields:FieldDeclarations? "}" => ExceptionType { Name => name, SuperType => super, Fields => fields};

		syntax ReturnTypeReference
			= tr:TypeReference => tr
			| TVoid => VoidType { Async => false }
			| TAsync => VoidType { Async => true };
        syntax TypeReference
            = str:SimpleTypeReference => str
            | atr:ArrayTypeReference => atr;
        syntax ArrayTypeReference
            = tr:TypeReference "[" "]" => ArrayType { ItemType => tr };
        syntax SimpleTypeReference
            = bit:BuiltInTypes => SimpleType { Name => bit, IsBuiltInType => true, IsNullable => false }
            | bit:BuiltInTypes "?" => SimpleType { Name => bit, IsBuiltInType => true, IsNullable => true }
            | nbit:NamespacedTypeReference => SimpleType { Name => nbit, IsBuiltInType => false, IsNullable => false }
            | nbit:NamespacedTypeReference "?" => SimpleType { Name => nbit, IsBuiltInType => false, IsNullable => true };
        token BuiltInTypes
            = TBool | TByte | TInt | TLong | TFloat | TDouble | TString | TGuid | TDate | TTime | TDateTime | TTimeSpan;
        token NonBuiltInTypes
            = Identifier - BuiltInTypes;
        syntax NamespacedTypeReferences
            = ntr:NamespacedTypeReference => [ntr]
            | ntrs:NamespacedTypeReferences "," ntr:NamespacedTypeReference => [valuesof(ntrs), ntr];
        syntax NamespacedTypeReference
            = nr:IdentifierList "." t:Identifier => NamespacedTypeReference { NamespaceReference => nr, Type => t }
            | t:NonBuiltInTypes => NamespacedTypeReference { NamespaceReference => null, Type => t };

        syntax EnumTypeDeclaration
            = TEnum name:Identifier "{" values:EnumValues? "}" => EnumType { Name => name, Values => values };
        syntax EnumValues
            = ev:EnumValue => [ev]
            | evs:EnumValues "," ev:EnumValue => [valuesof(evs), ev];
        syntax EnumValue
            = name:Identifier => EnumValue { Name => name };

        syntax ClaimsetDeclaration
            = TClaimset name:Identifier "{" fields:ClaimFieldDeclarations? "}" => ClaimsetType { Name => name, Uri => null, Fields => fields}
            | uri:UriAttribute TClaimset name:Identifier "{" fields:ClaimFieldDeclarations? "}" => ClaimsetType { Name => name, Uri => uri, Fields => fields};
        syntax ClaimFieldDeclarations
            = f:ClaimFieldDeclaration => [f]
            | fs:ClaimFieldDeclarations f:ClaimFieldDeclaration => [valuesof(fs), f];
        syntax ClaimFieldDeclaration
            = uri:UriAttribute? type:TypeReference name:Identifier ";" => ClaimField { Name => name, Type => type, Uri => uri };
        
        syntax InterfaceDeclaration
            = TInterface name:Identifier "{" operations:OperationDeclarations? "}" => Interface { Name => name, SuperInterfaces => null, Version => "1.0", Operations => operations }
            | TInterface name:Identifier "[" version:VersionLiteral "]" "{" operations:OperationDeclarations? "}" => Interface { Name => name, SuperInterfaces => null, Version => version, Operations => operations }
            | TInterface name:Identifier ":" supers:NamespacedTypeReferences "{" operations:OperationDeclarations? "}" => Interface { Name => name, SuperInterfaces => supers, Version => "1.0", Operations => operations }
            | TInterface name:Identifier ":" supers:NamespacedTypeReferences "[" version:VersionLiteral "]" "{" operations:OperationDeclarations? "}" => Interface { Name => name, SuperInterfaces => supers, Version => version, Operations => operations };
            
        syntax OperationDeclarations
            = od:OperationDeclaration => [od]
            | ods:OperationDeclarations od:OperationDeclaration => [valuesof(ods), od];
        syntax OperationDeclaration
            = operation:OperationSignature ";" => operation;
        syntax OperationSignature
            = return:ReturnTypeReference name:Identifier "(" parameters:ParameterDeclarations? ")" exceptions:ThrowsDeclarations? => Operation { Name => name, ReturnType => return, Parameters => parameters, Exceptions => exceptions };
        syntax ParameterDeclarations
            = pd:ParameterDeclaration => [pd]
            | pds:ParameterDeclarations "," pd:ParameterDeclaration => [valuesof(pds), pd];
        syntax ParameterDeclaration
            = type:TypeReference name:Identifier => Parameter { Name => name, Type => type };
        syntax ThrowsDeclarations
            = TThrows tts:ThrowsTypes => tts;
        syntax ThrowsTypes
            = tt:ThrowsType => [tt]
            | tts:ThrowsTypes "," tt:ThrowsType => [valuesof(tts), tt];
        syntax ThrowsType
            = type:NamespacedTypeReference => type;

        syntax ContractDeclaration
            = TContract name:Identifier ":" interface:NamespacedTypeReference "{" operations:OperationContractDeclarations? "}" => Contract { Name => name, Interface => interface, OperationContracts => operations };
        syntax OperationContractDeclarations
            = ocd:OperationContractDeclaration =>[ocd]
            | ocds:OperationContractDeclarations ocd:OperationContractDeclaration => [valuesof(ocds), ocd];
        syntax OperationContractDeclaration
            = operation:OperationSignature "{" ocsds:OperationContractStatementDeclarations? "}" => OperationContract { Operation => operation, OperationContractStatements => ocsds };
        syntax OperationContractStatementDeclarations
            = ocsd:OperationContractStatementDeclaration => [ocsd]
            | ocsds:OperationContractStatementDeclarations ocsd:OperationContractStatementDeclaration => [valuesof(ocsds), ocsd];
        syntax OperationContractStatementDeclaration
            = TEnsures text:TextLiteral "{" rule:Expression ";" "}" => Ensures { Text => text, Rule => rule }
            | TRequires text:TextLiteral "{" rule:Expression ";" "}" => Requires { Text => text, Rule => rule, Otherwise => null }
            | TRequires text:TextLiteral "{" rule:Expression ";" "}" TOtherwise "{" otherwise:Expression ";" "}" => Requires { Text => text, Rule => rule, Otherwise => otherwise };
            
        syntax AuthorizationDeclaration
            = TAuthorization name:Identifier ":" interface:NamespacedTypeReference "{" operations:OperationAuthorizationDeclarations? "}" => Authorization { Name => name, Interface => interface, OperationAuthorizations => operations };
        syntax OperationAuthorizationDeclarations
            = oad:OperationAuthorizationDeclaration => [oad]
            | oads:OperationAuthorizationDeclarations oad:OperationAuthorizationDeclaration => [valuesof(oads),oad];
        syntax OperationAuthorizationDeclaration
            = operation:OperationSignature "{" oacds:OperationAuthorizationClaimDeclarations? oasds:OperationAuthorizationStatementDeclarations? "}" => OperationAuthorization { Operation => operation, OperationAuthorizationClaims => oacds, OperationAuthorizationStatements => oasds };
        syntax OperationAuthorizationStatementDeclarations
            = oasd:OperationAuthorizationStatementDeclaration => [oasd]
            | oasds:OperationAuthorizationStatementDeclarations oasd:OperationAuthorizationStatementDeclaration => [valuesof(oasds), oasd];
        syntax OperationAuthorizationStatementDeclaration
            = TDemand text:TextLiteral "{" rule:Expression ";" "}" => Demand { Text => text, Rule => rule };
        syntax OperationAuthorizationClaimDeclarations
            = oacd:OperationAuthorizationClaimDeclaration => [oacd]
            | oacds:OperationAuthorizationClaimDeclarations oacd:OperationAuthorizationClaimDeclaration => [valuesof(oacds), oacd];
        syntax OperationAuthorizationClaimDeclaration
            = il:IdentifierList id:Identifier ";"  => Reference { Type => il, Name => id };
        
        syntax BindingDeclaration
            = TBinding name:Identifier "{" transport:TransportBindingElement encoding:EncodingBindingElement protocols:ProtocolBindingElements? "}" => Binding { Name => name, Transport => transport, Encoding => encoding, Protocols => protocols };
        syntax ProtocolBindingElements
            = pbe:ProtocolBindingElement => [pbe]
            | pbes:ProtocolBindingElements pbe:ProtocolBindingElement => [valuesof(pbes), pbe];
        syntax TransportBindingElement
            = TTransport t:GeneralBindingElement ";" => t;
        syntax EncodingBindingElement
            = TEncoding e:GeneralBindingElement ";" => e;
        syntax ProtocolBindingElement
            = TProtocol p:GeneralBindingElement ";" => p;
        
        syntax GeneralBindingElement
            = name:Identifier => BindingElement { Name => name, Properties => null }
            | name:Identifier "{" properties:GeneralBindingElementProperties "}" => BindingElement { Name => name, Properties => properties };    
        syntax GeneralBindingElementProperties
            = p:GeneralBindingElementProperty => [p]
            | ps:GeneralBindingElementProperties "," p:GeneralBindingElementProperty => [valuesof(ps), p];
        syntax GeneralBindingElementProperty
            = name:Identifier "=" value:Expression => BindingElementProperty { Name => name, Value => value };

        syntax EndpointDeclaration
            = TEndpoint name:Identifier ":" interface:NamespacedTypeReference "{" properties:EndpointDeclarationProperties "}" => Endpoint { Name => name, Interface => interface, Properties => properties };
        syntax EndpointDeclarationProperties
            = binding:EndpointBindingDeclaration? authorization:EndpointAuthorizationDeclaration? contract:EndpointContractDeclaration? location:EndpointLocationDeclaration => { Binding => binding, Authorization => authorization, Contract => contract, Location => location };
        syntax EndpointBindingDeclaration
            = TBinding name:NamespacedTypeReference ";" => name;
        syntax EndpointContractDeclaration
            = TContract name:NamespacedTypeReference ";" => name;
        syntax EndpointAuthorizationDeclaration
            = TAuthorization name:NamespacedTypeReference ";" => name;
        syntax EndpointLocationDeclaration
            = TLocation url:TextLiteral ";" => url;

        syntax IdentifierList
            = id:Identifier => [id]
            | il:IdentifierList "." id:Identifier => [valuesof(il), id];
        syntax Identifiers
            =  id:Identifier => [id]
            | ids:Identifiers "," id:Identifier => [valuesof(ids), id];

        syntax ExpressionList
            = e:Expression => [e]
            | el:ExpressionList "," e:Expression => [valuesof(el), e]
            ;
        syntax Expression
            = e:LambdaExpression => e
            | e:ConditionalExpression => e
            ;
        syntax LambdaExpression
            = params:LambdaParameterDeclarations? "=>" e:Expression => LambdaExpression { Parameters => params, Body => e }
            | "(" params:LambdaParameterDeclarations? ")" "=>" e:Expression => LambdaExpression { Parameters => params, Body => e }
            | "(" params:LambdaParameterDeclarations? ")" "=>" "{" e:Expression "}" => LambdaExpression { Parameters => params, Body => e }
            ;
        syntax LambdaParameterDeclarations
            = pd:LambdaParameterDeclaration => [pd]
            | pds:LambdaParameterDeclarations "," pd:LambdaParameterDeclaration => [valuesof(pds), pd]
            ;
        syntax LambdaParameterDeclaration
            = name:Identifier => LambdaParameter { Name => name, Type => null }
            | type:TypeReference name:Identifier => LambdaParameter { Name => name, Type => type }
            ;
        syntax ConditionalExpression
            = test:NullCoalescingExpression "?" ifThen:Expression ":" ifElse:Expression => ConditionalExpression { Test => test, IfThen => ifThen, IfElse => ifElse }
            | e:NullCoalescingExpression => e
            ;
        syntax NullCoalescingExpression
            = l:ConditionalOrExpression "??" r:NullCoalescingExpression => BinaryExpression { NodeType => "Coalesce", Left => l, Right => r }
            | e:ConditionalOrExpression => e
            ;
        syntax ConditionalOrExpression
            = l:ConditionalOrExpression "||" r:ConditionalAndExpression => BinaryExpression { NodeType => "OrElse", Left => l, Right => r }
            | e:ConditionalAndExpression => e
            ;
        syntax ConditionalAndExpression
            = l:ConditionalAndExpression "&&" r:InclusiveOrExpression => BinaryExpression { NodeType => "AndAlso", Left => l, Right => r }
            | e:InclusiveOrExpression => e
            ;
        syntax InclusiveOrExpression
            = l:InclusiveOrExpression "|" r:ExclusiveOrExpression => BinaryExpression { NodeType => "Or", Left => l, Right => r }
            | e:ExclusiveOrExpression => e
            ;
        syntax ExclusiveOrExpression
            = l:ExclusiveOrExpression "^" r:AndExpression => BinaryExpression { NodeType => "ExclusiveOr", Left => l, Right => r }
            | e:AndExpression => e
            ;
        syntax AndExpression
            = l:AndExpression "&" r:EqualityExpression => BinaryExpression { NodeType => "And", Left => l, Right => r }
            | e:EqualityExpression => e
            ;
        syntax EqualityExpression
            = l:EqualityExpression "==" r:RelationalExpression => BinaryExpression { NodeType => "Equal", Left => l, Right => r }
            | l:EqualityExpression "!=" r:RelationalExpression => BinaryExpression { NodeType => "NotEqual", Left => l, Right => r }
            | e:RelationalExpression => e
            ;
        syntax RelationalExpression
            = l:RelationalExpression ">" r:ShiftExpression => BinaryExpression { NodeType => "GreaterThan", Left => l, Right => r }
            | l:RelationalExpression ">=" r:ShiftExpression => BinaryExpression { NodeType => "GreaterThanOrEqual", Left => l, Right => r }
            | l:RelationalExpression "<" r:ShiftExpression => BinaryExpression { NodeType => "LessThan", Left => l, Right => r }
            | l:RelationalExpression "<=" r:ShiftExpression => BinaryExpression { NodeType => "LessThanOrEqual", Left => l, Right => r }
            | e:RelationalExpression TIs tr:TypeReference => TypeBinaryExpression { Type => tr, Expression => e }
            | e:RelationalExpression TAs tr:TypeReference => UnaryExpression { NodeType => "TypeAs", Type => tr, Expression => e }
            | e:ShiftExpression => e
            ;
        syntax ShiftExpression
            = l:ShiftExpression "<<" r:AdditiveExpression => BinaryExpression { NodeType => "LeftShift", Left => l, Right => r }
            | l:ShiftExpression ">>" r:AdditiveExpression => BinaryExpression { NodeType => "RightShift", Left => l, Right => r }
            | e:AdditiveExpression => e
            ;
        syntax AdditiveExpression
            = l:AdditiveExpression "+" r:MultiplicativeExpression => BinaryExpression { NodeType => "Add", Left => l, Right => r }
            | l:AdditiveExpression "-" r:MultiplicativeExpression => BinaryExpression { NodeType => "Subtract", Left => l, Right => r }
            | e:MultiplicativeExpression => e
            ;
        syntax MultiplicativeExpression
            = l:MultiplicativeExpression "*" r:UnaryExpression => BinaryExpression { NodeType => "Multiply", Left => l, Right => r }
            | l:MultiplicativeExpression "/" r:UnaryExpression => BinaryExpression { NodeType => "Divide", Left => l, Right => r }
            | l:MultiplicativeExpression "%" r:UnaryExpression => BinaryExpression { NodeType => "Modulo", Left => l, Right => r }
            | e:UnaryExpression => e
            ;
        syntax UnaryExpression
            = "+" e:UnaryExpression => UnaryExpression { NodeType => "UnaryPlus", Type => null, Expression => e }
            | "-" e:UnaryExpression => UnaryExpression { NodeType => "Negate", Type => null, Expression => e }
            | "!" e:UnaryExpression => UnaryExpression { NodeType => "Not", Type => null, Expression => e }
            | "~" e:UnaryExpression => UnaryExpression { NodeType => "OnesComplement", Type => null, Expression => e }
            | "(" tr:TypeReference ")" e:UnaryExpression => UnaryExpression { NodeType => "Convert", Type => tr, Expression => e }
            | e:PrimaryExpression => e
            ;
        syntax PrimaryExpression
            = e:NewArrayExpression => e
            | e:PrimaryNoArrayCreationExpression => e
            ;
        syntax NewArrayExpression
            = TNew tr:SimpleTypeReference "[" ex:Expression? "]" => NewArrayExpression { NodeType => "NewArrayBounds", Type => tr, Expressions => [ex] }
            | TNew tr:SimpleTypeReference "[" "]" "{" el:ExpressionList? "}" => NewArrayExpression { NodeType => "NewArrayInit", Type => tr, Expressions => el }
            ;
        syntax PrimaryNoArrayCreationExpression
            = e:NewExpression => e
            | e:ConstantExpression => e
            | e:DefaultExpression => e
            | e:OldExpression => e
            | e:IdentifierExpression => e
            | e:IndexExpression => e
            | e:MemberExpression => e
            | e:MethodCallExpression => e
//          | e:InvocationExpression => e
            | e:BracketExpression => e
            ;
        syntax NewExpression
            = TNew tr:SimpleTypeReference "(" /*args:ExpressionList?*/ ")" => NewExpression { Type => tr, /*Arguments => args,*/ Members => null }
            | TNew tr:SimpleTypeReference "(" /*args:ExpressionList?*/ ")" "{" members:MemberInitExpressionList "}" => NewExpression { Type => tr, /*Arguments => args,*/ Members => members }
            ;
        syntax MemberInitExpressionList
            = e:MemberInitExpression => [e]
            | el:MemberInitExpressionList "," e:MemberInitExpression => [valuesof(el), e]
            ;
        syntax MemberInitExpression
            = name:Identifier "=" e:Expression => MemberInitExpression { Name => name, Value => e }
            ;
        syntax ConstantExpression
            = v:Literal => ConstantExpression { Value => v }
            ;
        syntax DefaultExpression
            = TDefault "(" tr:TypeReference ")" => DefaultExpression { Type => tr }
            ;
        syntax OldExpression
            = TOld "(" i:Identifier ")" => OldExpression { Name => i }
            ;
        syntax IdentifierExpression
            = i:Identifier => IdentifierExpression { Name => i }
            ;  
        syntax IndexExpression
            = obj:PrimaryNoArrayCreationExpression "[" arg:Expression "]" => IndexExpression { Object => obj, Argument => arg }
            ;
        syntax MemberExpression
            = obj:PrimaryExpression "." name:Identifier => MemberExpression { Object => obj, Name => name }
            ;
        syntax MethodCallExpression
            = obj:PrimaryExpression "." op:Identifier "(" args:ExpressionList? ")" => MethodCallExpression { Object => obj, Operation => op, Arguments => args }
            ;
//      syntax InvocationExpression
//          = e:PrimaryExpression "(" args:ExpressionList? ")" => InvocationExpression { Expression => e, Arguments => args }
//          ;
        syntax BracketExpression
            = "(" e:Expression ")" => e
            ;
            
        syntax BpelDeclaration
            = TBpel name:Identifier "{" body:BpelScopeBody "}" => BpelDeclaration { Name => name, Interfaces => null, Body => body }
            | TBpel name:Identifier ":" interfaces:Identifiers "{" body:BpelScopeBody "}" => BpelDeclaration { Name => name, Interfaces => interfaces, Body => body };
        syntax BpelVariables
            = bv:BpelVariable => [bv]
            | bvs:BpelVariables bv:BpelVariable => [valuesof(bvs), bv];
        syntax BpelVariable
            = type:TypeReference name:Identifier ";" => BpelVariableDeclaration { Name => name, Type => type, DefaultValue => null }
            | type:TypeReference name:Identifier "=" default:Expression ";" => BpelVariableDeclaration { Name => name, Type => type, DefaultValue => default };                    
        syntax BpelScopeActivity
            = "{" body:BpelScopeBody "}" => body;
        syntax BpelScopeBody
            = variables:BpelVariables? try:BpelTry?;
        syntax BpelTry
            = try:BpelTryBlock catches:BpelCatchBlocks? compensation:BpelCompensationBlock? termination:BpelTerminationBlock? events:BpelEventsBlock? => BpelTry { Try => try, Catches => catches, Compensation => compensation, Termination => termination, Events => events };
        syntax BpelTryBlock
            = TTry activities:BpelSequenceActivity => BpelTryBlock { Activities => activities };
        syntax BpelCatchBlocks
            = bcb:BpelCatchBlock => [bcb]
            | bcbs:BpelCatchBlocks bcb:BpelCatchBlocks => [valuesof(bcbs), bcb];
        syntax BpelCatchBlock
            = TCatch activities:BpelSequenceActivity => BpelCatchBlock { FaultType => null, FaultName => null, Activities => activities }
            | TCatch "(" type:TypeReference name:Identifier ")" activities:BpelSequenceActivity => BpelCatchBlock { FaultType => type, FaultName => name, Activities => activities };
        syntax BpelCompensationBlock
            = TCompensation activities:BpelSequenceActivity => BpelCompensationBlock { Activities => activities };
        syntax BpelTerminationBlock
            = TTermination activities:BpelSequenceActivity => BpelTerminationBlock { Activities => activities };
        syntax BpelEventsBlock
            = TEvents "{" events:BpelReceiveList? wait:BpelRepeatAlarm? "}" => BpelEventsBlock { Events => events, Wait => wait };
        syntax BpelRepeatAlarm
            = TWait TFor for:Expression activities:BpelSequenceActivity => BpelAlarm { For => for, Until => null, Repeat => null, Activities => activities }   
            | TWait TFor for:Expression TRepeat repeat:Expression activities:BpelSequenceActivity => BpelAlarm { For => for, Until => null, Repeat => repeat, Activities => activities }   
            | TWait TUntil until:Expression activities:BpelSequenceActivity => BpelAlarm { For => null, Until => until, Repeat => null, Activities => activities }   
            | TWait TUntil until:Expression TRepeat repeat:Expression activities:BpelSequenceActivity => BpelAlarm { For => null, Until => until, Repeat => repeat, Activities => activities };
        syntax BpelActivity
            = activity:BpelNonameActivity => BpelActivity { Name => null, Condition => null, Activity => activity }
            | name:Identifier ":" activity:BpelNonameActivity => BpelActivity { Name => name, Condition => null, Activity => activity }
            | name:Identifier TIf condition:Expression ":" activity:BpelNonameActivity => BpelActivity { Name => name, Condition => condition, Activity => activity };
        syntax BpelNonameActivity
            = ba:BpelInvokeActivity => ba
            | ba:BpelReceiveActivity => ba
            | ba:BpelReplyActivity => ba
            | ba:BpelThrowReplyActivity => ba
            | ba:BpelAssignActivity => ba
            | ba:BpelValidateActivity => ba             
            | ba:BpelThrowActivity => ba 
            | ba:BpelWaitActivity => ba
            | ba:BpelEmptyActivity => ba
            | ba:BpelExitActivity => ba
            | ba:BpelCompensateActivity => ba
            | ba:BpelPickActivity => ba
            | ba:BpelIfActivity => ba
            | ba:BpelWhileActivity => ba
            | ba:BpelRepeatUntilActivity => ba
            | ba:BpelForActivity => ba
            | ba:BpelSequenceActivity => ba
            | ba:BpelParallelActivity => ba
            | ba:BpelScopeActivity => ba
            ;
        syntax BpelInvokeActivity
            = TInvoke interface:TypeReference "." operation:Identifier "(" params:Identifiers? ")" ";" => BpelInvokeActivity { Interface => interface, Operation => operation, Params => params, Result => null }
            | TInvoke result:Identifier "=" interface:TypeReference "." operation:Identifier "(" params:Identifiers? ")" ";" => BpelInvokeActivity { Interface => interface, Operation => operation, Params => params, Result => result };
        syntax BpelReceiveActivity
            = TReceive interface:TypeReference "." operation:Identifier "(" params:BpelReceiveParams? ")" ";" => BpelReceiveActivity { Interface => interface, Operation => operation, Params => params, Instantiate => false }
            | TInstantiate TReceive interface:TypeReference "." operation:Identifier "(" params:BpelReceiveParams? ")" ";" => BpelReceiveActivity { Interface => interface, Operation => operation, Params => params, Instantiate => true };
        syntax BpelReplyActivity
            = TReply interface:TypeReference "." operation:Identifier "(" result:Identifier ")" ";" => BpelReplyActivity { Interface => interface, Operation => operation, Result => result };
        syntax BpelThrowReplyActivity
            = TThrow interface:TypeReference "." operation:Identifier "(" fault:Identifier ")" ";" => BpelThrowReplyActivity { Interface => interface, Operation => operation, Fault => fault };
        syntax BpelAssignActivity
            = TAssign assignments:BpelAssignment ";" => BpelAssignActivity { Assignments => assignments }
            | TAssign "{" assignments:BpelAssignmentStatements "}" => BpelAssignActivity { Assignments => assignments };
        syntax BpelAssignment
            = lhs:Expression "=" rhs:Expression => BpelAssignment { To => lhs, From => rhs };
        syntax BpelAssignmentStatements
            = ba:BpelAssignmentStatement => [ba]
            | bas:BpelAssignmentStatements ba:BpelAssignmentStatement => [valuesof(bas), ba];
        syntax BpelAssignmentStatement
            = ba:BpelAssignment ";" => ba;
        syntax BpelValidateActivity
            = TValidate variables:Identifiers ";" => BpelValidateActivity { Variables => variables };
        syntax BpelThrowActivity
            = TThrow ";" => BpelThrowActivity { Exception => null }
            | TThrow exception:Identifier ";" => BpelThrowActivity { Exception => exception };
        syntax BpelWaitActivity
            = TWait TFor for:TimeLiteral ";" => BpelWaitActivity { For => for, Until => null }   
            | TWait TUntil until:TimeLiteral ";" => BpelWaitActivity { For => null, Until => until };
        syntax BpelEmptyActivity
            = ";" => BpelEmptyActivity {};
        syntax BpelExitActivity
            = TExit ";" => BpelExitActivity {};
        syntax BpelCompensateActivity
            = TCompensate ";" => BpelCompensateActivity { Scope => null }
            | TCompensate scope:Identifier ";" => BpelCompensateActivity { Scope => scope };
        syntax BpelPickActivity
            = TPick "{" receives:BpelReceiveList? wait:BpelAlarm? "}" => BpelPickActivity { Receives => receives, Wait => wait, Instantiate => false }
            | TInstantiate TPick "{" receives:BpelReceiveList? wait:BpelAlarm? "}" => BpelPickActivity { Receives => receives, Wait => wait, Instantiate => true };
        syntax BpelReceiveList
            = br:BpelReceiveListItem => [br]
            | brl:BpelReceiveList br:BpelReceiveListItem => [valuesof(brl), br];
        syntax BpelReceiveListItem
            = TReceive interface:TypeReference "." operation:Identifier "(" params:BpelReceiveParams ")" ";" => BpelReceive { Interface => interface, Operation => operation, Params => params, Activities => null }
            | TReceive interface:TypeReference "." operation:Identifier "(" params:BpelReceiveParams ")" activities:BpelSequenceActivity => BpelReceive { Interface => interface, Operation => operation, Params => params, Activities => activities };
        syntax BpelReceiveParams
            = brp:BpelReceiveParam => [brp]
            | brps:BpelReceiveParams "," brp:BpelReceiveParam => [valuesof(brps), brp];
        syntax BpelReceiveParam
            = TOut variable:Identifier => BpelReceiveParam { Variable => variable };
        syntax BpelAlarm
            = TWait TFor for:Expression activities:BpelSequenceActivity => BpelAlarm { For => for, Until => null, Repeat => null, Activities => activities }   
            | TWait TUntil until:Expression activities:BpelSequenceActivity => BpelAlarm { For => null, Until => until, Repeat => null, Activities => activities };  
        syntax BpelIfActivity
            = precedence 2: TIf "(" condition:Expression ")" then:BpelActivity => BpelIfActivity { Condition => condition, Then => then, Else => null }
            | precedence 1: TIf "(" condition:Expression ")" then:BpelActivity TElse else:BpelActivity => BpelIfActivity { Condition => condition, Then => then, Else => else };
        syntax BpelWhileActivity
            = TWhile "(" condition:Expression ")" activity:BpelActivity => BpelWhileActivity { Condition => condition, Activity => activity };
        syntax BpelRepeatUntilActivity
            = TRepeat TUntil activity:BpelActivity condition:Expression => BpelRepeatActivity { Condition => condition, Activity => activity };
        syntax BpelForActivity
            = TFor "(" TInt varDef:Identifier "=" start:Expression ";" varUse1:Identifier "<=" end:Expression ";" varUse2:Identifier "++" ")" activity:BpelActivity => BpelForActivity { Variable => [varDef, varUse1, varUse2], Start => start, End => end, Parallel => false, Completed => null }
            | TFor "(" TInt varDef:Identifier "=" start:Expression ";" varUse1:Identifier "<=" end:Expression ";" "++" varUse2:Identifier ")" activity:BpelActivity => BpelForActivity { Variable => [varDef, varUse1, varUse2], Start => start, End => end, Parallel => false, Completed => null }
            | TParallel TFor "(" TInt varDef:Identifier "=" start:Expression ";" varUse1:Identifier "<=" end:Expression ";" varUse2:Identifier "++" ")" activity:BpelActivity => BpelForActivity { Variable => [varDef, varUse1, varUse2], Start => start, End => end, Parallel => true, Completed => null }
            | TParallel TFor "(" TInt varDef:Identifier "=" start:Expression ";" varUse1:Identifier "<=" end:Expression ";" "++" varUse2:Identifier ")" activity:BpelActivity => BpelForActivity { Variable => [varDef, varUse1, varUse2], Start => start, End => end, Parallel => true, Completed => null }
            | TFor "(" TInt varDef:Identifier "=" start:Expression ";" varUse1:Identifier "<=" end:Expression "||" TCompleted completed:Expression ";" varUse2:Identifier "++" ")" activity:BpelActivity => BpelForActivity { Variable => [varDef, varUse1, varUse2], Start => start, End => end, Parallel => false, Completed => completed }
            | TFor "(" TInt varDef:Identifier "=" start:Expression ";" varUse1:Identifier "<=" end:Expression "||" TCompleted completed:Expression ";" "++" varUse2:Identifier ")" activity:BpelActivity => BpelForActivity { Variable => [varDef, varUse1, varUse2], Start => start, End => end, Parallel => false, Completed => completed }
            | TParallel TFor "(" TInt varDef:Identifier "=" start:Expression ";" varUse1:Identifier "<=" end:Expression "||" TCompleted completed:Expression ";" varUse2:Identifier "++" ")" activity:BpelActivity => BpelForActivity { Variable => [varDef, varUse1, varUse2], Start => start, End => end, Parallel => true, Completed => completed }
            | TParallel TFor "(" TInt varDef:Identifier "=" start:Expression ";" varUse1:Identifier "<=" end:Expression "||" TCompleted completed:Expression ";" "++" varUse2:Identifier ")" activity:BpelActivity => BpelForActivity { Variable => [varDef, varUse1, varUse2], Start => start, End => end, Parallel => true, Completed => completed }
            | TForEach "(" type:TypeReference var:Identifier TIn collection:Expression")" activity:BpelActivity => BpelForEachActivity { Variable => var, ItemType => type, Collection => collection, Parallel => false }
            | TParallel TForEach "(" type:TypeReference var:Identifier TIn collection:Expression")" activity:BpelActivity => BpelForEachActivity { Variable => var, ItemType => type, Collection => collection, Parallel => true }
            | TForEach "(" TVar var:Identifier TIn collection:Expression")" activity:BpelActivity => BpelForEachActivity { Variable => var, ItemType => null, Collection => collection, Parallel => false }
            | TParallel TForEach "(" TVar var:Identifier TIn collection:Expression")" activity:BpelActivity => BpelForEachActivity { Variable => var, ItemType => null, Collection => collection, Parallel => true };
        syntax BpelSequenceActivity
            = "{" activities:BpelSequence? "}" => BpelSequenceActivity { Activities => activities };
        syntax BpelSequence
            = ba:BpelActivity => [ba]
            | bas:BpelSequence ba:BpelActivity => [valuesof(bas), ba];
        syntax BpelParallelActivity
            = TParallel "{" "}" => BpelParallelActivity { Activities => [BpelEmptyActivity {}] }
            | TParallel "{" activities:BpelSequence links:BpelParallelLinks? "}" => BpelParallelActivity { Activities => activities, Links => links };
        syntax BpelParallelLinks
            = TLinks "{" ll:BpelLinks "}" => ll;
        syntax BpelLinks
            = bl:BpelLink => [bl]
            | bls:BpelLinks bl:BpelLink => [valuesof(bls), bl];
        syntax BpelLink
            = name:Identifier ":" from:Identifier TTo to:Identifier ";" => BpelLink { Name => name, Condition => null, From => from, To => to }
            | name:Identifier TIf condition:Expression ":" from:Identifier TTo to:Identifier ";" => BpelLink { Name => name, Condition => condition, From => from, To => to };

        //Keywords:
        @{Classification["Keyword"]}
        token TTrue = "true";
        @{Classification["Keyword"]}
        token TFalse = "false";
        @{Classification["Keyword"]}
        token TNull = "null";
        @{Classification["Keyword"]}
        token TNew = "new";
        @{Classification["Keyword"]}
        token TAs = "as";
        @{Classification["Keyword"]}
        token TIs = "is";
        @{Classification["Keyword"]}
        token TDefault = "default";
        @{Classification["Keyword"]}
        token TNamespace = "namespace";
        @{Classification["Keyword"]}
        token TUsing = "using";
        @{Classification["Keyword"]}
        token TStruct = "struct";
        @{Classification["Keyword"]}
        token TException = "exception";
        @{Classification["Keyword"]}
        token TEnum = "enum";
        @{Classification["Keyword"]}
        token TInterface = "interface";
        @{Classification["Keyword"]}
        token TBinding = "binding";
        @{Classification["Keyword"]}
        token TEndpoint = "endpoint";
        @{Classification["Keyword"]}
        token TContract = "contract";

        @{Classification["Keyword"]}
        token TVoid = "void";
        @{Classification["Keyword"]}
        token TAsync = "asynchronous";
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
        token TGuid = "Guid";
        @{Classification["Keyword"]}
        token TDate = "Date";
        @{Classification["Keyword"]}
        token TTime = "Time";
        @{Classification["Keyword"]}
        token TDateTime = "DateTime";
        @{Classification["Keyword"]}
        token TTimeSpan = "TimeSpan";

        @{Classification["Keyword"]}
        token TThrows = "throws";
        @{Classification["Keyword"]}
        token TTransport = "transport";
        @{Classification["Keyword"]}
        token TCustom = "custom";
        @{Classification["Keyword"]}
        token TEncoding = "encoding";
        @{Classification["Keyword"]}
        token TProtocol = "protocol";
        @{Classification["Keyword"]}
        token TLocation = "location";

        @{Classification["Keyword"]}
        token TClaimset = "claimset";
        @{Classification["Keyword"]}
        token TAuthorization = "authorization";
        @{Classification["Keyword"]}
        token TDemand = "demand";
        @{Classification["Keyword"]}
        token TRole = "role";
        @{Classification["Keyword"]}
        token TUri = "Uri";

        @{Classification["Keyword"]}
        token TBpel = "bpel";
        @{Classification["Keyword"]}
        token TTry = "try";
        @{Classification["Keyword"]}
        token TCatch = "catch";
        @{Classification["Keyword"]}
        token TEvents = "events";
        @{Classification["Keyword"]}
        token TReceive = "receive";
        @{Classification["Keyword"]}
        token TWait = "wait";
        @{Classification["Keyword"]}
        token TFor = "for";
        @{Classification["Keyword"]}
        token TWhile = "while";
        @{Classification["Keyword"]}
        token TUntil = "until";
        @{Classification["Keyword"]}
        token TRepeat = "repeat";
        @{Classification["Keyword"]}
        token TCompensation = "compensation";
        @{Classification["Keyword"]}
        token TAssign = "assign";
        @{Classification["Keyword"]}
        token TThrow = "throw";
        @{Classification["Keyword"]}
        token TValidate = "validate";
        @{Classification["Keyword"]}
        token TExit = "exit";
        @{Classification["Keyword"]}
        token TCompensate = "compensate";
        @{Classification["Keyword"]}
        token TPick = "pick";
        @{Classification["Keyword"]}
        token TInvoke = "invoke";
        @{Classification["Keyword"]}
        token TReply = "reply";
        @{Classification["Keyword"]}
        token TInstantiate = "instantiate";
        @{Classification["Keyword"]}
        token TIf = "if";
        @{Classification["Keyword"]}
        token TElse = "else";
        @{Classification["Keyword"]}
        token TParallel = "parallel";
        @{Classification["Keyword"]}
        token TForEach = "foreach";
        @{Classification["Keyword"]}
        token TVar = "var";
        @{Classification["Keyword"]}
        token TIn = "in";
        @{Classification["Keyword"]}
        token TLinks = "links";
        @{Classification["Keyword"]}
        token TTo = "to";
        @{Classification["Keyword"]}
        token TCompleted = "completed";
        @{Classification["Keyword"]}
        token TTermination = "termination";
        @{Classification["Keyword"]}
        token TOut = "out";

        @{Classification["Keyword"]}
        token TRequires = "requires";
        @{Classification["Keyword"]}
        token TEnsures = "ensures";
        @{Classification["Keyword"]}
        token TOtherwise = "otherwise";
        @{Classification["Keyword"]}
        token TOld = "old";
        @{Classification["Keyword"]}

        // General syntax elements:
        interleave Whitespace
            = WhitespaceCharacter+;
        token WhitespaceCharacter
            = "\u0009" // Horizontal Tab
            | "\u000B" // Vertical Tab
            | "\u000C" // Form Feed
            | "\u0020" // Space
            | NewLineCharacter;
        token NewLineCharacter
            = "\u000A" // New Line
            | "\u000D" // Carriage Return
            | "\u0085" // Next Line
            | "\u2028" // Line Separator
            | "\u2029"; // Paragraph Separator
        interleave Comment
            = CommentDelimited
            | CommentLine;
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
            = d:DateLiteral => Date { StringValue => d }
            | t:TimeLiteral => Time { StringValue => t }
            | dt:DateTimeLiteral => DateTime { StringValue => dt }
            | dto:DateTimeOffsetLiteral => TimeSpan { StringValue => dto }
            | g:GuidLiteral => Guid { StringValue => g }
            | i:IntegerLiteral => Integer { StringValue => i }
            | d:DecimalLiteral => Float { StringValue => d }
            | s:ScientificLiteral => Float { StringValue => s }
            | l:LogicalLiteral => Boolean { StringValue => l }
            | t:TextLiteral => String { StringValue => t }
            | n:NullLiteral => Null { StringValue => n };
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
        token VersionLiteral = DecimalLiteral;
   }
}