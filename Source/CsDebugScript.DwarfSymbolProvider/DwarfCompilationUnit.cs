﻿using System;
using System.Collections.Generic;

namespace CsDebugScript.DwarfSymbolProvider
{
    /// <summary>
    /// DWARF compilation unit instance.
    /// </summary>
    internal class DwarfCompilationUnit
    {
        /// <summary>
        /// The dictionary of symbols located by offset in the debug data stream.
        /// </summary>
        private Dictionary<int, DwarfSymbol> symbolsByOffset = new Dictionary<int, DwarfSymbol>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DwarfCompilationUnit"/> class.
        /// </summary>
        /// <param name="debugData">The debug data stream.</param>
        /// <param name="debugDataDescription">The debug data description stream.</param>
        /// <param name="debugStrings">The debug strings.</param>
        /// <param name="addressNormalizer">Normalize address delegate (<see cref="NormalizeAddressDelegate"/>)</param>
        public DwarfCompilationUnit(DwarfMemoryReader debugData, DwarfMemoryReader debugDataDescription, DwarfMemoryReader debugStrings, DwarfMemoryReader debugStringOffsets, NormalizeAddressDelegate addressNormalizer)
        {
            ReadData(debugData, debugDataDescription, debugStrings, debugStringOffsets, addressNormalizer);
        }

        /// <summary>
        /// Gets the symbols tree of all top level symbols defined in this compilation unit.
        /// </summary>
        public DwarfSymbol[] SymbolsTree { get; private set; }

        /// <summary>
        /// Gets all symbols defined in this compilation unit.
        /// </summary>
        public IEnumerable<DwarfSymbol> Symbols
        {
            get
            {
                return symbolsByOffset.Values;
            }
        }

