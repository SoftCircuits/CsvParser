namespace RandomDataTests
{
    [Flags]
    public enum CharType
    {
        Upper = 0x00001,
        Lower = 0x00002,
        Digits = 0x00004,
        Punctuation = 0x00008,
        Alpha = Upper | Lower,
        Alphanumeric = Upper | Lower | Digits,
        All = Upper | Lower | Digits | Punctuation,
    }
}
