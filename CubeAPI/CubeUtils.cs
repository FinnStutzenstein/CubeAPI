using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubeAPI
{
    public static class CubeUtils
    {

        public static void saveCube (string filename, vCube cube)
        {
            byte[] bytes = cube.get_Bytes();

            FileStream fs = new FileStream(filename, FileMode.Create);

            try
            {  
                foreach (byte b in bytes)
                {
                    fs.WriteByte(b);
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

       public static vCube loadCube (string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException();
            }

            FileStream fs = new FileStream(filename, FileMode.Open);
            try
            {
                fs.Seek(0, SeekOrigin.Begin);
                byte[] bytes = new byte[125];

                if( ((int)fs.Length) != 125)
                {
                    throw new ArgumentOutOfRangeException("Datei ist beschädigt");
                }

                //lesen
                for (int i = 0; i < 125; i++)
                {
                    bytes[i] = (byte)fs.ReadByte();
                }

                vCube cube = new vCube(bytes);

                fs.Close();
                return cube;
            }
            catch
            {
                throw;
            }
            finally
            {
                fs.Close();
            }

        }

    }
}
