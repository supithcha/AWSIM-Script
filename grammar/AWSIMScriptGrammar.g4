grammar AWSIMScriptGrammar;

// We assume that the
// e.g., run(npc1, npc2)
// to spawn NPC: vehicle type, initial position, goal position, and (OPTIONAL) routes

scenario
    : (statement)+ EOF;

statement
    : assignmentStm
    | function ';' ;

assignmentStm
    : variableExp '=' expression ';';

expression
    : vehicleTypeExp
    | positionExp
    | routesExp
    | variableExp 
    | function;

function
    : ID '(' argumentList? ')'
    ;

argumentList
    : expression ( ',' expression )*
    ;

vehicleTypeExp
    : STRING;

// to specify a (2D) position
// e.g., "TrafficLane.239" at 3.5 expresses the position on lane 239, 3.5m from the starting point of the lane.
positionExp
    : lanePositionExp;
lanePositionExp
    : laneExp
    | laneExp 'at' offsetExp;
laneExp
    : STRING;
offsetExp
    : NUMBER;

// routes
routesExp
    : '[' routeExp (',' routeExp)* ']'
    | '[' ']';
routeExp
    : laneExp ('with-speed-limit' speedExp)?;
speedExp
    : NUMBER;

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


// TODO: Add comments