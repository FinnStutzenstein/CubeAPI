#include "Cube.h"

volatile unsigned char ISR_Layer = 0;

int a = 0;
 
int main(void)
{   
   
    init();
	
	while(1)
	{
	
		//sonstiges --> NIX :D
		
		//abfrage
		connection_scheduling();
		
		if(switch_pressed()){
			if(a){
				a = 0;
				WholeCube(CLEAR);
			}else{
				a=1;
				WholeCube(SET);
			}
		}
		
    }
 
  return 0;
}

ISR (TIMER1_COMPA_vect){
	ISR_Layer++;
	if(ISR_Layer == 10){
		ISR_Layer = 0;
	}
	
	clear_Layer();
	
	for(char i=0; i<13; i++){
		PORT_DATA = CUBE[ISR_Layer][i];
		save_latch(i);
	}
	
	select_Layer(ISR_Layer);
	
	switch_update();
}

void select_Layer(unsigned char layer){ //**PORT B!!!**
	for(signed char i=15; i>=0; i--){
		if (i == layer)
		{
			PORT_REGA |= (1<<REG_Daten);	//1,aus
		}else{
			PORT_REGA &= ~(1<<REG_Daten);	//0,an
		}
		
		PORT_REGA &= ~(1<<REG_Schieben); //Schieben
		PORT_REGA |= (1<<REG_Schieben);
		}
	PORT_REGA &= ~(1<<REG_Speicher); //Speichern;Aktualisieren
	PORT_REGA |= (1<<REG_Speicher);
}

void clear_Layer(void){
	PORT_REGA &= ~(1<<REG_Daten);	//0,an
	for(unsigned char i=0; i<16; i++){
		PORT_REGA &= ~(1<<REG_Schieben); //Schieben, 16 mal eine 0 (an)
		PORT_REGA |= (1<<REG_Schieben);
	}
	PORT_REGA &= ~(1<<REG_Speicher); //Speichern;Aktualisieren
	PORT_REGA |= (1<<REG_Speicher);
}

void save_latch(unsigned char latch){ //**PORT C!!!**
	for(signed char i=15; i>=0; i--){   //## Latch aktivieren
		if (i == latch)
		{
			//PORT_REGB &= ~(1<<REG_Daten);	//0,an
			PORT_REGB |= (1<<REG_Daten);	//1,aus
		}else{
			//PORT_REGB |= (1<<REG_Daten);	//1,aus
			PORT_REGB &= ~(1<<REG_Daten);	//0,an
		}
		
		PORT_REGB &= ~(1<<REG_Schieben); //Schieben
		PORT_REGB |= (1<<REG_Schieben);
	}
	PORT_REGB &= ~(1<<REG_Speicher); //Speichern;Aktualisieren
	PORT_REGB |= (1<<REG_Speicher);
	
	//_delay_ms(3000);
	
		//## Schieberegister leeren, kein Latch aktiviert
	
	//PORT_REGB |= (1<<REG_Daten); //1,aus
	PORT_REGB &= ~(1<<REG_Daten);	//0,an
	for(unsigned char i=0; i<16; i++){
		PORT_REGB &= ~(1<<REG_Schieben); //Schieben
		PORT_REGB |= (1<<REG_Schieben);
	}
	PORT_REGB &= ~(1<<REG_Speicher); //Speichern;Aktualisieren
	PORT_REGB |= (1<<REG_Speicher);
}