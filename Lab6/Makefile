all:
	yacc -d lab6-parser.y
	flex lab6.lex
	gcc lex.yy.c lab6-parser.tab.c -o exe