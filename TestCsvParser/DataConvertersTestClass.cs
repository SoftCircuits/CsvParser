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
                StringMember = "wimhx,Y8#0",
                StringArrayMember = new string[] { "Td89PYQrav", "TF99'TmQ:U", "5GdbEZ54CC", "a@Hcb'.h$;", "uZg;T#Q X@", "2Oms%Gl0@8", "Pd7lAhaS#i", ":L6\"8y3REp", "\"%2?7P'DuX", ";Z8GuCe8qm" },
                CharMember = 'L',
                CharArrayMember = new char[] { 'K', 'w', 'A', 'i', ';', '?', ' ', '!', '%', ',' },
                NullableCharMember = 'S',
                NullableCharArrayMember = new Nullable<char>[] { 'r', 'o', 'T', '@', 'm', null, null, 'n', null, '?' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, true, true, true, true, true, true, false, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, false, true, null, null, true, true, null, false },
                ByteMember = 216,
                ByteArrayMember = new byte[] { 56, 212, 21, 214, 71, 107, 6, 23, 218, 83 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, null, null, 39, null, 142, 193, 137, 89, 82 },
                SByteMember = -123,
                SByteArrayMember = new sbyte[] { -56, 56, 36, -83, -96, 22, 79, 80, -96, 21 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -83, -114, -71, -21, 95, 52, 21, null, 120, null },
                Int16Member = 20089,
                Int16ArrayMember = new short[] { 23100, 16362, -15587, -5698, -8920, 28980, 529, -27989, 11976, 26728 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { null, 9240, null, null, 16471, -32693, -4257, 12237, -31411, null },
                UInt16Member = 60052,
                UInt16ArrayMember = new ushort[] { 20030, 40285, 43857, 8586, 63229, 27753, 5609, 23736, 33385, 15415 },
                NullableUInt16Member = 25928,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 10030, 53498, 46579, null, null, 7548, null, 52656, 29647, null },
                Int32Member = 1191742283,
                Int32ArrayMember = new int[] { -349279960, 1600906919, -1186136421, -548614870, 1391261534, -762653829, -266000207, 2136888880, -7322824, 1156387123 },
                NullableInt32Member = 2072799633,
                NullableInt32ArrayMember = new Nullable<int>[] { 2131673101, -1068950983, null, -1803602138, -738843560, 1520603157, -1466429307, -1474728699, null, 1098023447 },
                UInt32Member = 437492511,
                UInt32ArrayMember = new uint[] { 3221930596, 3316642256, 1407892043, 443833443, 2811676252, 2099874016, 3600180545, 328605676, 3279983455, 4087197343 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 336324560, null, 1933304962, 1189588609, 1903328163, 520826338, 2739136206, 965917528, 1551142380, 726656126 },
                Int64Member = -5523206225892224588,
                Int64ArrayMember = new long[] { 4895645604696191136, 334718077389747862, -2798715799862820303, 2604465711254997617, -2978123336452467104, 2377245715070989759, -480408976417905634, 2316632857823547726, -3024458956954603582, 108623778140748851 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, -7164344140954141034, -3394555254743634851, null, -1505365284828398453, null, null, 5573723521623377932, -6507418085868279484, -5707130111992325 },
                UInt64Member = 13348554080335204055,
                UInt64ArrayMember = new ulong[] { 15817747609580899003, 10749623207622723470, 15546306110832118492, 17131641435742204779, 1078528323918378665, 4503920013522535106, 5292684366820679337, 12179677644672781323, 13348375538437199992, 579500601610755183 },
                NullableUInt64Member = 15626939542704664343,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 14150124124050008085, 10905135719777519848, 3418936939929121339, 16417297327489944887, 17764941425407638835, 11740156879214163669, 6170232659227317393, 8593454777262357933, null, 17618894572264972268 },
                SingleMember = 0.9725F,
                SingleArrayMember = new float[] { 0.7846F, 0.6563F, 0.9599F, 0.6659F, 0.2497F, 0.0164F, 0.7929F, 0.1476F, 0.8536F, 0.5664F },
                NullableSingleMember = 0.6027F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.6436F, 0.3214F, 0.4956F, 0.4676F, null, null, 0.8922F, 0.3025F, 0.8663F, 0.791F },
                DoubleMember = 0.5796,
                DoubleArrayMember = new double[] { 0.4622, 0.335, 0.6849, 0.1833, 0.4372, 0.4133, 0.3083, 0.7897, 0.858, 0.7006 },
                NullableDoubleMember = 0.4703,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.554, null, 0.6178, 0.1992, 0.3918, 0.0299, 0.0939, 0.3122, 0.7278, 0.0389 },
                DecimalMember = 0.90580600070285m,
                DecimalArrayMember = new decimal[] { 0.498256495231553m, 0.703020249566075m, 0.969025551673578m, 0.792174636940358m, 0.523909639858265m, 0.909343970880669m, 0.0872702747059138m, 0.395910514046876m, 0.931097601192497m, 0.090968024264275m },
                NullableDecimalMember = 0.267285758411322m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.33682260369716m, 0.778782030768247m, 0.82446991871811m, null, null, 0.0686050583313988m, 0.0915139317320229m, null, null, 0.0564435570680754m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), null, null },
                DateTimeMember = DateTime.Now.AddSeconds(317300737),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-91055053), DateTime.Now.AddSeconds(246512010), DateTime.Now.AddSeconds(144163938), DateTime.Now.AddSeconds(61642860), DateTime.Now.AddSeconds(-39872114), DateTime.Now.AddSeconds(-81954949), DateTime.Now.AddSeconds(244845796), DateTime.Now.AddSeconds(342862714), DateTime.Now.AddSeconds(-210511656), DateTime.Now.AddSeconds(243475906) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(-4275575), DateTime.Now.AddSeconds(-232715508), DateTime.Now.AddSeconds(150295587), DateTime.Now.AddSeconds(-175654951), DateTime.Now.AddSeconds(135401577), DateTime.Now.AddSeconds(-185949551), DateTime.Now.AddSeconds(-232960076), null, DateTime.Now.AddSeconds(-196250015) },
            },
            new DataConvertersTestClass {
                StringMember = "u%dZC89WkH",
                StringArrayMember = new string[] { ";C37BX9Lcq", "$8vaa7e.@ ", "i!Aejud1ts", "M8PmK1DE@ ", "LF$AgTvzOG", "pp;F2ws2O%", "hdfUkq?TT%", "lwQ1Ke\"zbl", "JvHKqbDnm%", "fBIx#8%V6L" },
                CharMember = 'H',
                CharArrayMember = new char[] { 'T', 'M', 'V', '4', 'h', 'R', 'X', 'i', 'M', 'R' },
                NullableCharMember = ':',
                NullableCharArrayMember = new Nullable<char>[] { null, null, 'J', 'L', null, 'Q', 'J', null, null, 'X' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, false, false, true, true, true, false, true, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, true, true, false, null, null, true, true, null, null },
                ByteMember = 169,
                ByteArrayMember = new byte[] { 39, 235, 29, 166, 53, 51, 47, 246, 190, 22 },
                NullableByteMember = 249,
                NullableByteArrayMember = new Nullable<byte>[] { null, 155, 98, 27, null, 213, 42, 162, 148, 197 },
                SByteMember = -103,
                SByteArrayMember = new sbyte[] { 101, 70, 107, -80, -72, -18, -47, -84, 99, -2 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 52, 97, 86, 93, -40, -37, null, -57, -45, null },
                Int16Member = -22828,
                Int16ArrayMember = new short[] { 32448, -29203, -4231, -18784, 15412, 23145, -7133, 26633, -28890, -12394 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { 12534, -17091, null, null, 15189, 9353, null, -11602, -27290, -14454 },
                UInt16Member = 40067,
                UInt16ArrayMember = new ushort[] { 12071, 10740, 28408, 44678, 53412, 14804, 60022, 13309, 63162, 59459 },
                NullableUInt16Member = 50624,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, null, null, null, 23543, null, null, null, 58899, 44190 },
                Int32Member = -1184305143,
                Int32ArrayMember = new int[] { -1766154746, 1636787871, 1700256521, -1026322653, 721585777, 388480646, -2139615187, 1155834669, 2052913749, -1581107137 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { -137031923, -1166432931, 576651675, -949200069, -756128938, null, null, 1268927596, null, 206833357 },
                UInt32Member = 1118186781,
                UInt32ArrayMember = new uint[] { 2754076629, 2949311317, 4085884814, 3070871080, 3796673798, 1462747740, 1274762099, 3019742937, 1628611953, 1649541043 },
                NullableUInt32Member = 261755667,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2636696635, 1484719220, null, 256219870, 3092964599, 2142530268, 3323232224, 4223671128, 2600002974, null },
                Int64Member = 1363337998424704404,
                Int64ArrayMember = new long[] { -1037516197578391338, -3663053649894273906, -8050853685293748335, 3836679416351699354, 7030460188225176448, 1861700034782177654, 4996180766071427048, 591812806928045001, 8075603690510388662, 8674360363503446357 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, null, 5234508020302540216, -1070707674044470602, -9197520792355257724, 8201490889610096347, 6992822949826615640, -9156790251813042238, null, 5245756814947343656 },
                UInt64Member = 3800988367783674921,
                UInt64ArrayMember = new ulong[] { 11389782899064280704, 3716103075846465875, 10303150388718496840, 14661313927495399201, 1168382215148455858, 15803266156403205085, 16919384613906964685, 2304876594826063872, 7536917276820306400, 5124962562840180257 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 11587910378291252670, 10049553065763617438, null, 3175632749673183548, 2528872633041555489, 2739058549651676337, 9378094852505846322, null, 696916055898897678, null },
                SingleMember = 0.3547F,
                SingleArrayMember = new float[] { 0.4396F, 0.3631F, 0.4621F, 0.1214F, 0.9654F, 0.3038F, 0.9493F, 0.6302F, 0.9776F, 0.2012F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.2755F, 0.4113F, 0.4924F, 0.6655F, 0.0912F, 0.0113F, null, 0.5456F, 0.0605F, null },
                DoubleMember = 0.6132,
                DoubleArrayMember = new double[] { 0.2426, 0.0927, 0.7604, 0.9102, 0.197, 0.8107, 0.4141, 0.4818, 0.1366, 0.6792 },
                NullableDoubleMember = 0.2898,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.3226, 0.1176, null, 0.3381, 0.6378, 0.2941, 0.7827, 0.9664, 0.6641, null },
                DecimalMember = 0.966128901257054m,
                DecimalArrayMember = new decimal[] { 0.501621338796655m, 0.827048891288252m, 0.524699567890263m, 0.542378150605553m, 0.758357104131078m, 0.302964797780321m, 0.721081525043134m, 0.340276252869708m, 0.392541123465847m, 0.885870722202894m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, 0.277901887408042m, 0.539680093075706m, null, 0.830588173169795m, 0.942401419646425m, 0.664155563075228m, 0.672361689732238m, 0.577310967214798m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(340806267),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(265431240), DateTime.Now.AddSeconds(-38519048), DateTime.Now.AddSeconds(18560940), DateTime.Now.AddSeconds(-47270287), DateTime.Now.AddSeconds(-346011932), DateTime.Now.AddSeconds(227455230), DateTime.Now.AddSeconds(188861775), DateTime.Now.AddSeconds(158011541), DateTime.Now.AddSeconds(27405810), DateTime.Now.AddSeconds(-88658332) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-143562491),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(280556771), DateTime.Now.AddSeconds(-97699777), null, DateTime.Now.AddSeconds(-63367741), null, DateTime.Now.AddSeconds(318644918), DateTime.Now.AddSeconds(-274000299), DateTime.Now.AddSeconds(265953007), null, null },
            },
            new DataConvertersTestClass {
                StringMember = "xJ50xjCCTg",
                StringArrayMember = new string[] { "zRWopGUtU7", "jAmLmTo\"xx", "#Q 2Zj4koa", "!'9CmjO3iR", ",YH!'?FWvH", "IHj63U%yXm", "@hIyFH33?F", "pl6'2\"?C@@", "\"vN1iSL0JD", "gVIoi:.FeV" },
                CharMember = 'D',
                CharArrayMember = new char[] { 'X', 'Z', 'J', '5', '9', 'o', 'y', '5', 'X', '9' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { '?', 'T', null, null, '%', 'E', 'u', 'x', ',', null },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, false, false, false, false, true, false, false, false, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, true, false, null, null, true, null, null, true },
                ByteMember = 185,
                ByteArrayMember = new byte[] { 188, 232, 248, 59, 2, 230, 70, 215, 123, 94 },
                NullableByteMember = 236,
                NullableByteArrayMember = new Nullable<byte>[] { null, 228, 200, 224, 230, 212, 216, 31, 26, 241 },
                SByteMember = -75,
                SByteArrayMember = new sbyte[] { 118, 26, 63, 106, -18, 110, -121, -45, -80, -72 },
                NullableSByteMember = -81,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, 36, null, null, null, -35, null, 55, 14, 84 },
                Int16Member = 1898,
                Int16ArrayMember = new short[] { 17219, 13225, 31603, -32652, -9179, 21008, -5141, 16364, -7066, -10297 },
                NullableInt16Member = -10080,
                NullableInt16ArrayMember = new Nullable<short>[] { 22363, null, 6323, null, null, null, -21436, 8855, -4891, 7605 },
                UInt16Member = 41919,
                UInt16ArrayMember = new ushort[] { 24663, 15437, 58260, 19341, 33064, 15262, 2154, 26915, 57085, 34920 },
                NullableUInt16Member = 12006,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, null, 38927, 42071, 44791, null, 6983, null, 55052, 22702 },
                Int32Member = 826615613,
                Int32ArrayMember = new int[] { 1366818466, -1915256818, 259220293, -627277049, -547083996, -1964975161, 1173066184, 2046688181, -2128862934, -1154755096 },
                NullableInt32Member = -669123561,
                NullableInt32ArrayMember = new Nullable<int>[] { 1895496472, -1428002699, -525652680, null, 56941708, -413338467, -1986745007, null, -829473969, null },
                UInt32Member = 2189767620,
                UInt32ArrayMember = new uint[] { 69123107, 3996289141, 3637089771, 728462742, 1186675684, 3591131238, 2817609012, 4058358128, 2955496619, 3293970967 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, null, null, 1752002852, null, 2215625414, null, 2666392580, 95531327, 2457021157 },
                Int64Member = -7029681519455869121,
                Int64ArrayMember = new long[] { -2637595368690136968, -2157284531081899568, -4491403858271956413, 7903949642352806401, 6663757644155375965, 5518852705843727296, 4170755303714473552, 2054734391199140614, -1371748391933099224, 4218230257184532270 },
                NullableInt64Member = -2143983352897199922,
                NullableInt64ArrayMember = new Nullable<long>[] { 7563858675827447389, null, -3281509487561486847, 2053974572657818809, -480575878984169935, -7406468616155346729, -4677343289339162450, 5445133935057887325, 7429759875639446316, 8981686785887122771 },
                UInt64Member = 8761721614273241168,
                UInt64ArrayMember = new ulong[] { 17854845090524960257, 4208642859592269892, 4841463004663944922, 5543680916226384080, 2064320546681397277, 8783571087377594791, 3050984715810653704, 4924190009307398968, 14206921290417470840, 11968159627188037147 },
                NullableUInt64Member = 3764600693836934492,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 4757751372452214127, 16661249942280652864, 13511078413048465843, null, 1011345047864310317, 521195133126403859, 1129140151758780315, null, 6168006658628664409 },
                SingleMember = 0.57F,
                SingleArrayMember = new float[] { 0.9444F, 0.226F, 0.4002F, 0.7754F, 0.3679F, 0.2213F, 0.7062F, 0.5279F, 0.0757F, 0.3446F },
                NullableSingleMember = 0.8641F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.2939F, null, null, 0.8518F, 0.1979F, 0.727F, 0.6167F, 0.5405F, 0.0824F },
                DoubleMember = 0.4078,
                DoubleArrayMember = new double[] { 0.2514, 0.161, 0.0889, 0.2412, 0.0102, 0.0638, 0.776, 0.1083, 0.7349, 0.0484 },
                NullableDoubleMember = 0.0825,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.6282, 0.1233, 0.706, 0.6381, 0.8406, null, 0.7273, 0.884, 0.0682, 0.0326 },
                DecimalMember = 0.863029155661359m,
                DecimalArrayMember = new decimal[] { 0.530849768920662m, 0.149466118021293m, 0.6710700923993m, 0.0444469764916667m, 0.288625899476553m, 0.768973137045702m, 0.979640499785706m, 0.866906797299757m, 0.931444510757929m, 0.569548827388921m },
                NullableDecimalMember = 0.728858483746622m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.0281746783349135m, null, 0.519445260976387m, 0.386696628939593m, null, null, 0.0509193360667932m, 0.326629562891258m, 0.1451671260776m, 0.670191976724879m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-135201048),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-94609193), DateTime.Now.AddSeconds(-271685950), DateTime.Now.AddSeconds(-96566327), DateTime.Now.AddSeconds(-128986516), DateTime.Now.AddSeconds(171328166), DateTime.Now.AddSeconds(-199086465), DateTime.Now.AddSeconds(-49459185), DateTime.Now.AddSeconds(-118942337), DateTime.Now.AddSeconds(50017060), DateTime.Now.AddSeconds(213572884) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(286189289),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-273786765), null, DateTime.Now.AddSeconds(-38025009), DateTime.Now.AddSeconds(-91994264), DateTime.Now.AddSeconds(296081188), null, DateTime.Now.AddSeconds(-302666548), null, null, DateTime.Now.AddSeconds(-76932784) },
            },
            new DataConvertersTestClass {
                StringMember = "ODpAXGJWc#",
                StringArrayMember = new string[] { "D'1Z%nd$I;", "EvPR;rW09k", "D7'wYbQ1VF", "cN@@,omImB", "'Cx6SX75sW", "W@Y : ;oU6", "Q,NwC9037Y", "rTg5UaH#C%", "Q3 AU9tE;i", "8d m2S'24;" },
                CharMember = ',',
                CharArrayMember = new char[] { ',', 'u', 'd', 'z', 'q', 'O', 'L', '8', 'T', '9' },
                NullableCharMember = '6',
                NullableCharArrayMember = new Nullable<char>[] { null, 'X', null, 'P', null, null, 'I', '$', '7', 'w' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, true, true, false, true, true, true, false, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, true, true, false, false, null, false, true, null, true },
                ByteMember = 162,
                ByteArrayMember = new byte[] { 247, 68, 201, 47, 3, 85, 102, 203, 152, 178 },
                NullableByteMember = 85,
                NullableByteArrayMember = new Nullable<byte>[] { null, 243, null, 56, 222, 75, 172, 251, 236, 70 },
                SByteMember = 5,
                SByteArrayMember = new sbyte[] { 48, -39, 25, -87, -12, -101, -46, 124, -124, 101 },
                NullableSByteMember = -25,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -64, null, 118, -84, -44, 58, null, null, -86 },
                Int16Member = 23834,
                Int16ArrayMember = new short[] { 30043, -12286, 18362, -27383, 11604, -13880, -1826, 4954, 26088, 11792 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { 6690, null, -17784, 3552, -25244, 19514, -23849, 18348, null, -32105 },
                UInt16Member = 25615,
                UInt16ArrayMember = new ushort[] { 16564, 48013, 20507, 33516, 61927, 22121, 15589, 56150, 32843, 24213 },
                NullableUInt16Member = 18673,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 44302, 45973, 59850, 27020, 12080, 15121, 24999, 47888, 57037, null },
                Int32Member = -308886445,
                Int32ArrayMember = new int[] { 582256317, 1659642360, -701417692, -471952413, -1566813792, 635391835, -188776079, -1008770053, -1814268966, -1026584074 },
                NullableInt32Member = -637815121,
                NullableInt32ArrayMember = new Nullable<int>[] { 277487514, -1453511780, null, null, -2100239328, 1085990617, null, -1170169789, null, null },
                UInt32Member = 719204009,
                UInt32ArrayMember = new uint[] { 2895714186, 1655202408, 1317843470, 3745464580, 2379205590, 583373670, 1197083855, 724394440, 3193477297, 3062314426 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3175567336, 2063421627, 1927971016, null, 4027133597, 428393494, 3671802773, 4109567829, 617625719, 4240341368 },
                Int64Member = 7049297501124215502,
                Int64ArrayMember = new long[] { -1047557470866136858, 3344189897714893177, -1535480843643365487, 8620439186666335609, 3052661006081682439, 8210178361447471944, 8023474439499105905, -6687754539904080001, 6251666853473814380, -2828507428138587108 },
                NullableInt64Member = -3645943960052525451,
                NullableInt64ArrayMember = new Nullable<long>[] { -2190368329908496164, -5316160946232728067, 1431496774605857988, -818376135350854451, 5213418363471079817, 1747610820352289698, -8337591347107705786, null, 2926389481638482185, 9146917187270419396 },
                UInt64Member = 9001009367355705669,
                UInt64ArrayMember = new ulong[] { 12899117660354031164, 8271794245027163315, 2844055530178922014, 12180828167406000416, 5386800899809722590, 930626302571090314, 2579638465864578106, 14134052404145095568, 4716938558270287975, 13778144154966347727 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, null, 4614799296228086552, null, 3312954655476650852, null, 3385557778929364099, 14842107593339424597, null, 11526001780587065770 },
                SingleMember = 0.0078F,
                SingleArrayMember = new float[] { 0.1982F, 0.5902F, 0.5382F, 0.4577F, 0.7546F, 0.1139F, 0.714F, 0.0097F, 0.8027F, 0.0385F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.1967F, null, 0.7889F, 0.8885F, 0.8913F, 0.6875F, null, 0.8276F, 0.1863F, 0.3339F },
                DoubleMember = 0.8808,
                DoubleArrayMember = new double[] { 0.7964, 0.9123, 0.3431, 0.7519, 0.7172, 0.0544, 0.3496, 0.3248, 0.3126, 0.0246 },
                NullableDoubleMember = 0.5701,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.245, null, null, 0.4579, 0.5288, 0.4115, null, null, 0.2347, 0.232 },
                DecimalMember = 0.467640152476077m,
                DecimalArrayMember = new decimal[] { 0.425794750982306m, 0.695500971268087m, 0.388447230376465m, 0.047463752916827m, 0.476342636899295m, 0.117968185280029m, 0.175548800607148m, 0.750830661891044m, 0.617237934905212m, 0.8291101580723m },
                NullableDecimalMember = 0.42122234126925m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, null, 0.456211147619052m, 0.739677031876786m, 0.555375977650801m, 0.139064294318137m, 0.535845710016987m, 0.546478476706236m, 0.749258045668942m, 0.804580431460493m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(125581196),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-151833654), DateTime.Now.AddSeconds(232998845), DateTime.Now.AddSeconds(51505608), DateTime.Now.AddSeconds(172609770), DateTime.Now.AddSeconds(-22685075), DateTime.Now.AddSeconds(184026709), DateTime.Now.AddSeconds(339814400), DateTime.Now.AddSeconds(-308708034), DateTime.Now.AddSeconds(176452443), DateTime.Now.AddSeconds(286766477) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-138598603),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-249610263), DateTime.Now.AddSeconds(-20967098), null, null, DateTime.Now.AddSeconds(-324999953), null, null, DateTime.Now.AddSeconds(-69424973), null, DateTime.Now.AddSeconds(143574190) },
            },
            new DataConvertersTestClass {
                StringMember = "1rD8zGd$iB",
                StringArrayMember = new string[] { "Xl?gkjJu63", "61KW8;;tRZ", "35VbLoU8HL", "@Pu8WN19.0", "1#WHM1LA'd", "Q8oIDNkGi#", "nh0Lc##;@F", ".lMT.YKU,;", "\"@;m0Wf$02", "XXQyZSHMPK" },
                CharMember = 'P',
                CharArrayMember = new char[] { 'B', 'j', 'y', '"', 'a', 'F', ':', 'h', 'C', 'r' },
                NullableCharMember = '#',
                NullableCharArrayMember = new Nullable<char>[] { null, null, null, 'v', 'b', '5', 'A', '1', 's', 'E' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, false, true, false, true, true, true, false, true, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, null, null, null, false, null, false, null, false, true },
                ByteMember = 173,
                ByteArrayMember = new byte[] { 113, 88, 113, 50, 150, 234, 23, 181, 32, 183 },
                NullableByteMember = 98,
                NullableByteArrayMember = new Nullable<byte>[] { 10, 119, 147, 186, null, null, 236, 103, 5, null },
                SByteMember = -14,
                SByteArrayMember = new sbyte[] { -26, -126, -77, -102, 35, 10, -80, -77, -44, -53 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -89, 34, null, -89, null, 12, null, null, null },
                Int16Member = 26238,
                Int16ArrayMember = new short[] { 2757, -31715, -15662, -25923, -27756, 29657, -18191, 6236, -9337, -1499 },
                NullableInt16Member = -1664,
                NullableInt16ArrayMember = new Nullable<short>[] { null, -25715, null, -12597, 21588, null, -2420, 15899, 16497, 31362 },
                UInt16Member = 51387,
                UInt16ArrayMember = new ushort[] { 26828, 50340, 33645, 4058, 55128, 62265, 2419, 21267, 1055, 33448 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 12563, 22969, 23680, 35124, 55311, 18592, 39174, 885, null, 8043 },
                Int32Member = 190161897,
                Int32ArrayMember = new int[] { -367492047, 905900793, 679299732, 1722668687, 1734535842, -1900766285, 520971130, -339210618, -1560666550, 1723719902 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { -303326274, 841521200, null, -471693038, -1161702417, null, null, 2101161279, null, -1331357775 },
                UInt32Member = 1734785226,
                UInt32ArrayMember = new uint[] { 2633890918, 3691611666, 1745876330, 2042112065, 1139964069, 2406866803, 3157803488, 348292317, 669960663, 683670596 },
                NullableUInt32Member = 1181665657,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 303215719, 3787782433, 946175520, 3065175950, null, null, 172267153, 961984432, 1733813388, 3330428398 },
                Int64Member = -4028538315286049013,
                Int64ArrayMember = new long[] { -4094614844165853602, 4013342957755261884, -2906285486975708648, -216768689316965550, -5174919228153655514, -7557690154660157826, -8291763872306654881, -2504662110843299911, 6240695404513470391, 3019426050624114421 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -3506363181841256973, -7839935827687006940, null, -2401105819524905942, -5627270351576448733, 7611460701734648723, -474796352484623025, 5315255625383983857, -1680750932286413954, null },
                UInt64Member = 3691517787677534551,
                UInt64ArrayMember = new ulong[] { 12516257498569510946, 804492169638399444, 13286443779929089446, 9786134375684900072, 5219409475203057063, 9541782841884351243, 5200128082885243126, 3774223823676137381, 8394612882864603826, 12057460251063155924 },
                NullableUInt64Member = 11170000147573352523,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 4038584832057872459, null, 6639169128132420416, null, 3867590262520398006, 6463126088156863210, null, 12984202713665094935, null, 12762668211605997030 },
                SingleMember = 0.1192F,
                SingleArrayMember = new float[] { 0.9225F, 0.1913F, 0.1952F, 0.8501F, 0.3337F, 0.2917F, 0.1249F, 0.9567F, 0.1408F, 0.1306F },
                NullableSingleMember = 0.0095F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.2674F, 0.6168F, 0.5354F, null, 0.0757F, null, 0.0168F, 0.7201F, 0.1085F },
                DoubleMember = 0.1206,
                DoubleArrayMember = new double[] { 0.1452, 0.369, 0.6079, 0.3716, 0.8746, 0.4041, 0.6369, 0.162, 0.461, 0.0934 },
                NullableDoubleMember = 0.3963,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.1478, 0.0058, 0.0005, null, null, 0.9178, 0.6982, null, 0.8186, 0.4995 },
                DecimalMember = 0.89340758109113m,
                DecimalArrayMember = new decimal[] { 0.981241404282218m, 0.642408577522404m, 0.988567778039564m, 0.40816307903347m, 0.488613365621246m, 0.362361541972723m, 0.193947994812847m, 0.653431349871972m, 0.00497502866240496m, 0.119140522020091m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.276023628397343m, 0.675619455196542m, 0.333517784859386m, null, null, null, 0.898872072273407m, 0.20820807570486m, 0.254073720392821m, 0.805562446607236m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-241546192),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-248978332), DateTime.Now.AddSeconds(-142404945), DateTime.Now.AddSeconds(111299893), DateTime.Now.AddSeconds(-27153259), DateTime.Now.AddSeconds(-78177926), DateTime.Now.AddSeconds(234005907), DateTime.Now.AddSeconds(-64681519), DateTime.Now.AddSeconds(258212658), DateTime.Now.AddSeconds(-242204748), DateTime.Now.AddSeconds(126114385) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(24112616), DateTime.Now.AddSeconds(54790302), DateTime.Now.AddSeconds(138122437), null, DateTime.Now.AddSeconds(-293387357), DateTime.Now.AddSeconds(-165654330), DateTime.Now.AddSeconds(62178435), DateTime.Now.AddSeconds(143839096), null, DateTime.Now.AddSeconds(247533970) },
            },
            new DataConvertersTestClass {
                StringMember = "RDwb9#\"Ayh",
                StringArrayMember = new string[] { "NRkmSL8q2$", "xt 7isQfpv", "Fw;v2Ii6D ", "CsUr5nX:K\"", ", !NQXWtQ#", "9\"D8%dm1qz", "sKriugHGe3", "QeyIZvV7iI", "%Kh'9C7gK3", "?KHPq#xfHE" },
                CharMember = 'h',
                CharArrayMember = new char[] { 'H', 'v', 'q', ' ', 'E', '3', 'b', 'w', 'x', 'P' },
                NullableCharMember = 'X',
                NullableCharArrayMember = new Nullable<char>[] { null, null, null, null, '9', null, 'x', null, '.', 'd' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, false, false, false, false, false, false, true, false, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, null, true, null, null, null, null, true, true, null },
                ByteMember = 192,
                ByteArrayMember = new byte[] { 148, 30, 239, 62, 80, 55, 146, 79, 51, 188 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 4, 56, 156, 18, 97, null, 218, 182, 77, null },
                SByteMember = -20,
                SByteArrayMember = new sbyte[] { -19, -65, 83, -49, -94, -101, -91, -121, -37, 91 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 79, 5, null, 119, -106, 99, null, 124, 91, -32 },
                Int16Member = -24807,
                Int16ArrayMember = new short[] { 30189, -19901, 17783, -27221, -28311, -32046, 31819, -7277, 937, -3896 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { null, null, -21164, 9218, 11020, -20004, -22830, null, null, 16108 },
                UInt16Member = 14535,
                UInt16ArrayMember = new ushort[] { 8865, 8896, 4494, 3165, 14547, 52863, 10, 5710, 16399, 15026 },
                NullableUInt16Member = 58913,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 59200, null, 59234, 39484, 63161, null, null, 16145, null, 22619 },
                Int32Member = -103594360,
                Int32ArrayMember = new int[] { 1878937371, 557067146, 2065235120, 336453580, 665589381, 1037154651, -187967945, 590074360, 831555647, 1159286650 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { null, -1355641508, -1582486240, null, -1429724488, -851934800, -2109952823, -1217175875, -145243059, 221527445 },
                UInt32Member = 1234024936,
                UInt32ArrayMember = new uint[] { 665439568, 289818532, 10720435, 3219733971, 3681200368, 1347187916, 1880085681, 4079473043, 3151441251, 2104370155 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3964167157, 59303104, 2899004965, null, 3636312542, 3428221795, null, 3847269872, null, 438596805 },
                Int64Member = -7498923049119499878,
                Int64ArrayMember = new long[] { -9156226484284873281, 7652885969263305217, -872477773182852056, 4889820361885008271, -4825253910520719319, -6833399021128562157, -3926997758235740790, -2608275871171802374, -3083790749414537314, -7229826430265613903 },
                NullableInt64Member = -5784984193062020960,
                NullableInt64ArrayMember = new Nullable<long>[] { -8206045501073454866, -3204987940436199589, null, null, 6353686912190087848, -7961820275602612089, null, -8438264176078092521, 7557666849604121963, null },
                UInt64Member = 17970818079752086887,
                UInt64ArrayMember = new ulong[] { 14352882933370074213, 21682244066986268, 9954279300949010189, 13197541482012939655, 15411242615926433892, 10445137499432787931, 2089705340423068202, 16475095329341298391, 10599984345965602273, 11486155156959267957 },
                NullableUInt64Member = 17682847020682311379,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, null, 6269967989812337815, 13563408059305945007, 17569087908130158466, null, null, null, null, null },
                SingleMember = 0.1638F,
                SingleArrayMember = new float[] { 0.8887F, 0.6786F, 0.6958F, 0.6425F, 0.7477F, 0.988F, 0.4963F, 0.9681F, 0.5752F, 0.2624F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.7948F, 0.2461F, 0.0945F, 0.4033F, 0.065F, null, 0.8857F, 0.668F, null, null },
                DoubleMember = 0.3734,
                DoubleArrayMember = new double[] { 0.808, 0.1147, 0.4664, 0.8827, 0.3225, 0.9577, 0.0572, 0.266, 0.5306, 0.0969 },
                NullableDoubleMember = 0.4234,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.1745, 0.084, 0.6844, null, 0.016, 0.992, 0.5377, 0.4473, 0.1458, null },
                DecimalMember = 0.0664479379392032m,
                DecimalArrayMember = new decimal[] { 0.751940860685563m, 0.101827351372345m, 0.40960927860715m, 0.0340979031936647m, 0.0746896334284096m, 0.823586868354625m, 0.896076955444881m, 0.0212396731160345m, 0.175293722429581m, 0.665596963495143m },
                NullableDecimalMember = 0.735043458949125m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.944076291393337m, 0.512136725254864m, 0.786953465563465m, 0.547153439840124m, null, null, null, 0.484899521780225m, 0.55805378082052m, 0.105230016150494m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(76397146),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(238639544), DateTime.Now.AddSeconds(-192173266), DateTime.Now.AddSeconds(-33071719), DateTime.Now.AddSeconds(301692247), DateTime.Now.AddSeconds(99547356), DateTime.Now.AddSeconds(302570328), DateTime.Now.AddSeconds(232899605), DateTime.Now.AddSeconds(281978913), DateTime.Now.AddSeconds(113807161), DateTime.Now.AddSeconds(237603657) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(219008036), DateTime.Now.AddSeconds(-171076833), DateTime.Now.AddSeconds(37754138), DateTime.Now.AddSeconds(249996646), null, DateTime.Now.AddSeconds(-250129958), null, null, DateTime.Now.AddSeconds(58492215) },
            },
            new DataConvertersTestClass {
                StringMember = "pJ6YPAH#J6",
                StringArrayMember = new string[] { "Kim@yzzvta", "#vh,W9RRsk", "fbB:%IaloW", "@vl:LDb9'f", "ZpJ1ocNCn8", "iph%ej8aLh", "%Cl?Nv\"de4", "OGuQh9vvw4", "..'nylI,k9", "\"v;Yl''w;T" },
                CharMember = ';',
                CharArrayMember = new char[] { 'f', 'e', 'B', 'l', 'B', ';', 'm', 'R', '4', '?' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, 'h', null, null, null, 'M', null, '5', 'Q', ':' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, true, true, true, false, true, true, true, false, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, null, null, null, false, false, false, false, null, true },
                ByteMember = 122,
                ByteArrayMember = new byte[] { 97, 129, 86, 77, 51, 195, 47, 212, 173, 48 },
                NullableByteMember = 72,
                NullableByteArrayMember = new Nullable<byte>[] { 223, 80, 123, null, 131, 199, null, null, 29, 129 },
                SByteMember = -51,
                SByteArrayMember = new sbyte[] { -46, -22, -34, -60, -93, -20, 70, 96, -14, 8 },
                NullableSByteMember = 49,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -94, null, -49, -35, -69, null, null, null, -86 },
                Int16Member = -16436,
                Int16ArrayMember = new short[] { 18118, -6920, 18923, 31806, 71, -32056, -7695, 12137, 30112, 27233 },
                NullableInt16Member = -21750,
                NullableInt16ArrayMember = new Nullable<short>[] { -15552, -26671, 12061, -29064, null, 28828, null, 29813, null, 10086 },
                UInt16Member = 48219,
                UInt16ArrayMember = new ushort[] { 30932, 22934, 8406, 17997, 40960, 2092, 3213, 31372, 23924, 37403 },
                NullableUInt16Member = 49975,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 49847, 6088, 14436, null, 1949, 5736, 52305, null, 56190 },
                Int32Member = -1508285963,
                Int32ArrayMember = new int[] { 5189517, 682832690, 163538384, -570546587, 790156242, 2023282921, 818885783, -629472837, -1782954391, -907612182 },
                NullableInt32Member = -1129998196,
                NullableInt32ArrayMember = new Nullable<int>[] { null, -1777456499, null, -420775771, null, -147749682, 1023295079, -83320451, -1188188080, 2092377039 },
                UInt32Member = 2676530203,
                UInt32ArrayMember = new uint[] { 3520971486, 2912882513, 2581348112, 4220405408, 1447719417, 3616592402, 3035654573, 3541344138, 4214393910, 696425918 },
                NullableUInt32Member = 3994131478,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 990058638, 3735148616, null, 1038852106, null, null, null, null, 982720625, null },
                Int64Member = 6754162421283736921,
                Int64ArrayMember = new long[] { -2077520083788237563, -5751663416185179262, 6378856048737772286, 3988857923781147753, -8985477561652095860, 3004665863737728964, -5163153313178364003, -1357457682385695233, -4703419550547985592, 6620503306216930238 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, -6627169385701645799, -5272442663333070555, 7997778127536049132, -6647918967275340981, null, -8254593496196146639, null, 297351489531506900, null },
                UInt64Member = 17759463914682479526,
                UInt64ArrayMember = new ulong[] { 15522369379030505547, 11023733548354575178, 2050437399495797376, 11802390526460194121, 1468158124393225577, 261128643732912580, 1780572050188840926, 11506536217999143750, 6415887190454884921, 801502364772932635 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 18218737630920071901, 6441030035690010731, null, 807452202100978398, null, null, null, null, null },
                SingleMember = 0.3378F,
                SingleArrayMember = new float[] { 0.0534F, 0.2594F, 0.2334F, 0.3932F, 0.5229F, 0.7256F, 0.8655F, 0.9286F, 0.4831F, 0.0465F },
                NullableSingleMember = 0.984F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.292F, 0.1938F, null, null, 0.763F, null, null, 0.772F, null, null },
                DoubleMember = 0.5679,
                DoubleArrayMember = new double[] { 0.2362, 0.9553, 0.4323, 0.5018, 0.0252, 0.1099, 0.8315, 0.1848, 0.2513, 0.5369 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { null, null, 0.1192, 0.3503, null, 0.0632, null, 0.5372, null, 0.135 },
                DecimalMember = 0.181987602499154m,
                DecimalArrayMember = new decimal[] { 0.665305969428857m, 0.35320151368601m, 0.613011869693485m, 0.776343640889567m, 0.73827579379048m, 0.286453914666812m, 0.983447654527699m, 0.129440487523547m, 0.758712302380172m, 0.118882549329958m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.595348006858445m, 0.642058322716965m, 0.982990163965623m, null, null, 0.51781389854242m, 0.987319736109789m, 0.798360064826609m, 0.91178489302389m, 0.407688519169592m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(232447071),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(274731693), DateTime.Now.AddSeconds(110140895), DateTime.Now.AddSeconds(-26820227), DateTime.Now.AddSeconds(-20310457), DateTime.Now.AddSeconds(260245196), DateTime.Now.AddSeconds(-126737417), DateTime.Now.AddSeconds(303980765), DateTime.Now.AddSeconds(74698124), DateTime.Now.AddSeconds(-251539197), DateTime.Now.AddSeconds(-348246423) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(18084352),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-145754901), DateTime.Now.AddSeconds(273181752), null, DateTime.Now.AddSeconds(25624353), DateTime.Now.AddSeconds(-176284828), DateTime.Now.AddSeconds(11050906), DateTime.Now.AddSeconds(-342164868), DateTime.Now.AddSeconds(-141271019), null, null },
            },
            new DataConvertersTestClass {
                StringMember = "i9,rWR';T6",
                StringArrayMember = new string[] { "Kem@@E@BFy", "xoxTnKN:l8", "lpRGz9fR3.", "#o1QHMvf4:", "H PpBgTt;j", ".OAF#%#SfO", "0ZI;WG$R4b", "p'w,r84Gsv", "eYAWYULMmH", "p$S;SXX,R0" },
                CharMember = 'A',
                CharArrayMember = new char[] { 'J', 'z', 'B', '2', 'J', '9', 'b', 'X', ':', 'J' },
                NullableCharMember = 'R',
                NullableCharArrayMember = new Nullable<char>[] { null, '9', 'h', null, null, 'a', '#', 'p', 'J', null },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, false, true, true, true, false, true, true, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, null, false, false, true, false, false, false, null, null },
                ByteMember = 178,
                ByteArrayMember = new byte[] { 134, 203, 228, 193, 13, 250, 216, 143, 159, 11 },
                NullableByteMember = 168,
                NullableByteArrayMember = new Nullable<byte>[] { 239, 172, 7, 209, 253, null, null, null, null, 70 },
                SByteMember = 43,
                SByteArrayMember = new sbyte[] { 36, -19, -45, -123, -43, 18, 62, 63, 49, 38 },
                NullableSByteMember = 72,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -6, -13, -104, 56, 28, 60, 4, 103, -98 },
                Int16Member = 27841,
                Int16ArrayMember = new short[] { 25073, 24486, -2811, 6397, -32406, -23695, -18326, 27690, -27925, -24791 },
                NullableInt16Member = -25450,
                NullableInt16ArrayMember = new Nullable<short>[] { -21553, 7916, 4484, -16743, -6337, -17297, -18610, null, null, 1870 },
                UInt16Member = 62643,
                UInt16ArrayMember = new ushort[] { 57211, 24090, 48423, 6603, 15780, 10516, 36231, 16028, 44933, 33002 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 2403, null, 12705, 61212, 14673, null, null, 6843, 29584, 57990 },
                Int32Member = 1848991011,
                Int32ArrayMember = new int[] { 619398295, -711371046, 1996863758, -755261992, 867289692, 1622646051, 729218793, 1503562482, -587048904, 1475352631 },
                NullableInt32Member = 494136668,
                NullableInt32ArrayMember = new Nullable<int>[] { 2017606575, -1777689296, null, 1346656240, -1526664679, 1618169609, null, -1180517119, null, null },
                UInt32Member = 2780180554,
                UInt32ArrayMember = new uint[] { 1081747359, 2042124755, 365560619, 1002458404, 2109416685, 3332832651, 2495491636, 2431282220, 2845023071, 332253502 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 700928502, null, 223583137, 128853881, 317383943, 536537894, 1466479896, null, 4102217222, 747201764 },
                Int64Member = -722797412501390871,
                Int64ArrayMember = new long[] { -8390338369922501605, -2893927884673962947, 1399716295705494752, 6143512802701967235, -7279365484974701512, 4655612851201341284, -4575204600476816309, -710305243008640088, -4586096476827441336, 666202938191764453 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { 4247823796984342704, 7012362886610076943, 7148642529594684046, 5156012981836800420, -4771780183783705854, null, -8151365218841513325, 1212537170074261443, null, -874437944464090388 },
                UInt64Member = 1335130591113044340,
                UInt64ArrayMember = new ulong[] { 13650702986588616652, 523113840858112456, 15291178903115488977, 10831481159727832388, 4270952007209642112, 10653146640378051875, 18121517232143378298, 11096761061808944978, 11421511315371905666, 14793624693668704657 },
                NullableUInt64Member = 1529742789223093895,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 4261239587491667908, 6461688823932293384, 17348660254239344740, null, 663645259333193571, null, 9028320567297280864, null, null },
                SingleMember = 0.527F,
                SingleArrayMember = new float[] { 0.1913F, 0.2081F, 0.6003F, 0.898F, 0.2477F, 0.8888F, 0.8925F, 0.8465F, 0.1345F, 0.8845F },
                NullableSingleMember = 0.208F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.879F, 0.9715F, 0.5142F, null, 0.4035F, 0.7719F, 0.6787F, null, 0.0634F, 0.3456F },
                DoubleMember = 0.4647,
                DoubleArrayMember = new double[] { 0.6494, 0.7898, 0.6242, 0.0408, 0.8067, 0.1907, 0.0743, 0.5525, 0.8199, 0.618 },
                NullableDoubleMember = 0.3937,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.7436, 0.8715, null, null, 0.9341, 0.4636, null, 0.2648, 0.4951 },
                DecimalMember = 0.208280947579023m,
                DecimalArrayMember = new decimal[] { 0.510613929029689m, 0.680818512519814m, 0.974382262364711m, 0.84734403931151m, 0.885261923273756m, 0.282029151011542m, 0.876292718542155m, 0.871720450080264m, 0.779421127273982m, 0.530753658970262m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.741444820677482m, null, 0.791568789225016m, null, 0.891864218443149m, 0.530081384643279m, 0.877091437764768m, null, null, 0.607198856529264m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(231513852),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(251370849), DateTime.Now.AddSeconds(56871344), DateTime.Now.AddSeconds(270139649), DateTime.Now.AddSeconds(-281565970), DateTime.Now.AddSeconds(-243927495), DateTime.Now.AddSeconds(32026785), DateTime.Now.AddSeconds(151389468), DateTime.Now.AddSeconds(-134766593), DateTime.Now.AddSeconds(332142068), DateTime.Now.AddSeconds(-172633157) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(189718287),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, null, DateTime.Now.AddSeconds(-292406172), DateTime.Now.AddSeconds(320325270), DateTime.Now.AddSeconds(-333147037), DateTime.Now.AddSeconds(-159585357), DateTime.Now.AddSeconds(-6659023), DateTime.Now.AddSeconds(-236275337), null, DateTime.Now.AddSeconds(-217403800) },
            },
            new DataConvertersTestClass {
                StringMember = "np$YO\"CTtP",
                StringArrayMember = new string[] { "!d!#e2OuwU", "WO uv!2WlA", "vLjYE0F%5!", "8.KvTMgQJW", "dDk%.5a$Pn", "Q.185s,6kv", "SZqa06%jtr", "!JjHEN69Z ", "s GK#vPPFj", "Ikh8f?I7rT" },
                CharMember = '"',
                CharArrayMember = new char[] { 'u', 'm', 'M', 'b', 'W', '#', 'E', 'u', '7', '.' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { '?', 'I', null, null, 'I', null, '"', null, 'R', null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, false, true, true, true, true, false, true, false, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, true, null, null, true, false, null, false, false, null },
                ByteMember = 61,
                ByteArrayMember = new byte[] { 20, 58, 127, 94, 206, 227, 162, 127, 112, 210 },
                NullableByteMember = 75,
                NullableByteArrayMember = new Nullable<byte>[] { null, 66, null, 188, null, 72, 226, 82, null, 52 },
                SByteMember = -57,
                SByteArrayMember = new sbyte[] { 87, -89, -10, -8, 78, 93, -6, -36, -30, 36 },
                NullableSByteMember = -88,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, 43, null, -13, null, 115, null, null, -55, -128 },
                Int16Member = 32039,
                Int16ArrayMember = new short[] { -5886, -2581, -9240, 24643, 4799, -11478, 17938, 14550, 8265, -28077 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -8110, null, -2925, 19660, null, -23898, null, 4623, null, -25083 },
                UInt16Member = 15699,
                UInt16ArrayMember = new ushort[] { 27082, 55062, 50168, 27331, 21377, 58093, 49597, 52469, 41333, 23163 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 16220, null, null, 23240, 3555, 7814, 43703, 12957, 54903, 48204 },
                Int32Member = -497866718,
                Int32ArrayMember = new int[] { 1918432825, 133812509, -725071768, 2012496769, -87637412, 953172344, 1935614232, 187918948, -1700314528, -370403877 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { null, null, 1648328010, null, 562157433, 843677595, 589541225, null, -513369448, null },
                UInt32Member = 2210222857,
                UInt32ArrayMember = new uint[] { 887281517, 931661166, 851164245, 2961141179, 201070938, 3656791807, 1725739490, 1687641800, 1126502926, 3148328796 },
                NullableUInt32Member = 210657938,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3822285362, 2832390820, 1071388241, 3204774384, null, 633604122, 464360066, 1188948853, 816334847, null },
                Int64Member = 5399008854889883460,
                Int64ArrayMember = new long[] { -2982437989901760709, -9044268747440749768, 6805018040660604861, -8918271082423783247, 3465272581221205343, -7730200970492731635, -3274101007963124730, -797679522130964282, -2207450325148966119, -8876433415994020629 },
                NullableInt64Member = 4453049175554843890,
                NullableInt64ArrayMember = new Nullable<long>[] { -6026682850000105257, -3341205807708850259, null, null, null, 5147154637536630123, null, null, 4351869725211698868, 1617592477976339270 },
                UInt64Member = 10579359851560871451,
                UInt64ArrayMember = new ulong[] { 518585836014074850, 6973970120645015552, 8305847538597549693, 11238181438173004990, 5591988556899350353, 12990013898897785086, 13499653022762478188, 3249273941735749635, 5062196537476074727, 2211057724782022680 },
                NullableUInt64Member = 17428908336998514677,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 12627892732657866830, 5978957080117882094, 5266990620337077868, 16839614162851212951, 12839765614268576802, 6056924408437295396, 12950207966351528257, 4526543451150479685, 14036903351209143164 },
                SingleMember = 0.3092F,
                SingleArrayMember = new float[] { 0.569F, 0.9401F, 0.136F, 0.1519F, 0.4236F, 0.1828F, 0.5227F, 0.3722F, 0.1312F, 0.9862F },
                NullableSingleMember = 0.1893F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.6144F, 0.0561F, null, null, 0.6302F, 0.2227F, null, null, 0.618F, null },
                DoubleMember = 0.2198,
                DoubleArrayMember = new double[] { 0.4079, 0.9385, 0.8109, 0.3204, 0.4401, 0.3936, 0.7087, 0.1741, 0.6948, 0.1647 },
                NullableDoubleMember = 0.2531,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.4716, null, null, 0.8185, null, 0.5795, null, null, null, 0.7165 },
                DecimalMember = 0.158180101801865m,
                DecimalArrayMember = new decimal[] { 0.997453621381561m, 0.442193267785236m, 0.498988963353m, 0.550864277311169m, 0.10428695797468m, 0.346653475526944m, 0.0290265883162429m, 0.409289783454969m, 0.635532166664519m, 0.864163152044995m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.553138412883482m, 0.814388093547676m, null, null, 0.360253577128288m, 0.269169786944476m, null, 0.94830450054848m, null, 0.369735199554405m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(52710587),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(177305017), DateTime.Now.AddSeconds(270030604), DateTime.Now.AddSeconds(-143654579), DateTime.Now.AddSeconds(284686188), DateTime.Now.AddSeconds(251366376), DateTime.Now.AddSeconds(318991461), DateTime.Now.AddSeconds(-178189192), DateTime.Now.AddSeconds(-70619986), DateTime.Now.AddSeconds(-199605556), DateTime.Now.AddSeconds(-131125533) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(126883169),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(234770781), DateTime.Now.AddSeconds(-205975936), DateTime.Now.AddSeconds(-273245142), null, DateTime.Now.AddSeconds(298931947), null, null, DateTime.Now.AddSeconds(-130518859), DateTime.Now.AddSeconds(-20923011), null },
            },
            new DataConvertersTestClass {
                StringMember = "ecw0Z!Iw9k",
                StringArrayMember = new string[] { "9CJkwonG x", ".bHRQlC2Wx", ";nF't.gP7r", "0B@l\"mABmg", "c6pDpOYToU", "ZK8TKQ#DlQ", "P' CEafjFK", "'f!:Eyym0J", "uTR1fZ\"VXz", "uAJ;oD3mKY" },
                CharMember = ';',
                CharArrayMember = new char[] { 'c', 'J', 'z', '?', 'J', 'Q', 'N', 'J', 'G', 'X' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, ' ', '%', null, ';', null, ' ', 'M', null, null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, true, true, false, false, false, true, false, false, false },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, true, null, false, true, false, false, null, false, false },
                ByteMember = 151,
                ByteArrayMember = new byte[] { 101, 184, 181, 183, 60, 169, 147, 21, 20, 228 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 89, 74, 4, 219, 123, 3, null, null, 11, 24 },
                SByteMember = 69,
                SByteArrayMember = new sbyte[] { 86, 90, 118, 66, 46, 42, 43, -126, -13, 68 },
                NullableSByteMember = 31,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, null, -61, 108, 58, null, 90, null, null, null },
                Int16Member = 17349,
                Int16ArrayMember = new short[] { -21108, -21556, 27309, -31253, -25671, 31922, 24097, -9744, 9359, 16522 },
                NullableInt16Member = -11513,
                NullableInt16ArrayMember = new Nullable<short>[] { null, 31639, -7596, -6231, -25166, null, -15397, null, 20879, 10582 },
                UInt16Member = 51401,
                UInt16ArrayMember = new ushort[] { 10457, 52268, 39759, 21588, 17299, 18502, 25365, 2036, 1819, 49601 },
                NullableUInt16Member = 44493,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 10059, 46044, 14712, null, 58231, null, 14384, null, 13707, null },
                Int32Member = 1250006888,
                Int32ArrayMember = new int[] { -2059405196, -369999860, 946101697, -363268762, 420495395, -960908490, -2044398143, 905662396, -717260494, -112913154 },
                NullableInt32Member = 1527853407,
                NullableInt32ArrayMember = new Nullable<int>[] { 625785312, -234132641, 927980242, -1284159031, 2114253770, null, -181870821, -313706253, null, -1791204603 },
                UInt32Member = 1213861086,
                UInt32ArrayMember = new uint[] { 1694777099, 3208381488, 779929192, 2743716734, 316764641, 2018731130, 1930598328, 2420279346, 2325306851, 4192119480 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 243684526, 1975080045, 898857328, null, null, null, null, null, 1478903560 },
                Int64Member = -6820771926724123650,
                Int64ArrayMember = new long[] { -7730433115215693742, 2720156459762249213, 4510349819394774531, 2286751551304352210, 2169030377737208385, -1209220804442351456, 1492455002620608327, 3668186088230755023, 8427737690026804553, 7130280892870560382 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { 4227960080403899645, -2209840321948835300, 1107588982737607273, 1761484757649228749, null, -6906137253277934116, null, 7985221798275595777, -5958032146826733689, 551332766436687580 },
                UInt64Member = 18021007327846591220,
                UInt64ArrayMember = new ulong[] { 9364015382548458887, 1757138099954640246, 15595312550851387378, 1153602229320897734, 7647218266787525775, 6608159397841614833, 8767836777012725400, 14323154235625912082, 723238450659427455, 8419068894449308578 },
                NullableUInt64Member = 493322240192895804,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 1727296210092392054, 7908474893590164631, 14225409768124128640, 6579748794068812165, 11751472387918232603, 1291896069352820179, null, 9552200639057998875, 12212517752500448158 },
                SingleMember = 0.3428F,
                SingleArrayMember = new float[] { 0.8455F, 0.1698F, 0.9816F, 0.6482F, 0.3456F, 0.8027F, 0.6901F, 0.0181F, 0.7111F, 0.0459F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.5817F, null, 0.085F, null, null, 0.9595F, null, null, 0.0812F, null },
                DoubleMember = 0.9827,
                DoubleArrayMember = new double[] { 0.7072, 0.6584, 0.3559, 0.4795, 0.2271, 0.9531, 0.4638, 0.3997, 0.614, 0.6006 },
                NullableDoubleMember = 0.1749,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.0718, null, 0.6593, 0.9215, 0.7055, null, null, null, 0.6092 },
                DecimalMember = 0.110864848168494m,
                DecimalArrayMember = new decimal[] { 0.828628799727064m, 0.982938900536271m, 0.486722914145212m, 0.986469277331578m, 0.648736895427465m, 0.357994861755791m, 0.0320510279722285m, 0.436231527051276m, 0.832456251163206m, 0.970090213086099m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.967478208733724m, null, 0.987338780809684m, 0.173098585525133m, null, 0.212166403014664m, 0.741399684641729m, 0.960979108058374m, 0.359619886463732m, 0.5858941835824m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), null, null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(227885046),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(319920691), DateTime.Now.AddSeconds(-23899442), DateTime.Now.AddSeconds(-269406482), DateTime.Now.AddSeconds(33180097), DateTime.Now.AddSeconds(56257932), DateTime.Now.AddSeconds(148793398), DateTime.Now.AddSeconds(154141991), DateTime.Now.AddSeconds(211580132), DateTime.Now.AddSeconds(296088277), DateTime.Now.AddSeconds(162192451) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(287938633),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(332289923), null, DateTime.Now.AddSeconds(-139933563), null, DateTime.Now.AddSeconds(344564264), null, DateTime.Now.AddSeconds(-23013196), DateTime.Now.AddSeconds(-22189773), DateTime.Now.AddSeconds(92821494) },
            },
        };
    }
}
