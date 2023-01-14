%{
#include <stdio.h>
#include <string.h>
#include "attrib.h"
#include "codeASM.h"

int yylex(void);
void yyerror(char const *s) {
  printf ("%s\n", s);
}

char data_segment_buffer[10000];
char code_segment_buffer[10000];
char temp[1000];

int tempnr = 1;
void newTempName(char* s) {
  sprintf(s, "temp%d", tempnr);
  tempnr++;
}

%}

%union {
    char varname[10];
    attributes att;
}

%token INT
%token CONSREADLINE
%token CONSWRITELINE
%token <varname> ID
%token <varname> CONST_NUMBER

%type <att> operand
%type <att> expr

%%

program : lista_instr {
    printf("GOOD FILE FORMAT\n");
    
    FILE *ptr;
    ptr = fopen("C:\\facultate\\Semestrul 5\\LFTC\\LFTC-Lab\\Lab6\\program.asm", "w");
    fprintf(ptr, PROGRAM_HEADER);
    fprintf(ptr, DATA_SEGMENT, data_segment_buffer);
    fprintf(ptr, CODE_SEGMENT, code_segment_buffer);
    fclose(ptr);
}
;
lista_instr : instr
    | instr lista_instr
;
instr : decl
    | atribuire
    | instr_io
;

instr_io : instr_read ';'
    | instr_write ';'
;
instr_read : CONSREADLINE '(' ID ')' {
    sprintf(temp, "[%s]", $3);
    strcpy($3, temp);
    sprintf(temp, READ_FORMAT, $3);
    strcat(code_segment_buffer, temp);
  }
;
instr_write : CONSWRITELINE '(' ID ')' {
    sprintf(temp, "[%s]", $3);
    strcpy($3, temp);
    sprintf(temp, PRINT_FORMAT, $3);
    strcat(code_segment_buffer, temp);
  }
;

decl : INT ID ';' {
    sprintf(temp, DECLARE_INT_FORMAT, $2);
    strcat(data_segment_buffer, temp);
  }
;

atribuire : ID '=' expr ';' {
    char temp2[100];
    sprintf(temp2, "[%s]", $1);
    sprintf(temp, ASSIGN_FORMAT, $3.varn, temp2);
    strcat(code_segment_buffer, temp);
}
;
expr : operand {
        strcpy($$.varn, $1.varn);
    }
    | operand '+' expr {
        newTempName($$.varn);
        sprintf(temp, DECLARE_INT_FORMAT, $$.varn);
        strcat(data_segment_buffer, temp);
        sprintf(temp, "[%s]", $$.varn);
        strcpy($$.varn, temp);
        sprintf(temp, ADD_FORMAT, $1.varn, $3.varn, $$.varn);
        strcat(code_segment_buffer, temp);
    }
    | operand '-' expr {
        newTempName($$.varn);
        sprintf(temp, DECLARE_INT_FORMAT, $$.varn);
        strcat(data_segment_buffer, temp);
        sprintf(temp, "[%s]", $$.varn);
        strcpy($$.varn, temp);
        sprintf(temp, SUB_FORMAT, $1.varn, $3.varn, $$.varn);
        strcat(code_segment_buffer, temp);
    }
    | operand '*' expr {
        newTempName($$.varn);
        sprintf(temp, DECLARE_INT_FORMAT, $$.varn);
        strcat(data_segment_buffer, temp);
        sprintf(temp, "[%s]", $$.varn);
        strcpy($$.varn, temp);
        sprintf(temp, MUL_FORMAT, $1.varn, $3.varn, $$.varn);
        strcat(code_segment_buffer, temp);
    }
    | operand '/' expr {
        newTempName($$.varn);
        sprintf(temp, DECLARE_INT_FORMAT, $$.varn);
        strcat(data_segment_buffer, temp);
        sprintf(temp, "[%s]", $$.varn);
        strcpy($$.varn, temp);
        sprintf(temp, DIV_FORMAT, $1.varn, $3.varn, $$.varn);
        strcat(code_segment_buffer, temp);
    }
;
operand : ID {
        sprintf($$.varn, "[%s]", $1);
    }
    | CONST_NUMBER {
        sprintf($$.varn, $1);
    }
;

%%

int main() {
    yyparse();

    return 0;
}