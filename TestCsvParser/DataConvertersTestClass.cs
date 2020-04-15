// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Diagnostics;
using System.Reflection;

namespace CsvParserTests
{
    public class DataConvertersTestClass
    {
        public string StringMember { get; set; }
        public string[] StringArrayMember { get; set; }
        public char CharMember { get; set; }
        public char[] CharArrayMember { get; set; }
        public char? NullableCharMember { get; set; }
        public char?[] NullableCharArrayMember { get; set; }
        public bool BooleanMember { get; set; }
        public bool[] BooleanArrayMember { get; set; }
        public bool? NullableBooleanMember { get; set; }
        public bool?[] NullableBooleanArrayMember { get; set; }
        public byte ByteMember { get; set; }
        public byte[] ByteArrayMember { get; set; }
        public byte? NullableByteMember { get; set; }
        public byte?[] NullableByteArrayMember { get; set; }
        public sbyte SByteMember { get; set; }
        public sbyte[] SByteArrayMember { get; set; }
        public sbyte? NullableSByteMember { get; set; }
        public sbyte?[] NullableSByteArrayMember { get; set; }
        public short Int16Member { get; set; }
        public short[] Int16ArrayMember { get; set; }
        public short? NullableInt16Member { get; set; }
        public short?[] NullableInt16ArrayMember { get; set; }
        public ushort UInt16Member { get; set; }
        public ushort[] UInt16ArrayMember { get; set; }
        public ushort? NullableUInt16Member { get; set; }
        public ushort?[] NullableUInt16ArrayMember { get; set; }
        public int Int32Member { get; set; }
        public int[] Int32ArrayMember { get; set; }
        public int? NullableInt32Member { get; set; }
        public int?[] NullableInt32ArrayMember { get; set; }
        public uint UInt32Member { get; set; }
        public uint[] UInt32ArrayMember { get; set; }
        public uint? NullableUInt32Member { get; set; }
        public uint?[] NullableUInt32ArrayMember { get; set; }
        public long Int64Member { get; set; }
        public long[] Int64ArrayMember { get; set; }
        public long? NullableInt64Member { get; set; }
        public long?[] NullableInt64ArrayMember { get; set; }
        public ulong UInt64Member { get; set; }
        public ulong[] UInt64ArrayMember { get; set; }
        public ulong? NullableUInt64Member { get; set; }
        public ulong?[] NullableUInt64ArrayMember { get; set; }
        public float SingleMember { get; set; }
        public float[] SingleArrayMember { get; set; }
        public float? NullableSingleMember { get; set; }
        public float?[] NullableSingleArrayMember { get; set; }
        public double DoubleMember { get; set; }
        public double[] DoubleArrayMember { get; set; }
        public double? NullableDoubleMember { get; set; }
        public double?[] NullableDoubleArrayMember { get; set; }
        public decimal DecimalMember { get; set; }
        public decimal[] DecimalArrayMember { get; set; }
        public decimal? NullableDecimalMember { get; set; }
        public decimal?[] NullableDecimalArrayMember { get; set; }
        public Guid GuidMember { get; set; }
        public Guid[] GuidArrayMember { get; set; }
        public Guid? NullableGuidMember { get; set; }
        public Guid?[] NullableGuidArrayMember { get; set; }
        public DateTime DateTimeMember { get; set; }
        public DateTime[] DateTimeArrayMember { get; set; }
        public DateTime? NullableDateTimeMember { get; set; }
        public DateTime?[] NullableDateTimeArrayMember { get; set; }

        #region IEquatable

        public override int GetHashCode()
        {
            // No good option here
            return 1;
        }

