%{ 				
#include <string.h>
#include <stdlib.h>
#include "attrib.h"
#include "lab6-parser.tab.h"
%}

%option noyywrap
%option yylineno

KEYWORD int|Console.ReadLine|Console.WriteLine
WRONGID [a-zA-Z][^; \"{}(),+*/<>=!|&\n-]*[^a-zA-Z; \"{}(),+*/<>=!|&\n-][^; \"{}(),+*/<>=!|&\n-]*
ID [a-z][a-zA-Z]*
DIGIT [0-9]
DELIMITER ";"|"{"|"}"|"("|")"|","
OPERATOR_ONE_CHAR "+"|"-"|"*"|"/"|"="
NEWLINE [\n]
WHITESPACE [ \t]+

%%

int {
    return INT;
}

Console.ReadLine {
    return CONSREADLINE;
}

Console.WriteLine {
    return CONSWRITELINE;
}

{KEYWORD} {
}

{WRONGID} {
    printf("unrecognized: %s\n", yytext);
    exit(0);
}

{DIGIT}+ {
    // CONSTINT
    strcpy(yylval.varname, yytext);
    return CONST_NUMBER;
}

{ID} {
    strcpy(yylval.varname, yytext);
    return ID;
}

{DELIMITER} {
    return yytext[0];
}

{OPERATOR_ONE_CHAR} {
    return yytext[0];
}

{NEWLINE} {
}

{WHITESPACE} {
}

. {
    // Unrecognized
    printf("Unrecognized character: %s\n", yytext);
    exit(0);
}

%%
