using System;
using System.Runtime.InteropServices;

namespace DIA
{
    /// <summary>
    /// Represents a source file.
    /// </summary>
    [ComImport, Guid("0CF4B60E-35B1-4c6c-BDD8-854B9C8E3857"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDiaSectionContrib
    {
        /// <summary>
        /// Retrieves a reference to the compiland symbol that contributed this section.
        /// </summary>
        [DispId(2)]
        IDiaSymbol compiland
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            get;
        }

        /// <summary>
        /// Retrieves the section part of the contribution's address.
        /// </summary>
        [DispId(3)]
        uint addressSection { get; }

        /// <summary>
        /// Retrieves the offset part of the contribution's address.
        /// </summary>
        [DispId(4)]
        uint addressOffset { get; }

        /// <summary>
        /// Retrieves the image relative virtual address(RVA) of the contribution.
        /// </summary>
        [DispId(5)]
        uint relativeVirtualAddress { get; }


        /// <summary>
        /// Retrieves the virtual address(VA) of the contribution.
        /// </summary>
        [DispId(6)]
        ulong virtualAddress { get; }

        /// <summary>
        /// Retrieves the number of bytes in a section.
        /// </summary>
        [DispId(7)]
        ulong length { get; }

        /// <summary>
        /// Retrieves a flag that indicates whether the section cannot be paged out of memory.
        /// </summary>
        [DispId(8)]
        bool notPaged
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag indicating whether the section should not be padded to the next memory boundary.
        /// </summary>
        [DispId(9)]
        bool nopad
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section contains executable code.
        /// </summary>
        [DispId(10)]
        bool code
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section contains 16-bit code.
        /// </summary>
        [DispId(11)]
        bool code16bit
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section contains initialized data.
        /// </summary>
        [DispId(12)]
        bool initializedData
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section contains uninitialized data.
        /// </summary>
        [DispId(12)]
        bool uninitializedData
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag indicating whether a section contains comments or similar information.
        /// </summary>
        [DispId(12)]
        bool informational
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section is removed before it is made part of the in-memory image.
        /// </summary>
        [DispId(13)]
        bool remove
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section is a COMDAT record.
        /// </summary>
        [DispId(14)]
        bool comdat
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section can be discarded.
        /// </summary>
        [DispId(15)]
        bool discardable
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section cannot be cached.
        /// </summary>
        [DispId(16)]
        bool notCached
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section can be shared in memory.
        /// </summary>
        [DispId(17)]
        bool share
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section can be shared in memory.
        /// </summary>
        [DispId(18)]
        bool execute
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section can be read.
        /// </summary>
        [DispId(19)]
        bool read
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves a flag that indicates whether the section can be written.
        /// </summary>
        [DispId(20)]
        bool write
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
        }

        /// <summary>
        /// Retrieves the cyclic redundancy check (CRC) of the data in the section.
        /// </summary>
        [DispId(21)]
        uint dataCrc { get; }

        /// <summary>
        /// Retrieves the CRC of the relocation information for the section.
        /// </summary>
        [DispId(22)]
        uint relocationsCrc { get; }

        /// <summary>
        /// Retrieves the compiland identifier for the section.
        /// </summary>
        [DispId(23)]
        uint compilandId { get; }
    }
}
