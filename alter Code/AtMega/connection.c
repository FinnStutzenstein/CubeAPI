#include "Cube.h"

void connection_scheduling (void){
	if(PORT_REGB & PIN_E){ //E==1?
		if((PIN_MC2 == 0x00) && !(PIN_REGB & PIN_Z)){ //d==0x00 und z==0
			PORT_REGB &= ~PIN_E; //E=0
			
		} //sonst nix
		
	}else if(PIN_MC2 != 0x00){ //d!=0x00
		unsigned char F = PIN_MC2;
		unsigned char F_, i, ok = 0xFF;
		
		for(i = 0; i<3; i++){
			F_ = PIN_MC2;
			if(F_ != F){
				ok = 0x00;
			}
		}
		
		if(ok){
			//auswertung von F
			process(F);
			
			//CUBE[0][0] = F;
			
			PORT_REGB |= PIN_E;
		}
		
	}else if(PIN_REGB & PIN_Z){ //z==1
		//auswertung von 0x00
		process(0x00);
		
		//CUBE[0][0] = 0x00;
		
		PORT_REGB |= PIN_E;
	}
}