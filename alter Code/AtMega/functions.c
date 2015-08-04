#include "Cube.h"

unsigned char WholeCube(unsigned char a){
	switch(a){
		case SET: _setWholeCube(); break;
		case CLEAR: _clearWholeCube(); break;
		case INVERT: _invertWholeCube(); break;
		default: return ERROR;
	}
	return OK;
}

void _setWholeCube(void){
	for (unsigned char layer = 0; layer<10; layer++){
		for (unsigned char latch = 0; latch<13; latch++){
			CUBE[layer][latch] = 0xFF;
		}
	}
}

void _clearWholeCube(void){
	for (unsigned char layer = 0; layer<10; layer++){
		for (unsigned char latch = 0; latch<13; latch++){
			CUBE[layer][latch] = 0x00;
		}
	}
}

void _invertWholeCube(void){
	for (unsigned char layer = 0; layer<10; layer++){
		for (unsigned char latch = 0; latch<13; latch++){
			CUBE[layer][latch] = ~(CUBE[layer][latch]);
		}
	}
}

unsigned char Layer(unsigned char layer, unsigned char a){
	if(layer <0 || layer > 9) return ERROR;
	switch(a){
		case SET: _setLayer(layer); break;
		case CLEAR: _clearLayer(layer); break;
		case INVERT: _invertLayer(layer); break;
		default: return ERROR;
	}
	return OK;
}

void _setLayer(unsigned char layer){
	for (unsigned char latch = 0; latch<13; latch++){
			CUBE[layer][latch] = 0xFF;
	}
}

void _clearLayer(unsigned char layer){
	for (unsigned char latch = 0; latch<13; latch++){
			CUBE[layer][latch] = 0x00;
	}
}

void _invertLayer(unsigned char layer){
	for (unsigned char latch = 0; latch<13; latch++){
		CUBE[layer][latch] = ~(CUBE[layer][latch]);
	}
}

unsigned char Pixel(unsigned char x, unsigned char y, unsigned char z, unsigned char a){
	if(x <0 || x > 9 || y <0 || y > 9 || z <0 || z > 9) return ERROR;
	switch(a){
		case SET: _setPixel(x, y, z); break;
		case CLEAR: _clearPixel(x, y, z); break;
		case INVERT: _invertPixel(x, y, z); break;
		default: return ERROR;
	}
	return OK;
}

void _setPixel(unsigned char x, unsigned char y, unsigned char z){
	unsigned char latch_;
	unsigned char pos_;
	latch_ = (unsigned char) (10*z + x) / 8;
	pos_ = (unsigned char) (10*z + x) % 8;
	CUBE[y][latch_] |= (1<<pos_);
}

void _clearPixel(unsigned char x, unsigned char y, unsigned char z){
	unsigned char latch_;
	unsigned char pos_;
	latch_ = (unsigned char) (10*z + x) / 8;
	pos_ = (unsigned char) (10*z + x) % 8;
	CUBE[y][latch_] &= ~(1<<pos_);
}

void _invertPixel(unsigned char x, unsigned char y, unsigned char z){
	unsigned char latch_;
	unsigned char pos_;
	latch_ = (unsigned char) (10*z + x) / 8;
	pos_ = (unsigned char) (10*z + x) % 8;
	CUBE[y][latch_] ^= (1<<pos_);
}

unsigned char Cube(unsigned char x1, unsigned char y1, unsigned char z1, unsigned char x2, unsigned char y2, unsigned char z2, unsigned char a){
	if(x1 <0 || x1 > 9 || y1 < 0 || y1 > 9 || z1 <0 || z1 > 9) return ERROR;
	if(x2 <0 || x2 > 9 || y2 < 0 || y2 > 9 || z2 <0 || z2 > 9) return ERROR;
	if(x1 > x2 || y1 > y2 || x1 > x2) return ERROR;
	
	if(FilledCube(x1, y1, z1, x2, y1, z2, a) == ERROR) return ERROR; //Oben
	if(FilledCube(x1, y2, z1, x2, y2, z2, a) == ERROR) return ERROR; //Unten
	FilledCube(x1, y1+1, z1, x2, y2-1, z1, a); //Seite vorne  ###Error muss fuer kleine Wuerfel toleriert werden
	FilledCube(x1, y1+1, z2, x2, y2-1, z2, a); //Seite hinten
	FilledCube(x2, y1+1, z1+1, x2, y2-1, z2-1, a); //Seite rechts
	FilledCube(x1, y1+1, z1+1, x1, y2-1, z2-1, a); //Seite links
	
	return OK;
}

