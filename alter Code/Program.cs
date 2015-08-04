using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace PortChat
{
    class Program
    {

        static bool _continue;
        static SerialPort sp;

        static void Main(string[] args)
        {
            string message;
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

            // Create a new SerialPort object with default settings.
            sp = new SerialPort();

            // Allow the user to set the appropriate properties.
            sp.PortName = SetPortName(/*sp.PortName*/"COM5");
            sp.BaudRate = SetPortBaudRate(/*sp.BaudRate57600*/250000);
            //sp.Parity = SetPortParity(sp.Parity);
            //sp.DataBits = SetPortDataBits(sp.DataBits);
            //sp.StopBits = SetPortStopBits(sp.StopBits);
            //sp.Handshake = SetPortHandshake(sp.Handshake);

            // Set the read/write timeouts
            sp.ReadTimeout = 25;
            sp.WriteTimeout = 25;

            sp.Open();
            _continue = true;
            //readThread.Start();

            Console.WriteLine("Type QUIT to exit");

           /* while (_continue)
            {
                //sp.DiscardInBuffer();
                message = Console.ReadLine();

                if (stringComparer.Equals("quit", message))
                {
                    _continue = false;
                }
                else if (message.StartsWith("n")) //Ganze Ebene
                {
                    byte[] bytes = { 103, 49, 49, 49 };

                    for (int i = 0; i < 100; i++)
                    {
                        bytes[0] = 103;
                        senden(bytes,false);
                        bytes[0] = 104;
                        senden(bytes,false);
                    }

                }
                else if (message.StartsWith("v")) //Ganze Ebene
                {

                    message = "vx31B0C01122334455FF00FF09CC";

                     string[] test = {"vx0FE10197F1F708950F931F9381",
                                      "vx13482F50E02091D2003091D300",
                                      "vx2F931F93DF93CF93CDB7F2F309",
                                      "vx31B0C01122334455FF00FF09CC",
                                      "vx430910509F408C18C309105165",
                                      "vx5740085E890E066EA70E00E946",
                                      "vx656C007C08330910571F104973",
                                      "vx71050CF475C001979093C4004C",
                                      "vx8F716F45DE251931082C9010C9",
                                      "vx9B70B881F991F5A95A9F780430"};

                    //string[] test = { "vx3FFFFFFFFFFFFFFFFFFFFFFFFF" };

                     foreach (String test_ in test)
                     {

                         try
                         {
                             message = test_;

                             char[] c = message.ToCharArray();
                             if (c.Length == 28)
                             {
                                 byte[] bytes = new byte[14];

                                 if (c[1] == 'x')
                                 {
                                     for (int i = 0; i < 26; i += 2)
                                     {
                                         string subHex = message.Substring(i + 2, 2);
                                         bytes[(i / 2) + 1] = Convert.ToByte(subHex, 16);
                                     }

                                     bytes[0] = (byte)'v';

                                     senden(bytes, false);

                                 }
                                 else if (c[1] == 'b')
                                 {

                                 }
                                 else
                                 {
                                     Console.WriteLine("Muss vx oder vb");
                                 }
                             }
                             else
                             {
                                 Console.WriteLine("Falsche Länge: Muss 28 (vx+26 oder vb+26)");
                             }
                         }
                         catch (TimeoutException)
                         {
                             Console.WriteLine("Versuch gescheitert (Timeout)");
                         }
                         catch (ArgumentException)
                         {
                             Console.WriteLine("Versuch gescheitert (Antwort falsch)");
                         }
                     }

                }
                else if (!stringComparer.Equals("", message))
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(message);
                    senden(bytes,true);
                }
                else //Leere Eingabe
                {
                    if (sp.BytesToRead == 0)
                    {
                        Console.WriteLine("Inputbuffer Leer!");
                    }

                    while (sp.BytesToRead > 0)
                    {
                        byte ret = (byte)sp.ReadByte();
                        Console.Write((char)ret);
                    }
                    Console.WriteLine("");
                }
            }*/

            for (int lauf = 0; lauf < 50; lauf++)
            {

                message = "vx31B0C01122334455FF00FF09CC";

                string[] test = new String[10];

                if (lauf % 2 == 0)
                {
                    test[0] = "vx0FE10197F1F708950F931F9381";
                    test[1] = "vx13482F50E02091D2003091D300";
                    test[2] = "vx2F931F93DF93CF93CDB7F2F309";
                    test[3] = "vx31B0C01122334455FF00FF09CC";
                    test[4] = "vx430910509F408C18C309105165";
                    test[5] = "vx5740085E890E066EA70E00E946";
                    test[6] = "vx656C007C08330910571F104973";
                    test[7] = "vx71050CF475C001979093C4004C";
                    test[8] = "vx8F716F45DE251931082C9010C9";
                    test[9] = "vx9B70B881F991F5A95A9F780430";
                }
                else
                {
                    test[0] = "vx00000000000000000000000000";
                    test[1] = "vx10000000000000000000000000";
                    test[2] = "vx20000000000000000000000000";
                    test[3] = "vx30000000000000000000000000";
                    test[4] = "vx40000000000000000000000000";
                    test[5] = "vx50000000000000000000000000";
                    test[6] = "vx60000000000000000000000000";
                    test[7] = "vx70000000000000000000000000";
                    test[8] = "vx80000000000000000000000000";
                    test[9] = "vx90000000000000000000000000";
                }

                //string[] test = { "vx3FFFFFFFFFFFFFFFFFFFFFFFFF" };

                foreach (String test_ in test)
                {

                    try
                    {
                        message = test_;

                        char[] c = message.ToCharArray();
                        if (c.Length == 28)
                        {
                            byte[] bytes = new byte[14];

                            if (c[1] == 'x')
                            {
                                for (int i = 0; i < 26; i += 2)
                                {
                                    string subHex = message.Substring(i + 2, 2);
                                    bytes[(i / 2) + 1] = Convert.ToByte(subHex, 16);
                                }

                                bytes[0] = (byte)'v';

                                senden(bytes, false);

                            }
                            else if (c[1] == 'b')
                            {

                            }
                            else
                            {
                                Console.WriteLine("Muss vx oder vb");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Falsche Länge: Muss 28 (vx+26 oder vb+26)");
                        }
                    }
                    catch (TimeoutException)
                    {
                        Console.WriteLine("Versuch gescheitert (Timeout)");
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Versuch gescheitert (Antwort falsch)");
                    }
                }
            }

        
            Console.WriteLine("ENDE!!");
            message = Console.ReadLine();                
            
            sp.Close();
        }

        public static void senden(byte[] bytes, bool check)
        {
            int pos = 0;

            int tries = 3;
            bool error = false;

            while (!error && pos < bytes.Length)
            {
                Console.WriteLine("Zeichen: {0}, {1}", (char)bytes[pos], bytes[pos]);

                try
                {
                    sp.Write(bytes, pos, 1); //Ein byte schreiben

                    if (check)
                    {
                        byte ret = 0;
                        do
                        {
                            ret = (byte)sp.ReadByte();

                            Console.WriteLine("Empfangen: {0}", (char)ret);
                        } while (sp.BytesToRead > 0);

                        //abgleichen: ret==aktuellesByte??
                        if (ret != bytes[pos] && bytes[0] != (byte)'z') //z: Version
                        {
                            throw new ArgumentException();
                        }
                    }

                    pos++;
                    tries = 3; //Zuruecksetzen
                }
                catch (TimeoutException)
                {
                    tries--;
                    Console.WriteLine("Versuch gescheitert (Timeout)");
                }
                catch (ArgumentException)
                {
                    tries--;
                    Console.WriteLine("Versuch gescheitert (Antwort falsch)");
                }

                if (tries == 0)
                {
                    Console.WriteLine("abgelaufen");
                    error = true;
                }
            }

            if (error)
            {
                Console.WriteLine("Fehler");
            }
            else
            {
                Console.WriteLine("OK");
            }

        }

        // Display Port values and prompt user to enter a port. 
        public static string SetPortName(string defaultPortName)
        {
            string portName;

            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
            portName = Console.ReadLine();

            if (portName == "" || !(portName.ToLower()).StartsWith("com"))
            {
                portName = defaultPortName;
            }
            return portName;
        }
        // Display BaudRate values and prompt user to enter a value. 
        public static int SetPortBaudRate(int defaultPortBaudRate)
        {
            string baudRate;

            Console.Write("Baud Rate(default: {0}): ", defaultPortBaudRate);
            baudRate = Console.ReadLine();

            if (baudRate == "")
            {
                baudRate = defaultPortBaudRate.ToString();
            }

            return int.Parse(baudRate);
        }

        // Display PortParity values and prompt user to enter a value. 
        public static Parity SetPortParity(Parity defaultPortParity)
        {
            string parity;

            Console.WriteLine("Available Parity options:");
            foreach (string s in Enum.GetNames(typeof(Parity)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter Parity value (Default: {0}):", defaultPortParity.ToString(), true);
            parity = Console.ReadLine();

            if (parity == "")
            {
                parity = defaultPortParity.ToString();
            }

            return (Parity)Enum.Parse(typeof(Parity), parity, true);
        }
        // Display DataBits values and prompt user to enter a value. 
        public static int SetPortDataBits(int defaultPortDataBits)
        {
            string dataBits;

            Console.Write("Enter DataBits value (Default: {0}): ", defaultPortDataBits);
            dataBits = Console.ReadLine();

            if (dataBits == "")
            {
                dataBits = defaultPortDataBits.ToString();
            }

            return int.Parse(dataBits.ToUpperInvariant());
        }

        // Display StopBits values and prompt user to enter a value. 
        public static StopBits SetPortStopBits(StopBits defaultPortStopBits)
        {
            string stopBits;

            Console.WriteLine("Available StopBits options:");
            foreach (string s in Enum.GetNames(typeof(StopBits)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter StopBits value (None is not supported and \n" +
             "raises an ArgumentOutOfRangeException. \n (Default: {0}):", defaultPortStopBits.ToString());
            stopBits = Console.ReadLine();

            if (stopBits == "")
            {
                stopBits = defaultPortStopBits.ToString();
            }

            return (StopBits)Enum.Parse(typeof(StopBits), stopBits, true);
        }
        public static Handshake SetPortHandshake(Handshake defaultPortHandshake)
        {
            string handshake;

            Console.WriteLine("Available Handshake options:");
            foreach (string s in Enum.GetNames(typeof(Handshake)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("End Handshake value (Default: {0}):", defaultPortHandshake.ToString());
            handshake = Console.ReadLine();

            if (handshake == "")
            {
                handshake = defaultPortHandshake.ToString();
            }

            return (Handshake)Enum.Parse(typeof(Handshake), handshake, true);
        }


    }
}