using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubeAPI
{
    internal interface ICube{
    
        void set_WholeCube();
        void set_WholeCube(bool value);
        void clear_WholeCube();
        void invert_WholeCube();

        void set_Layer(int nr);
        void set_Layer(int nr, bool value);
        void clear_Layer(int nr);
        void invert_Layer(int nr);

        void set_Pixel(int x, int y, int z);
        void set_Pixel(int x, int y, int z, bool value);
        void clear_Pixel(int x, int y, int z);
        void invert_Pixel(int x, int y, int z);

        void set_Pixel(Pixel p);
        void set_Pixel(Pixel p, bool value);
        void clear_Pixel(Pixel p);
        void invert_Pixel(Pixel p);

        bool get_Pixel(int x, int y, int z);
        bool get_Pixel(Pixel p);

        void set_Cube(int x1, int y1, int z1, int x2, int y2, int z2);
        void set_Cube(int x1, int y1, int z1, int x2, int y2, int z2, bool value);
        void clear_Cube(int x1, int y1, int z1, int x2, int y2, int z2);
        void invert_Cube(int x1, int y1, int z1, int x2, int y2, int z2);

        void set_Cube(Pixel p1, Pixel p2);
        void set_Cube(Pixel p1, Pixel p2, bool value);
        void clear_Cube(Pixel p1, Pixel p2);
        void invert_Cube(Pixel p1, Pixel p2);

        void set_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2);
        void set_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2, bool value);
        void clear_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2);
        void invert_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2);

        void set_FilledCube(Pixel p1, Pixel p2);
        void set_FilledCube(Pixel p1, Pixel p2, bool value);
        void clear_FilledCube(Pixel p1, Pixel p2);
        void invert_FilledCube(Pixel p1, Pixel p2);

        void set_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2);
        void set_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2, bool value);
        void clear_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2);
        void invert_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2);

        void set_EdgeCube(Pixel p1, Pixel p2);
        void set_EdgeCube(Pixel p1, Pixel p2, bool value);
        void clear_EdgeCube(Pixel p1, Pixel p2);
        void invert_EdgeCube(Pixel p1, Pixel p2);

        void set_Line(int x1, int y1, int z1, int x2, int y2, int z2);
        void set_Line(int x1, int y1, int z1, int x2, int y2, int z2, bool value);
        void clear_Line(int x1, int y1, int z1, int x2, int y2, int z2);
        void invert_Line(int x1, int y1, int z1, int x2, int y2, int z2);

        void set_Line(Pixel p1, Pixel p2);
        void set_Line(Pixel p1, Pixel p2, bool value);
        void clear_Line(Pixel p1, Pixel p2);
        void invert_Line(Pixel p1, Pixel p2);
    }
}
