// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
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
                StringMember = "W iMK:bKCK",
                StringArrayMember = new string[] { "DZe!dyIpVF", "OvLOtO0#:m", "SOoUz4s;Dd", "9m;:Dua#Af", "Zlvr8SkG?L", "F3BHVn#9,$", "MVuX72Ju1a", "o8WBz6%DRM", "eZbs:HYBip", "O%3VNAPSp%" },
                CharMember = 'v',
                CharArrayMember = new char[] { 'Z', 'P', 'V', 'Y', 'U', '@', 'M', 'N', 'C', '5' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'P', 'E', null, 'X', null, null, 'i', 'u', 'f', 'V' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, true, true, false, true, false, false, true, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, true, false, true, true, null, null, false, true, false },
                ByteMember = 230,
                ByteArrayMember = new byte[] { 139, 113, 78, 46, 17, 81, 168, 87, 96, 104 },
                NullableByteMember = 221,
                NullableByteArrayMember = new Nullable<byte>[] { 186, 27, 235, null, null, 41, null, 209, null, 174 },
                SByteMember = -111,
                SByteArrayMember = new sbyte[] { -29, 109, 114, -88, -106, 89, 102, -53, -80, 17 },
                NullableSByteMember = 104,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 94, -110, 28, null, null, -100, -33, null, -43, 115 },
                Int16Member = 25068,
                Int16ArrayMember = new short[] { 1848, 10078, -4848, -340, 29308, -11585, -11783, -4524, 9491, -15996 },
                NullableInt16Member = 11354,
                NullableInt16ArrayMember = new Nullable<short>[] { -16502, -7801, -800, -2861, null, null, 8493, 25405, null, null },
                UInt16Member = 36430,
                UInt16ArrayMember = new ushort[] { 55544, 19537, 33267, 18313, 33013, 36537, 3191, 19039, 53938, 22941 },
                NullableUInt16Member = 16267,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 58182, 35953, 3764, 39582, null, 15464, 30824, null, 32238, 4554 },
                Int32Member = 1651195798,
                Int32ArrayMember = new int[] { -656410890, 928506561, -278468646, 1188182308, -1480400247, -1599088698, 168924098, -1429081855, 893265125, 109136636 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { 746308646, 1499327279, -1583686314, null, 1541080617, null, 17354907, null, 1644246023, -262909481 },
                UInt32Member = 308191937,
                UInt32ArrayMember = new uint[] { 2057781879, 2621317808, 3604022086, 3277191376, 684545669, 2533060648, 2379300989, 1875430243, 1214852633, 2833447563 },
                NullableUInt32Member = 2789075371,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 730531662, 465316115, null, 4220003836, 1865166005, null, null, null, 1672999141, 1695939596 },
                Int64Member = -6367431894385689637,
                Int64ArrayMember = new long[] { 3479223508426169370, 1531523138471053550, 5019530471513753296, 6550973340555486889, -7048003704048098255, -5709731605926007808, 677199605782162379, -5967229330029066265, 6944723074908753578, 8187222460006318340 },
                NullableInt64Member = 1238460936669481579,
                NullableInt64ArrayMember = new Nullable<long>[] { null, -5951248975487368479, null, null, null, 4931038215683762731, null, -9013444671176465634, null, 6257249908562029020 },
                UInt64Member = 8440764581463246990,
                UInt64ArrayMember = new ulong[] { 8445219345904087794, 14654346105847570359, 58185133753883753, 1331127820028032792, 13464245201257166448, 6875570437463190501, 9847124376670847256, 14375344857183668947, 4670890938930968204, 15793726618342675969 },
                NullableUInt64Member = 14460338191333738882,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 8604243723634990570, 14508226883718643467, null, 7992809829747199240, 8893281178168564790, 5012266638523287772, 6516107394199837497, null, 9891142339021717549 },
                SingleMember = 0.8377F,
                SingleArrayMember = new float[] { 0.5361F, 0.6272F, 0.1923F, 0.8346F, 0.1421F, 0.6634F, 0.1075F, 0.4571F, 0.7634F, 0.2389F },
                NullableSingleMember = 0.5893F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.8719F, 0.071F, 0.0234F, 0.4615F, 0.1871F, 0.9736F, 0.1589F, null, null, 0.7904F },
                DoubleMember = 0.1778,
                DoubleArrayMember = new double[] { 0.567, 0.2382, 0.6989, 0.5934, 0.2245, 0.8982, 0.4016, 0.5149, 0.3721, 0.7965 },
                NullableDoubleMember = 0.2133,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.712, null, 0.561, null, null, null, 0.6332, 0.5034, 0.0973 },
                DecimalMember = 0.966125629521074m,
                DecimalArrayMember = new decimal[] { 0.256559718194202m, 0.845960072783698m, 0.653916350949125m, 0.526093493989212m, 0.319838539824585m, 0.126940208205972m, 0.92263579323516m, 0.136423497057999m, 0.145995129367273m, 0.249373706276816m },
                NullableDecimalMember = 0.523806100751588m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, 0.284940082985966m, null, null, null, 0.0773155065670456m, 0.909419750487104m, 0.61137362195035m, 0.0186845055482983m, 0.665437708670821m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, null },
                DateTimeMember = DateTime.Now.AddSeconds(-304682785),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-247698323), DateTime.Now.AddSeconds(-236173000), DateTime.Now.AddSeconds(245398566), DateTime.Now.AddSeconds(-52410833), DateTime.Now.AddSeconds(-282611907), DateTime.Now.AddSeconds(258694759), DateTime.Now.AddSeconds(154701526), DateTime.Now.AddSeconds(316779383), DateTime.Now.AddSeconds(-78269856), DateTime.Now.AddSeconds(50956117) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(252000957),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-96534405), DateTime.Now.AddSeconds(88073340), null, DateTime.Now.AddSeconds(-221578356), null, DateTime.Now.AddSeconds(-209961198), DateTime.Now.AddSeconds(198318201), DateTime.Now.AddSeconds(-110115655), DateTime.Now.AddSeconds(138890458), DateTime.Now.AddSeconds(-20519883) },
            },
            new DataConvertersTestClass {
                StringMember = "%W3qtJGbAO",
                StringArrayMember = new string[] { "Dgdfs#'%\"B", "%lP$5Ik\"MJ", "!K8%Ej1tWv", "F6r1H\"V?5t", "dkPdJqJO@'", "'YGb.OpR?T", "kdeufQdIbG", ": 6W$R'!ta", ";ywD!W%Kw,", ";60#HpOAY5" },
                CharMember = 'k',
                CharArrayMember = new char[] { '#', 'F', 'R', '!', 'B', '8', 'u', 'T', 'n', '5' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'k', 'U', '5', '9', 'N', 'h', 'O', null, 'b', 'S' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, false, false, true, false, false, false, true, false },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, false, null, null, null, null, null, null, false, true },
                ByteMember = 79,
                ByteArrayMember = new byte[] { 86, 254, 141, 248, 167, 231, 107, 33, 68, 61 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 41, 89, 249, null, 113, null, 177, 178, 175, 215 },
                SByteMember = 54,
                SByteArrayMember = new sbyte[] { 87, 72, 123, -115, 98, 56, 62, -78, 70, 115 },
                NullableSByteMember = -1,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 4, -88, -12, null, -75, 7, null, null, -38, 73 },
                Int16Member = 19276,
                Int16ArrayMember = new short[] { 28348, -7656, 32037, 16713, -9892, 6168, -1303, -11947, 26823, 17544 },
                NullableInt16Member = -3392,
                NullableInt16ArrayMember = new Nullable<short>[] { -18541, null, null, null, null, null, null, null, -10981, null },
                UInt16Member = 18792,
                UInt16ArrayMember = new ushort[] { 19175, 17256, 19418, 17743, 37596, 16624, 57898, 49260, 8165, 45144 },
                NullableUInt16Member = 50641,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 24577, null, null, null, 19744, null, 13782, 40585, null, null },
                Int32Member = -1331741677,
                Int32ArrayMember = new int[] { 355352422, 622921971, 1615832216, -1057841517, 1170326747, -1222516003, -1801496796, -698933429, 1203428213, 1900546849 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { -404372307, 293686813, 986466171, 1294215510, -791921609, 1704731959, null, 207943483, null, null },
                UInt32Member = 1892921985,
                UInt32ArrayMember = new uint[] { 637992282, 1983745412, 3458535564, 4230758054, 1660925892, 3119855951, 1338590732, 1508399911, 971340366, 4246001677 },
                NullableUInt32Member = 3346456208,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 876685036, null, null, null, null, null, 3885074964, null, 1781146601 },
                Int64Member = 134639243649886636,
                Int64ArrayMember = new long[] { 4558058290358308081, -5690175610355203351, 4805822830611410595, -2332918046265736164, 4720920377530348191, 5420866033629138328, -7818441021089931839, 6806804961719417704, -4839752834911662434, 5007581181901170886 },
                NullableInt64Member = -2483284375601344471,
                NullableInt64ArrayMember = new Nullable<long>[] { null, -5430494827124041003, null, null, 2115547711196768722, 6222078388521592168, 6551316921376873753, 7788457046096026880, -9183780301520522972, -3248151729243689907 },
                UInt64Member = 7251892878509916012,
                UInt64ArrayMember = new ulong[] { 11959385712258174841, 6343313136321578242, 5537668357766958932, 7781052095540329401, 17725723255009035385, 9669579353084343730, 488016077780099783, 4118184417876671370, 4575919938019843501, 6707686278834307922 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 1215568619750974556, null, null, 7725355026089804210, null, null, null, 14043370465734681426, null, 228553368453381883 },
                SingleMember = 0.4284F,
                SingleArrayMember = new float[] { 0.2009F, 0.4301F, 0.9146F, 0.1545F, 0.6552F, 0.716F, 0.4205F, 0.6216F, 0.163F, 0.8843F },
                NullableSingleMember = 0.1783F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.7396F, 0.7602F, null, 0.1622F, null, 0.5307F, 0.6456F, 0.2167F, null },
                DoubleMember = 0.9285,
                DoubleArrayMember = new double[] { 0.9238, 0.3121, 0.0496, 0.5227, 0.4744, 0.5368, 0.077, 0.2688, 0.6121, 0.0011 },
                NullableDoubleMember = 0.3232,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.3413, null, null, 0.2618, 0.5092, 0.5831, 0.2443, null, 0.4659, 0.1946 },
                DecimalMember = 0.221384455255588m,
                DecimalArrayMember = new decimal[] { 0.915159334489348m, 0.79617891520857m, 0.576058062008948m, 0.367913786438054m, 0.710595539333973m, 0.370840183543304m, 0.251456125828984m, 0.389568364684344m, 0.0771220042485717m, 0.495658719524734m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.768914337330694m, 0.980363143307414m, null, 0.398617183488425m, 0.634398529811796m, 0.104944801493163m, null, 0.616351086817545m, null, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, null, null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-38440793),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-178195658), DateTime.Now.AddSeconds(-91445824), DateTime.Now.AddSeconds(-309609818), DateTime.Now.AddSeconds(299196959), DateTime.Now.AddSeconds(96203717), DateTime.Now.AddSeconds(-152630540), DateTime.Now.AddSeconds(-225868323), DateTime.Now.AddSeconds(-217079995), DateTime.Now.AddSeconds(202078656), DateTime.Now.AddSeconds(-135557991) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(204876772), DateTime.Now.AddSeconds(-182522631), null, null, null, DateTime.Now.AddSeconds(161227311), DateTime.Now.AddSeconds(-276554575), null, DateTime.Now.AddSeconds(149018759) },
            },
            new DataConvertersTestClass {
                StringMember = "R6w?fksmDh",
                StringArrayMember = new string[] { "zErij'0,#6", "WJ0mpul'B%", "a90hRjax:g", "3ovw2$rJOU", "UZLp9GCosl", "94KWffd@LG", "1lGcTSy\"x'", "DfZER:x6Gn", "2Qt4H8rVO.", "AoN7:0aqsb" },
                CharMember = 'v',
                CharArrayMember = new char[] { '3', 'j', 'H', 'r', 'd', 'M', 'E', '3', ';', 'c' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, null, null, '2', '5', null, null, 'A', 'f', null },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, true, false, true, true, true, true, false, true, false },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, true, true, true, true, false, true, null, true, false },
                ByteMember = 32,
                ByteArrayMember = new byte[] { 190, 31, 86, 51, 228, 5, 36, 78, 231, 30 },
                NullableByteMember = 248,
                NullableByteArrayMember = new Nullable<byte>[] { 225, 50, null, 167, 226, null, 35, 123, 149, null },
                SByteMember = 82,
                SByteArrayMember = new sbyte[] { 78, -128, -27, -127, 81, 9, 83, 20, 92, 106 },
                NullableSByteMember = -119,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, 74, null, -67, null, null, 65, -10, -111, null },
                Int16Member = -17690,
                Int16ArrayMember = new short[] { -16984, 6678, -1563, -3859, 22955, -25688, 4337, 22544, 15075, 11877 },
                NullableInt16Member = -25424,
                NullableInt16ArrayMember = new Nullable<short>[] { -6746, -718, null, 5064, 98, 14029, -29913, -8121, -3536, null },
                UInt16Member = 1548,
                UInt16ArrayMember = new ushort[] { 25701, 57003, 62458, 15770, 2880, 41564, 53673, 65477, 18044, 853 },
                NullableUInt16Member = 47379,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 26768, null, 14771, 47662, 14256, 55043, null, 33225, null, 61782 },
                Int32Member = 949773931,
                Int32ArrayMember = new int[] { 2096063963, 952140983, 1013651471, 163339228, -1014760622, -70500779, -1661698766, -1972379591, 138995378, 1253130331 },
                NullableInt32Member = -839939977,
                NullableInt32ArrayMember = new Nullable<int>[] { null, 1439339274, -503090675, 237749126, null, -740508477, -778308862, null, 1660146364, 1436409384 },
                UInt32Member = 862489490,
                UInt32ArrayMember = new uint[] { 407213071, 1047560559, 2528306240, 3204816541, 1891890861, 2782705179, 3687399706, 4041001179, 1250654652, 160095410 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3076542771, 4211109857, 1790655048, 4196316066, null, null, null, 23901530, 4119995646, 1701874751 },
                Int64Member = -982602380473372848,
                Int64ArrayMember = new long[] { 7484224651680959250, 6820183873047213962, 38985930384394958, -7773318989490095772, -5630590695791506191, 2417190123317439786, 4390601575547069853, -4700132925802630101, 8479593594629558619, 3013265797136049574 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { 8849311306420806766, null, 1573003550411162613, 6294263872734166427, -1492901532554399519, 1487403137048019744, 6678184752069157893, -2771886749941694431, 7161944026108162065, null },
                UInt64Member = 11871099944713343840,
                UInt64ArrayMember = new ulong[] { 3594774141959253611, 17292538053555607269, 1597236818559250281, 5013832605691098795, 594945561904826864, 4225476446924913450, 5072786503367275618, 12034852679345077611, 3428119023980574930, 10635251284566547361 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 135589629996119486, 16191909349880329952, 12921097266498699740, 17840182178416495411, null, 13973965212149045969, 11689203070963876498, 11113239835692272050, 6810156423538338763, 1226894028322628915 },
                SingleMember = 0.127F,
                SingleArrayMember = new float[] { 0.3954F, 0.7497F, 0.9665F, 0.7243F, 0.3214F, 0.8465F, 0.6346F, 0.8946F, 0.6823F, 0.8664F },
                NullableSingleMember = 0.4864F,
                NullableSingleArrayMember = new Nullable<float>[] { null, null, 0.2007F, 0.3764F, 0.434F, 0.53F, 0.7349F, null, 0.6646F, null },
                DoubleMember = 0.2893,
                DoubleArrayMember = new double[] { 0.1688, 0.1094, 0.2667, 0.0909, 0.2564, 0.2863, 0.2687, 0.586, 0.8277, 0.6243 },
                NullableDoubleMember = 0.9338,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.2538, null, 0.8503, null, 0.3971, 0.7737, 0.9366, 0.7711, null },
                DecimalMember = 0.370622065834415m,
                DecimalArrayMember = new decimal[] { 0.1976713719661m, 0.888884088772433m, 0.416910802550196m, 0.611570188731152m, 0.158123778800633m, 0.610182203551163m, 0.900105572838842m, 0.510307531241193m, 0.142651326765818m, 0.0380858902457108m },
                NullableDecimalMember = 0.638053888550221m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.69476879030897m, 0.946110967391454m, null, 0.12113447043158m, 0.234274106568592m, null, null, 0.00189784585475017m, 0.623570423537528m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-167129201),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-149120037), DateTime.Now.AddSeconds(-200557138), DateTime.Now.AddSeconds(278591738), DateTime.Now.AddSeconds(313534673), DateTime.Now.AddSeconds(221616468), DateTime.Now.AddSeconds(64771569), DateTime.Now.AddSeconds(-243196183), DateTime.Now.AddSeconds(-154476663), DateTime.Now.AddSeconds(-125334190), DateTime.Now.AddSeconds(-1602963) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, null, DateTime.Now.AddSeconds(222613818), DateTime.Now.AddSeconds(-152928529), DateTime.Now.AddSeconds(-155686900), DateTime.Now.AddSeconds(-132819864), DateTime.Now.AddSeconds(267676034), DateTime.Now.AddSeconds(-97387910), null, DateTime.Now.AddSeconds(-282743273) },
            },
            new DataConvertersTestClass {
                StringMember = "U?%Vy0pDzY",
                StringArrayMember = new string[] { "VUKWz;RnWF", "iduAk99;l9", "1ARC.b'aNL", "nE!\"MBYU#R", "xkNWQTTtMc", ";sC1E6%B9h", "vY2F,zX?VD", "7yqsZv$%.B", "9XDvSIJ4Da", "VKFtB3,wsA" },
                CharMember = '!',
                CharArrayMember = new char[] { '9', 'p', 'R', 'D', 'z', '2', 'a', 'c', '9', 'X' },
                NullableCharMember = ';',
                NullableCharArrayMember = new Nullable<char>[] { null, '8', 'E', '#', 'g', null, null, 'Q', 'q', null },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, false, true, false, true, false, true, true, false, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, null, true, false, null, true, null, null, null, true },
                ByteMember = 238,
                ByteArrayMember = new byte[] { 240, 95, 232, 213, 159, 20, 138, 48, 56, 162 },
                NullableByteMember = 218,
                NullableByteArrayMember = new Nullable<byte>[] { 96, null, null, 178, 211, null, null, 222, null, null },
                SByteMember = -34,
                SByteArrayMember = new sbyte[] { 126, 63, 73, 44, 28, 77, -1, 122, 94, 3 },
                NullableSByteMember = 84,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, 12, -4, 38, 107, 46, 90, 33, -53, -121 },
                Int16Member = 17018,
                Int16ArrayMember = new short[] { 13967, -8553, -30550, -9244, 10825, 19614, 5978, 31348, -23608, 30842 },
                NullableInt16Member = -10488,
                NullableInt16ArrayMember = new Nullable<short>[] { 650, -26137, null, null, 19511, 8851, null, null, -17198, 10985 },
                UInt16Member = 53903,
                UInt16ArrayMember = new ushort[] { 5381, 47638, 8110, 54601, 36211, 61663, 17439, 33730, 31463, 3630 },
                NullableUInt16Member = 49386,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 63887, 20144, 15176, 40566, 43764, 2061, 51065, null, 29460, 45943 },
                Int32Member = -74822986,
                Int32ArrayMember = new int[] { -338454514, -1952935260, -1226752815, 393691884, -1034379303, 1192526616, 2037953172, -1834938319, -1088967777, 117134601 },
                NullableInt32Member = 849786298,
                NullableInt32ArrayMember = new Nullable<int>[] { null, -2073240804, -1505868320, -1021248745, null, 1473374864, 1064338417, 1592222608, 1817231946, null },
                UInt32Member = 16507799,
                UInt32ArrayMember = new uint[] { 3103911262, 3733685793, 2890959434, 2346917487, 1170122344, 2483523392, 78402845, 811284250, 1523967115, 619925475 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 937886987, null, 2481518525, 3615904436, null, 2136850367, 1563003404, 611181518, null, null },
                Int64Member = 769022161502650262,
                Int64ArrayMember = new long[] { -8929087705182022732, -7213676394238845341, -5068191222141975950, 4128831871378077774, -6712033616293757536, 3669240602511933430, -5353904075814826806, -8400130743856048244, -6517312440559500450, -5143534969689046005 },
                NullableInt64Member = 2145586789879013396,
                NullableInt64ArrayMember = new Nullable<long>[] { 1879876015335391189, null, 3147023118316186760, -6798113400498088886, 8353308442403850882, -4503805703341433935, 5320133943692519552, -7582597130882614351, 6304471750632522526, -8339587618624472638 },
                UInt64Member = 6125031400546126950,
                UInt64ArrayMember = new ulong[] { 7775019822090795583, 10556861171728336074, 3011306169303928820, 12371414850986996620, 14523490837963062969, 4863031519254540680, 1999134872822611857, 13490660873947704394, 15812524267184274040, 17246467200255056929 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 12702152762817040057, 9362777440515059, 16096419548317284610, 14110641273382121007, 17490823594813521496, 10413526481690306566, 7295857359515205142, 17734807903100951493, 13986283691953601848, 6449390110340263706 },
                SingleMember = 0.8693F,
                SingleArrayMember = new float[] { 0.7506F, 0.3519F, 0.1515F, 0.0053F, 0.9398F, 0.496F, 0.3772F, 0.0134F, 0.1484F, 0.9555F },
                NullableSingleMember = 0.0578F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.1493F, 0.3693F, 0.0021F, 0.9717F, 0.096F, 0.8802F, null, null, 0.0739F },
                DoubleMember = 0.0652,
                DoubleArrayMember = new double[] { 0.634, 0.5456, 0.3893, 0.7921, 0.9334, 0.2153, 0.5853, 0.4377, 0.7323, 0.6015 },
                NullableDoubleMember = 0.7815,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.6203, 0.0119, 0.0908, 0.0161, 0.839, null, 0.8389, 0.4415, 0.7595 },
                DecimalMember = 0.389024589240208m,
                DecimalArrayMember = new decimal[] { 0.163257855547892m, 0.885530235864038m, 0.936760007221799m, 0.188738430123034m, 0.99153677083018m, 0.160053042639213m, 0.358084782984942m, 0.669617792307763m, 0.75547900368211m, 0.985153713746884m },
                NullableDecimalMember = 0.156467587122867m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.0851122760892575m, 0.396849581252526m, 0.0827888403489534m, 0.471916091463893m, null, 0.281770352450103m, 0.576860768807255m, 0.747885484729304m, 0.204411735196858m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-93115376),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(65539574), DateTime.Now.AddSeconds(187839271), DateTime.Now.AddSeconds(-292339120), DateTime.Now.AddSeconds(22180131), DateTime.Now.AddSeconds(289166324), DateTime.Now.AddSeconds(247464699), DateTime.Now.AddSeconds(-12734137), DateTime.Now.AddSeconds(-216528890), DateTime.Now.AddSeconds(-140930767), DateTime.Now.AddSeconds(-265670453) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-159741125), DateTime.Now.AddSeconds(310122051), null, null, DateTime.Now.AddSeconds(148534234), null, DateTime.Now.AddSeconds(108693809), DateTime.Now.AddSeconds(-288114096), DateTime.Now.AddSeconds(-219582147), null },
            },
            new DataConvertersTestClass {
                StringMember = "\"bVZx TvCv",
                StringArrayMember = new string[] { "P2WmPr'c6s", "B1C$z$\"!QQ", "tlqjAjTYNQ", "7u@hsMR9am", "@;8XXcrQ6;", "B%Q\"WN4s0t", "D9 jqXmCHK", "RrCK%yfxQB", "s.s7:HU@;f", "ec3KPn268$" },
                CharMember = 'y',
                CharArrayMember = new char[] { '1', 'f', 'X', 'C', 'O', 'U', '3', 'v', 'q', ' ' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'Z', 'i', 'x', 'u', null, 'K', 'V', null, null, null },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, false, true, true, false, true, true, true, false, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, null, false, null, true, false, true, false, null },
                ByteMember = 50,
                ByteArrayMember = new byte[] { 142, 98, 204, 219, 50, 102, 159, 12, 139, 199 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, null, 207, 48, 237, 106, 191, null, 52, null },
                SByteMember = 91,
                SByteArrayMember = new sbyte[] { 98, -99, -85, -63, 50, -105, -120, -65, -25, 96 },
                NullableSByteMember = 85,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -66, 96, 2, 61, -122, null, 111, 0, -93 },
                Int16Member = 28856,
                Int16ArrayMember = new short[] { -26411, 3277, 1442, -15421, -22126, -31549, 22690, 25843, -31200, 17238 },
                NullableInt16Member = -4902,
                NullableInt16ArrayMember = new Nullable<short>[] { null, 25080, 19990, null, 16045, 22687, null, 30909, -4813, 6290 },
                UInt16Member = 44068,
                UInt16ArrayMember = new ushort[] { 34139, 14841, 8810, 13337, 6590, 41171, 4239, 51528, 33932, 8096 },
                NullableUInt16Member = 21630,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 40768, 3841, null, 16703, null, null, null, 29201, null, null },
                Int32Member = 1118351683,
                Int32ArrayMember = new int[] { -1287896586, -2001990417, 729218339, 366945479, -182597954, -1930526810, -1982769785, 1644951351, 1548877158, 1264714687 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { -534188280, null, 1634148815, 168497207, 1777935685, null, 1804821274, 1505607973, 1068863480, null },
                UInt32Member = 3726440480,
                UInt32ArrayMember = new uint[] { 256640925, 2563914168, 1134040278, 4058723339, 1556058042, 3428620506, 4249871145, 2320917779, 2063939414, 1986169376 },
                NullableUInt32Member = 3013780201,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1176357420, 1086284610, 460493570, null, 2197485594, 3397959375, null, 2880427251, 4252828188, 2102456874 },
                Int64Member = -2400641632715922258,
                Int64ArrayMember = new long[] { 4344362771104218776, -936697790901762467, -5650808765578126891, 645770557393902569, 6865297695762421509, 3929804530367761856, -3307295780104449248, 7132570796100269428, 6718481549540380312, -8354207709205282198 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -5652999743569113491, 4903950500699032914, null, 3838178937020097679, null, 5432550009432296071, 3835654130248102981, 1791049283744632615, 1093082039181491294, null },
                UInt64Member = 6254932486861116041,
                UInt64ArrayMember = new ulong[] { 1501868305036001183, 2525269720710269958, 12843114437497214233, 12413690082672377542, 9121767822606433368, 1783595141006762615, 6815996145389531085, 7685164558586715828, 12330211812986788640, 2746974427366777452 },
                NullableUInt64Member = 9309087781382139855,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 2578504378476262101, 15541281724989930170, 1677039983101891298, null, null, 3091581015309583485, 3583585857474312914, 7490762661066646557, 16757554066070620112, null },
                SingleMember = 0.1609F,
                SingleArrayMember = new float[] { 0.017F, 0.706F, 0.529F, 0.7659F, 0.5654F, 0.0723F, 0.9212F, 0.5873F, 0.9453F, 0.0453F },
                NullableSingleMember = 0.5804F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.0133F, null, null, 0.7486F, null, 0.9156F, 0.2299F, 0.4571F, null, 0.1286F },
                DoubleMember = 0.1277,
                DoubleArrayMember = new double[] { 0.6068, 0.3942, 0.9468, 0.6776, 0.6716, 0.5911, 0.8821, 0.5214, 0.1547, 0.3827 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.7082, 0.5284, 0.4383, 0.5461, 0.1072, 0.3314, 0.6438, null, null, 0.628 },
                DecimalMember = 0.183600459843802m,
                DecimalArrayMember = new decimal[] { 0.574119235215302m, 0.142777377258157m, 0.514882482057782m, 0.434521126071764m, 0.472703063973163m, 0.589207348315596m, 0.355570186126288m, 0.516157665854063m, 0.0440735145542738m, 0.44878486959102m },
                NullableDecimalMember = 0.901673862598147m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.560032212576224m, null, 0.0406576924911015m, 0.137725760605504m, 0.48913463909095m, null, 0.0224921135160828m, null, null, 0.503586900824513m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(247977166),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-213508642), DateTime.Now.AddSeconds(-207614544), DateTime.Now.AddSeconds(270662496), DateTime.Now.AddSeconds(345943750), DateTime.Now.AddSeconds(-196414805), DateTime.Now.AddSeconds(68395039), DateTime.Now.AddSeconds(-25488670), DateTime.Now.AddSeconds(-219950013), DateTime.Now.AddSeconds(46768497), DateTime.Now.AddSeconds(-87359005) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-347353774), null, DateTime.Now.AddSeconds(-47559114), DateTime.Now.AddSeconds(-79713891), null, DateTime.Now.AddSeconds(17315506), DateTime.Now.AddSeconds(275657997), DateTime.Now.AddSeconds(-201577820), DateTime.Now.AddSeconds(11457915), DateTime.Now.AddSeconds(325557934) },
            },
            new DataConvertersTestClass {
                StringMember = "XoWjwoe$Fn",
                StringArrayMember = new string[] { ".bu11#'8fD", "mSJ.Kt5lr9", "MljQ%:yWzr", "otzF5Z$zvJ", "Ro pRG2cUw", "80pbxtiZjB", "4:oxZubf?M", "aa51ErBzT2", "%1S8aw8Zqc", "aPGueQW7xa" },
                CharMember = ';',
                CharArrayMember = new char[] { 'N', 's', 'N', 'R', 'l', 'W', 'L', 'g', 'r', 'L' },
                NullableCharMember = 'g',
                NullableCharArrayMember = new Nullable<char>[] { '4', 'U', 'Q', 'F', null, null, 'b', null, 'C', 'm' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, true, true, true, true, false, false, true, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, null, null, true, false, false, true, false, null, true },
                ByteMember = 200,
                ByteArrayMember = new byte[] { 18, 34, 80, 26, 134, 155, 142, 88, 188, 129 },
                NullableByteMember = 179,
                NullableByteArrayMember = new Nullable<byte>[] { null, null, null, null, 94, 62, null, null, null, null },
                SByteMember = -125,
                SByteArrayMember = new sbyte[] { -79, 81, -45, -107, 61, 10, 87, 64, 122, -113 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -55, -24, -122, 111, null, null, 47, null, null },
                Int16Member = -28827,
                Int16ArrayMember = new short[] { -28463, -31634, 11552, -25108, 21023, 32126, -32375, 6045, -22644, -26103 },
                NullableInt16Member = 10819,
                NullableInt16ArrayMember = new Nullable<short>[] { 15367, null, -24912, 4987, 17615, null, 4659, -13260, -14101, null },
                UInt16Member = 55049,
                UInt16ArrayMember = new ushort[] { 37814, 36708, 22233, 60440, 54314, 29562, 23159, 29771, 543, 55448 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 52130, 60446, 47206, 6059, null, 60060, 17255, 912, 6219, null },
                Int32Member = 1548376067,
                Int32ArrayMember = new int[] { -1042772344, -1074703643, -43347615, -1147979219, -691688010, -592244218, -1160351673, 783213940, -1153874550, -525438340 },
                NullableInt32Member = -532416080,
                NullableInt32ArrayMember = new Nullable<int>[] { 1277197451, 1316413583, null, 933140346, null, null, null, null, null, 483522211 },
                UInt32Member = 535824295,
                UInt32ArrayMember = new uint[] { 12968405, 4099116507, 1914028952, 109929613, 3678789203, 971519501, 3173255168, 3618319649, 2181747688, 2672365189 },
                NullableUInt32Member = 2381123173,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1301914999, 2221093491, null, 3522991824, 660826118, 3065531176, null, 1305607497, null, null },
                Int64Member = 164079334741656000,
                Int64ArrayMember = new long[] { 5779738335460604674, 5585906589264157601, -914454018214930035, -5389830649925246312, -6725053226410141093, -2856547356005649977, 5883323755330304404, 4354525467612598785, 1606899499378588853, -5760899779343552060 },
                NullableInt64Member = 833588112030406529,
                NullableInt64ArrayMember = new Nullable<long>[] { 6789859635943001181, 5024168916676178980, null, null, 5050074378883326103, 8689725177281586621, -5066649270018898712, null, null, null },
                UInt64Member = 12804830784465849787,
                UInt64ArrayMember = new ulong[] { 69176905541857496, 8641291111423776500, 8336637634220482360, 9847926408029404185, 15802026693967887683, 15169260933713376332, 4192325766469440097, 13597373217521509679, 18331403703537955112, 15315125078808166206 },
                NullableUInt64Member = 13341848719118470025,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 12564836332315339768, 13035543532900253359, 14873298303427402628, 4015785557058860908, 6225671380892449468, 8753320793058397439, 4208727503280596868, null, 317758924176563607, null },
                SingleMember = 0.3168F,
                SingleArrayMember = new float[] { 0.9177F, 0.991F, 0.6587F, 0.5097F, 0.5332F, 0.9587F, 0.3695F, 0.992F, 0.9329F, 0.7974F },
                NullableSingleMember = 0.8908F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.0046F, 0.3903F, 0.5214F, 0.9245F, null, null, null, 0.0051F, 0.8403F },
                DoubleMember = 0.9464,
                DoubleArrayMember = new double[] { 0.0476, 0.2131, 0.6324, 0.2469, 0.5738, 0.9956, 0.1878, 0.1488, 0.4412, 0.2154 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.9144, null, null, 0.2376, null, null, 0.8726, 0.2646, 0.4579, null },
                DecimalMember = 0.709795911129054m,
                DecimalArrayMember = new decimal[] { 0.307451156672763m, 0.678000781139316m, 0.22258494447032m, 0.21794260114627m, 0.182227206218725m, 0.0659979327355472m, 0.108934590547654m, 0.211010613310326m, 0.550779211685334m, 0.555995364075956m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.378285807890698m, 0.767055193082944m, 0.167214015288948m, null, 0.973516654797465m, 0.63378719830519m, 0.199172155469542m, null, 0.710869298355065m, 0.722873833839649m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { null, null, null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, null },
                DateTimeMember = DateTime.Now.AddSeconds(79526448),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(267373490), DateTime.Now.AddSeconds(-140905562), DateTime.Now.AddSeconds(-86164763), DateTime.Now.AddSeconds(-197971332), DateTime.Now.AddSeconds(-80509115), DateTime.Now.AddSeconds(84119374), DateTime.Now.AddSeconds(-33682253), DateTime.Now.AddSeconds(-153688477), DateTime.Now.AddSeconds(-328646995), DateTime.Now.AddSeconds(255679805) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, null, DateTime.Now.AddSeconds(88148834), DateTime.Now.AddSeconds(-223091649), null, null, null, DateTime.Now.AddSeconds(77777110), DateTime.Now.AddSeconds(55269974), DateTime.Now.AddSeconds(-293797129) },
            },
            new DataConvertersTestClass {
                StringMember = "S.razW22KS",
                StringArrayMember = new string[] { "p!1g\"!V#8f", "xTT0X@QdgW", "JR1,..bYXA", "T,V@HFum08", "QT2AGOeFXK", "l E?WyaT;1", "Zj3TB\"@kd4", "L03hm\"QefF", "v\"m'r.hhzM", ",eldMmFk4m" },
                CharMember = 'A',
                CharArrayMember = new char[] { 'O', 's', 'i', '1', 'D', 's', '7', '1', 'k', 'K' },
                NullableCharMember = 'V',
                NullableCharArrayMember = new Nullable<char>[] { '5', null, 'D', null, 'p', null, null, 'K', '7', null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, true, false, true, true, true, true, true, false, false },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, null, null, false, true, null, null, true, true, true },
                ByteMember = 227,
                ByteArrayMember = new byte[] { 18, 238, 40, 101, 122, 65, 172, 41, 254, 148 },
                NullableByteMember = 117,
                NullableByteArrayMember = new Nullable<byte>[] { 171, 193, 189, 152, 187, null, 63, 84, 168, 100 },
                SByteMember = 40,
                SByteArrayMember = new sbyte[] { 33, -93, 81, -74, 120, -1, 110, -7, 79, 71 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -99, -83, -75, null, 117, 83, null, 54, null, null },
                Int16Member = 713,
                Int16ArrayMember = new short[] { 32283, 28388, -5978, -31269, -15773, 31661, -18273, -4750, -27399, 6997 },
                NullableInt16Member = 26447,
                NullableInt16ArrayMember = new Nullable<short>[] { 17734, -17425, 28500, null, 9556, null, 18258, 7906, -23788, 20516 },
                UInt16Member = 14140,
                UInt16ArrayMember = new ushort[] { 41369, 61026, 419, 28893, 40428, 1344, 11005, 45617, 64407, 14750 },
                NullableUInt16Member = 43016,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 36519, 22747, 33847, 4879, null, null, 17469, 15657, null, null },
                Int32Member = 1221569221,
                Int32ArrayMember = new int[] { 1670344725, -138043727, 345569888, 1017431282, -1167192387, 437838564, -799424089, -1349736969, 1136332554, -1209173299 },
                NullableInt32Member = -38775711,
                NullableInt32ArrayMember = new Nullable<int>[] { -1781218745, -75229360, null, 2130592620, -1449969915, -1354660467, 1953932590, 193859859, null, -601139801 },
                UInt32Member = 663482683,
                UInt32ArrayMember = new uint[] { 1531554480, 295060967, 1123028411, 3633565097, 4073822603, 399538124, 3567116492, 3123583191, 3811730161, 2880254975 },
                NullableUInt32Member = 979101638,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, null, 3003111563, 3923917277, 2901874156, 1016214048, 2206739725, 1015327984, null, 2679077870 },
                Int64Member = -2542862710168613443,
                Int64ArrayMember = new long[] { 5384330296343696256, 230054043389158333, -224089518115429895, -551611541080485122, 6399898981108032095, 999337195687562661, -1291204387368910370, -2525646889995572598, 7974314588042148434, -1086172570876268013 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { 7873918887051138143, 4911415449485591105, -6044774436084021672, null, null, -2808371730540569771, 4527159981673108804, 8397505394651100672, 8212196836080348556, null },
                UInt64Member = 18444983179934508994,
                UInt64ArrayMember = new ulong[] { 10132393785916005107, 4721162737058459670, 1632187118248143635, 12887904632258271600, 8835113359140288894, 3860830692143829321, 9876804870653710428, 18359967889448843195, 16017402134067010833, 5712745405350156057 },
                NullableUInt64Member = 4855381809668700352,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 10327785373861789769, 6221754530541996854, 14110382633135079730, null, 10703259543923494312, 16046073716171695787, null, 16381896739845909474, 6491965413198495560, 4462119673386180651 },
                SingleMember = 0.9653F,
                SingleArrayMember = new float[] { 0.7499F, 0.4996F, 0.9719F, 0.052F, 0.8615F, 0.8298F, 0.5115F, 0.9939F, 0.749F, 0.8651F },
                NullableSingleMember = 0.5583F,
                NullableSingleArrayMember = new Nullable<float>[] { null, null, 0.4302F, null, 0.1011F, null, null, 0.5362F, 0.4455F, 0.4646F },
                DoubleMember = 0.8526,
                DoubleArrayMember = new double[] { 0.5529, 0.2687, 0.0734, 0.8499, 0.7597, 0.4695, 0.2675, 0.8079, 0.2816, 0.7121 },
                NullableDoubleMember = 0.5745,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.6565, null, null, 0.9487, 0.5755, 0.9294, 0.5693, null, null },
                DecimalMember = 0.253438227212157m,
                DecimalArrayMember = new decimal[] { 0.216129829725867m, 0.165589336453853m, 0.94236027399502m, 0.594572310093669m, 0.956908812192819m, 0.822863423734684m, 0.544160234005314m, 0.814546742418263m, 0.609137208359788m, 0.260228617814148m },
                NullableDecimalMember = 0.291165655091571m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, 0.847432361895631m, null, null, 0.104242169732495m, 0.760245952924079m, 0.109205142786366m, null, 0.0363440977230214m, 0.192974816570112m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(279187107),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-39228807), DateTime.Now.AddSeconds(-335013707), DateTime.Now.AddSeconds(-69354946), DateTime.Now.AddSeconds(211109667), DateTime.Now.AddSeconds(225274125), DateTime.Now.AddSeconds(3418247), DateTime.Now.AddSeconds(-260830761), DateTime.Now.AddSeconds(12156408), DateTime.Now.AddSeconds(-128155362), DateTime.Now.AddSeconds(-324209291) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, null, DateTime.Now.AddSeconds(159767128), null, DateTime.Now.AddSeconds(238242910), DateTime.Now.AddSeconds(-239519326), null, DateTime.Now.AddSeconds(124760448), DateTime.Now.AddSeconds(282752889), DateTime.Now.AddSeconds(298882024) },
            },
            new DataConvertersTestClass {
                StringMember = "M\"b:wP6xU6",
                StringArrayMember = new string[] { "f.jd@bb2lD", "d\"KE22nc8T", "WNZp6MXsJ9", "!G\"O0azz0W", "4w4KUydMZo", "BY8pGJtyg%", ";eV;Fu94QK", "67P%udK19V", "@DkWj%fR#1", "D9!GMNCdyo" },
                CharMember = 'G',
                CharArrayMember = new char[] { '\'', 'm', 'n', '#', 'X', 'C', 'g', 'G', 's', 'L' },
                NullableCharMember = 'D',
                NullableCharArrayMember = new Nullable<char>[] { null, 'f', null, 'L', 's', null, null, '2', '$', null },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, false, true, true, true, true, false, true, false, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, false, null, false, true, null, false, false, true },
                ByteMember = 196,
                ByteArrayMember = new byte[] { 147, 31, 43, 85, 19, 244, 22, 54, 223, 54 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, 224, 245, null, null, 46, 54, null, 183, null },
                SByteMember = 80,
                SByteArrayMember = new sbyte[] { 14, 84, -114, -113, -101, 77, 41, 12, -104, 72 },
                NullableSByteMember = 50,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -35, null, 113, 82, -2, 19, -22, 20, 117, 37 },
                Int16Member = 8224,
                Int16ArrayMember = new short[] { 26905, 18395, -16526, -16711, -30786, 20930, 3949, -17071, 16909, -16135 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { 22582, -12051, 30908, -12479, -23997, 31616, 7911, -26211, -26809, null },
                UInt16Member = 42690,
                UInt16ArrayMember = new ushort[] { 33772, 26263, 24045, 41410, 62880, 63674, 10622, 45218, 46837, 36254 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 41115, 28016, null, 32699, 2050, 33172, null, null, null, null },
                Int32Member = -408858896,
                Int32ArrayMember = new int[] { 1814073677, 552922724, -1749773648, 1842828831, 368056445, 755122338, -133421407, 720778917, 730244264, 2107796834 },
                NullableInt32Member = 1301713885,
                NullableInt32ArrayMember = new Nullable<int>[] { 1275790275, 778489323, null, null, -1138253276, -305431626, -195653261, -436441514, 1273795594, 2017608557 },
                UInt32Member = 2745609768,
                UInt32ArrayMember = new uint[] { 1870899473, 4089265149, 3580235487, 1856764659, 3678439246, 767490575, 246809118, 475068669, 714161394, 4287952036 },
                NullableUInt32Member = 3654494371,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 995831489, null, 1937128642, 1320682540, null, null, 2169813744, 3356127531, null, null },
                Int64Member = -4480577140380204597,
                Int64ArrayMember = new long[] { -8985978370026783468, 4708096170849998470, -6022730527156471297, -8270890339304505552, 1961362525921328575, -3851042879750788438, 3998739010183987200, -886741483421095171, -7276708516196651734, 8788430403201477541 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -4942412603647782892, -974718705602787333, null, 7958240024577265327, null, null, -5302073132809664527, null, null, null },
                UInt64Member = 9252150130566534173,
                UInt64ArrayMember = new ulong[] { 1355911060515718835, 18172834128403486711, 16863236831280434079, 3544751866858224320, 12262037966979975632, 5455733671490431305, 8063893896495779385, 16056101869262136557, 11474392727472872242, 6676444663032631439 },
                NullableUInt64Member = 608988838330721504,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 13255733099569508766, 1634496129972194631, 18131042121687726158, 8257949240594744498, null, 198048211243114497, null, 6524856880114796810, null, 12880079365480471178 },
                SingleMember = 0.2797F,
                SingleArrayMember = new float[] { 0.6883F, 0.0712F, 0.5797F, 0.3913F, 0.0987F, 0.4276F, 0.4868F, 0.2637F, 0.2599F, 0.0377F },
                NullableSingleMember = 0.1999F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.4918F, 0.1925F, 0.9814F, null, null, null, 0.9698F, 0.7103F, 0.2139F },
                DoubleMember = 0.9269,
                DoubleArrayMember = new double[] { 0.1908, 0.7389, 0.4754, 0.2312, 0.427, 0.8398, 0.5896, 0.8206, 0.0008, 0.2325 },
                NullableDoubleMember = 0.011,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.1836, null, 0.8866, null, null, 0.8862, 0.4947, 0.5546, null, null },
                DecimalMember = 0.0412749718656008m,
                DecimalArrayMember = new decimal[] { 0.114067181194525m, 0.615390844693018m, 0.734573215492074m, 0.905455950348404m, 0.277121345413004m, 0.210067664440816m, 0.749789021876854m, 0.085579854112548m, 0.778367165808693m, 0.733771688393321m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.848771851891718m, null, null, 0.622580735985818m, null, 0.383726306663456m, 0.319437428459454m, 0.86778012829777m, 0.790195169352262m, 0.166372578468538m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(-32307132),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-15502415), DateTime.Now.AddSeconds(271197599), DateTime.Now.AddSeconds(119592367), DateTime.Now.AddSeconds(-47859639), DateTime.Now.AddSeconds(-130956190), DateTime.Now.AddSeconds(-35823865), DateTime.Now.AddSeconds(-188657422), DateTime.Now.AddSeconds(-347641007), DateTime.Now.AddSeconds(-340605682), DateTime.Now.AddSeconds(118060992) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-76294362),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-270895734), DateTime.Now.AddSeconds(287266978), null, null, DateTime.Now.AddSeconds(-236948523), DateTime.Now.AddSeconds(-137740459), DateTime.Now.AddSeconds(153699374), DateTime.Now.AddSeconds(-266999916), DateTime.Now.AddSeconds(-285753668), DateTime.Now.AddSeconds(-142241892) },
            },
            new DataConvertersTestClass {
                StringMember = ":Ej;kl3y@D",
                StringArrayMember = new string[] { "@h?5?U3'EE", "HmX4gK;POn", "w5v\"oycB:2", ",I4IHXI8A3", "ZAhUQSD@#2", "qq9ys1ufQG", "wV?Yfy?S'A", "#qC@b8joNu", "4jjnmviBVa", "irK3mOb'nB" },
                CharMember = 'Z',
                CharArrayMember = new char[] { '2', 'v', ',', 'o', 'G', 'i', 'i', '#', 'I', 'T' },
                NullableCharMember = 'v',
                NullableCharArrayMember = new Nullable<char>[] { 'u', ':', 'U', 'S', 'o', null, null, null, 'W', 'R' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, true, true, true, true, true, false, true, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, true, null, false, null, null, true, true, null, true },
                ByteMember = 179,
                ByteArrayMember = new byte[] { 69, 128, 155, 67, 28, 218, 40, 30, 103, 5 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 165, 57, 53, null, null, 154, 22, 210, 1, 182 },
                SByteMember = -89,
                SByteArrayMember = new sbyte[] { -102, 74, 37, 86, -106, -114, -47, 51, -56, 106 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 106, null, 48, -17, null, -89, -68, null, null, -80 },
                Int16Member = 19655,
                Int16ArrayMember = new short[] { -10181, -20184, 2248, 896, -2401, -4184, 19592, 3332, -24907, -16522 },
                NullableInt16Member = 32729,
                NullableInt16ArrayMember = new Nullable<short>[] { null, -26699, 6647, null, -1077, -2973, null, -30552, null, null },
                UInt16Member = 32199,
                UInt16ArrayMember = new ushort[] { 44411, 29102, 61362, 40741, 43191, 56596, 35304, 52989, 1558, 19959 },
                NullableUInt16Member = 52652,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 26064, 59079, null, 10349, 56341, null, 40530, 18469, 42143 },
                Int32Member = -449586081,
                Int32ArrayMember = new int[] { -757976029, 105084551, 155564818, -828513215, -905092824, -1880799876, -1738475808, -798104305, 2033640858, -69904271 },
                NullableInt32Member = -1054163354,
                NullableInt32ArrayMember = new Nullable<int>[] { null, 1545130453, 751672643, -603261173, null, null, null, -750097218, 1500316546, null },
                UInt32Member = 1102753310,
                UInt32ArrayMember = new uint[] { 3798201268, 2014830163, 787774719, 502529815, 263403843, 1005095681, 1419885181, 2688900183, 658398284, 1214747790 },
                NullableUInt32Member = 739366979,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3123235664, 3167637978, 2911190308, null, 1422505831, 1705599264, 694096036, 2589473407, null, 2597862374 },
                Int64Member = -7343500364669099917,
                Int64ArrayMember = new long[] { 8195329473173955812, -2158997772312454103, 1321343723443018381, -7791933222035979765, 6195197488957401540, 7656186267117882514, 4958916394835155527, 2399675312949997960, -6898065630165517228, 3817715061444244620 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, -7903809655605453949, 4060915605831777409, 1564192186618042346, -3443603545519513145, null, -3755323739340418256, null, 2800541566648296879, -6784182549714459127 },
                UInt64Member = 4624454312986111942,
                UInt64ArrayMember = new ulong[] { 7771380288232778165, 14432424197007825013, 13177592582041398359, 5159668597701840096, 7359382858522583068, 2674776948403383731, 16002240416530033883, 13474517219914786113, 8071160103878103184, 14343731038105520141 },
                NullableUInt64Member = 3404612289501242742,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 18314345175406508529, 9997301019188783754, 5959351051286993121, null, null, 16190154260095345129, null, 15041115933057764168, 2581512556367371005, 11852801490738093956 },
                SingleMember = 0.5373F,
                SingleArrayMember = new float[] { 0.2415F, 0.3065F, 0.0588F, 0.6729F, 0.1962F, 0.8127F, 0.2909F, 0.4716F, 0.1076F, 0.0107F },
                NullableSingleMember = 0.2412F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.6969F, 0.9814F, null, null, null, 0.6027F, null, 0.8668F, null, 0.8832F },
                DoubleMember = 0.791,
                DoubleArrayMember = new double[] { 0.5373, 0.1256, 0.9697, 0.07, 0.049, 0.3077, 0.0591, 0.8086, 0.6643, 0.1449 },
                NullableDoubleMember = 0.674,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.2172, null, 0.5108, 0.1541, null, 0.6068, 0.3832, 0.9565, 0.7402, 0.8776 },
                DecimalMember = 0.36362694001137m,
                DecimalArrayMember = new decimal[] { 0.609287825200481m, 0.364362161463244m, 0.70512204250921m, 0.575631538692403m, 0.479020099864611m, 0.444189560086482m, 0.378962136832077m, 0.489855444161498m, 0.0426461808398525m, 0.916076138809725m },
                NullableDecimalMember = 0.314012061455271m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.00794200626046304m, 0.265654897298066m, 0.063597741203074m, null, 0.883173749950312m, null, 0.625007486197287m, 0.281242221206383m, 0.56421724335432m, 0.918427333754708m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(-247450389),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(347524685), DateTime.Now.AddSeconds(-273045259), DateTime.Now.AddSeconds(100791379), DateTime.Now.AddSeconds(-340260643), DateTime.Now.AddSeconds(57394614), DateTime.Now.AddSeconds(-241736377), DateTime.Now.AddSeconds(-303088742), DateTime.Now.AddSeconds(293075058), DateTime.Now.AddSeconds(-308640370), DateTime.Now.AddSeconds(-37701611) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(-129810997), DateTime.Now.AddSeconds(-335941411), DateTime.Now.AddSeconds(-335278668), DateTime.Now.AddSeconds(169610910), DateTime.Now.AddSeconds(-248986216), DateTime.Now.AddSeconds(-94878215), DateTime.Now.AddSeconds(-305144516), null, DateTime.Now.AddSeconds(210090291) },
            },
            new DataConvertersTestClass {
                StringMember = " WbmeNyMBx",
                StringArrayMember = new string[] { "NpsZckspj:", "rmbpK5y27t", "'Rk lGfjzY", "tHgYs7XlG%", "EZRz7l5Nyu", "JCB.d,;h%N", ";vaqMpLFSZ", "%YhAWl6Rb ", "c8y,VuG GE", "9hQImZ@pzh" },
                CharMember = 'n',
                CharArrayMember = new char[] { '8', 'K', 's', 'q', 's', 'H', '.', 'u', 's', 's' },
                NullableCharMember = 'Y',
                NullableCharArrayMember = new Nullable<char>[] { 'Z', 'Z', null, ' ', null, 'l', 'Z', 'm', null, 'l' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, false, true, false, true, true, false, true, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, true, false, null, null, false, true, false, true },
                ByteMember = 190,
                ByteArrayMember = new byte[] { 232, 113, 176, 94, 209, 91, 210, 181, 221, 173 },
                NullableByteMember = 142,
                NullableByteArrayMember = new Nullable<byte>[] { 251, 149, null, 189, 151, null, null, 163, 176, 5 },
                SByteMember = -78,
                SByteArrayMember = new sbyte[] { 54, 91, -96, -8, -55, 99, 55, -86, 101, -33 },
                NullableSByteMember = -24,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 12, -84, 55, 34, 94, null, null, -92, 71, 92 },
                Int16Member = -13989,
                Int16ArrayMember = new short[] { 23020, -15115, -18544, 10325, -7988, -15086, -7926, -16268, 16060, 17834 },
                NullableInt16Member = -25666,
                NullableInt16ArrayMember = new Nullable<short>[] { null, -13798, null, -5979, 9664, null, -31947, 31743, null, 14156 },
                UInt16Member = 61613,
                UInt16ArrayMember = new ushort[] { 18432, 13567, 40927, 51018, 17172, 18822, 60468, 59068, 16849, 53659 },
                NullableUInt16Member = 34697,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 38395, 60458, 55530, null, 64872, 65515, 53708, 50519, 28389, null },
                Int32Member = 602469246,
                Int32ArrayMember = new int[] { 281164659, 50765903, -1812568098, 935048358, -1627912014, 1654213114, 568565114, -799912683, 2113457652, 1196382899 },
                NullableInt32Member = 567096001,
                NullableInt32ArrayMember = new Nullable<int>[] { null, 180908000, 840365901, null, null, 1938222206, null, null, 1677655818, 1544421506 },
                UInt32Member = 2162314298,
                UInt32ArrayMember = new uint[] { 195763833, 1150243683, 1486375907, 3680795115, 2487694, 3837688978, 1529843523, 2993059389, 2123470320, 2732410732 },
                NullableUInt32Member = 3652010350,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1344378715, 3538692179, 1083245842, 1995039617, 2733491011, null, 2050424657, 2254121101, 2550108937, 1589034432 },
                Int64Member = 860161517628396202,
                Int64ArrayMember = new long[] { -5872624531652194596, -1248071305369878002, 3418981614029377278, -8629828179656274969, -1767004357434768174, -7402024937927408829, 3902936672294759515, 2316146983350101777, -6414789822356758091, -7662808254648684838 },
                NullableInt64Member = -5794897097805491228,
                NullableInt64ArrayMember = new Nullable<long>[] { -6801246555776280969, -6541486363533445164, -3053011500001370973, null, null, null, -7455558053779699031, 2793623409410222352, -8189073828831877812, -5120751953014955400 },
                UInt64Member = 3251677936038670846,
                UInt64ArrayMember = new ulong[] { 4034468177406177481, 231900750919246577, 15579695486573903050, 15138671404544088255, 8347280039199572372, 15452152111819478116, 489890476906432261, 17943429730641532419, 14352011114275600677, 7191572170024865418 },
                NullableUInt64Member = 7090084697299767369,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 17437784357462232232, 12266030397784795034, 1920728274280204614, null, 14797389815685730361, 10336702171712874594, 17782564672551474846, 15717834959544903016, 14784381678473757475, 12376295818494395005 },
                SingleMember = 0.0958F,
                SingleArrayMember = new float[] { 0.1891F, 0.5594F, 0.8976F, 0.7439F, 0.4666F, 0.7007F, 0.8708F, 0.5534F, 0.8946F, 0.7859F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.3769F, 0.3723F, 0.2697F, 0.3069F, 0.8735F, 0.475F, 0.6495F, null, 0.9884F, 0.2561F },
                DoubleMember = 0.3657,
                DoubleArrayMember = new double[] { 0.1252, 0.0787, 0.5044, 0.9363, 0.1785, 0.8418, 0.7692, 0.305, 0.2028, 0.3061 },
                NullableDoubleMember = 0.3115,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.8091, 0.2814, null, 0.9586, 0.4146, 0.6246, 0.4176, 0.9347, 0.4446 },
                DecimalMember = 0.533357874182152m,
                DecimalArrayMember = new decimal[] { 0.194270718327453m, 0.832991257201013m, 0.207285612544263m, 0.526627965242916m, 0.907771783058524m, 0.947341332788643m, 0.829421727614415m, 0.564855087014054m, 0.996438128546837m, 0.0497491899408132m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.693425582740427m, 0.503865412430871m, 0.325761213069594m, 0.937384714449378m, null, 0.0748401623324337m, 0.37288051692148m, 0.0649907275928905m, 0.841982510039856m, 0.723517485008479m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(-306907784),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-111209021), DateTime.Now.AddSeconds(-63188347), DateTime.Now.AddSeconds(-21726841), DateTime.Now.AddSeconds(126904538), DateTime.Now.AddSeconds(-165384627), DateTime.Now.AddSeconds(-1391488), DateTime.Now.AddSeconds(264046259), DateTime.Now.AddSeconds(-122151809), DateTime.Now.AddSeconds(94923569), DateTime.Now.AddSeconds(-27818523) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-93023966),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(21543304), DateTime.Now.AddSeconds(157781309), DateTime.Now.AddSeconds(219899818), DateTime.Now.AddSeconds(-294964292), DateTime.Now.AddSeconds(348079244), null, null, DateTime.Now.AddSeconds(117946499), DateTime.Now.AddSeconds(-151274411), DateTime.Now.AddSeconds(-110442702) },
            },
        };
    }
}
