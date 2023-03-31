// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Diagnostics;
using System.Reflection;

namespace CsvParserTests
{
    public class DataConvertersTestClass
    {
        public string? StringMember { get; set; }
        public string[]? StringArrayMember { get; set; }
        public char CharMember { get; set; }
        public char[]? CharArrayMember { get; set; }
        public Nullable<char> NullableCharMember { get; set; }
        public Nullable<char>[]? NullableCharArrayMember { get; set; }
        public bool BooleanMember { get; set; }
        public bool[]? BooleanArrayMember { get; set; }
        public Nullable<bool> NullableBooleanMember { get; set; }
        public Nullable<bool>[]? NullableBooleanArrayMember { get; set; }
        public byte ByteMember { get; set; }
        public byte[]? ByteArrayMember { get; set; }
        public Nullable<byte> NullableByteMember { get; set; }
        public Nullable<byte>[]? NullableByteArrayMember { get; set; }
        public sbyte SByteMember { get; set; }
        public sbyte[]? SByteArrayMember { get; set; }
        public Nullable<sbyte> NullableSByteMember { get; set; }
        public Nullable<sbyte>[]? NullableSByteArrayMember { get; set; }
        public short Int16Member { get; set; }
        public short[]? Int16ArrayMember { get; set; }
        public Nullable<short> NullableInt16Member { get; set; }
        public Nullable<short>[]? NullableInt16ArrayMember { get; set; }
        public ushort UInt16Member { get; set; }
        public ushort[]? UInt16ArrayMember { get; set; }
        public Nullable<ushort> NullableUInt16Member { get; set; }
        public Nullable<ushort>[]? NullableUInt16ArrayMember { get; set; }
        public int Int32Member { get; set; }
        public int[]? Int32ArrayMember { get; set; }
        public Nullable<int> NullableInt32Member { get; set; }
        public Nullable<int>[]? NullableInt32ArrayMember { get; set; }
        public uint UInt32Member { get; set; }
        public uint[]? UInt32ArrayMember { get; set; }
        public Nullable<uint> NullableUInt32Member { get; set; }
        public Nullable<uint>[]? NullableUInt32ArrayMember { get; set; }
        public long Int64Member { get; set; }
        public long[]? Int64ArrayMember { get; set; }
        public Nullable<long> NullableInt64Member { get; set; }
        public Nullable<long>[]? NullableInt64ArrayMember { get; set; }
        public ulong UInt64Member { get; set; }
        public ulong[]? UInt64ArrayMember { get; set; }
        public Nullable<ulong> NullableUInt64Member { get; set; }
        public Nullable<ulong>[]? NullableUInt64ArrayMember { get; set; }
        public float SingleMember { get; set; }
        public float[]? SingleArrayMember { get; set; }
        public Nullable<float> NullableSingleMember { get; set; }
        public Nullable<float>[]? NullableSingleArrayMember { get; set; }
        public double DoubleMember { get; set; }
        public double[]? DoubleArrayMember { get; set; }
        public Nullable<double> NullableDoubleMember { get; set; }
        public Nullable<double>[]? NullableDoubleArrayMember { get; set; }
        public decimal DecimalMember { get; set; }
        public decimal[]? DecimalArrayMember { get; set; }
        public Nullable<decimal> NullableDecimalMember { get; set; }
        public Nullable<decimal>[]? NullableDecimalArrayMember { get; set; }
        public Guid GuidMember { get; set; }
        public Guid[]? GuidArrayMember { get; set; }
        public Nullable<Guid> NullableGuidMember { get; set; }
        public Nullable<Guid>[]? NullableGuidArrayMember { get; set; }
        public DateTime DateTimeMember { get; set; }
        public DateTime[]? DateTimeArrayMember { get; set; }
        public Nullable<DateTime> NullableDateTimeMember { get; set; }
        public Nullable<DateTime>[]? NullableDateTimeArrayMember { get; set; }

        #region IEquatable

        public override int GetHashCode()
        {
            // No good option here
            return 1;
        }

        public override bool Equals(object? obj) => obj is DataConvertersTestClass testClass && Equals(testClass);

