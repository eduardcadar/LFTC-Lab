all:
	yacc -d parser.y
	flex tema3.lex
	gcc lex.yy.c parser.tab.h -o exe