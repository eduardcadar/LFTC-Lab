%{ 				
#include <math.h>
#include <string.h>
#include <stdlib.h>
#include "parser.tab.c"

const int noLexicalAtoms = 37;
const char* lexicalAtoms[] = {
    "ID", "CONSTINT", "CONSTREAL", "CONSTTEXT",
    "int", "double", "string", "struct",
    "Console.WriteLine", "Console.ReadLine", "+", "-",
    "*", "/", "=", "<", ">", "<=", ">=", "==", "!=",
    "true", "false", "||", "&&", "if", "else", "while",
    ";", ",", "(", ")", "{", "}",
    "repeta", "panacand", "sfrepeta"
};
const int noSymbolTypes = 4;
const char* FIPFilePath = "./FIP.txt";
const char* TSFilePath = "./TS.txt";

int atomIndex(char* atomText) {
    for (int i = 0; i < noLexicalAtoms; i++)
        if (strcmp(atomText, lexicalAtoms[i]) == 0)
            return i;
    return -1;
}

#define VECTOR_INIT_CAPACITY 4
struct vectorTS {
    char** items;
    int size, capacity;
};

struct vectorTS TS;
int columnNo;

void vectorInitTS(struct vectorTS *v) {
    v->capacity = VECTOR_INIT_CAPACITY;
    v->size = 0;
    v->items = malloc(sizeof(char*) * v->capacity);
}

int vectorGetIndexTS(struct vectorTS *v, char* element) {
    for (int i = 0; i < v->size; i++)
        if (strcmp(v->items[i], element) == 0)
            return i;
    return -1;
}

void vectorFreeTS(struct vectorTS *v) {
    for (int i = 0; i < v->size; i++)
        free(v->items[i]);
    free(v->items);
}

void vectorResizeTS(struct vectorTS* v) {
    int newCapacity = 2 * v->capacity;
    char** newItems = malloc(newCapacity * sizeof(char*));
    for (int i = 0; i < v->size; i++) {
        newItems[i] = malloc((strlen(v->items[i]) + 1) * sizeof(char));
        strcpy(newItems[i], v->items[i]);
    }
    v->capacity = newCapacity;
    vectorFreeTS(v);
    v->items = newItems;
}

// returns index of item added
int vectorPushBackTS(struct vectorTS* v, char* element) {
    if (v->size == v->capacity)
        vectorResizeTS(v);
    v->items[v->size] = malloc((strlen(element) + 1) * sizeof(char));
    strcpy(v->items[v->size], element);
    v->items[v->size][strlen(element)] = 0;
    v->size++;
    return v->size - 1;
}

void appendToFIPFile(int typeID, int symbolID) {
    FILE* file = fopen(FIPFilePath, "a");
    fprintf(file, "%d %d\n", typeID, symbolID);
    fclose(file);
}

void appendToTSFile(int symbolID, char* symbol) {
    FILE* file = fopen(TSFilePath, "a");
    fprintf(file, "%d %s\n", symbolID, symbol);
    fclose(file);
}

void solveAtom(int typeID, char* atomText) {
    int symbolID = vectorGetIndexTS(&TS, atomText);
    if (typeID < noSymbolTypes) {
        if (symbolID < 0) {
            symbolID = vectorPushBackTS(&TS, atomText);
            appendToTSFile(symbolID, atomText);
        }
    }
    appendToFIPFile(typeID, symbolID);
}

%}

%option noyywrap
%option yylineno

