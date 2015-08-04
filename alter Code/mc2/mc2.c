/*DDRx****PORTx******** Funktion *******
*   0   *   0   * EINGANG ohne Pull-Up *
****************************************
*   0   *   1   * EINGANG mit Pull-Up  *
****************************************
*   1   *   0   * AUSGANG LOW  (an)    *
****************************************
*   1   *   1   * AUSGANG HIGH (aus)   *
***************************************/
//
#define F_CPU 16000000UL //16MHz
//#define UART_BAUD_RATE      115200
#define UART_BAUD_RATE		250000
//#define UART_BAUD_RATE      38400 
//#define UART_BAUD_RATE      9600  

#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>

//#include "uart.c"
//#include "uart.h"

// Size of the circular receive buffer, must be power of 2
#define UART_RX_BUFFER_SIZE 64
// Size of the circular transmit buffer, must be power of 2
#define UART_TX_BUFFER_SIZE 64

// size of RX/TX buffers
#define UART_RX_BUFFER_MASK ( UART_RX_BUFFER_SIZE - 1)
#define UART_TX_BUFFER_MASK ( UART_TX_BUFFER_SIZE - 1)

#define UART_FRAME_ERROR      0x1000              // Framing Error by UART       
#define UART_OVERRUN_ERROR    0x0800              // Overrun condition by UART   
#define UART_PARITY_ERROR     0x0400              // Parity Error by UART        
#define UART_BUFFER_OVERFLOW  0x0200              // receive ringbuffer overflow 
#define UART_NO_DATA          0x0100              // no receive data available   

#define UART0_RECEIVE_INTERRUPT   USART_RXC_vect
#define UART0_TRANSMIT_INTERRUPT  USART_UDRE_vect
#define UART0_STATUS   UCSRA
#define UART0_CONTROL  UCSRB
#define UART0_DATA     UDR
#define UART0_UDRIE    UDRIE
 
#define DIR_DATA			DDRA
#define PORT_DATA			PORTA
#define STD_DIR_DATA		0xFF
#define STD_PORT_DATA		0x00
#define PIN_DATA			PINA

#define DIR_CTRL			DDRC
#define PORT_CTRL			PORTC
#define STD_DIR_CTRL		0b11111101
#define STD_PORT_CTRL		0b00000000
#define PIN_CTRL			PINC

#define PIN_Z				(1<<0)
#define PIN_E				(1<<1)
 
static volatile unsigned char UART_TxBuf[UART_TX_BUFFER_SIZE];
static volatile unsigned char UART_RxBuf[UART_RX_BUFFER_SIZE];
static volatile unsigned char UART_TxHead;
static volatile unsigned char UART_TxTail;
static volatile unsigned char UART_RxHead;
static volatile unsigned char UART_RxTail;
static volatile unsigned char UART_LastRxError;

ISR (UART0_RECEIVE_INTERRUPT)	
// *************************************************************************
// Function: UART Receive Complete interrupt
// Purpose:  called when the UART has received a character
// **************************************************************************
{
    unsigned char tmphead;
    unsigned char data;
    unsigned char usr;
    unsigned char lastRxError;
 
 
    // read UART status register and UART data register
    usr  = UART0_STATUS;
    data = UART0_DATA;
    
   
#if defined( AT90_UART )
    lastRxError = (usr & (_BV(FE)|_BV(DOR)) );
#elif defined( ATMEGA_USART )
    lastRxError = (usr & (_BV(FE)|_BV(DOR)) );
#elif defined( ATMEGA_USART0 )
    lastRxError = (usr & (_BV(FE0)|_BV(DOR0)) );
#elif defined ( ATMEGA_UART )
    lastRxError = (usr & (_BV(FE)|_BV(DOR)) );
#endif
        
    // calculate buffer index 
    tmphead = ( UART_RxHead + 1) & UART_RX_BUFFER_MASK;
    
    if ( tmphead == UART_RxTail ) {
        // error: receive buffer overflow
        lastRxError = UART_BUFFER_OVERFLOW >> 8;
    }else{
        // store new index 
        UART_RxHead = tmphead;
        // store received data in buffer 
        UART_RxBuf[tmphead] = data;
    }
    UART_LastRxError |= lastRxError;   
}


ISR (UART0_TRANSMIT_INTERRUPT)
// *************************************************************************
// Function: UART Data Register Empty interrupt
// Purpose:  called when the UART is ready to transmit the next byte
// **************************************************************************
{
    unsigned char tmptail;

    
    if ( UART_TxHead != UART_TxTail) {
        // calculate and store new buffer index
        tmptail = (UART_TxTail + 1) & UART_TX_BUFFER_MASK;
        UART_TxTail = tmptail;
        // get one byte from buffer and write it to UART
        UART0_DATA = UART_TxBuf[tmptail];  // start transmission
    }else{
        // tx buffer empty, disable UDRE interrupt
        UART0_CONTROL &= ~_BV(UART0_UDRIE);
    }
}

