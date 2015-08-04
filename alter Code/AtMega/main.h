#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>

#define ATmega16
#define F_CPU 16000000UL //16MHz

void select_Layer(unsigned char layer);
void clear_Layer(void);
void save_latch(unsigned char latch);