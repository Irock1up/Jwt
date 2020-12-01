﻿using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace JsonWebToken
{
    // inspired from https://github.com/dotnet/runtime/blob/master/src/libraries/System.Text.Json/src/System/Text/Json/Document/JsonDocument.DbRow.cs
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct JsonRow
    {
        internal const int Size = 12;

        // Sign bit is currently unassigned
        private readonly int _location;

        // Sign bit is used for "NeedUnescaping" (StartArray)
        private readonly int _lengthUnion;

        // Top nybble is JsonTokenType
        // remaining nybbles are the number of rows to skip to get to the next value
        // This isn't limiting on the number of rows, since Span.MaxLength / sizeof(DbRow) can't
        // exceed that range.
        private readonly int _numberOfRowsAndTypeUnion;

        internal int Location => _location;

        // length of text in JSON payload (or number of elements if its a JSON array)
        internal int Length => _lengthUnion & int.MaxValue;

        internal bool IsUnknownSize => _lengthUnion == UnknownSize;

        internal bool NeedUnescaping => _lengthUnion < 0;

        internal int NumberOfRows =>
            _numberOfRowsAndTypeUnion & 0x0FFFFFFF; // Number of rows that the current JSON element occupies within the database

        internal JsonTokenType TokenType => (JsonTokenType)(unchecked((uint)_numberOfRowsAndTypeUnion) >> 28);

        internal const int UnknownSize = -1;

        internal JsonRow(JsonTokenType jsonTokenType, int location, int sizeOrLength)
        {
            Debug.Assert(jsonTokenType > JsonTokenType.None && jsonTokenType <= JsonTokenType.Null);
            Debug.Assert((byte)jsonTokenType < 1 << 4);
            Debug.Assert(location >= 0);
            Debug.Assert(sizeOrLength >= UnknownSize);

            _location = location;
            _lengthUnion = sizeOrLength;
            _numberOfRowsAndTypeUnion = (int)jsonTokenType << 28;
        }

        internal bool IsSimpleValue => TokenType >= JsonTokenType.PropertyName;
    }
}