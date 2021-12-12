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
                StringMember = "6KB5DT;lw?",
                StringArrayMember = new string[] { "AJw;qX9#,k", "6kwWEvGKDz", "Z2jH3HLDty", "7hQdYwzTxZ", "P8: X?:hwO", "HkOm$j09Yp", "H8X!emTx3 ", "TtKw,aL @N", "nV%DiBF3w2", "pBoZh'2OMJ" },
                CharMember = ' ',
                CharArrayMember = new char[] { '3', 'a', ',', '5', '#', 'h', 'b', 'd', '#', 'q' },
                NullableCharMember = '.',
                NullableCharArrayMember = new Nullable<char>[] { 'w', null, 'o', null, 't', 'a', null, null, null, 'l' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, false, false, false, false, false, false, true, false, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, false, false, true, true, true, null, null, null },
                ByteMember = 221,
                ByteArrayMember = new byte[] { 54, 60, 119, 246, 217, 167, 20, 23, 154, 246 },
                NullableByteMember = 182,
                NullableByteArrayMember = new Nullable<byte>[] { 112, 46, 28, 150, 117, 54, 65, 83, 185, 231 },
                SByteMember = -29,
                SByteArrayMember = new sbyte[] { -103, 62, -7, 8, 15, -96, 42, 73, 44, -44 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -37, -16, 13, -103, 9, null, 57, null, null, null },
                Int16Member = 986,
                Int16ArrayMember = new short[] { -30393, -21655, -1071, -12543, 11696, -19714, -18023, 18609, 2684, 19830 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -30029, null, null, null, 10710, 4789, null, 30416, null, 14933 },
                UInt16Member = 7480,
                UInt16ArrayMember = new ushort[] { 26002, 2469, 62919, 59088, 53576, 27789, 47275, 41360, 59296, 7193 },
                NullableUInt16Member = 53257,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, null, 53647, 7914, 44495, null, 39753, 22387, 55310, 10446 },
                Int32Member = 464586764,
                Int32ArrayMember = new int[] { -777002771, -1621123974, -1249618171, -63005252, -1758243667, -1563694390, 98381889, -975777860, -1138749315, -1552371385 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { -412768970, 2145362524, null, -42766182, null, -264164053, 417170075, 515908958, 1299187235, -1609919762 },
                UInt32Member = 1127438983,
                UInt32ArrayMember = new uint[] { 173601425, 4186863291, 1399518201, 1339911551, 375085645, 3255273369, 2952185803, 3092293721, 59240401, 3541601100 },
                NullableUInt32Member = 2471374355,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 2914992612, 2375505606, 3168300500, 2079818740, null, 2264102871, 4085080645, null, 3579006009 },
                Int64Member = -4454413821036044112,
                Int64ArrayMember = new long[] { -2338754932416587873, 552474377318705934, -4515144480654232188, -7278579415126063347, 6937947804827723997, -498654985731837046, -831280443069732529, 1417898766877061196, -3714369093332617755, 5554932556580562615 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -5249008854638234858, 7475522042061589428, -2328409089103462734, null, -8094373643126966127, 3117382660307533777, null, 8684489032911098366, -7727120862272547575, -4816075185149246772 },
                UInt64Member = 13242941389743808420,
                UInt64ArrayMember = new ulong[] { 10291512461409322495, 8909306294362865865, 428542456945990312, 15011957660214722799, 17631453181543048899, 18339108796696363794, 809338474527316436, 11050640746507617006, 11993429320652452633, 1911427897709533913 },
                NullableUInt64Member = 10745059157777261662,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 10504310630292386865, 16099078963092791508, 13388396234955685568, 6105995366482656202, 7110070159888762732, 7127479760213342137, 13522321026887927954, null, 11027562148857418002, 14178821615993711808 },
                SingleMember = 0.0102F,
                SingleArrayMember = new float[] { 0.3545F, 0.1718F, 0.8362F, 0.6691F, 0.1664F, 0.6013F, 0.9106F, 0.1591F, 0.5599F, 0.7457F },
                NullableSingleMember = 0.7236F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.9945F, null, null, 0.8184F, 0.5082F, null, null, null, null, 0.9904F },
                DoubleMember = 0.1419,
                DoubleArrayMember = new double[] { 0.0794, 0.1737, 0.3719, 0.1687, 0.8999, 0.4313, 0.0763, 0.0565, 0.4441, 0.5291 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.8603, null, 0.1959, 0.9963, null, null, 0.8134, null, 0.47, 0.4679 },
                DecimalMember = 0.795915808977588m,
                DecimalArrayMember = new decimal[] { 0.325567604581332m, 0.865838034404109m, 0.355579199812544m, 0.52492829977383m, 0.388730957584762m, 0.584123371155367m, 0.264363986559581m, 0.128844087839219m, 0.918078982614193m, 0.204597859069922m },
                NullableDecimalMember = 0.368240399307637m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.153280297222352m, 0.635982302869017m, 0.720137466988654m, 0.64922045970497m, null, null, 0.0775564840132793m, null, 0.238075406378572m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-100766238),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-92654896), DateTime.Now.AddSeconds(328578340), DateTime.Now.AddSeconds(-33495656), DateTime.Now.AddSeconds(162036365), DateTime.Now.AddSeconds(188596116), DateTime.Now.AddSeconds(-34119713), DateTime.Now.AddSeconds(106363899), DateTime.Now.AddSeconds(172062720), DateTime.Now.AddSeconds(-305418259), DateTime.Now.AddSeconds(-291115167) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(192637872),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-246315375), DateTime.Now.AddSeconds(-268683589), DateTime.Now.AddSeconds(113083005), DateTime.Now.AddSeconds(-208317964), null, DateTime.Now.AddSeconds(-284964151), null, null, DateTime.Now.AddSeconds(281657750), DateTime.Now.AddSeconds(303692099) },
            },
            new DataConvertersTestClass {
                StringMember = "QY1N3dUia:",
                StringArrayMember = new string[] { "AiY@.yh1cX", ";%IhUEFSoo", ",kx;8!WIUJ", "N3ks EEfI8", "vH7bYCU!Bf", "oCx,cnEnvK", "Qslkmw1VIC", "JEfPc@e24t", "X@HlLHyui'", "ssqvs8@3bM" },
                CharMember = 'W',
                CharArrayMember = new char[] { 'M', 'u', 'o', '7', '?', 'S', '#', '\'', 'm', 'Z' },
                NullableCharMember = '@',
                NullableCharArrayMember = new Nullable<char>[] { 'h', '#', '"', null, 'P', 'a', null, null, null, '"' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, true, true, false, false, false, true, false, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, true, true, true, false, true, null, null, null, false },
                ByteMember = 129,
                ByteArrayMember = new byte[] { 214, 111, 158, 242, 242, 64, 22, 203, 191, 11 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, null, 244, 242, null, null, 81, 42, null, 117 },
                SByteMember = 55,
                SByteArrayMember = new sbyte[] { 73, 109, 1, 41, 98, 15, 14, -46, 49, 10 },
                NullableSByteMember = 101,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -123, null, 41, 1, null, -18, 88, -47, -66, -30 },
                Int16Member = 31774,
                Int16ArrayMember = new short[] { -8150, -23545, 5810, 22199, -17599, -10061, 11905, -21785, 84, 31777 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { 6641, null, -10537, -26703, null, 3974, null, null, -20070, null },
                UInt16Member = 45613,
                UInt16ArrayMember = new ushort[] { 15870, 4547, 22618, 52779, 1867, 52202, 47648, 48048, 16837, 16196 },
                NullableUInt16Member = 39528,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 41516, 29272, null, 20622, 53334, 35010, null, null, 39522, 8937 },
                Int32Member = 1456489520,
                Int32ArrayMember = new int[] { 595006548, 1419951983, -888199218, -2005141376, 1587248487, -252036719, 903996329, -2068586648, -1814695513, 1631530782 },
                NullableInt32Member = 697386159,
                NullableInt32ArrayMember = new Nullable<int>[] { 1069263982, null, 1897286267, -174001292, -937210556, -692008407, -1581104847, -1280376358, -366426853, -978033513 },
                UInt32Member = 3055242101,
                UInt32ArrayMember = new uint[] { 4016817847, 3116493021, 844516307, 4235604241, 267887774, 2884662157, 3658429848, 3840434142, 728806141, 2412793841 },
                NullableUInt32Member = 1603818441,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 2067143272, 242330027, 3433061066, 3872977103, 900568068, 3171199614, 2950738519, 2926124831, 848604436 },
                Int64Member = -6994709862619464328,
                Int64ArrayMember = new long[] { 4609878293089501468, -4676296369807049535, -2642335870894009897, 740898754827334429, 6539891868866974507, 6775424745687685678, -8565654970155866967, 8912211231726090403, -5062936381770923350, 7044373785481963156 },
                NullableInt64Member = -2937366566505070848,
                NullableInt64ArrayMember = new Nullable<long>[] { -8920077093043861380, null, -6588317970247409344, 7870853446940287175, 3865404344027328504, null, -7592799773603547240, null, 1822129929636375356, 4992146602809941765 },
                UInt64Member = 11301417778055967948,
                UInt64ArrayMember = new ulong[] { 3784464920142139279, 10808775079670393758, 2108870098489263689, 10988525147293433163, 12898532787954136517, 9628225942572514128, 1945190736354010172, 17849679329844890490, 3784977025254016818, 8244646113821346762 },
                NullableUInt64Member = 8302895943870513128,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 3218397608283368918, null, 2464076704454946759, null, 14531544235754167673, 3478458850580645340, 3180582455215757, 11737651890794739469, 9257194085732133550, 529363492292410332 },
                SingleMember = 0.9887F,
                SingleArrayMember = new float[] { 0.0599F, 0.0893F, 0.8766F, 0.4508F, 0.6327F, 0.1137F, 0.3845F, 0.4859F, 0.78F, 0.8664F },
                NullableSingleMember = 0.3476F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.8754F, null, 0.5347F, 0.3565F, 0.0736F, 0.8559F, 0.0814F, null, null },
                DoubleMember = 0.1661,
                DoubleArrayMember = new double[] { 0.0284, 0.0608, 0.8166, 0.4582, 0.3392, 0.3079, 0.6374, 0.7041, 0.8583, 0.9121 },
                NullableDoubleMember = 0.9137,
                NullableDoubleArrayMember = new Nullable<double>[] { null, null, null, 0.0175, 0.5352, null, 0.1183, 0.8101, null, 0.2238 },
                DecimalMember = 0.0355348325678881m,
                DecimalArrayMember = new decimal[] { 0.770986416649035m, 0.947981044930818m, 0.0956554755520328m, 0.970782743074815m, 0.348525303483331m, 0.514828361704275m, 0.691876755584688m, 0.425171995817463m, 0.0966596066712216m, 0.150415862994262m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.0580216756610278m, null, null, 0.358559696990276m, 0.273214024646227m, null, 0.610690890652628m, null, null, 0.453981733261891m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), null, null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-123227068),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-213549691), DateTime.Now.AddSeconds(-250027990), DateTime.Now.AddSeconds(-189455400), DateTime.Now.AddSeconds(325980613), DateTime.Now.AddSeconds(186518726), DateTime.Now.AddSeconds(338460619), DateTime.Now.AddSeconds(112670052), DateTime.Now.AddSeconds(8794166), DateTime.Now.AddSeconds(252994048), DateTime.Now.AddSeconds(-137101784) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(96405310),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-329545929), DateTime.Now.AddSeconds(-213099880), DateTime.Now.AddSeconds(-73391670), null, DateTime.Now.AddSeconds(-233400159), DateTime.Now.AddSeconds(-316199739), DateTime.Now.AddSeconds(269881886), DateTime.Now.AddSeconds(-336177346), DateTime.Now.AddSeconds(87470056), DateTime.Now.AddSeconds(-228525774) },
            },
            new DataConvertersTestClass {
                StringMember = "%?c!EwdKyn",
                StringArrayMember = new string[] { "XJ,bmsGg%O", "m8Tw@@#kp2", "2mWGs0J,s8", "Rxhw9,6xhu", "yRbA2VByG8", "xL7?UGH3gs", "yqBHb@bxAF", "TaOy2i7vmI", "LWcZ\"u5!KH", "E,CV't1G%." },
                CharMember = 'D',
                CharArrayMember = new char[] { 'A', 'M', 'd', ':', 'K', '\'', 'J', 'D', '@', 'K' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, null, 'y', 'h', 'i', 'l', null, null, null, null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, false, false, true, true, false, true, true, false },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, true, false, null, true, true, false, null, true, null },
                ByteMember = 202,
                ByteArrayMember = new byte[] { 209, 54, 60, 165, 145, 15, 134, 82, 182, 185 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 247, null, null, null, null, 21, null, 221, 42, 32 },
                SByteMember = -29,
                SByteArrayMember = new sbyte[] { -30, 23, 102, 79, 100, 24, 90, 20, -14, -48 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 111, -68, 115, -31, -89, 1, 79, -40, 124, 59 },
                Int16Member = -24663,
                Int16ArrayMember = new short[] { 2470, -20352, -5340, -15960, 13271, 22444, -2012, -4401, -32598, -753 },
                NullableInt16Member = 11625,
                NullableInt16ArrayMember = new Nullable<short>[] { -31830, -24441, -3231, -15545, 3271, 20468, -4546, null, 1440, 15829 },
                UInt16Member = 7376,
                UInt16ArrayMember = new ushort[] { 22807, 35636, 40644, 3036, 30725, 39576, 13339, 33408, 43955, 56736 },
                NullableUInt16Member = 12556,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 50177, null, 31484, 21620, 13332, null, null, null, null, 51338 },
                Int32Member = -761211924,
                Int32ArrayMember = new int[] { -907784486, -947527588, -1570953451, -659232502, -618193204, -2038688416, -2084983523, 1361888563, -1780176517, -709397500 },
                NullableInt32Member = 998307824,
                NullableInt32ArrayMember = new Nullable<int>[] { -439415599, 479984694, -1920603071, null, null, -768572339, -1728715625, -2095462917, -1687251566, null },
                UInt32Member = 1313424430,
                UInt32ArrayMember = new uint[] { 2532431155, 3107273663, 1027034587, 2738835869, 235470712, 2963190913, 1953524080, 3234398984, 863057498, 4247619521 },
                NullableUInt32Member = 1459454321,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 4221349447, 57026086, 3930596483, 3475332713, null, 3631351732, 3362070887, 1410795660, 3436223468 },
                Int64Member = -8518773781826261426,
                Int64ArrayMember = new long[] { -4596160213971155351, -2893467650824246306, -8539339296929375118, 4429888801094230023, 2508017424210806181, 2765110447508767921, 3976493556973645055, 2854927640889111580, -1172150163676829464, -6112566347731714163 },
                NullableInt64Member = 1116567604288864856,
                NullableInt64ArrayMember = new Nullable<long>[] { 8995786145455696221, null, null, 4964158871034401357, null, 8341175834767853666, -8890564217282188799, null, 4602415479467276712, null },
                UInt64Member = 4355593459154279170,
                UInt64ArrayMember = new ulong[] { 2421704200216970001, 16561672622253800403, 8632842824230888863, 10343248056564272128, 3411221955565748177, 2739478158415238417, 218532928020314293, 14405967287864293645, 14899215941228429576, 11865752251690486696 },
                NullableUInt64Member = 7713440190324524404,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, null, 13750768497933224979, 1283235847395961363, null, 8118969209637962744, null, 1222878637403891149, null, 4278673115949280464 },
                SingleMember = 0.068F,
                SingleArrayMember = new float[] { 0.0835F, 0.1098F, 0.4446F, 0.3482F, 0.3344F, 0.763F, 0.0771F, 0.1245F, 0.8602F, 0.9526F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.5489F, null, 0.6927F, 0.9846F, null, 0.6752F, 0.5634F, 0.4477F, 0.4242F },
                DoubleMember = 0.507,
                DoubleArrayMember = new double[] { 0.6644, 0.5998, 0.001, 0.0604, 0.8677, 0.3243, 0.6647, 0.7358, 0.5513, 0.0404 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.1576, null, null, null, null, null, null, 0.9784, 0.1515, null },
                DecimalMember = 0.527072219467134m,
                DecimalArrayMember = new decimal[] { 0.104125190896965m, 0.339941646845951m, 0.736936218250721m, 0.092334905062662m, 0.0548505040832211m, 0.401174610044545m, 0.185053228841635m, 0.875888829212466m, 0.47570908632416m, 0.665144457132194m },
                NullableDecimalMember = 0.258049393569396m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.951675098657308m, null, 0.197125408193338m, 0.559896584793036m, 0.69179005711404m, 0.207526229736984m, null, 0.914322572826464m, null, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-196899849),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(33927123), DateTime.Now.AddSeconds(296810082), DateTime.Now.AddSeconds(21622854), DateTime.Now.AddSeconds(-261685528), DateTime.Now.AddSeconds(12108606), DateTime.Now.AddSeconds(216531441), DateTime.Now.AddSeconds(-74761340), DateTime.Now.AddSeconds(-302382023), DateTime.Now.AddSeconds(-82546808), DateTime.Now.AddSeconds(333603199) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-12865740), DateTime.Now.AddSeconds(339823218), null, null, DateTime.Now.AddSeconds(-36813380), DateTime.Now.AddSeconds(332900563), DateTime.Now.AddSeconds(19299581), null, DateTime.Now.AddSeconds(-50188988), DateTime.Now.AddSeconds(-39099336) },
            },
            new DataConvertersTestClass {
                StringMember = "5vuf093Ljq",
                StringArrayMember = new string[] { "Um9MX,GXMj", "fHd8J2 AKu", "SBX9Bp1Uqn", "Q@@ZVDbT6l", "uZ.9Vchbyt", "TkxweZQNl\"", "$qpcvkeFsX", "3N Q9h8z61", "1nZ:$8dT!o", "eu!#u.J,uv" },
                CharMember = 'M',
                CharArrayMember = new char[] { 'u', 't', ':', '%', 'y', '"', 'h', 'r', 'g', '0' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'o', 'y', 'J', null, 'T', 'w', null, '5', null, null },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, true, false, true, false, true, false, true, false, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, true, null, null, null, true, null, true, true },
                ByteMember = 52,
                ByteArrayMember = new byte[] { 3, 123, 116, 109, 106, 71, 67, 156, 169, 165 },
                NullableByteMember = 179,
                NullableByteArrayMember = new Nullable<byte>[] { 181, 89, 152, 153, 97, null, 128, 104, null, null },
                SByteMember = 97,
                SByteArrayMember = new sbyte[] { -25, 90, -102, 11, 104, -87, -18, 73, -26, 46 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -39, -73, 54, null, -103, -57, 79, 120, null, null },
                Int16Member = -5742,
                Int16ArrayMember = new short[] { 14867, 20900, 5198, 10765, -1527, 7157, -27445, -24492, -14832, 9215 },
                NullableInt16Member = 14145,
                NullableInt16ArrayMember = new Nullable<short>[] { 26453, -16626, -24291, null, -11780, null, 16451, null, null, -14214 },
                UInt16Member = 11830,
                UInt16ArrayMember = new ushort[] { 39470, 22154, 64991, 24678, 35265, 2877, 4405, 22807, 12328, 51745 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 30609, 19343, 58003, null, null, 61432, 30567, 35967, 24779, 40185 },
                Int32Member = -647207916,
                Int32ArrayMember = new int[] { 1073071198, 833153075, -448304354, -1660503263, -1967829882, -320876142, -1675355447, -820999003, -985673386, 740933512 },
                NullableInt32Member = -182451105,
                NullableInt32ArrayMember = new Nullable<int>[] { 295549922, 75390117, null, -1923729311, -1235686706, -1166241579, -1059817784, 1144017218, -1588218330, -1729059482 },
                UInt32Member = 1892777689,
                UInt32ArrayMember = new uint[] { 547710995, 3091180737, 4173730379, 2668880881, 3860625580, 3911767594, 1114538766, 179913841, 786589807, 2803406067 },
                NullableUInt32Member = 1402162221,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2463836391, 3485929251, 3831044396, null, 1988286973, null, null, 181843560, 1385658588, null },
                Int64Member = -1468429761003006530,
                Int64ArrayMember = new long[] { -3083100844411436905, 6099680523712819337, 4191044777141668879, 697311791399256563, 715522636109760139, 3375444802038579072, -6102221688197136786, -160440460325437419, 5569466660706844769, -2300200492655646539 },
                NullableInt64Member = -7351040990778742695,
                NullableInt64ArrayMember = new Nullable<long>[] { -5681245556593382166, -442062231772091551, -4616796855867095912, -4855277866832474093, -7212895751878657095, null, 7814337861658006396, -5093223498053599849, 2340167912482754135, -4162074434025483712 },
                UInt64Member = 2591508093793668134,
                UInt64ArrayMember = new ulong[] { 4129636154340513749, 4263863715647284684, 12803431912440357636, 7154843216644357426, 8224891142341298549, 17788180307260994962, 3156112209658501235, 1347970342502281365, 3944284669788118936, 10431205707510485364 },
                NullableUInt64Member = 3752531406391773796,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 3772164223804492328, 17796814624399594371, null, 226189707605330069, null, null, 10384814910445805299, null, 16902212952038204840, null },
                SingleMember = 0.9778F,
                SingleArrayMember = new float[] { 0.7192F, 0.9899F, 0.2178F, 0.2329F, 0.7207F, 0.306F, 0.2239F, 0.3693F, 0.9752F, 0.5761F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.379F, null, 0.6475F, 0.3471F, 0.1983F, null, null, 0.6809F, null },
                DoubleMember = 0.2045,
                DoubleArrayMember = new double[] { 0.9521, 0.3033, 0.718, 0.7632, 0.7165, 0.4148, 0.3043, 0.828, 0.5195, 0.7979 },
                NullableDoubleMember = 0.6851,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.7019, 0.5544, 0.0296, 0.285, 0.5936, null, 0.4351, 0.1966, null, 0.8916 },
                DecimalMember = 0.891601121011892m,
                DecimalArrayMember = new decimal[] { 0.0141103424298018m, 0.0356068921019067m, 0.382629537626722m, 0.674860279673349m, 0.508327287421649m, 0.402373210907234m, 0.708642550645601m, 0.381521734627307m, 0.322997386317877m, 0.716651707388411m },
                NullableDecimalMember = 0.0623702594116314m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, null, 0.399807041402892m, 0.377225113607971m, null, 0.798692761413007m, 0.200598274545488m, 0.670170677910204m, 0.125630125926431m, 0.402330146341712m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(333373529),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(124324334), DateTime.Now.AddSeconds(-203198577), DateTime.Now.AddSeconds(-319700814), DateTime.Now.AddSeconds(-40470355), DateTime.Now.AddSeconds(-71433031), DateTime.Now.AddSeconds(193628562), DateTime.Now.AddSeconds(31167805), DateTime.Now.AddSeconds(-171564031), DateTime.Now.AddSeconds(-307279401), DateTime.Now.AddSeconds(-269646910) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-255693638),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-166175901), null, null, DateTime.Now.AddSeconds(58443837), null, DateTime.Now.AddSeconds(37398326), DateTime.Now.AddSeconds(12445116), DateTime.Now.AddSeconds(114018581), null, DateTime.Now.AddSeconds(72493189) },
            },
            new DataConvertersTestClass {
                StringMember = "%,F7@AJFkv",
                StringArrayMember = new string[] { "@.9J$RSXE7", "SL4\"YX,#CI", "2ToOk.fNIi", "pk4F\"hN9'U", ".IyUhROKbN", "1J:Mj6Gp!R", "PiAl,f0cMS", "5eil7B83.J", "2Dbz3Y3CG#", "xV0V$rY,m$" },
                CharMember = 'R',
                CharArrayMember = new char[] { 'p', 'P', 'V', 'o', 'U', 'N', '.', 'V', '@', 'p' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, 'm', ':', 'p', 'v', null, 'V', ':', 'X', 'm' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, false, true, false, true, true, false, false, false, false },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, true, false, false, true, true, true, true, null },
                ByteMember = 225,
                ByteArrayMember = new byte[] { 226, 115, 127, 83, 81, 244, 72, 201, 213, 207 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, 9, null, 231, 233, 116, null, null, 250, 10 },
                SByteMember = -7,
                SByteArrayMember = new sbyte[] { -121, -114, -25, 36, -124, 24, -128, 109, 36, 108 },
                NullableSByteMember = 59,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, 11, -23, null, 28, -86, null, -128, 57, -46 },
                Int16Member = 5078,
                Int16ArrayMember = new short[] { -9526, -2147, -5542, 17463, 26199, 2853, 6070, -2780, -24927, 30942 },
                NullableInt16Member = 10975,
                NullableInt16ArrayMember = new Nullable<short>[] { 15044, null, -17104, -18325, -8675, 6745, -600, 7805, 13147, -6485 },
                UInt16Member = 9802,
                UInt16ArrayMember = new ushort[] { 295, 48767, 7194, 57630, 62050, 27349, 2910, 28087, 44802, 38895 },
                NullableUInt16Member = 64290,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 41716, 10824, null, null, null, 1523, 64199, null, null, null },
                Int32Member = -1720301486,
                Int32ArrayMember = new int[] { 111916991, -906762605, 1970645774, -417176678, -677666842, 1494307772, 982625129, -13090843, -998290944, -1525844257 },
                NullableInt32Member = 1586182968,
                NullableInt32ArrayMember = new Nullable<int>[] { -187585435, -1524358507, 735034671, 113941158, null, -217995494, 1359228288, 65878790, null, 1214297565 },
                UInt32Member = 3112287998,
                UInt32ArrayMember = new uint[] { 1428763220, 759369628, 2120392094, 3061990168, 2238493210, 425461506, 2830671584, 1002807646, 2860029640, 64741889 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 240573220, null, 1527726686, 1367873883, 1998936623, 2934087201, null, 4126525145, 4042216015, null },
                Int64Member = -2828934988502291607,
                Int64ArrayMember = new long[] { -7087793572661611560, -4264805153700016151, -1051150281439002092, 2557189753530356252, -554312476722075402, -8626319661900755286, -1015469932965567551, -6173899993603036924, 4629021776355967078, -4827123054522411063 },
                NullableInt64Member = 8231287916270753331,
                NullableInt64ArrayMember = new Nullable<long>[] { null, null, 6127571060900759193, null, -6201774818294934273, null, 3292042574602003830, null, 2776933601596634572, null },
                UInt64Member = 9526056809157640264,
                UInt64ArrayMember = new ulong[] { 15838447095701824581, 16886799852261393906, 4598627464769529683, 6571990359392113513, 1614881768680360323, 15036630792548616699, 8611532360933654748, 2926785270988681333, 11027407301268662197, 14338538714168640372 },
                NullableUInt64Member = 295455583167264821,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 12836895590764755360, 13215248556830460365, 11282454923866227805, 775634398115088503, null, 13500945116516166996, null, 7888316403775313353, 13103070767041997792, null },
                SingleMember = 0.4377F,
                SingleArrayMember = new float[] { 0.1099F, 0.6791F, 0.7355F, 0.1668F, 0.5174F, 0.1828F, 0.1149F, 0.1614F, 0.9153F, 0.6718F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.0321F, 0.1936F, 0.4728F, 0.2001F, 0.5944F, null, 0.3565F, null, 0.0074F, 0.2846F },
                DoubleMember = 0.9818,
                DoubleArrayMember = new double[] { 0.8916, 0.3933, 0.0155, 0.1233, 0.2301, 0.4895, 0.1171, 0.7416, 0.1166, 0.3075 },
                NullableDoubleMember = 0.979,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.8227, 0.4289, 0.4718, null, 0.9767, null, null, null, 0.3549 },
                DecimalMember = 0.221338281600881m,
                DecimalArrayMember = new decimal[] { 0.378933926014809m, 0.639444025088472m, 0.0147145935148882m, 0.92976141343284m, 0.932631972136452m, 0.42929788794823m, 0.866878424530666m, 0.905444011776123m, 0.431674114046131m, 0.592603715376656m },
                NullableDecimalMember = 0.414725539472158m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.0328054946180721m, 0.778271493554164m, null, 0.63901108123876m, 0.523305727308368m, null, null, 0.247517061949395m, 0.544107840650656m, 0.988121134067756m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(13071163),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-57929922), DateTime.Now.AddSeconds(288485088), DateTime.Now.AddSeconds(-108185524), DateTime.Now.AddSeconds(242587329), DateTime.Now.AddSeconds(341245174), DateTime.Now.AddSeconds(-154623225), DateTime.Now.AddSeconds(46929231), DateTime.Now.AddSeconds(3977163), DateTime.Now.AddSeconds(-55751728), DateTime.Now.AddSeconds(-237597315) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(134803508),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, null, DateTime.Now.AddSeconds(47812718), null, null, DateTime.Now.AddSeconds(80913330), null, DateTime.Now.AddSeconds(-100801399), null, DateTime.Now.AddSeconds(-114909819) },
            },
            new DataConvertersTestClass {
                StringMember = "Sjm9T4k6HA",
                StringArrayMember = new string[] { "XR0D6NlsvA", "KPLK!2ugLy", "%\":ZUMOa o", "zUSzsTCAbY", "1d;xmF!cxg", "hAhi#Y;v3r", "b.!jt8vtVT", "pFe'rtZn'R", "S?0AIq\"yuC", "$RH8gpw.iC" },
                CharMember = 'A',
                CharArrayMember = new char[] { 'L', 'r', 't', 'm', 'E', '@', '%', 'x', 'd', ',' },
                NullableCharMember = 'g',
                NullableCharArrayMember = new Nullable<char>[] { 'W', 'p', null, 'y', 'q', null, null, null, null, 'Q' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, false, false, false, true, true, true, true, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, false, true, false, true, true, null, null, true },
                ByteMember = 146,
                ByteArrayMember = new byte[] { 157, 204, 34, 137, 83, 219, 142, 182, 31, 159 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, null, 163, 125, null, null, null, 81, 21, null },
                SByteMember = 33,
                SByteArrayMember = new sbyte[] { -15, 73, 99, 101, -6, 122, 44, -99, -90, 46 },
                NullableSByteMember = 43,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -37, null, null, -12, 88, null, null, null, -18, -118 },
                Int16Member = -26090,
                Int16ArrayMember = new short[] { 14073, 24705, -16048, 4374, 27344, 17318, -3726, 30215, -15184, 17272 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { 5300, -23605, null, 32227, 2682, -30021, 28744, -31531, -5605, null },
                UInt16Member = 48836,
                UInt16ArrayMember = new ushort[] { 16748, 27952, 13308, 47634, 25432, 25821, 52586, 60440, 59157, 37704 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 41180, 62192, 15309, 34760, 63586, null, 37479, null, null },
                Int32Member = -1699893034,
                Int32ArrayMember = new int[] { 652811299, 253434571, -448940469, -415067140, -1383337719, -1321479395, -1050485083, -1017768651, -1360681770, 1357317404 },
                NullableInt32Member = -1694467037,
                NullableInt32ArrayMember = new Nullable<int>[] { null, null, -1335196161, 1733545113, 1522522976, 1164437760, 77688838, 892118665, null, 436734071 },
                UInt32Member = 263516450,
                UInt32ArrayMember = new uint[] { 93894003, 3873246259, 267166772, 1171870527, 2099405805, 2294759745, 3423136133, 1313360570, 3345581894, 495630626 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2864519538, null, 1136432320, 3164794729, 485781021, 3446510576, 1807691808, 1180566712, null, null },
                Int64Member = -2920722830805215602,
                Int64ArrayMember = new long[] { -4185624076630886331, 7686805647165409249, -6850113002199040459, 8603755988433586276, -4452186138526434566, -892594086323929229, 8339977039661291705, -3386152142309482847, -4337165644150074292, 2663739865062076278 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -6669777709287661645, null, -7190988747301521161, -2498361349233196004, null, 5646899621872104752, null, null, 8597827126533105911, -6059472092057640816 },
                UInt64Member = 8047488177705777229,
                UInt64ArrayMember = new ulong[] { 14310099010026995131, 4792946697077671650, 16935188887394522884, 6499506386736008412, 15344470009175498007, 12463288403563120512, 10048235339923583943, 15878641006419098704, 873845582740069144, 14153823514825186394 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 7591138248865832726, 12525422693951150631, 17255421686792064083, 1216444708759030024, 16040857120108887057, 4105727517607378659, 2727361747366494048, null, null, null },
                SingleMember = 0.8856F,
                SingleArrayMember = new float[] { 0.65F, 0.1133F, 0.716F, 0.6218F, 0.4324F, 0.0596F, 0.3203F, 0.475F, 0.9153F, 0.6444F },
                NullableSingleMember = 0.764F,
                NullableSingleArrayMember = new Nullable<float>[] { null, 0.4368F, 0.1819F, 0.4626F, null, null, null, null, 0.1098F, 0.5854F },
                DoubleMember = 0.8525,
                DoubleArrayMember = new double[] { 0.7217, 0.1907, 0.875, 0.2738, 0.9061, 0.087, 0.3187, 0.4953, 0.8372, 0.0434 },
                NullableDoubleMember = 0.7983,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.2193, 0.1841, 0.6291, 0.5005, 0.5009, 0.2053, null, 0.082, 0.8425, null },
                DecimalMember = 0.617927181416076m,
                DecimalArrayMember = new decimal[] { 0.258921779818737m, 0.86233984255471m, 0.754347988281942m, 0.668218511103928m, 0.998001126187664m, 0.942276868522342m, 0.530412670660221m, 0.21666718909059m, 0.25106911802373m, 0.489954279640922m },
                NullableDecimalMember = 0.173998534519455m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.694110481330806m, 0.816777626522233m, 0.9845344206481m, null, 0.279880463864773m, 0.999317670929237m, 0.706082843960033m, 0.17992491768567m, 0.493605609511053m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-95361455),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(348652585), DateTime.Now.AddSeconds(-346532311), DateTime.Now.AddSeconds(-271498965), DateTime.Now.AddSeconds(4109609), DateTime.Now.AddSeconds(326196106), DateTime.Now.AddSeconds(87275440), DateTime.Now.AddSeconds(320928753), DateTime.Now.AddSeconds(-233628729), DateTime.Now.AddSeconds(-264687621), DateTime.Now.AddSeconds(-183585234) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(277602195),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(234533482), DateTime.Now.AddSeconds(-306512954), DateTime.Now.AddSeconds(-306613542), DateTime.Now.AddSeconds(-302118701), null, null, DateTime.Now.AddSeconds(-28183503), DateTime.Now.AddSeconds(126659558), DateTime.Now.AddSeconds(225186615) },
            },
            new DataConvertersTestClass {
                StringMember = "J6 rj;BB.m",
                StringArrayMember = new string[] { "2;6q9S.bV,", "V3jh\"eOTtt", ",;.%LlTvv?", "fioNXwDk7W", "TtbtSm7uTE", "K\"fYI$w mx", "NQuB,jjhne", "gaZXj$2ZU#", "Xj.kGiC6%y", "Tm5jg?AZ0P" },
                CharMember = ':',
                CharArrayMember = new char[] { '9', '@', 'e', 's', 'S', 'b', 'U', 'e', 'R', '4' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'b', null, 'c', 'W', null, null, null, null, 'N', 'o' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, false, false, true, true, false, true, true, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, true, null, null, null, false, null, true, true, null },
                ByteMember = 33,
                ByteArrayMember = new byte[] { 103, 60, 31, 183, 190, 49, 118, 93, 11, 80 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 166, 158, 58, 209, 156, null, 31, 168, 234, 222 },
                SByteMember = -73,
                SByteArrayMember = new sbyte[] { 10, 85, -8, 73, 93, -51, -109, -97, -47, -10 },
                NullableSByteMember = 60,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -98, 92, 100, 67, 51, -62, 58, 70, -36 },
                Int16Member = 32230,
                Int16ArrayMember = new short[] { 15640, 26837, -2292, -29901, 14654, 27957, -31446, 23955, 21819, -1482 },
                NullableInt16Member = 31901,
                NullableInt16ArrayMember = new Nullable<short>[] { null, null, -19771, null, null, 29433, 4195, 3529, -13995, 2282 },
                UInt16Member = 26540,
                UInt16ArrayMember = new ushort[] { 18948, 37260, 36235, 52942, 7930, 64090, 42222, 25651, 58848, 53751 },
                NullableUInt16Member = 26012,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 24295, 63464, null, 25266, null, 36750, 28403, null, 32818, null },
                Int32Member = -879523194,
                Int32ArrayMember = new int[] { 1733174075, 1903194536, -998840999, 530831428, 1664751367, -631797599, -95919704, 711952318, 1922384621, -471816053 },
                NullableInt32Member = 23102061,
                NullableInt32ArrayMember = new Nullable<int>[] { 877237011, 1856842724, 759769010, -287624669, 352546726, null, -892777012, 620895649, 1601810663, null },
                UInt32Member = 2008806016,
                UInt32ArrayMember = new uint[] { 2291860279, 2686187045, 919492994, 1149892199, 4088158311, 4189195569, 588994445, 2528244152, 3757570007, 987566449 },
                NullableUInt32Member = 929322485,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2046688712, 3356629168, null, 3841135090, 1241343228, 1254092376, null, 2577487472, 3721885263, null },
                Int64Member = -511439056552200376,
                Int64ArrayMember = new long[] { -7994080822486656597, 6033620519435286264, 1232998523567469963, 7820973889982098486, 6877456663388001365, -8548960556620840825, -5922370859849554535, -4636842708933778597, -7395202877163577812, 2511016920580182216 },
                NullableInt64Member = -5019580303549154201,
                NullableInt64ArrayMember = new Nullable<long>[] { null, -6191276713448452365, 4439383713223737961, -985288079341845776, 1142751905507030669, null, 1606835005196911711, -3861749074297048286, null, 3385998910566129227 },
                UInt64Member = 13798139736416619517,
                UInt64ArrayMember = new ulong[] { 6026530830138526585, 17078418520117618243, 13865761509530557931, 16671298126216190085, 5281721807142253665, 17918386693111473154, 9254470731370024108, 12285871607902070941, 12389521150608391566, 6955560407607109468 },
                NullableUInt64Member = 16048740523548558409,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 11493054777802010359, null, 14858700083692498631, 9450742748423281674, 10303329417582611686, 11496338473922079940, 3338686065284059275, null, 12676989436624151402, 2852702223632217071 },
                SingleMember = 0.1035F,
                SingleArrayMember = new float[] { 0.7956F, 0.3799F, 0.9306F, 0.4959F, 0.7698F, 0.0113F, 0.0985F, 0.1093F, 0.1845F, 0.7225F },
                NullableSingleMember = 0.2649F,
                NullableSingleArrayMember = new Nullable<float>[] { null, null, null, 0.0816F, 0.6152F, 0.3168F, null, 0.7557F, 0.5139F, 0.6663F },
                DoubleMember = 0.185,
                DoubleArrayMember = new double[] { 0.3723, 0.862, 0.6849, 0.7208, 0.6641, 0.7605, 0.4742, 0.1734, 0.553, 0.9326 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.2821, 0.0988, 0.8267, 0.1465, null, 0.9261, 0.3829, 0.4911, 0.4414, null },
                DecimalMember = 0.9303248681458m,
                DecimalArrayMember = new decimal[] { 0.783963237408136m, 0.224308137765639m, 0.21562220849403m, 0.93995969338091m, 0.43416278266697m, 0.634993632699467m, 0.533513652506866m, 0.676818588369945m, 0.584003819503646m, 0.914025726732496m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, 0.420660031302761m, null, 0.579717880715166m, 0.3106623043526m, null, 0.787314500473372m, 0.143275508719273m, 0.593599761289247m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(47239278),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-102034265), DateTime.Now.AddSeconds(-94864129), DateTime.Now.AddSeconds(-223685366), DateTime.Now.AddSeconds(-236949178), DateTime.Now.AddSeconds(-9812494), DateTime.Now.AddSeconds(254027074), DateTime.Now.AddSeconds(-130002004), DateTime.Now.AddSeconds(-193704084), DateTime.Now.AddSeconds(-7115665), DateTime.Now.AddSeconds(-170474452) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(260275196),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(-250544570), DateTime.Now.AddSeconds(222063590), null, DateTime.Now.AddSeconds(-224398009), DateTime.Now.AddSeconds(-123945070), DateTime.Now.AddSeconds(163456674), DateTime.Now.AddSeconds(343335236), DateTime.Now.AddSeconds(-292251419), DateTime.Now.AddSeconds(221751546) },
            },
            new DataConvertersTestClass {
                StringMember = "?;:wKJcR1.",
                StringArrayMember = new string[] { "o;$b6%!TpV", "gvdD@SfxYX", "wUAq\"cAxwp", "d%\"lj1oJeB", "T!y#5r\"pSM", "SEp%zst7uW", "'2!UD Oq5X", "@PWGWyB21W", "PpqKypa,TL", "NYy4?VeLAg" },
                CharMember = 'G',
                CharArrayMember = new char[] { 'R', 'l', ';', '$', 'B', ',', '5', ';', 'i', '@' },
                NullableCharMember = 'U',
                NullableCharArrayMember = new Nullable<char>[] { 'J', 'O', null, 'P', '.', 'P', ':', null, 'c', '2' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { true, true, true, true, false, true, true, true, false, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, true, false, null, null, null, true, true, true, null },
                ByteMember = 112,
                ByteArrayMember = new byte[] { 59, 254, 59, 251, 197, 57, 245, 217, 22, 50 },
                NullableByteMember = 2,
                NullableByteArrayMember = new Nullable<byte>[] { null, null, 251, null, 138, 189, 166, 124, 124, 204 },
                SByteMember = -80,
                SByteArrayMember = new sbyte[] { -83, -82, 39, 75, 110, -30, -28, 87, -100, 51 },
                NullableSByteMember = -110,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 117, -110, -52, null, 3, 91, 53, -70, 100, 32 },
                Int16Member = -23265,
                Int16ArrayMember = new short[] { 24469, -18441, 4596, 21913, -9784, -9151, -27784, 27969, -327, 15307 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -12369, -24580, 15295, null, -8987, 8924, 26879, -23474, null, 7853 },
                UInt16Member = 55006,
                UInt16ArrayMember = new ushort[] { 40781, 49053, 55459, 24773, 59220, 30549, 33585, 24248, 53802, 35499 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 19503, 16302, 25437, 45939, 18880, null, null, null, null, 20841 },
                Int32Member = 1155947778,
                Int32ArrayMember = new int[] { -1025083289, 1858214745, 82193808, 481738505, -57458769, -969926083, -135763922, -1076149101, 301885128, -1009943640 },
                NullableInt32Member = 1032730147,
                NullableInt32ArrayMember = new Nullable<int>[] { 1764733848, 22593722, 1420202536, 1247551061, 334337044, 1555020116, null, null, null, -2089518912 },
                UInt32Member = 283561955,
                UInt32ArrayMember = new uint[] { 577675388, 2400497228, 1446795433, 3114116946, 3922927014, 883547965, 3261892634, 2496282550, 4029977068, 505275929 },
                NullableUInt32Member = 3868276095,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1370234980, 1740690054, 600695905, null, 3140469292, 98239561, null, null, 19735249, 3645206597 },
                Int64Member = 619946181186678389,
                Int64ArrayMember = new long[] { 4981807499913199133, 2944251305158800749, 2355845959086562882, 1062139110367221989, 6427407223308090819, -6556871399765050233, -1721400384802861470, 5849942553662017759, 7258212241638850882, -3693715558715217340 },
                NullableInt64Member = -3547569941721351131,
                NullableInt64ArrayMember = new Nullable<long>[] { -8624229729589071319, null, -7040890935624763789, null, null, null, 495491686199588775, 4841062957906050602, -7314588332770068231, null },
                UInt64Member = 16610764239705353785,
                UInt64ArrayMember = new ulong[] { 16542828754409094551, 8211975668729787695, 15415910520024977650, 658912975750553706, 11418128646312666215, 1357102354439616824, 2106201833641140888, 11937722877946786754, 18207492884776082132, 6130443075652484980 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 17954213075129707460, 673841687010524581, 14381532051060825054, null, 1129770735829306085, 13754416355268843594, 11724804774306099807, 16276126595529404945, 16534268723068807885, null },
                SingleMember = 0.3338F,
                SingleArrayMember = new float[] { 0.7925F, 0.2582F, 0.6982F, 0.1683F, 0.8735F, 0.0325F, 0.7541F, 0.147F, 0.4165F, 0.3526F },
                NullableSingleMember = 0.3984F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.1755F, 0.7168F, null, 0.129F, 0.4309F, null, null, null, 0.5498F, 0.259F },
                DoubleMember = 0.907,
                DoubleArrayMember = new double[] { 0.377, 0.8634, 0.4246, 0.2985, 0.7496, 0.855, 0.7264, 0.9484, 0.816, 0.1343 },
                NullableDoubleMember = 0.5774,
                NullableDoubleArrayMember = new Nullable<double>[] { null, 0.1516, null, null, null, 0.37, 0.4205, null, null, null },
                DecimalMember = 0.651338713922099m,
                DecimalArrayMember = new decimal[] { 0.915089571104504m, 0.136307817275908m, 0.195778948504306m, 0.702995516757567m, 0.546231551699777m, 0.983855076342954m, 0.369543607387379m, 0.105325415443722m, 0.00383469393648494m, 0.965034195766801m },
                NullableDecimalMember = 0.696306718078618m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.674515597918672m, 0.69015645756104m, 0.391566508625801m, null, 0.525522711876163m, null, null, 0.0892046232767403m, 0.59408898119276m, 0.168508874502057m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(194279612),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-26606), DateTime.Now.AddSeconds(-175878437), DateTime.Now.AddSeconds(-298631616), DateTime.Now.AddSeconds(-118827373), DateTime.Now.AddSeconds(-158675159), DateTime.Now.AddSeconds(170172300), DateTime.Now.AddSeconds(281036403), DateTime.Now.AddSeconds(-73383581), DateTime.Now.AddSeconds(-303504273), DateTime.Now.AddSeconds(331246166) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(4870800),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-257484871), null, DateTime.Now.AddSeconds(114323648), null, null, DateTime.Now.AddSeconds(-182744698), DateTime.Now.AddSeconds(-17801070), DateTime.Now.AddSeconds(309681141), DateTime.Now.AddSeconds(312270144), DateTime.Now.AddSeconds(-213392878) },
            },
            new DataConvertersTestClass {
                StringMember = "%mgAwzX.R5",
                StringArrayMember = new string[] { ".9.JAmTfoR", "GOh IdCknR", "37WhNaDGWX", "fnzBs.gBMI", "u!Ef$jM3aO", "3s!Y0\"1F8m", "PLW8#TEM7o", "f:KFc0toHn", "@ifur@WQYn", "gMVZtHt0fW" },
                CharMember = 'G',
                CharArrayMember = new char[] { '8', 'C', 'c', 'L', 'k', 'o', 'L', 'q', 'W', 'V' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'S', null, '2', '5', null, 'i', '8', 'T', null, 'I' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, false, true, false, true, false, true, false, false, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, null, true, true, false, false, null, true, null },
                ByteMember = 164,
                ByteArrayMember = new byte[] { 32, 45, 122, 245, 2, 179, 116, 87, 161, 211 },
                NullableByteMember = 124,
                NullableByteArrayMember = new Nullable<byte>[] { 14, 203, 10, null, 0, 100, 229, null, 164, 228 },
                SByteMember = -26,
                SByteArrayMember = new sbyte[] { 97, -56, -90, 75, -121, -96, -17, 61, -32, 64 },
                NullableSByteMember = null,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -108, null, -50, null, null, null, 60, null, -41 },
                Int16Member = -29504,
                Int16ArrayMember = new short[] { -6972, 20781, 30063, 10070, -4687, 7384, -2748, 8349, -19481, 13031 },
                NullableInt16Member = 1500,
                NullableInt16ArrayMember = new Nullable<short>[] { 4439, null, 11822, 22712, -4619, null, null, -14627, null, 12372 },
                UInt16Member = 58864,
                UInt16ArrayMember = new ushort[] { 20886, 30731, 24313, 12381, 36941, 7642, 22166, 28751, 51761, 42915 },
                NullableUInt16Member = 9332,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 60364, 6294, 59387, 26278, null, 19484, null, 63062, 24088, null },
                Int32Member = -1925006704,
                Int32ArrayMember = new int[] { -1517195449, -216420708, -626605697, 312002779, 128543419, -275236386, 1644304886, 498089137, -271486619, 1864227523 },
                NullableInt32Member = 1564744156,
                NullableInt32ArrayMember = new Nullable<int>[] { 1810887961, -1391930344, -1432977054, 207777396, 947306992, 466952420, null, null, 1886801368, null },
                UInt32Member = 3954610128,
                UInt32ArrayMember = new uint[] { 2180231129, 1011347219, 368300656, 782215505, 4116347661, 1635049545, 3674173752, 728336868, 1563422317, 3026306919 },
                NullableUInt32Member = 2202708460,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3849730589, 943194551, null, null, null, 1736518464, 3143227868, 2995077729, 4163231770, 2703241920 },
                Int64Member = 6765794035676137872,
                Int64ArrayMember = new long[] { 4829928578031009287, -2332358457602640646, -1133353704257637295, 2637456966696042825, -6503654474515109393, 3367218108777962540, 4494638470209154783, 1826205363077156343, -1958463325423725184, 1888092448658952429 },
                NullableInt64Member = -9169433648522521853,
                NullableInt64ArrayMember = new Nullable<long>[] { null, -5204514765231529381, 6018146565067941044, 7720312799181648213, null, 4334506059206493817, null, null, null, 6115466536127079600 },
                UInt64Member = 9258169192805204616,
                UInt64ArrayMember = new ulong[] { 6913117100843762867, 14258947490295074188, 10279738354031575980, 3482926510742748942, 12791330531074947481, 17346867938414452423, 6609701099694556329, 15566650144192329669, 14047913662660559002, 18363794374175051455 },
                NullableUInt64Member = 17421739106362295965,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 12788123284463249844, 868074271177702741, 8808114150047049697, 10707521258871269240, 7368892497103708313, 4498477382156024337, 4965636977676162995, null, 13843136793509102366, 11123686393709340606 },
                SingleMember = 0.9956F,
                SingleArrayMember = new float[] { 0.735F, 0.4849F, 0.4521F, 0.9481F, 0.9227F, 0.0837F, 0.4035F, 0.5366F, 0.5107F, 0.0264F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.6509F, 0.0489F, 0.8976F, null, 0.2093F, 0.877F, 0.4274F, 0.2037F, 0.8825F, 0.785F },
                DoubleMember = 0.7626,
                DoubleArrayMember = new double[] { 0.862, 0.8453, 0.0553, 0.0632, 0.2475, 0.3766, 0.9036, 0.4299, 0.311, 0.1328 },
                NullableDoubleMember = 0.0334,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.886, 0.1089, 0.419, null, null, 0.7046, 0.775, 0.7696, 0.387, 0.9835 },
                DecimalMember = 0.992219147500052m,
                DecimalArrayMember = new decimal[] { 0.805926894311922m, 0.521965554004608m, 0.710525572478965m, 0.664878041365901m, 0.768187708239335m, 0.982373805374606m, 0.780215665088166m, 0.809715916300362m, 0.35488659958296m, 0.0570747327250075m },
                NullableDecimalMember = 0.697534493757945m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, 0.49388215069562m, null, null, 0.0417191803502879m, 0.0372917237119088m, 0.991189660614526m, 0.132979637241794m, null, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = DateTime.Now.AddSeconds(109729814),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(40111238), DateTime.Now.AddSeconds(222651414), DateTime.Now.AddSeconds(-27831175), DateTime.Now.AddSeconds(278496595), DateTime.Now.AddSeconds(172253618), DateTime.Now.AddSeconds(-270171111), DateTime.Now.AddSeconds(-178330941), DateTime.Now.AddSeconds(173419034), DateTime.Now.AddSeconds(-59296638), DateTime.Now.AddSeconds(347221639) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-75657449),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-192376086), DateTime.Now.AddSeconds(32346730), DateTime.Now.AddSeconds(119044475), DateTime.Now.AddSeconds(181487496), DateTime.Now.AddSeconds(-233660848), null, DateTime.Now.AddSeconds(226739759), null, DateTime.Now.AddSeconds(123304665), DateTime.Now.AddSeconds(326404832) },
            },
            new DataConvertersTestClass {
                StringMember = "9Zii; #szr",
                StringArrayMember = new string[] { "\"j c8tffkw", "H?d0JKdvjd", "o 532425;U", "VhfRgdl\"wU", "MC9T?Nq3#;", "q2HL0jTh7p", "9' exG?yoX", "A;9;RVNed!", " HcVnjtBk$", "!ceH0$chND" },
                CharMember = 'n',
                CharArrayMember = new char[] { ' ', 'y', 'a', 'X', ',', 'j', '#', '7', 'c', 'b' },
                NullableCharMember = 'S',
                NullableCharArrayMember = new Nullable<char>[] { null, 'l', 'c', 'K', null, null, 'R', null, 'N', 'X' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, true, true, true, false, false, true, true, false, false },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, true, null, true, null, true, null, null, false, true },
                ByteMember = 140,
                ByteArrayMember = new byte[] { 110, 52, 168, 250, 222, 200, 223, 122, 38, 70 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, null, 82, 101, 54, 45, 132, 13, null, 236 },
                SByteMember = -67,
                SByteArrayMember = new sbyte[] { -8, -18, 107, -103, 83, -119, 38, -102, -77, -82 },
                NullableSByteMember = -5,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, 80, 45, 41, -35, 102, 92, 37, -124, 76 },
                Int16Member = -20187,
                Int16ArrayMember = new short[] { 11175, 20242, 4595, -24967, -3538, -6513, -102, 25835, -3825, 31827 },
                NullableInt16Member = 25209,
                NullableInt16ArrayMember = new Nullable<short>[] { null, 1694, -31868, 22544, null, -28035, 31087, -4169, 32397, null },
                UInt16Member = 45227,
                UInt16ArrayMember = new ushort[] { 59958, 52509, 48988, 62873, 28136, 47028, 60539, 57353, 465, 30166 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, null, null, 40428, 63735, null, 1924, null, 26840, 18065 },
                Int32Member = 1501103242,
                Int32ArrayMember = new int[] { 727749348, 1583600357, -1923313072, 1556026185, 1899306165, 488679090, 1184280133, 800966537, -1590548557, -723083463 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { 2115908407, -1532532, -1018749763, 1047121078, null, -263958129, null, 192848175, -190957088, null },
                UInt32Member = 1226577885,
                UInt32ArrayMember = new uint[] { 3919250469, 3163076183, 2870999854, 3207848716, 1588194530, 1059504198, 894580347, 1560130079, 3733994454, 1103123444 },
                NullableUInt32Member = 403636285,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 349137189, 3960250691, 1108006277, 3219973300, null, 2714478636, 2578669805, 1399639290, 3063915382, null },
                Int64Member = -1776734304106162350,
                Int64ArrayMember = new long[] { 2453482353175379698, 1317070995453811008, -3403414149326766735, -1255146363973176081, 6802888558099832427, -6108137427146998790, -5631456520042618220, -2053455955508292402, -3599432428470673132, -1213448396451131539 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -8680275934186058936, 5663202575006105965, -5223633944808525272, 985054876677866054, -351298487342464230, -5996991375635992564, 1464074397500565870, 6241331124625840991, -6717658493223868053, -4438995121694838817 },
                UInt64Member = 18344705587213732021,
                UInt64ArrayMember = new ulong[] { 6425671749120952519, 9166298444419767267, 8872382067715204935, 1610234215499861293, 13864842687026878508, 12574663783242776241, 18121808092077834846, 1731438076946538616, 16192930685725892267, 8415350589741641828 },
                NullableUInt64Member = 14270146819469472026,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 7250424267724213146, 3402339178902000506, 6823774992121118167, 2610612219252601975, 756650673433808762, 1988671291444219821, 1820590250031946476, 11486135283477319529, 18017824285873096586, null },
                SingleMember = 0.4466F,
                SingleArrayMember = new float[] { 0.0781F, 0.3502F, 0.5399F, 0.2806F, 0.9326F, 0.8239F, 0.6043F, 0.2908F, 0.8744F, 0.6798F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.6773F, 0.6734F, null, null, 0.2796F, 0.7776F, 0.0422F, 0.5972F, 0.4249F, null },
                DoubleMember = 0.1187,
                DoubleArrayMember = new double[] { 0.1722, 0.8181, 0.8014, 0.6177, 0.8515, 0.6732, 0.3906, 0.7774, 0.6531, 0.22 },
                NullableDoubleMember = 0.5505,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.0377, null, 0.0077, 0.9974, 0.5381, null, 0.5973, 0.5267, null, null },
                DecimalMember = 0.888972769657547m,
                DecimalArrayMember = new decimal[] { 0.272476839474538m, 0.362239054031257m, 0.816658712581277m, 0.0372597294113091m, 0.166131451893456m, 0.379128777481114m, 0.551702282949629m, 0.500921090791771m, 0.380294392055736m, 0.334257788097824m },
                NullableDecimalMember = 0.545668391408334m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.561500180945005m, 0.883819542767986m, 0.284312990924918m, 0.699309935567922m, 0.589568548178172m, null, 0.86346560977136m, 0.469030040929229m, 0.305736401177605m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(152619094),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-70590843), DateTime.Now.AddSeconds(-185006113), DateTime.Now.AddSeconds(-316786526), DateTime.Now.AddSeconds(73962804), DateTime.Now.AddSeconds(310278898), DateTime.Now.AddSeconds(300077494), DateTime.Now.AddSeconds(-85632902), DateTime.Now.AddSeconds(-187052365), DateTime.Now.AddSeconds(273323515), DateTime.Now.AddSeconds(347512035) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(3656265), null, DateTime.Now.AddSeconds(46723376), DateTime.Now.AddSeconds(97548231), null, DateTime.Now.AddSeconds(-572621), DateTime.Now.AddSeconds(343752722), DateTime.Now.AddSeconds(-151453530), null, DateTime.Now.AddSeconds(253783613) },
            },
        };
    }
}