unsigned char FilledCube(unsigned char x1, unsigned char y1, unsigned char z1, unsigned char x2, unsigned char y2, unsigned char z2, unsigned char a){
	if(x1 <0 || x1 > 9 || y1 < 0 || y1 > 9 || z1 <0 || z1 > 9) return ERROR;
	if(x2 <0 || x2 > 9 || y2 < 0 || y2 > 9 || z2 <0 || z2 > 9) return ERROR;
	if(x1 > x2 || y1 > y2 || x1 > x2) return ERROR;
	switch(a){
		case SET: _setFilledCube(x1, y1, z1, x2, y2, z2); break;
		case CLEAR: _clearFilledCube(x1, y1, z1, x2, y2, z2); break;
		case INVERT: _invertFilledCube(x1, y1, z1, x2, y2, z2); break;
		default: return ERROR;
	}
	return OK;
}

void _setFilledCube(unsigned char x1, unsigned char y1, unsigned char z1, unsigned char x2, unsigned char y2, unsigned char z2){
	unsigned char latch_;
	unsigned char pos_;
			
	for (unsigned char x = x1; x<=x2; x++){
		for (unsigned char z = z1; z<=z2; z++){
			latch_ = (unsigned char) (10*z + x) / 8;
			pos_ = (unsigned char) (10*z + x) % 8;
			for (unsigned char y = y1; y<=y2; y++){
				CUBE[y][latch_] |= (1<<pos_);
			}
		}
	}
}

void _clearFilledCube(unsigned char x1, unsigned char y1, unsigned char z1, unsigned char x2, unsigned char y2, unsigned char z2){
	unsigned char latch_;
	unsigned char pos_;
			
	for (unsigned char x = x1; x<=x2; x++){
		for (unsigned char z = z1; z<=z2; z++){
			latch_ = (unsigned char) (10*z + x) / 8;
			pos_ = (unsigned char) (10*z + x) % 8;
			for (unsigned char y = y1; y<=y2; y++){
				CUBE[y][latch_] &= ~(1<<pos_);
			}
		}
	}
}

void _invertFilledCube(unsigned char x1, unsigned char y1, unsigned char z1, unsigned char x2, unsigned char y2, unsigned char z2){
	unsigned char latch_;
	unsigned char pos_;
			
	for (unsigned char x = x1; x<=x2; x++){
		for (unsigned char z = z1; z<=z2; z++){
			latch_ = (char) (10*z + x) / 8;
			pos_ = (char) (10*z + x) % 8;
			for (unsigned char y = y1; y<=y2; y++){
				CUBE[y][latch_] ^= (1<<pos_);
			}
		}
	}
}

