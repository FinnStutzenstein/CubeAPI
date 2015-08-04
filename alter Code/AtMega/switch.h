#define SWITCH_MAX		0xFFFFFFFF

unsigned char _switch_ref;
unsigned long _switch_counter;

void init_switch(void);
void update_taster(void);
unsigned char taster_pressed(void);
unsigned char taster_released(void);