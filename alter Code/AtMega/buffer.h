volatile unsigned char Xmax;
volatile unsigned char Xbuf[14]; //Laenge: Xmax
volatile unsigned char Xstart;
volatile unsigned char Xende;

unsigned char Con_nextParam(void);
void Con_delBuf(void);
void Con_saveData(unsigned char Con_in);