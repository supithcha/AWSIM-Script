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
    | spawnDelayOptionExp
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

spawnDelayOptionExp
    : 'delay' '(' NUMBER ')'
    | 'delay-movement' '(' NUMBER ')'
    | 'delay-until-ego-move' '(' NUMBER ')'
    | 'delay-until-ego-engaged' '(' NUMBER ')';

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


// non-moving NPC
// spawn an NPC with 5 seconds delay (i.e., spawn at time 5)
// spawn an NPC and delay its movement by 5 seconds
// spawn an NPC and make it move at 1 second after the Ego moves
// spawn an NPC, delay its movement, and make it move when the Ego engage
