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
                StringMember = (string)"gI7#E7tLmO",
                StringArrayMember = new string[] { "E'79$W,YtK", "bzEi\"w8hNj", "viUqgyXajm", "nn?Vs3QUL!", "w0#BD4EYCe", " M%DqtuRdV", " l,Gu,95;N", "gnVT9@lTny", "u%XEmao92 ", "I ;?@L,?dO" },
                CharMember = (char)'"',
                CharArrayMember = new char[] { '#', 'd', 'G', '@', 's', 'I', 'j', '%', '!', 'S' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { null, null, null, null, '5', null, null, 'E', null, null },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, false, true, false, false, false, false, false, true, true },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { true, true, false, null, true, true, true, true, true, true },
                ByteMember = (byte)229,
                ByteArrayMember = new byte[] { 179, 33, 215, 161, 157, 187, 108, 161, 172, 127 },
                NullableByteMember = (byte?)100,
                NullableByteArrayMember = new byte?[] { null, 78, null, 168, 115, 155, null, 187, null, 63 },
                SByteMember = (sbyte)117,
                SByteArrayMember = new sbyte[] { -32, -15, -34, -90, -120, -24, 113, -16, -122, -9 },
                NullableSByteMember = (sbyte?)-14,
                NullableSByteArrayMember = new sbyte?[] { 28, null, null, 123, null, null, -119, null, -98, 49 },
                Int16Member = (short)-25897,
                Int16ArrayMember = new short[] { -11657, -18908, 31035, -31937, 5719, 8193, -19526, -29728, 20372, 310 },
                NullableInt16Member = (short?)28678,
                NullableInt16ArrayMember = new short?[] { 27643, null, 549, -16268, -21349, -1796, -16754, null, null, -12606 },
                UInt16Member = (ushort)15864,
                UInt16ArrayMember = new ushort[] { 731, 35985, 47948, 33943, 36892, 48057, 7712, 30088, 21892, 51212 },
                NullableUInt16Member = (ushort?)13615,
                NullableUInt16ArrayMember = new ushort?[] { 26376, 10720, null, null, 24425, 49654, null, 30538, 32092, null },
                Int32Member = (int)-2021120924,
                Int32ArrayMember = new int[] { -1768429088, 2079970566, -632067900, 1831020960, 227783688, 1394484682, -860856358, 1112178, -73154508, 1766724526 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { 1076741952, 143786957, -1145209968, null, 757442491, null, null, -2048875181, 694613438, 643228748 },
                UInt32Member = (uint)1602448099,
                UInt32ArrayMember = new uint[] { 3720425747, 1380969744, 518660555, 3816480575, 591736702, 4214169295, 1102158071, 4277832907, 3829056943, 2201159682 },
                NullableUInt32Member = (uint?)3868048974,
                NullableUInt32ArrayMember = new uint?[] { 1125153963, 2422154675, 2028389889, null, null, 1877747519, 1977652901, 1602286273, 904667157, null },
                Int64Member = (long)4021112625118839777,
                Int64ArrayMember = new long[] { -3879681453839732054, -1570224604494598156, 2168939291508569835, 3997378510630898571, -5572147270284943245, 8049085501486540681, 822895509630493133, 7653473580681992217, 2978760550075551222, 8250584362509144010 },
                NullableInt64Member = (long?)8498042146416410751,
                NullableInt64ArrayMember = new long?[] { 8138267342978055277, null, 819833025943538404, -2870090452522874682, null, null, 1786760552454046769, 3815506328138960, 5344838362067771046, null },
                UInt64Member = (ulong)17320211971464861207,
                UInt64ArrayMember = new ulong[] { 10521588253776163843, 10645562105990268144, 5974742211179826307, 9811865711140378280, 6433179741468926654, 8389276508166962723, 8615012090937478873, 3580656184681229642, 5021454368519708826, 17782949907977309829 },
                NullableUInt64Member = (ulong?)1374353189512552167,
                NullableUInt64ArrayMember = new ulong?[] { 11640987567448651175, null, null, 2183317558161002680, null, 8709940707072353423, null, 679130649006547041, null, 8291011957991875864 },
                SingleMember = (float)0.5012548F,
                SingleArrayMember = new float[] { 0.6827997F, 0.2981908F, 0.2565546F, 0.9510862F, 0.4160863F, 0.1503882F, 0.8316167F, 0.7158437F, 0.5638958F, 0.6527448F },
                NullableSingleMember = (float?)0.1639443F,
                NullableSingleArrayMember = new float?[] { null, 0.2480053F, 0.3103343F, null, null, 0.6190175F, 0.2193163F, 0.4577089F, null, 0.6127911F },
                DoubleMember = (double)0.6443002,
                DoubleArrayMember = new double[] { 0.26199466, 0.2410006, 0.83548359, 0.38969011, 0.59691293, 0.36779889, 0.2178957, 0.49687245, 0.66497737, 0.35022262 },
                NullableDoubleMember = (double?)0.34974304,
                NullableDoubleArrayMember = new double?[] { null, 0.56000084, 0.16336363, 0.51162347, 0.18470235, null, 0.12772418, 0.86136902, 0.99616899, 0.06512557 },
                DecimalMember = (decimal)0.790549447662453m,
                DecimalArrayMember = new decimal[] { 0.231193807549399m, 0.877562935872731m, 0.601045922656099m, 0.659960335427877m, 0.0862945542141304m, 0.417451353938063m, 0.694297276294929m, 0.119355146363077m, 0.298136304737132m, 0.433774119910679m },
                NullableDecimalMember = (decimal?)0.0785800055966619m,
                NullableDecimalArrayMember = new decimal?[] { null, 0.00114556262322961m, 0.589249746217043m, 0.353855570477366m, null, 0.054264322414186m, 0.566818151421295m, 0.749541687196839m, 0.886824931431014m, 0.00677723344731016m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-238832158),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(112951089), DateTime.Now.AddSeconds(322388800), DateTime.Now.AddSeconds(-231171793), DateTime.Now.AddSeconds(125067782), DateTime.Now.AddSeconds(73000849), DateTime.Now.AddSeconds(-23597029), DateTime.Now.AddSeconds(-210963695), DateTime.Now.AddSeconds(-229922309), DateTime.Now.AddSeconds(327812668), DateTime.Now.AddSeconds(-184449965) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-10053265),
                NullableDateTimeArrayMember = new DateTime?[] { null, DateTime.Now.AddSeconds(231290297), null, DateTime.Now.AddSeconds(321197931), DateTime.Now.AddSeconds(-239675559), DateTime.Now.AddSeconds(-40848106), null, DateTime.Now.AddSeconds(-168970185), DateTime.Now.AddSeconds(99180398), null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"!Vi?$j:1Ru",
                StringArrayMember = new string[] { "Hb01xAN6rU", "WAD;FhxgXx", "Gw;Y3ZmhGD", "j.rH'4VLO@", "$EVJ'!Ss7n", "8ierohkc$Y", "k20n7jg@%X", "MlRczCb3g9", "G!xqRBgAqh", "T'N;hSb4Yf" },
                CharMember = (char)'U',
                CharArrayMember = new char[] { '%', 'I', 'E', 'W', 'j', 'w', 'b', '@', 'V', 'j' },
                NullableCharMember = (char?)'g',
                NullableCharArrayMember = new char?[] { null, '%', 'L', null, 'H', null, null, '!', null, 'S' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, false, true, false, false, false, false, false, false, true },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { false, true, false, null, true, null, true, null, null, null },
                ByteMember = (byte)192,
                ByteArrayMember = new byte[] { 146, 102, 104, 9, 4, 111, 110, 235, 20, 48 },
                NullableByteMember = (byte?)149,
                NullableByteArrayMember = new byte?[] { null, null, null, 196, 141, 14, null, 212, null, null },
                SByteMember = (sbyte)38,
                SByteArrayMember = new sbyte[] { -47, 82, 65, 6, -26, -88, -118, 107, -117, 47 },
                NullableSByteMember = (sbyte?)-93,
                NullableSByteArrayMember = new sbyte?[] { null, -48, null, null, 111, 63, -21, -62, -81, null },
                Int16Member = (short)28712,
                Int16ArrayMember = new short[] { -15417, 17520, 20950, 24944, -17521, -13104, 24121, 13908, 24717, 6181 },
                NullableInt16Member = (short?)-26435,
                NullableInt16ArrayMember = new short?[] { 29516, -30589, null, null, -27515, 28479, 17641, null, 16703, 21533 },
                UInt16Member = (ushort)18201,
                UInt16ArrayMember = new ushort[] { 30466, 11055, 60166, 60208, 52652, 33654, 57829, 23479, 16881, 20925 },
                NullableUInt16Member = (ushort?)37597,
                NullableUInt16ArrayMember = new ushort?[] { 56499, 5456, 64183, 55071, 65273, null, 29497, 19241, 26771, 27360 },
                Int32Member = (int)-924609383,
                Int32ArrayMember = new int[] { 1868624095, 820511672, 1134974826, 1853125689, 1206491865, 1226036187, -697248409, 304225652, -618044094, -571677704 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { null, 1810218909, -28514192, null, null, null, null, -1830615018, -1825223237, null },
                UInt32Member = (uint)3628521095,
                UInt32ArrayMember = new uint[] { 3796186192, 1203446332, 1719650083, 3593496570, 3091227514, 2500816227, 2344988768, 1778375941, 1176441824, 1403237215 },
                NullableUInt32Member = (uint?)3770273657,
                NullableUInt32ArrayMember = new uint?[] { null, 3059662009, null, null, 2955273076, 1093688460, null, null, null, null },
                Int64Member = (long)2270876525494226541,
                Int64ArrayMember = new long[] { 236392740232091040, 3050732902665163506, 5622206614137937462, -8248139115504208137, 5239028858382782279, 899015925573422502, -8502855220762529544, 4167656463753655503, 8227265579351828362, 294540323362849641 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { null, 7522155408056310139, -3453979206586680866, null, -1389369674436453737, 6871272738798017892, -5722979235161009388, -5089874318308090648, 268003508384852984, -4971514549528610051 },
                UInt64Member = (ulong)15066872443843002104,
                UInt64ArrayMember = new ulong[] { 8641327608754542015, 4775785538675415260, 6237779626515879336, 15417912037166050524, 17215091426275223562, 17928587555496535864, 12991951039393112056, 7937457626937180548, 6832505982755626272, 13326493448932538092 },
                NullableUInt64Member = (ulong?)11266935502842917255,
                NullableUInt64ArrayMember = new ulong?[] { 7926253184249180370, 16567983309428310535, 2424227771074342525, 2067185786263653623, 16860845187192079971, null, 12600586044730140811, 2380331778102911109, 11495910758533152221, 10744215860307792129 },
                SingleMember = (float)0.003774364F,
                SingleArrayMember = new float[] { 0.1693018F, 0.9040813F, 0.2167958F, 0.7039442F, 0.7989569F, 0.4450477F, 0.2507914F, 0.2096516F, 0.1262912F, 0.1683744F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { null, 0.701458F, null, null, null, 0.8314816F, 0.1414486F, 0.9654523F, 0.9236432F, 0.6548637F },
                DoubleMember = (double)0.82471521,
                DoubleArrayMember = new double[] { 0.67575274, 0.55218857, 0.21121063, 0.74720576, 0.068859, 0.98888563, 0.84512563, 0.66935947, 0.67467944, 0.2084963 },
                NullableDoubleMember = (double?)0.76940932,
                NullableDoubleArrayMember = new double?[] { null, 0.0290345, 0.4720288, 0.48768413, 0.6984751, 0.54138616, null, 0.46142681, 0.44065535, 0.38681217 },
                DecimalMember = (decimal)0.723289096133452m,
                DecimalArrayMember = new decimal[] { 0.882182287463072m, 0.299654854135427m, 0.97614394220344m, 0.326448584127449m, 0.383807202979832m, 0.0369057613596813m, 0.114644071140626m, 0.363682289777176m, 0.879849428255041m, 0.35383735799875m },
                NullableDecimalMember = (decimal?)0.72231266448382m,
                NullableDecimalArrayMember = new decimal?[] { 0.928645161878618m, 0.262602497480159m, 0.286915869585665m, 0.772379595214678m, null, null, 0.0573396710945944m, 0.168995259408371m, 0.850205183425082m, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-87618918),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-342470654), DateTime.Now.AddSeconds(-239580467), DateTime.Now.AddSeconds(-337278976), DateTime.Now.AddSeconds(33896653), DateTime.Now.AddSeconds(-314473266), DateTime.Now.AddSeconds(23921659), DateTime.Now.AddSeconds(-230651633), DateTime.Now.AddSeconds(84894716), DateTime.Now.AddSeconds(-318635689), DateTime.Now.AddSeconds(202101034) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-198669460),
                NullableDateTimeArrayMember = new DateTime?[] { null, DateTime.Now.AddSeconds(128077769), DateTime.Now.AddSeconds(-2475011), DateTime.Now.AddSeconds(151323742), DateTime.Now.AddSeconds(132895575), null, DateTime.Now.AddSeconds(327649407), null, null, DateTime.Now.AddSeconds(-125567416) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"lWujaXQB:l",
                StringArrayMember = new string[] { "G4@LRpFJV6", "DfcnETXa@h", "xteAY7XOP!", "7eIEbm$Cv:", "MSipcF\"GeH", " ?Gh,jZ@.h", "S:\"b%vXYMp", "WU2DUfL?vS", ":Q6I!!YxQ'", "MCPOwVYHq4" },
                CharMember = (char)'k',
                CharArrayMember = new char[] { 'E', ' ', ',', 'h', '#', '8', '#', 'j', 'D', 'N' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { ':', null, null, 'W', '3', null, null, null, null, 't' },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, false, false, false, true, true, false, true, false, true },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { false, null, null, false, false, true, false, false, true, null },
                ByteMember = (byte)23,
                ByteArrayMember = new byte[] { 196, 211, 228, 55, 133, 45, 224, 26, 25, 119 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { null, null, 149, null, null, null, 148, 191, 245, 167 },
                SByteMember = (sbyte)-128,
                SByteArrayMember = new sbyte[] { 50, -121, 54, 53, -54, -97, -78, 52, 75, -117 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { null, -43, null, -47, 96, -113, -102, null, 102, -26 },
                Int16Member = (short)12127,
                Int16ArrayMember = new short[] { 19377, -22174, 15631, 11018, -2279, -14749, -9747, -32046, 18228, 164 },
                NullableInt16Member = (short?)23750,
                NullableInt16ArrayMember = new short?[] { null, -5906, null, 12079, null, -10413, null, 30194, null, 22036 },
                UInt16Member = (ushort)25403,
                UInt16ArrayMember = new ushort[] { 34503, 58660, 30843, 10537, 54751, 5593, 37755, 2501, 24911, 63987 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 18765, 25960, null, 13712, null, null, null, 45977, 34430, 23015 },
                Int32Member = (int)1972980436,
                Int32ArrayMember = new int[] { -1458852363, -1988787996, 1721264513, 1713623436, -31557466, 1258937314, 1745238923, -1919425492, 1347842774, -1962348128 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { -1409713529, 1512837499, 445881132, 2041061056, 198286429, null, null, 1890961106, null, null },
                UInt32Member = (uint)2011842790,
                UInt32ArrayMember = new uint[] { 2028847046, 3025575401, 3473708324, 3571066791, 540843602, 4128715183, 4035524623, 1899279819, 1693444049, 1166677416 },
                NullableUInt32Member = (uint?)1358323195,
                NullableUInt32ArrayMember = new uint?[] { null, null, 1194476013, 2828322761, 1695854952, 4254752878, 1009546807, 3568587255, 1805495973, 1710064522 },
                Int64Member = (long)-8866502868609739697,
                Int64ArrayMember = new long[] { 7863776804946776015, 6207876431313330866, 4251795837213292977, -7972827984898651305, 4491206933364463586, -9150531944993506280, 7330309076195422637, -2360843961834958066, -5085387681528585720, -8030330875292067867 },
                NullableInt64Member = (long?)2046126222487837265,
                NullableInt64ArrayMember = new long?[] { -1369234674047232763, 9131545892290064202, -7258919667764810464, 957231464830893629, 4834808323310444148, 4926788073192138856, null, 7909097983698882687, 2325173030836993332, -947441741842571453 },
                UInt64Member = (ulong)16749070515529258153,
                UInt64ArrayMember = new ulong[] { 11553096766198825110, 12336901172026150079, 5006090752579455903, 1727272341156237030, 2967691335984112516, 3338723572440834620, 6981515966234323934, 4509897487122610246, 4836664947478864147, 6835936086009557915 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 17689910976648093206, 5138762052616256978, null, null, null, 13403403076662545301, null, 5923318694835094527, null, null },
                SingleMember = (float)0.07887451F,
                SingleArrayMember = new float[] { 0.4917531F, 0.4933773F, 0.7737737F, 0.9602181F, 0.2188257F, 0.7120694F, 0.3945939F, 0.7486635F, 0.09667227F, 0.4600001F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { null, 0.9045315F, 0.4140331F, 0.2041562F, 0.326478F, 0.2243297F, 0.3137536F, null, 0.8623258F, 0.938629F },
                DoubleMember = (double)0.22172672,
                DoubleArrayMember = new double[] { 0.49494971, 0.59985179, 0.52550741, 0.47646545, 0.79755447, 0.28213724, 0.16497789, 0.49114184, 0.89762054, 0.318238 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.51571145, null, 0.16525385, 0.92000026, null, null, 0.51423861, 0.12624011, null, 0.75161278 },
                DecimalMember = (decimal)0.105270952035333m,
                DecimalArrayMember = new decimal[] { 0.807472344863914m, 0.129274056353268m, 0.291359071289822m, 0.710485023777226m, 0.985183045261159m, 0.778925151461235m, 0.423493113100293m, 0.973673124319722m, 0.617338359643397m, 0.763603463658878m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.474006142222325m, 0.78224987154m, 0.476900564728724m, null, 0.48693918971668m, 0.59990906184535m, 0.234397638232632m, 0.359757650345451m, 0.918804271108845m, 0.214811943571461m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-169526074),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-55962033), DateTime.Now.AddSeconds(-109994047), DateTime.Now.AddSeconds(227222573), DateTime.Now.AddSeconds(198211136), DateTime.Now.AddSeconds(207696609), DateTime.Now.AddSeconds(320283321), DateTime.Now.AddSeconds(-294869499), DateTime.Now.AddSeconds(146986230), DateTime.Now.AddSeconds(306050082), DateTime.Now.AddSeconds(-129769678) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(111941929),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(198077405), DateTime.Now.AddSeconds(-290912854), DateTime.Now.AddSeconds(-198702236), DateTime.Now.AddSeconds(319878447), null, DateTime.Now.AddSeconds(-148328242), DateTime.Now.AddSeconds(-144919556), DateTime.Now.AddSeconds(-90984366), DateTime.Now.AddSeconds(-314928383), DateTime.Now.AddSeconds(93051112) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"5s!?81KCNf",
                StringArrayMember = new string[] { "XkWrpBH8gE", "WwITcUFod%", "HEV:jVW!WP", "6Vk7rz2uvv", "NmxmhJkxjo", "wtwOXTPfHU", "2NlQUALXz0", "oh'57rd6ga", "8vi5qNwWkX", "kWmb'ABgRg" },
                CharMember = (char)'Z',
                CharArrayMember = new char[] { '!', 'k', 'd', 'J', ',', 'n', 'd', ':', ':', 'W' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { null, null, null, 'm', '%', 't', ':', 'q', null, null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, true, false, true, false, true, false, false, true, true },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { false, false, false, true, null, null, null, null, true, null },
                ByteMember = (byte)55,
                ByteArrayMember = new byte[] { 178, 237, 56, 146, 144, 90, 87, 169, 23, 46 },
                NullableByteMember = (byte?)64,
                NullableByteArrayMember = new byte?[] { null, 122, null, 106, null, 39, null, 180, null, null },
                SByteMember = (sbyte)79,
                SByteArrayMember = new sbyte[] { -61, -10, 120, 50, 107, -41, 39, 21, -20, -37 },
                NullableSByteMember = (sbyte?)-98,
                NullableSByteArrayMember = new sbyte?[] { 41, -25, -125, 76, 111, -51, null, -2, -44, -29 },
                Int16Member = (short)-31175,
                Int16ArrayMember = new short[] { 15485, 27761, -3138, -20522, 6751, -4374, -14100, 21793, -11380, -14102 },
                NullableInt16Member = (short?)-26213,
                NullableInt16ArrayMember = new short?[] { -21407, 19922, 28927, 25584, 4470, 22587, 14795, null, 10729, -14590 },
                UInt16Member = (ushort)47774,
                UInt16ArrayMember = new ushort[] { 53012, 52922, 29023, 30590, 46658, 14768, 31962, 4883, 53473, 41039 },
                NullableUInt16Member = (ushort?)58520,
                NullableUInt16ArrayMember = new ushort?[] { 22060, null, 55916, null, 36783, 43928, 8185, 15089, 24695, 27398 },
                Int32Member = (int)1368282197,
                Int32ArrayMember = new int[] { 1451953375, 1614514687, -1405362629, 308269791, -1461047718, 1318126, -358259393, -969014119, -1690341867, 324974318 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { 641678420, 998248930, null, -121412634, 1357093143, -1770676170, -1541094509, null, -658029807, -641499623 },
                UInt32Member = (uint)3160088327,
                UInt32ArrayMember = new uint[] { 819572145, 2532184425, 3856294837, 871699575, 3222501141, 347754631, 3948896936, 2683647347, 2407250652, 2733164091 },
                NullableUInt32Member = (uint?)3259691146,
                NullableUInt32ArrayMember = new uint?[] { 3113055627, 882510991, 109626610, 1837048323, 1721351433, 3664809806, 575762528, 3854130666, 461718163, null },
                Int64Member = (long)-9091169272967200130,
                Int64ArrayMember = new long[] { 4812969549510199181, 7029752090891325115, 2201717812001601366, -3423085817851755670, 2925095593959284358, -3770975927065782829, 8850283188267021940, -383918856450657223, 5724261705832649360, -7151756469933909794 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { null, -6087447221790278625, null, 5155533843490951693, null, 1871536819795133898, null, -5958124259256733422, null, -5713793795142386132 },
                UInt64Member = (ulong)6125572969659736878,
                UInt64ArrayMember = new ulong[] { 6017021036893689941, 17905814575391531659, 8220044125361729277, 12417430221284383641, 5963357170348371313, 17573082140769166793, 16051232795573965860, 17894084336421742277, 16444946638990707726, 8672034197261116971 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { null, null, 1912207864990865481, null, 1213030445240826477, null, 9491003776850486556, 1150299902773586899, null, null },
                SingleMember = (float)0.9689646F,
                SingleArrayMember = new float[] { 0.780301F, 0.9167196F, 0.7901841F, 0.1719169F, 0.7178985F, 0.6329138F, 0.3946007F, 0.6559284F, 0.3693915F, 0.6117077F },
                NullableSingleMember = (float?)0.7637035F,
                NullableSingleArrayMember = new float?[] { null, 0.7446539F, 0.7726446F, 0.9500444F, null, null, 0.586997F, 0.3719252F, 0.6650392F, null },
                DoubleMember = (double)0.00801656,
                DoubleArrayMember = new double[] { 0.60743219, 0.28112623, 0.40447028, 0.93381556, 0.28786728, 0.98676958, 0.33894002, 0.16955753, 0.08881253, 0.57529592 },
                NullableDoubleMember = (double?)0.93913064,
                NullableDoubleArrayMember = new double?[] { 0.78830767, null, 0.18969472, null, 0.10878524, 0.29617297, null, null, 0.87785424, null },
                DecimalMember = (decimal)0.00267799012487661m,
                DecimalArrayMember = new decimal[] { 0.264438887715544m, 0.0269294227598838m, 0.774578476219708m, 0.546365013134836m, 0.821685848674591m, 0.430953593193997m, 0.521182087958409m, 0.943462116617459m, 0.985795579378398m, 0.839109384379866m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.0820325096519815m, 0.650092865643135m, 0.0575390015996708m, 0.844977827204847m, 0.396054781692128m, 0.849813677300612m, 0.207563283018564m, 0.0569614330571896m, null, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-129877535),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(244915783), DateTime.Now.AddSeconds(-95579210), DateTime.Now.AddSeconds(87326343), DateTime.Now.AddSeconds(-37904919), DateTime.Now.AddSeconds(297705920), DateTime.Now.AddSeconds(-46880597), DateTime.Now.AddSeconds(159697847), DateTime.Now.AddSeconds(-210038418), DateTime.Now.AddSeconds(72947267), DateTime.Now.AddSeconds(79695353) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { null, DateTime.Now.AddSeconds(-26197206), DateTime.Now.AddSeconds(250436306), DateTime.Now.AddSeconds(268562261), DateTime.Now.AddSeconds(-208822142), DateTime.Now.AddSeconds(-95445863), DateTime.Now.AddSeconds(110533141), DateTime.Now.AddSeconds(279365917), null, DateTime.Now.AddSeconds(61267686) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"%N%5 O0:d2",
                StringArrayMember = new string[] { ";f 4iG13xG", "b,.cQS@o?7", "q#oAcvx06b", "j5\"4hEX5'9", "VU6KEJato3", "TMBtM\":%c.", "gB#Vut!F!M", "@MvDlVMd@S", "VELCIX3Xro", "NYcFdUXlni" },
                CharMember = (char)'P',
                CharArrayMember = new char[] { 'w', '$', 't', 'j', 'N', '.', 't', ';', 'j', 'F' },
                NullableCharMember = (char?)'Y',
                NullableCharArrayMember = new char?[] { ':', 'r', 'o', 'f', 'b', 'h', null, 'A', 'D', null },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, false, true, true, false, false, true, false, false },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { null, false, true, true, null, false, true, null, null, true },
                ByteMember = (byte)21,
                ByteArrayMember = new byte[] { 17, 4, 252, 238, 9, 220, 244, 46, 44, 224 },
                NullableByteMember = (byte?)200,
                NullableByteArrayMember = new byte?[] { 3, 125, null, null, 117, 138, 35, null, null, null },
                SByteMember = (sbyte)125,
                SByteArrayMember = new sbyte[] { 74, 100, 8, 20, 14, 93, 79, 124, 117, -102 },
                NullableSByteMember = (sbyte?)-21,
                NullableSByteArrayMember = new sbyte?[] { 25, null, null, 121, 98, 25, null, -51, -108, 88 },
                Int16Member = (short)27456,
                Int16ArrayMember = new short[] { 16497, -23839, 2928, -20207, 25092, 6835, -13447, 26107, -8592, 14971 },
                NullableInt16Member = (short?)-10641,
                NullableInt16ArrayMember = new short?[] { 4960, null, 32280, null, 1215, null, null, 1294, -30329, null },
                UInt16Member = (ushort)15731,
                UInt16ArrayMember = new ushort[] { 2327, 18941, 60283, 41550, 41993, 2715, 62055, 35569, 10059, 26490 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 59696, 862, 32204, null, 40441, 45492, 44249, 61655, 59163, 34286 },
                Int32Member = (int)1545062681,
                Int32ArrayMember = new int[] { -1552217297, 801480291, -1692539835, 1316327369, 1435917133, -81800046, 452839456, 1840019232, 1701255560, 96477207 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { 1017798604, null, null, 851348341, null, -2105480358, -837834079, null, 271101715, null },
                UInt32Member = (uint)4058070646,
                UInt32ArrayMember = new uint[] { 3698354146, 3687742599, 114983, 4281843612, 795177858, 2590614983, 952542984, 277006718, 2516090797, 2781451247 },
                NullableUInt32Member = (uint?)3841471758,
                NullableUInt32ArrayMember = new uint?[] { 458064631, 699586563, 3772045054, 1063148949, 2714180946, null, 3089547177, 2963453297, null, 931049314 },
                Int64Member = (long)1837854410474174890,
                Int64ArrayMember = new long[] { 5138161954096078984, -2390785114147283899, 5516905496812839617, -29066403165288364, -4518362377259486402, 8076551242118229423, -8374244006022655002, -8645597381936347824, 3783976156786193924, -6288332081774974057 },
                NullableInt64Member = (long?)-6086177698081751554,
                NullableInt64ArrayMember = new long?[] { 3055698693000353311, null, null, null, null, 4747805855898914948, 7160466040153504329, 7356148595333692224, 4166748598486900603, 876276441695076809 },
                UInt64Member = (ulong)11425482781558154576,
                UInt64ArrayMember = new ulong[] { 10221143833514442449, 14773723339741661156, 9064804479138836985, 14988347889033096587, 3477113709424620311, 9352269829094644968, 3096247869466497183, 10776981971114873009, 14433193389677830661, 11214489585911349103 },
                NullableUInt64Member = (ulong?)4179842541005221706,
                NullableUInt64ArrayMember = new ulong?[] { null, 16732153959854512431, 14550096866371363499, 15687892909461556847, 2210193934030364772, 17233012600995843881, 14662874561052938419, 15036542216868561025, 14025868344280115678, 150180677753991764 },
                SingleMember = (float)0.6015295F,
                SingleArrayMember = new float[] { 0.3758931F, 0.8497436F, 0.3593796F, 0.8100346F, 0.1277077F, 0.6796991F, 0.6671854F, 0.8751591F, 0.8252969F, 0.186633F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { 0.0869937F, 0.6835326F, 0.9432458F, 0.6589675F, null, 0.07028796F, null, 0.3872098F, null, 0.3297227F },
                DoubleMember = (double)0.9807111,
                DoubleArrayMember = new double[] { 0.64988589, 0.3992673, 0.97529854, 0.78675206, 0.53052446, 0.15696133, 0.88370965, 0.61649431, 0.91376771, 0.57496482 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { null, 0.58383861, 0.5006669, 0.70097051, 0.92841181, 0.15304424, 0.16352511, 0.64282754, 0.31125922, null },
                DecimalMember = (decimal)0.789252854785534m,
                DecimalArrayMember = new decimal[] { 0.504688841991447m, 0.824154025327486m, 0.402526416071936m, 0.760925473534002m, 0.210166787360873m, 0.854340112700286m, 0.699427666002618m, 0.70607854272522m, 0.960564022865409m, 0.900998064736369m },
                NullableDecimalMember = (decimal?)0.694710872925218m,
                NullableDecimalArrayMember = new decimal?[] { 0.647829805336813m, null, 0.0624126070469676m, 0.541833829387014m, 0.93213329507603m, null, 0.522717248891814m, null, null, 0.652329935530354m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(288679161),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(111914613), DateTime.Now.AddSeconds(-314642798), DateTime.Now.AddSeconds(127179210), DateTime.Now.AddSeconds(-65023665), DateTime.Now.AddSeconds(217026145), DateTime.Now.AddSeconds(32293479), DateTime.Now.AddSeconds(-239248939), DateTime.Now.AddSeconds(157001814), DateTime.Now.AddSeconds(-238618774), DateTime.Now.AddSeconds(5874497) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-103068820), DateTime.Now.AddSeconds(-207970083), null, DateTime.Now.AddSeconds(231277649), DateTime.Now.AddSeconds(8579327), null, DateTime.Now.AddSeconds(-233516994), null, null, null },
            },
        };
    }
}
