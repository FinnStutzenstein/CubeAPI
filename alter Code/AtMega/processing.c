#include "Cube.h"

/**                                     a b c  d e f  g h i  j k l  m n o  p q r  s t u  v   w x y z **/
unsigned char Con_arrayParameter[26] = {0,0,0, 1,1,1, 3,3,3, 6,6,6, 6,6,6, 6,6,6, 6,6,6, 13, 0,0,0,0}; //hier fuer jeden Befehl die Anzahl der Parameter

void init_processing(void){
	verbleibendeParameter = 0;
	aktuellerBefehl = 0x00;
}

void process(unsigned char Con_in){

	if(aktuellerBefehl == 0x00){ //neuen Befehl annehmen
		char nr = Con_in - 'a';

		if(nr == 0){ //sofort ausfuehren
			WholeCube(SET);
			aktuellerBefehl = 0x00;
		}else if( nr == 1){ //b
			WholeCube(CLEAR);
			aktuellerBefehl = 0x00;
		}else if(nr == 2){ //c
			WholeCube(INVERT);
			aktuellerBefehl = 0x00;
		}else if(nr == 22){ //w      -->senden, ob Cube bereit ist, 2 layer anzunehmen
			//nix
			aktuellerBefehl = Con_in;
		}else if(nr == 23){ //x
			//Handshake --> einfaches return
			aktuellerBefehl = 0x00;
		}else if(nr == 24){ //y
			//Reset (TODO)
			aktuellerBefehl = 0x00;
		}else if(nr == 25){ //z
			//Version
			aktuellerBefehl = 0x00;
			//uart_puts_P("LC_FS_V_1.3");
			
		}else if(nr >= 3 && nr <= 21){ //a-u --> Befehl annehmen	
			Con_delBuf();
			
			verbleibendeParameter = Con_arrayParameter[nr];
			aktuellerBefehl = Con_in;								
		}else{
		
			/*char buffer[7];
			uart_puts("falsche Nr.: ");
			itoa( Con_in, buffer, 10);
			uart_puts(buffer);
			uart_puts("\n");*
			
			uart_puts_P("FalBef");*/
		}
	}else{
		if(verbleibendeParameter>0){  //vP = 0 --> es gibt keine Parameter, nicht speichern
			verbleibendeParameter--;
		
			Con_saveData(Con_in);
			
			
		}

		if (verbleibendeParameter == 0){
			unsigned char param[14];
			unsigned char nrBef = aktuellerBefehl - 'a';
			unsigned char a = nrBef%3;
			switch(a){
				case 0: a = SET; break;
				case 1: a = CLEAR; break;
				case 2: a = INVERT; break;
			}
		
			switch(nrBef){
				case 3: case 4: case 5: //d,e,f --> Layer
					param[0] = Con_nextParam() - '0';
					//if(Layer(param[0],a) == ERROR) uart_puts_P("Fehler");
					Layer(param[0],a);
					break;
				case 6: case 7: case 8: //g,h,i --> Pixel
					for(char i = 0; i<3; i++){
						param[i] = Con_nextParam() - '0';
					}
					//if(Pixel(param[0],param[1],param[2],a) == ERROR) uart_puts_P("Fehler");
					Pixel(param[0],param[1],param[2],a);
					break;
				case 9: case 10: case 11: //j,k,l --> Cube
					for(char i = 0; i<6; i++){
						param[i] = Con_nextParam() - '0';
					}
					//if(Cube(param[0],param[1],param[2],param[3],param[4],param[5],a) == ERROR) uart_puts_P("Fehler");
					Cube(param[0],param[1],param[2],param[3],param[4],param[5],a);
					break;
				case 12: case 13: case 14: //m,n,o --> FilledCube
					for(char i = 0; i<6; i++){
						param[i] = Con_nextParam() - '0';
					}
					//if(FilledCube(param[0],param[1],param[2],param[3],param[4],param[5],a) == ERROR) uart_puts_P("Fehler");
					FilledCube(param[0],param[1],param[2],param[3],param[4],param[5],a);
					break;
				case 15: case 16: case 17: //p,q,r --> EdgeCube
					for(char i = 0; i<6; i++){
						param[i] = Con_nextParam() - '0';
					}
					//if(EdgeCube(param[0],param[1],param[2],param[3],param[4],param[5],a) == ERROR) uart_puts_P("Fehler");
					EdgeCube(param[0],param[1],param[2],param[3],param[4],param[5],a);
					break;
				case 18: case 19: case 20: //s,t,u --> Line
					for(char i = 0; i<6; i++){
						param[i] = Con_nextParam() - '0';
					}
					//if(Line(param[0],param[1],param[2],param[3],param[4],param[5],a) == ERROR) uart_puts_P("Fehler");
					Line(param[0],param[1],param[2],param[3],param[4],param[5],a);
					break;
				case 21: //kompletter Wuerfel
					param[0] = Con_nextParam(); 
					char layer_ = param[0]>>4; // 0xF0 --> 0xF sind hier die Daten
					CUBE[layer_][12] = (param[0] & 0x0F); //letzte Bytes setzen (0x0F --> 0xF sind die Daten)
				
					for(char i = 1; i<13; i++){ //ein versetzt
						param[i] = Con_nextParam(); //Hier wirklich daten, keine ascii-codes
						
						CUBE[layer_][i-1] = param[i]; // on beginn an auffuellen (param[0] ueberspringen, da dort layer und letzte bytes
					}

			}
			aktuellerBefehl = 0x00; //nach abschluss befehl auf NULL setzen
			//uart_puts_P("\n");
		}
	}//end if: aktueller befehl
}