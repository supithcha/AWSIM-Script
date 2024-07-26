grammar AWSIMScriptGrammar;

scenario
    : (statement)+ EOF;

statement
    : assignmentStm
    | function ';' ;

assignmentStm
    : variableExp '=' expression ';';

expression
    : STRING
    | positionExp
    | routeExp
    | arrayExp
    | variableExp
    | function;

function
    : ID '(' argumentList? ')' ;

argumentList
    : expression ( ',' expression )*
    ;

arrayExp
    : '[' argumentList? ']';

// to specify a (2D) position
// e.g., "TrafficLane.239" at 3.5 expresses the position on lane 239, 3.5m from the starting point of the lane.
positionExp
    : lanePositionExp;
lanePositionExp
    : STRING ('at' NUMBER)?;

routeExp
    : STRING ('with-speed-limit' NUMBER)?;

// variable name, e.g., npc1
variableExp: ID;

// number and string data types
STRING : '"' .*? '"';
SIGN
    : ('+' | '-');
NUMBER
    : SIGN? ( [0-9]* '.' )? [0-9]+;

ID  : [a-zA-Z_] [a-zA-Z0-9_]*;

// ignore space(s)
WS  : (' '|'\t'|'\r'|'\n')+ -> skip;

LINE_COMMENT
    : '//' ~[\r\n]* -> skip;