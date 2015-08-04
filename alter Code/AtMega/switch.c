#include "Cube.h"

void init_switch(void){
	_switch_ref = PIN_REGA; //kein Tastendruck am anfang erkennen!
	_switch_counter = 0;
}

void switch_update(void){
	if(_switch_counter != SWITCH_MAX){
		_switch_counter++;
	}
}

unsigned char switch_pressed(void){
	if((PIN_REGA & (1<<4)) != (_switch_ref & (1<<4)) && _switch_counter == SWITCH_MAX ){
		_switch_ref = PIN_REGA;
		_switch_counter = 0;
		
		if((PIN_REGA & (1<<4)) == 0x00){ //Press
			return 1;
		}
	}
	
	return 0;
}

unsigned char switch_released(void){
	if((PIN_REGA & (1<<4)) != (_switch_ref & (1<<4)) && _switch_counter == SWITCH_MAX ){
		_switch_ref = PIN_REGA;
		_switch_counter = 0;
		
		if((PIN_REGA & (1<<4)) != 0x00){ //release
			return 1;
		}

	}
	
	return 0;
}