        /// <summary>
        /// Reads the data for this instance.
        /// </summary>
        /// <param name="debugData">The debug data.</param>
        /// <param name="debugDataDescription">The debug data description.</param>
        /// <param name="debugStrings">The debug strings.</param>
        /// <param name="addressNormalizer">Normalize address delegate (<see cref="NormalizeAddressDelegate"/>)</param>
        private void ReadData(DwarfMemoryReader debugData, DwarfMemoryReader debugDataDescription, DwarfMemoryReader debugStrings, DwarfMemoryReader debugStringOffsets, NormalizeAddressDelegate addressNormalizer)
        {
            // Read header
            bool is64bit;
            int beginPosition = debugData.Position;
            ulong length = debugData.ReadLength(out is64bit);
            int endPosition = debugData.Position + (int)length;
            ushort version = debugData.ReadUshort();
            int debugDataDescriptionOffset;
            byte addressSize;
            byte UnitType = 1; //DW_UT_compile

            if (version >= 5)
            {
                UnitType = debugData.ReadByte();
                addressSize = debugData.ReadByte();
                debugDataDescriptionOffset = debugData.ReadOffset(is64bit);
            }
            else
            {
                debugDataDescriptionOffset = debugData.ReadOffset(is64bit);
                addressSize = debugData.ReadByte();
            }

            if (UnitType != 1) return;

            DataDescriptionReader dataDescriptionReader = new DataDescriptionReader(debugDataDescription, debugDataDescriptionOffset);

            // Read data
            List<DwarfSymbol> symbols = new List<DwarfSymbol>();
            Stack<DwarfSymbol> parents = new Stack<DwarfSymbol>();

            int OffsetByteSize = 4;
            ulong unitStringOffsetsBase = 0;
            ulong unitStringOffsetsSize = 0;

            while (debugData.Position < endPosition)
            {
                int dataPosition = debugData.Position;
                uint code = debugData.LEB128();

                if (code == 0)
                {
                    DwarfSymbol parent = parents.Peek();

                    if (parent.Children.Count == 0 && (parent.Tag == DwarfTag.CompileUnit || parent.Tag == DwarfTag.Namespace))
                    {
                        symbols.Remove(parent);
                        symbolsByOffset.Remove(parent.Offset);
                    }
                    parents.Pop();
                    continue;
                }

                DataDescription description = dataDescriptionReader.GetDebugDataDescription(code);
                
                Dictionary<DwarfAttribute, DwarfAttributeValue> attributes = new Dictionary<DwarfAttribute, DwarfAttributeValue>();

                bool skipSymbol = false;

                foreach (DataDescriptionAttribute descriptionAttribute in description.Attributes)
                {
                    DwarfAttribute attribute = descriptionAttribute.Attribute;
                    DwarfFormat format = descriptionAttribute.Format;
                    DwarfAttributeValue attributeValue = new DwarfAttributeValue();



                    switch (format)
                    {
                        case DwarfFormat.Address:
                            attributeValue.Type = DwarfAttributeValueType.Address;
                            attributeValue.Value = debugData.ReadUlong(addressSize);
                            break;
                        case DwarfFormat.Block:
                            attributeValue.Type = DwarfAttributeValueType.Block;
                            attributeValue.Value = debugData.ReadBlock(debugData.LEB128());
                            break;
                        case DwarfFormat.Block1:
                            attributeValue.Type = DwarfAttributeValueType.Block;
                            attributeValue.Value = debugData.ReadBlock(debugData.ReadByte());
                            break;
                        case DwarfFormat.Block2:
                            attributeValue.Type = DwarfAttributeValueType.Block;
                            attributeValue.Value = debugData.ReadBlock(debugData.ReadUshort());
                            break;
                        case DwarfFormat.Block4:
                            attributeValue.Type = DwarfAttributeValueType.Block;
                            attributeValue.Value = debugData.ReadBlock(debugData.ReadUint());
                            break;
                        case DwarfFormat.Data1:
                            attributeValue.Type = DwarfAttributeValueType.Constant;
                            attributeValue.Value = (ulong)debugData.ReadByte();
                            break;
                        case DwarfFormat.Data2:
                            attributeValue.Type = DwarfAttributeValueType.Constant;
                            attributeValue.Value = (ulong)debugData.ReadUshort();
                            break;
                        case DwarfFormat.Data4:
                            attributeValue.Type = DwarfAttributeValueType.Constant;
                            attributeValue.Value = (ulong)debugData.ReadUint();
                            break;
                        case DwarfFormat.Data8:
                            attributeValue.Type = DwarfAttributeValueType.Constant;
                            attributeValue.Value = (ulong)debugData.ReadUlong();
                            break;
                        case DwarfFormat.SData:
                            attributeValue.Type = DwarfAttributeValueType.Constant;
                            attributeValue.Value = (ulong)debugData.SLEB128();
                            break;
                        case DwarfFormat.UData:
                            attributeValue.Type = DwarfAttributeValueType.Constant;
                            attributeValue.Value = (ulong)debugData.LEB128();
                            break;
                        case DwarfFormat.String:
                            attributeValue.Type = DwarfAttributeValueType.String;
                            attributeValue.Value = debugData.ReadString();
                            break;
                        case DwarfFormat.Strp:
                            attributeValue.Type = DwarfAttributeValueType.String;
                            attributeValue.Value = debugStrings.ReadString(debugData.ReadOffset(is64bit));
                            break;
                        case DwarfFormat.Flag:
                            attributeValue.Type = DwarfAttributeValueType.Flag;
                            attributeValue.Value = debugData.ReadByte() != 0;
                            break;
                        case DwarfFormat.FlagPresent:
                            attributeValue.Type = DwarfAttributeValueType.Flag;
                            attributeValue.Value = true;
                            break;
                        case DwarfFormat.Ref1:
                            attributeValue.Type = DwarfAttributeValueType.Reference;
                            attributeValue.Value = (ulong)debugData.ReadByte() + (ulong)beginPosition;
                            break;
                        case DwarfFormat.Ref2:
                            attributeValue.Type = DwarfAttributeValueType.Reference;
                            attributeValue.Value = (ulong)debugData.ReadUshort() + (ulong)beginPosition;
                            break;
                        case DwarfFormat.Ref4:
                            attributeValue.Type = DwarfAttributeValueType.Reference;
                            attributeValue.Value = (ulong)debugData.ReadUint() + (ulong)beginPosition;
                            break;
                        case DwarfFormat.Ref8:
                            attributeValue.Type = DwarfAttributeValueType.Reference;
                            attributeValue.Value = (ulong)debugData.ReadUlong() + (ulong)beginPosition;
                            break;
                        case DwarfFormat.RefUData:
                            attributeValue.Type = DwarfAttributeValueType.Reference;
                            attributeValue.Value = (ulong)debugData.LEB128() + (ulong)beginPosition;
                            break;
                        case DwarfFormat.RefAddr:
                            attributeValue.Type = DwarfAttributeValueType.Reference;
                            attributeValue.Value = (ulong)debugData.ReadOffset(is64bit);
                            break;
                        case DwarfFormat.RefSig8:
                            attributeValue.Type = DwarfAttributeValueType.Invalid;
                            debugData.Position += 8;
                            break;
                        case DwarfFormat.ExpressionLocation:
                            attributeValue.Type = DwarfAttributeValueType.ExpressionLocation;
                            attributeValue.Value = debugData.ReadBlock(debugData.LEB128());
                            break;
                        case DwarfFormat.SecOffset:
                            attributeValue.Type = DwarfAttributeValueType.SecOffset;
                            attributeValue.Value = (ulong)debugData.ReadOffset(is64bit);
                            break;
                        case DwarfFormat.Strx1:
                            attributeValue.Type = DwarfAttributeValueType.Invalid;
                            attributeValue.Value = (ulong)debugData.ReadByte();
                            break;
                        case DwarfFormat.Strx2:
                            attributeValue.Type = DwarfAttributeValueType.Invalid;
                            attributeValue.Value = (ulong)debugData.ReadUshort();
                            break;
                        case DwarfFormat.Strx3:
                            attributeValue.Type = DwarfAttributeValueType.Invalid;
                            attributeValue.Value = (ulong)debugData.ReadByte() + 8 * (ulong)debugData.ReadUshort();
                            break;
                        case DwarfFormat.Strx4:
                            attributeValue.Type = DwarfAttributeValueType.Invalid;
                            attributeValue.Value = (ulong)debugData.ReadUint();
                            break;
                        case DwarfFormat.Rnglistx:
                        case DwarfFormat.Loclistx:
                        case DwarfFormat.Addrx:
                            attributeValue.Type = DwarfAttributeValueType.Invalid;
                            attributeValue.Value = (ulong)debugData.SLEB128();
                            break;
                        default:
                            throw new Exception($"Unsupported DwarfFormat: {format}");
                    }

                    if (unitStringOffsetsSize > 0 && (format == DwarfFormat.Strx1 || format == DwarfFormat.Strx2 || format == DwarfFormat.Strx4))
                    {
                        debugStringOffsets.Position = (int)unitStringOffsetsBase + (int)attributeValue.SecOffset * OffsetByteSize;
                        int offset = debugStringOffsets.ReadOffset(is64bit);
                        attributeValue.Type = DwarfAttributeValueType.String;
                        attributeValue.Value = debugStrings.ReadString(offset);
                    }

                    if (attribute == DwarfAttribute.StrOffsetsBase)
                    {
                        debugStringOffsets.Position = (int)attributeValue.SecOffset - (is64bit ? 16 : 8);

                        bool is64bitOfs; // should be the same as is64bit
                        ulong ContributionSize = debugStringOffsets.ReadLength(out is64bitOfs);
                        ushort Version = debugStringOffsets.ReadUshort();
                        debugStringOffsets.ReadUshort(); // padding

                        OffsetByteSize = is64bitOfs ? 8 : 4;

                        unitStringOffsetsBase = attributeValue.SecOffset;
                        unitStringOffsetsSize = ContributionSize;
                    }


                    bool skipAttribute = true;
                    if (attribute == DwarfAttribute.Name || 
                        attribute == DwarfAttribute.ByteSize ||
                        attribute == DwarfAttribute.Type ||
                        attribute == DwarfAttribute.DataMemberLocation ||
                        // for inheritance
                        attribute == DwarfAttribute.Virtuality ||
                        // for array
                        attribute == DwarfAttribute.Count ||
                        attribute == DwarfAttribute.UpperBound)
                    {
                        skipAttribute = false;
                    }

                    if (attribute == DwarfAttribute.Declaration)
                    {
                        skipSymbol = true;
                    }

                    if (skipAttribute)
                    {
                        continue;
                    }
                    if (attribute == DwarfAttribute.Count && attributeValue.Type != DwarfAttributeValueType.Constant)
                    {
                        attributeValue.Type = DwarfAttributeValueType.Constant;
                    }

                    if (attributes.ContainsKey(attribute))
                    {
                        if (attributes[attribute] != attributeValue)
                        {
                            attributes[attribute] = attributeValue;
                        }
                    }
                    else
                    {
                        attributes.Add(attribute, attributeValue);
                    }
                }


                if (description.Tag != DwarfTag.BaseType &&
                    description.Tag != DwarfTag.Typedef &&
                    description.Tag != DwarfTag.PointerType &&
                    description.Tag != DwarfTag.ClassType &&
                    description.Tag != DwarfTag.StructureType &&
                    description.Tag != DwarfTag.UnionType &&
                    description.Tag != DwarfTag.EnumerationType &&
                    description.Tag != DwarfTag.ArrayType &&
                    description.Tag != DwarfTag.SubrangeType &&
                    description.Tag != DwarfTag.Inheritance &&
                    description.Tag != DwarfTag.Member &&
                    description.Tag != DwarfTag.CompileUnit &&
                    description.Tag != DwarfTag.Namespace)
                {
                    skipSymbol = true;
                }

                if (description.Tag == DwarfTag.PointerType)
                {
                    if (!attributes.ContainsKey(DwarfAttribute.ByteSize))
                    {
                        DwarfAttributeValue attributeValue = new DwarfAttributeValue();
                        attributeValue.Type = DwarfAttributeValueType.Constant;
                        attributeValue.Value = (ulong)8;

                        attributes.Add(DwarfAttribute.ByteSize, attributeValue);
                    }
                }

                DwarfSymbol symbol = new DwarfSymbol()
                {
                    Tag = description.Tag,
                    Attributes = attributes,
                    Offset = dataPosition,
                };

                if (!skipSymbol) symbolsByOffset.Add(symbol.Offset, symbol);

                if (parents.Count > 0)
                {
                    if (!skipSymbol)
                    {
                        parents.Peek().Children.Add(symbol);
                    }

                    symbol.Parent = parents.Peek();
                }
                else
                {
                    if (!skipSymbol)
                    {
                        symbols.Add(symbol);
                    }
                }

                if (description.HasChildren)
                {
                    symbol.Children = new List<DwarfSymbol>();
                    parents.Push(symbol);
                }
            }

            SymbolsTree = symbols.ToArray();

            if (SymbolsTree.Length > 0)
            {
                // Add void type symbol
                DwarfSymbol voidSymbol = new DwarfSymbol()
                {
                    Tag = DwarfTag.BaseType,
                    Offset = -1,
                    Parent = SymbolsTree[0],
                    Attributes = new Dictionary<DwarfAttribute, DwarfAttributeValue>()
                    {
                        { DwarfAttribute.Name, new DwarfAttributeValue() { Type = DwarfAttributeValueType.String, Value = "void" } },
                        { DwarfAttribute.ByteSize, new DwarfAttributeValue() { Type = DwarfAttributeValueType.Constant, Value = (ulong)0 } },
                    },
                };
                if (SymbolsTree[0].Children == null)
                {
                    SymbolsTree[0].Children = new List<DwarfSymbol>();
                }
                SymbolsTree[0].Children.Insert(0, voidSymbol);
                symbolsByOffset.Add(voidSymbol.Offset, voidSymbol);

                // Post process all symbols
                foreach (DwarfSymbol symbol in Symbols)
                {
                    Dictionary<DwarfAttribute, DwarfAttributeValue> attributes = symbol.Attributes as Dictionary<DwarfAttribute, DwarfAttributeValue>;

                    foreach (DwarfAttributeValue value in attributes.Values)
                    {
                        if (value.Type == DwarfAttributeValueType.Reference)
                        {
                            DwarfSymbol reference;

                            value.Type = DwarfAttributeValueType.ResolvedReference;

                            if (symbolsByOffset.TryGetValue((int)value.Address, out reference))
                            {
                                value.Value = reference;
                            }
                            else
                            {
                                value.Value = voidSymbol;
                            }
                        }
                        else if (value.Type == DwarfAttributeValueType.Address)
                        {
                            value.Value = addressNormalizer(value.Address);
                        }
                    }

                    if ((symbol.Tag == DwarfTag.PointerType && !attributes.ContainsKey(DwarfAttribute.Type))
                        || (symbol.Tag == DwarfTag.Typedef && !attributes.ContainsKey(DwarfAttribute.Type)))
                    {
                        attributes.Add(DwarfAttribute.Type, new DwarfAttributeValue()
                        {
                            Type = DwarfAttributeValueType.ResolvedReference,
                            Value = voidSymbol,
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Symbol data description
        /// </summary>
        private struct DataDescription
        {
            /// <summary>
            /// Gets or sets the symbol tag.
            /// </summary>
            public DwarfTag Tag { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether symbol has children.
            /// </summary>
            /// <value>
            ///   <c>true</c> if symbol has children; otherwise, <c>false</c>.
            /// </value>
            public bool HasChildren { get; set; }

            /// <summary>
            /// Gets or sets the symbol data description attributes list.
            /// </summary>
            public List<DataDescriptionAttribute> Attributes { get; set; }
        }

        /// <summary>
        /// Symbol data description attribute.
        /// </summary>
        private struct DataDescriptionAttribute
        {
            /// <summary>
            /// Gets or sets the attribute.
            /// </summary>
            public DwarfAttribute Attribute { get; set; }

            /// <summary>
            /// Gets or sets the format.
            /// </summary>
            public DwarfFormat Format { get; set; }
        }

        /// <summary>
        /// Data description reader helper
        /// </summary>
        private class DataDescriptionReader
        {
            /// <summary>
            /// The debug data description stream
            /// </summary>
            DwarfMemoryReader debugDataDescription;

            /// <summary>
            /// The dictionary of already read symbol data descriptions located by code.
            /// </summary>
            Dictionary<uint, DataDescription> readDescriptions;

            /// <summary>
            /// The last read position.
            /// </summary>
            int lastReadPosition;

            /// <summary>
            /// Initializes a new instance of the <see cref="DataDescriptionReader"/> class.
            /// </summary>
            /// <param name="debugDataDescription">The debug data description.</param>
            /// <param name="startingPosition">The starting position.</param>
            public DataDescriptionReader(DwarfMemoryReader debugDataDescription, int startingPosition)
            {
                readDescriptions = new Dictionary<uint, DataDescription>();
                lastReadPosition = startingPosition;
                this.debugDataDescription = debugDataDescription;
            }

            /// <summary>
            /// Gets the debug data description for the specified code.
            /// </summary>
            /// <param name="findCode">The code to be found.</param>
            public DataDescription GetDebugDataDescription(uint findCode)
            {
                DataDescription result;

                if (readDescriptions.TryGetValue(findCode, out result))
                {
                    return result;
                }

                debugDataDescription.Position = lastReadPosition;
                while (!debugDataDescription.IsEnd)
                {
                    uint code = debugDataDescription.LEB128();
                    DwarfTag tag = (DwarfTag)debugDataDescription.LEB128();
                    bool hasChildren = debugDataDescription.ReadByte() != 0;
                    List<DataDescriptionAttribute> attributes = new List<DataDescriptionAttribute>();

                    while (!debugDataDescription.IsEnd)
                    {
                        DwarfAttribute attribute = (DwarfAttribute)debugDataDescription.LEB128();
                        DwarfFormat format = (DwarfFormat)debugDataDescription.LEB128();

                        while (format == DwarfFormat.Indirect)
                        {
                            format = (DwarfFormat)debugDataDescription.LEB128();
                        }

                        if (attribute == DwarfAttribute.None && format == DwarfFormat.None)
                        {
                            break;
                        }

                        attributes.Add(new DataDescriptionAttribute()
                        {
                            Attribute = attribute,
                            Format = format,
                        });
                    }

                    result = new DataDescription()
                    {
                        Tag = tag,
                        HasChildren = hasChildren,
                        Attributes = attributes,
                    };
                    readDescriptions.Add(code, result);
                    if (code == findCode)
                    {
                        lastReadPosition = debugDataDescription.Position;
                        return result;
                    }
                }

                throw new NotImplementedException();
            }
        }
    }
}