// *************************************************************************
// Function: uart_getc()
// Purpose:  return byte from ringbuffer  
// Returns:  lower byte:  received byte from ringbuffer
//           higher byte: last receive error
// **************************************************************************
unsigned int uart_getc(void)
{    
    unsigned char tmptail;
    unsigned char data;


    if ( UART_RxHead == UART_RxTail ) {
        return UART_NO_DATA;   // no data available
    }
    
    // calculate /store buffer index
    tmptail = (UART_RxTail + 1) & UART_RX_BUFFER_MASK;
    UART_RxTail = tmptail; 
    
    // get data from receive buffer
    data = UART_RxBuf[tmptail];
    
    data = (UART_LastRxError << 8) + data;
    UART_LastRxError = 0;
    return data;

}// uart_getc


// *************************************************************************
// Function: uart_putc()
// Purpose:  write byte to ringbuffer for transmitting via UART
// Input:    byte to be transmitted
// Returns:  none          
// **************************************************************************
void uart_putc(unsigned char data)
{
    unsigned char tmphead;

    
    tmphead  = (UART_TxHead + 1) & UART_TX_BUFFER_MASK;
    
    while ( tmphead == UART_TxTail ){
        ;// wait for free space in buffer
    }
    
    UART_TxBuf[tmphead] = data;
    UART_TxHead = tmphead;

    // enable UDRE interrupt
    UART0_CONTROL    |= _BV(UART0_UDRIE);

}// uart_putc

int main(void)
{   
    DIR_DATA = STD_DIR_DATA;
	PORT_DATA = STD_PORT_DATA;
	DIR_CTRL = STD_DIR_CTRL;
	PORT_CTRL = STD_PORT_CTRL;
	
	//uart_init( UART_BAUD_SELECT_DOUBLE_SPEED(UART_BAUD_RATE,F_CPU) );
	
	
	int baudrate = ( ((((F_CPU) + 4UL * (UART_BAUD_RATE)) / (8UL * (UART_BAUD_RATE)) -1UL)) | 0x8000); //double speed
	//int baudrate = (((F_CPU) + 8UL * (UART_BAUD_RATE)) / (16UL * (UART_BAUD_RATE)) -1UL); //single speed
	
    if ( baudrate & 0x8000 )
    {
    	 UART0_STATUS = (1<<U2X);  //Enable 2x speed 
    	 baudrate &= ~0x8000;
    }
    UBRRH = (unsigned char)(baudrate>>8);
    UBRRL = (unsigned char) baudrate;
   
    // Enable USART receiver and transmitter and receive complete interrupt
    UART0_CONTROL = _BV(RXCIE)|(1<<RXEN)|(1<<TXEN);
    
    // Set frame format: asynchronous, 8data, no parity, 1stop bit
    #ifdef URSEL
    UCSRC = (1<<URSEL)|(3<<UCSZ0);
    #else
    UCSRC = (3<<UCSZ0);
    #endif 
	
	sei();
	
	unsigned char tmptail;
	unsigned char data;
		
	data = 0x00;
	
	PORT_CTRL &= ~PIN_Z;
	
	//unsigned int c;
	
	while(1){	


		/*c = uart_getc();
        if ( c & UART_NO_DATA )
        {
           
        }
        else
        {
           
            /*if ( c & UART_FRAME_ERROR )
            {
                uart_puts_P("UART Frame Error: ");
            }
            if ( c & UART_OVERRUN_ERROR )
            {
                uart_puts_P("UART Overrun Error: ");
            }
            if ( c & UART_BUFFER_OVERFLOW )
            {
                uart_puts_P("Buffer overflow error: ");
            }*
            uart_putc( (unsigned char)c );
			
			PORT_CTRL |= PIN_Z;
        }*/

	
		
		//aus getc:
		if(UART_RxHead != UART_RxTail){ //Daten vorhanden
			// calculate /store buffer index
			tmptail = (UART_RxTail + 1) & UART_RX_BUFFER_MASK;
			UART_RxTail = tmptail; 
			
			// get data from receive buffer
			data = UART_RxBuf[tmptail];
			
			if ( UART_LastRxError & UART_FRAME_ERROR ){
				//uart_puts_P("UART Frame Error: ");
			}else if ( UART_LastRxError & UART_OVERRUN_ERROR ){
				//uart_puts_P("UART Overrun Error: ");
			}else if ( UART_LastRxError & UART_BUFFER_OVERFLOW ){
				//uart_puts_P("Buffer overflow error: ");
			}
			
			if(data == 0x00){
				PORT_CTRL |= PIN_Z;
			}else{
				PORT_DATA = data;
			}
			
			while((PIN_CTRL & PIN_E) == 0x00){ 
				//Warten bis E=1
			}
			
			PORT_DATA = 0x00;
			PORT_CTRL &= ~PIN_Z;
			
			while((PIN_CTRL & PIN_E) != 0x00){
				//Warten bis E=0
			}
				
			//Zurueckschreiben;
			uart_putc(data);
		}
		
		
		
		/*PORT_CTRL |= PIN_Z;
		_delay_ms(200);
		PORT_CTRL &= ~PIN_Z;
		_delay_ms(200);*/
	}
	
	return 0;
}





