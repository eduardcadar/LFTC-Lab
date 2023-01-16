#ifndef CODEASM_H
#define CODEASM_H

#define PROGRAM_HEADER "\
bits 32 \n\
global start \n\
extern exit, printf, scanf \n\
import exit msvcrt.dll \n\
import printf msvcrt.dll \n\
import scanf msvcrt.dll \n\
"

#define DATA_SEGMENT "\n\
segment data use32 class=data\n\
formatRead db 37, 100, 0\n\
formatWrite db 37, 100, 10, 13, 0\n\
temp dd 0\n\
%s\n\
"

#define DECLARE_INT_FORMAT "\
%s dw 0\n\
"

#define CODE_SEGMENT "\
segment code use32 class=code\n\
start:\n\
%s\n\
push dword 0\n\
call [exit]\n\
"

#define ASSIGN_FORMAT "\
mov ax, %s\n\
mov %s, ax\n\
"

#define ADD_FORMAT "\
mov ax, %s\n\
add ax, %s\n\
mov %s, ax\n\
"

#define SUB_FORMAT "\
mov ax, %s\n\
sub ax, %s\n\
mov %s, ax\n\
"

#define MUL_FORMAT "\
mov ax, %s\n\
mov dx, %s\n\
imul dx\n\
mov %s, ax\n\
"

#define DIV_FORMAT "\
mov ax, %s\n\
mov dx, 0\n\
mov cx, %s\n\
idiv cx\n\
mov %s, ax\n\
"

#define PRINT_FORMAT "\
mov eax, 0\n\
mov ax, %s\n\
push eax\n\
push dword formatWrite\n\
call [printf]\n\
add esp, 4 * 2\n\
"

#define READ_FORMAT "\
push dword temp\n\
push dword formatRead\n\
call [scanf]\n\
add esp, 4 * 2\n\
mov eax, [temp]\n\
mov %s, ax\n\
"

#endif