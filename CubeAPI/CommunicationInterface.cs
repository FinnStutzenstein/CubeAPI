using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Threading;

namespace CubeAPI
{
    internal class CommunicationInterface
    {
        SerialPort serialPort;

        const int DEFAULTWRITETIMEOUT = 25;
        const int DEFAULTREADTIMEOUT = 25;

        const int DEFAULTWRITETIMEOUT_CONNECT = 100;
        const int DEFAULTREADTIMEOUT_CONNECT = 100;

        const int STANDARTBAUDRATE = 250000;

        public CommunicationInterface() {}

        public CommunicationInterface(string port)
        {
            try
            {
                init(STANDARTBAUDRATE, port, DEFAULTREADTIMEOUT, DEFAULTWRITETIMEOUT);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public CommunicationInterface(int baud, string port)
        {
            try
            {
                init(baud, port, DEFAULTREADTIMEOUT, DEFAULTWRITETIMEOUT);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public CommunicationInterface(int baud, string port, int ReadTimeout, int WriteTimeout)
        {
            try
            {
                init(baud, port, ReadTimeout, WriteTimeout);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void init(string port)
        {
            init(STANDARTBAUDRATE, port, DEFAULTREADTIMEOUT, DEFAULTWRITETIMEOUT);
        }

        public void init(int baud, string port)
        {
            init(baud, port, DEFAULTREADTIMEOUT, DEFAULTWRITETIMEOUT);
        }

        public void init(int baud, string port, int ReadTimeout, int WriteTimeout)
        {
            // Create a new SerialPort object with default settings.
            serialPort = new SerialPort();

            try
            {
                serialPort.PortName = port;
                serialPort.BaudRate = baud;
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException("Baud oder Port falsch", e);
            }

            // Set the read/write timeouts
            try
            {
                serialPort.ReadTimeout = ReadTimeout;
                serialPort.WriteTimeout = WriteTimeout;
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException("Read- oder WriteTimeout falsch", e);
            }
        }


        public void Connect()
        {
            if (serialPort == null) throw new NoInitialisationException("SerialConnection wurde nicht initialisiert");


            try
            {
                serialPort.Open();
            }
            catch (Exception e)
            {
                throw new ConnectionRefusedException("Kann nicht zu " + serialPort.PortName + " verbinden", e);
            }
        }

        public string AutoConnect()
        {
            try
            {
                return AutoConnect(STANDARTBAUDRATE, DEFAULTREADTIMEOUT_CONNECT, DEFAULTWRITETIMEOUT_CONNECT);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string AutoConnect(int baud)
        {
            try
            {
                return AutoConnect(baud, DEFAULTREADTIMEOUT_CONNECT, DEFAULTWRITETIMEOUT_CONNECT);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string AutoConnect(int baud, int ReadTimeout, int WriteTimeout) //return: Portname, "" falls erfolglos
        {
            string[] ports = getAvailablePortNames();

            foreach (string s in ports)
            {
                init(baud, s, ReadTimeout, WriteTimeout);
                try
                {
                    Connect();

                    if (isConnected())
                    {
                        write(CTP.Handshake());
                        byte ret = read();
                        if (ret == (byte)CTP.Handshake().ToCharArray()[0]) //Handshake bekommen
                        {
                            return s; //aktuellen Portnamen zurückgeben
                        }
                    }
                }
                catch (ConnectionRefusedException)
                {
                    //Nächsten probieren
                }
                catch (NoInitialisationException)
                {
                    throw new NoInitialisationException("SerialConnection wurde nicht initialisiert");
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            return ""; //nichts gefunden
        }

        public void Disconnect()
        {
            try
            {
                serialPort.Close();
            }
            catch (Exception e)
            {
                throw new NoInitialisationException("CommunicationInterface wurde nicht initialisiert!", e);
            }
        }

        public bool isConnected()
        {
            try
            {
                return serialPort.IsOpen;
            }
            catch (Exception e)
            {
                throw new NoInitialisationException("CommunicationInterface wurde nicht initialisiert!", e);
            }
        }


        public void write(string s)
        {
            try
            {
                if (isConnected()){
                    byte[] bytes = StringToByteArray(s);
                    write(bytes);
                }
                else throw new NoConnectionException("Nicht verbunden!");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void write(char[] c)
        {
            try
            {
                if (isConnected())
                {
                    byte[] bytes = CharToByteArray(c);
                    write(bytes);
                }
                else throw new NoConnectionException("Nicht verbunden!");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void write(byte[] b)
        {
            try
            {
                if (isConnected()) serialPort.Write(b, 0, b.Length);
                else throw new NoConnectionException("Nicht verbunden!");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void writeCube(vCube c)
        {

            byte[] b_layer;

            for (byte layer = 0; layer < 1; layer++) //Für jedes Layer   < 10 !! jetzt nur layer 0
            {
                b_layer = new byte[14];
                b_layer[0] = (byte)'v';

                for (int i = 0; i < 100; i++) //jeder Pixel
                {
                    int x = i % 10; //x im vCube
                    int z = i / 10; //z im vCube

                    int position = (int) Math.Floor(i / 8.0); //immer 8 Bit in einer Speicherstelle
                    b_layer[position] = (byte)(b_layer[position] | ((c.get_Pixel(x, layer, z) ? 1 : 0) << (i % 8)) );   
                }

                //layer einfügen

                b_layer[13] =  (byte) (b_layer[13] | ((byte)layer << 4));

                

                write(b_layer);

            }
        }

        public byte read()
        {
            try
            {
                byte read;
                
                do
                {
                    read = (byte)serialPort.ReadByte();
                } while (serialPort.BytesToRead > 0);

                return read;
            }
            catch(TimeoutException e)
            {
                throw e;
            }
        }


        public string getPort()
        {
            try
            {
                return serialPort.PortName;
            }
            catch (Exception e)
            {
                throw new NoInitialisationException("CommunicationInterface wurde nicht initialisiert!", e);
            }

        }

        public int getBaudRate()
        {
            try
            {
                return serialPort.BaudRate;
            }
            catch (Exception e)
            {
                throw new NoInitialisationException("CommunicationInterface wurde nicht initialisiert!", e);
            }
        }


        public string[] getAvailablePortNames()
        {
            return SerialPort.GetPortNames();
        }

        public int getStandartBaudRate()
        {
            return STANDARTBAUDRATE;
        }

        private byte[] StringToByteArray(string str)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(str);
        }

        private byte[] CharToByteArray(char[] c)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(c);
        }
    }
}
