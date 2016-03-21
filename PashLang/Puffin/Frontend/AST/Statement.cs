﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols;
using Puffin.Frontend.Symbols.Modifiers;
using Puffin.Frontend.Symbols.TypeInfo;
using Puffin.Frontend.Tokens;

namespace Puffin.Frontend.AST
{
    public class Statement
    {
        protected List<Token> statementTokens;
        protected List<Modifier> modifiers; 
        protected bool valueStatement;
        protected bool isTerminated;
        protected Information typeInformation;

        public Statement(List<Token> statemenrTokens, bool valueStatement, bool isTerminated)
        {
            this.statementTokens = statemenrTokens;
            this.valueStatement = valueStatement;
            this.isTerminated = isTerminated;
            this.modifiers = new List<Modifier>();
        }

        public void DefineSymbols()
        {
            foreach (EnumKeywords ty in statementTokens.OfType<KeywordToken>().Select(tok => (EnumKeywords)tok.Type))
            {
                if ((int)ty <= 0x03 || (int)ty == 0x25 || (int)ty == 0x26)
                {
                    if (modifiers.Count == 1)
                    {
                        if (!modifiers.Any(x => x.Value == EnumModifiers.STATIC) && ((int) ty) == 0x03)
                        {
                            modifiers.Add(new Modifier(EnumModifiers.STATIC));
                            continue;
                        }
                        Logger.WriteError("Too Many Modifiers");
                        return;
                      }
                    modifiers.Add(new Modifier((EnumModifiers) Enum.Parse(typeof(EnumModifiers),ty.ToString().ToUpper())));
                }
                else if (((int)ty >= 0x04 && (int)ty <= 0x0F) || ((int)ty >= 0x30 && (int)ty <= 0x33) || (int) ty == 0x45)
                {
                    if (typeInformation != null)
                    {
                        Logger.WriteError("Too many types ");
                        return;
                    }
                    switch (ty)
                    {
                        case EnumKeywords.INT:
                            typeInformation = new StructInformation(nameof(Int32),0,true,false);
                            break;
                        case EnumKeywords.BOOLEAN:
                            typeInformation = new StructInformation(nameof(Boolean),false,true,false);
                            break;
                        case EnumKeywords.LONG:
                            typeInformation = new StructInformation(nameof(Int64),0L,true,false);
                            break;
                        case EnumKeywords.SHORT:
                            typeInformation = new StructInformation(nameof(Int16),(short) 0,true,false);
                            break;
                        case EnumKeywords.BYTE:
                            typeInformation = new StructInformation(nameof(Byte),(byte) 0, true,false);
                            break;
                        case EnumKeywords.CHAR:
                            typeInformation = new StructInformation(nameof(Char),'\0',true,false);
                            break;
                        case EnumKeywords.FLOAT:
                            typeInformation = new StructInformation(nameof(Single), 0.0f,true,false);
                            break;
                        case EnumKeywords.DOUBLE:
                            typeInformation = new StructInformation(nameof(Double), 0.0, true,false);
                            break;
                        case EnumKeywords.DATASET:
                            Logger.WriteWarning("Datasets are not dealt with yet");
                            break;
                        case EnumKeywords.UINT:
                            typeInformation = new StructInformation(nameof(UInt32),(uint) 0, true,false);
                            break;
                        case EnumKeywords.UBYTE:
                            typeInformation = new StructInformation(nameof(Byte),(byte)0, true, false);
                            break;
                        case EnumKeywords.USHORT:
                            typeInformation = new StructInformation(nameof(UInt16), (ushort) 0,true,false);
                            break;
                        case EnumKeywords.ULONG:
                            typeInformation = new StructInformation(nameof(UInt64), 0UL, true, false);
                            break;
                        case EnumKeywords.OBJECT:
                            typeInformation = new ClassInformation(nameof(Object), null,true,true);
                            break;
                        case EnumKeywords.VOID:
                            typeInformation = new StructInformation(typeof(void).ToString(),null,true,true);
                            break;
                        default:
                            Logger.WriteWarning("User Defined types are not dealt with yet");
                            break;
                    }

                }
            }

            //LinkedListNode<Token> node = statementTokens.First;
            //while (node != null)
            //{

            //    node = node.Next;
            //}
        }
    }
}