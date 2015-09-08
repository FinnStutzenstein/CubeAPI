using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Threading;

namespace CubeAPI
{
    internal class CommunicationInterface : IDisposable
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
            catch
            {
                throw;
            }
        }


        public CommunicationInterface(int baud, string port)
        {
            try
            {
                init(baud, port, DEFAULTREADTIMEOUT, DEFAULTWRITETIMEOUT);
            }
            catch
            {
                throw;
            }
        }

        public CommunicationInterface(int baud, string port, int ReadTimeout, int WriteTimeout)
        {
            try
            {
                init(baud, port, ReadTimeout, WriteTimeout);
            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            serialPort.Dispose();
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
            if (!isInitialized()) throw new NoInitialisationException("SerialConnection wurde nicht initialisiert");


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
            return AutoConnect(STANDARTBAUDRATE, DEFAULTREADTIMEOUT_CONNECT, DEFAULTWRITETIMEOUT_CONNECT);
        }

        public string AutoConnect(int baud)
        {
            try
            {
                return AutoConnect(baud, DEFAULTREADTIMEOUT_CONNECT, DEFAULTWRITETIMEOUT_CONNECT);
            }
            catch
            {
                throw;
            }
        }

        public string AutoConnect(int baud, int ReadTimeout, int WriteTimeout) //return: Portname, "" falls erfolglos
        {
            string[] ports = getAvailablePortNames();

            foreach (string s in ports)
            {
                if (this.isInitialized()) //falls offen, schliessen!
                {
                    serialPort.Close();
                }

                try
                {
                    init(baud, s, ReadTimeout, WriteTimeout); // initialisieren
                    serialPort.Open(); //öffnen

                    if (isConnected()) //wenn verbunden
                    {
                        this.write(CTP.Handshake()); //auf handshake prüfen
                        byte ret = this.read();      // falls keiner kommt --> error und Nächsten probieren
                        if (ret != (byte)CTP.Handshake().ToCharArray()[0]) throw new ConnectionRefusedException("Kein Handshake beim Verbinden");

                        return s;
                    }
                } 
                catch (NoInitialisationException)
                {
                    throw new NoInitialisationException("SerialConnection wurde nicht initialisiert");
                }
                catch
                {
                    //nix unternehmen bei Fehlern, nur neuen Versuch starten
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

        public bool isInitialized()
        {
            return serialPort != null;
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

        public bool isReady()
        {
            if (isInitialized())
            {
                if (isConnected()) { return true; }
            }

            return false;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
            }
        }

        public void write(byte[] b)
        {
            /*try
            {
                if (isConnected()) serialPort.Write(b, 0, b.Length);
                else throw new NoConnectionException("Nicht verbunden!");
            }
            catch
            {
                throw;
            }*/

            int pos = 0;

            while (pos < b.Length)
            {

                try
                {
                    serialPort.Write(b, pos, 1); //Ein byte schreiben

                    pos++;
                }
                catch
                {
                    throw;
                }
            
            }
        }

        public void writeCube(vCube c)
        {

            for (byte layer = 0; layer < 10; layer++) //Für jedes Layer
            {
                byte[] b_layer = new byte[14];

                b_layer[0] = (byte)'v';

                for (int i = 0; i < 96; i++) //jeder Pixel, ausser letzten 4
                {
                    int x = i % 10; //x im vCube
                    int z = (int) Math.Floor(i / 10.0); //z im vCube

                    int position = (int) Math.Floor(i / 8.0) + 2; //immer 8 Bit in einer Speicherstelle !!+2 weil versetzt (0:'v', 1:layer + letzte 4 Pixel)
                    b_layer[position] = (byte)(b_layer[position] | ((c.get_Pixel(x, layer, z) ? 1 : 0) << (i % 8)) );   
                }

                for (int x = 6; x < 10; x++) //Letzten 4 (z von 6 bis 9)
                { 
                    b_layer[1] = (byte)(b_layer[1] | ((c.get_Pixel(x, layer, 9) ? 1 : 0) << (x-6) ));  //an speicherstelle 1
                } 

                //layer einfügen
                b_layer[1] =  (byte) (b_layer[1] | ((byte)layer << 4));

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
            catch
            {
                throw;
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
