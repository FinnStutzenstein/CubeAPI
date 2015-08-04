using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubeAPI
{
    internal static class CTP
    {
        public static string set_WholeCube()
        {
            return "a";
        }
        public static string set_WholeCube(bool value)
        {
            if (value) return set_WholeCube();
            else return clear_WholeCube();
        }
        public static string clear_WholeCube()
        {
            return "b";
        }
        public static string invert_WholeCube()
        {
            return "c";
        }

        public static string set_Layer(int nr)
        {
            return "d" + nr.ToString();
        }
        public static string set_Layer(int nr, bool value)
        {
            if (value) return set_Layer(nr);
            else return clear_Layer(nr);
        }
        public static string clear_Layer(int nr)
        {
            return "e" + nr.ToString();
        }
        public static string invert_Layer(int nr)
        {
            return "f" + nr.ToString();
        }

        public static string set_Pixel(int x, int y, int z)
        {
            return "g" + x.ToString() + y.ToString() + z.ToString();
        }
        public static string set_Pixel(int x, int y, int z, bool value)
        {
            if (value) return set_Pixel(x, y, z);
            else return clear_Pixel(x, y, z);
        }
        public static string clear_Pixel(int x, int y, int z)
        {
            return "h" + x.ToString() + y.ToString() + z.ToString();
        }
        public static string invert_Pixel(int x, int y, int z)
        {
            return "i" + x.ToString() + y.ToString() + z.ToString();
        }

        public static string set_Pixel(Pixel p)
        {
            return "g" + p.getX().ToString() + p.getY().ToString() + p.getZ().ToString();
        }
        public static string set_Pixel(Pixel p, bool value)
        {
            if (value) return set_Pixel(p);
            else return clear_Pixel(p);
        }
        public static string clear_Pixel(Pixel p)
        {
            return "h" + p.getX().ToString() + p.getY().ToString() + p.getZ().ToString();
        }
        public static string invert_Pixel(Pixel p)
        {
            return "i" + p.getX().ToString() + p.getY().ToString() + p.getZ().ToString();
        }

        public static string set_Cube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "j" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }
        public static string set_Cube(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {
            if (value) return set_Cube(x1, y1, z1, x2, y2, z2);
            else return clear_Cube(x1, y1, z1, x2, y2, z2);
        }
        public static string clear_Cube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "k" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }
        public static string invert_Cube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "l" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }

        public static string set_Cube(Pixel p1, Pixel p2)
        {
            return set_Cube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public static string set_Cube(Pixel p1, Pixel p2, bool value)
        {
            return set_Cube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ(), value);
        }
        public static string clear_Cube(Pixel p1, Pixel p2)
        {
            return clear_Cube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public static string invert_Cube(Pixel p1, Pixel p2)
        {
            return invert_Cube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }

        public static string set_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "m" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }
        public static string set_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {
            if (value) return set_FilledCube(x1, y1, z1, x2, y2, z2);
            else return clear_FilledCube(x1, y1, z1, x2, y2, z2);
        }
        public static string clear_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "n" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }
        public static string invert_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "o" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }

        public static string set_FilledCube(Pixel p1, Pixel p2)
        {
            return set_FilledCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public static string set_FilledCube(Pixel p1, Pixel p2, bool value)
        {
            return set_FilledCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ(), value);
        }
        public static string clear_FilledCube(Pixel p1, Pixel p2)
        {
            return clear_FilledCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public static string invert_FilledCube(Pixel p1, Pixel p2)
        {
            return invert_FilledCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }

        public static string set_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "p" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }
        public static string set_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {
            if (value) return set_EdgeCube(x1, y1, z1, x2, y2, z2);
            else return clear_EdgeCube(x1, y1, z1, x2, y2, z2);
        }
        public static string clear_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "q" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }
        public static string invert_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "r" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }

        public static string set_EdgeCube(Pixel p1, Pixel p2)
        {
            return set_EdgeCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public static string set_EdgeCube(Pixel p1, Pixel p2, bool value)
        {
            return set_EdgeCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ(), value);
        }
        public static string clear_EdgeCube(Pixel p1, Pixel p2)
        {
            return clear_EdgeCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public static string invert_EdgeCube(Pixel p1, Pixel p2)
        {
            return invert_EdgeCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }

        public static string set_Line(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "s" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }
        public static string set_Line(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {
            if (value) return set_Line(x1, y1, z1, x2, y2, z2);
            else return clear_Line(x1, y1, z1, x2, y2, z2);
        }
        public static string clear_Line(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "t" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }
        public static string invert_Line(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return "u" + x1.ToString() + y1.ToString() + z1.ToString() + x2.ToString() + y2.ToString() + z2.ToString();
        }

        public static string set_Line(Pixel p1, Pixel p2)
        {
            return set_Line(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public static string set_Line(Pixel p1, Pixel p2, bool value)
        {
            return set_Line(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ(), value);
        }
        public static string clear_Line(Pixel p1, Pixel p2)
        {
            return clear_Line(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public static string invert_Line(Pixel p1, Pixel p2)
        {
            return invert_Line(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }

        public static string sendCube()
        {
            return "v";
        }
        public static string Handshake()
        {
            return "x";
        }
        public static string Reset()
        {
            return "y";
        }

    }
}
