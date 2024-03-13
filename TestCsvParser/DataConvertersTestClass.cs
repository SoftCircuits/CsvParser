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
                StringMember = "DBLw6RoNlC",
                StringArrayMember = new string[] { "Z,KIiJZd8w", "SA1ilU;IAo", "Zxm%Qhp5ob", "6auMF%T$n!", "%S!CFC.UXB", "l7A54l8L4M", "pDhfLb,,Ki", "RgEtdyCm0s", ":lbHLRLeb6", "WyS\"Hts$%p" },
                CharMember = 'g',
                CharArrayMember = new char[] { '7', 'Y', 'q', '4', ':', ':', 'b', 'B', '9', 'H' },
                NullableCharMember = 'o',
                NullableCharArrayMember = new Nullable<char>[] { null, null, 'v', 'N', null, 'C', 'h', 'm', null, null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, false, true, true, true, false, false, false, false, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, true, null, null, null, true, true, false, null },
                ByteMember = 78,
                ByteArrayMember = new byte[] { 23, 150, 102, 199, 67, 43, 203, 122, 133, 193 },
                NullableByteMember = 66,
                NullableByteArrayMember = new Nullable<byte>[] { 21, null, 39, null, 219, null, 131, 210, 113, 242 },
                SByteMember = 40,
                SByteArrayMember = new sbyte[] { 83, -75, -18, 95, 52, 77, -69, -106, 23, -100 },
                NullableSByteMember = 4,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -39, null, 82, -17, null, null, null, -40, -103, null },
                Int16Member = 10869,
                Int16ArrayMember = new short[] { 15094, -14476, 31410, -9726, -1625, -18338, 14366, -31473, -29262, -16573 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { null, 1325, -16401, null, null, null, -4332, -14125, -31969, -2443 },
                UInt16Member = 30808,
                UInt16ArrayMember = new ushort[] { 56573, 53585, 11231, 3794, 27911, 56175, 44304, 35141, 54286, 41680 },
                NullableUInt16Member = 13675,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 23496, 6902, 46860, 56920, 74, null, 5107, 788, null, 41082 },
                Int32Member = 1528534912,
                Int32ArrayMember = new int[] { -2034231768, 184942696, 463163412, -140050595, 690011366, -912865955, 302004907, -1161814293, 1688556784, 83392387 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { 1487703419, 448808844, -62215332, null, -735746218, -1758149996, 1582052593, null, null, null },
                UInt32Member = 963713964,
                UInt32ArrayMember = new uint[] { 1018062302, 2293249449, 3880336380, 3799939417, 2383698659, 2417634446, 1717390654, 124667853, 3426182655, 4182903575 },
                NullableUInt32Member = 2827515001,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 3347396785, null, null, 1403255800, 784908166, 2321426854, 1279402992, null, null },
                Int64Member = 5738882787995600920,
                Int64ArrayMember = new long[] { -2900934604643922255, -1851664516083773296, 8146335078676634907, 7006872863059389435, 8435450729839748024, 6793714090163157835, -7916700256675926421, 654421866386929971, -913828582271159241, 4895882561814549021 },
                NullableInt64Member = -6868413842742593200,
                NullableInt64ArrayMember = new Nullable<long>[] { 8796814421073829108, null, 5344110377181061422, null, null, -4668034594005055860, null, -5799777128648061518, -3206385104181776335, null },
                UInt64Member = 1448683893684426247,
                UInt64ArrayMember = new ulong[] { 7359554310635044131, 17392647947181484268, 1197085808758259676, 2783863382965817225, 969680008585843451, 703184933590286069, 2121700116803928606, 7282930347444441667, 11080616180731251079, 14296692617617723960 },
                NullableUInt64Member = 5085361063969493862,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 9924900795067988992, 10694902301622725413, 11962549518620681369, 7198946045900497231, 12563373358850966889, 4761035081709873138, 10922879117592633963, null, 804243714475936347, 28005723682218333 },
                SingleMember = 0.5814F,
                SingleArrayMember = new float[] { 0.5795F, 0.8881F, 0.8699F, 0.988F, 0.5793F, 0.0326F, 0.5879F, 0.9884F, 0.3826F, 0.2411F },
                NullableSingleMember = 0.7951F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.2941F, 0.9747F, null, 0.5372F, 0.2008F, 0.3638F, 0.72F, 0.3337F, 0.2233F, 0.3495F },
                DoubleMember = 0.8763,
                DoubleArrayMember = new double[] { 0.3381, 0.0568, 0.3143, 0.0039, 0.3388, 0.3289, 0.8801, 0.7205, 0.6709, 0.8797 },
                NullableDoubleMember = 0.5047,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.627, null, 0.9315, null, 0.9335, 0.4117, 0.0554, null, 0.7037, null },
                DecimalMember = 0.322174645275453m,
                DecimalArrayMember = new decimal[] { 0.886388429499236m, 0.249147456788071m, 0.699740696618486m, 0.533557090959802m, 0.831425388768874m, 0.522507422264744m, 0.94738543202866m, 0.0410989305400856m, 0.45545510870679m, 0.0436263699126771m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, null, 0.479951511431621m, null, null, 0.446766240693727m, null, 0.765511292714122m, 0.427296066815424m, 0.727941650245491m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(-142766927),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-179007260), DateTime.Now.AddSeconds(54880308), DateTime.Now.AddSeconds(-263386711), DateTime.Now.AddSeconds(-337796869), DateTime.Now.AddSeconds(-161218413), DateTime.Now.AddSeconds(55568505), DateTime.Now.AddSeconds(171085007), DateTime.Now.AddSeconds(265581522), DateTime.Now.AddSeconds(-285466512), DateTime.Now.AddSeconds(77596131) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-302151181),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-341695553), DateTime.Now.AddSeconds(-162244864), DateTime.Now.AddSeconds(194452534), DateTime.Now.AddSeconds(79380696), null, null, DateTime.Now.AddSeconds(-248698224), null, DateTime.Now.AddSeconds(-6798573), DateTime.Now.AddSeconds(334031866) },
            },
            new DataConvertersTestClass {
                StringMember = "hqV6'OND3R",
                StringArrayMember = new string[] { "IU40;u5Bd1", "kiB!O7JrPf", "5NH@Su'QV6", "UGOcL%a1og", "z#axF\"?INz", "PCwqHVh9:V", "UL1izVirDB", "mIma9MpIPY", "syK4J:Ll4E", "1mW@Gk!#PH" },
                CharMember = ',',
                CharArrayMember = new char[] { 't', 'A', 't', 'm', 'w', ':', '7', 'v', '6', '\'' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { '6', '@', null, 'G', 'U', 'g', '7', ',', 'q', 'N' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, false, true, false, false, false, true, true, true, false },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, null, null, true, null, true, true, true, false, false },
                ByteMember = 173,
                ByteArrayMember = new byte[] { 143, 133, 116, 47, 56, 157, 104, 179, 82, 67 },
                NullableByteMember = 0,
                NullableByteArrayMember = new Nullable<byte>[] { 131, 89, 11, null, 129, 70, null, 36, 49, 60 },
                SByteMember = -9,
                SByteArrayMember = new sbyte[] { -110, -46, -44, 48, 120, -52, -41, 21, 121, 94 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 98, -64, null, null, null, -46, null, -103, null, 93 },
                Int16Member = -29432,
                Int16ArrayMember = new short[] { -23882, 10983, -3537, 14400, 32389, 5850, -4291, 25940, -1254, 4921 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { 31966, null, 14998, 25884, null, -13711, null, -23811, 23026, -6869 },
                UInt16Member = 39500,
                UInt16ArrayMember = new ushort[] { 29434, 26569, 47884, 46241, 38669, 49992, 61537, 61076, 63027, 57534 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 34672, 63724, null, 6501, 22109, 18596, 37633, 41088, null },
                Int32Member = -1814793597,
                Int32ArrayMember = new int[] { -1593038882, -542785709, -1759870268, 1981252517, 181032695, -1832790441, 896109359, 1864248993, -2046713555, -325942856 },
                NullableInt32Member = -105312805,
                NullableInt32ArrayMember = new Nullable<int>[] { null, null, null, 1022321555, null, null, 1675154323, null, null, 2013045176 },
                UInt32Member = 315554533,
                UInt32ArrayMember = new uint[] { 2650002598, 3671396838, 2477479870, 375363190, 185378562, 2840374914, 3023876119, 2833909431, 1165207435, 3948367816 },
                NullableUInt32Member = 1403825370,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, null, 4026050760, 352684061, 1538659390, 2265339781, null, null, null, 2085804427 },
                Int64Member = -8838746515644721050,
                Int64ArrayMember = new long[] { 480401380863454000, -9055120068673524410, -271362779346837329, 5098910949147499015, -1405134405264067894, 6715705687374284292, 1843825596295940898, -3904601701701160903, -8525222030310165194, -60721425439905344 },
                NullableInt64Member = -5624107397161626285,
                NullableInt64ArrayMember = new Nullable<long>[] { -8795310096545430069, -5244594905403715430, 7845921497474695789, null, null, 55196314521634057, 2233396608124241585, 6506781615330697678, null, null },
                UInt64Member = 4653156646724519254,
                UInt64ArrayMember = new ulong[] { 13834953625800803357, 2487873944760545841, 16123415996318962144, 9343718937392492901, 17773013220009292129, 11256506939388776875, 6203543898417488190, 5373716660240520698, 8144168558655120463, 5022159471934537144 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 953909567214190142, 14165512491383933973, 9804927090360109950, 2708545906083524163, null, 6748697330931036901, 15522686672096122756, null, 16454739217434298875, null },
                SingleMember = 0.3916F,
                SingleArrayMember = new float[] { 0.9591F, 0.2847F, 0.2392F, 0.131F, 0.1821F, 0.419F, 0.5215F, 0.2292F, 0.6057F, 0.4106F },
                NullableSingleMember = 0.0735F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.4253F, null, 0.903F, null, null, 0.6886F, null, null, 0.0844F, 0.126F },
                DoubleMember = 0.4964,
                DoubleArrayMember = new double[] { 0.8405, 0.1305, 0.6299, 0.6142, 0.2642, 0.2873, 0.4063, 0.6068, 0.8382, 0.0932 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.0741, 0.5744, 0.4184, 0.6866, 0.9556, 0.0152, 0.108, 0.3979, 0.589 },
                DecimalMember = 0.332895529828796m,
                DecimalArrayMember = new decimal[] { 0.686428091435554m, 0.748883139700015m, 0.119650683965345m, 0.494950949040972m, 0.347618823518273m, 0.82725193296696m, 0.59418779425855m, 0.115927661366659m, 0.259359623658765m, 0.22571057527525m },
                NullableDecimalMember = 0.496906453369063m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.681921266025851m, 0.778520244750955m, 0.37886728941191m, 0.638561251506798m, 0.40664379933936m, 0.776776218051873m, 0.231724720385832m, 0.0457743806255505m, 0.0347239064027549m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, null, null },
                DateTimeMember = DateTime.Now.AddSeconds(-331885878),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(292966579), DateTime.Now.AddSeconds(307398709), DateTime.Now.AddSeconds(152367528), DateTime.Now.AddSeconds(-48732574), DateTime.Now.AddSeconds(-276911340), DateTime.Now.AddSeconds(-107634617), DateTime.Now.AddSeconds(189066585), DateTime.Now.AddSeconds(-248775202), DateTime.Now.AddSeconds(-229621599), DateTime.Now.AddSeconds(30932221) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-16877229),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(292735313), null, DateTime.Now.AddSeconds(21807065), DateTime.Now.AddSeconds(216920197), null, DateTime.Now.AddSeconds(-303579856), DateTime.Now.AddSeconds(210228638), null, null, DateTime.Now.AddSeconds(-331793639) },
            },
            new DataConvertersTestClass {
                StringMember = "iXOU\"O:P3a",
                StringArrayMember = new string[] { "#ne8dd9%h0", "fBpylQv5aB", "'#d\"\"AcN9o", "ni? RLsSil", "yH660gyuJC", "v2LK!?#Kji", "rQCop0CxV7", "JCg\"% BE3x", "4jP4VO0b3o", "dWgkODR96L" },
                CharMember = 'a',
                CharArrayMember = new char[] { 'u', 'y', 'o', 'G', 'K', 'D', 'w', 'u', ':', 'v' },
                NullableCharMember = 'c',
                NullableCharArrayMember = new Nullable<char>[] { 'h', null, 'm', '3', null, 'M', '0', '$', 'w', 'O' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, true, true, true, false, false, true, false, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, null, null, false, true, false, false, true, false },
                ByteMember = 14,
                ByteArrayMember = new byte[] { 63, 68, 157, 89, 33, 28, 159, 115, 217, 238 },
                NullableByteMember = 248,
                NullableByteArrayMember = new Nullable<byte>[] { 217, null, 24, 202, 191, null, 104, 73, 231, 181 },
                SByteMember = -56,
                SByteArrayMember = new sbyte[] { -65, -12, -80, 61, -91, 50, -34, 66, -25, 95 },
                NullableSByteMember = 47,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -89, -44, null, -109, 104, null, null, -33, 43, -55 },
                Int16Member = 17599,
                Int16ArrayMember = new short[] { -31679, -14149, -2410, -14997, 24540, -10629, -23282, 4688, 21957, -20734 },
                NullableInt16Member = -11038,
                NullableInt16ArrayMember = new Nullable<short>[] { 25210, 13154, null, 15498, 14910, 26319, null, 23294, -11852, 14237 },
                UInt16Member = 35591,
                UInt16ArrayMember = new ushort[] { 17730, 33411, 27820, 5726, 56603, 4009, 46840, 5406, 37725, 49695 },
                NullableUInt16Member = 45690,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 59027, 28250, 2254, 14634, null, 65105, null, null, null, null },
                Int32Member = -2022339799,
                Int32ArrayMember = new int[] { 1071298041, -98408937, 418659951, -915846147, -178546843, -1130806073, -1210250085, 9326102, 56765636, 354458777 },
                NullableInt32Member = -1811965919,
                NullableInt32ArrayMember = new Nullable<int>[] { -6116176, 1141648845, 1423002897, null, 726845793, null, null, null, null, null },
                UInt32Member = 2450643754,
                UInt32ArrayMember = new uint[] { 1548742388, 3697566944, 1913209291, 2349540132, 2591390920, 3776482840, 133452812, 4242479404, 2745353983, 1810458615 },
                NullableUInt32Member = 1333883676,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1423387916, null, null, 3891596601, 2837018321, 3361062462, null, 3890398432, 3176157483, null },
                Int64Member = -278364574196199154,
                Int64ArrayMember = new long[] { 7786776869471606175, 6984762631420260139, 4405281702024491492, -6808800982314568674, -9000896615124242749, -9041821593288885359, -4170649535995811544, 7682407759679531515, 2802642827712139318, 4622325810503727143 },
                NullableInt64Member = 2253622455026236017,
                NullableInt64ArrayMember = new Nullable<long>[] { 4829644187857039997, null, 5816738205856039205, -6621938100292751766, -5802551589795266969, null, 4507276539816703062, null, 226270991358095947, 8622440056965100398 },
                UInt64Member = 4360844940003960024,
                UInt64ArrayMember = new ulong[] { 12723927461949428761, 14351442889539179062, 5032042490181461944, 2040475234490833347, 9478970588549116992, 11836939291517595928, 1729607899726058675, 9058190332096743395, 12760574524240820847, 874117986243352876 },
                NullableUInt64Member = 16278096627458436262,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 10159322912129781620, 513313013317313994, 2947527362323459193, 17700990409330126717, 9500703530184554118, 1293533742602224596, 5196609102120303218, 4392521396403289857, null },
                SingleMember = 0.3986F,
                SingleArrayMember = new float[] { 0.0483F, 0.4511F, 0.8919F, 0.1747F, 0.4532F, 0.9705F, 0.8364F, 0.5153F, 0.757F, 0.5631F },
                NullableSingleMember = 0.8574F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.8866F, 0.0133F, null, 0.48F, 0.0322F, 0.5568F, 0.8141F, null, 0.6824F, null },
                DoubleMember = 0.8922,
                DoubleArrayMember = new double[] { 0.2099, 0.2297, 0.0786, 0.1549, 0.5746, 0.5414, 0.4052, 0.9407, 0.9353, 0.0799 },
                NullableDoubleMember = 0.0481,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.3745, 0.3048, 0.6946, null, 0.3159, 0.7721, 0.5811, null, 0.8233, 0.8666 },
                DecimalMember = 0.179853201280247m,
                DecimalArrayMember = new decimal[] { 0.165293995506197m, 0.148069697767121m, 0.0507117274244782m, 0.165890362249488m, 0.120087202297494m, 0.987942900560276m, 0.785985586459231m, 0.534579408753842m, 0.342029295403684m, 0.845879272173976m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.176034883512735m, null, 0.507827764676353m, null, 0.0916541027219713m, null, null, null, 0.684999973304612m, 0.346214917452695m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(291381442),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-175291004), DateTime.Now.AddSeconds(-137060078), DateTime.Now.AddSeconds(161491564), DateTime.Now.AddSeconds(-281083154), DateTime.Now.AddSeconds(-251675523), DateTime.Now.AddSeconds(-275775122), DateTime.Now.AddSeconds(-160239391), DateTime.Now.AddSeconds(334693641), DateTime.Now.AddSeconds(-346587141), DateTime.Now.AddSeconds(-279915904) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(330635423),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(237468262), DateTime.Now.AddSeconds(-230673136), DateTime.Now.AddSeconds(-111824852), DateTime.Now.AddSeconds(33809553), null, DateTime.Now.AddSeconds(298440965), DateTime.Now.AddSeconds(334764972), DateTime.Now.AddSeconds(316897238), DateTime.Now.AddSeconds(-134560105), DateTime.Now.AddSeconds(322534392) },
            },
            new DataConvertersTestClass {
                StringMember = "Bljyw'i54J",
                StringArrayMember = new string[] { "cX\"!1VuXGT", "X EY'vKOU!", "XZKt9Dyc!y", "2tsX0@TbZb", "LdHXtR\";SI", "zJsq.pDPdV", "9jODUW'!.Z", "t9qEuAo0.c", "dkAOP5S\",A", "2KQu8Sm;5k" },
                CharMember = '2',
                CharArrayMember = new char[] { 'L', 'B', 'o', '3', 'q', '@', '!', '?', ';', 'X' },
                NullableCharMember = 'R',
                NullableCharArrayMember = new Nullable<char>[] { 'X', 'q', 'e', 'Z', '"', 't', null, null, ',', null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, false, false, false, false, false, false, false, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, true, false, true, null, true, null, false, null, false },
                ByteMember = 197,
                ByteArrayMember = new byte[] { 6, 115, 195, 176, 121, 84, 71, 160, 49, 41 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 115, null, 85, 237, 76, 249, 130, null, 68, 75 },
                SByteMember = 95,
                SByteArrayMember = new sbyte[] { -124, -36, 100, 14, -103, 26, -78, -124, 18, 38 },
                NullableSByteMember = 94,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -90, -85, null, null, 65, null, -74, -24, -73 },
                Int16Member = -22154,
                Int16ArrayMember = new short[] { -9356, 19914, -22007, -23612, 4712, -26530, 32061, -25298, 20892, -17032 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -30355, -14395, -14140, 6758, -8476, 26372, -2213, -2747, 8414, 22743 },
                UInt16Member = 8582,
                UInt16ArrayMember = new ushort[] { 64876, 35231, 29288, 51544, 12979, 40404, 27808, 55655, 44900, 55084 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 56607, 39062, 16279, 50984, null, 9178, 23277, 23029, 49407, 60916 },
                Int32Member = 1956101307,
                Int32ArrayMember = new int[] { 391195242, 1046188022, 398722115, -1802014384, 774328316, -539655997, -230283090, 993884247, -39186147, 1680584550 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { null, -1439820406, -1864676074, 1264606614, 310147492, null, -1931596682, 2039962608, -874185319, null },
                UInt32Member = 1854666634,
                UInt32ArrayMember = new uint[] { 2771204286, 979372001, 288378046, 2195074114, 2098632998, 3831441709, 1803919802, 471062587, 3046919143, 2263038025 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3656514573, 1729414311, null, 4086100011, null, 402893946, 297335065, 649486498, 757009787, 3918234803 },
                Int64Member = -2973174613685751596,
                Int64ArrayMember = new long[] { 5317290937192581697, -8183692368106682441, -5936016301886372458, 478931217074541225, 1754949796659319178, 6936812817197476274, 4756532758276276657, -8124341307329530044, -6251096935176120774, 1382410199017587480 },
                NullableInt64Member = -7766729931726843574,
                NullableInt64ArrayMember = new Nullable<long>[] { -5240592682845830381, -3949048877080488314, 610444872515745902, -8598812183903301629, -5344101984168067545, 775180848281613674, -7007568801730156180, 3676786197357962085, null, -4860943922293352332 },
                UInt64Member = 17054204193163330641,
                UInt64ArrayMember = new ulong[] { 3733062000468110450, 14405596660344917337, 16191695937999674626, 9969065687100007008, 16447365630944228565, 9824478245254666871, 6473072858819339160, 15987400910894456484, 11445045830343689597, 14794632248676822549 },
                NullableUInt64Member = 5995618161409416200,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 1056746617556458830, null, 540991333253482730, 11088479354701773789, 6895629544212137977, 15781556582636078332, null, 3066208073339996440, 9644772530028085149, null },
                SingleMember = 0.0297F,
                SingleArrayMember = new float[] { 0.0885F, 0.7519F, 0.0698F, 0.2075F, 0.7518F, 0.2586F, 0.9216F, 0.3764F, 0.7472F, 0.9984F },
                NullableSingleMember = 0.3868F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.2922F, 0.3574F, null, null, 0.0343F, 0.3055F, 0.6937F, 0.2553F, 0.6699F, 0.2877F },
                DoubleMember = 0.3425,
                DoubleArrayMember = new double[] { 0.0535, 0.8579, 0.3001, 0.4914, 0.0817, 0.7089, 0.7185, 0.7287, 0.5513, 0.1869 },
                NullableDoubleMember = 0.8673,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.9881, 0.0492, 0.1058, 0.4062, 0.6134, 0.1283, null, null, 0.0578 },
                DecimalMember = 0.496616873416229m,
                DecimalArrayMember = new decimal[] { 0.706226026873104m, 0.643941001444109m, 0.440229326671759m, 0.277380302998731m, 0.0474034776337722m, 0.626717687366657m, 0.947971291739741m, 0.809516092623928m, 0.475191045025402m, 0.449348163990899m },
                NullableDecimalMember = 0.484088544985597m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.866070436161332m, null, 0.0391170520668364m, 0.985963168403563m, 0.756764722670435m, 0.254080317075713m, 0.451628228954414m, null, null, 0.214248292056501m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, null, null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-12266812),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-71967704), DateTime.Now.AddSeconds(276661175), DateTime.Now.AddSeconds(-74697919), DateTime.Now.AddSeconds(327176481), DateTime.Now.AddSeconds(14085742), DateTime.Now.AddSeconds(78968072), DateTime.Now.AddSeconds(-303984516), DateTime.Now.AddSeconds(-300020832), DateTime.Now.AddSeconds(37300359), DateTime.Now.AddSeconds(32628845) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-64913676),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(16918235), DateTime.Now.AddSeconds(71664160), DateTime.Now.AddSeconds(36406734), null, DateTime.Now.AddSeconds(37776589), DateTime.Now.AddSeconds(92660079), DateTime.Now.AddSeconds(-104237919), DateTime.Now.AddSeconds(-450448), null, DateTime.Now.AddSeconds(305657741) },
            },
            new DataConvertersTestClass {
                StringMember = "sX\"lOUg#54",
                StringArrayMember = new string[] { "uaWR6@8upm", ":$,HKKprDA", "7,gqDCMx0D", "lu;OX3:52R", "r0MVVDB%Yd", "pTHx$kVCat", ".Y?4'ILx0x", "mjn6l$6eny", "wgk5%umtoj", "%B4Wuexav:" },
                CharMember = 'x',
                CharArrayMember = new char[] { 'w', 'p', 'B', '7', 'X', '6', 'y', 'Y', 'F', 'W' },
                NullableCharMember = 'D',
                NullableCharArrayMember = new Nullable<char>[] { 'd', null, 'w', null, '\'', null, '%', ',', 'p', '2' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, false, true, false, true, true, false, true, true, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, false, true, false, true, null, null, null, null },
                ByteMember = 35,
                ByteArrayMember = new byte[] { 32, 171, 196, 245, 250, 202, 123, 7, 95, 62 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, null, 44, 155, null, null, null, null, 104, 210 },
                SByteMember = 91,
                SByteArrayMember = new sbyte[] { -60, -85, -109, 30, 112, 96, 99, -104, 102, 105 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 119, 110, null, 6, null, 99, -84, null, -110, 3 },
                Int16Member = 16193,
                Int16ArrayMember = new short[] { -15402, 21037, -29173, 7566, 1340, -16703, 26176, -7301, -23575, 13916 },
                NullableInt16Member = -32550,
                NullableInt16ArrayMember = new Nullable<short>[] { null, 12367, 3246, null, -12982, -28187, 20383, -23675, -2497, 14985 },
                UInt16Member = 63540,
                UInt16ArrayMember = new ushort[] { 64858, 50733, 62382, 50687, 55748, 59314, 64108, 22026, 17393, 43354 },
                NullableUInt16Member = 10366,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, null, 12185, 49745, 12488, 24279, null, null, 64744, 12408 },
                Int32Member = 881634659,
                Int32ArrayMember = new int[] { -1528638175, 1879549741, -1357666461, 1036755064, -1287923623, -993336073, -749523026, 935083276, 1664775067, 820823304 },
                NullableInt32Member = -933756128,
                NullableInt32ArrayMember = new Nullable<int>[] { null, null, -2078622763, -401447491, null, -1775341615, 1781141365, 815937082, null, 1382950323 },
                UInt32Member = 4220599527,
                UInt32ArrayMember = new uint[] { 2682263102, 1066172423, 262147141, 1115769013, 3861718067, 3908560438, 2533187281, 2065087720, 2969163807, 961685117 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3560654763, null, null, 1942162451, 2305488061, 1841939693, 2961308012, null, 984096085, 2307575869 },
                Int64Member = 5713161139293693404,
                Int64ArrayMember = new long[] { -2107284981715892416, -2651982264265079026, 5086612212346282777, 195512875644333089, -245936495166124007, -3953152363940788439, 4452693916422509729, -579659149229146113, -1150188376569458423, 2323870064183986434 },
                NullableInt64Member = -4274548915632008026,
                NullableInt64ArrayMember = new Nullable<long>[] { null, 2055056628634437650, null, -4572149571849557497, 5491700146279871640, 6568668636523806070, null, -2825155272909894510, -676489824585970347, -7659396559641529721 },
                UInt64Member = 13452934131906741110,
                UInt64ArrayMember = new ulong[] { 16605599790864825956, 11623633646577296179, 3027025652714155158, 395696150872626407, 2675951860420679652, 4728794642932911637, 3638995078103505361, 9264376971461869519, 4749965306108941813, 8918743269798275193 },
                NullableUInt64Member = 14522697553742415560,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 16162166114581153555, 5638511841082173542, 13008301212797485689, 13083814667910051985, null, 18185057432819270235, 8629797715542808832, 9528481694771022946, null, null },
                SingleMember = 0.3924F,
                SingleArrayMember = new float[] { 0.0142F, 0.0444F, 0.2604F, 0.7006F, 0.3221F, 0.9843F, 0.0071F, 0.6326F, 0.5003F, 0.0881F },
                NullableSingleMember = 0.659F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.1671F, 0.7518F, null, 0.8861F, 0.9287F, 0.0374F, 0.7831F, null, null },
                DoubleMember = 0.2615,
                DoubleArrayMember = new double[] { 0.3586, 0.7066, 0.7271, 0.1194, 0.0186, 0.4607, 0.2424, 0.6821, 0.0444, 0.8975 },
                NullableDoubleMember = 0.2205,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.095, null, 0.6179, 0.3309, 0.6772, 0.3948, 0.1173, 0.28, 0.8815, null },
                DecimalMember = 0.302818695354461m,
                DecimalArrayMember = new decimal[] { 0.726160127079617m, 0.722028443393135m, 0.579245226845504m, 0.423715788208595m, 0.106280497935834m, 0.359082280129159m, 0.41342118924503m, 0.404444140444052m, 0.295290547020466m, 0.670147013722289m },
                NullableDecimalMember = 0.0710063725967998m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, null, 0.195455651794738m, 0.369419084871461m, 0.733020815315864m, null, 0.378406025929803m, null, null, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(194188340),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(323970214), DateTime.Now.AddSeconds(264799537), DateTime.Now.AddSeconds(-21466167), DateTime.Now.AddSeconds(346967639), DateTime.Now.AddSeconds(-7667285), DateTime.Now.AddSeconds(-261371680), DateTime.Now.AddSeconds(-87384372), DateTime.Now.AddSeconds(96308196), DateTime.Now.AddSeconds(-209370782), DateTime.Now.AddSeconds(230143343) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-306406168),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, null, DateTime.Now.AddSeconds(-98973172), DateTime.Now.AddSeconds(95057753), DateTime.Now.AddSeconds(275722728), DateTime.Now.AddSeconds(-153199329), DateTime.Now.AddSeconds(-181346530), null, null, null },
            },
            new DataConvertersTestClass {
                StringMember = "jycUgQp42Z",
                StringArrayMember = new string[] { "j1b$78Z@x0", "1UdPodngOx", "ivo0G6XUXP", "BEQVg0pUos", "jPz18a5raV", "fmCV9RSot5", "sEEtg1.P92", "DUeNdNRO?b", "4Qg8ok$HGF", "K%QYO7jS9o" },
                CharMember = 's',
                CharArrayMember = new char[] { 'b', 'M', 'X', 'U', 'h', '7', 'p', 'd', 'v', 'f' },
                NullableCharMember = '1',
                NullableCharArrayMember = new Nullable<char>[] { null, 'T', ' ', null, 'H', 'w', null, 'u', null, null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, false, false, true, true, true, false, true, false, false },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, true, false, true, true, false, false, false, null },
                ByteMember = 252,
                ByteArrayMember = new byte[] { 49, 98, 98, 250, 208, 181, 118, 160, 176, 176 },
                NullableByteMember = 77,
                NullableByteArrayMember = new Nullable<byte>[] { 58, null, 143, null, 10, 167, null, null, 53, 15 },
                SByteMember = 109,
                SByteArrayMember = new sbyte[] { 1, -1, -40, 106, 33, 31, 83, -28, 21, -117 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -40, 104, null, null, -30, -80, null, 100, -86, null },
                Int16Member = -31957,
                Int16ArrayMember = new short[] { -26328, 6424, 15292, 9547, -4422, -11987, 8624, 29896, -21690, 21536 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -26974, null, null, 14698, -2259, 11168, -12665, -2611, null, -24225 },
                UInt16Member = 7411,
                UInt16ArrayMember = new ushort[] { 61161, 36369, 51435, 34087, 38562, 47713, 64892, 6499, 6226, 56704 },
                NullableUInt16Member = 29429,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 58658, 14847, 22130, null, 62558, null, 18056, 56161, null },
                Int32Member = 1918297412,
                Int32ArrayMember = new int[] { -1551534033, 181030174, 640903473, 215140797, -1327358746, -1993809537, 1394879678, 499337591, 1550637949, -441720845 },
                NullableInt32Member = 949957367,
                NullableInt32ArrayMember = new Nullable<int>[] { null, null, 776984957, null, -1426741834, -1993337151, null, -1969814125, -574194981, -1590410995 },
                UInt32Member = 3037765409,
                UInt32ArrayMember = new uint[] { 909354375, 597685446, 2669892858, 3313969304, 3901413769, 101186544, 717356320, 2841235104, 3744363155, 1084107714 },
                NullableUInt32Member = 978189823,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1089425157, null, null, 2460262832, null, 301849016, null, 2923710515, 3779990413, 178325126 },
                Int64Member = -6441866164891345953,
                Int64ArrayMember = new long[] { 5921435633881753647, 812119202798157554, -8008853646717906436, 5061031202624378716, 4408470712997463064, 2108958476856686935, 3021812822458821248, 6192346257457325456, -4588689622973993471, -4694687170766523564 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -3150883932878377467, -3356204784823517134, 7399462796486345308, -8708461241574153310, 4988466369078974403, null, null, -7766958296998385239, null, null },
                UInt64Member = 3613505252503205577,
                UInt64ArrayMember = new ulong[] { 16681375062479118173, 12516096134859737081, 17198111255295582414, 10876409652180160388, 5201544998307560712, 6761434043888271130, 8064245875089884807, 4861331607085004942, 15519655264196302536, 12461667821311335915 },
                NullableUInt64Member = 14724356792587990276,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 16285543748871058863, 8294592981610623130, 13547611957567216386, 10999267110610177983, 2154527751465406478, 16209774192394033620, 12410039057023170149, 7086028379832789528, null, 5818725083164789969 },
                SingleMember = 0.5136F,
                SingleArrayMember = new float[] { 0.299F, 0.0783F, 0.9191F, 0.3691F, 0.6553F, 0.6579F, 0.8454F, 0.0148F, 0.0609F, 0.4912F },
                NullableSingleMember = 0.268F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.8364F, 0.0464F, 0.2163F, 0.1866F, null, 0.0552F, 0.9405F, 0.2491F, 0.3473F, 0.4178F },
                DoubleMember = 0.817,
                DoubleArrayMember = new double[] { 0.603, 0.4467, 0.2167, 0.8033, 0.8893, 0.0358, 0.9469, 0.9413, 0.3421, 0.6602 },
                NullableDoubleMember = 0.592,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.6132, 0.8665, 0.8198, 0.4347, 0.8328, 0.051, null, 0.3049, null, 0.7672 },
                DecimalMember = 0.438362498692176m,
                DecimalArrayMember = new decimal[] { 0.207724308810781m, 0.211271748810497m, 0.134770442531616m, 0.878247455378527m, 0.388162546047828m, 0.299174519973996m, 0.438506105965404m, 0.498299901062736m, 0.957147342783961m, 0.862274774671182m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, null, 0.663904761561927m, 0.781416206282126m, 0.941130511975045m, 0.0179172206915361m, null, 0.428923848101055m, null, 0.250345325659156m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null, null },
                DateTimeMember = DateTime.Now.AddSeconds(-75111930),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-121536836), DateTime.Now.AddSeconds(-229477551), DateTime.Now.AddSeconds(-97041390), DateTime.Now.AddSeconds(-51179026), DateTime.Now.AddSeconds(-280031922), DateTime.Now.AddSeconds(215864158), DateTime.Now.AddSeconds(118497124), DateTime.Now.AddSeconds(25966673), DateTime.Now.AddSeconds(295633320), DateTime.Now.AddSeconds(132064582) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-283965421), DateTime.Now.AddSeconds(86262364), DateTime.Now.AddSeconds(58652785), null, DateTime.Now.AddSeconds(42805458), DateTime.Now.AddSeconds(309620833), DateTime.Now.AddSeconds(-345592982), null, null, null },
            },
            new DataConvertersTestClass {
                StringMember = ":RHv::5'Wv",
                StringArrayMember = new string[] { "Ynr!Q2Iuj4", "dA?!?y5xTw", "fQmsMiwlCh", "%rtVmKV\" L", "Ug!umqb;2o", "wJv2J@$X#B", "@GeQ$Sw'bT", "6oH?o0.tlA", "Of4j,i:;b2", "$Fvd7tZbS4" },
                CharMember = '4',
                CharArrayMember = new char[] { 'T', '\'', 'P', 'y', 'R', 'P', '3', '9', '7', 'l' },
                NullableCharMember = 'p',
                NullableCharArrayMember = new Nullable<char>[] { '\'', '6', null, '\'', ';', 'K', 'J', null, null, null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, false, false, false, false, false, true, true, false, false },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, null, false, null, false, true, true, true, null, false },
                ByteMember = 211,
                ByteArrayMember = new byte[] { 54, 248, 128, 20, 198, 173, 141, 231, 198, 217 },
                NullableByteMember = 164,
                NullableByteArrayMember = new Nullable<byte>[] { 156, null, 250, 210, 105, 57, 158, null, 87, 91 },
                SByteMember = 80,
                SByteArrayMember = new sbyte[] { 117, -106, 103, -24, 14, -41, -120, -104, -80, -8 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -48, null, 105, null, null, null, 36, -33, -26, 90 },
                Int16Member = 17312,
                Int16ArrayMember = new short[] { 11354, 18111, 29058, -9544, 23973, -14303, 25771, 20164, 6009, 29039 },
                NullableInt16Member = -8008,
                NullableInt16ArrayMember = new Nullable<short>[] { null, 22378, -30965, null, 28509, null, -30975, null, null, null },
                UInt16Member = 12237,
                UInt16ArrayMember = new ushort[] { 59974, 12369, 35690, 23645, 14448, 10124, 25196, 61744, 188, 56434 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 55115, null, 13634, 39141, 17397, null, null, null, 53851, 32904 },
                Int32Member = -622680879,
                Int32ArrayMember = new int[] { -400747127, -473053070, 779140024, -1200957559, 673680027, 1310330584, 63167122, -1839123924, 132679449, -964136414 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { null, 949811244, null, 651262296, 1382086214, -544689477, null, null, -1850233382, -1485553347 },
                UInt32Member = 2673442445,
                UInt32ArrayMember = new uint[] { 1714814181, 114802761, 3599280193, 3844330643, 2289874632, 1707143833, 4199030421, 98700468, 2810058408, 788383806 },
                NullableUInt32Member = 1548538870,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3494957766, 23883357, null, null, null, 2272954767, 3372067043, 603843848, 3700807947, 4194371573 },
                Int64Member = 6414443787359267416,
                Int64ArrayMember = new long[] { -7261891131965109138, -238901292039299640, 846379342661208560, -2434207631127732514, -3255330821381421804, -5078425594801517144, -3707484097209971474, -1413621731017013355, 7581750419035504184, -692627141892714064 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { 3957984457460237335, -1027685055466596394, 4203683669402643321, 1482366352936802979, null, 5708867974384018456, -1461082134153979871, 8991692869374072746, -7646593589159049585, 1521274464458221197 },
                UInt64Member = 12603734453712036742,
                UInt64ArrayMember = new ulong[] { 8909123939254278174, 10067309259992031235, 11201306275009204139, 4694454510853377241, 13969720136731095661, 4466124348452157383, 598883020538262665, 4419799896399001213, 12317888700426646813, 2204466343752855553 },
                NullableUInt64Member = 6736544139334901434,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 14207312883159943324, null, 10647237780068653653, 16243939828269732635, 8045103740004372709, 1030743974701356889, null, 3534651543633482534, null, 3872426959736889335 },
                SingleMember = 0.4935F,
                SingleArrayMember = new float[] { 0.7169F, 0.7236F, 0.5372F, 0.3602F, 0.6285F, 0.3283F, 0.8942F, 0.7192F, 0.7823F, 0.9923F },
                NullableSingleMember = 0.4554F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.5122F, 0.4316F, null, null, null, 0.9291F, 0.965F, null, 0.9558F },
                DoubleMember = 0.4832,
                DoubleArrayMember = new double[] { 0.0392, 0.403, 0.6171, 0.9408, 0.2311, 0.1286, 0.7278, 0.9069, 0.6179, 0.0783 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.8883, 0.6124, null, 0.2355, null, 0.1619, 0.1075, 0.813, null },
                DecimalMember = 0.40660081149276m,
                DecimalArrayMember = new decimal[] { 0.111577026520907m, 0.211947490218848m, 0.619419970525116m, 0.116965019449586m, 0.959158205823412m, 0.825059343682223m, 0.541599364347346m, 0.198920630979458m, 0.309659069007208m, 0.0750366295482783m },
                NullableDecimalMember = 0.510796359483496m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.804380684580351m, 0.275948198470413m, 0.981138180501596m, 0.44757942844562m, null, 0.694350440053128m, 0.669314283799754m, null, 0.749176104984244m, 0.855403669421656m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(291293138),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-107518262), DateTime.Now.AddSeconds(-227602273), DateTime.Now.AddSeconds(-247398047), DateTime.Now.AddSeconds(-338068061), DateTime.Now.AddSeconds(196633326), DateTime.Now.AddSeconds(-333605278), DateTime.Now.AddSeconds(-87126601), DateTime.Now.AddSeconds(-308366365), DateTime.Now.AddSeconds(-334988799), DateTime.Now.AddSeconds(82027301) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-337811113),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-136780922), DateTime.Now.AddSeconds(-305631507), DateTime.Now.AddSeconds(-46783067), DateTime.Now.AddSeconds(-209755903), null, null, DateTime.Now.AddSeconds(-274543924), null, DateTime.Now.AddSeconds(-61488578), DateTime.Now.AddSeconds(-158227653) },
            },
            new DataConvertersTestClass {
                StringMember = "\"O2Zs#W4N0",
                StringArrayMember = new string[] { "wcZpE? p.z", "7bYeKf2IiZ", "#KA:9XO9n0", "!U9,gG!DIE", "o4 ZWm3bSN", "Akd!?t5R%S", "Gg,\"rs;eT%", "am;kRxcqQJ", "FcCesDW0,V", "%ffU!QjqrR" },
                CharMember = 'E',
                CharArrayMember = new char[] { 'R', 'q', '\'', 'X', 'z', 'Q', 'A', 'F', ',', 'I' },
                NullableCharMember = 'N',
                NullableCharArrayMember = new Nullable<char>[] { 'o', 'k', null, 'b', 'Y', 'x', null, 'S', null, null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, false, true, true, false, false, true, true, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, true, false, false, true, true, null, false, false },
                ByteMember = 55,
                ByteArrayMember = new byte[] { 64, 247, 61, 70, 197, 185, 82, 232, 84, 131 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 237, 109, 237, 71, 30, 251, null, 80, null, null },
                SByteMember = -78,
                SByteArrayMember = new sbyte[] { -109, 96, 118, -128, 26, 21, -98, -83, 113, -107 },
                NullableSByteMember = -74,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 112, null, 6, null, -22, 14, -64, 18, 43, -104 },
                Int16Member = 24761,
                Int16ArrayMember = new short[] { 2700, 19491, -32274, -12482, -8886, 18799, -7808, 17367, -364, 16557 },
                NullableInt16Member = -20237,
                NullableInt16ArrayMember = new Nullable<short>[] { null, null, null, null, -30686, -25167, null, 15029, null, 24782 },
                UInt16Member = 31970,
                UInt16ArrayMember = new ushort[] { 41642, 30835, 262, 59105, 39893, 44903, 1591, 1375, 33797, 21383 },
                NullableUInt16Member = 27542,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 52641, 19804, 5891, 45518, 31928, 63728, null, 40602, 40745 },
                Int32Member = 1567721549,
                Int32ArrayMember = new int[] { 660987995, 96778225, 399432323, 1745634534, 103595720, 699005199, 1856537819, -1198325849, -460291883, -331686333 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { 1239174255, -1065774264, -734225614, null, null, 606157517, -6544935, -1069160553, null, -2064869850 },
                UInt32Member = 1728349439,
                UInt32ArrayMember = new uint[] { 3042479406, 311609138, 1020908710, 3820375202, 1979746105, 4229528850, 3268923319, 384370584, 1907368509, 326007386 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 314396617, 1631309425, 406325158, 4178727086, null, null, null, 3806796800, 4068977656, 4250986886 },
                Int64Member = 2231274921127969864,
                Int64ArrayMember = new long[] { 811913289937742295, -2651733682207857311, -1576467188189541938, 7829787678843584771, 8749710287384636294, 5372611761142540017, -7760091579846150399, -1274191988886324863, 7693488555892507843, -7147770743061134332 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, -6652015006903093512, 1074144123894018888, null, 4320539269982507122, 1955976847411717604, null, -8294618697365658613, 3333456487752132693, null },
                UInt64Member = 13393864545290742925,
                UInt64ArrayMember = new ulong[] { 16034834677816927302, 16328540381360863174, 16311551428154793182, 1981138321744018975, 11032423731385788531, 12819606694150465698, 4848338693711332913, 15157162039928231666, 7572963556599459326, 14928117157820254302 },
                NullableUInt64Member = 8545089669159825434,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 13795501365640168642, null, 15869532841771336863, 12118294343968991928, 9041393474120334204, 2458992058339327019, null, 7888593047478257913, 15243644225650641411, 3171352421951692949 },
                SingleMember = 0.5222F,
                SingleArrayMember = new float[] { 0.4975F, 0.8639F, 0.9258F, 0.6568F, 0.5639F, 0.3837F, 0.5555F, 0.423F, 0.7765F, 0.6166F },
                NullableSingleMember = 0.6155F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.1883F, null, 0.2244F, null, null, null, null, 0.6289F, 0.8908F, 0.1677F },
                DoubleMember = 0.1377,
                DoubleArrayMember = new double[] { 0.9534, 0.8213, 0.9001, 0.3523, 0.1625, 0.8725, 0.7255, 0.985, 0.5514, 0.8312 },
                NullableDoubleMember = 0.146,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.4413, 0.9232, null, null, 0.4422, 0.0381, 0.4358, 0.9323, 0.9453, 0.3467 },
                DecimalMember = 0.157884725102729m,
                DecimalArrayMember = new decimal[] { 0.492101095758344m, 0.243359832870728m, 0.171051642343549m, 0.593621008892488m, 0.0413672865841492m, 0.69472634365176m, 0.301481150646384m, 0.640789895798967m, 0.65948519751732m, 0.189402317511079m },
                NullableDecimalMember = 0.389249863101454m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.208824437218923m, null, null, 0.107062808136626m, 0.0160477154981445m, null, 0.923830376586894m, 0.233871645579702m, 0.638466690251966m, 0.183308448013819m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { null, null, null, Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(-203254239),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-18161340), DateTime.Now.AddSeconds(-171325855), DateTime.Now.AddSeconds(237078428), DateTime.Now.AddSeconds(-133522204), DateTime.Now.AddSeconds(238501577), DateTime.Now.AddSeconds(-179432880), DateTime.Now.AddSeconds(-23206236), DateTime.Now.AddSeconds(-173854459), DateTime.Now.AddSeconds(-142126165), DateTime.Now.AddSeconds(-22130963) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-270868060),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(-50924213), DateTime.Now.AddSeconds(183587225), DateTime.Now.AddSeconds(94676465), DateTime.Now.AddSeconds(300048921), DateTime.Now.AddSeconds(8043061), DateTime.Now.AddSeconds(99460301), DateTime.Now.AddSeconds(120925623), DateTime.Now.AddSeconds(226453878), DateTime.Now.AddSeconds(98219927) },
            },
            new DataConvertersTestClass {
                StringMember = "zMBd,3q9Bf",
                StringArrayMember = new string[] { "xGs3SF.w!j", "Vrn6:Eh!6T", "EJYoJ0VfVI", "twq6 rJ6mX", "8%0SgIb qK", " ui;APhDUl", "e895yr;k$j", "gCy7bmE\"XL", "9NBy1%QQRa", "v%2rw7oG17" },
                CharMember = 'J',
                CharArrayMember = new char[] { '7', '0', 'V', 'b', 's', 'J', 's', '#', '6', 'Y' },
                NullableCharMember = 'C',
                NullableCharArrayMember = new Nullable<char>[] { 'I', 'P', '#', 'i', 'E', 's', null, 'p', 'J', 'o' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, true, true, false, false, true, false, false, false, false },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, null, null, false, true, null, true, true, true, true },
                ByteMember = 152,
                ByteArrayMember = new byte[] { 235, 58, 116, 151, 183, 27, 137, 101, 90, 175 },
                NullableByteMember = 16,
                NullableByteArrayMember = new Nullable<byte>[] { null, 145, 26, 111, 65, 194, 211, 54, 224, 188 },
                SByteMember = 112,
                SByteArrayMember = new sbyte[] { -40, -113, 57, 111, -57, -94, -37, 79, 6, -37 },
                NullableSByteMember = 87,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 112, -35, -28, 49, 56, 42, -84, null, 4, -66 },
                Int16Member = -21655,
                Int16ArrayMember = new short[] { 12316, -1073, -26182, -13068, -1065, -11581, -11664, 12676, 21397, -15034 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { 25128, -27334, 25393, null, 31055, 25842, 5981, 876, -15379, -13297 },
                UInt16Member = 8527,
                UInt16ArrayMember = new ushort[] { 54945, 9953, 6678, 35366, 8800, 65298, 27313, 27790, 12871, 33488 },
                NullableUInt16Member = 31259,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 60265, 6591, 27577, 21598, null, 19369, 8190, 15495, 1972 },
                Int32Member = -395415066,
                Int32ArrayMember = new int[] { 232973615, 2010522056, 1421734706, 968725869, -115177481, -655246769, 898920410, -2056127934, -918438039, -766477769 },
                NullableInt32Member = 1786743041,
                NullableInt32ArrayMember = new Nullable<int>[] { -203036764, 1249603832, null, -287291431, -992810039, 1716328248, -306165444, null, null, null },
                UInt32Member = 2696700907,
                UInt32ArrayMember = new uint[] { 2549174067, 2132869588, 323324735, 4128619081, 2011340589, 2274646004, 170171134, 2982798552, 4114269376, 1728088178 },
                NullableUInt32Member = 264135934,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 2605187749, 3164662937, 1324612243, null, 120815184, null, 4066016746, 3831684821, 2739694886 },
                Int64Member = -8216058885531191155,
                Int64ArrayMember = new long[] { 5218771607968369349, 4438594821482807093, -126421109878926146, -9136061100059970900, 2926237912118205418, -7129655842128400792, 2712531212048842938, -4963282739038043459, -4127029148280899933, 7318526712235581081 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -7304833477850879632, -7658439587869479875, -7258940587003298709, 4530617867811706533, 837350964021742293, null, -4366324334267941230, null, 8906995258801175718, -1234832140122692984 },
                UInt64Member = 6163757728882409565,
                UInt64ArrayMember = new ulong[] { 11170574555778064770, 17560416946767720669, 18035934085171815868, 11546036978183621197, 16791665022063671200, 11038601896836369322, 17243602263064629592, 10831805369717078413, 10725010193826462936, 1093312694484053809 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 1779211045219013423, null, 10449027718148272459, 8978638989809288515, null, null, 7363617001587023829, null, 14739308308696218957 },
                SingleMember = 0.3674F,
                SingleArrayMember = new float[] { 0.0458F, 0.8491F, 0.9431F, 0.1002F, 0.9996F, 0.6474F, 0.9338F, 0.0599F, 0.905F, 0.5847F },
                NullableSingleMember = 0.5265F,
                NullableSingleArrayMember = new Nullable<float>[] { null, null, 0.9418F, 0.0363F, 0.2023F, 0.7207F, 0.0616F, 0.2426F, null, null },
                DoubleMember = 0.8133,
                DoubleArrayMember = new double[] { 0.8564, 0.6887, 0.0669, 0.1236, 0.7083, 0.5628, 0.1827, 0.9073, 0.1183, 0.5538 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.8863, 0.5244, 0.0616, 0.3692, null, null, null, 0.1255, 0.1788, 0.5476 },
                DecimalMember = 0.0288361874120893m,
                DecimalArrayMember = new decimal[] { 0.209293814308068m, 0.342024455307547m, 0.597115899391516m, 0.915589673153026m, 0.0309012030280779m, 0.690421743546594m, 0.145258391797759m, 0.410458054923511m, 0.343490406579417m, 0.0125642041405513m },
                NullableDecimalMember = 0.160160050584841m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, 0.249021995399703m, 0.585721378999267m, 0.411956926762734m, 0.188845727038533m, null, 0.136293051944882m, null, 0.617351763683572m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-283028156),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(59595323), DateTime.Now.AddSeconds(-343843322), DateTime.Now.AddSeconds(-192192703), DateTime.Now.AddSeconds(-202620682), DateTime.Now.AddSeconds(143904016), DateTime.Now.AddSeconds(333450837), DateTime.Now.AddSeconds(-307272971), DateTime.Now.AddSeconds(-283199990), DateTime.Now.AddSeconds(-58316327), DateTime.Now.AddSeconds(147352393) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(324019322),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(341248466), DateTime.Now.AddSeconds(169024896), DateTime.Now.AddSeconds(201878302), DateTime.Now.AddSeconds(-266953660), null, DateTime.Now.AddSeconds(88425346), DateTime.Now.AddSeconds(275856863), DateTime.Now.AddSeconds(25785643), DateTime.Now.AddSeconds(50314278) },
            },
            new DataConvertersTestClass {
                StringMember = "W0G o!U5c7",
                StringArrayMember = new string[] { "3oO':1N@d9", "cmPn?9xD4G", "'iF@,I!Z7a", "VYu?GN7sdO", "1JDE,acaJv", "l,6d;rou l", ":OPj@Er1iD", "#eDNH06i5z", "61t@dCi;DH", "X0RC#chZ1i" },
                CharMember = 'F',
                CharArrayMember = new char[] { 'q', 'O', 'f', ';', 'X', 'Q', 'H', 'I', 'z', 'F' },
                NullableCharMember = 'd',
                NullableCharArrayMember = new Nullable<char>[] { null, 'x', null, '.', null, null, null, 'J', '0', 'P' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, false, false, true, false, false, false, false, false, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, false, true, false, false, null, null, false, null },
                ByteMember = 241,
                ByteArrayMember = new byte[] { 192, 223, 65, 13, 13, 70, 183, 246, 135, 131 },
                NullableByteMember = 113,
                NullableByteArrayMember = new Nullable<byte>[] { null, null, 121, null, 146, 72, 136, null, null, 227 },
                SByteMember = 81,
                SByteArrayMember = new sbyte[] { -108, 56, 34, -17, -127, -25, -81, -68, 99, -27 },
                NullableSByteMember = 71,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -59, -75, 76, -25, 69, -50, null, 54, null, -55 },
                Int16Member = 32328,
                Int16ArrayMember = new short[] { 7357, -10472, -9844, -15339, -9667, -19372, 21374, 22535, 16390, -7099 },
                NullableInt16Member = 31649,
                NullableInt16ArrayMember = new Nullable<short>[] { -16390, 8231, -6892, 30043, -13765, 26662, -27224, -32283, 27644, -11321 },
                UInt16Member = 29905,
                UInt16ArrayMember = new ushort[] { 6087, 6181, 10967, 26736, 22559, 2632, 60883, 17121, 33923, 23346 },
                NullableUInt16Member = 56963,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 31996, null, 47311, 20630, null, 30296, 47373, null, null, null },
                Int32Member = 1892999213,
                Int32ArrayMember = new int[] { 1174962990, -1289432400, -707128095, 90874404, 1855269641, 969643041, -312323888, 283803594, -1817341443, 581285695 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { null, -1999297972, null, -852678612, 41498968, 1158318421, 649613960, -695795887, 1071589953, 207272680 },
                UInt32Member = 2906753856,
                UInt32ArrayMember = new uint[] { 2144749708, 1628585916, 4203630611, 3317752980, 1600803939, 2939300571, 3626922267, 2929520659, 3056895711, 3352398307 },
                NullableUInt32Member = 1006907102,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, null, 1681898698, null, 993932776, 4229523745, null, null, 3410633839, null },
                Int64Member = -6654560066058647929,
                Int64ArrayMember = new long[] { -4650774296246270727, 7272854090668717108, 943725055088992627, 9179258530533960435, -2137373028660758973, 4569923757785987859, -7300726573613503337, 4301261517084112560, -7711081940506350847, -1763762527892230753 },
                NullableInt64Member = 34731978471418109,
                NullableInt64ArrayMember = new Nullable<long>[] { 2100366422491533677, 7229342818390618454, 7560739244854762254, 8525379420989422651, -1932153547864867809, 2079825333741937172, 2343396383326620559, -1053404251917671968, -5301467511755617570, -8773898766111291294 },
                UInt64Member = 16391693472969303463,
                UInt64ArrayMember = new ulong[] { 15786040384804817865, 16730181828158033655, 18205715484481108258, 11536652362780363653, 3582532551658039997, 7493355123487885926, 5002776021871917029, 14635602690457118651, 14934029908399677433, 7577648716992670574 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 9718687669675874909, 17786257464892549465, 116429424241990355, 16633547627570131147, 12465380523169513368, 16814766317398242691, 12469551390102350567, 2421293416513498215, 6629026657394059588 },
                SingleMember = 0.8978F,
                SingleArrayMember = new float[] { 0.547F, 0.3401F, 0.4311F, 0.6432F, 0.379F, 0.1554F, 0.8028F, 0.6842F, 0.96F, 0.1327F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.3263F, null, 0.8394F, null, null, 0.5226F, null, null, 0.1056F, 0.6203F },
                DoubleMember = 0.5259,
                DoubleArrayMember = new double[] { 0.965, 0.4649, 0.9478, 0.7049, 0.8988, 0.857, 0.376, 0.7282, 0.4128, 0.3222 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.0432, 0.5522, 0.6727, null, null, null, 0.1824, 0.7802, 0.3632, 0.2006 },
                DecimalMember = 0.541329641948438m,
                DecimalArrayMember = new decimal[] { 0.769564969331978m, 0.762555209036268m, 0.68287894451851m, 0.611844520305244m, 0.203512297225299m, 0.0294281496826481m, 0.795776636560836m, 0.341995109928983m, 0.0882037281746868m, 0.0907861197692191m },
                NullableDecimalMember = 0.522183793163712m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, 0.131971598896286m, 0.307909011294959m, 0.327287624622705m, 0.771940799577645m, null, null, null, 0.178075812979032m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-329652015),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-233218407), DateTime.Now.AddSeconds(-163947949), DateTime.Now.AddSeconds(31100065), DateTime.Now.AddSeconds(6470310), DateTime.Now.AddSeconds(101848788), DateTime.Now.AddSeconds(129021764), DateTime.Now.AddSeconds(119391623), DateTime.Now.AddSeconds(72875255), DateTime.Now.AddSeconds(-93254747), DateTime.Now.AddSeconds(221926742) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(15669276),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(94759258), DateTime.Now.AddSeconds(15960670), null, DateTime.Now.AddSeconds(336911877), DateTime.Now.AddSeconds(320927200), null, DateTime.Now.AddSeconds(-324059861), DateTime.Now.AddSeconds(202448559), null, DateTime.Now.AddSeconds(215372040) },
            },
        };
    }
}
