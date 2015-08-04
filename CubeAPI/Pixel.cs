using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubeAPI
{
    public sealed class Pixel
    {
        private int x, y, z;
        private bool value;

        public Pixel()
        {
            setCoords(0, 0, 0);
            this.value = false;
        }
        public Pixel(bool value)
        {
            setCoords(0, 0, 0);
            this.value = value;
        }
        public Pixel(int x, int y, int z)
        {
            try
            {
                setCoords(x, y, z);
                this.value = false;
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
            }
        }
        public Pixel(int x, int y, int z, bool value)
        {
            try
            {
                setCoords(x, y, z);
                this.value = value;
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
            }
        }



        public void setX(int x)
        {
            if (InRange(x))
                this.x = x;
            else throw new ArgumentOutOfRangeException("x (" + x + ") muss zwischen 0 und 9 liegen!");
        }
        public int getX() { return this.x; }
        
        public void setY(int y)
        {
            if (InRange(y))
                this.y = y;
            else throw new ArgumentOutOfRangeException("y (" + y + ") muss zwischen 0 und 9 liegen!");
        }
        public int getY() { return this.y; }

        public void setZ(int z)
        {
            if (InRange(z))
                this.z = z;
            else throw new ArgumentOutOfRangeException("z (" + z + ") muss zwischen 0 und 9 liegen!");
        }
        public int getZ() { return this.z; }

        public void setCoords(int x, int y, int z)
        {
            if (InRange(x) && InRange(y) && InRange(z))
            {
                setX(x);
                setY(y);
                setZ(z);
            }
            else throw new ArgumentOutOfRangeException("Alle Parameter muss zwischen 0 und 9 liegen!");
        }



        public void set_Pixel()
        {
            this.value = true;
        }
        public void set_Pixel(bool value)
        {
            this.value = value;
        }
        public void clear_Pixel()
        {
            this.value = false;
        }
        public void invert_Pixel()
        {
            this.value = !this.value;
        }

        public bool InRange(int x)
        {
            return (x >= 0) && (x <= 9);
        }
    }
}
