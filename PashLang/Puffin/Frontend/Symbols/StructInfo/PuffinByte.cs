using System;
using System.Collections.Generic;
using Puffin.Frontend.Symbols.ClassInfo;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols.StructInfo
{
    public struct PuffinByte : Struct.IStruct, INumber
    {
        private object value;
        public StructInformation information
        {
            get { return CreateInformation(); }
        }

        public object Value
        {
            get { return value; }

            set { this.value = value; }
        }

        public bool Convertable(object o)
        {
            return true;
        }

        public StructInformation CreateInformation()
        {
            StructInformation info = new StructInformation(nameof(Byte),0x00,true,false);
            info.DefinedMethods.Add(new MethodInformation(nameof(ToByte), info, new Information[] { new ParameterInformation(name), } ));

        }

        public PuffinByte ToByte(INumber inNumber)
        {
            PuffinByte b = new PuffinByte();
            b.Value = (byte) value;
            return b;
        }

        public PuffinInt16 ToInt16(INumber inNumber)
        {
            PuffinInt16 i16 = new PuffinInt16();
            i16.Value = (short) value;
            return i16;
        }

        public PuffinInt32 ToInt32(INumber inNumber)
        {
            PuffinInt32 i32 = new PuffinInt32();
            i32.Value = (int)value;
            return i32;
        }

        public PuffinInt64 ToInt64(INumber inNumber)
        {
            PuffinInt64 i64 = new PuffinInt64();
            i64.Value = (long)value;
            return i64;
        }

        public PuffinString ToString(INumber inNumber)
        {
            throw new NotImplementedException();
        }

        public T ToType<T>(INumber inNumber, Information typeInformation)
        {
            throw new NotImplementedException();
        }

        public PuffinUInt16 ToUint16(INumber inNumber)
        {
            PuffinUInt16 ui16 = new PuffinUInt16();
            ui16.Value = (ushort)value;
            return ui16;
        }

        public PuffinUInt32 ToUint32(INumber inNumber)
        {
            PuffinUInt32 ui32 = new PuffinUInt32();
            ui32.Value = (uint)value;
            return ui32;
        }

        public PuffinUInt64 ToUint64(INumber inNumber)
        {
            PuffinUInt64 ui64 = new PuffinUInt64();
            ui64.Value = (ulong)value;
            return ui64;
        }
    }
}