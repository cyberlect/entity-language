grammar EntityLanguage;

start      : 'module' ID definition* ;
definition : 'entity' ID '{' property* '}' ;
property   : ID ':' type ;
type       : ID ;

ID         : [a-zA-Z_\-][a-zA-Z0-9_\-]* ;
WS         : [ \t\r\n]+ -> skip ;