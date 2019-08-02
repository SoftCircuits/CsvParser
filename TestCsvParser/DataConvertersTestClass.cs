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

            // Verify same type
            Type type = a.GetType();
            if (type != b.GetType())
            {
                Debug.Assert(false);
                return false;
            }

            // Value types
            if (type.IsValueType)
                return (a is DateTime) ? DateTimesAreEqual((DateTime)a, (DateTime)b) : a.Equals(b);

            // Hande arrays
            if (type.IsArray)
            {
                Array aArray = (Array)a;
                Array bArray = (Array)b;

                if (aArray.Length != bArray.Length)
                    return false;
                for (int i = 0; i <  aArray.Length; i++)
                {
                    if (!ObjectsAreEqual(aArray.GetValue(i), bArray.GetValue(i)))
                        return false;
                }
                return true;
            }

            // Non-null reference types
            return (a.Equals(b));
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
                StringMember = (string)"LcQTfy\"jO ",
                StringArrayMember = new string[] { "zzd,2evWzE", "52i#:2ojEI", "I6pp@b6qye", "KtWljt: Wv", "c?\"YGg!Ucz", "JheG:\"K,\"a", "vEvV!Mf#jU", ".pMf2T;Qim", "glO%UzOQtq", "iCF@AYQue\"" },
                CharMember = (char)'N',
                CharArrayMember = new char[] { 'v', 'T', 'o', 'u', '#', 'X', 'T', ';', 'n', 'V' },
                NullableCharMember = (char?)'U',
                NullableCharArrayMember = new char?[] { null, 'Q', 'R', null, null, 'B', '.', 'O', 'L', 'M' },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, true, true, true, false, true, true, true, true, false },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { false, true, null, null, true, false, true, null, false, false },
                ByteMember = (byte)51,
                ByteArrayMember = new byte[] { 40, 138, 29, 88, 154, 137, 201, 151, 90, 232 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { 28, null, 236, 100, null, 128, null, 188, null, 174 },
                SByteMember = (sbyte)-47,
                SByteArrayMember = new sbyte[] { -112, 19, -106, -60, 0, -30, -1, 15, -73, 14 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { -20, -58, null, null, null, 24, -51, null, -11, 6 },
                Int16Member = (short)24904,
                Int16ArrayMember = new short[] { 11615, -21412, 28746, 3210, -22754, -2255, 28209, -24320, -24489, -24927 },
                NullableInt16Member = (short?)9918,
                NullableInt16ArrayMember = new short?[] { 25760, -1858, 28836, -30974, 10674, -32102, -16574, -27229, null, -22232 },
                UInt16Member = (ushort)36805,
                UInt16ArrayMember = new ushort[] { 11845, 39768, 41955, 26612, 2447, 25331, 25929, 5241, 33983, 36112 },
                NullableUInt16Member = (ushort?)64174,
                NullableUInt16ArrayMember = new ushort?[] { null, 51930, null, 55633, 61386, 46091, 11822, 5102, null, null },
                Int32Member = (int)1966835213,
                Int32ArrayMember = new int[] { -1887510264, 420139494, 262730710, 982932154, -321674667, -1785439253, 1341116347, -63071725, 781226401, 527277553 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { 1720472358, null, null, -1301669544, -380785271, null, 990297058, -903254357, -1271548087, null },
                UInt32Member = (uint)1693801044,
                UInt32ArrayMember = new uint[] { 3172891567, 3678123627, 2634106716, 975820953, 436785423, 1977286186, 1821838663, 3613038456, 1694574014, 111868676 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 3820879997, 168925643, 2956646994, 509507240, 703382626, null, 339067863, 2171626242, null, 3536566883 },
                Int64Member = (long)-4706919252393460191,
                Int64ArrayMember = new long[] { -2357361133594663873, -4191162468870670952, 4472130846170220889, 3751208214761002942, 3511663777215751987, 2489304715077756301, 823949403013045801, -6782376387514627821, 7650289145079975847, -735774179653133490 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { -9099257415328980746, 7514612652007079184, -4501327263778684347, 3630858135680500549, null, null, null, 5243505097357332626, 1551499619989061349, 5865303493246824296 },
                UInt64Member = (ulong)130754940541926783,
                UInt64ArrayMember = new ulong[] { 487608831302079215, 10945369299986414472, 17875778899929647796, 16702332339313479367, 11532589205113882850, 13540748923762609336, 7552179015352715544, 17167914233116238207, 13316938979526983728, 6886173050457102782 },
                NullableUInt64Member = (ulong?)8730256084179942800,
                NullableUInt64ArrayMember = new ulong?[] { 12775315316200154400, null, null, 6752131186661935568, 9561837398029034880, 11863222065440648, 15220313312712151938, 7517011785057927722, null, null },
                SingleMember = (float)0.2880054F,
                SingleArrayMember = new float[] { 0.4000143F, 0.3451011F, 0.9901711F, 0.2015283F, 0.9697858F, 0.9305506F, 0.3975879F, 0.2507171F, 0.5066652F, 0.5651379F },
                NullableSingleMember = (float?)0.1101993F,
                NullableSingleArrayMember = new float?[] { 0.6685213F, 0.1715821F, 0.3444352F, 0.02120287F, 0.6508704F, null, 0.1908554F, null, 0.4906643F, 0.9258217F },
                DoubleMember = (double)0.29378024,
                DoubleArrayMember = new double[] { 0.49639171, 0.3284322, 0.56359254, 0.71123232, 0.85001254, 0.10797785, 0.74588469, 0.95714457, 0.18797465, 0.12156228 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.86934567, null, 0.98939931, 0.3671491, null, null, null, 0.21801728, 0.88706809, 0.01169138 },
                DecimalMember = (decimal)0.813328942662724m,
                DecimalArrayMember = new decimal[] { 0.0220591360805832m, 0.522014852856293m, 0.0854310929241735m, 0.392808261976022m, 0.739783290652457m, 0.946941783627002m, 0.0234649451558781m, 0.0984851937268326m, 0.818513027307816m, 0.713079356920477m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { null, 0.55013296173426m, 0.235856160631336m, 0.784394776348208m, null, null, null, 0.407014165728825m, 0.297205536299015m, 0.321539369095834m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-166292061),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(26627998), DateTime.Now.AddSeconds(130619281), DateTime.Now.AddSeconds(231645922), DateTime.Now.AddSeconds(-143906792), DateTime.Now.AddSeconds(-137018583), DateTime.Now.AddSeconds(50565239), DateTime.Now.AddSeconds(122598613), DateTime.Now.AddSeconds(340777043), DateTime.Now.AddSeconds(-261755223), DateTime.Now.AddSeconds(-27155523) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { null, DateTime.Now.AddSeconds(-196049031), DateTime.Now.AddSeconds(280439535), DateTime.Now.AddSeconds(285338622), DateTime.Now.AddSeconds(-233235974), DateTime.Now.AddSeconds(299118080), null, DateTime.Now.AddSeconds(-320945944), null, DateTime.Now.AddSeconds(89928780) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"A$7H?rV%bu",
                StringArrayMember = new string[] { "D!ce!J;wAS", "$Pa\"98jQfd", "CS9oli1HJ0", "dQ,%mnwyB:", "hqtL@8u:UD", "IFq$s7I,\"i", "f6nX,4abdV", "L7Oo#ivxtz", "..9FsYx:d2", "f85pixDWXN" },
                CharMember = (char)'3',
                CharArrayMember = new char[] { 'P', 'm', 'C', 'Q', 'b', 'R', 'E', 'I', '5', '!' },
                NullableCharMember = (char?)'V',
                NullableCharArrayMember = new char?[] { null, 'U', 'K', 'Z', '!', null, null, 'L', null, null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, false, true, true, true, false, true, false, true, true },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { null, true, null, null, false, null, null, true, false, true },
                ByteMember = (byte)64,
                ByteArrayMember = new byte[] { 180, 108, 137, 33, 135, 34, 145, 161, 109, 219 },
                NullableByteMember = (byte?)129,
                NullableByteArrayMember = new byte?[] { null, 226, null, null, null, null, 129, 180, 40, 178 },
                SByteMember = (sbyte)-45,
                SByteArrayMember = new sbyte[] { -85, -70, -64, -98, 40, 10, 30, -61, -87, -90 },
                NullableSByteMember = (sbyte?)-23,
                NullableSByteArrayMember = new sbyte?[] { -95, null, -89, null, null, 42, -31, -66, -49, -67 },
                Int16Member = (short)1177,
                Int16ArrayMember = new short[] { 1962, 1696, 3072, 16102, 5324, 518, 27840, -6353, -4375, 11053 },
                NullableInt16Member = (short?)-31204,
                NullableInt16ArrayMember = new short?[] { null, null, -12548, -12228, null, -8084, null, null, null, -29155 },
                UInt16Member = (ushort)2199,
                UInt16ArrayMember = new ushort[] { 1310, 50005, 21638, 42807, 13462, 62961, 55561, 22630, 12093, 38666 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { null, null, 21927, null, 39673, null, 53518, 47495, 37971, null },
                Int32Member = (int)155268235,
                Int32ArrayMember = new int[] { -391376044, 1238532538, -505280431, 1029097735, -1645586077, 2022897632, 895553534, 2029880062, 58712076, -1169436107 },
                NullableInt32Member = (int?)-417260447,
                NullableInt32ArrayMember = new int?[] { null, 1786175470, 1546167113, null, null, 1268588459, null, 1227347305, 806195207, -1871586434 },
                UInt32Member = (uint)2245561446,
                UInt32ArrayMember = new uint[] { 3293637604, 3880551516, 347690728, 4108191125, 1542905045, 801732431, 2735927752, 939326546, 2298127123, 2413363078 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { null, 566609690, 2315048923, 577517180, 2826005499, null, null, null, 1443557888, 2170946517 },
                Int64Member = (long)7852064996710097793,
                Int64ArrayMember = new long[] { 8773968202821579187, -2711855710294745111, 3575017401877418780, -2638468260576900757, 203795336382888608, -812920648884724501, -5834069835048987194, -6818412199868657900, 1518563623437920508, 9094607534439119743 },
                NullableInt64Member = (long?)-5735204167500965321,
                NullableInt64ArrayMember = new long?[] { 287325237654206530, null, -6147413208960149484, 5117141569719980292, null, -5486858494098246966, 4137160376465296626, null, -5189646479685176959, -6967751014375667651 },
                UInt64Member = (ulong)18356420049631150034,
                UInt64ArrayMember = new ulong[] { 3645815698283835146, 3576046286317210633, 2400225921719665031, 2059266953736689131, 4788309949865225105, 2877419857664553644, 13419735476991946463, 995098004899864288, 624306532204555577, 4900982437952652941 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { null, 14156272778664029556, 17315411906724788669, null, 12385683166609073067, 16094422877570112278, 1462474946089053026, 6626096957345246520, null, 5525436819715495270 },
                SingleMember = (float)0.2677146F,
                SingleArrayMember = new float[] { 0.7782391F, 0.7408843F, 0.5115841F, 0.1913269F, 0.8316565F, 0.1818667F, 0.5296897F, 0.04731629F, 0.9773519F, 0.5576525F },
                NullableSingleMember = (float?)0.21534F,
                NullableSingleArrayMember = new float?[] { null, 0.7557985F, 0.8626265F, null, 0.03914514F, null, 0.1876747F, 0.7491438F, 0.2974963F, 0.342822F },
                DoubleMember = (double)0.31343916,
                DoubleArrayMember = new double[] { 0.67331994, 0.10143694, 0.39599376, 0.46280328, 0.20714861, 0.93035515, 0.13301291, 0.49718527, 0.8563685, 0.18592245 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.65698604, 0.51302294, 0.37732785, 0.74203133, 0.05031039, null, null, 0.71027326, null, 0.37433785 },
                DecimalMember = (decimal)0.631060790098766m,
                DecimalArrayMember = new decimal[] { 0.360849170182296m, 0.271159285805728m, 0.176608309231982m, 0.913324907381704m, 0.191017133272727m, 0.775926160521771m, 0.603008720373273m, 0.843489740436659m, 0.0091305356515248m, 0.596292449439081m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.0550077012996225m, null, 0.0989495488344457m, 0.950151309813443m, 0.38323566288838m, 0.373853279451771m, 0.11560309590567m, 0.600824342854705m, 0.0475380243954891m, 0.20539253121493m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-173170434),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-300020650), DateTime.Now.AddSeconds(321583395), DateTime.Now.AddSeconds(-71828114), DateTime.Now.AddSeconds(191864530), DateTime.Now.AddSeconds(-333939051), DateTime.Now.AddSeconds(-48859658), DateTime.Now.AddSeconds(-121238089), DateTime.Now.AddSeconds(185768886), DateTime.Now.AddSeconds(-89536829), DateTime.Now.AddSeconds(-250544185) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-348800691), null, null, DateTime.Now.AddSeconds(67984318), DateTime.Now.AddSeconds(112046232), null, DateTime.Now.AddSeconds(-49344102), null, null, null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"iTk%'X;BBK",
                StringArrayMember = new string[] { "MhRQur$i2m", "Hj$y;ZCwFV", "fZZfQrZ,MV", "iv;Tp$qo!:", "0sq8G V9Hp", "UQ#229I9AD", "SIVhEjs1I\"", "5l1Os95oZR", "Vn3tARyKo3", "?3n:vrXJ4t" },
                CharMember = (char)'k',
                CharArrayMember = new char[] { 'g', 'B', 'M', 'O', '1', '6', 'F', 's', 'R', 'P' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { 'U', 'x', null, '2', '4', 'k', 'b', 'W', 'z', '9' },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { false, true, true, false, false, false, true, true, true, true },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { false, true, true, null, false, false, null, false, false, true },
                ByteMember = (byte)130,
                ByteArrayMember = new byte[] { 188, 96, 211, 121, 217, 136, 226, 237, 95, 122 },
                NullableByteMember = (byte?)248,
                NullableByteArrayMember = new byte?[] { 85, null, 39, 218, null, 106, 237, 203, 189, 74 },
                SByteMember = (sbyte)-78,
                SByteArrayMember = new sbyte[] { 77, -66, 91, -84, -29, 124, -50, -12, -76, 96 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { -78, null, 88, null, 118, 59, 64, 39, 87, 94 },
                Int16Member = (short)-18852,
                Int16ArrayMember = new short[] { 15550, 12989, 26166, -26441, 31801, -658, -32627, -24796, 3992, -21481 },
                NullableInt16Member = (short?)-26813,
                NullableInt16ArrayMember = new short?[] { null, 13195, 15576, -21860, null, -22208, 26822, -26973, null, 21358 },
                UInt16Member = (ushort)46408,
                UInt16ArrayMember = new ushort[] { 50134, 9506, 7765, 54933, 54881, 44799, 38661, 7157, 29232, 44334 },
                NullableUInt16Member = (ushort?)27083,
                NullableUInt16ArrayMember = new ushort?[] { null, 56877, null, null, null, 4612, 40343, 35888, 50276, null },
                Int32Member = (int)1433587063,
                Int32ArrayMember = new int[] { 801838984, -2128431256, -1700731135, 374150726, -1524977283, 618229388, -2119505383, -579624176, -1212004436, 1869363775 },
                NullableInt32Member = (int?)1839728336,
                NullableInt32ArrayMember = new int?[] { 1519282034, null, null, 1025099981, -1592951531, -289246115, 1121359187, null, null, null },
                UInt32Member = (uint)1739586613,
                UInt32ArrayMember = new uint[] { 3350515715, 2691891323, 1703767447, 510461029, 3230631217, 1395573830, 2233886852, 1862335280, 2599991667, 3944341692 },
                NullableUInt32Member = (uint?)2089117920,
                NullableUInt32ArrayMember = new uint?[] { 1217843464, 569273776, 306340257, null, null, null, null, 195883550, 2758496609, null },
                Int64Member = (long)-5244920706514507356,
                Int64ArrayMember = new long[] { 3351728375847123542, 5441028562209348653, -4131055206962628084, -2010262708950825436, -9102446632654465972, 4177094518129268676, -3852261212264738521, 1186404887864618831, 7946832150328465665, 2193649085740610637 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { -2050747456736729497, 4630299442652686291, -2152858574798363126, null, null, null, 6117453502057797566, null, -7032285299091711993, 6706439451334486006 },
                UInt64Member = (ulong)10223403187567699520,
                UInt64ArrayMember = new ulong[] { 2964347681400792761, 18077128433540628137, 13477222411920747095, 587078802346833032, 16982472445433120157, 7188896181165676402, 156794051208058568, 17229282813025708702, 8882383747072869721, 8267767980723768445 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 16454061412031101092, 12365684719606544843, null, 12163764832997699009, 11678053034678328278, null, 15979467335923296664, null, 7798527391111287244, 11462521646768646375 },
                SingleMember = (float)0.6881294F,
                SingleArrayMember = new float[] { 0.9069236F, 0.5394772F, 0.5808434F, 0.2974233F, 0.3440702F, 0.3693151F, 0.4690504F, 0.4771944F, 0.1239078F, 0.9897153F },
                NullableSingleMember = (float?)0.2971188F,
                NullableSingleArrayMember = new float?[] { null, null, null, null, 0.5923094F, null, null, null, 0.6447862F, 0.6578512F },
                DoubleMember = (double)0.11041492,
                DoubleArrayMember = new double[] { 0.50449484, 0.82416423, 0.00144285, 0.09628648, 0.43244553, 0.28382463, 0.08653771, 0.72556852, 0.19709882, 0.87438489 },
                NullableDoubleMember = (double?)0.26640626,
                NullableDoubleArrayMember = new double?[] { 0.13303081, 0.88490044, null, 0.49237932, null, null, 0.55710255, 0.73035238, 0.13813788, null },
                DecimalMember = (decimal)0.0956027256770072m,
                DecimalArrayMember = new decimal[] { 0.99046496534276m, 0.625402414531169m, 0.392404155057112m, 0.737636023544537m, 0.547353741502088m, 0.536471047688495m, 0.551812932152214m, 0.223050134360348m, 0.249895040527869m, 0.947124241360987m },
                NullableDecimalMember = (decimal?)0.226725624979812m,
                NullableDecimalArrayMember = new decimal?[] { null, null, 0.535600992169045m, 0.780301934937156m, 0.806884319897222m, null, 0.938303706207454m, 0.986995890730524m, 0.0610052133263113m, 0.590192790418022m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(33420402),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(77509917), DateTime.Now.AddSeconds(-137477044), DateTime.Now.AddSeconds(145720535), DateTime.Now.AddSeconds(-172960849), DateTime.Now.AddSeconds(178323758), DateTime.Now.AddSeconds(197957817), DateTime.Now.AddSeconds(-317330168), DateTime.Now.AddSeconds(163947477), DateTime.Now.AddSeconds(-311734573), DateTime.Now.AddSeconds(1261081) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(129256987),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(202810626), null, DateTime.Now.AddSeconds(219517095), DateTime.Now.AddSeconds(-142722894), DateTime.Now.AddSeconds(149088227), null, DateTime.Now.AddSeconds(-63663943), null, null, DateTime.Now.AddSeconds(-168072919) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"dr0O76m5J'",
                StringArrayMember = new string[] { "Eqk652cyg9", "7hNcEB$.pA", "mv72.19o9R", "2\"BvyVT4I\"", "Pv0!8@dXK5", "8yPJcf65@E", "'.,OH:go05", "5P1v8;@?\":", "XleEgD0:TT", "mk9EBb7l.!" },
                CharMember = (char)'E',
                CharArrayMember = new char[] { '@', '9', 'r', 'y', '4', '5', '1', 'q', 'w', 'A' },
                NullableCharMember = (char?)'i',
                NullableCharArrayMember = new char?[] { 'f', null, null, '?', 'U', null, 'y', '@', '7', null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { false, true, true, true, true, false, true, true, false, false },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { true, true, false, null, false, false, false, false, true, true },
                ByteMember = (byte)248,
                ByteArrayMember = new byte[] { 121, 111, 15, 228, 12, 8, 194, 166, 231, 19 },
                NullableByteMember = (byte?)3,
                NullableByteArrayMember = new byte?[] { 107, null, 44, null, null, 27, 40, 81, 228, 14 },
                SByteMember = (sbyte)-29,
                SByteArrayMember = new sbyte[] { -70, 91, -123, 117, -40, -2, 13, 86, 60, -40 },
                NullableSByteMember = (sbyte?)50,
                NullableSByteArrayMember = new sbyte?[] { 30, 39, 96, -27, -18, -113, null, -4, 3, null },
                Int16Member = (short)-15749,
                Int16ArrayMember = new short[] { 6512, 30477, 9904, -23429, -8506, 13226, -16202, 27900, 21201, -18393 },
                NullableInt16Member = (short?)25078,
                NullableInt16ArrayMember = new short?[] { -11857, null, 23205, null, null, 26686, 9474, 19520, null, 6999 },
                UInt16Member = (ushort)3760,
                UInt16ArrayMember = new ushort[] { 42531, 50416, 8818, 25633, 47514, 22795, 60732, 16168, 9973, 24709 },
                NullableUInt16Member = (ushort?)41628,
                NullableUInt16ArrayMember = new ushort?[] { 38488, 17638, 50487, 5335, 56302, 21689, 43127, null, 17224, 28330 },
                Int32Member = (int)-1238275908,
                Int32ArrayMember = new int[] { -213747581, 699174837, 2041151199, 1643062680, 883449733, 326346666, -2117040904, -1341370871, 93515788, -826495542 },
                NullableInt32Member = (int?)-1434447975,
                NullableInt32ArrayMember = new int?[] { 1213537597, 217890908, 821702405, 1905270826, null, -1763077464, 292346530, 1188850162, null, -256944448 },
                UInt32Member = (uint)3601423205,
                UInt32ArrayMember = new uint[] { 4015284371, 38841320, 4189755269, 285711707, 416051448, 3327983198, 1174333123, 3803976649, 2997783287, 4035509636 },
                NullableUInt32Member = (uint?)1343515932,
                NullableUInt32ArrayMember = new uint?[] { null, 3784467379, 2507640975, null, 195887404, 30989169, null, 3908799573, 1217413885, 3779780841 },
                Int64Member = (long)1547639125319585449,
                Int64ArrayMember = new long[] { -5698077653875826480, 8778002847165079126, 6411087691564941304, -8687160965944626566, -7357655537477591052, -1276441277470227397, 994094529210069436, 6165468715124509416, -7191011908991771155, 5687254700399985306 },
                NullableInt64Member = (long?)-7766227371707987635,
                NullableInt64ArrayMember = new long?[] { null, -2708857904747408517, null, 4085464273782253972, null, 946069091259047095, null, -8569214600856461127, -5682324377973711792, -7003696323683754453 },
                UInt64Member = (ulong)2277200845859996168,
                UInt64ArrayMember = new ulong[] { 13655733040295188145, 10190898448874985473, 285556059609005220, 5958820157139310798, 18324046125942105788, 12716704959423848549, 3559377290771506901, 459903921260353824, 10209824943739761190, 18318983672688244015 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 8467130727038924229, 13106248412687994499, 6011876097078016840, 2460551485537491405, 3232805791487038226, 13070402375268017636, null, 15831167769832439685, 11732039046919619684, 12504941770955783435 },
                SingleMember = (float)0.6699244F,
                SingleArrayMember = new float[] { 0.5532096F, 0.5064929F, 0.4421831F, 0.06343994F, 0.8843704F, 0.5190169F, 0.7044942F, 0.2892224F, 0.6507289F, 0.266226F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { 0.7562952F, null, null, null, null, 0.8350811F, 0.7720764F, 0.5278801F, 0.728942F, 0.5756159F },
                DoubleMember = (double)0.44452929,
                DoubleArrayMember = new double[] { 0.75253002, 0.7822327, 0.08799015, 0.75867506, 0.38677035, 0.35187366, 0.36175822, 0.4808058, 0.65160436, 0.22454051 },
                NullableDoubleMember = (double?)0.42648523,
                NullableDoubleArrayMember = new double?[] { 0.18022903, null, 0.01493274, 0.40215126, 0.06656404, 0.62965085, null, 0.76431506, 0.26706168, 0.3659341 },
                DecimalMember = (decimal)0.501115223160533m,
                DecimalArrayMember = new decimal[] { 0.555470695977784m, 0.728489053309192m, 0.803738004436594m, 0.500689723762073m, 0.403589595762822m, 0.47746068773673m, 0.662603546242511m, 0.615547557648061m, 0.64686543524585m, 0.562478501145951m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.494686540446564m, null, 0.14712765447196m, 0.725262880663044m, null, 0.0894765132523498m, 0.806936070233088m, null, 0.985728221007496m, 0.716999987474177m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), Guid.NewGuid(), null, null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-238072510),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(99323452), DateTime.Now.AddSeconds(-138922281), DateTime.Now.AddSeconds(-333246889), DateTime.Now.AddSeconds(-260050587), DateTime.Now.AddSeconds(-17359454), DateTime.Now.AddSeconds(223630425), DateTime.Now.AddSeconds(-333761046), DateTime.Now.AddSeconds(-309951650), DateTime.Now.AddSeconds(-266575981), DateTime.Now.AddSeconds(-78867943) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-347248881),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-53940133), DateTime.Now.AddSeconds(122762672), DateTime.Now.AddSeconds(283378917), DateTime.Now.AddSeconds(151398600), DateTime.Now.AddSeconds(81138802), DateTime.Now.AddSeconds(-295858332), null, DateTime.Now.AddSeconds(-237282869), null, DateTime.Now.AddSeconds(215969414) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"m,ub s#qXl",
                StringArrayMember = new string[] { "k\",E3dg65D", "NpVhTUdxuh", "GgC ci:CF.", "GJq46sw5Xu", "!A263#qNI1", "PW#;eD;,yV", "md1@#D%oxx", "vvZ mG1TSf", "'1!au7N0Z3", "27GCInV790" },
                CharMember = (char)'f',
                CharArrayMember = new char[] { '$', 'c', 'k', 'f', 'K', 'f', 'M', 'z', 'h', '"' },
                NullableCharMember = (char?)'R',
                NullableCharArrayMember = new char?[] { 'J', 'I', '.', 'a', 'B', 'P', null, null, 'L', '2' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, true, false, false, true, true, false, false, false, false },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { true, null, false, true, false, true, false, null, null, false },
                ByteMember = (byte)202,
                ByteArrayMember = new byte[] { 36, 53, 21, 129, 175, 137, 1, 111, 115, 44 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { null, 118, 103, 130, null, 145, 55, 220, 70, 146 },
                SByteMember = (sbyte)-92,
                SByteArrayMember = new sbyte[] { 123, 21, 79, 38, 66, 125, -69, -89, -45, -54 },
                NullableSByteMember = (sbyte?)-93,
                NullableSByteArrayMember = new sbyte?[] { null, -119, null, 53, -92, -72, null, 75, 116, null },
                Int16Member = (short)9012,
                Int16ArrayMember = new short[] { -26141, -13690, 11291, -8510, -18311, -14396, 14814, -51, -1941, 31346 },
                NullableInt16Member = (short?)-9640,
                NullableInt16ArrayMember = new short?[] { null, -6568, 4936, 31277, null, null, -28898, null, 24821, 15978 },
                UInt16Member = (ushort)33182,
                UInt16ArrayMember = new ushort[] { 4944, 903, 27888, 8923, 8250, 33362, 41029, 55396, 20897, 59674 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 9563, 5235, 33468, 39130, 50024, null, 28669, null, 32537, 17958 },
                Int32Member = (int)11985679,
                Int32ArrayMember = new int[] { 260813723, 243235229, -1969967439, -1172197013, 1157337317, -367147502, 962592267, -983758910, 620489180, -1748055273 },
                NullableInt32Member = (int?)-1322403868,
                NullableInt32ArrayMember = new int?[] { -181072192, -311302017, -1643929195, 1370115141, -55147859, 727958042, null, 1055013103, null, -589431604 },
                UInt32Member = (uint)1405563190,
                UInt32ArrayMember = new uint[] { 2920878950, 1116131830, 956031760, 3291862904, 216401166, 70620083, 541202619, 1057106275, 3181085716, 697673429 },
                NullableUInt32Member = (uint?)2506091673,
                NullableUInt32ArrayMember = new uint?[] { 2259358461, 2696563504, 3740472541, null, null, null, null, 1022567612, 3161417711, null },
                Int64Member = (long)92423929433690138,
                Int64ArrayMember = new long[] { 2983375012692325504, -5827267336151960117, -228636649634844566, 4701321384682199570, 1669774260707827213, -5304559267960523402, -3975389319864977938, 6360317587177689248, 3447577874688912310, -6374427994068498172 },
                NullableInt64Member = (long?)-3856832399812913865,
                NullableInt64ArrayMember = new long?[] { -2544230382206351553, -2567019159141148857, -1228429614615585705, null, 5837232271167608949, -8123690364777670782, 3324245319965829199, 7375988008297152538, 6414758278600015075, -7061030120041212371 },
                UInt64Member = (ulong)1981536263389639785,
                UInt64ArrayMember = new ulong[] { 6920522119190703261, 16449094153292924971, 18407939313274386114, 3434014467337324314, 7581867388450934569, 9129445708464022386, 8851219420381380525, 1903134165328697846, 10280626333622127391, 14735484329047310782 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 4807962063411541777, 6997742974902526111, 16719137886947984955, 4160436027233766898, 1004280394931321831, 10505090131273926705, 14387344798432658275, 3785537427017925934, 15321603953885673967, 6361322724488186266 },
                SingleMember = (float)0.7159284F,
                SingleArrayMember = new float[] { 0.3083348F, 0.9551604F, 0.8946911F, 0.6941669F, 0.6395721F, 0.6741459F, 0.8161055F, 0.9625575F, 0.03501124F, 0.0604679F },
                NullableSingleMember = (float?)0.6850471F,
                NullableSingleArrayMember = new float?[] { null, 0.8128214F, 0.801034F, 0.8360142F, 0.09587389F, 0.5367697F, null, 0.3451066F, 0.8290852F, 0.1285698F },
                DoubleMember = (double)0.16931677,
                DoubleArrayMember = new double[] { 0.14568675, 0.4529148, 0.28103923, 0.4445688, 0.09215219, 0.85100037, 0.5077312, 0.89819384, 0.78115715, 0.31476028 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.57072455, 0.18254569, 0.50154082, 0.30349754, 0.49778256, 0.84232739, null, null, 0.73800447, 0.62245529 },
                DecimalMember = (decimal)0.750640132814012m,
                DecimalArrayMember = new decimal[] { 0.229717555562834m, 0.475465834362184m, 0.505417924609696m, 0.632857343942326m, 0.748779817367336m, 0.391653545383203m, 0.141259905016636m, 0.422987620543217m, 0.138651198772085m, 0.986750619479804m },
                NullableDecimalMember = (decimal?)0.872997445926535m,
                NullableDecimalArrayMember = new decimal?[] { null, 0.745586115748429m, 0.619953051032477m, null, null, null, 0.00345400860693958m, null, null, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-234793680),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(107396628), DateTime.Now.AddSeconds(200742751), DateTime.Now.AddSeconds(52019986), DateTime.Now.AddSeconds(-61058025), DateTime.Now.AddSeconds(-209816638), DateTime.Now.AddSeconds(-339926455), DateTime.Now.AddSeconds(39307573), DateTime.Now.AddSeconds(55520755), DateTime.Now.AddSeconds(329180201), DateTime.Now.AddSeconds(90364142) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(136093075),
                NullableDateTimeArrayMember = new DateTime?[] { null, null, DateTime.Now.AddSeconds(-38163605), DateTime.Now.AddSeconds(159238395), DateTime.Now.AddSeconds(22342406), DateTime.Now.AddSeconds(-168674416), null, DateTime.Now.AddSeconds(-331815351), null, DateTime.Now.AddSeconds(69377370) },
            },
        };
    }
}