        public override bool Equals(object obj) => Equals(obj as DataConvertersTestClass);

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
        private bool ObjectsAreEqual(object a, object b)
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
                StringMember = (string)"5uInon$NQx",
                StringArrayMember = new string[] { "YUyN4Qriic", "H8ujsa::L ", "Oe ,;Zp07m", "bo'T8Ye';7", "yl;Fw#Av@N", "wGCQj!U.Xe", "x;y@Bq\"ayT", "# AaYzo!P\"", "Z\"4u;Vmy7:", "DqriK7.!EG" },
                CharMember = (char)'e',
                CharArrayMember = new char[] { '8', 'Z', ';', 's', 'z', '3', 'm', '!', 'K', 'C' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { 'P', null, '%', 'k', null, 'T', null, null, null, null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, false, false, true, true, true, true, false, true, true },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { false, true, false, false, false, false, true, null, false, null },
                ByteMember = (byte)122,
                ByteArrayMember = new byte[] { 123, 226, 217, 181, 70, 140, 15, 169, 63, 51 },
                NullableByteMember = (byte?)95,
                NullableByteArrayMember = new byte?[] { 172, 77, null, 86, 118, 220, 142, 14, 217, null },
                SByteMember = (sbyte)-17,
                SByteArrayMember = new sbyte[] { -121, 101, -14, 63, -119, -84, 70, 123, -37, 101 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { null, 1, 32, null, 3, 123, null, null, null, 99 },
                Int16Member = (short)11430,
                Int16ArrayMember = new short[] { 6534, -31863, 18918, -11791, -29121, -16184, -24314, 32629, -4826, -4954 },
                NullableInt16Member = (short?)null,
                NullableInt16ArrayMember = new short?[] { 17183, 1217, 23597, -12735, null, null, 10467, 12778, null, null },
                UInt16Member = (ushort)51867,
                UInt16ArrayMember = new ushort[] { 6247, 13613, 46720, 17089, 61926, 21820, 44473, 21029, 26311, 16086 },
                NullableUInt16Member = (ushort?)13107,
                NullableUInt16ArrayMember = new ushort?[] { 40859, 57031, 25216, null, 11943, 24900, null, null, 21696, 4839 },
                Int32Member = (int)1215875210,
                Int32ArrayMember = new int[] { -963554814, 955697137, 654150990, -830393666, 1930471166, -536456256, -1484187937, 1586499738, 680817211, -1665477154 },
                NullableInt32Member = (int?)-1047933794,
                NullableInt32ArrayMember = new int?[] { null, -1145396346, -1366883145, null, 1329453799, -358679235, null, null, 1961568752, 528254753 },
                UInt32Member = (uint)3779646449,
                UInt32ArrayMember = new uint[] { 3370889106, 208654729, 2039927831, 2472962621, 2559101466, 382304133, 3875939092, 502174956, 2232433074, 2654159683 },
                NullableUInt32Member = (uint?)2219461504,
                NullableUInt32ArrayMember = new uint?[] { 1213836081, 1538214694, 1706349797, 3867353224, 3763995590, 4159280855, 613514803, 4188310874, null, 2700992003 },
                Int64Member = (long)-4571738493768188103,
                Int64ArrayMember = new long[] { -6192110628523108259, -1562716737216687312, 2926697757663014740, 2590560327885806269, 7324165962853800637, 2062125821899527881, -6582332478130025106, -5996963630970909445, -6212728656515858080, -7447206394397718005 },
                NullableInt64Member = (long?)-5412579548169035330,
                NullableInt64ArrayMember = new long?[] { -3562269189213301152, 6044189668592888065, -7270654612549934350, -7714566544855026705, -6616511109513986807, -1209991554435394004, -6852384388866401572, null, 1646019372482105127, -4256602575371391407 },
                UInt64Member = (ulong)17160928741521514072,
                UInt64ArrayMember = new ulong[] { 10006327540324024099, 14436481143665368745, 10917785956624592547, 9254495706726764852, 17045768492329717889, 12222206698389527635, 6877727266503353605, 1709617205313773627, 16692509363465964335, 3700286378122899167 },
                NullableUInt64Member = (ulong?)6838994316340957225,
                NullableUInt64ArrayMember = new ulong?[] { 15033731481008373864, null, null, 6707115542166230355, 5671897103828659587, 14566694360769646102, 11490770385972866892, null, 4321216538010096301, 11686374104943943856 },
                SingleMember = (float)0.8741F,
                SingleArrayMember = new float[] { 0.9494F, 0.0221F, 0.3531F, 0.9842F, 0.0324F, 0.0717F, 0.5751F, 0.0938F, 0.4778F, 0.8598F },
                NullableSingleMember = (float?)0.3006F,
                NullableSingleArrayMember = new float?[] { 0.6707F, null, 0.4073F, null, 0.9157F, 0.7877F, 0.0238F, 0.6429F, null, 0.5998F },
                DoubleMember = (double)0.3054,
                DoubleArrayMember = new double[] { 0.6381, 0.2549, 0.6822, 0.7999, 0.429, 0.0667, 0.225, 0.7784, 0.9558, 0.0619 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.5902, 0.6457, null, null, 0.43, 0.3544, null, 0.5915, 0.0297, 0.7059 },
                DecimalMember = (decimal)0.147087719359942m,
                DecimalArrayMember = new decimal[] { 0.808660106644342m, 0.207054653301395m, 0.225743607257373m, 0.662257633480363m, 0.602731924319049m, 0.704322024576516m, 0.629415849516828m, 0.0798955243452897m, 0.343189514867584m, 0.021605191296714m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { null, 0.863598957128636m, 0.744305644530945m, 0.103608700495031m, 0.98169353137803m, 0.58064086110361m, 0.0882963380256185m, null, 0.62375254445884m, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-299818766),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-341522917), DateTime.Now.AddSeconds(234423819), DateTime.Now.AddSeconds(-286912258), DateTime.Now.AddSeconds(135011186), DateTime.Now.AddSeconds(-13592763), DateTime.Now.AddSeconds(-127871064), DateTime.Now.AddSeconds(71043452), DateTime.Now.AddSeconds(-256408199), DateTime.Now.AddSeconds(-160735498), DateTime.Now.AddSeconds(-6084988) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(221604155),
                NullableDateTimeArrayMember = new DateTime?[] { null, null, DateTime.Now.AddSeconds(143195667), DateTime.Now.AddSeconds(173711523), DateTime.Now.AddSeconds(-165801772), null, DateTime.Now.AddSeconds(-185634417), DateTime.Now.AddSeconds(-211416287), DateTime.Now.AddSeconds(201663795), DateTime.Now.AddSeconds(324420634) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"Cw$zRxFy'6",
                StringArrayMember = new string[] { "cavdE4JDE2", "ERq6PcepQo", "?t;I;0@FcQ", "P.6Al0@Nec", "x6?YB8FOj,", "Snib\"6kmmb", "8Ey,nRipVm", "gQL.;G3oGF", "u4sn$zbfH2", "PgxCqtIB87" },
                CharMember = (char)'Q',
                CharArrayMember = new char[] { '5', 'F', 'h', 't', '.', 'E', 'j', 'Z', '$', 'g' },
                NullableCharMember = (char?)'o',
                NullableCharArrayMember = new char?[] { 'o', 'F', null, null, '5', null, 'm', 'e', 'i', 'K' },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { false, false, true, true, true, true, true, false, false, true },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { null, false, false, false, true, false, true, true, true, false },
                ByteMember = (byte)249,
                ByteArrayMember = new byte[] { 44, 23, 103, 10, 29, 199, 120, 198, 135, 14 },
                NullableByteMember = (byte?)109,
                NullableByteArrayMember = new byte?[] { 163, null, 33, 44, 78, null, 3, 6, 39, null },
                SByteMember = (sbyte)17,
                SByteArrayMember = new sbyte[] { 6, -112, -52, -45, -15, 63, -14, -59, -89, 101 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { null, null, null, 43, null, null, -54, -56, -77, -122 },
                Int16Member = (short)-27992,
                Int16ArrayMember = new short[] { -4431, -12396, -32595, 27833, -12102, -1759, -15291, 17700, -23648, 13795 },
                NullableInt16Member = (short?)null,
                NullableInt16ArrayMember = new short?[] { -25391, -613, null, null, null, 30581, -11762, null, null, -24356 },
                UInt16Member = (ushort)40784,
                UInt16ArrayMember = new ushort[] { 58418, 4899, 53084, 58675, 39602, 56033, 25593, 28881, 37850, 1627 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 1155, null, null, 56947, 36020, 44072, 20061, 29582, 43596, 43887 },
                Int32Member = (int)50584381,
                Int32ArrayMember = new int[] { -1481878304, -605623498, 1608510765, -1936850516, -386943454, -124597942, 660966722, -5664305, -373470406, 1622406993 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { null, -904655813, null, null, null, -1189352569, null, null, -1098536860, 635454619 },
                UInt32Member = (uint)3553464174,
                UInt32ArrayMember = new uint[] { 1690853399, 1361914123, 2411919480, 3747002465, 1938768742, 2306603888, 633579294, 1205944412, 2766149064, 4219869546 },
                NullableUInt32Member = (uint?)934024456,
                NullableUInt32ArrayMember = new uint?[] { null, 958236747, 476273407, 1701070399, 2048276725, 1974654796, 1739560124, 937906638, null, 1219888958 },
                Int64Member = (long)1479619001806773945,
                Int64ArrayMember = new long[] { 6022361148557115897, 4592477416993095155, -6076197147674557911, 7314879882350996423, -7429657373596392787, -584069776416660198, -634897540958626654, 5175851528707190932, -7258913274831799558, -3528385189635423011 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { -814789408287415649, 746702299690355914, null, 6559216644050789094, null, -22041552650884885, null, -658705900707492610, 2599950416223273067, -6767966043214013314 },
                UInt64Member = (ulong)14399487496196810079,
                UInt64ArrayMember = new ulong[] { 5530126615578203565, 6909033532751192567, 12909808900493667599, 5843060796780234650, 13573523272600723917, 10177495730422166562, 15275106196721639043, 4500649479108150444, 4025845282022790283, 16087270166879952270 },
                NullableUInt64Member = (ulong?)2717967922218186378,
                NullableUInt64ArrayMember = new ulong?[] { null, 2855211373569838523, 14491103932427401748, null, null, 8988285805942965046, 4911776976464637291, 17334297122460915456, 5922726376268432248, 8144516946085199651 },
                SingleMember = (float)0.2825F,
                SingleArrayMember = new float[] { 0.8791F, 0.8178F, 0.0826F, 0.4624F, 0.2674F, 0.4724F, 0.4973F, 0.8048F, 0.1609F, 0.0321F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { null, null, null, null, null, null, null, null, null, 0.7513F },
                DoubleMember = (double)0.5005,
                DoubleArrayMember = new double[] { 0.5583, 0.3842, 0.0712, 0.4494, 0.0449, 0.1765, 0.2111, 0.0698, 0.9302, 0.6052 },
                NullableDoubleMember = (double?)0.8424,
                NullableDoubleArrayMember = new double?[] { 0.3872, 0.75, 0.692, 0.1752, 0.1743, 0.5735, 0.951, 0.511, 0.6436, null },
                DecimalMember = (decimal)0.398944000433639m,
                DecimalArrayMember = new decimal[] { 0.268725543408061m, 0.160462885238446m, 0.0471804044429122m, 0.299666831409404m, 0.954806138274635m, 0.244797376098483m, 0.625364077103028m, 0.449113440443349m, 0.276909579186193m, 0.597656323852789m },
                NullableDecimalMember = (decimal?)0.503172944534185m,
                NullableDecimalArrayMember = new decimal?[] { 0.33151743250504m, 0.789311345568538m, 0.780002800645308m, null, null, null, null, null, 0.0559663344435237m, 0.228019245540732m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-342998271),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(266379865), DateTime.Now.AddSeconds(115391107), DateTime.Now.AddSeconds(-240872390), DateTime.Now.AddSeconds(-136480960), DateTime.Now.AddSeconds(-224007086), DateTime.Now.AddSeconds(67332256), DateTime.Now.AddSeconds(116098546), DateTime.Now.AddSeconds(-297948538), DateTime.Now.AddSeconds(219264620), DateTime.Now.AddSeconds(-1083068) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-257785926),
                NullableDateTimeArrayMember = new DateTime?[] { null, DateTime.Now.AddSeconds(254936531), DateTime.Now.AddSeconds(-72736001), DateTime.Now.AddSeconds(10743032), DateTime.Now.AddSeconds(63300053), DateTime.Now.AddSeconds(-51700755), DateTime.Now.AddSeconds(-51437223), DateTime.Now.AddSeconds(70058235), DateTime.Now.AddSeconds(-3572527), DateTime.Now.AddSeconds(20465141) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"Zn\"#fBcvk.",
                StringArrayMember = new string[] { "C@fh3EiK;.", "?DipWApITT", "$23W3H,vI#", "X3!TzUAQVm", "L%K$@WFawe", "iUbR?WOJ1w", "Q9B!nJJO?C", "AZ94I s6ag", "Bk 1kCb0dd", "F:mwVY#Qg8" },
                CharMember = (char)'L',
                CharArrayMember = new char[] { 'y', 'Y', '$', 'j', '$', 'h', 'j', 'n', 'P', 's' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { 'j', 'W', null, null, 'w', 'M', 'P', '.', 'p', null },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, true, true, true, false, false, true, false, false },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { false, true, null, null, null, true, true, null, true, false },
                ByteMember = (byte)125,
                ByteArrayMember = new byte[] { 43, 207, 209, 57, 203, 71, 215, 161, 28, 132 },
                NullableByteMember = (byte?)135,
                NullableByteArrayMember = new byte?[] { 234, null, 250, 8, 236, 172, 170, null, 100, 23 },
                SByteMember = (sbyte)63,
                SByteArrayMember = new sbyte[] { -13, -28, -3, 117, 8, -91, 26, -64, 106, -114 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { 31, -69, null, -125, null, 85, 117, -102, 24, -67 },
                Int16Member = (short)5077,
                Int16ArrayMember = new short[] { -7581, -25181, 4452, -28252, 3312, 3187, 7614, -2057, -32239, -20479 },
                NullableInt16Member = (short?)-21495,
                NullableInt16ArrayMember = new short?[] { -18795, 7894, -27386, null, -17811, -26114, 10262, 1129, -19121, 28553 },
                UInt16Member = (ushort)61071,
                UInt16ArrayMember = new ushort[] { 44516, 46675, 43081, 25054, 34590, 51395, 1727, 59070, 20308, 47057 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 11915, 44690, 34008, 30701, 17914, null, 32833, 16079, null, 46022 },
                Int32Member = (int)983909216,
                Int32ArrayMember = new int[] { 797901355, -346683700, 776323630, 320968159, 648450590, -620063843, -165187079, -596971732, -2066696877, 267184988 },
                NullableInt32Member = (int?)1072051912,
                NullableInt32ArrayMember = new int?[] { null, null, null, -1647442228, -1258842211, -1084236197, 702244952, -1523337315, 610129082, -752728727 },
                UInt32Member = (uint)3200496238,
                UInt32ArrayMember = new uint[] { 2502087467, 2531070907, 1931702460, 1861003478, 922462827, 744484119, 834039021, 2589471327, 2034889747, 1425276825 },
                NullableUInt32Member = (uint?)326538551,
                NullableUInt32ArrayMember = new uint?[] { 1192924614, 2794305219, null, null, 2874953639, 1602819978, 1809085279, 1491086759, null, 371238376 },
                Int64Member = (long)626102740623026138,
                Int64ArrayMember = new long[] { 1236795139171654064, 4483845012493797148, -8750757075123853171, -6680202488119503339, -8905354987564967699, 602010933829648754, -6972992949100839763, -732339590433817846, -4941590761014495940, 5819874743646092607 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { null, 4453952252617661886, -3676665267619979371, null, -5932966702822147162, 6276027873172977584, null, null, null, null },
                UInt64Member = (ulong)5464718048872094758,
                UInt64ArrayMember = new ulong[] { 6987930894195199605, 13357020137047803119, 4094952865985391567, 7951994929199510087, 295385676538188667, 8891857919799143877, 4853843065756251519, 14204150335515083706, 7206652065371851496, 976461238902915794 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { null, 8613335682123449675, 11589196766151109835, null, 11137836595765400476, 9282784493936228003, 16544133434855991337, 10096551314549486401, 15246686508381095001, 8509437397597572153 },
                SingleMember = (float)0.0977F,
                SingleArrayMember = new float[] { 0.315F, 0.0216F, 0.1617F, 0.933F, 0.3293F, 0.8617F, 0.3895F, 0.5878F, 0.5859F, 0.4939F },
                NullableSingleMember = (float?)0.9807F,
                NullableSingleArrayMember = new float?[] { null, 0.6485F, null, 0.5063F, null, 0.6571F, null, null, null, null },
                DoubleMember = (double)0.9725,
                DoubleArrayMember = new double[] { 0.5191, 0.2779, 0.3975, 0.6777, 0.4105, 0.2598, 0.5453, 0.0071, 0.4346, 0.5062 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { null, 0.1402, null, 0.603, 0.8555, null, 0.5306, null, null, null },
                DecimalMember = (decimal)0.216522192217653m,
                DecimalArrayMember = new decimal[] { 0.137447269231755m, 0.146222051766804m, 0.561249113903031m, 0.0805694801176756m, 0.115305032634784m, 0.680951523911651m, 0.0744639714595228m, 0.861142306058268m, 0.548187860077334m, 0.0783268823653119m },
                NullableDecimalMember = (decimal?)0.618801581030154m,
                NullableDecimalArrayMember = new decimal?[] { null, 0.829055635644614m, null, null, null, null, 0.187354258348865m, 0.826942923863858m, 0.161864071694139m, 0.77770036634882m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-146081610),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-230828833), DateTime.Now.AddSeconds(-167737228), DateTime.Now.AddSeconds(77535146), DateTime.Now.AddSeconds(-179066315), DateTime.Now.AddSeconds(112476545), DateTime.Now.AddSeconds(-206064591), DateTime.Now.AddSeconds(-215536340), DateTime.Now.AddSeconds(-270386282), DateTime.Now.AddSeconds(-322902880), DateTime.Now.AddSeconds(-323606759) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-57518089),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(9404382), DateTime.Now.AddSeconds(-143088371), null, DateTime.Now.AddSeconds(291104201), null, DateTime.Now.AddSeconds(-219224959), null, DateTime.Now.AddSeconds(-182908139), DateTime.Now.AddSeconds(264956262), null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"N;kueJhuI;",
                StringArrayMember = new string[] { "1f5Uu2.Whk", "4f7j#J9Mi6", "CbZpTL bXa", "s4rnyxISBq", "lx5@D4FByU", "XULZ.ZFQAj", "2j G'A,U8W", "AqL'Vctelm", "F%0CCXsSzY", "r:So.ByzCD" },
                CharMember = (char)'P',
                CharArrayMember = new char[] { 's', 't', 'f', 'O', '9', '4', 'Z', 'q', 'l', 'r' },
                NullableCharMember = (char?)'s',
                NullableCharArrayMember = new char?[] { 'a', 'W', null, 'T', null, '2', null, null, ' ', null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, true, false, true, true, true, true, true, true, true },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { null, true, true, false, null, true, true, false, true, true },
                ByteMember = (byte)173,
                ByteArrayMember = new byte[] { 81, 110, 9, 226, 4, 155, 167, 214, 62, 51 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { 167, null, 199, 96, null, 253, 118, null, null, 26 },
                SByteMember = (sbyte)79,
                SByteArrayMember = new sbyte[] { -103, -18, -22, -128, 125, 96, 41, 111, -111, -31 },
                NullableSByteMember = (sbyte?)-69,
                NullableSByteArrayMember = new sbyte?[] { 28, -9, -117, 67, null, -63, 104, null, null, 91 },
                Int16Member = (short)-24998,
                Int16ArrayMember = new short[] { 4900, 13914, 2081, -16467, 29390, 1485, 23517, -20513, -15555, -5870 },
                NullableInt16Member = (short?)-10598,
                NullableInt16ArrayMember = new short?[] { null, -3970, 24219, -21325, -13427, 4995, null, null, -18872, null },
                UInt16Member = (ushort)36418,
                UInt16ArrayMember = new ushort[] { 11204, 55839, 20183, 8657, 59940, 63926, 35031, 27289, 20693, 3131 },
                NullableUInt16Member = (ushort?)32389,
                NullableUInt16ArrayMember = new ushort?[] { 59985, 50388, null, 27614, 22804, null, 13983, 15140, null, 63110 },
                Int32Member = (int)1067247711,
                Int32ArrayMember = new int[] { -1942480941, 831632808, -1913190389, 1073919447, 684922245, 986235910, 382474706, 1449102524, 302459067, 2098073739 },
                NullableInt32Member = (int?)1707912805,
                NullableInt32ArrayMember = new int?[] { -515722301, 963843402, null, -376521986, null, -777934721, 1974792365, 362812758, null, -753314501 },
                UInt32Member = (uint)3818214680,
                UInt32ArrayMember = new uint[] { 67784070, 4154742692, 1577713756, 920954250, 208695975, 3871760143, 2847923340, 874419516, 2497291650, 338718292 },
                NullableUInt32Member = (uint?)1306513583,
                NullableUInt32ArrayMember = new uint?[] { 3938534030, 3548964261, null, 2546432036, null, null, 3839678178, 1094246991, 3884867577, 3900460546 },
                Int64Member = (long)2381699004235035794,
                Int64ArrayMember = new long[] { -5941577686547144862, 2273313886048984014, -7020966325738188119, 6888159680612472829, -3262293883484205925, 2585654720320858723, -2463821830730009561, 2734111801153051087, -6259938128166614524, -1920005704896572776 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { 2431623038981933023, -4007225242056519259, -8668072395485773981, -6610644298573815067, 8915244506761443833, -9033944220405337510, null, null, 8520767647293790579, 8362737072198252296 },
                UInt64Member = (ulong)1403408260572589245,
                UInt64ArrayMember = new ulong[] { 14087659617802315599, 13701234111152945458, 11563227603311449824, 15850772132165940777, 14912105420134367569, 3288231208486926027, 8779451404931467527, 17329445652005525358, 8133202162976153727, 343601364171854051 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { null, 14254010950791168955, 18104831104208642101, 1439364408417969936, 14025834859878135770, 4185579590622678287, 14087761515701637571, null, null, null },
                SingleMember = (float)0.4669F,
                SingleArrayMember = new float[] { 0.6257F, 0.2484F, 0.9771F, 0.402F, 0.1336F, 0.1802F, 0.1278F, 0.7201F, 0.7677F, 0.4408F },
                NullableSingleMember = (float?)0.5393F,
                NullableSingleArrayMember = new float?[] { 0.9431F, 0.4444F, 0.8478F, 0.578F, 0.2789F, 0.1036F, null, 0.4998F, null, null },
                DoubleMember = (double)0.4976,
                DoubleArrayMember = new double[] { 0.6863, 0.0939, 0.1425, 0.7001, 0.9791, 0.2866, 0.2059, 0.1263, 0.3233, 0.6887 },
                NullableDoubleMember = (double?)0.4632,
                NullableDoubleArrayMember = new double?[] { null, 0.1466, 0.7406, null, null, null, 0.6229, null, 0.908, 0.7881 },
                DecimalMember = (decimal)0.635899484919337m,
                DecimalArrayMember = new decimal[] { 0.818845199802353m, 0.0608991221808359m, 0.24370049696588m, 0.0568930022683428m, 0.0167781305577504m, 0.408841884885375m, 0.311666171211594m, 0.130687527419388m, 0.0364072099497575m, 0.248380323987631m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.257053461045517m, 0.241123485491203m, null, null, 0.865231747210599m, 0.803770422844109m, 0.513430889469306m, null, null, 0.0291679338687882m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(347274020),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(242542093), DateTime.Now.AddSeconds(55623988), DateTime.Now.AddSeconds(101225583), DateTime.Now.AddSeconds(328451791), DateTime.Now.AddSeconds(-338154480), DateTime.Now.AddSeconds(323042907), DateTime.Now.AddSeconds(-284028955), DateTime.Now.AddSeconds(-120081193), DateTime.Now.AddSeconds(84002845), DateTime.Now.AddSeconds(-190755197) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(336194883),
                NullableDateTimeArrayMember = new DateTime?[] { null, DateTime.Now.AddSeconds(36906812), DateTime.Now.AddSeconds(-344182667), DateTime.Now.AddSeconds(289158395), DateTime.Now.AddSeconds(-136853394), null, DateTime.Now.AddSeconds(-173065166), DateTime.Now.AddSeconds(-2463639), DateTime.Now.AddSeconds(238561097), DateTime.Now.AddSeconds(-346158176) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"\"8PbU3JTDi",
                StringArrayMember = new string[] { "3y2ke46#Ds", "fsrj1IYuFT", "1\"bitU5Y5W", "OZORr\"M4KF", "S1vhvXQ3xL", "flO749XU:K", "%hO4eFeZ:8", "#qk A V\"9W", "DCyt;zLek5", "s'; H8n:Cp" },
                CharMember = (char)'I',
                CharArrayMember = new char[] { 'I', 'w', ';', '\'', '9', 'Y', 'u', ' ', 'D', 'y' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { null, ' ', 'V', 's', '4', null, null, ':', 'M', null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { false, false, true, false, false, true, true, false, true, false },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { null, true, null, null, false, null, true, true, false, null },
                ByteMember = (byte)141,
                ByteArrayMember = new byte[] { 54, 50, 115, 225, 4, 213, 193, 24, 195, 8 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { 83, 200, 109, 5, 227, 253, 56, 175, 246, null },
                SByteMember = (sbyte)-76,
                SByteArrayMember = new sbyte[] { 12, -41, -126, -101, 116, -93, -2, 30, -65, -38 },
                NullableSByteMember = (sbyte?)73,
                NullableSByteArrayMember = new sbyte?[] { -63, 26, -40, 33, 12, -7, 104, -66, -8, null },
                Int16Member = (short)9827,
                Int16ArrayMember = new short[] { 28745, 14854, 23750, 17265, 15498, -3289, 6050, 9031, -8848, 22809 },
                NullableInt16Member = (short?)5326,
                NullableInt16ArrayMember = new short?[] { null, 7344, null, null, 10528, -22696, null, null, null, 14935 },
                UInt16Member = (ushort)42311,
                UInt16ArrayMember = new ushort[] { 52317, 30837, 5430, 44094, 35089, 32237, 24061, 58520, 36311, 29534 },
                NullableUInt16Member = (ushort?)49772,
                NullableUInt16ArrayMember = new ushort?[] { null, 21731, 33683, 35315, 48999, 13670, 44245, 45449, 50987, null },
                Int32Member = (int)1075134170,
                Int32ArrayMember = new int[] { -1007161528, -1803528563, 590278808, 85247428, 1155017810, -107454785, 964569426, 1100654800, -1081515885, 522661045 },
                NullableInt32Member = (int?)-407864126,
                NullableInt32ArrayMember = new int?[] { 322991632, -2117048658, null, -664505858, -313069372, null, 642733371, -503464264, null, -133752455 },
                UInt32Member = (uint)1333274193,
                UInt32ArrayMember = new uint[] { 229822969, 2087319087, 4228510744, 1990831479, 1664550769, 2540998536, 2021034911, 2708017435, 316835433, 1102201368 },
                NullableUInt32Member = (uint?)2238439903,
                NullableUInt32ArrayMember = new uint?[] { 94655522, 3323260746, 2758613597, null, 1753876065, 1570229232, 2827744597, 1773408776, null, 2971638293 },
                Int64Member = (long)-7684765388799419509,
                Int64ArrayMember = new long[] { 473588649368843977, 1138232605383063217, 3341619883771155778, 2223302498862967788, -9059157821819573957, -4904723690932316315, -576187335693085680, -1211827229338494185, -2658697656453857252, 28224769467616398 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { 4983307149285426120, -245119981437303591, null, -315476187421398640, null, null, 368639797481513399, 1835663525243794596, 2311381232544034325, 2484180935995810493 },
                UInt64Member = (ulong)8362891335125551413,
                UInt64ArrayMember = new ulong[] { 7868308843871478016, 3847221507729189202, 8195341353316662914, 9280180728643432914, 9051103566344397084, 2485395336142477892, 5542467086243734372, 9095558906167661669, 9761625696157822751, 17045529222052644003 },
                NullableUInt64Member = (ulong?)13209072739330089236,
                NullableUInt64ArrayMember = new ulong?[] { null, 13380512637706057256, 7960767285793754134, 616236504687298745, 5730807687338357893, 15463436777735530891, 18266775119896821707, 3168370632859525431, 17222148268101899794, 14026741884587977259 },
                SingleMember = (float)0.8182F,
                SingleArrayMember = new float[] { 0.5109F, 0.7688F, 0.4027F, 0.8624F, 0.236F, 0.1411F, 0.6107F, 0.1839F, 0.3603F, 0.0151F },
                NullableSingleMember = (float?)0.7273F,
                NullableSingleArrayMember = new float?[] { null, 0.2975F, null, null, null, null, 0.6637F, null, 0.9523F, null },
                DoubleMember = (double)0.0616,
                DoubleArrayMember = new double[] { 0.004, 0.9885, 0.7139, 0.0134, 0.3007, 0.9129, 0.5735, 0.4515, 0.8647, 0.7345 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.4064, 0.8169, 0.2271, 0.5021, 0.1911, 0.79, 0.7977, 0.2553, 0.5306, 0.792 },
                DecimalMember = (decimal)0.838462276774674m,
                DecimalArrayMember = new decimal[] { 0.319279127902947m, 0.299769104132321m, 0.918705408423536m, 0.987489438144252m, 0.903251157562831m, 0.438564486540186m, 0.0422971062559155m, 0.487288737896499m, 0.834626385399432m, 0.228122783931029m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.983526032876003m, 0.469545166692485m, 0.0906526749444439m, null, 0.245318361672255m, 0.216177148379468m, 0.318777179959592m, 0.451648936351598m, 0.983179891008502m, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid(), null, null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-70983948),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-121020853), DateTime.Now.AddSeconds(241573274), DateTime.Now.AddSeconds(-267660303), DateTime.Now.AddSeconds(-215444973), DateTime.Now.AddSeconds(-29230748), DateTime.Now.AddSeconds(-146778681), DateTime.Now.AddSeconds(-35969880), DateTime.Now.AddSeconds(306588796), DateTime.Now.AddSeconds(36361475), DateTime.Now.AddSeconds(161169691) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(200591913),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(157775612), null, null, null, DateTime.Now.AddSeconds(-2695956), DateTime.Now.AddSeconds(-123772308), DateTime.Now.AddSeconds(-113071407), DateTime.Now.AddSeconds(-46419602), null, null },
            },
        };
    }
}
