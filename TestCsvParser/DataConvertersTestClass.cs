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
                StringMember = (string)"0'qEU0\"fm8",
                StringArrayMember = new string[] { "52bHbmhUAb", "%XgY:y?0hb", "mXLyuY''uP", ":wbjTFt Wq", "zf5zzciRMs", ",P$KViq0kD", "uox8kY#$F9", "F?d:zHoj%R", "?x3BayyRO7", "m'mT@K5#Pa" },
                CharMember = (char)'z',
                CharArrayMember = new char[] { 'n', 'q', '"', 'A', ' ', 'J', 'a', '$', 'N', 'i' },
                NullableCharMember = (char?)'T',
                NullableCharArrayMember = new char?[] { 's', null, 'O', null, 'y', 'd', null, null, 'H', null },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, false, true, true, true, true, true, false, false, true },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { false, true, false, true, false, null, false, false, false, true },
                ByteMember = (byte)0,
                ByteArrayMember = new byte[] { 134, 47, 82, 76, 174, 163, 238, 14, 53, 88 },
                NullableByteMember = (byte?)76,
                NullableByteArrayMember = new byte?[] { null, 178, 198, null, 106, 107, 158, 237, 39, null },
                SByteMember = (sbyte)-85,
                SByteArrayMember = new sbyte[] { -14, -112, -119, -43, -77, -6, -10, 0, -81, 105 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { -69, null, -90, null, 100, 76, 48, 124, -108, -54 },
                Int16Member = (short)4494,
                Int16ArrayMember = new short[] { -13763, 8954, 27140, 32332, -2653, 29500, 26292, 13079, 11067, 29037 },
                NullableInt16Member = (short?)27669,
                NullableInt16ArrayMember = new short?[] { null, 21970, 18, 30351, 17245, -1040, 8602, null, -32025, null },
                UInt16Member = (ushort)10417,
                UInt16ArrayMember = new ushort[] { 46365, 22455, 34806, 41252, 46700, 53625, 11466, 56838, 58443, 37525 },
                NullableUInt16Member = (ushort?)4312,
                NullableUInt16ArrayMember = new ushort?[] { null, 5505, 18812, 11093, 14514, 61295, null, 27557, 53615, 5267 },
                Int32Member = (int)240203748,
                Int32ArrayMember = new int[] { -1671242898, -1844795916, -34613919, -1153470820, -1229695443, 122228406, -1223076079, 1282132276, -374196811, 581854636 },
                NullableInt32Member = (int?)1299149791,
                NullableInt32ArrayMember = new int?[] { null, -1229841313, 1616353923, -1935832229, -1570138274, 240499559, -680422745, 1455340250, -569146981, 1465048346 },
                UInt32Member = (uint)2301219111,
                UInt32ArrayMember = new uint[] { 3427679259, 470774286, 2622765907, 732833547, 2023408976, 3337721893, 998333227, 994962865, 3453753477, 457835323 },
                NullableUInt32Member = (uint?)3397453387,
                NullableUInt32ArrayMember = new uint?[] { 1538851035, 1672492641, null, 4038303879, 435054186, null, null, 2068639713, 1888606892, 2189459590 },
                Int64Member = (long)-297552370444009364,
                Int64ArrayMember = new long[] { 3439576958000354871, -5596443505378128812, 8369286602382625763, -971020203861018360, -8042309108097627061, -324212475255938912, -8491448660151834198, 1651356178950636977, -1641156904943259003, -2451242361608609176 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { 7540145648642775876, 3352849472963476147, null, null, -692838013915415938, null, -1871369307807341878, 293277711040043058, null, null },
                UInt64Member = (ulong)120766969643871082,
                UInt64ArrayMember = new ulong[] { 3360868158266879427, 11248033189342757568, 10323217045076875572, 13068380670079805338, 18263210139944621930, 6905612706811156030, 6038059343282347282, 9958504972736519401, 4593398204462456805, 17321945358362809126 },
                NullableUInt64Member = (ulong?)5470951104837442129,
                NullableUInt64ArrayMember = new ulong?[] { null, 8313050658216413440, 16729416716442621380, null, 15410246244529340619, null, 6068248520768682277, null, null, null },
                SingleMember = (float)0.3217F,
                SingleArrayMember = new float[] { 0.3147F, 0.9735F, 0.6951F, 0.2411F, 0.0102F, 0.756F, 0.7466F, 0.121F, 0.3852F, 0.4937F },
                NullableSingleMember = (float?)0.547F,
                NullableSingleArrayMember = new float?[] { null, 0.6354F, null, 0.7128F, null, null, 0.4286F, 0.694F, 0.9065F, null },
                DoubleMember = (double)0.2202,
                DoubleArrayMember = new double[] { 0.8984, 0.2416, 0.7431, 0.1796, 0.5358, 0.7025, 0.9919, 0.5318, 0.1477, 0.0536 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.48, 0.5901, 0.0413, 0.4019, null, 0.0313, 0.9207, 0.9395, null, 0.5433 },
                DecimalMember = (decimal)0.0708175008514978m,
                DecimalArrayMember = new decimal[] { 0.447019135787626m, 0.851246232563279m, 0.310203877422122m, 0.202718343680128m, 0.0140205049021265m, 0.166172248388721m, 0.88508845674111m, 0.515677409021034m, 0.173041566355639m, 0.556780465206495m },
                NullableDecimalMember = (decimal?)0.0652124639904184m,
                NullableDecimalArrayMember = new decimal?[] { null, null, null, null, null, 0.588735997951001m, 0.142801055285521m, null, 0.48072059474919m, 0.603820046225479m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-88200227),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(227068745), DateTime.Now.AddSeconds(-135861161), DateTime.Now.AddSeconds(42961276), DateTime.Now.AddSeconds(274479777), DateTime.Now.AddSeconds(211735474), DateTime.Now.AddSeconds(246236505), DateTime.Now.AddSeconds(-85944718), DateTime.Now.AddSeconds(3906515), DateTime.Now.AddSeconds(-324638401), DateTime.Now.AddSeconds(-262920920) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(10981850),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-49103687), DateTime.Now.AddSeconds(38754595), null, null, DateTime.Now.AddSeconds(267204610), DateTime.Now.AddSeconds(-294292), DateTime.Now.AddSeconds(-170472425), DateTime.Now.AddSeconds(164840846), null, DateTime.Now.AddSeconds(12608918) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"glMan%9GiO",
                StringArrayMember = new string[] { "\"H2SN$8vh9", "w15UWMaK#p", "YsUu7bldlc", ",s6A:fNUF$", "YJlc112'Da", "vzJ'?Y9Hh9", "jfKF$E@5d:", " GE%QPG:\"F", "vsZK4Mi;Kp", "sF9Tl\"%gfw" },
                CharMember = (char)',',
                CharArrayMember = new char[] { '!', 'Y', 'x', 'I', ' ', 'V', '\'', 'W', 'M', ' ' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { '1', 'X', null, 'm', 'T', '.', 'Y', null, ';', 'A' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, false, false, false, true, true, true, true, false, false },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { true, true, true, null, null, null, false, null, false, true },
                ByteMember = (byte)65,
                ByteArrayMember = new byte[] { 59, 250, 208, 167, 246, 141, 127, 220, 126, 98 },
                NullableByteMember = (byte?)15,
                NullableByteArrayMember = new byte?[] { 150, 138, 79, 21, 38, 63, 26, 178, 191, 198 },
                SByteMember = (sbyte)-16,
                SByteArrayMember = new sbyte[] { 40, -124, -63, 90, -123, 82, 57, 111, -104, 103 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { 103, 17, 13, -75, null, -82, null, -35, null, null },
                Int16Member = (short)-17526,
                Int16ArrayMember = new short[] { -32556, -32656, -483, 23847, -11424, 5169, 27663, 10147, -595, 2442 },
                NullableInt16Member = (short?)19331,
                NullableInt16ArrayMember = new short?[] { -10656, 16127, -21300, 16001, 3933, 13610, null, -14052, null, 29364 },
                UInt16Member = (ushort)58269,
                UInt16ArrayMember = new ushort[] { 14320, 16415, 42308, 31632, 29044, 9586, 22819, 42276, 31235, 205 },
                NullableUInt16Member = (ushort?)5364,
                NullableUInt16ArrayMember = new ushort?[] { 27970, 2743, null, 56988, 7533, null, 37741, 31475, 32542, 19308 },
                Int32Member = (int)-1793992368,
                Int32ArrayMember = new int[] { 98337010, -1550251094, -420567866, -981456399, 1204658615, -2082687023, -1993483893, -289538455, 1451228310, -1307585346 },
                NullableInt32Member = (int?)635294742,
                NullableInt32ArrayMember = new int?[] { -1940000553, -993456977, null, -573474216, -1652678, null, -502912598, -474248422, 1006735966, -502374692 },
                UInt32Member = (uint)82530134,
                UInt32ArrayMember = new uint[] { 2186851540, 731276212, 2460016888, 3051851185, 3847384023, 4069065911, 857377753, 387141999, 1044517890, 3896205042 },
                NullableUInt32Member = (uint?)4042896212,
                NullableUInt32ArrayMember = new uint?[] { 3415799882, null, 3494708733, null, 1665738921, null, 2643495373, 796509096, null, null },
                Int64Member = (long)-6122177002801380729,
                Int64ArrayMember = new long[] { -1660442136547942455, 1749507882251043800, -8687910797555716918, -2080871020260787383, 2822519971878134653, 528272122478697798, 8251168729893035258, -7632485397623246415, 3468757160082501283, 8995131296614727936 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { null, 1570174565128613368, 4947625893375769163, null, -625160016835806523, 7501856837379202193, null, 1298978015391768925, -8635911218047565567, null },
                UInt64Member = (ulong)9209656716664731178,
                UInt64ArrayMember = new ulong[] { 9738918326185353296, 2781243308453167630, 5824733346442846410, 16956842461024579873, 6599630894210110989, 3687814019418634169, 12925511443136246237, 4550498149856083979, 3590106540941335856, 7061648020629208714 },
                NullableUInt64Member = (ulong?)17755652041959517037,
                NullableUInt64ArrayMember = new ulong?[] { null, 11666311069578058451, null, null, 8733445401675251538, null, 13534363553173789824, 6350593338923466805, 7789763076670262742, 6045841144952201788 },
                SingleMember = (float)0.1243F,
                SingleArrayMember = new float[] { 0.6282F, 0.8412F, 0.9225F, 0.7357F, 0.3434F, 0.2022F, 0.7526F, 0.6874F, 0.8111F, 0.907F },
                NullableSingleMember = (float?)0.2656F,
                NullableSingleArrayMember = new float?[] { 0.1925F, null, 0.5132F, null, 0.919F, 0.0591F, 0.6591F, null, 0.594F, 0.9915F },
                DoubleMember = (double)0.2106,
                DoubleArrayMember = new double[] { 0.678, 0.6055, 0.2427, 0.3849, 0.2201, 0.4652, 0.2198, 0.6743, 0.2614, 0.4369 },
                NullableDoubleMember = (double?)0.9322,
                NullableDoubleArrayMember = new double?[] { null, 0.2174, null, 0.2635, null, 0.5218, 0.8178, null, null, 0.4351 },
                DecimalMember = (decimal)0.991774283811345m,
                DecimalArrayMember = new decimal[] { 0.515465474927549m, 0.455271222374994m, 0.482984171008218m, 0.955419438870354m, 0.343057924110004m, 0.120553431622942m, 0.523513820731786m, 0.762333639786734m, 0.431623902838502m, 0.390198801360186m },
                NullableDecimalMember = (decimal?)0.690060322028613m,
                NullableDecimalArrayMember = new decimal?[] { 0.682104351782289m, 0.954248790607904m, 0.351340439799866m, null, 0.789319365652892m, null, null, null, null, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, null, null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(304743830),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(75419464), DateTime.Now.AddSeconds(12991197), DateTime.Now.AddSeconds(63935418), DateTime.Now.AddSeconds(124222822), DateTime.Now.AddSeconds(-106233658), DateTime.Now.AddSeconds(-68468950), DateTime.Now.AddSeconds(-241214028), DateTime.Now.AddSeconds(18036607), DateTime.Now.AddSeconds(-177004005), DateTime.Now.AddSeconds(20365171) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { null, DateTime.Now.AddSeconds(346978734), null, null, DateTime.Now.AddSeconds(-28971827), null, DateTime.Now.AddSeconds(-34253491), DateTime.Now.AddSeconds(-280476594), null, DateTime.Now.AddSeconds(-122636963) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"6$lMbovLzx",
                StringArrayMember = new string[] { "S!NbN;GAY8", "sFu.V\"T7kX", "RwjTWxGN6P", "nXNkWx:iFu", ".$ItE0Zy;h", "U.xcgti!En", "6'RIUU:Kov", "C82?kTo?Pt", "XfXZ!0x8E0", "O$dp,!3 Z6" },
                CharMember = (char)'c',
                CharArrayMember = new char[] { 'W', '\'', '7', 'm', 'C', ',', '8', '"', 'U', 'p' },
                NullableCharMember = (char?)'O',
                NullableCharArrayMember = new char?[] { null, 'E', '#', 'G', 'Q', 'G', '2', 'm', null, null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, true, false, true, true, false, true, false, false, true },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { null, true, true, false, null, false, null, null, null, true },
                ByteMember = (byte)182,
                ByteArrayMember = new byte[] { 242, 29, 84, 122, 173, 27, 37, 199, 232, 33 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { 202, null, 182, null, 141, 64, 20, 144, null, 58 },
                SByteMember = (sbyte)-52,
                SByteArrayMember = new sbyte[] { -40, -15, -26, -9, -75, 61, -63, 24, 122, -32 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { -21, null, 123, 66, 14, 73, -43, 81, null, null },
                Int16Member = (short)14599,
                Int16ArrayMember = new short[] { -25467, -6272, 13392, 8000, 3398, 7720, 1708, 18821, 11918, -13789 },
                NullableInt16Member = (short?)24483,
                NullableInt16ArrayMember = new short?[] { 13293, 26982, -29380, null, null, 19216, null, 7243, 7442, -23609 },
                UInt16Member = (ushort)36652,
                UInt16ArrayMember = new ushort[] { 18201, 1949, 8955, 64798, 23510, 28888, 61318, 18390, 60348, 38919 },
                NullableUInt16Member = (ushort?)14860,
                NullableUInt16ArrayMember = new ushort?[] { 31207, 50145, null, 56684, 21608, null, 64113, 38793, 33653, 18463 },
                Int32Member = (int)-1304151860,
                Int32ArrayMember = new int[] { -717333914, -166929237, 1189259302, 515737139, 1460158193, -806880604, 1784603808, -1132323368, -2002749595, -539016110 },
                NullableInt32Member = (int?)518665992,
                NullableInt32ArrayMember = new int?[] { -274704904, null, null, null, null, null, -952799374, 621289121, 904314081, -2144772436 },
                UInt32Member = (uint)26169443,
                UInt32ArrayMember = new uint[] { 2602648059, 438871716, 326289404, 2668868748, 739088513, 2913750631, 3140552687, 2145977747, 3886965915, 4279748647 },
                NullableUInt32Member = (uint?)3637875567,
                NullableUInt32ArrayMember = new uint?[] { 3390882870, null, 2912510787, 1973201779, 4091410310, null, 3111594758, null, 3619354759, 2674774362 },
                Int64Member = (long)-1692058883185864376,
                Int64ArrayMember = new long[] { 8997653768780836472, -1639311251948621665, -1876223086623197462, -2488453069082141333, -6155563139872278554, 5270470341156738200, 2872286289086234706, 7015945173413740024, -2380108889087971400, -1005688715894143645 },
                NullableInt64Member = (long?)-2703087742637643818,
                NullableInt64ArrayMember = new long?[] { 9203092313714897695, 1105571219811422057, null, null, -18822711384873802, null, 7044831619077156283, null, -4818578219606675137, -9134831713056197200 },
                UInt64Member = (ulong)6117599517016489598,
                UInt64ArrayMember = new ulong[] { 1599157339080144621, 7790836421600846813, 6431985021792773765, 8681861987373198280, 16298769245017389436, 16276082114357050353, 17079986790156497248, 12798260377985448563, 12188438558551848999, 9648796897236137014 },
                NullableUInt64Member = (ulong?)12695360963403596406,
                NullableUInt64ArrayMember = new ulong?[] { 11934367358712597410, 13689617236676254266, 9991590447581296294, 7414342232404011667, 12445137310072339092, 9741791274321239971, 798921717957761056, null, null, null },
                SingleMember = (float)0.0012F,
                SingleArrayMember = new float[] { 0.1464F, 0.1037F, 0.3364F, 0.6223F, 0.4928F, 0.5441F, 0.5712F, 0.4213F, 0.2036F, 0.4632F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { 0.6941F, 0.5968F, 0.1818F, null, 0.9846F, 0.6169F, 0.5006F, 0.569F, 0.2633F, 0.518F },
                DoubleMember = (double)0.8224,
                DoubleArrayMember = new double[] { 0.9535, 0.5963, 0.0933, 0.298, 0.9886, 0.9372, 0.3756, 0.9297, 0.0309, 0.0951 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.8806, null, null, 0.1157, 0.5664, 0.7033, null, 0.3456, null, null },
                DecimalMember = (decimal)0.325057846179725m,
                DecimalArrayMember = new decimal[] { 0.928016445565976m, 0.868505451767009m, 0.827726821334905m, 0.829483282672932m, 0.0559280896819793m, 0.410200458676648m, 0.174397964577376m, 0.369622981813561m, 0.0604061866460397m, 0.690057395347421m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.944659032367477m, 0.851031650254052m, 0.191205706070739m, 0.0699203866859527m, null, 0.15508560145045m, null, 0.129380154949324m, null, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(78927282),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-136566300), DateTime.Now.AddSeconds(327822157), DateTime.Now.AddSeconds(-303105373), DateTime.Now.AddSeconds(6517782), DateTime.Now.AddSeconds(-282896677), DateTime.Now.AddSeconds(341297597), DateTime.Now.AddSeconds(-279382231), DateTime.Now.AddSeconds(11659615), DateTime.Now.AddSeconds(67861143), DateTime.Now.AddSeconds(297046431) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-292870029),
                NullableDateTimeArrayMember = new DateTime?[] { null, null, DateTime.Now.AddSeconds(-112192280), null, null, DateTime.Now.AddSeconds(224810737), DateTime.Now.AddSeconds(131536004), null, null, DateTime.Now.AddSeconds(243061104) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"8PJRfjH8'M",
                StringArrayMember = new string[] { "Et2hPas:Nr", "uGF.gEfNBp", "momf8IWF!i", "s?\":wsS@Zk", "Z,85M2JW8a", "E!7ZxmGWwH", "Em$2%5MOO.", "8j.nQx24SF", "% UqeyFqQJ", "l@VSg9:G7e" },
                CharMember = (char)'G',
                CharArrayMember = new char[] { 'f', '5', 'W', '7', '0', 'v', 'X', 'P', '?', 'i' },
                NullableCharMember = (char?)'e',
                NullableCharArrayMember = new char?[] { 'h', null, 'W', null, 'c', '#', '$', 'G', '0', '@' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, true, false, false, true, false, false, true, true, true },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { false, null, null, null, false, false, false, null, false, null },
                ByteMember = (byte)203,
                ByteArrayMember = new byte[] { 169, 190, 156, 110, 87, 245, 120, 146, 15, 115 },
                NullableByteMember = (byte?)17,
                NullableByteArrayMember = new byte?[] { 59, 87, 90, null, 250, null, 55, 118, 8, 232 },
                SByteMember = (sbyte)-35,
                SByteArrayMember = new sbyte[] { -45, -4, -29, -13, -19, 23, 38, 1, 114, 83 },
                NullableSByteMember = (sbyte?)95,
                NullableSByteArrayMember = new sbyte?[] { -73, -96, -116, null, 40, null, null, 79, 13, -112 },
                Int16Member = (short)17173,
                Int16ArrayMember = new short[] { -850, 20102, -25795, 15585, -1365, 1308, -18982, -11782, -5990, -16589 },
                NullableInt16Member = (short?)null,
                NullableInt16ArrayMember = new short?[] { null, -10432, -28243, 9672, 8728, null, null, null, null, 26077 },
                UInt16Member = (ushort)35640,
                UInt16ArrayMember = new ushort[] { 17537, 4755, 3085, 8170, 7554, 14909, 32177, 37483, 34599, 1409 },
                NullableUInt16Member = (ushort?)3194,
                NullableUInt16ArrayMember = new ushort?[] { 29939, null, 34148, 35481, 10331, null, 18266, 61247, 20144, 2746 },
                Int32Member = (int)-1325546852,
                Int32ArrayMember = new int[] { -922162262, -1913377560, 372468087, 1628901347, -1003492811, 1866142968, 809706346, -1452184314, -611598732, -600450955 },
                NullableInt32Member = (int?)-1303156328,
                NullableInt32ArrayMember = new int?[] { null, 1271350941, -1783154541, null, null, -2017204027, null, -2077974134, 1387551980, -1108891040 },
                UInt32Member = (uint)991856329,
                UInt32ArrayMember = new uint[] { 1392626488, 2323258547, 4154255416, 3188596196, 1237603056, 4070628406, 3411123645, 1891421448, 1547614143, 2646857732 },
                NullableUInt32Member = (uint?)504385154,
                NullableUInt32ArrayMember = new uint?[] { 2948742385, 97829495, null, 3407255054, 724331394, null, 398045918, 2377335335, null, 152349918 },
                Int64Member = (long)-5875186815290103269,
                Int64ArrayMember = new long[] { 6029637658956282017, -9188000252189096, -4642058460594542592, -3708897373793499955, 2095751315585102705, -4615839108756079545, -8628130001122388917, -84029167970768476, 7297836195211114782, -8145246062987867054 },
                NullableInt64Member = (long?)-5640669236013962520,
                NullableInt64ArrayMember = new long?[] { null, null, -3517525542747088827, 4895128911776277845, -1473799770677748052, 2249553382242232295, 9068884153227322714, null, -4842532382844496996, 2902677966510124671 },
                UInt64Member = (ulong)17744859762042936863,
                UInt64ArrayMember = new ulong[] { 12251717220547331171, 14145368874721138018, 9756816021993366478, 2724106228317532322, 18109050922350164873, 12533590181316701451, 3925970806301256911, 7600810131524205085, 9864598760582641672, 6965152693616494290 },
                NullableUInt64Member = (ulong?)13812398228018149021,
                NullableUInt64ArrayMember = new ulong?[] { 5418265136006963992, null, null, null, 12572887948347225695, 10681072591798538731, null, 6321071199654777241, 18392313198410935707, null },
                SingleMember = (float)0.6081F,
                SingleArrayMember = new float[] { 0.8665F, 0.6959F, 0.8099F, 0.7606F, 0.0124F, 0.9292F, 0.6141F, 0.6997F, 0.8903F, 0.4262F },
                NullableSingleMember = (float?)0.2742F,
                NullableSingleArrayMember = new float?[] { null, null, 0.365F, 0.0624F, 0.6436F, 0.8918F, 0.8204F, null, 0.8358F, null },
                DoubleMember = (double)0.6888,
                DoubleArrayMember = new double[] { 0.4998, 0.7878, 0.8605, 0.0237, 0.3531, 0.0301, 0.3975, 0.7208, 0.6652, 0.3435 },
                NullableDoubleMember = (double?)0.9436,
                NullableDoubleArrayMember = new double?[] { null, null, 0.565, 0.6314, 0.2781, 0.7587, 0.2665, null, 0.3873, 0.3233 },
                DecimalMember = (decimal)0.202454918158452m,
                DecimalArrayMember = new decimal[] { 0.0852702698136076m, 0.00808080379296132m, 0.882626176291437m, 0.316270682176701m, 0.885207787102651m, 0.795495585443217m, 0.59549737516581m, 0.893828188019725m, 0.603897081503597m, 0.560715518687254m },
                NullableDecimalMember = (decimal?)0.279859863351965m,
                NullableDecimalArrayMember = new decimal?[] { 0.312481369968728m, 0.264191395260483m, null, null, null, 0.93828771539884m, null, 0.542668599888062m, null, 0.175243190105652m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(192153896),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-11002680), DateTime.Now.AddSeconds(53220385), DateTime.Now.AddSeconds(-56532210), DateTime.Now.AddSeconds(339986369), DateTime.Now.AddSeconds(15102959), DateTime.Now.AddSeconds(-333596695), DateTime.Now.AddSeconds(202603767), DateTime.Now.AddSeconds(245352271), DateTime.Now.AddSeconds(40214450), DateTime.Now.AddSeconds(-223898633) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-170526951),
                NullableDateTimeArrayMember = new DateTime?[] { null, null, null, DateTime.Now.AddSeconds(57422998), DateTime.Now.AddSeconds(-86101798), DateTime.Now.AddSeconds(-109513602), null, DateTime.Now.AddSeconds(118213761), DateTime.Now.AddSeconds(-103178008), null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"p@H$orGTIA",
                StringArrayMember = new string[] { "7YEqJFSeXW", "0s$FZ;NsHe", "6w$,nC:WC.", "s8hQZN:1Ib", "qj6l1#%Cvy", "dkM1QKbQDH", "N@bk0:K33L", "%ij3.W;jKq", "D?ybgIv3AO", "8yyBNvHV79" },
                CharMember = (char)'Y',
                CharArrayMember = new char[] { 'j', 'b', 'N', 'K', '!', '\'', '2', 'l', 'f', '?' },
                NullableCharMember = (char?)'D',
                NullableCharArrayMember = new char?[] { null, null, '$', null, null, '7', 'F', 'K', null, 'Y' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, false, false, true, true, false, true, false, false, true },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { true, true, false, false, true, true, null, false, false, false },
                ByteMember = (byte)43,
                ByteArrayMember = new byte[] { 62, 233, 250, 63, 98, 194, 196, 115, 139, 204 },
                NullableByteMember = (byte?)126,
                NullableByteArrayMember = new byte?[] { null, null, 241, 15, 132, 67, 207, 203, null, null },
                SByteMember = (sbyte)46,
                SByteArrayMember = new sbyte[] { -29, -66, 70, -69, 39, 99, -71, 23, 125, 121 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { null, 110, 45, 18, -76, null, -97, null, 43, 42 },
                Int16Member = (short)-24621,
                Int16ArrayMember = new short[] { -11873, 16965, 6202, 29003, 28639, -12715, 1967, 30533, -4721, -1456 },
                NullableInt16Member = (short?)-29740,
                NullableInt16ArrayMember = new short?[] { -1353, 23578, 23126, null, null, -28306, 30835, null, 8518, 2900 },
                UInt16Member = (ushort)52464,
                UInt16ArrayMember = new ushort[] { 50840, 11571, 14086, 8168, 2712, 47790, 14332, 44964, 8471, 18499 },
                NullableUInt16Member = (ushort?)14637,
                NullableUInt16ArrayMember = new ushort?[] { null, 12171, 36772, 29745, 53837, null, 61525, 49453, null, null },
                Int32Member = (int)-374982069,
                Int32ArrayMember = new int[] { 381624894, 737478203, -210732836, 1532957037, 415530536, -34678006, -346577344, -1978344504, -397878718, 493187896 },
                NullableInt32Member = (int?)1155796752,
                NullableInt32ArrayMember = new int?[] { -1941287132, -561088820, -1781087949, 1677812234, null, 578275475, null, -855995994, 142729401, 323438139 },
                UInt32Member = (uint)2923765336,
                UInt32ArrayMember = new uint[] { 836940523, 3534570230, 3823304792, 1573103346, 3902273039, 3478229403, 1773681306, 1277797814, 2068676071, 3586359548 },
                NullableUInt32Member = (uint?)2847074900,
                NullableUInt32ArrayMember = new uint?[] { null, 4189579319, 1494441094, 2327549715, 2530718235, 1632131445, 1174913587, 1525491103, 3333160832, 452225674 },
                Int64Member = (long)-7853239728890221857,
                Int64ArrayMember = new long[] { 9109914034488904349, 1588748417600278221, 4255165153510743367, -6662627051910201651, -7652050364629790922, -9087248258531217151, 4281083832316226653, 3926674806241153917, -3289395160903392059, 3629937499667237390 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { -7249178485208935754, 2828034790953908969, 371279872304770597, -6740634892056637455, 8061983260702438015, null, -6121119797451824797, 5508976078240710417, -5422255863701392777, null },
                UInt64Member = (ulong)1309755917090228282,
                UInt64ArrayMember = new ulong[] { 7721059786376192836, 9673914805076758110, 12670570564114453160, 3458875942424412107, 8548978703335469266, 10734409149691058652, 6577020859293833927, 17948014794757231053, 10858228139128311310, 9398807771191318893 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { null, 5139850226680584508, 8179118314574490652, 4216937109327093350, null, 2294568170061718152, 13869588104708839485, 2855035867826629718, 13751201950050870482, 3708134776465767337 },
                SingleMember = (float)0.9238F,
                SingleArrayMember = new float[] { 0.4988F, 0.3079F, 0.1685F, 0.6855F, 0.8551F, 0.4809F, 0.252F, 0.8842F, 0.1239F, 0.5844F },
                NullableSingleMember = (float?)0.5677F,
                NullableSingleArrayMember = new float?[] { 0.104F, null, 0.3426F, null, 0.3195F, 0.1339F, 0.9548F, 0.5492F, 0.689F, 0.2312F },
                DoubleMember = (double)0.5668,
                DoubleArrayMember = new double[] { 0.7477, 0.9398, 0.6041, 0.2484, 0.8758, 0.1419, 0.1705, 0.0714, 0.2791, 0.0948 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { null, 0.6216, 0.9833, 0.6648, null, 0.2147, 0.1472, 0.5339, 0.9351, null },
                DecimalMember = (decimal)0.772930844115527m,
                DecimalArrayMember = new decimal[] { 0.244240558354296m, 0.870238780914917m, 0.700103127723608m, 0.493916861942931m, 0.928790629808228m, 0.971943046418924m, 0.284352711999953m, 0.0523253828530784m, 0.807431219055984m, 0.28509034415944m },
                NullableDecimalMember = (decimal?)0.14916344692426m,
                NullableDecimalArrayMember = new decimal?[] { null, 0.311882500681971m, null, 0.790060523333988m, null, 0.227776334261418m, null, null, null, 0.496634725246874m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(290694460),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(81897696), DateTime.Now.AddSeconds(35633967), DateTime.Now.AddSeconds(21994309), DateTime.Now.AddSeconds(87827232), DateTime.Now.AddSeconds(-175957781), DateTime.Now.AddSeconds(-200856300), DateTime.Now.AddSeconds(81620448), DateTime.Now.AddSeconds(308439230), DateTime.Now.AddSeconds(-225606647), DateTime.Now.AddSeconds(307060062) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(162822005), null, DateTime.Now.AddSeconds(-238293648), null, DateTime.Now.AddSeconds(-86786573), DateTime.Now.AddSeconds(-197086297), DateTime.Now.AddSeconds(234765191), null, null, DateTime.Now.AddSeconds(-48332502) },
            },
        };
    }
}
