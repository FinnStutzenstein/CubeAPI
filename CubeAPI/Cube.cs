using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubeAPI
{
    public class Cube : ICube
    {
        private vCube bCube; //buffer
        private CommunicationInterface Cobj;

        private const string _VERSION = "1.0.0";

        public Cube()
        {
            bCube = new vCube();
            Cobj = new CommunicationInterface();
        }


        /* Todo:
         * - Weitere Funktionen uebertragen??
         * - Gross und Kleinschreibung bei Funktionen
         * 
         */

        public void Connect(string port)
        {
            try
            {
                Cobj.init(port);

                Cobj.Connect();

                Cobj.write(CTP.Handshake());
                byte ret = Cobj.read();
                if (ret != (byte)CTP.Handshake().ToCharArray()[0]) throw new ConnectionRefusedException("Kein Handshake beim Verbinden");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Connect(int baud, string port)
        {
            try
            {
                Cobj.init(baud, port);

                Cobj.Connect();

                Cobj.write(CTP.Handshake());
                byte ret = Cobj.read();
                if (ret != (byte)CTP.Handshake().ToCharArray()[0]) throw new ConnectionRefusedException("Kein Handshake beim Verbinden");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Connect(int baud, string port, int ReadTimeout, int WriteTimeout)
        {
            try
            {
                Cobj.init(baud, port, ReadTimeout, WriteTimeout);

                Cobj.Connect();

                Cobj.write(CTP.Handshake());
                byte ret = Cobj.read();
                if (ret != (byte)CTP.Handshake().ToCharArray()[0]) throw new ConnectionRefusedException("Kein Handshake beim Verbinden");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string AutoConnect()
        {
            try
            {
                return Cobj.AutoConnect();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Disconnect()
        {
            try
            {
                Cobj.Disconnect();
            }
            catch
            {
                //Nix tun
            }
        }

        public bool isConnected()
        {
            return Cobj.isConnected();
        }

        public string[] getAvailablePortNames()
        {
            return Cobj.getAvailablePortNames();
        }

        public int getStandartBaudRate()
        {
            return Cobj.getStandartBaudRate();
        }

        public string Version()
        {
            return _VERSION;
        }

        public void Flush()
        {
            try
            {
                Cobj.writeCube(bCube);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Flush(vCube cube)
        {
            try
            {
                bCube = cube;
                Flush();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void setBufferCube(vCube bCube)
        {
            this.bCube = bCube;
        }

        public vCube getBufferCube()
        {
            return bCube;
        }

        public void writeCube(vCube cube)
        {
            Cobj.writeCube(cube);
        }

        public void writeBytes(byte[] b)
        {
            Cobj.write(b);
        }

        public void sendCommand(string command)
        {
            try
            {
                Cobj.write(command);
                //string ret = Cobj.read();
                //if (ret != command) throw new ConnectionRefusedException("Keine Antwort auf Befehl \"" + command + "\"");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Handshake()
        {
            try
            {
                sendCommand(CTP.Handshake());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // !! Erst alles im bCube machen, falls es Fehler gibt, werden diese dort abgefangen und weitergeleitet, es muss keine zusätzliche Fehlerprüfung stattfinden
        #region "ICube implementierung" 

        public void set_WholeCube()
        {
            try
            {
                bCube.set_WholeCube();
                sendCommand(CTP.set_WholeCube());
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
                bCube.set_WholeCube(value);
                sendCommand(CTP.set_WholeCube(value));

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
                bCube.clear_WholeCube();
                sendCommand(CTP.clear_WholeCube());
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
                bCube.invert_WholeCube();
                sendCommand(CTP.invert_WholeCube());
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
                bCube.set_Layer(nr);
                sendCommand(CTP.set_Layer(nr));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_Layer(int nr, bool value)
        {
            try
            {
                bCube.set_Layer(nr, value);
                sendCommand(CTP.set_Layer(nr, value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_Layer(int nr)
        {
            try
            {
                bCube.clear_Layer(nr);
                sendCommand(CTP.clear_Layer(nr));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_Layer(int nr)
        {
            try
            {
                bCube.invert_Layer(nr);
                sendCommand(CTP.invert_Layer(nr));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void set_Pixel(int x, int y, int z)
        {
            try
            {
                bCube.set_Pixel(x, y, z);
                sendCommand(CTP.set_Pixel(x, y, z));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_Pixel(int x, int y, int z, bool value)
        {
            try
            {
                bCube.set_Pixel(x, y, z, value);
                sendCommand(CTP.set_Pixel(x, y, z, value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_Pixel(int x, int y, int z)
        {
            try
            {
                bCube.clear_Pixel(x, y, z);
                sendCommand(CTP.clear_Pixel(x, y, z));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_Pixel(int x, int y, int z)
        {
            try
            {
                bCube.invert_Pixel(x, y, z);
                sendCommand(CTP.invert_Pixel(x, y, z));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void set_Pixel(Pixel p)
        {
            try
            {
                bCube.set_Pixel(p);
                sendCommand(CTP.set_Pixel(p));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_Pixel(Pixel p, bool value)
        {
            try
            {
                bCube.set_Pixel(p, value);
                sendCommand(CTP.set_Pixel(p, value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_Pixel(Pixel p)
        {
            try
            {
                bCube.clear_Pixel(p);
                sendCommand(CTP.clear_Pixel(p));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_Pixel(Pixel p)
        {
            try
            {
                bCube.invert_Pixel(p);
                sendCommand(CTP.invert_Pixel(p));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool get_Pixel(int x, int y, int z)
        {
            return bCube.get_Pixel(x, y, z);
        }
        public bool get_Pixel(Pixel p)
        {
            return bCube.get_Pixel(p);
        }

        public void set_Cube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.set_Cube(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.set_Cube(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_Cube(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {
            try
            {
                bCube.set_Cube(x1, y1, z1, x2, y2, z2, value);
                sendCommand(CTP.set_Cube(x1, y1, z1, x2, y2, z2, value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_Cube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.clear_Cube(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.clear_Cube(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_Cube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.invert_Cube(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.invert_Cube(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void set_Cube(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.set_Cube(p1, p2);
                sendCommand(CTP.set_Cube(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_Cube(Pixel p1, Pixel p2, bool value)
        {
            try
            {
                bCube.set_Cube(p1, p2, value);
                sendCommand(CTP.set_Cube(p1, p2, value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_Cube(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.clear_Cube(p1, p2);
                sendCommand(CTP.clear_Cube(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_Cube(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.invert_Cube(p1, p2);
                sendCommand(CTP.invert_Cube(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void set_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.set_FilledCube(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.set_FilledCube(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {
            try
            {
                bCube.set_FilledCube(x1, y1, z1, x2, y2, z2, value);
                sendCommand(CTP.set_FilledCube(x1, y1, z1, x2, y2, z2, value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.clear_FilledCube(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.clear_FilledCube(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_FilledCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.invert_FilledCube(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.invert_FilledCube(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void set_FilledCube(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.set_FilledCube(p1, p2);
                sendCommand(CTP.set_FilledCube(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_FilledCube(Pixel p1, Pixel p2, bool value)
        {
            try
            {
                bCube.set_FilledCube(p1, p2, value);
                sendCommand(CTP.set_FilledCube(p1, p2, value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_FilledCube(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.clear_FilledCube(p1, p2);
                sendCommand(CTP.clear_FilledCube(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_FilledCube(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.invert_FilledCube(p1, p2);
                sendCommand(CTP.invert_FilledCube(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void set_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.set_EdgeCube(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.set_EdgeCube(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {
            try
            {
                bCube.set_EdgeCube(x1, y1, z1, x2, y2, z2, value);
                sendCommand(CTP.set_EdgeCube(x1, y1, z1, x2, y2, z2, value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.clear_EdgeCube(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.clear_EdgeCube(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_EdgeCube(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.invert_EdgeCube(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.invert_EdgeCube(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void set_EdgeCube(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.set_EdgeCube(p1, p2);
                sendCommand(CTP.set_EdgeCube(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_EdgeCube(Pixel p1, Pixel p2, bool value)
        {
            try
            {
                bCube.set_EdgeCube(p1, p2, value);
                sendCommand(CTP.set_EdgeCube(p1, p2, value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_EdgeCube(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.clear_EdgeCube(p1, p2);
                sendCommand(CTP.clear_EdgeCube(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_EdgeCube(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.invert_EdgeCube(p1, p2);
                sendCommand(CTP.invert_EdgeCube(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void set_Line(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.set_Line(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.set_Line(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_Line(int x1, int y1, int z1, int x2, int y2, int z2, bool value)
        {
            try
            {
                bCube.set_Line(x1, y1, z1, x2, y2, z2, value);
                sendCommand(CTP.set_Line(x1, y1, z1, x2, y2, z2, value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_Line(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.clear_Line(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.clear_Line(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_Line(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            try
            {
                bCube.invert_Line(x1, y1, z1, x2, y2, z2);
                sendCommand(CTP.invert_Line(x1, y1, z1, x2, y2, z2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void set_Line(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.set_Line(p1, p2);
                sendCommand(CTP.set_Line(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void set_Line(Pixel p1, Pixel p2, bool value)
        {
            try
            {
                bCube.set_Line(p1, p2, value);
                sendCommand(CTP.set_Line(p1, p2, value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void clear_Line(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.clear_Line(p1, p2);
                sendCommand(CTP.clear_Line(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void invert_Line(Pixel p1, Pixel p2)
        {
            try
            {
                bCube.invert_Line(p1, p2);
                sendCommand(CTP.invert_Line(p1, p2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
