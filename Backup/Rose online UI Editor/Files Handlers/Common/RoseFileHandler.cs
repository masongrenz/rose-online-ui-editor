using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Rose_online_UI_Editor.Files_Handlers.Common
{
    class RoseFileHandler
    {
        private  BinaryWriter bw;
        private BinaryReader br;
        private Encoding encoding;

        
      public RoseFileHandler(string file_path, FileMode mode, Encoding encodeType)
        {
            if (mode == FileMode.Open)
            {
                br = new BinaryReader(File.Open(file_path, FileMode.Open));
            }
            else if (mode == FileMode.Create)
            {
                bw = new BinaryWriter(File.Open(file_path, FileMode.Create));
            }
            this.encoding = encodeType;
        }

        public void Dispose()
        {
            if (br != null) br.Close();
            if (bw != null) bw.Close();
        }
        public T Read<T>()
        {
            T local;
            
            if (typeof(T) == typeof(byte)) return (T)(Object)this.br.ReadByte();

            else if (typeof(T) == typeof(sbyte)) return (T)(Object)this.br.ReadSByte();

            else if (typeof(T) == typeof(char)) return (T)(Object)this.br.ReadChar();

            else if (typeof(T) == typeof(short)) return (T)(Object)this.br.ReadInt16();

            else if (typeof(T) == typeof(ushort)) return (T)(Object)this.br.ReadUInt16();

            else if (typeof(T) == typeof(int)) return (T)(Object)this.br.ReadInt32();

            else if (typeof(T) == typeof(uint)) return (T)(Object)this.br.ReadUInt32();

            else if (typeof(T) == typeof(long)) return (T)(Object)this.br.ReadInt64();

            else if (typeof(T) == typeof(ulong)) return (T)(Object)this.br.ReadUInt64();

            else if (typeof(T) == typeof(float)) return (T)(Object)this.br.ReadSingle();

            else if (typeof(T) == typeof(double)) return (T)(Object)this.br.ReadDouble();

            else if (typeof(T) == typeof(decimal)) return (T)(Object)this.br.ReadDecimal();

            else
            {
                GCHandle handle = GCHandle.Alloc(this.br.ReadBytes(Marshal.SizeOf(typeof(T))), GCHandleType.Pinned);
                try
                {
                    local = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
                }
                finally
                {
                    handle.Free();
                }
                return (T)(object)local;
            }
            }

        public T Read<T>(int length)
        {
            if (typeof(T) == typeof(byte[]))
            {
                return (T)(Object)this.br.ReadBytes(length);
            }
            else if(typeof(T) == typeof(string) )
            {
                return (T)(Object)this.encoding.GetString(this.br.ReadBytes(length));
            }
                else
            {
             throw new Exception("Invalid data type");
            }

        }

        public string ReadBString()
        {
            int strlen = this.Read<byte>();
        
        if((strlen > 128))
        {

            strlen = strlen | (this.Read<byte>() << 7);
            
        }

        return this.Read<string>(strlen);
        }

        public string ReadSString()
        {
            return this.Read<string>(this.Read<short>());
        }

        public string ReadNString()
        {
      char read_value =  Read<char>();
        if (read_value.ToString() == "")
        {
          return "";
        }
        
            return (read_value + ReadNString());
        }

        public void seek(long offset)
        {
            this.br.BaseStream.Seek(offset, SeekOrigin.Begin);
        }
        
        public void Write<T>(T value)
        {
            if (typeof(T) == typeof(string))
            {
                this.bw.Write(this.encoding.GetBytes((string)(object)value));
            }
            else if (typeof(T) == typeof(byte))
            {
                this.bw.Write((byte)(object)value);
            }
            else if (typeof(T) == typeof(byte[]))
            {
                this.bw.Write((byte[])(object)value);
            }
            else if (typeof(T) == typeof(sbyte))
            {
                this.bw.Write((sbyte)(object)value);
            }
            else if (typeof(T) == typeof(char))
            {
                this.bw.Write((char)(object)value);
            }
            else if (typeof(T) == typeof(short))
            {
                this.bw.Write((short)(object)value);
            }
            else if (typeof(T) == typeof(ushort))
            {
                this.bw.Write((ushort)(object)value);
            }
            else if (typeof(T) == typeof(int))
            {
                this.bw.Write((int)(object)value);
            }
            else if (typeof(T) == typeof(uint))
            {
                this.bw.Write((uint)(object)value);
            }
            else if (typeof(T) == typeof(long))
            {
                this.bw.Write((long)(object)value);
            }
            else if (typeof(T) == typeof(ulong))
            {
                this.bw.Write((ulong)(object)value);
            }
            else if (typeof(T) == typeof(float))
            {
                this.bw.Write((float)(object)value);
            }
            else if (typeof(T) == typeof(double))
            {
                this.bw.Write((double)(object)value);
            }
            else if (typeof(T) == typeof(decimal))
            {
                this.bw.Write((decimal)(object)value);
            }
            else
            {
                int length = Marshal.SizeOf(typeof(T));
                byte[] buffer = new byte[length];
                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                try
                {
                    Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
                    Marshal.Copy(handle.AddrOfPinnedObject(), buffer, 0, length);
                }
                finally
                {
                    handle.Free();
                }
                this.bw.Write(buffer);
            }

        }
       
        public void WriteBString(string value)
        {
            Write<byte>((byte)(value.Length));
            Write<string>(value);
        }

        public void WriteSString(string value)
        {
            Write<short>((short)(value.Length));
            Write<string>(value);
        }

        public void WriteNString(string value)
        {
            Write<string>(value);
            Write<string>("");
        }
    
    
    }
}