        /// <summary>
        /// Uses reflection to compares this instance to <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The instance to compare to.</param>
        /// <returns>True if both instances are equal.</returns>
        public bool Equals(DataConvertersTestClass other)
        {
            if (other == null)
                return false;

            // Compare property values
            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < properties.Length; i++)
            {
                if (!ObjectsAreEqual(properties[i].GetValue(this), properties[i].GetValue(other)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Compares two <see cref="object"/>s.
        /// </summary>
        /// <param name="a">First object.</param>
        /// <param name="b">Second object.</param>
        /// <returns>True if both items are equal.</returns>
        private bool ObjectsAreEqual(object? a, object? b)
        {
            // Handle null cases
            if (a == null || b == null)
                return (a == null && b == null);

            // Get object type
            Type type = a.GetType();
            Debug.Assert(type == b.GetType());

            // Value types
            if (type.IsValueType)
                return (a is DateTime) ? DateTimesAreEqual((DateTime)a, (DateTime)b) : a.Equals(b);

            // Hande arrays
            if (type.IsArray)
            {
                Array array1 = (Array)a;
                Array array2 = (Array)b;

                if (array1.Length != array2.Length)
                    return false;
                for (int i = 0; i < array1.Length; i++)
                {
                    if (!ObjectsAreEqual(array1.GetValue(i), array2.GetValue(i)))
                        return false;
                }
                return true;
            }

            // Non-null reference types
            return a.Equals(b);
        }

        /// <summary>
        /// Compares the main elements of two <see cref="DateTime"/> values.
        /// Used because the default DateTimeConverter does not store
        /// milliseconds, time zone, etc.
        /// </summary>
        /// <param name="a">First DateTime value.</param>
        /// <param name="b">Second DateTime value.</param>
        /// <returns>True if both items are equal.</returns>
        private bool DateTimesAreEqual(DateTime a, DateTime b)
        {
            if (a.Year != b.Year)
                return false;
            if (a.Month != b.Month)
                return false;
            if (a.Day != b.Day)
                return false;
            if (a.Hour != b.Hour)
                return false;
            if (a.Minute != b.Minute)
                return false;
            if (a.Second != b.Second)
                return false;
            return true;
        }

        #endregion

        public static DataConvertersTestClass[] TestData = new DataConvertersTestClass[]
        {
            new DataConvertersTestClass {
                StringMember = "FVLAOgmT@S",
                StringArrayMember = new string[] { "mtMsFXs9EQ", "4vkJn4t7'm", "ZfsPuB1$k@", "Q4DbhTXDz1", "Uo.7'%d2!F", "??87QVkt2p", "k.kzqMYgcv", "q'd8XisfD4", "IC8YN\"tTYb", "quKj9oNCYH" },
                CharMember = 'a',
                CharArrayMember = new char[] { 'l', 'I', 'e', '5', 'B', 'G', '3', '.', 'q', 'E' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { '1', 'T', 's', 'Z', ':', 'q', null, 'n', null, '%' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, true, true, true, false, false, false, false, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, true, true, false, false, null, null, true, true },
                ByteMember = 127,
                ByteArrayMember = new byte[] { 125, 159, 30, 33, 106, 224, 122, 35, 186, 229 },
                NullableByteMember = 117,
                NullableByteArrayMember = new Nullable<byte>[] { 79, null, null, 104, null, 189, 144, 34, null, 241 },
                SByteMember = 102,
                SByteArrayMember = new sbyte[] { 104, 125, 45, 102, -61, 98, -38, 115, 25, -113 },
                NullableSByteMember = 55,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, 101, 90, -46, null, null, null, -10, 2, 12 },
                Int16Member = -30517,
                Int16ArrayMember = new short[] { 16932, -13429, 27170, -15111, -23392, 23370, 20951, -16882, -15843, -24061 },
                NullableInt16Member = -25263,
                NullableInt16ArrayMember = new Nullable<short>[] { 17293, 24387, null, -9785, null, -19656, null, 11924, -7364, 14077 },
                UInt16Member = 13731,
                UInt16ArrayMember = new ushort[] { 64012, 13179, 8480, 23065, 48806, 31092, 31802, 54264, 9874, 63596 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 41988, null, 41786, null, 58666, 30274, null, 15659, 37713, 7941 },
                Int32Member = -740088509,
                Int32ArrayMember = new int[] { 2065945944, 1045848136, 1653078597, 868549439, -389726022, -88944030, 1990043660, -1627590059, 820712825, -1963898699 },
                NullableInt32Member = 1494045241,
                NullableInt32ArrayMember = new Nullable<int>[] { null, null, -1260732757, -1019177164, null, -836126163, -1452207554, -1023273340, null, null },
                UInt32Member = 1704769485,
                UInt32ArrayMember = new uint[] { 3794301136, 436652114, 3236213931, 3938360877, 3033261761, 3474671659, 3551155734, 390923153, 3235808303, 1086958356 },
                NullableUInt32Member = 3711131475,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2830673768, null, null, 3830205421, 2976392218, null, 1475411454, 1923339620, 3811155700, 4023909862 },
                Int64Member = -5020175791033592996,
                Int64ArrayMember = new long[] { -3009347048124891177, 8315675429577208766, -6276335025194777856, 606428374383655293, -6236549123990318474, 1911810453553957186, 3578020229061055053, -2425027469470277602, -6361488450891445931, 776705566921116043 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, 4986902629735912847, 3374602319861656352, -6915116092094381350, -8871961431054842824, 6209999469991778988, 5154939779065960869, null, -4845138390795394012, null },
                UInt64Member = 18207561472323420917,
                UInt64ArrayMember = new ulong[] { 12527720178784873647, 9725106461280547478, 7892908837592893919, 6937856865008019820, 8287682421753232192, 1057589938258289313, 1496361251143460634, 10419409708497094077, 14173782783479391129, 15363996446145854648 },
                NullableUInt64Member = 10054967416609049134,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 4713397670876767823, null, 13068238649408498293, 14979600607837477441, 14981816218838923984, null, 11585160834494644802, 18219602119028201118, 1486189172034229434 },
                SingleMember = 0.2068F,
                SingleArrayMember = new float[] { 0.966F, 0.3665F, 0.3763F, 0.7315F, 0.413F, 0.9941F, 0.8421F, 0.664F, 0.2473F, 0.5356F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.0845F, 0.3535F, null, 0.1035F, 0.007F, 0.4915F, 0.4578F, 0.4081F, null },
                DoubleMember = 0.6374,
                DoubleArrayMember = new double[] { 0.6228, 0.3896, 0.1847, 0.5475, 0.4949, 0.1361, 0.9017, 0.3741, 0.54, 0.0442 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.161, 0.7886, 0.5358, 0.6863, 0.7015, 0.1393, 0.6432, null, 0.543, 0.3026 },
                DecimalMember = 0.0472852057564178m,
                DecimalArrayMember = new decimal[] { 0.903164743683788m, 0.225023817573903m, 0.897746290729214m, 0.976051983863905m, 0.728575708687754m, 0.729261999721676m, 0.565221351515583m, 0.523637000950346m, 0.36403751869305m, 0.540965486152984m },
                NullableDecimalMember = 0.855607522291739m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.742010556681202m, null, 0.491930784387118m, 0.278871798469831m, 0.951105179061355m, null, null, 0.290610377746467m, 0.604806569309919m, 0.187543987404073m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, null, null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(173434973),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(321338376), DateTime.Now.AddSeconds(-29631928), DateTime.Now.AddSeconds(47359363), DateTime.Now.AddSeconds(-207581789), DateTime.Now.AddSeconds(280745700), DateTime.Now.AddSeconds(-122326365), DateTime.Now.AddSeconds(293764625), DateTime.Now.AddSeconds(248425040), DateTime.Now.AddSeconds(-90496300), DateTime.Now.AddSeconds(108431658) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-274229393),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(101210114), DateTime.Now.AddSeconds(347068258), null, DateTime.Now.AddSeconds(221750563), DateTime.Now.AddSeconds(-32543210), DateTime.Now.AddSeconds(-33462556), DateTime.Now.AddSeconds(-137583346), null, null, DateTime.Now.AddSeconds(-27170755) },
            },
            new DataConvertersTestClass {
                StringMember = "b%1MMp\"Iyq",
                StringArrayMember = new string[] { "\" jIZX5484", "q6 \"38SQTo", "2hh1nG$ORT", "?XCWecPn '", "3\"A52!bK'x", "\"nE5fUhxuy", "5xwVoD#ts$", "#Ktpd#VMou", "7;nVVDj21f", "kJ BWNywAQ" },
                CharMember = 'I',
                CharArrayMember = new char[] { '8', 'a', 'r', '.', 'v', '0', 'D', 'b', '6', '7' },
                NullableCharMember = '@',
                NullableCharArrayMember = new Nullable<char>[] { ' ', 'I', null, 'h', 'H', 'e', 'f', 'w', 'J', 'P' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, false, true, true, true, true, false, true, true, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, false, true, null, true, false, false, true, null },
                ByteMember = 72,
                ByteArrayMember = new byte[] { 201, 236, 88, 152, 166, 158, 199, 37, 124, 225 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 192, 188, null, 144, null, 117, null, null, null, null },
                SByteMember = -3,
                SByteArrayMember = new sbyte[] { 113, 108, 113, -91, -73, -16, 54, 33, -82, 121 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 41, null, 114, -59, -107, 36, null, null, null, 70 },
                Int16Member = 25422,
                Int16ArrayMember = new short[] { 25644, -15549, -30191, -17030, -218, -3684, -7110, -2266, -25725, 5218 },
                NullableInt16Member = -2125,
                NullableInt16ArrayMember = new Nullable<short>[] { null, -11181, -5548, -31487, -2460, null, null, -1545, null, null },
                UInt16Member = 29755,
                UInt16ArrayMember = new ushort[] { 34720, 56317, 46343, 20375, 52540, 21598, 18146, 60558, 44495, 19885 },
                NullableUInt16Member = 59937,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 63775, 27518, 15894, 37203, null, 48880, null, 34109, null, 4994 },
                Int32Member = -395066494,
                Int32ArrayMember = new int[] { -1674051219, -878097345, 1995974938, -2082245470, -1179620166, -1269622736, -1415818410, 1471509407, -1852034663, -1501725136 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { 1027740033, 439168363, null, -443367628, null, 1491290849, -1551794105, null, null, 1833186462 },
                UInt32Member = 3919362332,
                UInt32ArrayMember = new uint[] { 1535886987, 1827012786, 3447769082, 3742432067, 2517345685, 1150735833, 319249350, 3144444911, 868197739, 766761399 },
                NullableUInt32Member = 3296083403,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2191459451, 1387371506, 1041010032, null, null, null, 516585828, null, 756302658, 1872848389 },
                Int64Member = -4777946284245609112,
                Int64ArrayMember = new long[] { 1446460504930420070, 7823939080582116118, 2371993070916421830, 4509262983220866003, 6015761653408382836, 8151094184304239252, -6478842612984221347, -4965230444226303757, -4096240185265221100, -686246631462628419 },
                NullableInt64Member = -5076624945019605939,
                NullableInt64ArrayMember = new Nullable<long>[] { 8981377635153930293, -1234240205440466655, 6368666346731298802, -4517760454413897055, null, -4308508467077891307, 1188193167354598300, 8488656440321613044, null, -7860411236742214484 },
                UInt64Member = 9280801670770460119,
                UInt64ArrayMember = new ulong[] { 1840172251078899998, 17189221520029825853, 15626549982137781823, 2027259184299030369, 18340134907112750450, 943161406514828513, 15650533133750381749, 17784615526537148775, 17863389356090570031, 16424528508438434062 },
                NullableUInt64Member = 5911882824756779942,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 17145000336890512183, 10297326479301213209, 16754508961657596824, null, 9598902575264850883, null, null, 352226616873424955, 12174933083030055287, 6727257902349316044 },
                SingleMember = 0.8959F,
                SingleArrayMember = new float[] { 0.0222F, 0.7863F, 0.72F, 0.1553F, 0.3106F, 0.3434F, 0.6538F, 0.8083F, 0.9829F, 0.2268F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.7401F, null, 0.3055F, 0.5165F, 0.4321F, 0.2449F, 0.4129F, 0.9673F, 0.0483F, 0.5889F },
                DoubleMember = 0.7573,
                DoubleArrayMember = new double[] { 0.8315, 0.6941, 0.861, 0.1043, 0.8029, 0.7704, 0.8756, 0.9981, 0.5137, 0.7107 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.8213, 0.8221, 0.0143, 0.2775, null, 0.9372, null, null, null, 0.1244 },
                DecimalMember = 0.574463549091089m,
                DecimalArrayMember = new decimal[] { 0.149293600828463m, 0.46846230322234m, 0.224237110709947m, 0.118364790588868m, 0.382050623489684m, 0.0630590240341068m, 0.833931580110833m, 0.929577200011493m, 0.931148764368291m, 0.345240963142299m },
                NullableDecimalMember = 0.179540932375823m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.845016960495886m, null, 0.689321690127902m, 0.0220145815690884m, 0.133245105046733m, 0.234405977404152m, 0.891174436572694m, 0.704581725463835m, 0.00476513050666794m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(263378175),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-236896953), DateTime.Now.AddSeconds(-137193043), DateTime.Now.AddSeconds(199458561), DateTime.Now.AddSeconds(126347502), DateTime.Now.AddSeconds(221364256), DateTime.Now.AddSeconds(82890622), DateTime.Now.AddSeconds(-97570075), DateTime.Now.AddSeconds(-161463458), DateTime.Now.AddSeconds(-32255589), DateTime.Now.AddSeconds(-341278058) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-93406775), DateTime.Now.AddSeconds(15169583), null, DateTime.Now.AddSeconds(-117901063), null, DateTime.Now.AddSeconds(-267461161), DateTime.Now.AddSeconds(314013064), DateTime.Now.AddSeconds(-55256398), null, DateTime.Now.AddSeconds(-14838125) },
            },
            new DataConvertersTestClass {
                StringMember = "H4RuFU7FTN",
                StringArrayMember = new string[] { "!hnrIHa?TZ", "4v3i. 2QJW", "iEOvezN Xv", "UA@R96Mhkm", "CssAE6poJH", "M!8,x5U5Af", "RRYKzdnyiY", "xPmIw2sOLj", "8G'NnwmOrE", "8TVLJP.BI9" },
                CharMember = 'n',
                CharArrayMember = new char[] { 'Y', 'h', '\'', '$', 'O', '2', ':', '!', 'Z', 'H' },
                NullableCharMember = 'u',
                NullableCharArrayMember = new Nullable<char>[] { '0', 'D', '7', '6', null, 'a', null, 's', 'i', '1' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, false, false, false, false, true, false, true, true, false },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, null, null, null, null, false, false, false, null, null },
                ByteMember = 234,
                ByteArrayMember = new byte[] { 82, 181, 193, 27, 120, 134, 163, 217, 224, 141 },
                NullableByteMember = 212,
                NullableByteArrayMember = new Nullable<byte>[] { 189, 92, 86, null, 221, null, 218, 189, 226, 170 },
                SByteMember = -84,
                SByteArrayMember = new sbyte[] { 44, 9, -10, -14, 90, -27, 71, 37, 73, -41 },
                NullableSByteMember = 91,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, 50, -90, null, -101, 86, null, null, -78, 83 },
                Int16Member = 12184,
                Int16ArrayMember = new short[] { 23847, 31010, 16586, 22080, -23451, -16248, 22252, -9655, 8050, 22081 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -14931, null, null, -19589, 15519, 28375, null, null, 11423, 22743 },
                UInt16Member = 16697,
                UInt16ArrayMember = new ushort[] { 51140, 17539, 1059, 37910, 24373, 39777, 24074, 641, 54582, 25606 },
                NullableUInt16Member = 9602,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 54207, null, null, 39822, 21750, 57385, 64653, 28761, null, null },
                Int32Member = 449049814,
                Int32ArrayMember = new int[] { -1059385426, -356561941, 1180999099, 788889254, 2056127189, -160181167, 1602496159, -1202377475, -228811940, -1678098066 },
                NullableInt32Member = -133898584,
                NullableInt32ArrayMember = new Nullable<int>[] { 946516392, -1501005498, 2103216681, null, -1736089716, null, 1877922823, null, -680305482, null },
                UInt32Member = 2437513119,
                UInt32ArrayMember = new uint[] { 660650180, 3562360846, 1934068853, 3107417326, 1328361026, 4238610719, 1542706051, 2831426195, 1331540635, 2003891127 },
                NullableUInt32Member = 730160490,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2246272435, null, 3082549931, null, 2402582460, 3782178500, 3887482636, null, 2002177506, 122237021 },
                Int64Member = -6146876315204322622,
                Int64ArrayMember = new long[] { 4869091428341508592, -588067331682577687, -5994607241718522261, 6802335395974576174, -5448849673310237797, -8427359084279495552, -5386539605336673231, 2042690261302611322, 7633705133868504088, 1521186016961097730 },
                NullableInt64Member = -834213191155102466,
                NullableInt64ArrayMember = new Nullable<long>[] { -5101490746912743380, -807499715621147113, -8998315150446526375, -1547698451296762490, -37505769798007198, -69448133778143320, -1342660962178400942, 6839341116850393075, -4128508284761882728, null },
                UInt64Member = 3960552292383880944,
                UInt64ArrayMember = new ulong[] { 10113615470120738387, 11131915629674789030, 15610150478210885352, 9179034181686174792, 6839076749966734422, 16327479013420863503, 4416766147122752256, 1366222928801313774, 6429002570611607734, 2619920706003725745 },
                NullableUInt64Member = 4739225591822802905,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 16053754823369877050, 8652687547318047967, 3768464747533979785, 10728296360521809401, 15798852939482831551, null, 4823486633577596078, 11430167021799201243, 18243339854287743571, 6013586646227464133 },
                SingleMember = 0.4183F,
                SingleArrayMember = new float[] { 0.0177F, 0.8245F, 0.0954F, 0.53F, 0.6849F, 0.9683F, 0.9863F, 0.2783F, 0.6858F, 0.2114F },
                NullableSingleMember = 0.4167F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.3028F, null, 0.6506F, 0.1574F, null, 0.4866F, 0.2835F, null, 0.241F, 0.78F },
                DoubleMember = 0.0916,
                DoubleArrayMember = new double[] { 0.0948, 0.1735, 0.6387, 0.0655, 0.9443, 0.844, 0.1027, 0.1015, 0.7608, 0.9304 },
                NullableDoubleMember = 0.1189,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.4514, 0.1846, 0.935, null, 0.3107, null, 0.7457, 0.1163, null },
                DecimalMember = 0.41625336541565m,
                DecimalArrayMember = new decimal[] { 0.601481500934004m, 0.244136204574283m, 0.146311837443127m, 0.0586209243013112m, 0.732792133177202m, 0.612169829385754m, 0.442086223020783m, 0.288434339257447m, 0.446849374565377m, 0.385648864787877m },
                NullableDecimalMember = 0.331024789363444m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, 0.640562858049213m, null, 0.588685295998216m, 0.374847184066928m, null, null, 0.0516015876178101m, 0.453008967577811m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-147961799),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-313818494), DateTime.Now.AddSeconds(-51267408), DateTime.Now.AddSeconds(139049002), DateTime.Now.AddSeconds(343376177), DateTime.Now.AddSeconds(255036404), DateTime.Now.AddSeconds(115699251), DateTime.Now.AddSeconds(-279631563), DateTime.Now.AddSeconds(11201765), DateTime.Now.AddSeconds(55747507), DateTime.Now.AddSeconds(224105434) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-111811478),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, null, DateTime.Now.AddSeconds(-974341), DateTime.Now.AddSeconds(70946529), DateTime.Now.AddSeconds(-270439688), DateTime.Now.AddSeconds(-312231527), null, DateTime.Now.AddSeconds(-127895514), DateTime.Now.AddSeconds(318732325), null },
            },
            new DataConvertersTestClass {
                StringMember = "q4T\"W@LUvO",
                StringArrayMember = new string[] { "ht2ZSTP,f'", ".ln,Sp7Ex0", "cGVWj6t3.L", "#w2Z\"#o9cZ", "\"3iqvK#'@ ", ",W1la,CY'S", ";w 5,hhJY;", "IMn4%Y@EhJ", "$dGy7u \"?c", "7F6\"y3OZ#L" },
                CharMember = 'g',
                CharArrayMember = new char[] { 'Y', ',', 'H', 'v', 's', '?', 'A', 'b', 'g', '2' },
                NullableCharMember = 'Y',
                NullableCharArrayMember = new Nullable<char>[] { 'l', 'd', 'M', 'f', 'u', 'K', 'f', 'Z', 'O', '5' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, false, true, false, false, true, false, true, true, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, false, false, null, null, null, true, true, null },
                ByteMember = 122,
                ByteArrayMember = new byte[] { 120, 77, 113, 50, 159, 189, 187, 251, 173, 191 },
                NullableByteMember = 60,
                NullableByteArrayMember = new Nullable<byte>[] { 166, 123, 78, null, 174, null, null, 229, 146, 26 },
                SByteMember = 100,
                SByteArrayMember = new sbyte[] { -53, -67, 57, 121, -127, 92, 120, -39, 123, 124 },
                NullableSByteMember = -74,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -62, -60, 74, null, null, null, 118, 124, -50, -29 },
                Int16Member = 10704,
                Int16ArrayMember = new short[] { 2695, 10263, -1775, 22228, -11780, -3459, -26657, 3830, 29393, 3490 },
                NullableInt16Member = 7152,
                NullableInt16ArrayMember = new Nullable<short>[] { -4311, -25522, null, -28154, null, -22070, -10379, null, null, null },
                UInt16Member = 61593,
                UInt16ArrayMember = new ushort[] { 33250, 62021, 39341, 56811, 15990, 52094, 41871, 53855, 7664, 63279 },
                NullableUInt16Member = 61873,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 58855, null, 3829, 36835, null, 21216, 57312, 25824, 27316, 14413 },
                Int32Member = -949979736,
                Int32ArrayMember = new int[] { 1784702886, -1276394778, 1096732945, -231135833, -876467731, 377717521, -1566359840, 372802319, 1984543538, 1953273893 },
                NullableInt32Member = 164847457,
                NullableInt32ArrayMember = new Nullable<int>[] { -354664146, 1372162572, null, null, -429612882, 2020505692, null, 208006550, -936261845, null },
                UInt32Member = 1502331468,
                UInt32ArrayMember = new uint[] { 3964752861, 2922263100, 2970557139, 3026209149, 3977194177, 2357911732, 2286454081, 2966370752, 3683574501, 1676206115 },
                NullableUInt32Member = 705471595,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1227433403, 2411252913, 594774604, 4003943154, null, 1957939106, 1928711312, null, 2802383228, 3777153612 },
                Int64Member = 294261733936530599,
                Int64ArrayMember = new long[] { -3851957674440860506, -101976137339978863, 6536499704309156049, -4932737180175456069, 8333457093716560468, 5301806245745990900, 6878933008115495504, 4095000178104604370, -756029536420643111, -4368959591249911708 },
                NullableInt64Member = 128671799077623446,
                NullableInt64ArrayMember = new Nullable<long>[] { 2174282875055735316, 2158315868323544225, -6086020961302071759, -6884001938895696293, -6709259846224484048, null, -5327080040828313074, -3580444430422590884, null, -2266965885650753060 },
                UInt64Member = 8532358832002840114,
                UInt64ArrayMember = new ulong[] { 8819342853605374760, 15175160785220337232, 6663561971903810311, 2198119097712892145, 825412706577948268, 1040673384775622918, 7698184808600972460, 10294660178516156786, 13757648517649583397, 9007161902317137650 },
                NullableUInt64Member = 4255951940323464574,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, null, 13241295896246350754, 13412195280725973711, 3963928764100359915, 11798325724561054938, 1806680404339303068, 10145870914129956501, null, null },
                SingleMember = 0.1913F,
                SingleArrayMember = new float[] { 0.0744F, 0.5214F, 0.6931F, 0.8874F, 0.3887F, 0.5943F, 0.7183F, 0.0169F, 0.2238F, 0.142F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.5232F, null, null, 0.3877F, null, 0.4978F, null, null, 0.8982F },
                DoubleMember = 0.29,
                DoubleArrayMember = new double[] { 0.533, 0.1807, 0.9878, 0.74, 0.6211, 0.6989, 0.2048, 0.3028, 0.8983, 0.677 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.682, null, 0.02, 0.3835, 0.7909, 0.6687, 0.3045, 0.2374, 0.2292, 0.0427 },
                DecimalMember = 0.335639894541211m,
                DecimalArrayMember = new decimal[] { 0.415768333905294m, 0.342446219229065m, 0.945962681553816m, 0.610171803893147m, 0.696407344121188m, 0.349220196394324m, 0.851025067218948m, 0.732263668508579m, 0.892163531538738m, 0.46254412386983m },
                NullableDecimalMember = 0.157685780006884m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.606728768970536m, 0.65197754504432m, 0.495143029457144m, null, 0.814159946741949m, 0.735137525852127m, null, null, 0.431924716176888m, 0.641337878100745m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(243234981),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-114377465), DateTime.Now.AddSeconds(187139803), DateTime.Now.AddSeconds(-118440043), DateTime.Now.AddSeconds(1839355), DateTime.Now.AddSeconds(131936215), DateTime.Now.AddSeconds(-76443769), DateTime.Now.AddSeconds(345879309), DateTime.Now.AddSeconds(164896960), DateTime.Now.AddSeconds(-109099539), DateTime.Now.AddSeconds(115365323) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-43345735),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-7057128), DateTime.Now.AddSeconds(-24145295), null, null, DateTime.Now.AddSeconds(-193850764), DateTime.Now.AddSeconds(228295914), DateTime.Now.AddSeconds(33932567), DateTime.Now.AddSeconds(152387140), null, DateTime.Now.AddSeconds(300083686) },
            },
            new DataConvertersTestClass {
                StringMember = "B:Af#Gm9Mm",
                StringArrayMember = new string[] { "w1j#JOKa;,", "wY'!5b2jg?", "3 gJseB,0I", "Ex@F?W8jq@", "T5yV1!ZaqP", "8,KC ,n4p$", "K.T$qrXlwQ", "W:. 10J!6j", "5sa0H%1Bup", "T'rOb0y20v" },
                CharMember = 'C',
                CharArrayMember = new char[] { '3', 'Q', 's', 'V', 'k', ':', 'r', ';', '$', 'D' },
                NullableCharMember = 'B',
                NullableCharArrayMember = new Nullable<char>[] { 's', 'q', null, 'W', 'd', 'I', 'e', 'd', 'l', null },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, false, true, false, false, false, false, false, false },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, false, true, null, null, false, null, null, true },
                ByteMember = 81,
                ByteArrayMember = new byte[] { 152, 164, 202, 198, 196, 16, 100, 200, 61, 228 },
                NullableByteMember = 146,
                NullableByteArrayMember = new Nullable<byte>[] { 128, 18, 196, null, 68, 243, 248, 130, null, 243 },
                SByteMember = -69,
                SByteArrayMember = new sbyte[] { 126, 72, 45, -96, 101, -12, -52, 50, 90, 56 },
                NullableSByteMember = 87,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 69, 42, -91, 1, -123, -1, null, -70, null, -65 },
                Int16Member = 13487,
                Int16ArrayMember = new short[] { 31506, 1589, -30981, 26938, -12114, -5245, -7468, 17988, 13009, -27095 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { null, null, null, null, -14685, 3012, -30024, -13181, -29068, 29459 },
                UInt16Member = 49964,
                UInt16ArrayMember = new ushort[] { 14808, 49276, 40593, 14524, 63642, 28527, 39238, 54551, 12609, 20008 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 16373, 63565, 7046, null, null, null, null, 6159, 20594, 42939 },
                Int32Member = -2025662976,
                Int32ArrayMember = new int[] { -210465984, 1730155410, 1879184340, -403845274, 686790638, 376565426, -316133662, 237094675, -426197823, -731965660 },
                NullableInt32Member = 481586162,
                NullableInt32ArrayMember = new Nullable<int>[] { null, 583262683, -1737507176, -1279128122, null, null, 1002860696, null, null, -152082345 },
                UInt32Member = 3048036615,
                UInt32ArrayMember = new uint[] { 3268624668, 818121364, 2743131967, 2785284410, 1109015244, 1038035545, 3183633079, 729204136, 3374639427, 2953286266 },
                NullableUInt32Member = 1947641895,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, null, 354168284, 4098240756, 2241554144, 1539054205, null, null, null, 2281756323 },
                Int64Member = 1209545141221294845,
                Int64ArrayMember = new long[] { -9003128312762782108, 5321958478706519264, 7059657968898026079, -6444970401672400771, -987518162115773411, -5371790248061943969, -8374142355454313450, -3853890288150944490, -6839653265002181382, -905752581005019677 },
                NullableInt64Member = -1753967748034040634,
                NullableInt64ArrayMember = new Nullable<long>[] { -7752059937087751693, -6369685867599014282, null, 8482115978447802448, 4943728271099825006, -1489261931190981179, -8095204136269100194, 2874036459505808262, -4169393943312938226, -7054731335186629724 },
                UInt64Member = 12666601650756243677,
                UInt64ArrayMember = new ulong[] { 11606457211977432384, 11859607436170457389, 13782101589612098549, 1886108945525756955, 10595972569361780740, 17982095272789080841, 13445423892956686132, 10302973366407143094, 6880608382027462403, 1840147192402475075 },
                NullableUInt64Member = 11447790855374815982,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 8720078147613687427, null, null, 9430025607140757560, 5600190172340053646, 9532740143014559681, 7271001041473310648, 1168962795823770070, null, null },
                SingleMember = 0.7298F,
                SingleArrayMember = new float[] { 0.1585F, 0.7682F, 0.7118F, 0.1706F, 0.5322F, 0.7487F, 0.4227F, 0.4574F, 0.7139F, 0.8595F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.1787F, 0.7589F, null, null, 0.2788F, 0.448F, 0.1865F, null, 0.4941F },
                DoubleMember = 0.6772,
                DoubleArrayMember = new double[] { 0.7619, 0.9273, 0.0562, 0.858, 0.5655, 0.4942, 0.4314, 0.25, 0.8183, 0.2815 },
                NullableDoubleMember = 0.5715,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.8939, null, null, null, null, null, null, 0.8952, 0.5711 },
                DecimalMember = 0.613031131096534m,
                DecimalArrayMember = new decimal[] { 0.13671946814671m, 0.780482210762684m, 0.519520435266272m, 0.285208572835629m, 0.30327847952147m, 0.566492363069601m, 0.636769605128812m, 0.443651082843674m, 0.154433527695777m, 0.14281890808869m },
                NullableDecimalMember = 0.896811723679277m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, 0.878205134802851m, null, null, 0.018867775374505m, 0.919336756606483m, 0.461830169483981m, null, 0.743136677280862m, 0.0320419511703395m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-190705844),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(144367721), DateTime.Now.AddSeconds(-60856525), DateTime.Now.AddSeconds(-153871557), DateTime.Now.AddSeconds(106259769), DateTime.Now.AddSeconds(332648001), DateTime.Now.AddSeconds(-276690502), DateTime.Now.AddSeconds(111393173), DateTime.Now.AddSeconds(42683118), DateTime.Now.AddSeconds(254297425), DateTime.Now.AddSeconds(-222192268) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(219222027),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-79266301), null, DateTime.Now.AddSeconds(-72912103), DateTime.Now.AddSeconds(348760338), null, DateTime.Now.AddSeconds(-335443163), null, DateTime.Now.AddSeconds(-108137990), DateTime.Now.AddSeconds(-234752697), null },
            },
            new DataConvertersTestClass {
                StringMember = "mra\"OIIX$W",
                StringArrayMember = new string[] { "0OLO8PJqpW", "aZhT6@nj f", "rk:B38fee;", "ul8KK!bvc1", "rR9gSj8epY", "HU#pNWeQ%d", "::pcF!mItu", "8:#c#nladh", "tH;DoQ9qfF", "'J1v4A\"fK@" },
                CharMember = '!',
                CharArrayMember = new char[] { 'K', 'R', 'f', 'l', 'S', 'O', '1', '4', 'A', 'X' },
                NullableCharMember = 'v',
                NullableCharArrayMember = new Nullable<char>[] { 'N', 'j', null, 'S', ',', 'V', '0', null, null, 'W' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, true, false, false, false, false, true, true, false },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, true, false, false, true, true, false, null, true },
                ByteMember = 7,
                ByteArrayMember = new byte[] { 164, 86, 62, 6, 206, 209, 235, 231, 240, 79 },
                NullableByteMember = 140,
                NullableByteArrayMember = new Nullable<byte>[] { 46, 222, null, null, null, 154, 150, 216, 139, null },
                SByteMember = -74,
                SByteArrayMember = new sbyte[] { -103, 45, 1, 66, -36, -107, -94, -104, -114, 92 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -124, null, 94, null, -12, null, null, 103, null },
                Int16Member = 5341,
                Int16ArrayMember = new short[] { 1122, -15024, -22474, -18646, -29428, -10279, -790, -30943, -19135, 31789 },
                NullableInt16Member = -6389,
                NullableInt16ArrayMember = new Nullable<short>[] { null, -31391, 26089, 9704, null, null, null, 19125, 29703, -8815 },
                UInt16Member = 37727,
                UInt16ArrayMember = new ushort[] { 7113, 43264, 35614, 13040, 22185, 64883, 12185, 35518, 11621, 13496 },
                NullableUInt16Member = 12265,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, null, null, null, null, null, 27637, 4359, null, null },
                Int32Member = -1962691298,
                Int32ArrayMember = new int[] { 1537600923, 1903802312, 806793648, -158103355, 1402921914, 1393928388, -176379243, 244217734, -321987141, 1356410724 },
                NullableInt32Member = 1057807169,
                NullableInt32ArrayMember = new Nullable<int>[] { null, -1726556258, null, 661143139, null, 1085453033, 754605207, 685475967, null, null },
                UInt32Member = 3565218594,
                UInt32ArrayMember = new uint[] { 2175761698, 774504078, 4280671958, 1428874326, 2602217656, 3063186500, 1673345552, 1238675723, 215452154, 1081936125 },
                NullableUInt32Member = 3605613374,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2161110611, 100077366, 1900443721, 1138782160, null, 557706277, 2989557776, 3381428669, 2479350861, null },
                Int64Member = 3817398316605007406,
                Int64ArrayMember = new long[] { -6177904806249308317, -1900060750812773198, -1655924336593919641, 8471049680302533344, -6315514389868135571, -5346307635473243506, 3824529320180899548, -4275995883752686987, 6244584828123510529, 4690999557660008375 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { 3412214820160440501, -5040313002749825955, -7110689707684535500, null, 7228085288107509055, -5174678589579533321, 6955007389961234842, 1326699950260233897, null, 8368317150218917543 },
                UInt64Member = 6418469012547001877,
                UInt64ArrayMember = new ulong[] { 3662262370269256961, 17511648265162064764, 7968380144109338939, 16515749084821847370, 13594285049105789638, 11583505500379189719, 14379776211097386003, 3229836945111827331, 8628623432825387042, 3990326012333389916 },
                NullableUInt64Member = 4589408276109062873,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 7817388220177734377, 12191722172037359613, null, null, null, null, 5179197727052214333, 11891326234092891077, 17749075409732058075, null },
                SingleMember = 0.2831F,
                SingleArrayMember = new float[] { 0.2804F, 0.6047F, 0.756F, 0.2338F, 0.5208F, 0.9151F, 0.1457F, 0.3344F, 0.1527F, 0.3719F },
                NullableSingleMember = 0.2413F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.1828F, 0.9647F, 0.7244F, 0.3644F, 0.9797F, null, null, 0.3404F, 0.4438F, 0.2344F },
                DoubleMember = 0.0679,
                DoubleArrayMember = new double[] { 0.2923, 0.7948, 0.1005, 0.9555, 0.0407, 0.1639, 0.4993, 0.2764, 0.0852, 0.9337 },
                NullableDoubleMember = 0.428,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.2748, 0.9949, null, 0.446, 0.702, null, null, null, 0.4528, 0.1569 },
                DecimalMember = 0.461964341388271m,
                DecimalArrayMember = new decimal[] { 0.670919208490265m, 0.470247134076659m, 0.198322808073207m, 0.060772224882565m, 0.940737509993591m, 0.355315180857574m, 0.855346200786459m, 0.680000200949643m, 0.857841251795957m, 0.926414659648232m },
                NullableDecimalMember = 0.587356575919974m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.326075639985141m, 0.972403847669278m, 0.579077415647702m, null, 0.0472631052436679m, 0.742241210039283m, null, 0.322871006902866m, null, 0.895523402746043m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(68595210),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-219598820), DateTime.Now.AddSeconds(35170968), DateTime.Now.AddSeconds(214127630), DateTime.Now.AddSeconds(-60100794), DateTime.Now.AddSeconds(-331695227), DateTime.Now.AddSeconds(244312193), DateTime.Now.AddSeconds(112802081), DateTime.Now.AddSeconds(186168857), DateTime.Now.AddSeconds(236999588), DateTime.Now.AddSeconds(123640138) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-222135465),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(215030114), DateTime.Now.AddSeconds(332927244), null, DateTime.Now.AddSeconds(139640968), null, null, null, DateTime.Now.AddSeconds(-310897773), DateTime.Now.AddSeconds(104349238), null },
            },
            new DataConvertersTestClass {
                StringMember = "h!ERF6C.@d",
                StringArrayMember = new string[] { "vA?zZLqr H", "vMEzPj2,bZ", " k?ALZ7DML", "J2%,%KL#G\"", "iOc@E%$e.v", "XHsFKai0sR", "h;Lz?1M\"x3", "Z,6@u23H7f", "InOBL.0C y", ":b;xmgBorv" },
                CharMember = ',',
                CharArrayMember = new char[] { '7', 'X', 'h', ';', 'm', 'Q', 'D', 'k', 'V', 'q' },
                NullableCharMember = '.',
                NullableCharArrayMember = new Nullable<char>[] { 'x', 't', 'p', null, '4', 'W', 'E', '%', null, 'V' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, true, false, false, false, true, true, false, false, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, true, null, false, true, true, false, null, null },
                ByteMember = 178,
                ByteArrayMember = new byte[] { 168, 111, 253, 158, 104, 38, 254, 62, 215, 226 },
                NullableByteMember = 66,
                NullableByteArrayMember = new Nullable<byte>[] { 62, 6, 153, 152, null, 73, 22, 76, 235, null },
                SByteMember = 13,
                SByteArrayMember = new sbyte[] { -101, 88, -49, 74, -88, -122, 88, -45, -114, 113 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, null, null, null, null, null, -56, -75, 77, 96 },
                Int16Member = -23729,
                Int16ArrayMember = new short[] { -15051, -3928, -22877, 31200, -32610, 4719, 2991, -19430, 5994, -11428 },
                NullableInt16Member = 20805,
                NullableInt16ArrayMember = new Nullable<short>[] { null, null, null, null, -27660, -31730, null, null, -6679, 8978 },
                UInt16Member = 54709,
                UInt16ArrayMember = new ushort[] { 25604, 50751, 10769, 49384, 10041, 13592, 50354, 28115, 19863, 13956 },
                NullableUInt16Member = 27755,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 10276, 27618, null, 13030, 15970, 54686, 39674, 48552, null },
                Int32Member = 714398553,
                Int32ArrayMember = new int[] { -340543795, -1853906627, 1456973720, -290586073, -689651727, -530638703, 1803366689, 171710608, -1100922918, -1299058271 },
                NullableInt32Member = 1389584194,
                NullableInt32ArrayMember = new Nullable<int>[] { null, -255112450, -1873945566, -1361024622, -564723940, -417989129, 1839914974, 1463900223, -1887072761, null },
                UInt32Member = 2117387343,
                UInt32ArrayMember = new uint[] { 609926464, 416372402, 3402847249, 833863661, 2409683371, 2671407858, 3114948682, 3293604528, 4184127384, 1069450759 },
                NullableUInt32Member = 1313657886,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1650588865, null, 323100163, null, 1050456217, 994065192, 3144853886, 3016090226, null, 3538112331 },
                Int64Member = 2591739785721455393,
                Int64ArrayMember = new long[] { -4063729326136089954, 3383030548949057374, 8065956754198591632, 7304478444909924870, 3980642790607309332, 4877705840142303803, 5071104805389753216, 4620723105086201737, 9037886979362387311, -2329489657665280545 },
                NullableInt64Member = 5330591282780107367,
                NullableInt64ArrayMember = new Nullable<long>[] { -1940027683819031276, 1570689234401246843, 5497637083928839973, -8660669805992932074, -8255626229521774150, 839694518729102912, null, 9183139542499318036, null, -650158582558103743 },
                UInt64Member = 12444899398943366389,
                UInt64ArrayMember = new ulong[] { 4636283029173234735, 5109481482930354382, 15162289979720338576, 2765652585803726134, 9354205752183575997, 18061926546084823723, 13164078438455158942, 11162185527435650227, 13305425865747203428, 5939880396172556054 },
                NullableUInt64Member = 17606316207242866767,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 13567092482038502584, 3314836316577892914, 2557866091076596193, 6545570444131867592, 4022270491336265016, 14711042685679561895, null, null, null, 6128640764104947783 },
                SingleMember = 0.8316F,
                SingleArrayMember = new float[] { 0.1133F, 0.8677F, 0.532F, 0.0867F, 0.9769F, 0.2744F, 0.1816F, 0.9554F, 0.0419F, 0.5964F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.6962F, 0.6513F, 0.1958F, 0.4724F, null, null, 0.3314F, null, 0.5298F, 0.8132F },
                DoubleMember = 0.8872,
                DoubleArrayMember = new double[] { 0.0184, 0.6741, 0.5261, 0.7922, 0.8122, 0.6565, 0.3917, 0.0288, 0.0559, 0.2619 },
                NullableDoubleMember = 0.863,
                NullableDoubleArrayMember = new Nullable<double>[] { null, null, null, 0.1759, null, null, 0.0488, 0.8581, null, 0.5267 },
                DecimalMember = 0.454256788799279m,
                DecimalArrayMember = new decimal[] { 0.685104346044783m, 0.813403434119328m, 0.801994702636149m, 0.816799849483781m, 0.911460043920989m, 0.601437282943362m, 0.973322624768522m, 0.129257144698928m, 0.344650622215545m, 0.138860998172678m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.123146371954563m, 0.253301437216075m, 0.50546988793752m, 0.976011004950809m, 0.226813326488771m, 0.657868343366885m, 0.610332960843772m, 0.57624010694776m, null, 0.596760932788142m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(15519720),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(168573759), DateTime.Now.AddSeconds(-111937278), DateTime.Now.AddSeconds(-44086401), DateTime.Now.AddSeconds(275499952), DateTime.Now.AddSeconds(-95363263), DateTime.Now.AddSeconds(127308983), DateTime.Now.AddSeconds(-270271978), DateTime.Now.AddSeconds(-3173085), DateTime.Now.AddSeconds(-141803271), DateTime.Now.AddSeconds(124656053) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-216347923),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(52550766), null, DateTime.Now.AddSeconds(-214384802), DateTime.Now.AddSeconds(-69969205), null, DateTime.Now.AddSeconds(295691481), null, DateTime.Now.AddSeconds(-57585316), DateTime.Now.AddSeconds(-107803527), null },
            },
            new DataConvertersTestClass {
                StringMember = "s3q1csDKez",
                StringArrayMember = new string[] { "muPru:c%w#", "@J3.x%b8Tn", ":t G2q6kuh", "rP\";UlOA@8", "2%9bHSlbA7", ";?cq,YPp,G", "UpZX9aUs7K", "xfL?9wo:rN", "?Etgi%Tv1A", "Fp0Gi!tYDc" },
                CharMember = 'Q',
                CharArrayMember = new char[] { 'z', ':', 'p', 'B', '?', 'k', 'k', '5', 'C', 'Z' },
                NullableCharMember = 'u',
                NullableCharArrayMember = new Nullable<char>[] { null, 'y', null, '7', null, '2', null, '"', null, null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, true, true, false, false, true, false, true, true, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, null, null, false, false, false, null, false, null },
                ByteMember = 163,
                ByteArrayMember = new byte[] { 162, 239, 50, 207, 142, 174, 134, 89, 244, 212 },
                NullableByteMember = 144,
                NullableByteArrayMember = new Nullable<byte>[] { null, 32, 72, null, 45, 166, null, 215, 146, 94 },
                SByteMember = -27,
                SByteArrayMember = new sbyte[] { -106, 1, -62, 116, -22, 16, 94, -19, -83, 43 },
                NullableSByteMember = -82,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -60, null, null, null, -41, null, null, -10, 95, null },
                Int16Member = -16200,
                Int16ArrayMember = new short[] { 20018, 14604, 30021, -28471, -14146, -15712, 19755, -25786, -9944, 20347 },
                NullableInt16Member = -8560,
                NullableInt16ArrayMember = new Nullable<short>[] { 31025, null, 343, -16889, -7048, null, 18746, null, null, -3181 },
                UInt16Member = 48854,
                UInt16ArrayMember = new ushort[] { 42550, 34718, 63538, 10429, 19703, 61135, 64855, 19314, 45667, 58777 },
                NullableUInt16Member = 36756,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 5196, null, 32904, 33299, 41083, 42497, 55125, 46676, 30335 },
                Int32Member = -880585470,
                Int32ArrayMember = new int[] { 578398156, 1211358075, -585437198, 70752595, 1711946386, -1085822339, 604734847, -527644255, 1960596138, -771929882 },
                NullableInt32Member = 1215073261,
                NullableInt32ArrayMember = new Nullable<int>[] { null, 609971614, -1370253604, null, -1865279901, null, null, -1758930406, null, 1118344404 },
                UInt32Member = 1614334659,
                UInt32ArrayMember = new uint[] { 2961242810, 1364513450, 33077038, 341260895, 2990968088, 1733090698, 3539307160, 3805645180, 2629053143, 3699427330 },
                NullableUInt32Member = 2544555924,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3748559650, 4062565615, 635017624, null, null, 2287590083, 1038903566, null, 2718664161, 3368502681 },
                Int64Member = -2684056215784402155,
                Int64ArrayMember = new long[] { 6964939986727358303, 4366744805955855138, 6336018188057374723, -2080667704414318584, -625174111026962815, -3819535388010403666, 380274592752035827, -2033195094653797876, 7609885510693648441, -761050342531249149 },
                NullableInt64Member = -100150397519238873,
                NullableInt64ArrayMember = new Nullable<long>[] { -2869557786008462989, null, -2651453742411258866, 759869090160146798, null, -244588805132232061, 1763633175542754127, -3256369183812593665, -7448114462727521658, -7687866003687282599 },
                UInt64Member = 5336056040723365681,
                UInt64ArrayMember = new ulong[] { 14791567866535628083, 493403642714875016, 17603094019384600427, 1493239609379198791, 4292634154339389174, 14180638098982225024, 1408102673090196306, 14310999614279677987, 8374476830511294542, 9050181805938878249 },
                NullableUInt64Member = 16189452550659536071,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 12337915215469548875, 115349864157044439, 7112434266551103102, 6799317740831371019, 16453204165870303170, 4661523318509540775, null, 16894424228934306462, null, 13545402821627314054 },
                SingleMember = 0.0031F,
                SingleArrayMember = new float[] { 0.3375F, 0.5987F, 0.036F, 0.1923F, 0.0698F, 0.5651F, 0.8814F, 0.2599F, 0.4778F, 0.3271F },
                NullableSingleMember = 0.6839F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.8844F, 0.8698F, null, null, null, null, null, null, 0.8862F },
                DoubleMember = 0.3008,
                DoubleArrayMember = new double[] { 0.2246, 0.9562, 0.8272, 0.2793, 0.8033, 0.1033, 0.3812, 0.6894, 0.503, 0.026 },
                NullableDoubleMember = 0.7313,
                NullableDoubleArrayMember = new Nullable<double>[] { null, null, 0.9151, 0.4981, 0.5723, 0.0354, 0.7656, null, 0.9066, null },
                DecimalMember = 0.428932111233448m,
                DecimalArrayMember = new decimal[] { 0.247213536857242m, 0.606728419254655m, 0.662201110664868m, 0.319032551979769m, 0.884738361632331m, 0.90592126281893m, 0.804323620051519m, 0.00996580357422083m, 0.941481414714894m, 0.354534153301995m },
                NullableDecimalMember = 0.547761265055481m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.494411967873773m, 0.108220916354002m, 0.784865762636926m, 0.297711930270805m, null, 0.411381192612933m, null, 0.413407499958315m, 0.47882011741591m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(-153129358),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-323382623), DateTime.Now.AddSeconds(9393030), DateTime.Now.AddSeconds(185209132), DateTime.Now.AddSeconds(-204416545), DateTime.Now.AddSeconds(-14621500), DateTime.Now.AddSeconds(22676231), DateTime.Now.AddSeconds(125331036), DateTime.Now.AddSeconds(-194160751), DateTime.Now.AddSeconds(322125380), DateTime.Now.AddSeconds(269456294) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-1690256), null, DateTime.Now.AddSeconds(114675960), DateTime.Now.AddSeconds(-324543615), DateTime.Now.AddSeconds(113582224), null, DateTime.Now.AddSeconds(341848918), DateTime.Now.AddSeconds(110893235), DateTime.Now.AddSeconds(-23827481), null },
            },
            new DataConvertersTestClass {
                StringMember = "WPZJma;Y%l",
                StringArrayMember = new string[] { "YqFB.u529$", ",CtEmKYP1L", "vC.kf9awvv", "q$84MJ\"qU4", "vZSyx;Nto#", "nRQRvjoOlL", "teP@9apS58", "WU'?pPjDpK", "elNi:5:j,p", "lxO$pMs,GS" },
                CharMember = '?',
                CharArrayMember = new char[] { 'T', '?', 'i', '9', 'y', 'M', 'M', 'e', 'w', '#' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, 'g', 'k', 'V', null, 'q', 'q', 'J', null, 'u' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, false, true, false, true, true, true, false, true, false },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, null, true, null, true, null, null, true, true },
                ByteMember = 188,
                ByteArrayMember = new byte[] { 165, 250, 66, 86, 145, 121, 46, 189, 214, 33 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, 76, 158, 244, null, 96, null, 42, null, null },
                SByteMember = -32,
                SByteArrayMember = new sbyte[] { -71, 14, 2, -123, 102, 47, 102, 64, 87, 74 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 81, null, -77, null, null, 2, null, -4, -50, 4 },
                Int16Member = -24865,
                Int16ArrayMember = new short[] { 13963, -16365, -181, -8653, -29706, 18403, -18499, -14466, 29061, -19965 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { null, -12557, null, -10356, -31006, null, -17774, 16436, null, 29601 },
                UInt16Member = 19417,
                UInt16ArrayMember = new ushort[] { 58816, 8230, 33566, 18187, 14029, 31928, 2376, 25518, 28657, 9915 },
                NullableUInt16Member = 2619,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 31183, 24406, 30497, null, null, 27812, 34353, 16352, 43787 },
                Int32Member = -1819569158,
                Int32ArrayMember = new int[] { -667008676, 1594629231, -1481309385, -1522006312, 1760066249, -995602740, -1799691987, -402045011, 800943367, -761094258 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { -1639993207, null, -1298870239, null, 1936937721, -692716281, null, null, null, -1286515201 },
                UInt32Member = 3364537196,
                UInt32ArrayMember = new uint[] { 1900727815, 3786556791, 4289111848, 3733310858, 2600737145, 3554003302, 218261398, 2290934434, 2689694987, 3619618156 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1827210396, null, 912182816, null, 480362287, null, null, null, 3031368369, 4094482455 },
                Int64Member = 7961888124918305361,
                Int64ArrayMember = new long[] { 4873546562871986427, 3343072406146403980, 7085340932113404445, -6857762021744281308, 3762356659081869115, 2478987371668675970, -3236853987742326824, 8570644606758063769, 5229366674304152029, -7189992030497013892 },
                NullableInt64Member = 3952805948536405710,
                NullableInt64ArrayMember = new Nullable<long>[] { -7439258229602495311, 3586941246893280407, null, -6161622654052041731, 4035337412188280523, -4325536853910967430, -7687660986780028481, null, -908300435582330296, null },
                UInt64Member = 1047352652232056001,
                UInt64ArrayMember = new ulong[] { 7404060184852994968, 206329016272489225, 6224242975505190856, 6809467800657733815, 5216589783792071556, 9355712585114503876, 10307925833496897865, 5291929573080579488, 10231254187604478565, 2317833272556034091 },
                NullableUInt64Member = 1579533824248433647,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 12389848892428092410, 4605482903599399169, 15816778479167877868, null, null, null, 12046877344203895414, null, 18072410464726217106, 17574465768397050730 },
                SingleMember = 0.0843F,
                SingleArrayMember = new float[] { 0.0108F, 0.9111F, 0.0814F, 0.4416F, 0.2276F, 0.9089F, 0.8494F, 0.5299F, 0.6827F, 0.7969F },
                NullableSingleMember = 0.6779F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.5121F, 0.9197F, null, 0.9452F, 0.6879F, 0.3814F, 0.7205F, null, 0.9391F, 0.0506F },
                DoubleMember = 0.8318,
                DoubleArrayMember = new double[] { 0.6714, 0.6507, 0.2198, 0.9762, 0.3444, 0.7762, 0.5542, 0.8651, 0.2095, 0.199 },
                NullableDoubleMember = 0.2986,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.538, null, 0.2707, 0.6575, 0.2646, 0.7829, null, 0.7421, null, 0.4819 },
                DecimalMember = 0.75387360712166m,
                DecimalArrayMember = new decimal[] { 0.895793899752496m, 0.803737829852777m, 0.382935353718601m, 0.382195766968067m, 0.360701952412287m, 0.166440945466351m, 0.7919625228986m, 0.223326281172693m, 0.287596523897804m, 0.576346632007847m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.0375435594686796m, 0.634466921591713m, null, null, 0.301195427392794m, null, 0.983235325755791m, 0.221956590192495m, null, 0.246679529941626m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, null },
                DateTimeMember = DateTime.Now.AddSeconds(-177378270),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(99474661), DateTime.Now.AddSeconds(227685276), DateTime.Now.AddSeconds(-39699438), DateTime.Now.AddSeconds(60500263), DateTime.Now.AddSeconds(244358104), DateTime.Now.AddSeconds(333600141), DateTime.Now.AddSeconds(260716969), DateTime.Now.AddSeconds(10413217), DateTime.Now.AddSeconds(-89335720), DateTime.Now.AddSeconds(173899577) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-149092431),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(64686516), null, null, DateTime.Now.AddSeconds(-1120042), null, DateTime.Now.AddSeconds(-282115891), DateTime.Now.AddSeconds(-117849157), DateTime.Now.AddSeconds(-73424017), null, null },
            },
            new DataConvertersTestClass {
                StringMember = "hG w\"5ct;I",
                StringArrayMember = new string[] { "7?vtYiWB6B", "qogyb5Y'Ha", "Xkd2FLfAY7", "4fQ28B%Hhh", "\"xB,XEHgHK", "1Llv8,FqZ ", ".$;8b?RZ%Y", "qG8B0qiAjz", "BjHh? T4hH", "0e%xTXaj1C" },
                CharMember = ';',
                CharArrayMember = new char[] { '@', '?', 'A', ';', 'E', ',', 'q', '0', '2', 'E' },
                NullableCharMember = 'l',
                NullableCharArrayMember = new Nullable<char>[] { null, null, 'd', 'h', '6', '8', null, ';', '4', 'G' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, true, false, false, true, true, true, true, true, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, false, true, false, true, true, null, false, true },
                ByteMember = 49,
                ByteArrayMember = new byte[] { 10, 160, 104, 129, 226, 62, 223, 185, 39, 12 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 88, 105, 151, null, 17, null, 49, 228, 121, 68 },
                SByteMember = 56,
                SByteArrayMember = new sbyte[] { -92, -35, -114, -13, 6, 16, -99, -122, -42, -64 },
                NullableSByteMember = -16,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, null, -103, -25, null, 1, -95, 77, null, null },
                Int16Member = -11489,
                Int16ArrayMember = new short[] { -14053, -23936, -17091, -27013, -630, -17783, 20608, 31213, -19427, 12944 },
                NullableInt16Member = -22294,
                NullableInt16ArrayMember = new Nullable<short>[] { 143, null, 20120, -5753, -17430, 17089, 21862, -17415, 20952, null },
                UInt16Member = 48309,
                UInt16ArrayMember = new ushort[] { 6069, 65209, 26004, 13507, 22919, 28874, 56211, 65219, 41684, 23539 },
                NullableUInt16Member = 28608,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 40757, null, null, 53787, 60189, 9985, 150, 42229, null, 54465 },
                Int32Member = -197707092,
                Int32ArrayMember = new int[] { -1567484802, -1345549841, 690964254, 212151088, 415178559, 1885665941, -752512498, -2024758251, 767064353, -1801346807 },
                NullableInt32Member = -1767453204,
                NullableInt32ArrayMember = new Nullable<int>[] { -24184567, null, 788895747, null, -145120775, -620127002, 292629368, 1429561787, null, null },
                UInt32Member = 3975814421,
                UInt32ArrayMember = new uint[] { 2110628330, 476603850, 2522343048, 1458298045, 2196873204, 1762332113, 2006848960, 3722005671, 4009205883, 782140398 },
                NullableUInt32Member = 4204142217,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 4149443001, 3887708779, 3155757555, null, 136534748, null, null, null, 3313809078, 1912063346 },
                Int64Member = -266038720490995163,
                Int64ArrayMember = new long[] { 4046222526560997635, -5930148725356774439, 5506297485390163231, 6280539214592669522, -4735234584953169520, -4837146202333073895, -7226572463590918045, 6469496346282759979, -2056198288941765976, 7177889899967109788 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, 7050829286803803065, 1188541904285987741, -3390960676244538817, -5068726966994452903, -4435741950392109910, null, null, -2949239952976235509, 6201738797496295461 },
                UInt64Member = 7500446077689761408,
                UInt64ArrayMember = new ulong[] { 2156763731696438907, 13929552907982219326, 6551526271494748187, 15750506903192375852, 2354192892172386196, 2109307184441726314, 3121522030383240154, 10834064505163385481, 7831761402780680118, 7743290763928551121 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 11584085496214967610, null, null, 3881501872789660813, 4966829170719775006, 7096500544774703638, 5357545930346401505, 8783706914572103089, null, null },
                SingleMember = 0.5204F,
                SingleArrayMember = new float[] { 0.7283F, 0.1596F, 0.5766F, 0.2599F, 0.1424F, 0.7497F, 0.4202F, 0.7968F, 0.0983F, 0.5081F },
                NullableSingleMember = 0.3551F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.596F, null, 0.2971F, 0.9612F, 0.3526F, 0.15F, 0.9754F, 0.1826F, 0.7817F, 0.5464F },
                DoubleMember = 0.7906,
                DoubleArrayMember = new double[] { 0.7695, 0.0935, 0.6727, 0.4883, 0.5043, 0.2583, 0.6142, 0.144, 0.9663, 0.7673 },
                NullableDoubleMember = 0.5643,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.2248, 0.819, 0.1924, null, 0.9555, 0.5264, 0.7514, 0.7057, null, 0.4802 },
                DecimalMember = 0.708773421548851m,
                DecimalArrayMember = new decimal[] { 0.225557570161583m, 0.0934179951479619m, 0.886961593149073m, 0.174642779473143m, 0.686507579337318m, 0.614731909269549m, 0.083371437198766m, 0.0498987934212872m, 0.995465808067816m, 0.137964744825368m },
                NullableDecimalMember = 0.797929901705126m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.169765070665072m, 0.0394002388786429m, 0.246237239928451m, 0.316691014675641m, 0.0835653194609838m, 0.419960756359188m, null, 0.452648856257037m, 0.321585359424122m, 0.22315393691086m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(212360695),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-235753674), DateTime.Now.AddSeconds(58423235), DateTime.Now.AddSeconds(-25413999), DateTime.Now.AddSeconds(127762992), DateTime.Now.AddSeconds(-117115978), DateTime.Now.AddSeconds(18060554), DateTime.Now.AddSeconds(-211221507), DateTime.Now.AddSeconds(-337900917), DateTime.Now.AddSeconds(301840343), DateTime.Now.AddSeconds(102555297) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-280601109),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-20459988), DateTime.Now.AddSeconds(110929247), DateTime.Now.AddSeconds(-259182528), DateTime.Now.AddSeconds(-317884343), null, DateTime.Now.AddSeconds(295189847), DateTime.Now.AddSeconds(-136708464), null, DateTime.Now.AddSeconds(224929900), null },
            },
        };
    }
}
