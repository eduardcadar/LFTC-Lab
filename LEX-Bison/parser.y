%{
#include <stdio.h>
int yylex(void);
void yyerror(char const *s) {
    printf ("%s\n", s);
}
%}

%union {
    char text[300];
    double number;
}

%token ID
%token CONST_TEXT
%token CONST_NUMBER
%token NUME_STRUCT

%token INT
%token DOUBLE
%token STRING
%token STRUCT
%token IF
%token ELSE
%token WHILE
%token CONSREADLINE
%token CONSWRITELINE

%token VAL_TRUE
%token VAL_FALSE

%token OPERATOR_EQ
%token OPERATOR_NOT_EQ
%token OPERATOR_LO_EQ
%token OPERATOR_GR_EQ
%token OPERATOR_AND
%token OPERATOR_OR

%%

program : lista_instr { printf("GOOD FILE FORMAT\n"); }
;
lista_instr : instr
    | instr lista_instr
;
instr : decl
    | decl_struct
    | atribuire
    | instr_cond
    | instr_cicl
    | instr_io
;

instr_io : instr_read ';'
    | instr_write ';'
;
instr_read : CONSREADLINE '(' ID ')'
;
instr_write : CONSWRITELINE '(' text ')'
;
text : text_part
    | text_part '+' text
;
text_part : ID
    | ID '.' ID
    | CONST_TEXT
;

decl : tip lista_ID ';'
;
tip : INT
    | DOUBLE
    | STRING
    | NUME_STRUCT
;
lista_ID : ID ',' lista_ID
    | ID
;

decl_struct : STRUCT NUME_STRUCT '{' lista_decl '}'
;
lista_decl : decl
    | decl lista_decl
;

atribuire : ID '=' expr ';'
    | ID '.' ID '=' expr ';'
;
expr : operand
    |  operand operatie expr
;
operand : ID
    | ID '.' ID
    | CONST_NUMBER
    | CONST_TEXT
;
operatie : '+'
    | '-'
    | '*'
    | '/'
;

instr_cond : IF '(' lista_cond ')' '{' lista_instr '}'
    | IF '(' lista_cond ')' '{' lista_instr '}' ELSE '{' lista_instr '}'
;
lista_cond : cond
    | cond op_logic lista_cond
;
cond : expr rel expr | val_logica
;
rel : '<'
    | '>'
    | OPERATOR_EQ
    | OPERATOR_NOT_EQ
    | OPERATOR_LO_EQ
    | OPERATOR_GR_EQ
;
val_logica : VAL_TRUE
    | VAL_FALSE
;
op_logic : OPERATOR_OR
    | OPERATOR_AND
;

instr_cicl : WHILE '(' lista_cond ')' '{' lista_instr '}'
;

%%

int main() {
    yyparse();

    return 0;
}