// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
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
                StringMember = "7,JW.Ts9Oe",
                StringArrayMember = new string[] { "MT$jTcg5gh", "5t:\"12SFHz", "ljJgxIyuLs", "eJ'z#iw y1", "HQ:6RnAvh#", "l'V.t.FIwr", "WPW@,EvGgu", "QRZEy'k$'Y", "Z;6\"h1j9#p", "6@'FU7rykU" },
                CharMember = '9',
                CharArrayMember = new char[] { 'j', 'Z', 'o', '"', '\'', '?', 'f', '7', '!', 'q' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, 'Y', 'B', 'h', 'f', 's', 'p', 'h', 'f', 'D' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, false, true, true, true, false, true, true, true, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, false, true, true, null, true, null, null, true },
                ByteMember = 196,
                ByteArrayMember = new byte[] { 192, 131, 72, 142, 39, 91, 191, 181, 187, 20 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, 148, null, 30, 36, null, null, 158, 129, null },
                SByteMember = -127,
                SByteArrayMember = new sbyte[] { -113, -106, -58, -21, 104, 14, 12, -97, 100, 36 },
                NullableSByteMember = 120,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -8, -67, 110, 27, null, 3, 78, null, -77, -94 },
                Int16Member = -25875,
                Int16ArrayMember = new short[] { -12404, -31618, 30715, 25933, 6781, 15767, 17473, -4730, -22428, -6869 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -11983, 28744, 8698, -13821, 29264, null, null, -2510, 11270, -6440 },
                UInt16Member = 812,
                UInt16ArrayMember = new ushort[] { 19960, 30992, 9530, 1266, 390, 60312, 40912, 9345, 12389, 15495 },
                NullableUInt16Member = 15018,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 62750, 7423, null, null, null, 6037, 48549, null, null, null },
                Int32Member = -1554645418,
                Int32ArrayMember = new int[] { -1222995617, 194861004, 1367328508, -2097288754, 1473321800, 658415150, 984799376, -1800052595, -2131464496, -1161282581 },
                NullableInt32Member = 741213483,
                NullableInt32ArrayMember = new Nullable<int>[] { null, -765191788, 1833258617, 2071997231, 1368547217, 1561034351, -345583669, 438994585, null, null },
                UInt32Member = 1985373671,
                UInt32ArrayMember = new uint[] { 604366994, 1243567913, 3590480205, 3533236425, 2602057797, 3924853269, 846483539, 207761319, 954731869, 933263905 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3393875857, 1171290111, null, 2483601779, 365391049, 3140311053, 1095131589, 352263918, null, 2119008525 },
                Int64Member = -6679823617724912272,
                Int64ArrayMember = new long[] { 1324421151667723956, -2801476130415203248, 1214550844836864342, -3215298019355727783, 2052725371074317096, 2566559393745057751, 221010047625445240, 940744546778331783, 2952853073903684702, 8375883602549583234 },
                NullableInt64Member = -7876766785099346544,
                NullableInt64ArrayMember = new Nullable<long>[] { 7486304395764834070, -633069001684965060, -5965839173947955957, -3751073374817029662, -9139017324672188989, null, null, -326206131731480847, 7667688006660018650, -464981690121157664 },
                UInt64Member = 3782981648413084566,
                UInt64ArrayMember = new ulong[] { 10478178523746810223, 7578088547958618151, 10951753772880713945, 1412437486822431959, 987661484975757958, 2315383074967691878, 6314430609411291700, 13252612063695478212, 5699185315975821564, 9516637546490373809 },
                NullableUInt64Member = 4899232770586830050,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 16117050338076647685, null, 8465913031371436176, 10813373548497913140, 16655880414722633116, 5631614119726094995, 10945686461128458257, 3995543049751910072, 15993116778055530483, 3241768126608739545 },
                SingleMember = 0.2563F,
                SingleArrayMember = new float[] { 0.6354F, 0.4812F, 0.5362F, 0.2158F, 0.5381F, 0.9005F, 0.3969F, 0.2055F, 0.2538F, 0.3192F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.6213F, 0.5138F, 0.2426F, 0.9934F, 0.4807F, 0.8421F, 0.4955F, 0.0638F, 0.7061F, 0.4236F },
                DoubleMember = 0.853,
                DoubleArrayMember = new double[] { 0.1554, 0.0402, 0.0951, 0.1985, 0.5098, 0.4715, 0.7649, 0.0037, 0.8609, 0.3285 },
                NullableDoubleMember = 0.8647,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.6549, 0.9505, null, 0.9947, 0.8235, 0.8269, 0.2248, null, 0.8343, 0.8477 },
                DecimalMember = 0.0609859539179061m,
                DecimalArrayMember = new decimal[] { 0.0877852207242954m, 0.352304489748684m, 0.396106433479958m, 0.511362874089398m, 0.109338596811727m, 0.981068335989648m, 0.772847614681899m, 0.240997994302423m, 0.763963094739049m, 0.960029782766243m },
                NullableDecimalMember = 0.397824600013029m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, 0.0903610893905718m, 0.940152847333384m, 0.0836279719501938m, 0.365973867059656m, 0.630073331026525m, 0.00609214351698128m, null, null, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(159057153),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(263475297), DateTime.Now.AddSeconds(4835664), DateTime.Now.AddSeconds(309861864), DateTime.Now.AddSeconds(-325134007), DateTime.Now.AddSeconds(125624602), DateTime.Now.AddSeconds(349629711), DateTime.Now.AddSeconds(271644593), DateTime.Now.AddSeconds(28206155), DateTime.Now.AddSeconds(46888348), DateTime.Now.AddSeconds(297772514) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-30550397),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(205883699), null, null, DateTime.Now.AddSeconds(206910836), DateTime.Now.AddSeconds(-112421700), DateTime.Now.AddSeconds(-171066069), DateTime.Now.AddSeconds(-18953941), DateTime.Now.AddSeconds(40022878), null },
            },
            new DataConvertersTestClass {
                StringMember = "Hs401dF55F",
                StringArrayMember = new string[] { "RS$6:;.h1P", ",ctVVk#2un", "#bPD@\"a!d:", "edWeaY6\"UC", "Ng;!wA3q#D", "40Pgp'RA$a", "ticwrZ7Nmg", "KU.u1ZZkMU", "Z7vSn?,k@D", "8ykwGgX?8b" },
                CharMember = '$',
                CharArrayMember = new char[] { 'W', 'F', 'A', 'y', '9', '"', 't', '5', 'U', 'Z' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { ' ', 'j', '3', null, null, 'X', null, null, 'A', ' ' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, false, false, true, true, true, false, false, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, null, false, true, null, false, false, true, false, null },
                ByteMember = 163,
                ByteArrayMember = new byte[] { 184, 149, 20, 93, 122, 97, 153, 202, 73, 25 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 66, null, 78, 252, null, 210, 161, 149, 217, 37 },
                SByteMember = -114,
                SByteArrayMember = new sbyte[] { -121, -49, 41, -27, -104, 112, 47, -61, -64, 69 },
                NullableSByteMember = 45,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -44, -7, -126, -79, -60, 117, -65, 71, 120, 78 },
                Int16Member = -3636,
                Int16ArrayMember = new short[] { 5140, 23986, 29106, 7704, 15423, -17986, -14190, -19689, -7810, -24300 },
                NullableInt16Member = 25684,
                NullableInt16ArrayMember = new Nullable<short>[] { null, -1246, 11496, 23987, -23619, -15030, null, 21427, null, 27025 },
                UInt16Member = 44339,
                UInt16ArrayMember = new ushort[] { 65241, 27056, 32058, 21262, 63019, 50092, 22859, 50577, 47848, 57998 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 34893, 21516, 3495, null, null, 54608, null, 61977, 63486 },
                Int32Member = 1020183324,
                Int32ArrayMember = new int[] { -437570798, -1779299950, -1814038244, 928471796, 254890199, 667134077, 254056551, -1508700132, -1954154994, -1514073364 },
                NullableInt32Member = 659029207,
                NullableInt32ArrayMember = new Nullable<int>[] { -41937513, 238653147, -544135880, null, null, null, -1810859871, -1908055492, 660994352, 1007147370 },
                UInt32Member = 3958709462,
                UInt32ArrayMember = new uint[] { 1335217463, 4111344168, 576223759, 1316651151, 2789404065, 1797883720, 3903298027, 317791929, 1906508994, 2906599226 },
                NullableUInt32Member = 1631730718,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1715303198, 455756231, null, null, 1465700074, 1630499661, 1842128746, 40491580, 1631315986, 3919499979 },
                Int64Member = -600186957470434808,
                Int64ArrayMember = new long[] { -5643125865019565810, 1760345014661864906, 280095871467411572, -8573635673676544262, 8760665398918403562, -7206601712421909459, -1577690654364779010, 814258381863501381, -449309771910755828, 5414618588428137615 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -5830698215096836447, -5762313714560547788, null, 7420068260054843467, 5505296635117504519, 8915005153089308955, 7762771569413779243, -5745890555328009601, -5074724962962585165, null },
                UInt64Member = 9699777195634806768,
                UInt64ArrayMember = new ulong[] { 14541694723212257520, 231984246132756480, 11157098673670326278, 4483159307814363370, 629583528176166863, 5460056609805632464, 4932576995239513465, 9415841003762325584, 5482679963592782961, 13387495461495456062 },
                NullableUInt64Member = 17678551735303372217,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 10931292978622108097, 6891986339913516181, 4942346341874683863, 8481829614606196308, null, null, 8801361725817072975, 12200734140595675190, 6179766889315817940, 12391790586133508501 },
                SingleMember = 0.4236F,
                SingleArrayMember = new float[] { 0.5577F, 0.7195F, 0.116F, 0.6854F, 0.4252F, 0.89F, 0.6203F, 0.7087F, 0.0987F, 0.7164F },
                NullableSingleMember = 0.9079F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.3685F, 0.6984F, null, 0.9784F, 0.3634F, 0.4714F, null, 0.6654F, 0.0377F, null },
                DoubleMember = 0.4234,
                DoubleArrayMember = new double[] { 0.5882, 0.5387, 0.5367, 0.481, 0.4774, 0.0266, 0.7068, 0.1798, 0.9786, 0.672 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.4305, 0.1431, 0.0959, null, 0.0364, 0.1458, 0.2328, null, null },
                DecimalMember = 0.764653397244759m,
                DecimalArrayMember = new decimal[] { 0.391681539982391m, 0.696393608459747m, 0.373592511925947m, 0.683005073654841m, 0.796052171411614m, 0.0101610604536266m, 0.830632973296821m, 0.371131585750982m, 0.912171001689022m, 0.537855740789796m },
                NullableDecimalMember = 0.425573614450333m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.443258811386467m, 0.805781577722269m, 0.204441065461888m, null, 0.258450771724771m, 0.835985181316759m, null, null, 0.72332497296675m, 0.790913092915404m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-315611901),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(318358380), DateTime.Now.AddSeconds(-150171025), DateTime.Now.AddSeconds(43664209), DateTime.Now.AddSeconds(-302153459), DateTime.Now.AddSeconds(-50848789), DateTime.Now.AddSeconds(-255515833), DateTime.Now.AddSeconds(77911844), DateTime.Now.AddSeconds(-97125667), DateTime.Now.AddSeconds(217948469), DateTime.Now.AddSeconds(83016234) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-338547746), null, DateTime.Now.AddSeconds(-26731150), DateTime.Now.AddSeconds(154960299), null, DateTime.Now.AddSeconds(136372491), DateTime.Now.AddSeconds(154378499), DateTime.Now.AddSeconds(267800201), null, DateTime.Now.AddSeconds(-336552802) },
            },
            new DataConvertersTestClass {
                StringMember = "tkiyT2%j9B",
                StringArrayMember = new string[] { "Ht@T2v3HhG", "HCNf$d?Ib3", "%k EibWXFD", "2IxYwSImTs", "h?ba9z?,j6", "NXphMulHle", "1%!ighory8", "@fSNY:W%5d", "RlR\"4JdmTz", "0%ic,iQSI%" },
                CharMember = 'h',
                CharArrayMember = new char[] { '"', 'N', 'D', '.', '@', 'z', 'l', '!', 'g', '1' },
                NullableCharMember = ';',
                NullableCharArrayMember = new Nullable<char>[] { 'p', 'F', null, '#', 'A', '"', null, null, null, null },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, false, false, false, true, false, true, false, true, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, true, false, null, false, false, false, true, null, null },
                ByteMember = 193,
                ByteArrayMember = new byte[] { 27, 48, 34, 175, 99, 200, 193, 154, 245, 143 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, 247, 173, 169, 108, 109, null, null, null, null },
                SByteMember = 113,
                SByteArrayMember = new sbyte[] { 51, 95, 122, 75, -101, 69, 114, -62, 92, -24 },
                NullableSByteMember = 66,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 112, null, 45, -16, null, null, -16, null, -13, -123 },
                Int16Member = 28867,
                Int16ArrayMember = new short[] { 11645, 5477, 21855, -14202, 1905, 2068, -5575, 27423, -14862, 9315 },
                NullableInt16Member = -13336,
                NullableInt16ArrayMember = new Nullable<short>[] { -22708, -15121, null, null, 7621, null, 12107, -8271, 8869, -31065 },
                UInt16Member = 31018,
                UInt16ArrayMember = new ushort[] { 55717, 3241, 2394, 55245, 63828, 31201, 2969, 23646, 60829, 8289 },
                NullableUInt16Member = 11546,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 1213, 52302, null, null, 61387, 64651, null, 12662, 55285, 32221 },
                Int32Member = 1531388653,
                Int32ArrayMember = new int[] { 433236040, 552433219, 1978580064, -1732829941, 837943600, 2142752710, 1905830471, -2062775136, 1510178658, 860263105 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { null, null, 1181412766, 1283523543, null, null, null, 172083962, -34310071, 1605778390 },
                UInt32Member = 2443029598,
                UInt32ArrayMember = new uint[] { 2691387047, 4226451326, 1404828160, 3996722920, 1786717705, 2050588275, 4272915006, 3391033374, 2833490393, 1440169715 },
                NullableUInt32Member = 3123903859,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 3891379350, null, null, null, 1182352102, 2160288353, 2750648191, 1126077260, null },
                Int64Member = 1485435465640782398,
                Int64ArrayMember = new long[] { 4496303861383931460, 1196347860434518896, -3145889269480756568, 3800657861040194453, 9090365742260702099, 9107038378949137420, 3241617210653128903, -4792151205654730134, 8474807247322877765, -3574780512644209091 },
                NullableInt64Member = -5620516487785665407,
                NullableInt64ArrayMember = new Nullable<long>[] { -7246419958795398666, null, 8892935781829524427, 6726767416298005741, null, 1042951437591782907, 884535371296235822, -5320811813202948853, -5805203616986195563, -4738723314966926903 },
                UInt64Member = 11041642777813342546,
                UInt64ArrayMember = new ulong[] { 13063199397180451868, 6034261034028304414, 2518387640464626792, 11537751029085644927, 2630955753797915784, 15003230399182045371, 8185572487228163024, 6653169815789797406, 9697497655419679816, 10684637242254052398 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 8271617908878619137, 5758656931159869318, 6740155382418501580, 7806526242976958367, 14230846979215119829, 9680956233824176448, 4864740220937645505, 11498519030128616098, null, 11660534082908856241 },
                SingleMember = 0.5322F,
                SingleArrayMember = new float[] { 0.5836F, 0.0383F, 0.3074F, 0.6648F, 0.0431F, 0.3617F, 0.5552F, 0.2711F, 0.0244F, 0.5392F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.8941F, 0.7615F, 0.2443F, 0.485F, 0.8854F, 0.9701F, 0.5738F, null, null, null },
                DoubleMember = 0.313,
                DoubleArrayMember = new double[] { 0.2809, 0.0293, 0.7189, 0.6359, 0.5415, 0.9901, 0.1166, 0.5734, 0.542, 0.9051 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { null, null, 0.2887, 0.982, null, 0.5017, null, null, null, 0.6723 },
                DecimalMember = 0.00121690414998565m,
                DecimalArrayMember = new decimal[] { 0.868044557042969m, 0.852863289919212m, 0.271336127385665m, 0.148754829477699m, 0.501774105856954m, 0.44481569505152m, 0.706470584607495m, 0.446687183041714m, 0.895056548861397m, 0.730986073933413m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.972155122273344m, 0.607789008034819m, 0.051440943237715m, 0.196057693101215m, 0.992157849198945m, null, null, 0.456294570460163m, null, 0.830515138923452m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-349950316),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(57615735), DateTime.Now.AddSeconds(-174006363), DateTime.Now.AddSeconds(-129370968), DateTime.Now.AddSeconds(-156894909), DateTime.Now.AddSeconds(-186734346), DateTime.Now.AddSeconds(-97811550), DateTime.Now.AddSeconds(-338777948), DateTime.Now.AddSeconds(284222983), DateTime.Now.AddSeconds(-346397267), DateTime.Now.AddSeconds(-217092471) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-131146799),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(348032734), DateTime.Now.AddSeconds(274770187), null, null, DateTime.Now.AddSeconds(81019938), DateTime.Now.AddSeconds(53858899), DateTime.Now.AddSeconds(332649645), DateTime.Now.AddSeconds(-279958558), DateTime.Now.AddSeconds(-116652492) },
            },
            new DataConvertersTestClass {
                StringMember = "K$XIR CV\"O",
                StringArrayMember = new string[] { "SFooiS#q,1", "GrP0Hg\"DSm", ".$9'PrWI4h", "bgCP2wguG5", "pCvE,h I5J", "r;3qAqVl;z", "UR1#AK$Fav", "50V@P.1N1H", ";uz.UnS,IT", "VV0dSP3S2r" },
                CharMember = 'L',
                CharArrayMember = new char[] { 'y', 'e', 'a', 'V', 'H', 'I', '9', 'O', 'f', 'k' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'N', null, 's', 'D', null, 't', 'l', 'D', '4', null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, true, true, true, true, true, false, false, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, null, true, true, true, false, null, true, true },
                ByteMember = 188,
                ByteArrayMember = new byte[] { 243, 175, 86, 28, 22, 205, 144, 121, 92, 40 },
                NullableByteMember = 52,
                NullableByteArrayMember = new Nullable<byte>[] { 45, 54, 176, 39, 84, 152, 161, 149, 115, 210 },
                SByteMember = -112,
                SByteArrayMember = new sbyte[] { 76, 83, -84, 125, 59, -35, -103, 119, -46, -55 },
                NullableSByteMember = 11,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -32, 44, 61, 98, null, 90, -75, null, -6, null },
                Int16Member = -6193,
                Int16ArrayMember = new short[] { 14853, -16754, -8944, -3238, -29399, -16923, -8646, 15258, -28936, 8029 },
                NullableInt16Member = 21660,
                NullableInt16ArrayMember = new Nullable<short>[] { -18972, 657, -1066, 31258, -4300, -1633, null, null, 26963, -1463 },
                UInt16Member = 15993,
                UInt16ArrayMember = new ushort[] { 41103, 22090, 64324, 12236, 51326, 65339, 63003, 52325, 56889, 27853 },
                NullableUInt16Member = 37600,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 626, 39982, 1349, 47022, 36269, null, null, 6389, 52573, null },
                Int32Member = -1132994539,
                Int32ArrayMember = new int[] { 545712190, -2520647, 1678286517, 1755712810, -420682363, -651037024, -150735784, -2029934693, 3086944, 1773614956 },
                NullableInt32Member = -113284555,
                NullableInt32ArrayMember = new Nullable<int>[] { null, null, -1290991456, 40179659, 1266499182, -1310802409, -872159158, 506053709, 1250348385, 1608294084 },
                UInt32Member = 1091524267,
                UInt32ArrayMember = new uint[] { 2323900755, 302359659, 2238411590, 4213706328, 1558330179, 3517123341, 1948847867, 1167922580, 2674809087, 1691680470 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2334602453, 2881737654, null, 557263021, 1726055448, null, 4205599496, 3323986183, 3198229539, 437727958 },
                Int64Member = -5853154713074292494,
                Int64ArrayMember = new long[] { -8501984134325732424, 385711856542578190, 5478463283565827869, 2242199021834517462, -6396815110698082842, -8549872432122344268, 1378813511235116208, 6472457320720873668, -1244753937107093757, -45968897787423173 },
                NullableInt64Member = 7004165161958299503,
                NullableInt64ArrayMember = new Nullable<long>[] { -1800312981191706575, null, 7406705904380022703, -3013069531073192872, 7032110181313555135, 780438080901883652, -5817688749355816297, -3709792544593987944, 6341136436945549304, 2727204447526113422 },
                UInt64Member = 6819538636299614639,
                UInt64ArrayMember = new ulong[] { 8667761838085022170, 3299054378410526342, 5259582172729158849, 11194828104998227401, 947232673304832910, 10298548125850232978, 17602340453515997839, 5545611058929366578, 17210611845635970706, 18196157132593714837 },
                NullableUInt64Member = 6513196979346776650,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, null, 14049618769078862929, null, 12935354567078522183, 13961574566629722402, 9983906089546653426, 12652825848663316038, 4324126645301815784, 6510284281445798134 },
                SingleMember = 0.1986F,
                SingleArrayMember = new float[] { 0.5251F, 0.2295F, 0.0706F, 0.4934F, 0.9154F, 0.3838F, 0.97F, 0.0496F, 0.0658F, 0.8068F },
                NullableSingleMember = 0.2044F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.8496F, 0.1183F, 0.5862F, 0.0897F, null, 0.3878F, null, 0.2866F, 0.4341F, 0.4754F },
                DoubleMember = 0.5875,
                DoubleArrayMember = new double[] { 0.2659, 0.717, 0.2782, 0.8513, 0.1376, 0.7122, 0.0385, 0.5401, 0.0949, 0.9722 },
                NullableDoubleMember = 0.7858,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.7633, 0.4771, null, null, 0.2856, null, 0.0641, null, null, 0.2411 },
                DecimalMember = 0.987216079381165m,
                DecimalArrayMember = new decimal[] { 0.673191419021838m, 0.359312203206879m, 0.717425038591851m, 0.967212354648882m, 0.635374931233415m, 0.768869464739306m, 0.891784751316077m, 0.596832221623112m, 0.731240135673041m, 0.899148645650358m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.191937113805525m, 0.376544458091494m, null, null, null, 0.520321531188219m, null, null, 0.245727709383183m, 0.945826419711101m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, null, null },
                DateTimeMember = DateTime.Now.AddSeconds(-20737951),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(346027211), DateTime.Now.AddSeconds(295290202), DateTime.Now.AddSeconds(-197004326), DateTime.Now.AddSeconds(-111446470), DateTime.Now.AddSeconds(-191577846), DateTime.Now.AddSeconds(79828356), DateTime.Now.AddSeconds(242643632), DateTime.Now.AddSeconds(329498013), DateTime.Now.AddSeconds(-228685619), DateTime.Now.AddSeconds(303080757) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-131769933),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(55931914), DateTime.Now.AddSeconds(-187027749), DateTime.Now.AddSeconds(152327201), DateTime.Now.AddSeconds(34757170), DateTime.Now.AddSeconds(233419991), DateTime.Now.AddSeconds(-20061937), null, DateTime.Now.AddSeconds(117635002), DateTime.Now.AddSeconds(280137875), DateTime.Now.AddSeconds(-342321427) },
            },
            new DataConvertersTestClass {
                StringMember = "#l47zDTbbL",
                StringArrayMember = new string[] { "epsKKqP6Ut", "7Y%T@%aolZ", "0dkZvq2dic", "n7deQWkZMB", "9s#xkQt8HP", ",JjtH Jq!r", "co9GF2eur1", "49B$Ks2$%g", "kg!,;N#YIq", ".QcNoY9cwJ" },
                CharMember = 'M',
                CharArrayMember = new char[] { 'u', '1', 'u', ':', 'c', 'H', '\'', '"', 'W', 'G' },
                NullableCharMember = 'Z',
                NullableCharArrayMember = new Nullable<char>[] { '2', 'w', null, null, 'H', 't', '8', 'f', 'B', '"' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, false, true, false, false, false, false, true, false, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, true, true, true, false, false, false, null, false, true },
                ByteMember = 22,
                ByteArrayMember = new byte[] { 84, 149, 80, 67, 196, 147, 245, 207, 60, 130 },
                NullableByteMember = 90,
                NullableByteArrayMember = new Nullable<byte>[] { 1, null, 247, 154, 148, null, 68, 158, 79, 72 },
                SByteMember = 19,
                SByteArrayMember = new sbyte[] { 97, 69, -29, -74, 31, 21, 81, -53, -35, 64 },
                NullableSByteMember = 83,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -44, 33, -97, -65, null, -26, null, 111, 111 },
                Int16Member = -12409,
                Int16ArrayMember = new short[] { 21240, 30083, 18821, 22472, 8604, -28703, -5600, 5286, 14994, 12499 },
                NullableInt16Member = -32746,
                NullableInt16ArrayMember = new Nullable<short>[] { null, 10821, -23894, -25482, null, -16751, -4951, -25283, null, -19093 },
                UInt16Member = 13296,
                UInt16ArrayMember = new ushort[] { 30949, 24509, 29740, 31921, 28698, 21588, 44191, 14199, 39292, 22034 },
                NullableUInt16Member = 19697,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 51400, 51587, null, 14367, 8810, 58738, 56364, null, 9684, null },
                Int32Member = -2133826531,
                Int32ArrayMember = new int[] { 908942265, 1942696629, 1979671374, -880326789, 1970839700, 957485513, -1964009011, -1535783, -226688237, 853680273 },
                NullableInt32Member = 685290069,
                NullableInt32ArrayMember = new Nullable<int>[] { 1959273338, -1547525217, null, -533506538, -1749995878, 2080110640, null, -426874404, 2039275748, null },
                UInt32Member = 2975833561,
                UInt32ArrayMember = new uint[] { 2559220092, 41745628, 2560592567, 28068049, 4080004683, 3532847407, 2561245693, 1698811354, 324783396, 4237084569 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 2581744574, null, 1422899082, null, 1584594223, 1195674221, 150277847, 3927264593, 71657626 },
                Int64Member = 3325688168688099311,
                Int64ArrayMember = new long[] { 8748403647782344397, -1689816547804349314, 7126943324881001703, 7063617169952389400, 2665347662719131212, 907708907485115690, 6819193566696934750, -94823859287843924, 5552057445174341934, -6409783524738198706 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, null, -5059782727420050252, -7086084717785175486, null, 2650664109763060891, null, null, -441752187801474057, 8585599512039340717 },
                UInt64Member = 17182389556390900487,
                UInt64ArrayMember = new ulong[] { 13407518704869915097, 15971639497551002299, 4565862023158806608, 2747457915394430859, 18343757199089491617, 14748281526394062351, 14653116471046154816, 4277814570798529097, 6874142098072559651, 4366478355972097839 },
                NullableUInt64Member = 7179938477734187664,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 14858722680671431571, 9468059598982534362, null, 16654020103560526920, 4000529958782889120, 12141362110514104770, null, 18379105892609961870, 9665421361807677651 },
                SingleMember = 0.0886F,
                SingleArrayMember = new float[] { 0.8675F, 0.3233F, 0.7886F, 0.1215F, 0.5626F, 0.6894F, 0.9144F, 0.7089F, 0.8224F, 0.1076F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.1361F, null, 0.1195F, 0.3278F, 0.8459F, 0.1389F, 0.1989F, 0.781F, 0.4673F, 0.0315F },
                DoubleMember = 0.6557,
                DoubleArrayMember = new double[] { 0.5384, 0.622, 0.4244, 0.2358, 0.3692, 0.065, 0.9356, 0.2364, 0.9374, 0.5974 },
                NullableDoubleMember = 0.0976,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.6347, null, 0.7186, null, null, 0.4591, 0.3962, 0.4259, null, 0.1275 },
                DecimalMember = 0.755527230226122m,
                DecimalArrayMember = new decimal[] { 0.240193497242369m, 0.97989511414109m, 0.755842581163093m, 0.167658539741156m, 0.553384226676342m, 0.0718211212016677m, 0.750786266786063m, 0.928593460289432m, 0.12579003045698m, 0.703213477910797m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.28989075263987m, null, 0.205925497374849m, null, null, null, 0.131901577842918m, 0.257880742407026m, 0.849077116065532m, 0.920320827777768m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-291249078),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(111264563), DateTime.Now.AddSeconds(318065912), DateTime.Now.AddSeconds(196930604), DateTime.Now.AddSeconds(303606270), DateTime.Now.AddSeconds(50889078), DateTime.Now.AddSeconds(92846952), DateTime.Now.AddSeconds(25720860), DateTime.Now.AddSeconds(-74430551), DateTime.Now.AddSeconds(195240878), DateTime.Now.AddSeconds(328449419) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(147259596),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-78285669), DateTime.Now.AddSeconds(315710020), null, null, null, DateTime.Now.AddSeconds(-219908697), null, null, null, DateTime.Now.AddSeconds(159384475) },
            },
            new DataConvertersTestClass {
                StringMember = "jo,f#LzOLe",
                StringArrayMember = new string[] { "tD8.C2UHqs", "4oTI:\"UA5V", "OAXnj;6qIj", "T\"hehwaXY6", "?e0%ElK3c@", "JFB9@hQ6DO", "6XpFsgNdbH", "ncdE,IC%aG", "kFeKi:lPXS", "nGXN,Y806Z" },
                CharMember = '%',
                CharArrayMember = new char[] { 'N', 'G', 'e', 'k', 'R', 'M', '4', 'J', 'M', 'g' },
                NullableCharMember = '8',
                NullableCharArrayMember = new Nullable<char>[] { 'E', 'n', null, 'C', 'I', null, 'Q', ';', null, 'E' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, false, false, false, true, false, false, true, false, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, null, null, null, true, false, true, null, true, true },
                ByteMember = 58,
                ByteArrayMember = new byte[] { 98, 73, 94, 173, 75, 111, 233, 40, 250, 1 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 165, null, 233, null, 103, null, 2, 74, 88, 214 },
                SByteMember = 100,
                SByteArrayMember = new sbyte[] { 88, 36, 104, 126, -13, -11, -97, -44, -74, 44 },
                NullableSByteMember = 103,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, null, -68, null, null, -100, -113, 82, -64, 21 },
                Int16Member = 28197,
                Int16ArrayMember = new short[] { 20608, 30183, 26301, 10853, 30035, -10488, -13107, 15945, -28908, -10811 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -20514, -23074, -17049, null, 692, null, null, -7048, -32760, null },
                UInt16Member = 40973,
                UInt16ArrayMember = new ushort[] { 48516, 52213, 30019, 11023, 11626, 53683, 37711, 4168, 29530, 58387 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 49573, 8731, 40272, 63257, 13776, 22701, 45562, 35678, null },
                Int32Member = 1383539949,
                Int32ArrayMember = new int[] { -2116584919, -1471308829, 253298923, -1288156726, -713951810, -552054771, 2013928021, -92919534, -1913261199, 668320340 },
                NullableInt32Member = 241927295,
                NullableInt32ArrayMember = new Nullable<int>[] { null, -25919635, -1098691050, 1287451064, 1472293100, null, 1422490347, null, 330147806, -708407822 },
                UInt32Member = 3858268314,
                UInt32ArrayMember = new uint[] { 1291402765, 3473012933, 142146083, 3134330012, 286662849, 3933828236, 221131748, 3375626478, 2132282701, 147438343 },
                NullableUInt32Member = 3372394164,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2668940915, 4227002300, 135300957, 1913531763, null, 3843269073, null, 2077728728, 246226, 463285484 },
                Int64Member = 736788664843061185,
                Int64ArrayMember = new long[] { -3747867889087077812, -5735240749778346958, 7158708605309775233, 8949781897604412208, 1202917994656940526, 5447118907550917752, -1113252713044238473, 4008242475106951938, -3356618016893225528, 8745855050706404908 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { 7082711424349784100, -98861832546781204, null, 1380084997276810521, -1034629657536855894, -1616548981663238269, 639299752169239875, -6884842470823004419, -5305773023888447354, -8346518382986083863 },
                UInt64Member = 13098748044190904081,
                UInt64ArrayMember = new ulong[] { 8608053168273023407, 12491121373227391778, 5291779154130412355, 6587492979628409647, 16435258887820847691, 424064090732072887, 8757365848790720401, 16412203866906269492, 15145518530026043670, 15711022177244223438 },
                NullableUInt64Member = 6610158617267960011,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 14163784116463110904, 923584278322095624, 1727196281330732147, 4666211808629076002, 8591935141435908061, 13653647024982249834, 3231637652666284060, null, 11935949976906244162, 8032062507763662915 },
                SingleMember = 0.5625F,
                SingleArrayMember = new float[] { 0.506F, 0.918F, 0.7464F, 0.3002F, 0.85F, 0.0196F, 0.9527F, 0.9685F, 0.6126F, 0.5065F },
                NullableSingleMember = 0.8212F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.7362F, 0.4571F, 0.6534F, null, null, null, 0.6667F, null, 0.4736F },
                DoubleMember = 0.1211,
                DoubleArrayMember = new double[] { 0.8468, 0.5264, 0.5926, 0.6248, 0.5749, 0.9536, 0.2483, 0.2962, 0.5676, 0.984 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.235, 0.4227, 0.6952, null, null, null, 0.3537, 0.073, null, 0.4561 },
                DecimalMember = 0.808174081105245m,
                DecimalArrayMember = new decimal[] { 0.5995053757508m, 0.39648522689849m, 0.554036469962441m, 0.357324883386526m, 0.746797656832184m, 0.973288087626831m, 0.924019886957279m, 0.395884821050198m, 0.387757146178663m, 0.716737167523424m },
                NullableDecimalMember = 0.64465804900471m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.367455659293929m, 0.924965309770248m, 0.121253755252029m, 0.402058263655187m, null, 0.605587122742014m, null, 0.404988516371498m, 0.154934967659209m, 0.663403864763955m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(-66281160),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-160774454), DateTime.Now.AddSeconds(30911374), DateTime.Now.AddSeconds(172432998), DateTime.Now.AddSeconds(89836982), DateTime.Now.AddSeconds(-134779155), DateTime.Now.AddSeconds(-111911967), DateTime.Now.AddSeconds(162554291), DateTime.Now.AddSeconds(-255282941), DateTime.Now.AddSeconds(-125156250), DateTime.Now.AddSeconds(332705427) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, null, null, DateTime.Now.AddSeconds(291704059), null, null, DateTime.Now.AddSeconds(256633764), null, DateTime.Now.AddSeconds(269609002), null },
            },
            new DataConvertersTestClass {
                StringMember = "y2A8p:E4mg",
                StringArrayMember = new string[] { "HG8wD54eGo", "Wkn2F!X#ov", "LJ;pyC?\"aH", "M#EvXbaccc", "8kaj!2t3%7", "y5 CM:GW,@", "JUTefrWLVP", "KJq!73'jiG", "@FH'9'W?vm", "6lVQzofp91" },
                CharMember = '%',
                CharArrayMember = new char[] { '$', 'x', '"', 'l', '5', '!', '%', 'E', 'f', 'w' },
                NullableCharMember = 'V',
                NullableCharArrayMember = new Nullable<char>[] { 'P', '5', 'Q', 'D', 'x', '0', 'K', '?', null, 'r' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, false, false, true, true, true, false, true, true, false },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, null, null, true, true, null, null, null, null, null },
                ByteMember = 189,
                ByteArrayMember = new byte[] { 126, 168, 111, 183, 253, 75, 27, 130, 32, 56 },
                NullableByteMember = 8,
                NullableByteArrayMember = new Nullable<byte>[] { null, 236, 42, 66, 214, 13, 239, 90, null, 166 },
                SByteMember = 5,
                SByteArrayMember = new sbyte[] { -88, 96, -87, -14, -125, -48, -23, 58, 64, 24 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, null, 44, 88, null, 73, null, 112, -122, null },
                Int16Member = -656,
                Int16ArrayMember = new short[] { -18377, 31478, -31406, -3255, 32052, 13254, -16174, 11451, 21664, -14591 },
                NullableInt16Member = 27192,
                NullableInt16ArrayMember = new Nullable<short>[] { -13072, -7454, null, -25849, -14043, null, -24627, -1929, 5950, 25746 },
                UInt16Member = 57358,
                UInt16ArrayMember = new ushort[] { 20333, 7102, 23740, 16558, 49560, 34036, 60526, 59099, 11846, 27636 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, null, 1881, null, 37911, 26549, 5073, null, 33425, null },
                Int32Member = 1632178366,
                Int32ArrayMember = new int[] { -121966183, 1709065317, -539615156, 2009907547, -1417390536, -1878592292, -1961945815, 701775093, 343848100, -186898505 },
                NullableInt32Member = 1371305496,
                NullableInt32ArrayMember = new Nullable<int>[] { null, null, 2129534030, -1614010617, 276138571, -944951354, null, null, -1302626834, -1871083583 },
                UInt32Member = 1977777535,
                UInt32ArrayMember = new uint[] { 3732246098, 997959728, 945591417, 918744249, 598639365, 1002547203, 3133321004, 1007620335, 153837828, 3752011996 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 657862333, 3648454250, null, 593269751, 349862973, 1635823917, 3631864907, 1997834713, null, 2151907059 },
                Int64Member = 5070018967502950683,
                Int64ArrayMember = new long[] { 3076567015618707519, -7097734551271868928, 3157841479530820643, 6578856752529853452, 7354568080428329640, -4588123756869994108, -7851320967989207576, 8748983446188458296, -402635430520739, -7511246351446805807 },
                NullableInt64Member = 3368424590675000003,
                NullableInt64ArrayMember = new Nullable<long>[] { 6512220918725977106, null, 6481289948973508675, -7532674505082045375, 4009239777041364829, null, -6089390307587761824, null, 7875568599696044635, null },
                UInt64Member = 15938181050745138362,
                UInt64ArrayMember = new ulong[] { 3715775251366596681, 781489502508069280, 14932368084082210765, 11165679214122402420, 9227391584028133503, 17014852291408000931, 3297912958844981917, 16189360565756027356, 17980391107410030832, 16314502784033508142 },
                NullableUInt64Member = 6692596365539968477,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 8323720603464467200, 15460741440155417034, 7909869936110222821, 7581786493613556292, 7131786121899340830, 8418791513760923994, null, 17751567437593070021, null, 7057773520837930269 },
                SingleMember = 0.0592F,
                SingleArrayMember = new float[] { 0.6889F, 0.745F, 0.7969F, 0.4914F, 0.1232F, 0.6455F, 0.5712F, 0.2491F, 0.6533F, 0.042F },
                NullableSingleMember = 0.2566F,
                NullableSingleArrayMember = new Nullable<float>[] { null, null, 0.7984F, 0.615F, 0.2206F, 0.4622F, 0.744F, null, 0.0166F, 0.2928F },
                DoubleMember = 0.4593,
                DoubleArrayMember = new double[] { 0.2001, 0.6434, 0.5797, 0.5315, 0.5627, 0.9339, 0.0658, 0.4911, 0.9518, 0.7297 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.1855, null, 0.779, 0.6373, 0.9029, 0.7619, 0.1972, null, null, 0.9768 },
                DecimalMember = 0.880164211223925m,
                DecimalArrayMember = new decimal[] { 0.850473068407778m, 0.382287875639869m, 0.587583100599068m, 0.810010475219305m, 0.194788375159028m, 0.0850518778202219m, 0.121284766212382m, 0.352996355203362m, 0.775266912733661m, 0.894363027090534m },
                NullableDecimalMember = 0.748313825402033m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.955662890807266m, 0.230459501260279m, 0.262186800028407m, null, null, null, 0.508134116783826m, 0.394896807162947m, 0.976700240451939m, 0.604362601120341m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(256804064),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-229213956), DateTime.Now.AddSeconds(-349387551), DateTime.Now.AddSeconds(223827463), DateTime.Now.AddSeconds(-112688992), DateTime.Now.AddSeconds(217960498), DateTime.Now.AddSeconds(-16395754), DateTime.Now.AddSeconds(-189189412), DateTime.Now.AddSeconds(37948871), DateTime.Now.AddSeconds(37247275), DateTime.Now.AddSeconds(43487044) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(308791829),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-206592719), DateTime.Now.AddSeconds(7987169), DateTime.Now.AddSeconds(74898072), null, DateTime.Now.AddSeconds(-66169889), null, DateTime.Now.AddSeconds(150427545), DateTime.Now.AddSeconds(2402369), null, DateTime.Now.AddSeconds(-48988932) },
            },
            new DataConvertersTestClass {
                StringMember = "IDdMq2qiC1",
                StringArrayMember = new string[] { "%G!y0klnsc", "n$PDIzt!L#", "5NtU:26qVO", "DnG,wumIRf", "PBaYg\"zDG\"", "7nsWtvr\" z", "xLWgS#Lsje", "r.Y$7:tG1l", "MRFSN??X05", "IC;0H:QdGo" },
                CharMember = 'w',
                CharArrayMember = new char[] { 'c', 'h', '@', 's', 'L', '\'', 'r', 's', 'n', 'E' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'E', 'Q', null, 'E', 'b', null, '?', 'c', 'Q', '2' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, true, false, false, false, true, true, false, false, false },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, null, true, false, null, null, false, true, null, false },
                ByteMember = 113,
                ByteArrayMember = new byte[] { 146, 238, 47, 193, 0, 9, 147, 86, 84, 125 },
                NullableByteMember = 203,
                NullableByteArrayMember = new Nullable<byte>[] { 134, 81, 31, null, 112, 123, null, 243, null, 179 },
                SByteMember = 52,
                SByteArrayMember = new sbyte[] { 25, 12, 24, 117, 9, -99, -68, 10, 79, -126 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, null, -105, 125, 126, 61, -97, -49, null, 23 },
                Int16Member = -11931,
                Int16ArrayMember = new short[] { 18409, -28622, 15654, 16000, -13636, 16294, -20837, 32123, -23113, 2611 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { 28948, 1800, -17754, null, 15694, 6822, -31462, -31336, 20902, 11062 },
                UInt16Member = 31544,
                UInt16ArrayMember = new ushort[] { 63435, 34361, 15151, 22932, 30118, 15521, 5366, 12146, 48723, 57463 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 23669, null, 11110, 60641, 13180, 24569, 27890, 49966, 7848 },
                Int32Member = -2028618266,
                Int32ArrayMember = new int[] { -112508534, -971916634, 1514803772, 1159895629, 1563447979, -1978164853, 1548040411, -1949659786, -1934129523, 900448297 },
                NullableInt32Member = -1317221201,
                NullableInt32ArrayMember = new Nullable<int>[] { 1892463033, null, -1969381723, null, -2091713316, null, -542657434, -506939685, 1338255528, null },
                UInt32Member = 802068487,
                UInt32ArrayMember = new uint[] { 3431499623, 3092934237, 310643195, 2129348036, 2732506466, 1460374078, 442534763, 641388117, 2441006493, 2563935327 },
                NullableUInt32Member = 4122202753,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1944507017, 1453929660, null, null, 2207798402, 1476195704, 3599955213, null, 4104789413, 60067960 },
                Int64Member = 1190613961938026261,
                Int64ArrayMember = new long[] { 917585464790716868, 5411523629944308795, 4916791108231463832, -6293307768843670453, 740490727495146916, -694454800594554916, 8433786389379012260, 4303391589749371949, -3980556458955732087, 4430218548442622037 },
                NullableInt64Member = 8852958215677108818,
                NullableInt64ArrayMember = new Nullable<long>[] { 7248906718987943982, 6699066823497966913, -1529559867187956336, null, 7443946542367228519, 1522142084895828347, 3950714206910583978, 2450060778370118955, 1398810660128490705, null },
                UInt64Member = 12369157212432620243,
                UInt64ArrayMember = new ulong[] { 15188336607541015346, 11057461161116303232, 11945791902500038224, 13394337824224995251, 6413225130649503623, 6980756065081234716, 17621486593376770973, 7049846667348162018, 1832082663853041555, 2088411954275941394 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 17019045805998030019, 12919169564457490474, 3120225272168041763, null, 4454858963977174840, 2421816594074923466, null, null, 2706541215991264409, null },
                SingleMember = 0.487F,
                SingleArrayMember = new float[] { 0.9002F, 0.8929F, 0.7742F, 0.9362F, 0.5312F, 0.6438F, 0.2743F, 0.9879F, 0.8139F, 0.8143F },
                NullableSingleMember = 0.7266F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.8194F, 0.2638F, null, 0.8848F, null, null, null, 0.0254F, 0.0772F, 0.922F },
                DoubleMember = 0.6366,
                DoubleArrayMember = new double[] { 0.9668, 0.2345, 0.1016, 0.9113, 0.7921, 0.8893, 0.5209, 0.2345, 0.1242, 0.5779 },
                NullableDoubleMember = 0.3572,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.8739, 0.3197, null, null, null, 0.8481, null, 0.5917, null, 0.3141 },
                DecimalMember = 0.681622599890544m,
                DecimalArrayMember = new decimal[] { 0.057359867764244m, 0.974131928723615m, 0.257075752293149m, 0.0207593461250412m, 0.243750433798434m, 0.248693666879026m, 0.632503561414617m, 0.75313929520116m, 0.514117045587536m, 0.997939939537539m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.881024940839086m, null, 0.568763371853048m, null, 0.820557618081481m, null, 0.971465539816131m, 0.29331143533846m, 0.79606456034176m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null },
                DateTimeMember = DateTime.Now.AddSeconds(-30320981),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-57815536), DateTime.Now.AddSeconds(94331433), DateTime.Now.AddSeconds(-75637161), DateTime.Now.AddSeconds(-42794038), DateTime.Now.AddSeconds(418215), DateTime.Now.AddSeconds(5599738), DateTime.Now.AddSeconds(94745793), DateTime.Now.AddSeconds(-25766235), DateTime.Now.AddSeconds(53094836), DateTime.Now.AddSeconds(41697069) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-90261337),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, null, null, DateTime.Now.AddSeconds(-320054794), DateTime.Now.AddSeconds(159469479), DateTime.Now.AddSeconds(258405476), null, DateTime.Now.AddSeconds(178289391), DateTime.Now.AddSeconds(235508784), DateTime.Now.AddSeconds(-4843925) },
            },
            new DataConvertersTestClass {
                StringMember = "xo@$j3ohVF",
                StringArrayMember = new string[] { "50'UCoY5ry", "w;?I '''5Z", "81!g1,pIu3", "B#fx#R87$q", "'3lqP#H2m0", "x4@NE1\"w2R", "fQCmY4sT01", "lvf04LPFE#", ";Ixh?8y7lu", "b$9 mTD7i6" },
                CharMember = ';',
                CharArrayMember = new char[] { 'k', 'G', 'R', '8', 'r', 'T', '6', 'q', 'T', '2' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, 'g', null, 'A', 'j', null, null, 'i', null, 'w' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, false, false, false, false, false, false, true, false, false },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, null, false, null, false, true, null, true, true, true },
                ByteMember = 71,
                ByteArrayMember = new byte[] { 218, 67, 205, 73, 165, 98, 121, 17, 89, 33 },
                NullableByteMember = 140,
                NullableByteArrayMember = new Nullable<byte>[] { 71, 234, null, 217, null, null, null, 121, null, 203 },
                SByteMember = 7,
                SByteArrayMember = new sbyte[] { -59, 96, -117, 95, -14, -73, 38, 104, -103, 120 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 106, 79, 42, null, 14, -128, null, 84, -24, 24 },
                Int16Member = -4521,
                Int16ArrayMember = new short[] { 10537, 25318, -24304, -1373, 31765, -26226, 8858, 24528, 10391, -32554 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { 20544, -8734, null, null, -13850, null, -23653, null, -8519, 5876 },
                UInt16Member = 53052,
                UInt16ArrayMember = new ushort[] { 18660, 64014, 41891, 52510, 60272, 6466, 27179, 15070, 8182, 28753 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 43638, null, 36894, 10064, 27503, 51159, 18437, null, null, null },
                Int32Member = -897393258,
                Int32ArrayMember = new int[] { -513103083, 2011196454, 1048382722, 2097808012, -563401836, -191478775, -17293744, 1471065870, 1563751506, -1185437648 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { null, null, null, -2030469459, null, 304412246, -80233094, null, 931484320, -1695914505 },
                UInt32Member = 1531069747,
                UInt32ArrayMember = new uint[] { 3940755293, 1062425239, 2429467491, 2934173107, 3660651882, 239716880, 1047173319, 1339232047, 4240176147, 1489336060 },
                NullableUInt32Member = 1457972908,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1661072744, 2134492146, 610716082, 1271463731, 4250921258, null, null, null, null, 3026513538 },
                Int64Member = 5887769368382044725,
                Int64ArrayMember = new long[] { 6778475902566073986, -3179076888293258453, 6658407325790342827, 5066175896768510865, -8751760607303206231, 5586682487434036766, -7608138659866377520, 1707097200480751075, -7381846974473577194, -519975003152203856 },
                NullableInt64Member = -2886302368509540547,
                NullableInt64ArrayMember = new Nullable<long>[] { -7283381318976906063, null, 6475917787197864500, -1601847497680759752, -440777848973304785, 6949474240114682001, 98458150225096355, null, -6287846770053518957, null },
                UInt64Member = 8741689822873429694,
                UInt64ArrayMember = new ulong[] { 5684075420731426424, 11015525592720820157, 7850056686566381849, 17746262342091091139, 5687481708630917610, 10597092781494523529, 9958303800855411429, 2528425641811564408, 16774309627534873213, 12159181041548393607 },
                NullableUInt64Member = 16986649386007073120,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 3065681239870181501, null, 6572027452084573508, null, 9838415641363690199, null, 16707438738979170678, 7410357294608117553, 12571592810640610508, null },
                SingleMember = 0.5198F,
                SingleArrayMember = new float[] { 0.258F, 0.9244F, 0.4819F, 0.1652F, 0.8396F, 0.4341F, 0.6695F, 0.0286F, 0.6387F, 0.331F },
                NullableSingleMember = 0.4523F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.7223F, 0.3143F, null, null, 0.8741F, null, null, 0.375F, 0.9609F, 0.8571F },
                DoubleMember = 0.6789,
                DoubleArrayMember = new double[] { 0.7945, 0.7759, 0.7113, 0.713, 0.7811, 0.6871, 0.608, 0.8077, 0.7664, 0.4034 },
                NullableDoubleMember = 0.1004,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.9353, null, 0.839, null, 0.8808, 0.8434, 0.5561, null, 0.9633, 0.0264 },
                DecimalMember = 0.190575767895683m,
                DecimalArrayMember = new decimal[] { 0.237680219269983m, 0.698361003266687m, 0.892928741614913m, 0.700119653373016m, 0.199715539078926m, 0.877276443243484m, 0.59329827409278m, 0.611575988154246m, 0.0565150634821652m, 0.730845598078726m },
                NullableDecimalMember = 0.580709565005613m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.72152559351921m, 0.017134372820187m, 0.702241479155852m, 0.70956435257547m, 0.15807047127619m, null, null, null, 0.573174706848572m, 0.819484470328295m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(295190178),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-79892148), DateTime.Now.AddSeconds(322660010), DateTime.Now.AddSeconds(169035423), DateTime.Now.AddSeconds(-295352486), DateTime.Now.AddSeconds(233647884), DateTime.Now.AddSeconds(256057565), DateTime.Now.AddSeconds(-140743154), DateTime.Now.AddSeconds(151022413), DateTime.Now.AddSeconds(223577589), DateTime.Now.AddSeconds(213155407) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-141488266),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-332021469), DateTime.Now.AddSeconds(-240100714), DateTime.Now.AddSeconds(-128091169), DateTime.Now.AddSeconds(-223329215), DateTime.Now.AddSeconds(-60928976), DateTime.Now.AddSeconds(-255825033), DateTime.Now.AddSeconds(-190741944), null, DateTime.Now.AddSeconds(227581854), null },
            },
            new DataConvertersTestClass {
                StringMember = "3MV.NVPEty",
                StringArrayMember = new string[] { "UT6LM8@yNN", "Qi111UTMGm", "Q?Njqqlkqb", "WYMJVUQzM8", "Z?vH7VhcJF", "uL?;gK:g!P", "I#uYTO2B4B", "Bx7R9BTA%W", "e8kzVD2Eqo", "\"rCfflG,X6" },
                CharMember = 'M',
                CharArrayMember = new char[] { 'n', '.', 'B', '!', 'T', 'p', 'e', ';', 'M', 'n' },
                NullableCharMember = 'A',
                NullableCharArrayMember = new Nullable<char>[] { null, '#', '.', 'b', null, null, null, null, null, 'Q' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, false, false, false, true, false, false, true, true, false },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, false, true, false, true, null, null, true, false },
                ByteMember = 237,
                ByteArrayMember = new byte[] { 87, 137, 80, 135, 155, 127, 123, 96, 116, 183 },
                NullableByteMember = 48,
                NullableByteArrayMember = new Nullable<byte>[] { 88, 24, 161, null, 212, 237, 101, null, 111, null },
                SByteMember = 4,
                SByteArrayMember = new sbyte[] { 18, 9, 57, -54, -94, 73, 53, -119, 117, -108 },
                NullableSByteMember = 57,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -6, -5, null, 97, -18, null, null, 89, null, -114 },
                Int16Member = 28220,
                Int16ArrayMember = new short[] { -4018, 29986, -3195, -11212, -31247, -3275, -20996, 6277, -29423, -24083 },
                NullableInt16Member = 8771,
                NullableInt16ArrayMember = new Nullable<short>[] { null, 6539, 28500, null, null, -12775, -17991, 17999, 31194, null },
                UInt16Member = 13076,
                UInt16ArrayMember = new ushort[] { 47471, 21610, 53017, 3242, 65067, 6627, 59842, 4945, 26128, 32123 },
                NullableUInt16Member = 13655,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 55028, 42634, null, null, null, null, 17767, 14818, null, 28855 },
                Int32Member = -1591793846,
                Int32ArrayMember = new int[] { -1633160469, 390328404, 1382023224, -110906991, 938333830, 1396221328, 1609998300, 166982848, 1219327664, 618404528 },
                NullableInt32Member = -1259758145,
                NullableInt32ArrayMember = new Nullable<int>[] { null, 64698925, 1441575270, null, -597396242, -147698006, 723987079, 34864421, null, 1779388687 },
                UInt32Member = 2620962365,
                UInt32ArrayMember = new uint[] { 710520263, 2522057471, 1607704919, 1787512798, 1473004633, 1655846395, 3454357421, 2085546944, 2500132391, 2976421527 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 572213585, 3031218445, null, 459723597, 990520460, null, null, null, 2776313043, 4103237551 },
                Int64Member = -594420597554372304,
                Int64ArrayMember = new long[] { -3084910212275695351, 413020166954692049, -5218875571444099060, 8293962227794514588, -4585393833866680827, -5280513803666221503, -3079998002330770871, 2391127319653305500, 4241331298741167372, 2374420369126533287 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, 9054130023486623438, -2881050571763251546, 4681792072505840837, 7221183018019712006, -633970516400789501, null, 3941990114373424932, null, null },
                UInt64Member = 15945442308581904497,
                UInt64ArrayMember = new ulong[] { 1003044358723075365, 4227578309799315206, 17214937162959796529, 14608943818735417525, 17738006347465299279, 1698101394393908452, 4246416658902289217, 1054989636094524436, 8585890985885101951, 4469275828405076795 },
                NullableUInt64Member = 17262276090330442772,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 1611413045515349930, null, null, null, 11145942356403797022, null, 5064066822379788116, null, null },
                SingleMember = 0.4469F,
                SingleArrayMember = new float[] { 0.3957F, 0.7736F, 0.4064F, 0.2649F, 0.8019F, 0.7377F, 0.3969F, 0.5389F, 0.2484F, 0.0445F },
                NullableSingleMember = 0.6368F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.8601F, null, 0.1864F, 0.3855F, 0.2719F, 0.2667F, 0.8234F, 0.0407F, null },
                DoubleMember = 0.3287,
                DoubleArrayMember = new double[] { 0.5622, 0.0498, 0.3073, 0.2245, 0.6397, 0.1331, 0.5108, 0.7308, 0.0876, 0.8427 },
                NullableDoubleMember = 0.404,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.6028, null, 0.585, null, 0.706, 0.9207, 0.8392, null, 0.8491, 0.3682 },
                DecimalMember = 0.715170196760544m,
                DecimalArrayMember = new decimal[] { 0.455375817575912m, 0.687057022550052m, 0.370585448931093m, 0.392774572559525m, 0.103229890305883m, 0.545494383137924m, 0.301736259521397m, 0.893371027708516m, 0.857988052197567m, 0.548382567383256m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, null, 0.714084469234438m, 0.858341324473147m, null, null, 0.517299998758741m, 0.986087056493252m, 0.562749307978766m, 0.837579053620539m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-46131422),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(201380891), DateTime.Now.AddSeconds(-73768615), DateTime.Now.AddSeconds(74455781), DateTime.Now.AddSeconds(-150391340), DateTime.Now.AddSeconds(-84562003), DateTime.Now.AddSeconds(-46334108), DateTime.Now.AddSeconds(178597364), DateTime.Now.AddSeconds(169350103), DateTime.Now.AddSeconds(205611732), DateTime.Now.AddSeconds(126616567) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-307956975),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, null, DateTime.Now.AddSeconds(-271059908), DateTime.Now.AddSeconds(-317970697), null, null, DateTime.Now.AddSeconds(145665706), DateTime.Now.AddSeconds(77780912), DateTime.Now.AddSeconds(202288592), null },
            },
        };
    }
}
