#ifndef OBJECT_MODULE_H
#define OBJECT_MODULE_H

#define BOX_WIDTH 16
#define BOX_HEIGHT 8

struct box{
    int width, height;
    int x, y;
    int selected;
};

void draw_object(WINDOW* w, struct box object);
void move_object(struct box* obj, int x, int y);
void resize_object(struct box* obj, int width, int height);
int object_is_out_of_screen(int x, int y, int width, int height);
struct box object_init();
int cursor_inside_object(struct box obj, int cx, int cy);
#endif