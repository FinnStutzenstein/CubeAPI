#include "Cube.h"

void init_buffer(void){
	Xmax = 14;
	Xbuf[14]; //Laenge: Xmax
	Xstart = 0;
	Xende = 0;
}

unsigned char Con_nextParam(void){
    unsigned char data;
	
    if (Xstart<Xende) {
        data = Xbuf[Xstart];
		Xstart++;
    }else{
		//Error
		data = 0x00;
    }
	return data;
}

void Con_delBuf(void){
	Xstart = 0;
	Xende = 0;
}

void Con_saveData(unsigned char Con_in){
	if (Xende<(Xmax-1)){//es ist platz da
		Xbuf[Xende]=Con_in;
		Xende++;
	}
}