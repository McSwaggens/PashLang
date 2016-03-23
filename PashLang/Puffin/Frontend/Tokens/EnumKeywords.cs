using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Tokens
{
    public enum EnumKeywords
    {
        NO_KEYWORD = -0x01,
        PUBLIC = 0x00,
        PRIVATE = 0x01,
        PROTECTED = 0x02,
        STATIC = 0x03,
        INT = 0x04,
        BOOLEAN = 0x05,
        LONG = 0x06,
        SHORT = 0x07,
        BYTE = 0x08,
        CHAR = 0x09,
        FLOAT = 0x0A,
        DOUBLE = 0x0B,
        DATASET = 0x0C,
        NULL = 0x0D,
        NULLPTR = 0x0E,
        VOID = 0x0F,
        IF = 0x10,
        FOR = 0x11,
        WHILE = 0x12,
        DO = 0x13,
        RETURN = 0x14,
        IMPORT = 0x15,
        SWITCH = 0x16,
        CASE = 0x17,
        __EOF = 0x18,
        __PASM = 0x19,
        ENUM = 0x20,
        CLASS = 0x21,
        STRUCT = 0x22,
        EXTERN = 0x23,
        INTERFACE = 0x24,
        ABSTRACT = 0x25,
        SEALED = 0x26,
        ELSE = 0x27,
        BREAK = 0x28,
        CONTINUE = 0x29,
        UINT = 0x30,
        UBYTE = 0x31,
        ULONG = 0x32,
        USHORT = 0x33,
        EXTENDS = 0x44,
        OBJECT = 0x45,
        STRING = 0x46,
        OUT = 0x47,
        REF = 0x48,
    }
}
