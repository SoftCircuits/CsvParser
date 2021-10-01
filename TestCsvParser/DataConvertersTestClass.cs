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
        public string StringMember { get; set; }
        public string[] StringArrayMember { get; set; }
        public char CharMember { get; set; }
        public char[] CharArrayMember { get; set; }
        public Nullable<char> NullableCharMember { get; set; }
        public Nullable<char>[] NullableCharArrayMember { get; set; }
        public bool BooleanMember { get; set; }
        public bool[] BooleanArrayMember { get; set; }
        public Nullable<bool> NullableBooleanMember { get; set; }
        public Nullable<bool>[] NullableBooleanArrayMember { get; set; }
        public byte ByteMember { get; set; }
        public byte[] ByteArrayMember { get; set; }
        public Nullable<byte> NullableByteMember { get; set; }
        public Nullable<byte>[] NullableByteArrayMember { get; set; }
        public sbyte SByteMember { get; set; }
        public sbyte[] SByteArrayMember { get; set; }
        public Nullable<sbyte> NullableSByteMember { get; set; }
        public Nullable<sbyte>[] NullableSByteArrayMember { get; set; }
        public short Int16Member { get; set; }
        public short[] Int16ArrayMember { get; set; }
        public Nullable<short> NullableInt16Member { get; set; }
        public Nullable<short>[] NullableInt16ArrayMember { get; set; }
        public ushort UInt16Member { get; set; }
        public ushort[] UInt16ArrayMember { get; set; }
        public Nullable<ushort> NullableUInt16Member { get; set; }
        public Nullable<ushort>[] NullableUInt16ArrayMember { get; set; }
        public int Int32Member { get; set; }
        public int[] Int32ArrayMember { get; set; }
        public Nullable<int> NullableInt32Member { get; set; }
        public Nullable<int>[] NullableInt32ArrayMember { get; set; }
        public uint UInt32Member { get; set; }
        public uint[] UInt32ArrayMember { get; set; }
        public Nullable<uint> NullableUInt32Member { get; set; }
        public Nullable<uint>[] NullableUInt32ArrayMember { get; set; }
        public long Int64Member { get; set; }
        public long[] Int64ArrayMember { get; set; }
        public Nullable<long> NullableInt64Member { get; set; }
        public Nullable<long>[] NullableInt64ArrayMember { get; set; }
        public ulong UInt64Member { get; set; }
        public ulong[] UInt64ArrayMember { get; set; }
        public Nullable<ulong> NullableUInt64Member { get; set; }
        public Nullable<ulong>[] NullableUInt64ArrayMember { get; set; }
        public float SingleMember { get; set; }
        public float[] SingleArrayMember { get; set; }
        public Nullable<float> NullableSingleMember { get; set; }
        public Nullable<float>[] NullableSingleArrayMember { get; set; }
        public double DoubleMember { get; set; }
        public double[] DoubleArrayMember { get; set; }
        public Nullable<double> NullableDoubleMember { get; set; }
        public Nullable<double>[] NullableDoubleArrayMember { get; set; }
        public decimal DecimalMember { get; set; }
        public decimal[] DecimalArrayMember { get; set; }
        public Nullable<decimal> NullableDecimalMember { get; set; }
        public Nullable<decimal>[] NullableDecimalArrayMember { get; set; }
        public Guid GuidMember { get; set; }
        public Guid[] GuidArrayMember { get; set; }
        public Nullable<Guid> NullableGuidMember { get; set; }
        public Nullable<Guid>[] NullableGuidArrayMember { get; set; }
        public DateTime DateTimeMember { get; set; }
        public DateTime[] DateTimeArrayMember { get; set; }
        public Nullable<DateTime> NullableDateTimeMember { get; set; }
        public Nullable<DateTime>[] NullableDateTimeArrayMember { get; set; }

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
                StringMember = "00! rp#Tpj",
                StringArrayMember = new string[] { "WatfjQsIZD", "pRDiItReFI", "jvDmQuUdi7", "S6rbD,\"eit", "s;vdKjwj;%", "XpOg xWHP,", "#Owo75xyeU", "z.@insAxb4", "$7mkj0sLbc", "o8OuxdRGtc" },
                CharMember = 'l',
                CharArrayMember = new char[] { '#', 'e', 'N', 'u', 'Q', 'G', 'v', 'I', 'R', 'Y' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { '3', null, 'G', 'v', null, null, 'B', 'r', 'P', 'W' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, true, false, true, false, true, false, true, false, false },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, true, null, false, true, null, false, false, false, null },
                ByteMember = 238,
                ByteArrayMember = new byte[] { 203, 27, 211, 131, 201, 77, 103, 127, 178, 132 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, 30, 106, null, 38, 26, 76, 213, 33, null },
                SByteMember = -5,
                SByteArrayMember = new sbyte[] { -115, 20, -64, 5, -92, 0, -64, -99, -70, 15 },
                NullableSByteMember = 19,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 94, -118, -44, null, -19, null, -13, -83, 37, 42 },
                Int16Member = 3971,
                Int16ArrayMember = new short[] { 28099, -31537, 3728, 7440, -25393, 14871, 26697, -3641, 12796, 9225 },
                NullableInt16Member = -31237,
                NullableInt16ArrayMember = new Nullable<short>[] { 19700, 7900, -9411, -23695, null, 22920, null, 22855, -20488, null },
                UInt16Member = 34161,
                UInt16ArrayMember = new ushort[] { 29661, 58179, 62973, 24, 9723, 23131, 36549, 18331, 57708, 16786 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, null, 61550, null, null, 21932, 51639, 18341, 37111, 57634 },
                Int32Member = 1184925973,
                Int32ArrayMember = new int[] { 1087471385, 1216431514, -175332689, -1703138277, -846827440, -825219467, 423280988, 11096159, -58408693, 594934703 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { 989921774, 1626301599, -204352766, 198829058, -1929879576, -343889830, -972004614, null, -1701306164, -50008208 },
                UInt32Member = 2332816062,
                UInt32ArrayMember = new uint[] { 1916928730, 2711488759, 2284971652, 2299451388, 2472586505, 401429162, 476421470, 2122006544, 3103817412, 2407133348 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 3440825975, 279856543, 344514679, 2174966035, 3283249988, 434394583, 3506224337, 2714781505, null },
                Int64Member = 3764660938241050664,
                Int64ArrayMember = new long[] { -245837590960303982, 5107478069486698551, -1450890069431363910, 3395938583199157051, 1964262811610852259, -4610067499298874854, 3028946028216278950, 7770526832624628659, 4946965407978859039, 8854925344930024083 },
                NullableInt64Member = 2497477754707475681,
                NullableInt64ArrayMember = new Nullable<long>[] { null, null, 6825852592717843002, -3381424429052400654, null, null, null, -2930356753405447487, -9067355694182863888, 1151783593614526819 },
                UInt64Member = 8663862720618986569,
                UInt64ArrayMember = new ulong[] { 361217110895569490, 8841333164238110225, 10875534193772988209, 17591504163697206035, 18270869552200484645, 10379873045439061426, 12303502972193916106, 9020637841941209661, 390895417429939676, 6632877130675353207 },
                NullableUInt64Member = 5211561884440951402,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 10299438415935040439, 13731008028326000948, null, null, 2180842637464339982, null, null, 14686296113603959786, 7866010517773159598, null },
                SingleMember = 0.9471F,
                SingleArrayMember = new float[] { 0.6587F, 0.7925F, 0.0293F, 0.6349F, 0.4197F, 0.2916F, 0.8713F, 0.5662F, 0.2035F, 0.9771F },
                NullableSingleMember = 0.5879F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.7484F, null, 0.2618F, 0.4936F, null, 0.7375F, 0.562F, null, null },
                DoubleMember = 0.5029,
                DoubleArrayMember = new double[] { 0.9886, 0.0959, 0.3786, 0.4216, 0.1275, 0.8171, 0.963, 0.8279, 0.5865, 0.5276 },
                NullableDoubleMember = 0.8812,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.7649, null, 0.1755, 0.3154, 0.2157, 0.5339, 0.988, 0.5473, null, 0.3147 },
                DecimalMember = 0.388553951582198m,
                DecimalArrayMember = new decimal[] { 0.331229894110574m, 0.395943648366231m, 0.840671901982591m, 0.158252101465246m, 0.194767868236996m, 0.452427433548694m, 0.97526537299867m, 0.38581959083016m, 0.25741429452664m, 0.922684093435614m },
                NullableDecimalMember = 0.2894299930378m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.3823765839368m, null, 0.557078317067157m, null, 0.765731307103174m, 0.551735531329986m, 0.536879442416541m, 0.5623543064866m, 0.929086802028626m, 0.0674552717560228m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-292850190),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-313028439), DateTime.Now.AddSeconds(-39355436), DateTime.Now.AddSeconds(82874815), DateTime.Now.AddSeconds(285774782), DateTime.Now.AddSeconds(-120922756), DateTime.Now.AddSeconds(-322575881), DateTime.Now.AddSeconds(83725802), DateTime.Now.AddSeconds(325761672), DateTime.Now.AddSeconds(299875966), DateTime.Now.AddSeconds(69146414) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-282224109),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-293228685), null, DateTime.Now.AddSeconds(225755968), null, DateTime.Now.AddSeconds(-31664971), null, DateTime.Now.AddSeconds(-208601090), DateTime.Now.AddSeconds(-316498412), null, DateTime.Now.AddSeconds(330509323) },
            },
            new DataConvertersTestClass {
                StringMember = "9%FtVkt2'e",
                StringArrayMember = new string[] { "xw3h;LzMFG", "NlXB2;vY8K", "b\"D @ 2'O ", "H?S$2:F34l", "0FBmOXyE:f", ",U!5C3s3m;", "g7$F XTZV.", "8:rWgW'o7u", "YxB6K9@k!%", "WZR;F\"H@m:" },
                CharMember = 'f',
                CharArrayMember = new char[] { '8', ';', 'P', 'd', 'd', '"', '"', 'Q', 'F', '#' },
                NullableCharMember = '4',
                NullableCharArrayMember = new Nullable<char>[] { '5', 'n', 'B', 'L', null, ';', null, 't', 'j', null },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, true, false, true, false, false, true, true, true, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, true, null, true, false, true, false, true, null, false },
                ByteMember = 181,
                ByteArrayMember = new byte[] { 147, 85, 229, 201, 198, 201, 181, 247, 127, 163 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 49, 116, 134, 73, 48, null, null, 228, 207, 20 },
                SByteMember = 117,
                SByteArrayMember = new sbyte[] { -23, 55, -20, -39, 15, -1, -41, 33, -101, 111 },
                NullableSByteMember = -81,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 26, null, 84, 104, 19, null, -115, null, 76, null },
                Int16Member = -5119,
                Int16ArrayMember = new short[] { 20303, 16117, 8542, -5944, -28921, 14404, -204, 24176, 11068, -14888 },
                NullableInt16Member = 29477,
                NullableInt16ArrayMember = new Nullable<short>[] { -31836, null, -12286, -11142, 3791, -20579, null, -15369, 11541, -5816 },
                UInt16Member = 62434,
                UInt16ArrayMember = new ushort[] { 30878, 17735, 57493, 19193, 4655, 4060, 29722, 6925, 38250, 53071 },
                NullableUInt16Member = 64419,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 246, null, 58370, 33310, 30672, 54888, 61276, 16311, null, 60151 },
                Int32Member = 1284566839,
                Int32ArrayMember = new int[] { 412882604, 160417108, -137473675, -1442118428, -1140145505, 1086535392, 1870181233, 1806864115, 1044263703, 29117022 },
                NullableInt32Member = -892381243,
                NullableInt32ArrayMember = new Nullable<int>[] { 1524919248, null, null, 1400204500, 525115559, null, -1310694498, 106187980, -203482605, null },
                UInt32Member = 3966407114,
                UInt32ArrayMember = new uint[] { 2759368013, 3961511034, 205331797, 3964621603, 3684000073, 3085220544, 2311907742, 3016098818, 3765880320, 1013625182 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1771298662, 867467508, null, 3113364366, 2500890629, 193549905, null, 4137193070, 72604614, null },
                Int64Member = -7324560897131674179,
                Int64ArrayMember = new long[] { -8762873796600899463, -1766718118182255883, 2656927016507910735, 8954627305906044924, 6780121014854542020, 7121203060063207141, 3138714095076235401, -4940990323423583879, 8846349273560646845, 7255526388373960850 },
                NullableInt64Member = 6039790289747428903,
                NullableInt64ArrayMember = new Nullable<long>[] { null, 6035544582272393794, 2264479467652479737, 5571667753161087872, -9073796649114961912, 7656253564891616048, -3216267401915283568, null, null, 1214486873721984981 },
                UInt64Member = 15290195892404104560,
                UInt64ArrayMember = new ulong[] { 1110718742310082003, 9362520550665030798, 13700823349981246039, 16155733767389865174, 18221302451445738707, 1576933297405347825, 13372602015992078399, 15909993737440789681, 4933960548031450839, 8233847608632215187 },
                NullableUInt64Member = 1715082553349800952,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 8717924314427950409, 2494405182196625579, 713934611812654540, null, 8334283923938629643, 4727008632593452063, 17942493044057716701, 7787610990938440984, 185058405971904782, 3107244214633931484 },
                SingleMember = 0.7121F,
                SingleArrayMember = new float[] { 0.1248F, 0.0327F, 0.8313F, 0.9989F, 0.1536F, 0.63F, 0.6254F, 0.1073F, 0.2679F, 0.657F },
                NullableSingleMember = 0.8228F,
                NullableSingleArrayMember = new Nullable<float>[] { null, null, null, null, 0.9573F, 0.6685F, null, 0.43F, 0.8105F, 0.5302F },
                DoubleMember = 0.2689,
                DoubleArrayMember = new double[] { 0.3362, 0.6438, 0.2336, 0.5966, 0.2583, 0.223, 0.4084, 0.8376, 0.4193, 0.6486 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.6299, null, 0.8789, 0.5103, 0.0266, 0.9585, null, 0.1164, null, 0.4336 },
                DecimalMember = 0.188105562789415m,
                DecimalArrayMember = new decimal[] { 0.818964992565552m, 0.440526894964523m, 0.476772958634781m, 0.859848536485736m, 0.356055644972276m, 0.0408929619197235m, 0.267437368290237m, 0.295338202405413m, 0.714417931025111m, 0.298620309819756m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.8370867184536m, null, 0.38326819538291m, 0.959251806586632m, 0.371968079065889m, null, null, 0.686758575815129m, 0.498577122808703m, 0.351824628818698m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-298058258),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-46416258), DateTime.Now.AddSeconds(-146953605), DateTime.Now.AddSeconds(-176104255), DateTime.Now.AddSeconds(275614683), DateTime.Now.AddSeconds(323987114), DateTime.Now.AddSeconds(-17703611), DateTime.Now.AddSeconds(-60476555), DateTime.Now.AddSeconds(11038921), DateTime.Now.AddSeconds(311831890), DateTime.Now.AddSeconds(26873996) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-156628674), DateTime.Now.AddSeconds(-149605295), DateTime.Now.AddSeconds(81277523), null, DateTime.Now.AddSeconds(-47206133), DateTime.Now.AddSeconds(-118282177), DateTime.Now.AddSeconds(128639475), null, DateTime.Now.AddSeconds(341283635), DateTime.Now.AddSeconds(46393551) },
            },
            new DataConvertersTestClass {
                StringMember = "DY5Dt#0uY3",
                StringArrayMember = new string[] { ",D#:o55O@M", "b6eXoix0Rn", "O#B'rA:%MN", "JYLOKScZZW", "K\"AZnU\"ilK", "W d7o:CDXo", ":7B4DDuQNW", "GYb!cV#ycW", "GfjB:K!awS", "QZB8fZ:TWk" },
                CharMember = '3',
                CharArrayMember = new char[] { 'B', 'T', 'Y', 'z', 'f', 'h', 'L', 'b', 'Z', 'z' },
                NullableCharMember = ';',
                NullableCharArrayMember = new Nullable<char>[] { null, 'R', '!', 'B', '%', '.', '\'', null, '3', '6' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, true, true, false, true, false, false, false, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, true, false, false, true, false, null, true, false, null },
                ByteMember = 173,
                ByteArrayMember = new byte[] { 193, 44, 7, 200, 30, 46, 191, 183, 23, 84 },
                NullableByteMember = 113,
                NullableByteArrayMember = new Nullable<byte>[] { 151, 172, 187, 125, 25, 75, null, 237, 228, 248 },
                SByteMember = 118,
                SByteArrayMember = new sbyte[] { -31, 26, 102, 15, 25, 23, 91, 67, 5, -45 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -36, -17, null, 87, null, -55, null, 34, null },
                Int16Member = -17628,
                Int16ArrayMember = new short[] { -1404, 1084, -9431, -26339, 31707, -6874, 18999, 25546, 12100, 5729 },
                NullableInt16Member = -30596,
                NullableInt16ArrayMember = new Nullable<short>[] { null, null, -15750, 23831, 5341, -26497, null, 10802, null, null },
                UInt16Member = 62068,
                UInt16ArrayMember = new ushort[] { 37517, 53522, 32251, 20438, 57236, 18448, 49739, 36286, 42212, 35477 },
                NullableUInt16Member = 33727,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 6962, null, 53856, null, 61837, null, 25711, 48122, 60481, 15526 },
                Int32Member = -1962023986,
                Int32ArrayMember = new int[] { -2042875751, -339163581, -1256479998, 812691592, -1524645274, 1862864408, -1772961573, -1109858977, -1233415106, -358752926 },
                NullableInt32Member = -357479968,
                NullableInt32ArrayMember = new Nullable<int>[] { -1016173941, 1936245771, null, null, 829719210, 1985858333, -1982940620, null, 1489254311, null },
                UInt32Member = 148425781,
                UInt32ArrayMember = new uint[] { 3076313165, 652450417, 752528285, 2283141759, 398417161, 1922770449, 1045481635, 3285163188, 2174520376, 3281703673 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 740093168, 461127392, 670547981, null, 2756356372, 4095138561, null, null, 4240651918, 1437546185 },
                Int64Member = 5421688065216247083,
                Int64ArrayMember = new long[] { -4478298833229577276, 6787610568923677573, 6060764559168401735, -1037555933263286112, -927569197060769127, -4884374426776252162, -7274751348411477437, -6313591875960833562, -6150965091655650967, 7565694613023526737 },
                NullableInt64Member = 3509922011982722946,
                NullableInt64ArrayMember = new Nullable<long>[] { 6300079922262950121, null, null, null, -7383191104269655579, 8058247197766384422, null, 4679693988613615188, 1629564438523335170, -4268412341414155713 },
                UInt64Member = 11536082677230268140,
                UInt64ArrayMember = new ulong[] { 16242172805212941712, 11710646658625210947, 12258407920666209882, 7132705184676496235, 1332624956098553486, 6346977796475568954, 17551021708354336362, 14793836108913807374, 16882654152618022542, 7925634290805379695 },
                NullableUInt64Member = 10794319997524884193,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 1038490894986463174, null, null, 5018188333728804311, 18010046293136282507, 5378288629575304693, 687644903370184228, 12506864677447539827, 11698222646680583052 },
                SingleMember = 0.8835F,
                SingleArrayMember = new float[] { 0.055F, 0.0439F, 0.0729F, 0.852F, 0.846F, 0.1592F, 0.4414F, 0.1708F, 0.1458F, 0.8572F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.2711F, null, 0.3262F, 0.945F, 0.003F, 0.077F, 0.6047F, null, 0.1332F, null },
                DoubleMember = 0.3049,
                DoubleArrayMember = new double[] { 0.2698, 0.5664, 0.0625, 0.2719, 0.2587, 0.8015, 0.2374, 0.5111, 0.1683, 0.1619 },
                NullableDoubleMember = 0.7672,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.8961, null, null, 0.904, 0.8868, 0.3872, 0.9144, 0.5484, null, 0.9464 },
                DecimalMember = 0.632413561750396m,
                DecimalArrayMember = new decimal[] { 0.32429197212881m, 0.0461785709700447m, 0.466779737019343m, 0.189959022770617m, 0.734578530646199m, 0.0156946191637286m, 0.848903098073277m, 0.499130116542396m, 0.522528047451064m, 0.230096311881252m },
                NullableDecimalMember = 0.28964094877692m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.164468597697312m, null, 0.581215853607848m, 0.927392672247902m, 0.17908611110369m, 0.969834995441993m, 0.695487192690134m, 0.437527799716931m, null, 0.0803541737051467m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(252273193),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(312098127), DateTime.Now.AddSeconds(-237685240), DateTime.Now.AddSeconds(103216479), DateTime.Now.AddSeconds(273819930), DateTime.Now.AddSeconds(-3707777), DateTime.Now.AddSeconds(-133899982), DateTime.Now.AddSeconds(336086694), DateTime.Now.AddSeconds(-207838792), DateTime.Now.AddSeconds(341274418), DateTime.Now.AddSeconds(74474925) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(53820462),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(220249218), DateTime.Now.AddSeconds(127113778), DateTime.Now.AddSeconds(339862350), DateTime.Now.AddSeconds(153925943), null, DateTime.Now.AddSeconds(318350657), null, DateTime.Now.AddSeconds(-278134081), DateTime.Now.AddSeconds(45256270), null },
            },
            new DataConvertersTestClass {
                StringMember = "xzVPE$vjfS",
                StringArrayMember = new string[] { "@ayBhiYA?\"", "ITr!G4XqlC", "ezc'weKU#!", "mi@dW;x b$", "B;edbdIaJX", "xE$c.S%6cC", "XDD!dxXNvP", "GXPnBoV%oS", "W6LvUhkqqD", "':uYw%9j 0" },
                CharMember = '%',
                CharArrayMember = new char[] { 'g', 't', 'Z', '@', '@', 'g', 'U', 'Z', 'i', 'b' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'W', 'x', null, 'R', null, 'y', null, null, '9', null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, false, true, false, true, true, false, false, true, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, true, true, null, true, null, true, false, false },
                ByteMember = 229,
                ByteArrayMember = new byte[] { 227, 209, 225, 77, 61, 103, 31, 70, 128, 205 },
                NullableByteMember = 243,
                NullableByteArrayMember = new Nullable<byte>[] { 190, null, 48, 235, 247, null, null, null, 186, 14 },
                SByteMember = 97,
                SByteArrayMember = new sbyte[] { 30, -34, 112, 47, 49, 84, 42, 56, -44, -106 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -126, null, -49, -34, null, -85, 29, 85, 97, -104 },
                Int16Member = -19244,
                Int16ArrayMember = new short[] { -29679, 19374, 10442, -30952, 7437, 28936, 4945, 25031, 9051, 7620 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -30174, null, -30120, null, null, -19289, -10785, -21385, null, null },
                UInt16Member = 33268,
                UInt16ArrayMember = new ushort[] { 18440, 21586, 61674, 4046, 40608, 14560, 61091, 54978, 47633, 89 },
                NullableUInt16Member = 32542,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, null, null, 38526, null, null, null, null, null, null },
                Int32Member = 686241159,
                Int32ArrayMember = new int[] { -104016484, -1880088362, 1964651156, -433367861, 899145357, -427363148, -969162609, 1294158286, 1223625100, -744528642 },
                NullableInt32Member = 173488682,
                NullableInt32ArrayMember = new Nullable<int>[] { 1922622172, null, null, 1336374450, 702879338, null, -1024930546, 1042868195, 783977201, 777643216 },
                UInt32Member = 7307638,
                UInt32ArrayMember = new uint[] { 3069673783, 1282774391, 2029043215, 284087636, 2186557446, 350547626, 4046299664, 681415813, 2151326939, 1551654884 },
                NullableUInt32Member = 3547889222,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2655515848, 1441541182, 3567706780, null, 3340794823, null, 2787929755, null, null, null },
                Int64Member = 3748376541142030310,
                Int64ArrayMember = new long[] { 7123670189547989722, -8905889276863015280, 5464600914636535183, -1556207997192692044, -2084132467154885434, -368899418262684979, -5647584610080806608, 6003441953527873875, -2092994540400692433, 5574565248288377953 },
                NullableInt64Member = 4901659399974037947,
                NullableInt64ArrayMember = new Nullable<long>[] { null, -3366605329354262322, 7837188818299904977, null, null, 4748863197675938941, 8928990227594841985, 5645678985326587436, null, null },
                UInt64Member = 11486734745010096294,
                UInt64ArrayMember = new ulong[] { 11826374749801508480, 14919471208995782140, 10111345035427861078, 580510883954541340, 15205725742616302011, 765269516198489948, 14547642732581660348, 10957640924648834332, 13821595582077457519, 6051231807111774796 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 16027500785207143174, 10941808657483734010, null, 3006932385923077867, 2690069841406775685, null, null, 14749329278614390197, null, 1546487544445383530 },
                SingleMember = 0.6027F,
                SingleArrayMember = new float[] { 0.9588F, 0.1884F, 0.2574F, 0.0999F, 0.7419F, 0.0523F, 0.224F, 0.584F, 0.3479F, 0.9768F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.4061F, null, 0.523F, 0.3974F, 0.4469F, 0.8106F, 0.9221F, null, 0.978F, 0.4682F },
                DoubleMember = 0.4802,
                DoubleArrayMember = new double[] { 0.6963, 0.0127, 0.9748, 0.5328, 0.1495, 0.0822, 0.548, 0.497, 0.3712, 0.6923 },
                NullableDoubleMember = 0.3677,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.3484, 0.2594, null, 0.1747, 0.013, null, null, null, null, 0.68 },
                DecimalMember = 0.857478522163573m,
                DecimalArrayMember = new decimal[] { 0.737100502353674m, 0.915612739471538m, 0.662924640655017m, 0.390125958896301m, 0.252441738849712m, 0.0679956716801951m, 0.780560654951521m, 0.102508187807402m, 0.59379215100491m, 0.0444743493778977m },
                NullableDecimalMember = 0.404845153635761m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.92272876571991m, 0.36070676350999m, 0.373831636912111m, null, 0.438297315704775m, null, null, null, 0.0763435755280515m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-196181270),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-8987246), DateTime.Now.AddSeconds(-229722209), DateTime.Now.AddSeconds(314645369), DateTime.Now.AddSeconds(-290441304), DateTime.Now.AddSeconds(64775868), DateTime.Now.AddSeconds(431322), DateTime.Now.AddSeconds(-261995229), DateTime.Now.AddSeconds(-80448604), DateTime.Now.AddSeconds(59533351), DateTime.Now.AddSeconds(334294686) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, null, DateTime.Now.AddSeconds(-189983987), DateTime.Now.AddSeconds(-32206256), DateTime.Now.AddSeconds(23361399), DateTime.Now.AddSeconds(126558285), DateTime.Now.AddSeconds(-121758551), DateTime.Now.AddSeconds(-49950872), DateTime.Now.AddSeconds(-206035898), DateTime.Now.AddSeconds(123574063) },
            },
            new DataConvertersTestClass {
                StringMember = "F!2\"RZ?gaW",
                StringArrayMember = new string[] { "$ SJ3o'fx#", "M3urXO0;:C", "19%.MbERe!", "rEPTG'wkrI", "Fe8DoZXKsC", "w pYrKBe#c", "jxA3cInakf", "oPV5$XA@W.", "D\"wbVsl:y@", ";RoPveeFyf" },
                CharMember = 'M',
                CharArrayMember = new char[] { '7', ',', 'i', 'f', 'U', 'p', '0', 'd', 'T', 'Y' },
                NullableCharMember = 'V',
                NullableCharArrayMember = new Nullable<char>[] { null, null, 'q', 'i', null, null, 'R', '7', null, null },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, false, false, true, false, true, true, true, false, false },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, false, null, false, true, null, false, null, false },
                ByteMember = 63,
                ByteArrayMember = new byte[] { 182, 210, 36, 91, 36, 141, 16, 181, 94, 110 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, null, 128, 226, 197, null, 215, 6, 237, 115 },
                SByteMember = -102,
                SByteArrayMember = new sbyte[] { 26, 107, -73, 53, -68, -28, 19, 35, 87, 36 },
                NullableSByteMember = 34,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, null, 118, -104, 120, 109, 1, -24, 126, 60 },
                Int16Member = -12495,
                Int16ArrayMember = new short[] { -18186, 18189, -8489, -12335, 4320, 18103, 26889, 36, -12033, 22670 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -17056, -3788, 30435, null, 14396, 15569, -21632, -17891, -4099, -22421 },
                UInt16Member = 46730,
                UInt16ArrayMember = new ushort[] { 41898, 62465, 53116, 62918, 6081, 32554, 14870, 58091, 20468, 51433 },
                NullableUInt16Member = 7097,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 31097, null, null, 38387, null, 5735, 46347, null, 55770 },
                Int32Member = -1367715492,
                Int32ArrayMember = new int[] { -112749350, -292947433, 1223649306, 2098910781, 1791920821, -303382310, 848890656, -127534197, 755900052, 785443357 },
                NullableInt32Member = -886974763,
                NullableInt32ArrayMember = new Nullable<int>[] { -607520355, -737180719, 198728362, 52388439, null, 1609418782, 79011769, 1832526536, -1820151960, -2097919355 },
                UInt32Member = 1097625444,
                UInt32ArrayMember = new uint[] { 2839075212, 4217325031, 3824177134, 4104698113, 1459847012, 1478026763, 2192077272, 3673183029, 4195983664, 903928367 },
                NullableUInt32Member = 2858463180,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 34860331, 907308004, null, 2624967136, 375298758, null, 2328145312, null, 3090628246, 1874544205 },
                Int64Member = 7666688416645742280,
                Int64ArrayMember = new long[] { -4979849223717390959, 6721727518926055746, -7163280581758134500, 5925560341990224687, 4749954817652734695, 7576029565982172704, -6474227168697030859, 8214130920647669087, -2188536084289668115, -5126390836722362260 },
                NullableInt64Member = -7711392824581408497,
                NullableInt64ArrayMember = new Nullable<long>[] { -4077820670393186522, -4990050563718730620, 9024941914863258003, null, null, 7609002356609358552, null, -5998499660276599379, 3295873414641381261, null },
                UInt64Member = 17289979874568582782,
                UInt64ArrayMember = new ulong[] { 17664475599628490740, 17083540975256708669, 15062928659542894520, 13487928457170587870, 10986749420953920885, 10309669981169862287, 12547261929694781985, 9042637927125653325, 17404649763939501688, 10419207628630425966 },
                NullableUInt64Member = 14240518354779839482,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 1992500510389893836, null, 5097724359727239416, 7479158369819744140, 17851619379239897721, null, 3027816035252945126, 3743386682531853884, 2928091140257327294 },
                SingleMember = 0.2509F,
                SingleArrayMember = new float[] { 0.0689F, 0.026F, 0.9986F, 0.5681F, 0.187F, 0.6688F, 0.1769F, 0.6203F, 0.5292F, 0.5888F },
                NullableSingleMember = 0.7421F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.9682F, null, 0.7922F, null, 0.0817F, null, null, 0.9959F, null, 0.3229F },
                DoubleMember = 0.1481,
                DoubleArrayMember = new double[] { 0.9838, 0.7104, 0.4238, 0.2076, 0.5647, 0.6881, 0.4656, 0.7569, 0.7923, 0.5602 },
                NullableDoubleMember = 0.3571,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.6064, null, 0.023, null, null, null, 0.0287, 0.3154, 0.2644, null },
                DecimalMember = 0.494471830080483m,
                DecimalArrayMember = new decimal[] { 0.0291857267865845m, 0.578166819446798m, 0.249318837304282m, 0.709115733722744m, 0.442173873745917m, 0.372617789717679m, 0.157437550908624m, 0.374028209305382m, 0.459081354764794m, 0.774342638801012m },
                NullableDecimalMember = 0.0990242953873353m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.957771619296526m, null, 0.294948032263176m, 0.240679500736613m, 0.613160062401164m, null, 0.225752623856884m, 0.882075664532406m, null, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, null, Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null, null },
                DateTimeMember = DateTime.Now.AddSeconds(-75573696),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-261679689), DateTime.Now.AddSeconds(-59169576), DateTime.Now.AddSeconds(127985674), DateTime.Now.AddSeconds(187455687), DateTime.Now.AddSeconds(-266537829), DateTime.Now.AddSeconds(-327499938), DateTime.Now.AddSeconds(89175093), DateTime.Now.AddSeconds(204365381), DateTime.Now.AddSeconds(-194526651), DateTime.Now.AddSeconds(104846753) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-69671002), DateTime.Now.AddSeconds(-92542682), DateTime.Now.AddSeconds(-267202960), null, DateTime.Now.AddSeconds(302370454), null, DateTime.Now.AddSeconds(-328105130), DateTime.Now.AddSeconds(27260444), null, DateTime.Now.AddSeconds(-277822238) },
            },
            new DataConvertersTestClass {
                StringMember = "8#xMkg06aU",
                StringArrayMember = new string[] { "JGIIYqBvh6", "twzooXyFJp", "\"@T;CGTj:,", ":1od97Jf:E", "d5u:8MUIuN", "5vx8bO9SGS", "Xo4t''IV24", "NSOELKYcF;", "8K,0FTE2Cu", "$:Yv4?BB%\"" },
                CharMember = 'H',
                CharArrayMember = new char[] { 'X', '$', 'E', 'r', 'I', 'm', 'T', 'j', 'l', ' ' },
                NullableCharMember = 'T',
                NullableCharArrayMember = new Nullable<char>[] { 's', 'e', 'i', 'L', '5', ';', 'X', 'Z', null, 'x' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, false, false, true, false, true, true, true, true, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, true, false, true, false, true, false, null, null, null },
                ByteMember = 57,
                ByteArrayMember = new byte[] { 248, 130, 35, 17, 6, 200, 250, 177, 152, 228 },
                NullableByteMember = 143,
                NullableByteArrayMember = new Nullable<byte>[] { 209, 107, null, null, null, 100, null, null, null, null },
                SByteMember = 113,
                SByteArrayMember = new sbyte[] { 113, 32, -70, -6, 50, 89, -52, -54, 70, 58 },
                NullableSByteMember = 5,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 87, null, null, -101, -97, 38, -20, 86, 126, 35 },
                Int16Member = -21130,
                Int16ArrayMember = new short[] { -18914, -20776, 22645, 11088, -4958, -5315, 14954, 2783, 16080, -15534 },
                NullableInt16Member = -7345,
                NullableInt16ArrayMember = new Nullable<short>[] { -14844, null, null, null, -5242, -18488, -30365, -4999, -16674, 20529 },
                UInt16Member = 49078,
                UInt16ArrayMember = new ushort[] { 32766, 41195, 23291, 28512, 13250, 35056, 41978, 21763, 62389, 9432 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 54483, 39401, 43023, null, 20807, null, null, null, 37518 },
                Int32Member = -825945662,
                Int32ArrayMember = new int[] { 515273566, 830419412, 414022620, 802183652, -1650575135, -1649217856, -735149713, -1180865836, -1856515852, 139307571 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { 2077570188, -1597736661, 1863198579, 55815961, -203525796, -62979565, null, 1649454741, null, 1510142196 },
                UInt32Member = 1846287922,
                UInt32ArrayMember = new uint[] { 963799579, 2573863247, 18089701, 1288427412, 1152007782, 1878490781, 797126920, 2090036449, 4120422226, 2155180845 },
                NullableUInt32Member = 590917940,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1853519385, null, null, 1922945479, null, 3074131485, null, null, 3594513839, null },
                Int64Member = -1555283668346905745,
                Int64ArrayMember = new long[] { 3812673318722342365, -3350963433850022683, -7569031369119884171, 7184999165934005102, 1756103265821355192, 8334570480128381808, 8563574572257470173, -6374716226972059625, -8704391776412260579, 1649582884430592446 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -5073926547576699241, null, null, null, null, -6443462450240304133, null, null, 6022325979502934499, -9199737994112541225 },
                UInt64Member = 10224389531991384712,
                UInt64ArrayMember = new ulong[] { 2658325934986352224, 5343160374898469396, 14942672186982854525, 16294308879807642552, 8751540683694288729, 14995692523207843267, 6223323890831476347, 1264249083586101622, 14224846737916486153, 5902294142883512187 },
                NullableUInt64Member = 15107543709011600797,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, null, null, 9440438409752519061, null, 1588432956596215469, 1388297932574126423, 2154640124038220601, 4074389659415513089, null },
                SingleMember = 0.3353F,
                SingleArrayMember = new float[] { 0.5727F, 0.0566F, 0.0743F, 0.2213F, 0.1158F, 0.097F, 0.9079F, 0.3343F, 0.974F, 0.838F },
                NullableSingleMember = 0.4182F,
                NullableSingleArrayMember = new Nullable<float>[] { null, null, 0.3489F, null, null, 0.8973F, 0.4968F, 0.4157F, 0.3865F, 0.6891F },
                DoubleMember = 0.0383,
                DoubleArrayMember = new double[] { 0.4769, 0.4281, 0.1717, 0.2669, 0.4022, 0.3999, 0.5132, 0.6608, 0.2685, 0.6562 },
                NullableDoubleMember = 0.3038,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.3245, null, 0.3157, 0.6249, null, 0.5535, null, 0.4873, 0.3151, 0.7895 },
                DecimalMember = 0.476838135848212m,
                DecimalArrayMember = new decimal[] { 0.209597190008311m, 0.266596247100549m, 0.17609816332166m, 0.702988969489461m, 0.286088824871038m, 0.0733224610208173m, 0.0156253315581127m, 0.795155512073615m, 0.175022345117769m, 0.962671279889844m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.708298920983588m, 0.442625956350298m, 0.420714313360264m, null, null, 0.570198927805852m, 0.327612605098455m, 0.185962433082034m, 0.68449968643696m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(109509567),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-68071389), DateTime.Now.AddSeconds(-143532780), DateTime.Now.AddSeconds(-337496253), DateTime.Now.AddSeconds(145977332), DateTime.Now.AddSeconds(-214892953), DateTime.Now.AddSeconds(245974374), DateTime.Now.AddSeconds(305638201), DateTime.Now.AddSeconds(161130640), DateTime.Now.AddSeconds(258998141), DateTime.Now.AddSeconds(-211178396) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(108700319),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(268514645), DateTime.Now.AddSeconds(-347500418), null, null, null, null, DateTime.Now.AddSeconds(47897219), DateTime.Now.AddSeconds(171203609), DateTime.Now.AddSeconds(-184441927) },
            },
            new DataConvertersTestClass {
                StringMember = "E c; ngswR",
                StringArrayMember = new string[] { "?hNQgMzDnw", ".cTI6W\"pXW", "IXdL995%1p", "9jG74n0IPr", "M%kZFy3UGz", "t;UZIpDBdu", "e1Z$9bWiaS", "t8a2KK9EGK", "Gl:B##O3yP", "kz1sNiwmm1" },
                CharMember = '"',
                CharArrayMember = new char[] { 'z', 'i', 'd', 'f', 'P', '3', 'u', '6', 'H', 'N' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, 'P', null, '\'', '4', null, null, null, 'p', 'N' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, true, false, true, false, true, false, false, false },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, false, null, false, null, null, null, false, null },
                ByteMember = 32,
                ByteArrayMember = new byte[] { 222, 38, 30, 116, 54, 63, 156, 123, 100, 235 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, null, 132, null, 92, null, 5, 227, null, null },
                SByteMember = 20,
                SByteArrayMember = new sbyte[] { 73, -102, 49, -107, -126, -46, 93, -94, -100, 8 },
                NullableSByteMember = -54,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 0, null, 40, 64, null, 120, null, -54, null, 76 },
                Int16Member = 7516,
                Int16ArrayMember = new short[] { 1509, 21805, 12979, -11313, -21796, -29010, 143, 26179, 32337, -31839 },
                NullableInt16Member = 18086,
                NullableInt16ArrayMember = new Nullable<short>[] { -10535, null, -9180, 28022, 23110, -18027, 19687, 2271, null, -26025 },
                UInt16Member = 29990,
                UInt16ArrayMember = new ushort[] { 30443, 24206, 45672, 1273, 486, 10898, 34999, 52248, 29041, 54726 },
                NullableUInt16Member = 4490,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 21171, null, 60740, 28049, null, null, null, 33067, 2719, 53981 },
                Int32Member = 1175701775,
                Int32ArrayMember = new int[] { -790117940, 100268825, 1427771971, 696080353, -1620457414, 230521309, 1673155138, -1488105472, 1364700363, 1021537818 },
                NullableInt32Member = 1195273104,
                NullableInt32ArrayMember = new Nullable<int>[] { -646800154, 1769234503, null, 1086819802, null, -1988901389, -888263681, null, 2131578029, -266507495 },
                UInt32Member = 254462194,
                UInt32ArrayMember = new uint[] { 1270795368, 3020691538, 144663410, 2197477002, 4200022570, 3325656313, 925632139, 722882724, 3496523849, 98482099 },
                NullableUInt32Member = 3541386631,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1396971301, 372208047, 1287714963, 1277300693, null, null, 1926113645, null, 590836353, null },
                Int64Member = -931965696537121517,
                Int64ArrayMember = new long[] { 1478269382677898505, -7997505100124376878, 1391463340188079921, -306123669894917146, -515813097709427249, -1439910253602788262, -8040629463847699414, 7201428750032357077, -4910736008458683537, 9066438636421245631 },
                NullableInt64Member = -4837463912449852506,
                NullableInt64ArrayMember = new Nullable<long>[] { 3187630090699666078, -3668564959011385467, -6626631290493334481, -1106308547299457208, 5551736711899054139, -5859070389428135840, -803320005637817009, -2685083638951662803, -6247717170052323342, null },
                UInt64Member = 5302017445105190235,
                UInt64ArrayMember = new ulong[] { 16652185068036954241, 4452263531491307850, 12563252346899728911, 10179771990545122661, 1651006550381198680, 13573136329260465636, 16057377762068475260, 8734531955959118526, 9406044136762993388, 17085081521178310433 },
                NullableUInt64Member = 11967346830896943404,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 776476819159317263, 11157567473337962537, 6695532821085053655, 17067784225020903860, 17321429857915800369, null, 4845036396091396543, null, 11675601413666898267, 11317445909283918013 },
                SingleMember = 0.8305F,
                SingleArrayMember = new float[] { 0.0027F, 0.0693F, 0.0387F, 0.3459F, 0.8417F, 0.0476F, 0.4726F, 0.5145F, 0.7899F, 0.6263F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.2794F, 0.9542F, 0.5361F, 0.7286F, null, 0.5603F, 0.0517F, null, 0.0759F, 0.6586F },
                DoubleMember = 0.1084,
                DoubleArrayMember = new double[] { 0.3525, 0.3004, 0.2208, 0.4272, 0.28, 0.3642, 0.3885, 0.4798, 0.9548, 0.6573 },
                NullableDoubleMember = 0.872,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.3668, 0.0916, null, null, null, 0.9997, 0.6395, 0.9132, 0.9044, null },
                DecimalMember = 0.0235662134473986m,
                DecimalArrayMember = new decimal[] { 0.438839959650692m, 0.560098209213511m, 0.953436583258881m, 0.796736835873098m, 0.897190073457169m, 0.249484164756483m, 0.507746391700463m, 0.0385710951120458m, 0.396035325897874m, 0.51456637378529m },
                NullableDecimalMember = 0.317975189684879m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.119925687145407m, 0.884141795748911m, null, 0.443060631604428m, 0.0665902453784786m, 0.484452354015993m, 0.31790144896037m, 0.051217771624782m, 0.340434088530221m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), null, null, null, Guid.NewGuid(), Guid.NewGuid(), null, null },
                DateTimeMember = DateTime.Now.AddSeconds(-81064447),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(248135511), DateTime.Now.AddSeconds(-189804758), DateTime.Now.AddSeconds(93870701), DateTime.Now.AddSeconds(-219536280), DateTime.Now.AddSeconds(16997331), DateTime.Now.AddSeconds(-255144186), DateTime.Now.AddSeconds(223544738), DateTime.Now.AddSeconds(-101620467), DateTime.Now.AddSeconds(26605258), DateTime.Now.AddSeconds(328081370) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-289264857),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(-176201101), null, DateTime.Now.AddSeconds(-156329865), DateTime.Now.AddSeconds(223218117), DateTime.Now.AddSeconds(134633798), null, DateTime.Now.AddSeconds(-29495589), null, DateTime.Now.AddSeconds(-118121607) },
            },
            new DataConvertersTestClass {
                StringMember = "cU!.G7mpP%",
                StringArrayMember = new string[] { "2 @To\"9XB?", "Mp7L5\"G5pz", "lXob58qD!L", "gwo0jk4iDU", "vYlwo.k5EN", "14Amkfhqk2", "Z6c?gnAHzU", "Mv1VDM9:.!", "Y7%4hzwCPD", "DN$Y!?@vQB" },
                CharMember = 'V',
                CharArrayMember = new char[] { '7', 't', '!', '"', 'w', 'Z', 'D', 'q', 'x', 'P' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'm', 'M', ',', null, 'K', '!', 'g', 'H', 'a', 'd' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, false, false, false, false, false, false, true, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, false, true, true, true, null, null, false, null, false },
                ByteMember = 133,
                ByteArrayMember = new byte[] { 180, 148, 211, 136, 9, 164, 113, 228, 233, 211 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 174, 242, null, null, 14, 220, null, null, 37, 232 },
                SByteMember = 91,
                SByteArrayMember = new sbyte[] { -92, -51, -9, -94, -90, -30, -22, -75, 66, 5 },
                NullableSByteMember = -27,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 65, null, -37, 106, 40, 80, -89, 79, null, null },
                Int16Member = -12968,
                Int16ArrayMember = new short[] { -20558, 24280, -16834, 31910, 4060, -20642, -10241, 8503, -17940, 3561 },
                NullableInt16Member = 3013,
                NullableInt16ArrayMember = new Nullable<short>[] { 8050, -4226, null, -13098, null, -27193, null, -19812, -31011, -30819 },
                UInt16Member = 39196,
                UInt16ArrayMember = new ushort[] { 29808, 38052, 51997, 49489, 25952, 43627, 43029, 48946, 47097, 48384 },
                NullableUInt16Member = 48252,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 61509, 43061, null, null, 47254, 40786, null, null, 4123, null },
                Int32Member = 1174810663,
                Int32ArrayMember = new int[] { 1292124759, -65281728, 2037943386, -1649303852, 1632881222, 1159579187, 1534334084, -1025727198, -231592769, -355541253 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { 110477395, null, 1554612410, 630264740, -1066587192, -1162498123, 1755496044, null, 1551041432, -1594285186 },
                UInt32Member = 4061520124,
                UInt32ArrayMember = new uint[] { 3127336016, 1031577882, 445839291, 3357767587, 2840842336, 1266779419, 3047468575, 1807943279, 3317687254, 816101605 },
                NullableUInt32Member = 3531281388,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1528036318, null, null, null, null, 2602293963, 3033384957, null, 1572323139, 3089247399 },
                Int64Member = 6262974542134657985,
                Int64ArrayMember = new long[] { 6209105118633519283, -6104499899160878371, -4947230154758373735, -8970793167660467443, -379002045380210134, 5520389699769177792, -3822388366129981957, -8776817400223073903, 1586803629306713888, -5797448347106964736 },
                NullableInt64Member = 6702336005339264460,
                NullableInt64ArrayMember = new Nullable<long>[] { -1052639553312641983, null, 9037078004451065661, -4231596333745743575, null, -3397628016608863222, -2273487201647542620, -3227708752313057745, -2772315766695217090, -2833514344678146273 },
                UInt64Member = 11333151987839120388,
                UInt64ArrayMember = new ulong[] { 3003643483020973607, 908938473710301747, 18070197826190615231, 10352583718072772830, 12067445397803078499, 15196323226060875552, 16820508450226478359, 8007154797869688592, 10548664770267750534, 12575704777644734761 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 10314632098795711608, 6841046997891061311, null, null, 12087899876348179481, null, 16852384767279687650, 12618517868236505741, 14072841724221385855, null },
                SingleMember = 0.173F,
                SingleArrayMember = new float[] { 0.7293F, 0.2009F, 0.2189F, 0.5038F, 0.9713F, 0.7687F, 0.289F, 0.0723F, 0.5044F, 0.5785F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.6458F, 0.4797F, 0.0291F, null, 0.1811F, null, null, 0.1775F, 0.4424F, 0.9871F },
                DoubleMember = 0.7796,
                DoubleArrayMember = new double[] { 0.2602, 0.0848, 0.5581, 0.8713, 0.1601, 0.3038, 0.5026, 0.0829, 0.4556, 0.0444 },
                NullableDoubleMember = 0.6788,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.8628, 0.3134, 0.8397, 0.0119, null, null, 0.3935, 0.1087, 0.6609, 0.5141 },
                DecimalMember = 0.404777057191719m,
                DecimalArrayMember = new decimal[] { 0.388822774583857m, 0.658017716676936m, 0.771589298626217m, 0.736182385467078m, 0.402573070676333m, 0.403101121728821m, 0.968554378938188m, 0.811108847526418m, 0.652141243988714m, 0.934879564649835m },
                NullableDecimalMember = 0.0246789874623897m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.596076538132539m, 0.496778764061992m, 0.494366778291001m, 0.77220937878462m, 0.241439054832533m, null, 0.589278498938902m, 0.0147810732083307m, null, 0.198266393131701m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, null, Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(158570000),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-195297836), DateTime.Now.AddSeconds(324773723), DateTime.Now.AddSeconds(-35247084), DateTime.Now.AddSeconds(80416301), DateTime.Now.AddSeconds(36702647), DateTime.Now.AddSeconds(262995129), DateTime.Now.AddSeconds(98802898), DateTime.Now.AddSeconds(44950902), DateTime.Now.AddSeconds(-342947770), DateTime.Now.AddSeconds(-258297012) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(20135112), null, DateTime.Now.AddSeconds(-74129666), DateTime.Now.AddSeconds(-316808829), DateTime.Now.AddSeconds(226251318), DateTime.Now.AddSeconds(127618091), null, DateTime.Now.AddSeconds(-57660045), DateTime.Now.AddSeconds(-236498288), DateTime.Now.AddSeconds(175371518) },
            },
            new DataConvertersTestClass {
                StringMember = "9cG7AFC2iX",
                StringArrayMember = new string[] { "tp3pdo7ue4", ":%:vR xdaO", "RNy8I@1O4b", "w!q'glge!d", "EXQjJ,lipB", "fwbUGg:!hh", ".t;CI$f%P3", "pSi#J\"#PzT", "hSv2# pV9A", "tl#kqihNaM" },
                CharMember = ',',
                CharArrayMember = new char[] { 'Z', 'B', 'i', '9', 'N', 'M', 'U', '5', 'R', 'z' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { '2', null, null, 'l', null, null, 'O', 'D', '?', null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, false, false, true, false, true, false, false, true, false },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, true, null, false, true, true, null, false, true },
                ByteMember = 191,
                ByteArrayMember = new byte[] { 27, 216, 66, 131, 6, 175, 138, 7, 166, 218 },
                NullableByteMember = 182,
                NullableByteArrayMember = new Nullable<byte>[] { 164, 175, 39, 164, 103, null, 139, null, null, 60 },
                SByteMember = 73,
                SByteArrayMember = new sbyte[] { 0, 4, -112, -59, 119, -70, -44, -88, -105, -99 },
                NullableSByteMember = 119,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 39, null, 105, -122, 110, null, 7, -104, null, null },
                Int16Member = -12280,
                Int16ArrayMember = new short[] { 28873, -20067, 4426, -23572, -5060, 22046, -29401, 17250, -7288, -18704 },
                NullableInt16Member = -22388,
                NullableInt16ArrayMember = new Nullable<short>[] { null, null, null, -31513, -26668, null, 2085, -32476, 9011, -5801 },
                UInt16Member = 56055,
                UInt16ArrayMember = new ushort[] { 27520, 27966, 51530, 53542, 46431, 47347, 20254, 60204, 6428, 12696 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 4189, 40992, 41274, 50386, 33988, 24007, null, 14206, null, 32922 },
                Int32Member = -444531807,
                Int32ArrayMember = new int[] { -861857510, 2107774268, 278746633, 239684575, 1614929660, 504368243, 139071176, -1187504269, -1885351889, 1399190219 },
                NullableInt32Member = -710111509,
                NullableInt32ArrayMember = new Nullable<int>[] { -338087217, null, 242360250, -903019646, -484513527, 1119529999, -2029589127, 1065109603, null, null },
                UInt32Member = 2030400047,
                UInt32ArrayMember = new uint[] { 2712260583, 3717755668, 3941862507, 859250121, 1214127657, 1126102186, 4276844725, 1504162666, 2629018325, 1102652756 },
                NullableUInt32Member = 3283277897,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 4025780505, null, 4073635091, 2291919973, null, 362440119, null, 1678040731, null, 2137725793 },
                Int64Member = 5150178412900959077,
                Int64ArrayMember = new long[] { 9064405875656336790, -458173071659394257, -6969359440443967082, -7114782537041697283, -2208036982672041872, 2798102328293785204, -4988623032035058146, -467712713055676834, 2083346208459487831, 2477027866545254429 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { 3356193087381445459, -4311216866168006342, null, null, 4660196901393896912, null, -7711739234072576395, null, 1320322165769906199, 2791530450883733132 },
                UInt64Member = 6771870658594191852,
                UInt64ArrayMember = new ulong[] { 8175460031673803695, 2938500984008129317, 3108023816452712461, 11854986031351889636, 15846578698029156158, 4981342218709516045, 14063699734458088946, 14372188306374075906, 12883616951028239414, 11299555744298320701 },
                NullableUInt64Member = 11164773328143465901,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 15280655019906689078, 2456272079006848266, null, 12972462294642912175, 14597809971892260101, 9768801551746848902, 15663594391477308952, 14953382053391438304, 11289579632020825023 },
                SingleMember = 0.9203F,
                SingleArrayMember = new float[] { 0.8149F, 0.9717F, 0.309F, 0.8559F, 0.6169F, 0.8971F, 0.416F, 0.8414F, 0.7755F, 0.0851F },
                NullableSingleMember = 0.9456F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.1501F, 0.0122F, 0.4018F, null, null, 0.6355F, 0.4775F, null, 0.0334F },
                DoubleMember = 0.3566,
                DoubleArrayMember = new double[] { 0.4599, 0.0018, 0.7002, 0.6946, 0.5724, 0.5351, 0.0007, 0.5536, 0.063, 0.2704 },
                NullableDoubleMember = 0.1996,
                NullableDoubleArrayMember = new Nullable<double>[] { null, null, 0.1637, 0.6719, 0.66, 0.7474, null, null, 0.2782, 0.9354 },
                DecimalMember = 0.568762749698368m,
                DecimalArrayMember = new decimal[] { 0.698137006581825m, 0.879008789490447m, 0.109789316593571m, 0.495626383691852m, 0.659957473007942m, 0.301830865117642m, 0.383743301678329m, 0.43067275613112m, 0.48104067402009m, 0.445198675359226m },
                NullableDecimalMember = 0.790919387615714m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.945998962012119m, 0.60139541728487m, null, null, null, 0.444616863711093m, null, 0.428326430929977m, 0.0783433500110839m, 0.146903886528175m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(159959308),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(279829858), DateTime.Now.AddSeconds(272287624), DateTime.Now.AddSeconds(-8333656), DateTime.Now.AddSeconds(89809532), DateTime.Now.AddSeconds(-128096906), DateTime.Now.AddSeconds(-346692354), DateTime.Now.AddSeconds(180469034), DateTime.Now.AddSeconds(-19681158), DateTime.Now.AddSeconds(265116469), DateTime.Now.AddSeconds(-61018020) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-276868740), DateTime.Now.AddSeconds(-27928738), null, null, DateTime.Now.AddSeconds(342649), null, DateTime.Now.AddSeconds(53976301), DateTime.Now.AddSeconds(5627075), null, null },
            },
            new DataConvertersTestClass {
                StringMember = "gmT9jMOfKM",
                StringArrayMember = new string[] { "jySOAr$LGI", "eJMOJDV$dy", "j6,m.! 9T6", "7KLwn@T5j%", "lWHS5XaEzf", ";QBi00.3aF", "rK:WYU$nxO", "7nkfNNyt3n", "r@K.\"qIz@.", "Imfedae7\"k" },
                CharMember = 'k',
                CharArrayMember = new char[] { 'O', 'p', 'B', '0', 'X', 'U', 'b', 'L', 'N', 'D' },
                NullableCharMember = '%',
                NullableCharArrayMember = new Nullable<char>[] { 'r', null, 'i', null, 'T', 'G', '9', ':', 'w', null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, true, false, true, true, true, false, false, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, false, null, false, false, null, null, true, null, null },
                ByteMember = 0,
                ByteArrayMember = new byte[] { 98, 236, 187, 177, 201, 17, 145, 14, 29, 253 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 245, null, 86, 56, 167, 253, null, 212, 63, 247 },
                SByteMember = 121,
                SByteArrayMember = new sbyte[] { -12, 18, 109, 42, 119, -123, 79, -44, 50, 73 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, 72, -56, -91, 37, 69, null, null, null, 95 },
                Int16Member = -29883,
                Int16ArrayMember = new short[] { 31372, 67, -26695, 24402, -14276, 25580, 28656, 7684, 5929, -1914 },
                NullableInt16Member = 22887,
                NullableInt16ArrayMember = new Nullable<short>[] { 25187, 19842, null, 2247, 27925, 6929, null, -5810, -16063, 11918 },
                UInt16Member = 65289,
                UInt16ArrayMember = new ushort[] { 65498, 4855, 49349, 3693, 36398, 10282, 38555, 57533, 62497, 35444 },
                NullableUInt16Member = 21834,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 9369, null, 27481, 38815, 41568, 65216, null, null, null, 12856 },
                Int32Member = -9077880,
                Int32ArrayMember = new int[] { -2037359345, -2009534904, -1901731137, -2034275343, 1234872351, -1846928947, -293127771, 1853909939, -1294867288, 469382172 },
                NullableInt32Member = -677034396,
                NullableInt32ArrayMember = new Nullable<int>[] { -1853943465, 824959200, 249626227, null, null, -449355436, null, null, 2142104425, -1169829272 },
                UInt32Member = 157441858,
                UInt32ArrayMember = new uint[] { 1167467642, 3955282967, 3088923118, 1293997569, 535911720, 2894729305, 3597406453, 1017767493, 2482540326, 1409111502 },
                NullableUInt32Member = 1204482224,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 1444871051, 1789936237, null, 3932620623, 2464190216, null, 1607696013, 3206588790, 3688981905 },
                Int64Member = -4284564456073306396,
                Int64ArrayMember = new long[] { 2175010007830870490, 479940428106829998, -1332619252054280551, -232815912622342072, 6698258526154320448, -3683957856446808778, 4965991224202786485, -3533644438984540361, -7322386872652879028, 6130642208263838018 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, -3898991921671467341, null, null, -8905535346594857663, null, -3627211719788808684, 7252272329677435764, 6329846000351316827, null },
                UInt64Member = 12551890145557156564,
                UInt64ArrayMember = new ulong[] { 18037205614412392175, 9228465229471986890, 3058729938712706930, 14648910168271650925, 13263633932472065255, 13501892748395158738, 2594580757724211456, 7550850566100783603, 10283514858887048297, 1242794788622834349 },
                NullableUInt64Member = 11104285091491564981,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 1968582259764508859, 2857661083672572670, 16163819639885689147, 12005192872005680325, 4651420439190900008, null, 3016849133287878464, null, null, 7695208706144816889 },
                SingleMember = 0.0414F,
                SingleArrayMember = new float[] { 0.5199F, 0.5289F, 0.8366F, 0.302F, 0.152F, 0.6895F, 0.6068F, 0.0866F, 0.9786F, 0.0289F },
                NullableSingleMember = 0.0636F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.2536F, null, 0.2355F, 0.4196F, 0.0614F, 0.5279F, null, null, 0.8171F, null },
                DoubleMember = 0.0514,
                DoubleArrayMember = new double[] { 0.9888, 0.8417, 0.9227, 0.9415, 0.6878, 0.643, 0.677, 0.1357, 0.1682, 0.3952 },
                NullableDoubleMember = 0.2525,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.094, 0.3871, 0.3425, 0.7611, 0.1404, 0.413, 0.2729, 0.627, 0.9212, 0.3704 },
                DecimalMember = 0.326927215478815m,
                DecimalArrayMember = new decimal[] { 0.388320048520491m, 0.489718992025461m, 0.82055798211161m, 0.453874850857013m, 0.366116814951467m, 0.422454276784535m, 0.145670197971943m, 0.773267620137552m, 0.100450154440687m, 0.0648666033823353m },
                NullableDecimalMember = 0.654710429559793m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.470481605022439m, 0.882698332370584m, null, 0.010977006056801m, 0.474250437912648m, 0.885758014342635m, null, 0.124145885987275m, 0.13419163186764m, 0.348242663940528m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-2759751),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(135042598), DateTime.Now.AddSeconds(-211923655), DateTime.Now.AddSeconds(-128852681), DateTime.Now.AddSeconds(149493416), DateTime.Now.AddSeconds(313368517), DateTime.Now.AddSeconds(219579383), DateTime.Now.AddSeconds(-152907079), DateTime.Now.AddSeconds(-188126870), DateTime.Now.AddSeconds(96284924), DateTime.Now.AddSeconds(184333036) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-153291989),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(95401299), DateTime.Now.AddSeconds(-231925298), DateTime.Now.AddSeconds(-198744683), DateTime.Now.AddSeconds(54486831), DateTime.Now.AddSeconds(-142360945), DateTime.Now.AddSeconds(82310216), DateTime.Now.AddSeconds(-214908269), DateTime.Now.AddSeconds(50251683), DateTime.Now.AddSeconds(184683704), DateTime.Now.AddSeconds(-328108527) },
            },
        };
    }
}
