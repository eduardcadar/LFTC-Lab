bits 32 
global start 
extern exit, printf, scanf 
import exit msvcrt.dll 
import printf msvcrt.dll 
import scanf msvcrt.dll 

segment data use32 class=data
formatRead db 37, 100, 0
formatWrite db 37, 100, 10, 13, 0
temp dd 0
a dw 0
b dw 0
c dw 0
temp1 dw 0
temp2 dw 0

segment code use32 class=code
start:
mov ax, 25
mov [a], ax
push dword temp
push dword formatRead
call [scanf]
add esp, 4 * 2
mov eax, [temp]
mov [b], ax
mov ax, 3
mov dx, [b]
imul dx
mov [temp1], ax
mov ax, [a]
add ax, [temp1]
mov [temp2], ax
mov ax, [temp2]
mov [c], ax
mov eax, 0
mov ax, [c]
push eax
push dword formatWrite
call [printf]
add esp, 4 * 2
mov eax, 0
mov ax, [a]
push eax
push dword formatWrite
call [printf]
add esp, 4 * 2

push dword 0
call [exit]
