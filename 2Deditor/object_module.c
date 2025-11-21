#include <stdlib.h>
#include <ncurses.h>
#include "object_module.h"
#include "main_module.h"


void draw_object(WINDOW* w, struct box object){
    int x=object.x;
    int y=object.y;
    int width=object.width;
    int height=object.height;
    int sym='.';
    if(object.selected){
        sym='*';
    }
    for(int x_=x; x_<x+width; x_++){
        mvwprintw(w,y,x_,"%c",sym);
    }
    for(int x_=x; x_<x+width; x_++){
        mvwprintw(w,y+height-1,x_,"%c",sym);
    }
    for(int y_=y; y_<y+height; y_++){
        mvwprintw(w,y_,x,"%c",sym);
    }
    for(int y_=y; y_<y+height; y_++){
        mvwprintw(w,y_,x+width-1,"%c",sym);
    }
}

void move_object(struct box* obj, int x, int y){
    if(!object_is_out_of_screen(x,y, obj->width, obj->height)){
        obj->x=x;
        obj->y=y;
    }
}

void resize_object(struct box* obj, int width, int height){
    if(!object_is_out_of_screen(obj->x, obj->y, width, height)){
        obj->width=width;
        obj->height=height;
    }
}

int object_is_out_of_screen(int x, int y, int width, int height){
    return (x<1 || y<1 ||
         x+width>SCREEN_WIDTH || y+height>SCREEN_HEIGHT);
}

struct box object_init(){
    struct box obj;
    obj.x=SCREEN_WIDTH/2;
    obj.y=SCREEN_HEIGHT/2;
    obj.width=BOX_WIDTH;
    obj.height=BOX_HEIGHT;
    obj.selected=0;
    return obj;
}

int cursor_inside_object(struct box obj, int cx, int cy){
    return (cx>=obj.x && cy>=obj.y &&
         cx<=obj.x+obj.width && cy<=obj.y+obj.height);
}