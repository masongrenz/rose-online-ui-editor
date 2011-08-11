using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;

namespace Rose_online_UI_Editor.Files_Handlers.Common
{
    #region enums
    public enum ClientType
    {
        IROSE,
        JROSE,
        NAROSE,
    }
    #endregion

    #region classe RoseFile
    class RoseFile
    {
        #region Static Methodes
        /// <summary>
       /// Reads the Z string.
       /// </summary>
       /// <param name="br">The br.</param>
       /// <returns></returns>
       public static string ReadZString(ref BinaryReader br)
       {
           string mystring = "";
           while (true)
           {

               byte addingValue = br.ReadByte();
               if (addingValue == 0) break;
               mystring += (char)addingValue;
           }
           return mystring;
       }
       public static void WriteZString(ref BinaryWriter bw, string mystring)
       {
           byte[] byte_string = System.Text.Encoding.Default.GetBytes(mystring);
           bw.Write(byte_string, 0, mystring.Length);
           bw.Write((Byte)0);
       }
       public static string ReadFString(ref BinaryReader br, int length)
       {
           return new string(br.ReadChars(length));
       }
       public static void WriteFString(ref BinaryWriter bw, string mystring , int length)
       {
           byte[] mystring_byte = System.Text.Encoding.Default.GetBytes(mystring);
           bw.Write(mystring_byte, 0, mystring_byte.Length);
           for (int i = mystring_byte.Length; i < length; i++)
           {
               bw.Write((byte)0);
           }
       }
       public static string ReadSString(ref BinaryReader br)
       {
           Int16 string_lenght = br.ReadInt16();
           string result = Encoding.UTF7.GetString(br.ReadBytes(string_lenght));
           return result;
       }
       public static void WriteSString(ref BinaryWriter bw, string mystring)
       {
           bw.Write((Int16)mystring.Length);
           byte[] mystring_byte = System.Text.Encoding.Default.GetBytes(mystring);
           bw.Write(mystring_byte, 0, mystring.Length);
       }
       public static string ReadBString(ref BinaryReader br)
       {
           int strlen = br.ReadByte();
           if ((strlen > 128))
           {
               strlen = strlen | (br.ReadByte() << 7);
           }
           return System.Text.Encoding.UTF8.GetString(br.ReadBytes(strlen));
       }
       public static void WriteBString(ref BinaryWriter bw, string mystring)
       {
           byte[] mystring_byte = System.Text.Encoding.Default.GetBytes(mystring);

           if (mystring.Length < 0x80)
           {
               bw.Write((Byte)mystring.Length);
           }
           else
           {

               if (mystring.Length < 0x100)
               {
                   bw.Write(Convert.ToByte(mystring.Length / 0x80));
               }
               else
               {
                   bw.Write(Convert.ToByte((mystring.Length / 0x80) - 1) * 0x80);
               }
               bw.Write((Byte)mystring.Length);

           }
           bw.Write(mystring_byte, 0, mystring_byte.Length);
       }
       
       public static Vector2 ReadVector2(ref BinaryReader br)
       {
           Vector2 result = new Vector2();
           result.X = br.ReadSingle();
           result.Y = br.ReadSingle();
           return result;
       }
       public static Vector3 ReadVector3(ref BinaryReader br)
       {
           Vector3 result = new Vector3();
           result.X = br.ReadSingle();
           result.Y = br.ReadSingle();
           result.Z = br.ReadSingle();
           return result;
       }
       public static Vector4 ReadVector4(ref BinaryReader br)
       {
           Vector4 result = new Vector4();
           result.W = br.ReadSingle();
           result.X = br.ReadSingle();
           result.Y = br.ReadSingle();
           result.Z = br.ReadSingle();
           return result;
       }
       public static void WriteVector2(ref BinaryWriter bw, Vector2 value)
       {
           bw.Write((Single)value.X);
           bw.Write((Single)value.Y);
       }
       public static void WriteVector3(ref BinaryWriter bw, Vector3 value)
       {
           bw.Write((Single)value.X);
           bw.Write((Single)value.Y);
           bw.Write((Single)value.Z);
       }
       public static void WriteVector4(ref BinaryWriter bw, Vector4 value)
       {
           bw.Write((Single)value.W);
           bw.Write((Single)value.X);
           bw.Write((Single)value.Y);
           bw.Write((Single)value.Z);
       }
        #endregion
    }
#endregion
}