STRING \"[^\"]*\"
KEYWORD int|double|string|struct|if|else|while|Console.ReadLine|Console.WriteLine
WRONGID [a-zA-Z][^; \"{}(),+*/<>=!|&\n-]*[^a-zA-Z; \"{}(),+*/<>=!|&\n-][^; \"{}(),+*/<>=!|&\n-]*
ID [a-z][a-zA-Z]*
NUME_STRUCT [A-Z][a-zA-Z]*
DIGIT [0-9]
DIGIT_ [0-9_]
BINARY_DIGIT [01]
BINARY_DIGIT_ [01_]
HEXA_DIGIT [0-9a-fA-F]
HEXA_DIGIT_ [0-9a-fA-F_]
NUMBER ([0-9][0-9_]*[0-9])|[0-9]
DELIMITER ";"|"{"|"}"|"("|")"|","
OPERATOR_ONE_CHAR "+"|"-"|"*"|"/"|"<"|">"|"="|"!"
NEWLINE [\n]
WHITESPACE [ \t]+

%%

{STRING} {
    // CONSTTEXT
    // int typeID = atomIndex("CONSTTEXT");
    // solveAtom(typeID, yytext);
    // printf("string: %s atomID: %d\n", yytext, typeID);
    // columnNo += strlen(yytext);
    strcpy(yylval.text, yytext);
    return CONST_TEXT;
}

repeta {
    return REPETA;
}

panacand {
    return PANACAND;
}

sfrepeta {
    return SFREPETA;
}

int {
    return INT;
}

double {
    return DOUBLE;
}

string {
    return STRING;
}

struct {
    return STRUCT;
}

if {
    return IF;
}

else {
    return ELSE;
}

while {
    return WHILE;
}

Console.ReadLine {
    return CONSREADLINE;
}

Console.WriteLine {
    return CONSWRITELINE;
}

{KEYWORD} {
    // int typeID = atomIndex(yytext);
    // solveAtom(typeID, yytext);
    // printf("keyword: %s atomID: %d\n", yytext, typeID);
    // columnNo += strlen(yytext);
}

true {
    return VAL_TRUE;
}

false {
    return VAL_FALSE;
}

{WRONGID} {
    printf("unrecognized: %s\n", yytext);
    exit(0);
}

({DIGIT}+\.{DIGIT}{DIGIT_}*{DIGIT}(e[+-]{NUMBER}?)?[fF]?)|({DIGIT}{DIGIT_}*{DIGIT}\.{DIGIT}{DIGIT_}*{DIGIT}(e[+-]{NUMBER}?)?[fF]?)|({DIGIT}+\.{DIGIT}*(e[+-]{NUMBER}?)?[fF]?) {
    // CONSTREAL
    // int typeID = atomIndex("CONSTREAL");
    // solveAtom(typeID, yytext);
    // printf("constreal: %s atomID: %d\n", yytext, typeID);
    // columnNo += strlen(yytext);
    char *ptr;
    yylval.number = strtod(yytext, &ptr);
    return CONST_NUMBER;
}

(0b{BINARY_DIGIT_}*{BINARY_DIGIT})|(0x{HEXA_DIGIT_}*{HEXA_DIGIT})|({DIGIT}{DIGIT_}*{DIGIT})|({DIGIT}) {
    // CONSTINT
    // int typeID = atomIndex("CONSTINT");
    // solveAtom(typeID, yytext);
    // printf("constint: %s atomID: %d\n", yytext, typeID);
    // columnNo += strlen(yytext);
    yylval.number = atoi(yytext);
    return CONST_NUMBER;
}

{ID} {
    // if (strlen(yytext) > 250) {
    //     printf("ID too long on line %d, column %d", yylineno, columnNo);
    //     exit(0);
    // }
    // int typeID = atomIndex("ID");
    // solveAtom(typeID, yytext);
    // printf("identifier: %s atomID: %d\n", yytext, typeID);
    // columnNo += strlen(yytext);
    strcpy(yylval.text, yytext);
    return ID;
}

{NUME_STRUCT} {
    strcpy(yylval.text, yytext);
    return NUME_STRUCT;
}

{DELIMITER} {
    // int typeID = atomIndex(yytext);
    // solveAtom(typeID, yytext);
    // printf("delimiter: %s atomID: %d\n", yytext, typeID);
    // columnNo += strlen(yytext);
    return yytext[0];
}

== {
    return OPERATOR_EQ;
}

!= {
    return OPERATOR_NOT_EQ;
}

"<=" {
    return OPERATOR_LO_EQ;
}

>= {
    return OPERATOR_GR_EQ;
}

&& {
    return OPERATOR_AND;
}

"||" {
    return OPERATOR_OR;
}

{OPERATOR_ONE_CHAR} {
    // int typeID = atomIndex(yytext);
    // solveAtom(typeID, yytext);
    // printf("operator: %s atomID: %d\n", yytext, typeID);
    // columnNo += strlen(yytext);
    return yytext[0];
}

{NEWLINE} {
    // columnNo = 0;
    // return '\n';
}

{WHITESPACE} {
    // columnNo += strlen(yytext);
}

. {
    // Unrecognized
    printf("Unrecognized character: %s\n", yytext);
    exit(0);
}

%%
// int main(argc, argv)
// int argc;
// char **argv;
// {
//     ++argv, --argc; /* skip over program name */
//     if (argc > 0) 
//     	yyin = fopen(argv[0], "r");
//     else
//      	yyin = stdin;
//     FILE* file = fopen(FIPFilePath, "w");
//     fclose(file);
//     FILE* file2 = fopen(TSFilePath, "w");
//     fclose(file2);
//     columnNo = 0;
//     vectorInitTS(&TS);
//     yylex();
//     vectorFreeTS(&TS);
// }