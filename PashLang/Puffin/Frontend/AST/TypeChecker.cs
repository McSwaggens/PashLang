using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols;
using Puffin.Frontend.Symbols.TypeInfo;
using Puffin.Frontend.Tokens;

namespace Puffin.Frontend.AST
{
    public class TypeChecker
    {
        private List<Statement> statements;
        private SymbolTable<Symbol<Information>> symbols;
        private int strictness;
        private Information identTy = null;
        private EnumOperators operatorTy = EnumOperators.NO_OPERATOR;
        private Information resultTy = null;

        public TypeChecker(List<Statement> statements, SymbolTable<Symbol<Information>> symbols, int strictness = 4)
        {
            if (strictness > 4 || strictness < 1)
            {
                Logger.WriteError("Type checker strictness level must be between 1 and 4");
                return;
            }
            this.statements = statements;
            this.symbols = symbols;
            this.strictness = strictness;
        }

        public bool Start()
        {
            if (!CheckTypes())
                return false;
            return true;
        }

        private bool CheckTypes()
        {
            foreach (Statement smt in statements)
            {
                if (!ParseStatementTypes(smt))
                    return false;
            }
            return true;
        }

        private bool EnforceTypes(Statement smt)
        {
            if(identTy == null || resultTy == null)
                return true;
            return identTy.Name.Equals(resultTy.Name) && smt != null;
        }

        private bool ParseStatementTypes(Statement smt)
        {
            EnumKeywords ty;
            if (smt.StatementTokens.Any(x => x is OperatorToken) &&
                smt.StatementTokens.Any(x => x is IdentifierToken)) // type Checking is required
            {
                foreach (Token tok in smt.StatementTokens)
                {
                    if (tok is IdentifierToken && identTy == null)
                        identTy = symbols.Symbols.First(x => x.IdentifierName.Equals(tok.Value)).IdentifierValue;
                    else if (tok is OperatorToken)
                        operatorTy = (EnumOperators)tok.Type;
                    else if (tok is IdentifierToken && identTy != null)
                    {
                        resultTy = symbols.Symbols.First(x => x.IdentifierName.Equals(tok.Value)).IdentifierValue;
                    }
                    else if (tok is StringLiteralToken || tok is IntegerLiteralToken ||
                             tok is UnsignedIntegerLiteralToken || tok is ShortLiteralToken ||
                             tok is UnsignedShortLiteralToken || tok is LongLiteralToken ||
                             tok is UnsignedLongLiteralToken || tok is ByteLiteralToken || tok is FloatLiteralToken ||
                             tok is DoubleLiteralToken || tok is CharacterLiteralToken)
                    {
                        switch ((EnumLiterals)tok.ResolveType())
                        {
                            case EnumLiterals.CHAR:
                                resultTy = new StructInformation(nameof(Char), '\0', true, false);
                                break;
                            case EnumLiterals.BOOLEAN:
                                resultTy = new StructInformation(nameof(Boolean), false, true, false);
                                break;
                            case EnumLiterals.BYTE:
                                resultTy = new StructInformation(nameof(Byte), 0x00, true, false);
                                break;
                            case EnumLiterals.DOUBLE:
                                resultTy = new StructInformation(nameof(Double), 0.0, true, false);
                                break;
                            case EnumLiterals.FLOAT:
                                resultTy = new StructInformation(nameof(Single), 0.0f, true, false);
                                break;
                            case EnumLiterals.INT:
                                resultTy = new StructInformation(nameof(Int32), 0, true, false);
                                break;
                            case EnumLiterals.LONG:
                                resultTy = new StructInformation(nameof(Int64), 0L, true, false);
                                break;
                            case EnumLiterals.SHORT:
                                resultTy = new StructInformation(nameof(Int16), (short)0, true, false);
                                break;
                            case EnumLiterals.STRING:
                                resultTy = new ClassInformation(nameof(String), "", true, true);
                                break;
                            case EnumLiterals.UINT:
                                resultTy = new StructInformation(nameof(UInt32), (uint)0, true, false);
                                break;
                            case EnumLiterals.UBYTE:
                                resultTy = new StructInformation(nameof(Byte), (byte)0, true, false);
                                break;
                            case EnumLiterals.USHORT:
                                resultTy = new StructInformation(nameof(UInt16), (ushort)0, true, false);
                                break;
                            case EnumLiterals.ULONG:
                                resultTy = new StructInformation(nameof(UInt64), 0UL, true, false);
                                break;
                            default:
                                Logger.WriteWarning("This literal type is not implemented yet");
                                return false;
                        }
                    }
                    else if (!Enum.TryParse(tok.Value.Replace("*", "").ToUpper(), out ty))
                    {
                        continue;
                    }
                    else if (((int)ty >= 0x04 && (int)ty <= 0x0F) || ((int)ty >= 0x30 && (int)ty <= 0x33) || (int)ty == 0x45 ||
                        (int)ty == 0x46)
                        switch (ty)
                        {
                            case EnumKeywords.CHAR:
                                identTy = new StructInformation(nameof(Char), '\0', true, false);
                                break;
                            case EnumKeywords.BOOLEAN:
                                identTy = new StructInformation(nameof(Boolean), false, true, false);
                                break;
                            case EnumKeywords.BYTE:
                                identTy = new StructInformation(nameof(Byte), 0x00, true, false);
                                break;
                            case EnumKeywords.DOUBLE:
                                identTy = new StructInformation(nameof(Double), 0.0, true, false);
                                break;
                            case EnumKeywords.FLOAT:
                                identTy = new StructInformation(nameof(Single), 0.0f, true, false);
                                break;
                            case EnumKeywords.INT:
                                identTy = new StructInformation(nameof(Int32), 0, true, false);
                                break;
                            case EnumKeywords.LONG:
                                identTy = new StructInformation(nameof(Int64), 0L, true, false);
                                break;
                            case EnumKeywords.SHORT:
                                identTy = new StructInformation(nameof(Int16), (short)0, true, false);
                                break;
                            case EnumKeywords.STRING:
                                identTy = new ClassInformation(nameof(String), "", true, true);
                                break;
                            case EnumKeywords.UINT:
                                identTy = new StructInformation(nameof(UInt32), (uint)0, true, false);
                                break;
                            case EnumKeywords.UBYTE:
                                identTy = new StructInformation(nameof(Byte), (byte)0, true, false);
                                break;
                            case EnumKeywords.USHORT:
                                identTy = new StructInformation(nameof(UInt16), (ushort)0, true, false);
                                break;
                            case EnumKeywords.ULONG:
                                identTy = new StructInformation(nameof(UInt64), 0UL, true, false);
                                break;
                        }
                }
                if (!EnforceTypes(smt))
                    return false;
            }
            return true;
        }
    }
}
