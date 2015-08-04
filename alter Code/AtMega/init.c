#include "Cube.h"

void init(void){

	DIR_DATA = STD_DIR_DATA;
	DIR_REGA = STD_DIR_REGA;
	DIR_REGB = STD_DIR_REGB;
	DIR_MC2  = STD_DIR_MC2;
	
	PORT_DATA = STD_PORT_DATA;
	PORT_REGA = STD_PORT_REGA;
	PORT_REGB = STD_PORT_REGB;
	PORT_MC2  = STD_PORT_MC2;
	
	init_mux();
	init_switch();
	init_buffer();
	init_processing();
	
	for(char i = 0; i<10; i++){
		for(char j = 0; j<13; j++){
			CUBE[i][j]=0x01;
		}
	}
   
    //Timer A:
    TCCR1A = 0x00;
    TCCR1B = (1<<WGM12)|(1<CS12);
	
    OCR1AH = TM1ZAEHLER>>8;
    OCR1AL = TM1ZAEHLER & 0x00FF;
	
	TIMSK = (1<<OCIE1A);
   
    sei();

	PORT_REGB |= (1<<PIN_ENABLE_CUBE); // CUBE EINSCHALTEN
	
}

void init_mux(void){
	clear_Layer();
	
	//Einmal die Schieberegister leeren (Latches)
	PORT_REGB &= ~(1<<REG_Daten);	//0,an
	for(unsigned char i=0; i<16; i++){
		PORT_REGB &= ~(1<<REG_Schieben); //Schieben
		PORT_REGB |= (1<<REG_Schieben);
	}
	PORT_REGB &= ~(1<<REG_Speicher); //Speichern;Aktualisieren
	PORT_REGB |= (1<<REG_Speicher);
	
	//alles aus
	PORT_DATA = 0x00;
	for(unsigned char i=0; i<16; i++){
		save_latch(i);
	}
}