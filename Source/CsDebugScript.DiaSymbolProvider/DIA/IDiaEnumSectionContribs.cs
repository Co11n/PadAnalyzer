using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace DIA
{
    /// <summary>
    /// Enumerate the various section contributions contained in the data source.
    /// </summary>
    [ComImport, Guid("1994DEB2-2C82-4b1d-A57F-AFF424D54A68"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDiaEnumSectionContribs
    {
        /// <summary>
        /// Gets the enumerator. Internally, marshals the COM IEnumVARIANT interface to the .NET Framework <see cref="IEnumerator"/> interface, and vice versa.
        /// </summary>
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler")]
        IEnumerator GetEnumerator();

        /// <summary>
        /// Retrieves the number of source files.
        /// </summary>
        [DispId(1)]
        int count { get; }

        /// <summary>
        /// Retrieves a section contribution by means of an index.
        /// </summary>
        /// <param name="index">Index of the <see cref="IDiaSectionContrib"/> object to be retrieved. The index is the range 0 to count-1, where count is returned by the <see cref="IDiaEnumSectionContribs.count"/> property.</param>
        /// <returns>Returns an <see cref="IDiaSectionContrib"/> object representing the source file.</returns>
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaSectionContrib Item(
            [In] uint index);

        /// <summary>
        /// Retrieves a specified number of section contibutions in the enumeration sequence.
        /// </summary>
        /// <param name="celt">The number of section contibutions in the enumerator to be retrieved.</param>
        /// <param name="rgelt">Returns an array of <see cref="IDiaSectionContrib"/> objects that represents the desired section contribution.</param>
        /// <param name="pceltFetched">Returns the number of section contributions in the fetched enumerator.</param>
        void Next(
            [In] uint celt,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 0)] IDiaSectionContrib[] rgelt,
            [Out] out uint pceltFetched);

        /// <summary>
        /// Skips a specified number of section contributions in an enumeration sequence.
        /// </summary>
        /// <param name="celt">The number of section contributions in the enumeration sequence to skip.</param>
        void Skip(
            [In] uint celt);

        /// <summary>
        /// Resets an enumeration sequence to the beginning.
        /// </summary>
        void Reset();

        /// <summary>
        /// Creates an enumerator that contains the same enumeration state as the current enumerator.
        /// </summary>
        /// <returns>Returns an <see cref="IDiaEnumSectionContribs"/> object that contains a duplicate of the enumerator. The source files are not duplicated, only the enumerator.</returns>
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaEnumSectionContribs Clone();
    }
}
