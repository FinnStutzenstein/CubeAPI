using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubeAPI
{
    public sealed class vCube : ICube
    {
        bool[,,] cube = new bool[10,10,10];

        public vCube()
        {
            clear_WholeCube();
        }
        public vCube(bool state)
        {
            set_WholeCube(state);
        }



        public bool get_Pixel(int x, int y, int z)
        {
            try
            {
                return cube[x, y ,z];
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Koordinaten zwischen 0 und 9!");
            }
        }
        public bool get_Pixel(Pixel p)
        {
            try
            {
                return cube[p.getX(), p.getY(), p.getZ()];
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Koordinaten zwischen 0 und 9!");
            }
        }

        public void set_WholeCube()
        {
            try
            {
                set_WholeCube(true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_WholeCube(bool value)
        {
            try
            {
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        for (int z = 0; z < 10; z++)
                        {
                            cube[x, y, z] = value;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_WholeCube()
        {
            try
            {
                set_WholeCube(false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_WholeCube()
        {
            try
            {
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        for (int z = 0; z < 10; z++)
                        {
                            cube[x, y, z] = !cube[x, y, z];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void set_Layer(int nr)
        {
            try
            {
                set_Layer(nr, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_Layer(int nr, bool value)
        {
            if (!InRange(nr))
            {
                throw new ArgumentOutOfRangeException("Layernummer ist ausserhalb von 0 bis 9");
            }
            for (int x = 0; x < 10; x++)
            {
                for (int z = 0; z < 10; z++)
                {
                    set_Pixel(x, nr, z, value);
                }
            } 
        }
        public void clear_Layer(int nr)
        {
            try
            {
                set_Layer(nr, false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_Layer(int nr)
        {
            if (!InRange(nr))
            {
                throw new ArgumentOutOfRangeException("Layernummer ist ausserhalb von 0 bis 9");
            }
            for (int x = 0; x < 10; x++)
            {
                for (int z = 0; z < 10; z++)
                {
                    invert_Pixel(x, nr, z);
                }
            } 
        }

        public void set_Pixel(int x, int y, int z)
        {
            try
            {
                cube[x, y, z] = true;
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Koordinaten zwischen 0 und 9!");
            }
        }
        public void set_Pixel(int x, int y, int z, bool value)
        {
            try
            {
                cube[x, y, z] = value;
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Koordinaten zwischen 0 und 9!");
            }
        }
        public void clear_Pixel(int x, int y, int z)
        {
            try
            {
                cube[x, y, z] = false;
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Koordinaten zwischen 0 und 9!");
            }
        }
        public void invert_Pixel(int x, int y, int z)
        {
            try
            {
                cube[x, y, z] = !cube[x, y, z];
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Koordinaten zwischen 0 und 9!");
            }
        }

        public void set_Pixel(Pixel p)
        {
            try
            {
                cube[p.getX(), p.getY(), p.getZ()] = true;
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Koordinaten zwischen 0 und 9!");
            }
        }
        public void set_Pixel(Pixel p, bool value)
        {
            try
            {
                cube[p.getX(), p.getY(), p.getZ()] = value;
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Koordinaten zwischen 0 und 9!");
            }
        }
        public void clear_Pixel(Pixel p)
        {
            try
            {
                cube[p.getX(), p.getY(), p.getZ()] = false;
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Koordinaten zwischen 0 und 9!");
            }
        }
        public void invert_Pixel(Pixel p)
        {
            try
            {
                cube[p.getX(), p.getY(), p.getZ()] = !cube[p.getX(), p.getY(), p.getZ()];
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Koordinaten zwischen 0 und 9!");
            }
        }

        public void set_Cube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                set_Cube(x1, y1, z1, x2, y2, z2, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_Cube(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {
            if (!InRange(x1) || !InRange(y1) || !InRange(z1) || !InRange(x2) || !InRange(y2) || !InRange(z2))
            {
                throw new ArgumentOutOfRangeException("Parameter sind ausserhalb von 0 bis 9");
            }
            if (x2<x1 || y2<y1 || z2<z1)
            {
                throw new ArgumentOutOfRangeException("Negativintervall");
            }

            try { set_FilledCube(x1, y1, z1, x2, y1, z2, value); }catch { } //Oben
            try { set_FilledCube(x1, y2, z1, x2, y2, z2, value); }catch { } //Unten
            try { set_FilledCube(x1, y1 + 1, z1, x2, y2 - 1, z1, value); }catch { } //Seite vorne  ###Error muss fuer kleine Wuerfel toleriert werden
            try { set_FilledCube(x1, y1 + 1, z2, x2, y2 - 1, z2, value); }catch { } //Seite hinten
            try { set_FilledCube(x2, y1 + 1, z1 + 1, x2, y2 - 1, z2 - 1, value); }catch { } //Seite rechts
            try { set_FilledCube(x1, y1 + 1, z1 + 1, x1, y2 - 1, z2 - 1, value); }catch { } //Seite links

        }
        public void clear_Cube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                set_Cube(x1, y1, z1, x2, y2, z2, false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_Cube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            if (!InRange(x1) || !InRange(y1) || !InRange(z1) || !InRange(x2) || !InRange(y2) || !InRange(z2))
            {
                throw new ArgumentOutOfRangeException("Parameter sind ausserhalb von 0 bis 9");
            }
            if (x2<x1 || y2<y1 || z2<z1)
            {
                throw new ArgumentOutOfRangeException("Negativintervall");
            }

            try { invert_FilledCube(x1, y1, z1, x2, y1, z2); }catch { } //Oben
            try { invert_FilledCube(x1, y2, z1, x2, y2, z2); }catch { } //Unten
            try { invert_FilledCube(x1, y1 + 1, z1, x2, y2 - 1, z1); }catch { } //Seite vorne  ###Error muss fuer kleine Wuerfel toleriert werden
            try { invert_FilledCube(x1, y1 + 1, z2, x2, y2 - 1, z2); }catch { } //Seite hinten
            try { invert_FilledCube(x2, y1 + 1, z1 + 1, x2, y2 - 1, z2 - 1); }catch { } //Seite rechts
            try { invert_FilledCube(x1, y1 + 1, z1 + 1, x1, y2 - 1, z2 - 1); }catch { } //Seite links
        }

        public void set_Cube(Pixel p1, Pixel p2)
        {
            set_Cube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public void set_Cube(Pixel p1, Pixel p2, bool value)
        {
            set_Cube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ(), value);
        }
        public void clear_Cube(Pixel p1, Pixel p2)
        {
            clear_Cube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public void invert_Cube(Pixel p1, Pixel p2)
        {
            invert_Cube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }

        public void set_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                set_FilledCube(x1, y1, z1, x2, y2, z2, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {

            if (!InRange(x1) || !InRange(y1) || !InRange(z1) || !InRange(x2) || !InRange(y2) || !InRange(z2))
            {
                throw new ArgumentOutOfRangeException("Parameter sind ausserhalb von 0 bis 9");
            }
            if (x2<x1 || y2<y1 || z2<z1)
            {
                throw new ArgumentOutOfRangeException("Negativintervall");
            }

            for (int i = x1; i <= x2; i++) //i,j,k fuer x,y,z; nur anderen namen, damit keine Verwechslung mit Parametern
            {
                for (int j = y1; j <= y2; j++)
                {
                    for (int k = z1; k <= z2; k++)
                    {
                        cube[i, j, k] = value;
                    }
                }
            }
        }
        public void clear_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                set_FilledCube(x1, y1, z1, x2, y2, z2, false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            if (!InRange(x1) || !InRange(y1) || !InRange(z1) || !InRange(x2) || !InRange(y2) || !InRange(z2))
            {
                throw new ArgumentOutOfRangeException("Parameter sind ausserhalb von 0 bis 9");
            }
            if (x2<x1 || y2<y1 || z2<z1)
            {
                throw new ArgumentOutOfRangeException("Negativintervall");
            }

            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    for (int k = z1; k <= z2; k++)
                    {
                        cube[i, j, k] = !cube[i, j, k];
                    }
                }
            }
        }

        public void set_FilledCube(Pixel p1, Pixel p2)
        {
            set_FilledCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public void set_FilledCube(Pixel p1, Pixel p2, bool value)
        {
            set_FilledCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ(), value);
        }
        public void clear_FilledCube(Pixel p1, Pixel p2)
        {
            clear_FilledCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public void invert_FilledCube(Pixel p1, Pixel p2)
        {
            invert_FilledCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }

        public void set_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                set_EdgeCube(x1, y1, z1, x2, y2, z2, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {

            if (!InRange(x1) || !InRange(y1) || !InRange(z1) || !InRange(x2) || !InRange(y2) || !InRange(z2))
            {
                throw new ArgumentOutOfRangeException("Parameter sind ausserhalb von 0 bis 9");
            }
            if (x2<x1 || y2<y1 || z2<z1)
            {
                throw new ArgumentOutOfRangeException("Negativintervall");
            }

            set_Line(x1, y1, z1, x1, y1, z2, value); //Unten
            set_Line(x1, y1, z1, x2, y1, z1, value);
            set_Line(x1, y1, z2, x2, y1, z2, value);
            set_Line(x2, y1, z1, x2, y1, z2, value);

            set_Line(x1, y2, z1, x1, y2, z2, value); //Oben
            set_Line(x1, y2, z1, x2, y2, z1, value);
            set_Line(x1, y2, z2, x2, y2, z2, value);
            set_Line(x2, y2, z1, x2, y2, z2, value);

            set_Line(x1, y1, z1, x1, y2, z1, value); //Seiten
            set_Line(x2, y1, z1, x2, y2, z1, value);
            set_Line(x1, y1, z2, x1, y2, z2, value);
            set_Line(x2, y1, z2, x2, y2, z2, value);
           
        }
        public void clear_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                set_EdgeCube(x1, y1, z1, x2, y2, z2, false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            if (!InRange(x1) || !InRange(y1) || !InRange(z1) || !InRange(x2) || !InRange(y2) || !InRange(z2))
            {
                throw new ArgumentOutOfRangeException("Parameter sind ausserhalb von 0 bis 9");
            }
            if (x2<x1 || y2<y1 || z2<z1)
            {
                throw new ArgumentOutOfRangeException("Negativintervall");
            }

            invert_Line(x1, y1, z1, x1, y1, z2); //Unten
            invert_Line(x1, y1, z1, x2, y1, z1);
            invert_Line(x1, y1, z2, x2, y1, z2);
            invert_Line(x2, y1, z1, x2, y1, z2);

            invert_Line(x1, y2, z1, x1, y2, z2); //Oben
            invert_Line(x1, y2, z1, x2, y2, z1);
            invert_Line(x1, y2, z2, x2, y2, z2);
            invert_Line(x2, y2, z1, x2, y2, z2);

            invert_Line(x1, y1, z1, x1, y2, z1); //Seiten
            invert_Line(x2, y1, z1, x2, y2, z1);
            invert_Line(x1, y1, z2, x1, y2, z2);
            invert_Line(x2, y1, z2, x2, y2, z2);
        }

        public void set_EdgeCube(Pixel p1, Pixel p2)
        {
            set_EdgeCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public void set_EdgeCube(Pixel p1, Pixel p2, bool value)
        {
            set_EdgeCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ(), value);
        }
        public void clear_EdgeCube(Pixel p1, Pixel p2)
        {
            clear_EdgeCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }
        public void invert_EdgeCube(Pixel p1, Pixel p2)
        {
            invert_EdgeCube(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }

        public void set_Line(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                set_Line(x1, y1, z1, x2, y2, z2, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_Line(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {

            if (!InRange(x1) || !InRange(y1) || !InRange(z1) || !InRange(x2) || !InRange(y2) || !InRange(z2))
            {
                throw new ArgumentOutOfRangeException("Parameter sind ausserhalb von 0 bis 9");
            }

            int i, dx, dy, dz, l, m, n, x_inc, y_inc, z_inc, err_1, err_2, dx2, dy2, dz2;
            int[] point = new int[3];
    
            point[0] = x1;
            point[1] = y1;
            point[2] = z1;
            dx = x2 - x1;
            dy = y2 - y1;
            dz = z2 - z1;
            x_inc = (dx < 0) ? -1 : 1;
            l = Math.Abs(dx);
            y_inc = (dy < 0) ? -1 : 1;
            m = Math.Abs(dy);
            z_inc = (dz < 0) ? -1 : 1;
            n = Math.Abs(dz);
            dx2 = l << 1;
            dy2 = m << 1;
            dz2 = n << 1;
    
            if ((l >= m) && (l >= n)) {
                err_1 = dy2 - l;
                err_2 = dz2 - l;
                for (i = 0; i < l; i++) {
			        set_Pixel(point[0], point[1], point[2], value); //aendern
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
                    set_Pixel(point[0], point[1], point[2], value); //aendern
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
                    set_Pixel(point[0], point[1], point[2], value); //aendern
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
            set_Pixel(point[0], point[1], point[2], value); //aendern

            
        }
        public void clear_Line(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                set_Line(x1, y1, z1, x2, y2, z2, false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_Line(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            if (!InRange(x1) || !InRange(y1) || !InRange(z1) || !InRange(x2) || !InRange(y2) || !InRange(z2))
            {
                throw new ArgumentOutOfRangeException("Parameter sind ausserhalb von 0 bis 9");
            }
            int i, dx, dy, dz, l, m, n, x_inc, y_inc, z_inc, err_1, err_2, dx2, dy2, dz2;
            int[] point = new int[3];

            point[0] = x1;
            point[1] = y1;
            point[2] = z1;
            dx = x2 - x1;
            dy = y2 - y1;
            dz = z2 - z1;
            x_inc = (dx < 0) ? -1 : 1;
            l = Math.Abs(dx);
            y_inc = (dy < 0) ? -1 : 1;
            m = Math.Abs(dy);
            z_inc = (dz < 0) ? -1 : 1;
            n = Math.Abs(dz);
            dx2 = l << 1;
            dy2 = m << 1;
            dz2 = n << 1;

            if ((l >= m) && (l >= n))
            {
                err_1 = dy2 - l;
                err_2 = dz2 - l;
                for (i = 0; i < l; i++)
                {
                    invert_Pixel(point[0], point[1], point[2]); //aendern
                    if (err_1 > 0)
                    {
                        point[1] += y_inc;
                        err_1 -= dx2;
                    }
                    if (err_2 > 0)
                    {
                        point[2] += z_inc;
                        err_2 -= dx2;
                    }
                    err_1 += dy2;
                    err_2 += dz2;
                    point[0] += x_inc;
                }
            }
            else if ((m >= l) && (m >= n))
            {
                err_1 = dx2 - m;
                err_2 = dz2 - m;
                for (i = 0; i < m; i++)
                {
                    invert_Pixel(point[0], point[1], point[2]); //aendern
                    if (err_1 > 0)
                    {
                        point[0] += x_inc;
                        err_1 -= dy2;
                    }
                    if (err_2 > 0)
                    {
                        point[2] += z_inc;
                        err_2 -= dy2;
                    }
                    err_1 += dx2;
                    err_2 += dz2;
                    point[1] += y_inc;
                }
            }
            else
            {
                err_1 = dy2 - n;
                err_2 = dx2 - n;
                for (i = 0; i < n; i++)
                {
                    invert_Pixel(point[0], point[1], point[2]); //aendern
                    if (err_1 > 0)
                    {
                        point[1] += y_inc;
                        err_1 -= dz2;
                    }
                    if (err_2 > 0)
                    {
                        point[0] += x_inc;
                        err_2 -= dz2;
                    }
                    err_1 += dy2;
                    err_2 += dx2;
                    point[2] += z_inc;
                }
            }
            invert_Pixel(point[0], point[1], point[2]); //aendern
        }

        public void set_Line(Pixel p1, Pixel p2)
        {
            set_Line(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ(), true);
        }
        public void set_Line(Pixel p1, Pixel p2, bool value)
        {
            set_Line(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ(), value);
        }
        public void clear_Line(Pixel p1, Pixel p2)
        {
            set_Line(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ(), false);
        }
        public void invert_Line(Pixel p1, Pixel p2)
        {
            invert_Line(p1.getX(), p1.getY(), p1.getZ(), p2.getX(), p2.getY(), p2.getZ());
        }

        public bool InRange(int x)
        {
            return (x>=0) && (x<=9);
        }

    }
}
