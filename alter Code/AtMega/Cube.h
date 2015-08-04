/*DDRx****PORTx******** Funktion *******
*   0   *   0   * EINGANG ohne Pull-Up *
****************************************
*   0   *   1   * EINGANG mit Pull-Up  *
****************************************
*   1   *   0   * AUSGANG LOW  (an)    *
****************************************
*   1   *   1   * AUSGANG HIGH (aus)   *
***************************************/

#include "main.h"
#include "functions.h"
#include "init.h"
#include "buffer.h"
#include "switch.h"
#include "processing.h"
#include "connection.h"

#define TM1ZAEHLER			21000

#define DIR_DATA			DDRA
#define PORT_DATA			PORTA
#define STD_DIR_DATA		0xFF
#define STD_PORT_DATA		0xFF
#define PIN_DATA			PINA

#define DIR_REGA			DDRB
#define PORT_REGA			PORTB
#define STD_DIR_REGA		0b11110111
#define STD_PORT_REGA		0b11110111
#define PIN_REGA			PINB

#define DIR_REGB			DDRC
#define PORT_REGB			PORTC
#define STD_DIR_REGB		0b11101111
#define STD_PORT_REGB		0b11000111
#define PIN_REGB			PINC

#define PIN_ENABLE_CUBE		3
#define PIN_Z				(1<<4)
#define PIN_E				(1<<5)

#define DIR_MC2				DDRD
#define PORT_MC2			PORTD
#define STD_DIR_MC2			0x00
#define STD_PORT_MC2		0x00
#define PIN_MC2				PIND

#define REG_Daten			0
#define REG_Speicher		2
#define REG_Schieben		1

#define SET					0x10
#define CLEAR				0x20
#define INVERT				0x40

#define OK					0x00
#define ERROR				0x01

volatile unsigned char CUBE[10][13];