unsigned char EdgeCube(unsigned char x1, unsigned char y1, unsigned char z1, unsigned char x2, unsigned char y2, unsigned char z2, unsigned char a){
	if(x1 <0 || x1 > 9 || y1 < 0 || y1 > 9 || z1 <0 || z1 > 9) return ERROR;
	if(x2 <0 || x2 > 9 || y2 < 0 || y2 > 9 || z2 <0 || z2 > 9) return ERROR;
	if(x1 > x2 || y1 > y2 || x1 > x2) return ERROR;
	
	if(Line(x1,y1,z1, x1,y1,z2, a) == ERROR) return ERROR; //Unten
	if(Line(x1,y1,z1, x2,y1,z1, a) == ERROR) return ERROR;
	if(Line(x1,y1,z2, x2,y1,z2, a) == ERROR) return ERROR;
	if(Line(x2,y1,z1, x2,y1,z2, a) == ERROR) return ERROR;
	
	if(Line(x1,y2,z1, x1,y2,z2, a) == ERROR) return ERROR; //Oben
	if(Line(x1,y2,z1, x2,y2,z1, a) == ERROR) return ERROR;
	if(Line(x1,y2,z2, x2,y2,z2, a) == ERROR) return ERROR;
	if(Line(x2,y2,z1, x2,y2,z2, a) == ERROR) return ERROR;
	
	if(Line(x1,y1,z1, x1,y2,z1, a) == ERROR) return ERROR; //Seiten
	if(Line(x2,y1,z1, x2,y2,z1, a) == ERROR) return ERROR;
	if(Line(x1,y1,z2, x1,y2,z2, a) == ERROR) return ERROR;
	if(Line(x2,y1,z2, x2,y2,z2, a) == ERROR) return ERROR;
	
	return OK;
}

unsigned char Line(unsigned char x1, unsigned char y1, unsigned char z1, unsigned char x2, unsigned char y2, unsigned char z2, unsigned char a){ //Bresenham3D (4mal setzen)
    
	/*Pixel(0,x1,0,SET);
	Pixel(1,y1,0,SET);
	Pixel(2,z1,0,SET);
	
	Pixel(3,x2,0,SET);
	Pixel(4,y2,0,SET);
	Pixel(5,z2,0,SET);*/
	
	int i, dx, dy, dz, l, m, n, x_inc, y_inc, z_inc, err_1, err_2, dx2, dy2, dz2;
    int point[3];
    
    point[0] = x1;
    point[1] = y1;
    point[2] = z1;
    dx = x2 - x1;
    dy = y2 - y1;
    dz = z2 - z1;
    x_inc = (dx < 0) ? -1 : 1;
    l = abs(dx);
    y_inc = (dy < 0) ? -1 : 1;
    m = abs(dy);
    z_inc = (dz < 0) ? -1 : 1;
    n = abs(dz);
    dx2 = l << 1;
    dy2 = m << 1;
    dz2 = n << 1;
    
    if ((l >= m) && (l >= n)) {
        err_1 = dy2 - l;
        err_2 = dz2 - l;
        for (i = 0; i < l; i++) {
            if(Pixel(point[0], point[1], point[2], a) == ERROR) return ERROR; //aendern
            if (err_1 > 0) {
                point[1] += y_inc;
                err_1 -= dx2;
            }
            if (err_2 > 0) {
                point[2] += z_inc;
                err_2 -= dx2;
            }
            err_1 += dy2;
            err_2 += dz2;
            point[0] += x_inc;
        }
    } else if ((m >= l) && (m >= n)) {
        err_1 = dx2 - m;
        err_2 = dz2 - m;
        for (i = 0; i < m; i++) {
            if(Pixel(point[0], point[1], point[2], a) == ERROR) return ERROR; //aendern
            if (err_1 > 0) {
                point[0] += x_inc;
                err_1 -= dy2;
            }
            if (err_2 > 0) {
                point[2] += z_inc;
                err_2 -= dy2;
            }
            err_1 += dx2;
            err_2 += dz2;
            point[1] += y_inc;
        }
    } else {
        err_1 = dy2 - n;
        err_2 = dx2 - n;
        for (i = 0; i < n; i++) {
            if(Pixel(point[0], point[1], point[2], a) == ERROR) return ERROR; //aendern
            if (err_1 > 0) {
                point[1] += y_inc;
                err_1 -= dz2;
            }
            if (err_2 > 0) {
                point[0] += x_inc;
                err_2 -= dz2;
            }
            err_1 += dy2;
            err_2 += dx2;
            point[2] += z_inc;
        }
    }
    if(Pixel(point[0], point[1], point[2], a) == ERROR) return ERROR; //aendern
	
	return OK;
}