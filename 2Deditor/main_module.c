#include <stdio.h>
#include <stdlib.h> 
#include <ncurses.h>
#include "main_module.h"
#include "object_module.h"
int main()
{
    init_ncurses();
    MEVENT event;
    WINDOW* buf = newwin(SCREEN_HEIGHT + 2, SCREEN_WIDTH + 2, 0, 0);
    int mx=0, my=0;

    struct box box1=object_init();
    int dragging=0;
    int input;
    while((input=getch())!=27){
        int offsetX;
        int offsetY;
        if(getmouse(&event)==OK){
            if(event.bstate & REPORT_MOUSE_POSITION && event.x<SCREEN_WIDTH && event.y<SCREEN_HEIGHT){
                mx=event.x;
                my=event.y;
            }
            if(event.bstate & BUTTON1_PRESSED &&
                event.x<SCREEN_WIDTH && event.y<SCREEN_HEIGHT){
                dragging=1;
                offsetX = mx - box1.x;
                offsetY = my - box1.y;

            }
            if(event.bstate & BUTTON1_RELEASED &&
                event.x<SCREEN_WIDTH && event.y<SCREEN_HEIGHT){
                    dragging=0;
            }
        }

        if(cursor_inside_object(box1, mx, my) && dragging){
            box1.selected=1;
            // int cursor_offset_x=curx-box1.x;
            // int cursor_offset_y=cury-box1.y;
            move_object(&box1, mx-offsetX, my-offsetY);
        }
        else{
            box1.selected=0;
        }
        werase(buf);

        draw_object(buf, box1);
        draw_screen_borders(buf, mx, my);

        mvwprintw(buf, SCREEN_HEIGHT/2, SCREEN_WIDTH/2, ".");
        
        wnoutrefresh(buf);
        doupdate();

        napms(2);
    }
    printf("\033[?1003l\n"); // выключает режим движения мыши
    delwin(buf);
    endwin();
    
    return 0;
}

void draw_screen_borders(WINDOW* w, int cx, int cy){
    int x=0, y=0;
    for(int x=0; x<SCREEN_WIDTH; x++){
        mvwprintw(w, y, x, "-");
    }
    y=SCREEN_HEIGHT;
    for(int x=0; x<SCREEN_WIDTH; x++){
        if(x==SCREEN_WIDTH/2){
            mvwprintw(w,y,x,"%d", SCREEN_WIDTH);
            x++;
        }else if(x==(SCREEN_WIDTH/3)*2){
            mvwprintw(w,y,x,"x:%02d", cx);
            x+=3;
        }
        else if(x==(SCREEN_WIDTH/5)*4){
            mvwprintw(w,y,x,"y:%02d", cy);
            x+=3;
        }
        else{
            mvwprintw(w,y,x,"-");
        }
    }
    x=0;
    for(int y=0; y<SCREEN_HEIGHT; y++){
        mvwprintw(w,y,x,"|");
    }
    x=SCREEN_WIDTH;
    for(int y=0; y<SCREEN_HEIGHT; y++){
        if(y==SCREEN_HEIGHT/2){
            mvwprintw(w,y,x,"%02d", SCREEN_HEIGHT);
        }
        else{
            mvwprintw(w,y,x,"|");
        }
    }
    mvwprintw(w,0,0,"+");
    mvwprintw(w,SCREEN_HEIGHT,0,"+");
    mvwprintw(w,0,SCREEN_WIDTH,"+");
    mvwprintw(w,SCREEN_HEIGHT,SCREEN_WIDTH,"+");
}

void init_ncurses(){
    initscr();
    cbreak();
    noecho();
    keypad(stdscr, TRUE); // включает специальные клавиши (стрелки и т.д)
    nodelay(stdscr, TRUE); // чтобы getch был неблокирующим
    mousemask(ALL_MOUSE_EVENTS | REPORT_MOUSE_POSITION, NULL); // включение маски мыши
    printf("\033[?1003h\n"); // включает режим движения мыши
    //ESCDELAY = 1;
}