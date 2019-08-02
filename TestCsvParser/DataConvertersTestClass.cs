// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
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
                StringMember = (string)"nxs@uV:27b",
                StringArrayMember = new string[] { ".D:M,lE0# ", "0$;ftnC4%g", "5Y I!F8S9\"", "B.E@!DJnr ", "W8$c;p6MYH", "SJ3asmE5S6", "09;C%?\"!lp", "dmN%J$btSb", "Vv\"2y:G0Ze", " bU8Wk!!eA" },
                CharMember = (char)'h',
                CharArrayMember = new char[] { '\'', 'x', 'p', 'D', '9', 'O', 's', '%', 'k', 'x' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { '4', 'g', 'N', 'p', '"', 'h', 'V', 'G', '$', 'z' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, true, true, false, true, true, true, true, false },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { false, false, false, true, true, false, true, null, false, false },
                ByteMember = (byte)225,
                ByteArrayMember = new byte[] { 44, 104, 25, 252, 51, 94, 23, 210, 23, 28 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { 63, 209, 89, 93, 27, 237, 10, null, null, 108 },
                SByteMember = (sbyte)7,
                SByteArrayMember = new sbyte[] { -3, 126, 58, -91, -113, -20, 91, -55, 62, -24 },
                NullableSByteMember = (sbyte?)14,
                NullableSByteArrayMember = new sbyte?[] { 46, -29, null, null, null, -113, null, null, -125, 49 },
                Int16Member = (short)4506,
                Int16ArrayMember = new short[] { 29788, 15263, -24134, 8398, 19661, 11355, -2892, -14903, -2837, 8015 },
                NullableInt16Member = (short?)6661,
                NullableInt16ArrayMember = new short?[] { null, null, null, -29271, 26337, null, -7804, null, -18807, 1718 },
                UInt16Member = (ushort)6002,
                UInt16ArrayMember = new ushort[] { 62633, 3286, 17055, 58624, 62422, 44872, 51849, 57472, 43307, 881 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 64725, null, 36434, 40895, null, 35668, 41463, 8267, 28692, 18162 },
                Int32Member = (int)-1159404440,
                Int32ArrayMember = new int[] { -1994544424, -969785014, 956367193, -1902245104, 1268090446, -994657531, 1787407742, 1178721660, 93028921, 145510603 },
                NullableInt32Member = (int?)305245907,
                NullableInt32ArrayMember = new int?[] { 1170914388, -1265086750, -1660200461, -1082373689, -1736471041, -1702045187, 1538426124, null, 1586968480, 292474528 },
                UInt32Member = (uint)2288602534,
                UInt32ArrayMember = new uint[] { 556437011, 3291970171, 2400780254, 1017478670, 3543620784, 524525850, 418215508, 2614042980, 3319734670, 3180986449 },
                NullableUInt32Member = (uint?)126056557,
                NullableUInt32ArrayMember = new uint?[] { 3220977891, 1838552649, 80546635, 817651742, 3751800064, 3883582127, 672645160, null, null, 2240134308 },
                Int64Member = (long)-6618318153266231459,
                Int64ArrayMember = new long[] { 1668739352964317486, -8526865132869868781, -1135780628279707399, 8329286017102323516, -6439296730903857, -2335715670076265080, 5195715140833477212, -9036579809699351035, -2807354739460396020, 6770624490716831310 },
                NullableInt64Member = (long?)3525208117752966691,
                NullableInt64ArrayMember = new long?[] { null, 1677827159381489735, -4166773624333497878, null, -7390575282358260435, 2432758841201925873, null, 72184218260752082, 8235169216537259142, null },
                UInt64Member = (ulong)3033008713704848406,
                UInt64ArrayMember = new ulong[] { 3818148520404327147, 13729712869707351821, 739200780620127178, 18028422141535514292, 12829624132634595907, 6378436793627822044, 13832962233635326771, 4235835315490478040, 14957270506315713237, 17407001569079974864 },
                NullableUInt64Member = (ulong?)15687759000590357673,
                NullableUInt64ArrayMember = new ulong?[] { null, 6142346013716782548, null, null, 10367005620656688145, null, null, 17015622662277071084, null, 7675804677819931536 },
                SingleMember = (float)0.6429F,
                SingleArrayMember = new float[] { 0.7796F, 0.2927F, 0.9203F, 0.4649F, 0.4083F, 0.9932F, 0.1765F, 0.2899F, 0.6253F, 0.6236F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { 0.7293F, 0.3119F, 0.1918F, 0.1897F, null, 0.3017F, 0.4217F, 0.2838F, null, null },
                DoubleMember = (double)0.4671,
                DoubleArrayMember = new double[] { 0.1308, 0.7199, 0.8807, 0.08, 0.8479, 0.1082, 0.0868, 0.2242, 0.8311, 0.9283 },
                NullableDoubleMember = (double?)0.4587,
                NullableDoubleArrayMember = new double?[] { 0.6752, null, 0.1031, 0.5713, 0.3755, null, 0.5219, 0.3555, null, 0.7858 },
                DecimalMember = (decimal)0.138194423698911m,
                DecimalArrayMember = new decimal[] { 0.354069134385357m, 0.871164409849404m, 0.220935573438618m, 0.799937832076074m, 0.71613493827923m, 0.139022674476273m, 0.377331528988356m, 0.979741144915922m, 0.0271385577633691m, 0.333594638078285m },
                NullableDecimalMember = (decimal?)0.752451889567288m,
                NullableDecimalArrayMember = new decimal?[] { null, null, 0.737760983285383m, null, 0.0335452547453089m, 0.0194982872435349m, 0.180028598373769m, 0.885213670267357m, null, 0.117690881303368m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), null, null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-100010619),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(173017439), DateTime.Now.AddSeconds(-48717021), DateTime.Now.AddSeconds(130096469), DateTime.Now.AddSeconds(-229336036), DateTime.Now.AddSeconds(81681640), DateTime.Now.AddSeconds(51757973), DateTime.Now.AddSeconds(-151456795), DateTime.Now.AddSeconds(90524231), DateTime.Now.AddSeconds(-206839328), DateTime.Now.AddSeconds(-258209778) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(279130098),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(11913070), DateTime.Now.AddSeconds(-177994089), DateTime.Now.AddSeconds(23537070), null, DateTime.Now.AddSeconds(229049720), null, DateTime.Now.AddSeconds(-203518662), DateTime.Now.AddSeconds(-77465629), DateTime.Now.AddSeconds(161214342), DateTime.Now.AddSeconds(-89769908) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"v?y603#5Rv",
                StringArrayMember = new string[] { "Khn:kF\"LbG", "5QsGqCRbzw", "CgPR3g#KC!", "@Yj1Uo.dXR", "q6pXRfUsPx", "lrGrt0RWKE", "I5JgLh4:9B", "P,EiFS:' X", "Ma!bgaS:Hq", "WnSTI7wIM7" },
                CharMember = (char)'W',
                CharArrayMember = new char[] { 'f', 'O', 'j', 'q', '%', 'V', 'c', '1', '#', 'm' },
                NullableCharMember = (char?)'!',
                NullableCharArrayMember = new char?[] { 'l', null, null, 'P', null, 'B', 'o', 'Q', null, 'l' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, false, true, false, false, false, true, true, true },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { false, false, null, false, false, true, true, true, false, true },
                ByteMember = (byte)222,
                ByteArrayMember = new byte[] { 208, 205, 20, 190, 192, 65, 124, 171, 135, 14 },
                NullableByteMember = (byte?)180,
                NullableByteArrayMember = new byte?[] { null, null, 56, 224, null, 176, 211, 198, 121, 242 },
                SByteMember = (sbyte)70,
                SByteArrayMember = new sbyte[] { 105, -114, -88, 66, 104, 52, -6, -24, 98, -35 },
                NullableSByteMember = (sbyte?)29,
                NullableSByteArrayMember = new sbyte?[] { null, -19, 118, -57, 87, null, -109, -57, null, -21 },
                Int16Member = (short)7328,
                Int16ArrayMember = new short[] { -24, 12596, -18518, -14384, 14971, 23127, -16581, 30409, -27867, 27498 },
                NullableInt16Member = (short?)-21504,
                NullableInt16ArrayMember = new short?[] { 5352, -26262, -224, -27577, 30966, null, null, -20065, -31187, -16218 },
                UInt16Member = (ushort)46601,
                UInt16ArrayMember = new ushort[] { 64885, 5179, 32622, 7814, 60889, 61480, 40050, 21271, 16198, 16180 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 27018, null, 3679, null, 12331, 54232, 60771, null, 3588, 6068 },
                Int32Member = (int)1343178098,
                Int32ArrayMember = new int[] { 1899137769, 1019618135, 155747122, 260242881, 1135584878, -468572181, -1084308639, -1219525103, -1481556790, 945492954 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { 1431221101, null, -878485941, 2025940427, 130323216, 1049703339, null, null, 1984649781, -1772177061 },
                UInt32Member = (uint)2054009848,
                UInt32ArrayMember = new uint[] { 815084102, 1387990477, 2582981514, 2260063260, 1237298843, 1906701451, 12256216, 956823723, 2091148594, 2686077485 },
                NullableUInt32Member = (uint?)2650574189,
                NullableUInt32ArrayMember = new uint?[] { null, 3925739215, null, 2371022627, 3125922392, null, 3060185892, 2471582008, 2825264604, null },
                Int64Member = (long)-5897788907737677906,
                Int64ArrayMember = new long[] { -7111882327082684703, 7345254542497003348, -241150572428586153, -429715419143357582, -1670300938821076008, 1797082411128957858, -6928464958579365489, -323154091092025594, 8160696267414266286, -1293046770433057983 },
                NullableInt64Member = (long?)-6795045767872458661,
                NullableInt64ArrayMember = new long?[] { null, null, -8300849724775737049, -4199070432668609708, null, null, -3619466811014197040, 4857214632820078946, -2357786685101614334, 8462304329908156549 },
                UInt64Member = (ulong)7478113036304433782,
                UInt64ArrayMember = new ulong[] { 1011526829717914157, 9237335106644779057, 1296959706224446136, 9534503736950727769, 16511836495379972569, 15714465517261598226, 8388963762422915985, 15306901794458438659, 16478970267267058254, 6552719007921642149 },
                NullableUInt64Member = (ulong?)15367245017122696634,
                NullableUInt64ArrayMember = new ulong?[] { 12412211741723116317, 13831665515191292630, 6563471010916147063, null, 17145807681347281230, 583548801425787558, null, 14174741096727448301, 6872160340867036327, 12905176906266034408 },
                SingleMember = (float)0.7367F,
                SingleArrayMember = new float[] { 0.5518F, 0.7388F, 0.7528F, 0.2582F, 0.512F, 0.0113F, 0.5108F, 0.1545F, 0.8442F, 0.3253F },
                NullableSingleMember = (float?)0.1228F,
                NullableSingleArrayMember = new float?[] { 0.0485F, 0.2954F, 0.2469F, 0.2718F, null, null, 0.4831F, null, 0.478F, 0.4986F },
                DoubleMember = (double)0.3823,
                DoubleArrayMember = new double[] { 0.1705, 0.056, 0.2542, 0.3599, 0.3859, 0.8637, 0.4724, 0.7538, 0.7278, 0.7217 },
                NullableDoubleMember = (double?)0.9631,
                NullableDoubleArrayMember = new double?[] { null, 0.2813, 0.6193, 0.5802, 0.7629, 0.5892, 0.0917, null, null, null },
                DecimalMember = (decimal)0.0714141675603642m,
                DecimalArrayMember = new decimal[] { 0.451684686099964m, 0.638695912733067m, 0.180507885841889m, 0.814346585336303m, 0.121711462792806m, 0.205099878462544m, 0.28325443681481m, 0.0443822988515637m, 0.649312165868148m, 0.0734004653400744m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.78024717223842m, null, 0.0429191645434681m, null, null, 0.990440882737954m, 0.461777534085222m, 0.922797521074674m, 0.118696136455376m, 0.691135811475634m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-221572766),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(168727197), DateTime.Now.AddSeconds(217091664), DateTime.Now.AddSeconds(89718683), DateTime.Now.AddSeconds(-182639104), DateTime.Now.AddSeconds(140822356), DateTime.Now.AddSeconds(261833088), DateTime.Now.AddSeconds(-204759892), DateTime.Now.AddSeconds(-250178888), DateTime.Now.AddSeconds(-304543293), DateTime.Now.AddSeconds(-346245543) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-134088243),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-170787693), null, DateTime.Now.AddSeconds(-329068364), DateTime.Now.AddSeconds(265750599), DateTime.Now.AddSeconds(158685649), DateTime.Now.AddSeconds(-229572726), null, DateTime.Now.AddSeconds(-16443769), null, DateTime.Now.AddSeconds(-190852731) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"yd6Nhwnvma",
                StringArrayMember = new string[] { "9ZX9:4,4A7", "P?6T1ys7ZU", "t52Pw6r3\"6", "9uc33#Qfba", "#:Ibr8tnj5", "E3VR4EuINI", "NH.Q%skCm4", "iNg%Y1wau,", "MDUL2GYK y", "ZISSN,D'd " },
                CharMember = (char)'I',
                CharArrayMember = new char[] { 'g', 'B', 'z', '%', 'O', 's', 'q', '2', '?', '4' },
                NullableCharMember = (char?)'Y',
                NullableCharArrayMember = new char?[] { '5', null, null, null, '6', 'x', '5', '5', 's', null },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, true, false, true, false, true, false, false, false },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { false, true, null, null, null, false, false, null, null, null },
                ByteMember = (byte)217,
                ByteArrayMember = new byte[] { 213, 201, 101, 56, 75, 187, 234, 127, 99, 144 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { 198, 59, null, 87, 208, 167, 114, 225, 77, 165 },
                SByteMember = (sbyte)-119,
                SByteArrayMember = new sbyte[] { -94, 105, 57, -107, -33, 47, -65, -109, 36, -97 },
                NullableSByteMember = (sbyte?)62,
                NullableSByteArrayMember = new sbyte?[] { null, 69, -91, 122, 57, 92, 109, null, -108, null },
                Int16Member = (short)16703,
                Int16ArrayMember = new short[] { -25565, 10036, 21479, 18884, 12694, 11621, 6595, -8360, 11252, -32553 },
                NullableInt16Member = (short?)-16976,
                NullableInt16ArrayMember = new short?[] { 20309, null, null, -8290, 30298, 17762, -24916, 19503, 29355, -11084 },
                UInt16Member = (ushort)44634,
                UInt16ArrayMember = new ushort[] { 64923, 42780, 4531, 25290, 29580, 60881, 8331, 47223, 49611, 42999 },
                NullableUInt16Member = (ushort?)21960,
                NullableUInt16ArrayMember = new ushort?[] { 15705, null, 52036, 52050, 4291, 30822, 30078, 37454, 46454, 21077 },
                Int32Member = (int)1941671435,
                Int32ArrayMember = new int[] { 562291635, 804247551, 1959199629, 2143446154, 1454315453, -2142754448, 118037023, 84195097, -948464632, -1371639632 },
                NullableInt32Member = (int?)-2091407445,
                NullableInt32ArrayMember = new int?[] { null, -656476170, -1979488802, -840630013, -1023486443, -805573386, null, null, null, -51281467 },
                UInt32Member = (uint)4192928700,
                UInt32ArrayMember = new uint[] { 1169952684, 3998381582, 1330704905, 3640378801, 2689119457, 3063391694, 3956360916, 3332067557, 2197537125, 1079473511 },
                NullableUInt32Member = (uint?)1130319689,
                NullableUInt32ArrayMember = new uint?[] { 3479852843, 726383367, 3362850423, 1454901607, 2026565865, 3272039787, null, null, null, null },
                Int64Member = (long)5836576149377034838,
                Int64ArrayMember = new long[] { 8466597354529774347, 7978215037878905626, -1367277000875570371, -6058165987613309981, 2276360996928106847, 443593225787406200, -7601748039638010894, 527979375874348372, -348573931732744002, -1279938591751135116 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { null, -4188716860706604947, null, null, 781911688850159277, null, null, null, -3830145505513615723, 7919170760242231172 },
                UInt64Member = (ulong)6126331472148173921,
                UInt64ArrayMember = new ulong[] { 6955984383266022830, 3258823090621662952, 3172046474290602446, 1847641418154785045, 5333998780980928482, 13141039777793257016, 5755520041732178651, 14072877149222506335, 11784738173439655842, 13411517080731637018 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 7872059184283292101, null, 14851994033283550988, null, 10853153686288797640, null, 16119995094301107312, null, 14321389187209593078, 11148357082645867225 },
                SingleMember = (float)0.4587F,
                SingleArrayMember = new float[] { 0.1246F, 0.3348F, 0.8129F, 0.5532F, 0.0735F, 0.8847F, 0.8971F, 0.9258F, 0.2173F, 0.7831F },
                NullableSingleMember = (float?)0.7236F,
                NullableSingleArrayMember = new float?[] { null, 0.1682F, null, 0.2532F, 0.1263F, 0.0369F, 0.0918F, null, 0.6884F, 0.9162F },
                DoubleMember = (double)0.3607,
                DoubleArrayMember = new double[] { 0.4924, 0.7221, 0.3258, 0.4384, 0.4488, 0.7578, 0.7476, 0.2044, 0.5643, 0.2892 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.3493, 0.8496, 0.4124, 0.1131, 0.4829, 0.2235, 0.9787, null, null, null },
                DecimalMember = (decimal)0.135252745419393m,
                DecimalArrayMember = new decimal[] { 0.914724161808716m, 0.15144298838053m, 0.962606493366233m, 0.259640647684988m, 0.908326977355558m, 0.575453871663406m, 0.694898725345218m, 0.0333274495943112m, 0.938105304230985m, 0.454643108162397m },
                NullableDecimalMember = (decimal?)0.525701898394945m,
                NullableDecimalArrayMember = new decimal?[] { 0.936446503240823m, 0.600751085486613m, 0.239251115005115m, 0.481618914046147m, 0.348698578471643m, 0.182127718432866m, 0.184762566436437m, 0.364359551744703m, 0.613826882845642m, 0.172412295440404m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(192320252),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(37328862), DateTime.Now.AddSeconds(237705735), DateTime.Now.AddSeconds(232930579), DateTime.Now.AddSeconds(313362988), DateTime.Now.AddSeconds(-314314433), DateTime.Now.AddSeconds(-287961567), DateTime.Now.AddSeconds(31505914), DateTime.Now.AddSeconds(-155447826), DateTime.Now.AddSeconds(340915604), DateTime.Now.AddSeconds(-89827177) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-76284042),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-182806917), null, DateTime.Now.AddSeconds(-200920304), DateTime.Now.AddSeconds(-167250293), DateTime.Now.AddSeconds(188506083), DateTime.Now.AddSeconds(287067896), DateTime.Now.AddSeconds(-30258416), DateTime.Now.AddSeconds(96664389), DateTime.Now.AddSeconds(340788341), DateTime.Now.AddSeconds(-71782043) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"XqA1X;XW\"D",
                StringArrayMember = new string[] { "fTkY@J9zl:", "TaD,fZ!N8j", "x4#uotDq.,", "6BR@TFu!XD", "Bbem2#nMV$", "#JlhdxX;6Z", "GT?wISIIrZ", "2A?4hWah:r", "G;U?:wn7ps", "54K$$5l4dk" },
                CharMember = (char)'3',
                CharArrayMember = new char[] { 'a', 's', 'm', ':', 's', 'm', 'k', 'F', '#', '@' },
                NullableCharMember = (char?)'A',
                NullableCharArrayMember = new char?[] { 'a', null, 's', 'D', 'Y', 'Y', '4', null, null, 'j' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, false, false, false, true, true, false, false, false, false },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { false, false, false, true, null, null, true, true, null, true },
                ByteMember = (byte)198,
                ByteArrayMember = new byte[] { 160, 183, 172, 56, 23, 79, 44, 75, 122, 77 },
                NullableByteMember = (byte?)124,
                NullableByteArrayMember = new byte?[] { 133, 38, null, null, null, null, 212, 207, 27, 10 },
                SByteMember = (sbyte)32,
                SByteArrayMember = new sbyte[] { -67, -27, 123, 88, -70, 69, 99, -15, 104, 60 },
                NullableSByteMember = (sbyte?)99,
                NullableSByteArrayMember = new sbyte?[] { null, 33, 125, -51, null, 48, -104, -101, 93, 116 },
                Int16Member = (short)-32601,
                Int16ArrayMember = new short[] { -5765, -27458, -8589, -11558, 29848, -6270, 21239, -11588, 7244, -25867 },
                NullableInt16Member = (short?)null,
                NullableInt16ArrayMember = new short?[] { 20280, 16996, 25059, -17811, null, null, null, -30239, null, 4940 },
                UInt16Member = (ushort)8434,
                UInt16ArrayMember = new ushort[] { 5299, 42963, 20012, 45429, 7568, 40384, 8831, 23639, 28519, 18789 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 32804, 22878, 44458, 58233, null, 61471, 5917, null, 22510, 4785 },
                Int32Member = (int)269677596,
                Int32ArrayMember = new int[] { 655416891, 753766563, -531009876, 1490130293, 154113729, 462098171, 155085788, -236714326, -119875910, 1707645677 },
                NullableInt32Member = (int?)2128552654,
                NullableInt32ArrayMember = new int?[] { -1733349500, null, null, 1378620425, -464373942, 644385189, null, -853120296, -1393873910, 1494149052 },
                UInt32Member = (uint)2155420421,
                UInt32ArrayMember = new uint[] { 177430037, 200578330, 2609841249, 160841589, 2369447608, 2860932850, 2980546739, 2892730944, 1525999146, 1540259778 },
                NullableUInt32Member = (uint?)3446018157,
                NullableUInt32ArrayMember = new uint?[] { 784221261, null, null, 3635908332, null, null, null, null, null, 1893898702 },
                Int64Member = (long)3793847040882628860,
                Int64ArrayMember = new long[] { -2056243621575576106, 6210438624889884505, 6678692827299912893, -2608451078363111949, -4450200172812667865, 1397943949451461248, 6590989783784596392, 7877633111001986693, 563458802125312642, 8484540375468238815 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { null, null, -8152708307458383818, null, null, 5175549163643449223, null, null, null, 9055392369112557794 },
                UInt64Member = (ulong)11670472232143485850,
                UInt64ArrayMember = new ulong[] { 12482899714466214824, 7420768341310517710, 10675827034279325912, 12127386926304600171, 12555639014996957129, 13626879429265171437, 9259971606987706880, 12431439172299128906, 4628754766716379108, 428025284474703090 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 7806359430658627044, null, 404690779124872915, 509053492587711862, 17254612905313215685, 7860071640896640015, 10098735063712492399, null, null, null },
                SingleMember = (float)0.6035F,
                SingleArrayMember = new float[] { 0.5334F, 0.3462F, 0.6559F, 0.3855F, 0.2007F, 0.6867F, 0.3099F, 0.32F, 0.975F, 0.3968F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { 0.0296F, 0.4707F, null, 0.2737F, 0.9276F, 0.2248F, 0.3723F, null, 0.3633F, 0.2035F },
                DoubleMember = (double)0.5815,
                DoubleArrayMember = new double[] { 0.2682, 0.8386, 0.3542, 0.1135, 0.3931, 0.2355, 0.9996, 0.8488, 0.5247, 0.9978 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { null, 0.055, 0.965, null, null, null, 0.883, 0.4105, null, 0.8778 },
                DecimalMember = (decimal)0.592118625339176m,
                DecimalArrayMember = new decimal[] { 0.909129553432171m, 0.734588004059432m, 0.921957476959544m, 0.470187675892463m, 0.980175266964443m, 0.871920194882862m, 0.0621068380130952m, 0.00351095898240384m, 0.00959171494915696m, 0.598409787564729m },
                NullableDecimalMember = (decimal?)0.75752503506724m,
                NullableDecimalArrayMember = new decimal?[] { null, 0.525331030844399m, null, 0.442200600841176m, 0.348866741335423m, 0.0924890819436354m, null, 0.780478774933367m, 0.723564410919121m, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(271248852),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(290673843), DateTime.Now.AddSeconds(-146863546), DateTime.Now.AddSeconds(-33574406), DateTime.Now.AddSeconds(-106508880), DateTime.Now.AddSeconds(-17018822), DateTime.Now.AddSeconds(205284063), DateTime.Now.AddSeconds(-206473261), DateTime.Now.AddSeconds(1884567), DateTime.Now.AddSeconds(28084696), DateTime.Now.AddSeconds(176379462) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(5577775),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-115030309), null, DateTime.Now.AddSeconds(273086795), DateTime.Now.AddSeconds(-165523465), null, DateTime.Now.AddSeconds(296203508), DateTime.Now.AddSeconds(66026025), DateTime.Now.AddSeconds(-250473858), null, DateTime.Now.AddSeconds(-34625956) },
            },
            new DataConvertersTestClass {
                StringMember = (string)",er@S#U3 H",
                StringArrayMember = new string[] { "X?%PTQ%A4j", "1Yp r$?'7y", "7sEd3yVZt@", "c67iU'5@14", "7l !5m?sbV", "Gb#YW18ris", "!wd9H@oH1y", "P!OD:WtKuR", "8z$Y,A.jLp", "HNjI9KIN9." },
                CharMember = (char)'P',
                CharArrayMember = new char[] { '\'', ' ', '!', ' ', 'l', 'M', 'c', 'h', 'S', 'T' },
                NullableCharMember = (char?)'Z',
                NullableCharArrayMember = new char?[] { null, ';', '$', null, 'm', 'I', null, null, null, null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { false, true, true, false, false, true, false, false, false, false },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { false, null, false, false, false, false, null, false, null, null },
                ByteMember = (byte)104,
                ByteArrayMember = new byte[] { 194, 157, 135, 189, 201, 209, 243, 25, 22, 35 },
                NullableByteMember = (byte?)125,
                NullableByteArrayMember = new byte?[] { 138, null, 42, null, 42, 115, 150, 164, 150, 10 },
                SByteMember = (sbyte)45,
                SByteArrayMember = new sbyte[] { -86, 27, -125, 96, 77, 66, 38, 98, 126, -122 },
                NullableSByteMember = (sbyte?)92,
                NullableSByteArrayMember = new sbyte?[] { -45, 47, -71, -21, 70, -2, null, 98, null, 56 },
                Int16Member = (short)-20951,
                Int16ArrayMember = new short[] { 21575, -4311, -24161, -8296, -21748, -20228, 5999, -25586, -4309, 19255 },
                NullableInt16Member = (short?)null,
                NullableInt16ArrayMember = new short?[] { -4390, null, -28053, -30673, -24561, null, null, 17344, -29039, 26226 },
                UInt16Member = (ushort)7901,
                UInt16ArrayMember = new ushort[] { 9560, 60646, 18601, 61322, 41042, 62669, 1966, 12003, 47687, 2636 },
                NullableUInt16Member = (ushort?)64876,
                NullableUInt16ArrayMember = new ushort?[] { null, 26950, 63949, 31819, 34641, 14732, 12173, null, null, 21539 },
                Int32Member = (int)744410129,
                Int32ArrayMember = new int[] { -1515339343, 328225016, 1434209307, 1961228821, -1206702378, 1105788667, 1351506086, -646935996, -596362197, -697224435 },
                NullableInt32Member = (int?)1860234934,
                NullableInt32ArrayMember = new int?[] { null, -2103836223, null, -642633563, null, -834616672, -668294180, -1130142199, 135284091, 1338822736 },
                UInt32Member = (uint)1178356623,
                UInt32ArrayMember = new uint[] { 3084785721, 3693039236, 4066523719, 3410876177, 2168281150, 1832659161, 3443630738, 4173265898, 109176706, 1745433858 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 1368151824, 840387574, 292403726, 232924529, 1769753585, 3360118314, 3435713614, 3913408996, null, null },
                Int64Member = (long)-7638525663449373065,
                Int64ArrayMember = new long[] { 844204299562251016, 2015875830801177851, 6231271321627415432, 695333671878986730, -6163953930540794560, -6694190714192219919, 2041369583481145645, -6214590901771572103, 5444027956373891701, 8382100585807479773 },
                NullableInt64Member = (long?)-1596184563728501996,
                NullableInt64ArrayMember = new long?[] { null, -3516766384293235550, null, -4625706562347201037, null, 414650485146032453, -5505954402873369357, 504111209572835339, 2889098852485775427, -1877672091042525022 },
                UInt64Member = (ulong)1686780921828167673,
                UInt64ArrayMember = new ulong[] { 17333935150444654868, 11657574515034695195, 2627737294400628892, 15852681248582774764, 8588164956142828588, 17509925280536983929, 8859368656469940359, 1321471403697161658, 5916108874910952741, 3233322364076971128 },
                NullableUInt64Member = (ulong?)747317544574072658,
                NullableUInt64ArrayMember = new ulong?[] { null, 4681390289199306177, 9669172038354713125, 11018845971628550854, 6443751916343971112, null, 17728148150172547097, 17529335190208185325, 8344622631393234395, 7811370841584074730 },
                SingleMember = (float)0.1374F,
                SingleArrayMember = new float[] { 0.1963F, 0.6274F, 0.0096F, 0.6138F, 0.2498F, 0.4549F, 0.0677F, 0.9621F, 0.576F, 0.4815F },
                NullableSingleMember = (float?)0.5411F,
                NullableSingleArrayMember = new float?[] { 0.4497F, 0.7391F, 0.0679F, null, 0.668F, null, 0.9885F, null, 0.0281F, 0.8366F },
                DoubleMember = (double)0.7561,
                DoubleArrayMember = new double[] { 0.4986, 0.145, 0.4597, 0.8294, 0.2789, 0.2808, 0.6229, 0.2961, 0.4506, 0.261 },
                NullableDoubleMember = (double?)0.9563,
                NullableDoubleArrayMember = new double?[] { 0.8499, 0.8877, 0.7736, 0.4511, 0.3501, 0.422, 0.0796, 0.6985, null, null },
                DecimalMember = (decimal)0.330539173600515m,
                DecimalArrayMember = new decimal[] { 0.775617084827096m, 0.324278421385343m, 0.451627781824967m, 0.0643511321695294m, 0.933583273055769m, 0.784249131001648m, 0.201858499181391m, 0.00526159117243327m, 0.13056340959415m, 0.778143834685042m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { null, null, 0.750288790906914m, 0.424602683365626m, null, 0.464576121170342m, 0.935663929179154m, 0.123570304887169m, 0.382601727909689m, 0.168562420722359m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-37273109),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-146236167), DateTime.Now.AddSeconds(183649479), DateTime.Now.AddSeconds(331954866), DateTime.Now.AddSeconds(256154230), DateTime.Now.AddSeconds(-16957885), DateTime.Now.AddSeconds(-339714773), DateTime.Now.AddSeconds(200767488), DateTime.Now.AddSeconds(-145638256), DateTime.Now.AddSeconds(147551948), DateTime.Now.AddSeconds(-56204367) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(180865140),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-337873379), DateTime.Now.AddSeconds(-68509880), DateTime.Now.AddSeconds(-265228108), DateTime.Now.AddSeconds(143430198), DateTime.Now.AddSeconds(294748914), DateTime.Now.AddSeconds(261871858), DateTime.Now.AddSeconds(344470334), DateTime.Now.AddSeconds(250399568), null, null },
            },
        };
    }
}
