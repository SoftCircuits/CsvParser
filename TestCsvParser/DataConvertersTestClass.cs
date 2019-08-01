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

            PropertyInfo[] properties1 = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] properties2 = other.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (properties1.Length != properties2.Length)
            {
                Debug.Assert(false);
                return false;
            }

            // Compare property values
            for (int i = 0; i < properties1.Length; i++)
            {
                if (!ObjectsAreEqual(properties1[i].GetValue(this), properties2[i].GetValue(other)))
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
                StringMember = (string)"eZ\"BgfYFdi",
                StringArrayMember = new string[] { "SF96SL%'EU", "K;LlQn%IBB", "mFW2u8B%dR", "Hq76vcKsSC", "4t:TSnOf8'", "hPDc@M5HMh", "JAm.MrGc?.", "cc; U\"ODjH", "tsNk7'mf.j", "uDg8'4HEGx" },
                CharMember = (char)'J',
                CharArrayMember = new char[] { 's', 's', '%', 'W', 'J', 'X', 'G', '@', '9', '6' },
                NullableCharMember = (char?)'H',
                NullableCharArrayMember = new char?[] { 'Q', '3', 'g', null, null, null, '7', null, '#', 'N' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, true, true, true, true, true, true, false, true, false },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { true, null, false, false, false, null, false, false, false, true },
                ByteMember = (byte)248,
                ByteArrayMember = new byte[] { 152, 152, 215, 153, 13, 169, 83, 59, 120, 247 },
                NullableByteMember = (byte?)95,
                NullableByteArrayMember = new byte?[] { 153, 191, 251, 154, 206, null, 130, 14, 73, 75 },
                SByteMember = (sbyte)-43,
                SByteArrayMember = new sbyte[] { 60, 43, -42, 102, 102, -103, -27, -102, -58, 41 },
                NullableSByteMember = (sbyte?)1,
                NullableSByteArrayMember = new sbyte?[] { 77, null, 4, null, null, null, -24, 18, 51, -64 },
                Int16Member = (short)30633,
                Int16ArrayMember = new short[] { 10461, 23191, 28105, 11358, -8108, -28431, 16756, 17552, -12100, 28947 },
                NullableInt16Member = (short?)32072,
                NullableInt16ArrayMember = new short?[] { null, -7062, 32204, null, 12416, 17471, -21167, null, null, 5405 },
                UInt16Member = (ushort)13419,
                UInt16ArrayMember = new ushort[] { 51783, 7133, 45350, 59234, 59052, 37064, 37076, 60155, 16038, 21633 },
                NullableUInt16Member = (ushort?)42784,
                NullableUInt16ArrayMember = new ushort?[] { null, 25923, 58400, 22238, 40556, 44813, 4588, null, 18814, null },
                Int32Member = (int)-1798083983,
                Int32ArrayMember = new int[] { 713913684, 1869920965, 387363626, -1462580291, -691739076, 487067928, 383313491, -611092748, -368737086, 2140537943 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { null, -1902875997, null, null, -56259730, null, -556371860, 548633438, null, 1357366585 },
                UInt32Member = (uint)678430323,
                UInt32ArrayMember = new uint[] { 2626428639, 1220642567, 3430494282, 1838970891, 396789352, 3564793390, 3344816514, 3662270481, 3072835826, 136833118 },
                NullableUInt32Member = (uint?)2218940470,
                NullableUInt32ArrayMember = new uint?[] { 1599260015, 4200161516, null, 690821560, null, 3651135353, 2148459857, 385289117, 2557113935, null },
                Int64Member = (long)-5465036975316202057,
                Int64ArrayMember = new long[] { -3688476428439359477, 637208722417485232, 8808241795942489509, -5095279660228413932, 3398065182786588561, -1814719780728359298, -2128695871006777803, -4492425195033919949, 5386630828357435195, 4792066783044417397 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { -4928565995064113433, 3030376964237253960, -6070710335928276121, -3806352328020424211, 8767794110605567903, -2646238295184073071, -6039983149559522763, null, 175551324669453513, null },
                UInt64Member = (ulong)7777136501466182823,
                UInt64ArrayMember = new ulong[] { 7674027821449165685, 10952636132019284087, 2931272390581776081, 14980870395199776103, 6498030650274873863, 1315660390494865448, 8986512381594633908, 13342727067606632590, 3252784584706445122, 4323772822058184257 },
                NullableUInt64Member = (ulong?)5442152524498462713,
                NullableUInt64ArrayMember = new ulong?[] { 14293937892818790715, 13937762881219911640, 11032819388003879228, 18057247327606707440, 2942736004372910647, 5345809833312927590, null, 3633639840278635214, 8013142350404590321, null },
                SingleMember = (float)0.6625063F,
                SingleArrayMember = new float[] { 0.6147364F, 0.6739014F, 0.6119345F, 0.8720292F, 0.9188726F, 0.6313674F, 0.5601265F, 0.04048461F, 0.6323038F, 0.01299597F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { null, null, 0.9932202F, null, null, 0.7428812F, null, null, 0.93414F, null },
                DoubleMember = (double)0.44432351,
                DoubleArrayMember = new double[] { 0.80053703, 0.00382543, 0.68266939, 0.93467063, 0.89038123, 0.69145223, 0.67108798, 0.56669148, 0.22483694, 0.20817043 },
                NullableDoubleMember = (double?)0.32285037,
                NullableDoubleArrayMember = new double?[] { 0.33571796, null, null, 0.55201139, null, 0.84754391, 0.75339445, 0.31937218, 0.02135985, null },
                DecimalMember = (decimal)0.747885087853244m,
                DecimalArrayMember = new decimal[] { 0.768603456098867m, 0.961024169326306m, 0.0877152765578196m, 0.289433695976359m, 0.415759522195794m, 0.150991511135824m, 0.501555082621777m, 0.634776782540035m, 0.153035045207029m, 0.944408117301952m },
                NullableDecimalMember = (decimal?)0.72983813925173m,
                NullableDecimalArrayMember = new decimal?[] { 0.256847205691434m, 0.827105494135574m, 0.120741001852202m, 0.196985346822527m, 0.665424621042528m, 0.31116852737552m, null, 0.606987571160769m, null, 0.984985049341333m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-345762917),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-173947018), DateTime.Now.AddSeconds(-106756433), DateTime.Now.AddSeconds(-305387052), DateTime.Now.AddSeconds(51480803), DateTime.Now.AddSeconds(-147918466), DateTime.Now.AddSeconds(4538916), DateTime.Now.AddSeconds(296042505), DateTime.Now.AddSeconds(162976343), DateTime.Now.AddSeconds(47939515), DateTime.Now.AddSeconds(214239554) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-66807934), null, DateTime.Now.AddSeconds(216715309), DateTime.Now.AddSeconds(-188022769), DateTime.Now.AddSeconds(268157281), DateTime.Now.AddSeconds(206642595), null, null, DateTime.Now.AddSeconds(259768970), null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"g9q%nEYEHC",
                StringArrayMember = new string[] { "$1uE.o.CCU", "FxodQZ@EGm", "9K#9x7CO#2", "qhX8l'fc$b", "!EB9s5UN5N", "IU%sP.58eC", "lvFX0ua2:X", "\"qH1s4JJQ:", "m?g5tWj2sx", "Ku$!?W 0J%" },
                CharMember = (char)'k',
                CharArrayMember = new char[] { '?', ':', 'D', '2', '0', 'h', 'j', '.', 'z', '3' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { 'g', 'C', null, null, '@', 'F', 'O', null, null, 's' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, false, true, true, false, true, false, true, true },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { null, true, true, false, true, false, true, null, null, true },
                ByteMember = (byte)115,
                ByteArrayMember = new byte[] { 243, 112, 21, 227, 197, 64, 235, 61, 155, 35 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { 213, 25, 28, 161, 44, 179, 246, 22, 155, 241 },
                SByteMember = (sbyte)-92,
                SByteArrayMember = new sbyte[] { 38, 10, -69, 34, 4, 22, -95, 125, 114, -77 },
                NullableSByteMember = (sbyte?)123,
                NullableSByteArrayMember = new sbyte?[] { null, 99, -100, -27, 14, null, null, -15, -45, 67 },
                Int16Member = (short)-17151,
                Int16ArrayMember = new short[] { -1686, -23743, -10958, -8136, -21867, -29020, -30625, 24753, 20182, 4125 },
                NullableInt16Member = (short?)20604,
                NullableInt16ArrayMember = new short?[] { 32637, null, -24308, -13604, -19591, null, -417, 12111, -20577, null },
                UInt16Member = (ushort)19691,
                UInt16ArrayMember = new ushort[] { 6531, 61666, 57623, 8743, 12045, 19427, 51100, 11208, 14826, 9049 },
                NullableUInt16Member = (ushort?)38989,
                NullableUInt16ArrayMember = new ushort?[] { null, 16487, 20610, 31374, null, null, 60888, 17816, 56975, 20587 },
                Int32Member = (int)-1287368905,
                Int32ArrayMember = new int[] { -1301956388, 77879487, 889718769, 2109868230, -1648939289, -2086223931, 1834303023, 993440297, -1969272480, -2062041849 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { 85437322, 1751778097, null, 303885735, 922976386, -724709058, 712324968, 896229426, null, 1780789034 },
                UInt32Member = (uint)2036082098,
                UInt32ArrayMember = new uint[] { 330745496, 3757690534, 1524563729, 1948529380, 2351583341, 1510100675, 3282266220, 1417971088, 2218861962, 2976436890 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 3711432697, 103040324, null, null, 1556302262, 1312107325, null, 4039102346, 2028444633, 571167748 },
                Int64Member = (long)878083886053957685,
                Int64ArrayMember = new long[] { -512321597550463611, 4944394649973111025, -2609408601339940979, 974039727095510185, -4210459982406640092, 3464445078201627106, 97218044846340098, 8502875505102774955, 2112300775352823987, 8820954589683089949 },
                NullableInt64Member = (long?)5026230006922057845,
                NullableInt64ArrayMember = new long?[] { -7425984376713080437, null, null, 8847558638892208886, null, -3746520165566974120, 5742862325742366254, -6743581982139166641, 3610671146128176214, null },
                UInt64Member = (ulong)6536154205069097788,
                UInt64ArrayMember = new ulong[] { 15814806493906835859, 9250876562901080364, 9579407220156807146, 7873197506376392370, 11473675788133651484, 9554930321411491592, 12627479631812612185, 17613423576848357288, 4263093567974819413, 14124453423409308669 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { null, 3099767690724412570, 4141168535427801382, 17595398197420458293, 15332192200944747810, 1525421745297271672, 10392841922325268336, null, 11953166210204638451, null },
                SingleMember = (float)0.1927065F,
                SingleArrayMember = new float[] { 0.5154226F, 0.01549079F, 0.789892F, 0.1422806F, 0.5622869F, 0.4178321F, 0.8789418F, 0.5721979F, 0.5230424F, 0.1332631F },
                NullableSingleMember = (float?)0.4798814F,
                NullableSingleArrayMember = new float?[] { null, 0.8572075F, 0.9961467F, null, 0.6814213F, null, 0.1812985F, 0.619758F, 0.02876768F, 0.3336212F },
                DoubleMember = (double)0.90391691,
                DoubleArrayMember = new double[] { 0.50904932, 0.5497782, 0.19870528, 0.45541037, 0.22518549, 0.06914655, 0.45025476, 0.98555097, 0.73000214, 0.35156397 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.20669027, null, 0.17226851, null, 0.91201093, 0.17896167, 0.49700809, 0.57086074, null, 0.25043097 },
                DecimalMember = (decimal)0.8645648392218m,
                DecimalArrayMember = new decimal[] { 0.512482826836632m, 0.85659755387185m, 0.229508943496975m, 0.0392962861989142m, 0.197428794203991m, 0.206904076136138m, 0.287088006868534m, 0.952953953274039m, 0.554095754192255m, 0.842063326780714m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.992560006208979m, null, 0.904653442979163m, 0.0575013286701875m, 0.829017717311633m, 0.989369525103536m, 0.697523411688173m, 0.980419991994472m, 0.647942602004829m, 0.544003939975055m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(48676636),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-246601097), DateTime.Now.AddSeconds(308897043), DateTime.Now.AddSeconds(24660940), DateTime.Now.AddSeconds(-249504200), DateTime.Now.AddSeconds(-206723269), DateTime.Now.AddSeconds(20250910), DateTime.Now.AddSeconds(-110384855), DateTime.Now.AddSeconds(235937482), DateTime.Now.AddSeconds(-126470339), DateTime.Now.AddSeconds(273912162) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(266974097),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(156192041), DateTime.Now.AddSeconds(-2333916), null, DateTime.Now.AddSeconds(-220502217), null, DateTime.Now.AddSeconds(178763917), DateTime.Now.AddSeconds(-104029098), DateTime.Now.AddSeconds(-275589909), DateTime.Now.AddSeconds(-30589614), DateTime.Now.AddSeconds(41831085) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"fpVhUpMUAi",
                StringArrayMember = new string[] { "1vrmnwOW0d", "LvKQNWsM,V", "4fASj$q lE", "JO1K#lCieQ", "Hz 2.7fEU%", "#Af10WuZDp", "GP8wU#\" Uy", "TK4vOEfVfz", "pl2nHJ6 gx", ";0f,K':Zh;" },
                CharMember = (char)'7',
                CharArrayMember = new char[] { 'f', 'R', 'N', 'u', '\'', 'i', '$', '\'', 'V', 'b' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { 'h', 'W', 'A', '"', '5', 'g', '3', 'f', '0', null },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, false, true, true, true, false, true, false, true, true },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { false, null, null, null, true, false, true, null, true, null },
                ByteMember = (byte)109,
                ByteArrayMember = new byte[] { 171, 198, 107, 172, 131, 78, 248, 238, 16, 109 },
                NullableByteMember = (byte?)33,
                NullableByteArrayMember = new byte?[] { null, 64, null, 52, 36, null, 71, 187, 236, 42 },
                SByteMember = (sbyte)-110,
                SByteArrayMember = new sbyte[] { -77, -100, -92, 117, 30, -98, -125, 94, 76, -74 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { 16, null, 24, 7, -35, -26, null, 21, -114, null },
                Int16Member = (short)-29916,
                Int16ArrayMember = new short[] { -12502, -1842, 30961, -22034, 9792, 28850, -11261, 19187, -15160, 4018 },
                NullableInt16Member = (short?)-556,
                NullableInt16ArrayMember = new short?[] { null, -2873, 25019, 16526, null, -28207, null, null, 2698, -21325 },
                UInt16Member = (ushort)48970,
                UInt16ArrayMember = new ushort[] { 38137, 23312, 61384, 33078, 9555, 41391, 40847, 36285, 53588, 10786 },
                NullableUInt16Member = (ushort?)64595,
                NullableUInt16ArrayMember = new ushort?[] { 56768, 47806, 5983, 35288, null, 45619, 18533, 37064, 56502, 30288 },
                Int32Member = (int)-1557312833,
                Int32ArrayMember = new int[] { 2021809081, -2104083931, -632586746, -938397622, -230755125, -410892443, -782731753, -199504673, 572205476, -279920923 },
                NullableInt32Member = (int?)-370386257,
                NullableInt32ArrayMember = new int?[] { 1537079841, null, null, -663601606, null, -1564089682, 557209442, -1559436305, -1779583631, null },
                UInt32Member = (uint)2103377294,
                UInt32ArrayMember = new uint[] { 1512144748, 3254218426, 2250910526, 2087612208, 1403719085, 2070629029, 4122061185, 1075329464, 216119245, 1343500121 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 4072435867, null, 2155187942, 321050634, null, null, null, null, 3910272009, null },
                Int64Member = (long)5881362394689202911,
                Int64ArrayMember = new long[] { -3204589406877180272, 2376369042755471146, -7193753606886479182, 1360198322367964584, -978058992302777871, 5804131044329989424, -1721725316902704528, -4203931930408288268, 8546054339026131686, 4480592948127000125 },
                NullableInt64Member = (long?)9060433876194718851,
                NullableInt64ArrayMember = new long?[] { 6232294430341400406, 630804944157971411, -6112554228140733447, null, -7973443962386429289, null, -9175140575424107742, 2698433619888467891, null, 6536621178756824397 },
                UInt64Member = (ulong)10232171772662206048,
                UInt64ArrayMember = new ulong[] { 13712715926493882224, 15709270774910512300, 2062862205263274212, 3528983869601907133, 14013082758168591531, 15429836907951598885, 12488193347345282380, 3719270321936360903, 7806103474075060082, 17452256802633648487 },
                NullableUInt64Member = (ulong?)17199951585903149278,
                NullableUInt64ArrayMember = new ulong?[] { 8579538827650867297, 13437932537508109090, 14455068487067155481, 8620836904354161905, 1121855354283354527, null, 3573705380918398408, 15141003351481969785, 15122785163441282779, null },
                SingleMember = (float)0.1257098F,
                SingleArrayMember = new float[] { 0.71176F, 0.7397615F, 0.03607147F, 0.6793227F, 0.40345F, 0.6158766F, 0.7702345F, 0.898204F, 0.07001697F, 0.5951638F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { 0.0236166F, 0.9358631F, 0.8411625F, 0.8856737F, null, null, null, 0.8944417F, 0.2514144F, 0.7931466F },
                DoubleMember = (double)0.87276427,
                DoubleArrayMember = new double[] { 0.61529845, 0.18197392, 0.67009238, 0.23365717, 0.66277457, 0.71578762, 0.65839223, 0.54234437, 0.86744, 0.71686781 },
                NullableDoubleMember = (double?)0.11868407,
                NullableDoubleArrayMember = new double?[] { null, null, 0.94673321, null, null, 0.8155094, null, 0.56757427, 0.62165054, 0.21738126 },
                DecimalMember = (decimal)0.0989872264205419m,
                DecimalArrayMember = new decimal[] { 0.805286141953099m, 0.168049995865696m, 0.079843278080152m, 0.518679493814092m, 0.87324058537988m, 0.719010823741094m, 0.21755091949252m, 0.842909639162435m, 0.620201417999436m, 0.739704293543335m },
                NullableDecimalMember = (decimal?)0.504108274590274m,
                NullableDecimalArrayMember = new decimal?[] { null, null, null, 0.761358782537914m, 0.299088121065445m, null, null, 0.1559673320297m, 0.523113301733096m, 0.531985531808802m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { null, null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-152221925),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-57687436), DateTime.Now.AddSeconds(-295739962), DateTime.Now.AddSeconds(-102634131), DateTime.Now.AddSeconds(59577440), DateTime.Now.AddSeconds(89042472), DateTime.Now.AddSeconds(-160421386), DateTime.Now.AddSeconds(-119138096), DateTime.Now.AddSeconds(24572873), DateTime.Now.AddSeconds(262941155), DateTime.Now.AddSeconds(-170440033) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { null, DateTime.Now.AddSeconds(-85352389), DateTime.Now.AddSeconds(278345435), DateTime.Now.AddSeconds(-302993014), DateTime.Now.AddSeconds(265485742), DateTime.Now.AddSeconds(130271187), DateTime.Now.AddSeconds(92286960), null, DateTime.Now.AddSeconds(-221309962), DateTime.Now.AddSeconds(-281133360) },
            },
            new DataConvertersTestClass {
                StringMember = (string)";f7s19S7nv",
                StringArrayMember = new string[] { ";Z!bArjVpL", "M\"qt8q1\"lV", "NF.o8J1J8U", "; j76o5E.p", "ft7W0#,N7L", "IZWSi8ldp;", ";ZkVUNh6.T", "7xF@?rVn.?", "VnZwMozRY%", "Ccm$zFImCm" },
                CharMember = (char)'K',
                CharArrayMember = new char[] { '\'', ',', 'z', 'f', 'h', 'q', '6', 'I', '$', 'W' },
                NullableCharMember = (char?)'Q',
                NullableCharArrayMember = new char?[] { 'W', '2', 'm', null, null, ';', 'C', null, 'A', '\'' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, false, true, true, true, true, true, true, true, true },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { null, false, null, false, true, false, true, false, false, null },
                ByteMember = (byte)24,
                ByteArrayMember = new byte[] { 160, 75, 48, 233, 16, 14, 171, 108, 85, 204 },
                NullableByteMember = (byte?)177,
                NullableByteArrayMember = new byte?[] { 189, 145, 18, null, 177, null, null, 5, null, null },
                SByteMember = (sbyte)-79,
                SByteArrayMember = new sbyte[] { -28, -62, -66, -70, 95, -128, 50, 86, -14, -64 },
                NullableSByteMember = (sbyte?)-80,
                NullableSByteArrayMember = new sbyte?[] { -1, 61, null, -112, 87, 75, -26, null, 5, -32 },
                Int16Member = (short)17550,
                Int16ArrayMember = new short[] { 15483, -10678, 16107, -29164, -11691, -9761, -31703, -4447, 22750, 12068 },
                NullableInt16Member = (short?)8690,
                NullableInt16ArrayMember = new short?[] { -4893, -3452, 16969, 30851, null, -7542, null, -29669, 2993, null },
                UInt16Member = (ushort)11673,
                UInt16ArrayMember = new ushort[] { 49183, 39077, 39703, 3818, 51064, 50575, 43672, 5815, 64513, 7969 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 53174, 51304, null, null, 19817, 55273, 9187, 51096, 33261, null },
                Int32Member = (int)861754615,
                Int32ArrayMember = new int[] { -397333117, 1235807081, 228678986, -1977611077, -452925038, 647198894, -2054633027, 1516870979, 203330180, 902590225 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { 1073917592, null, 225119180, -1710521224, 593666090, null, 696862707, 488863763, -455145608, null },
                UInt32Member = (uint)593596025,
                UInt32ArrayMember = new uint[] { 1515911293, 3204165319, 236762255, 4019700521, 4190035906, 2483247026, 1570166007, 108804306, 2297916036, 1930473718 },
                NullableUInt32Member = (uint?)2673690855,
                NullableUInt32ArrayMember = new uint?[] { null, 4143610688, 644901071, 418603974, 372791020, 4178562239, 3403762383, null, 1442055156, 920797656 },
                Int64Member = (long)-1624831348033642119,
                Int64ArrayMember = new long[] { 6028680488110422093, 9044447847019952251, 8592388745272220339, -220875606497099636, 2359946673476938271, 2663034284845352146, 134646660574022813, 3496002756280495930, 8970760199150710729, -8294327148776987879 },
                NullableInt64Member = (long?)5531140838572343565,
                NullableInt64ArrayMember = new long?[] { null, null, null, null, -5086987493178309084, -7379121010404455150, null, -6953669143586477738, -4774292620475470866, 3868249385099972275 },
                UInt64Member = (ulong)18379014309221186783,
                UInt64ArrayMember = new ulong[] { 1958191642403038705, 14871329184508692477, 3719711982425289951, 12954643450815881803, 4174610633170703317, 5520793382143481163, 16044349821447251162, 15746332710982148038, 12434060196482897535, 9142300736716603204 },
                NullableUInt64Member = (ulong?)12311025918611036780,
                NullableUInt64ArrayMember = new ulong?[] { 2508245566320730610, null, 2858320759471658652, 1999393405411314087, 12516918557634307630, 16312553462010540703, null, 937776096636623336, null, null },
                SingleMember = (float)0.5225226F,
                SingleArrayMember = new float[] { 0.6642199F, 0.3890645F, 0.2057165F, 0.2385767F, 0.3973326F, 0.421503F, 0.3175956F, 0.44381F, 0.945236F, 0.3758506F },
                NullableSingleMember = (float?)0.1187852F,
                NullableSingleArrayMember = new float?[] { 0.3390606F, null, 0.537258F, 0.1668369F, 0.5089635F, 0.9386617F, 0.8885207F, null, 0.7457947F, 0.01159652F },
                DoubleMember = (double)0.05277011,
                DoubleArrayMember = new double[] { 0.88801122, 0.88454963, 0.6284059, 0.34846087, 0.19982379, 0.1127867, 0.11470369, 0.16959004, 0.75648309, 0.6041475 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.27949283, null, 0.42396521, null, 0.51509168, 0.29656066, null, 0.95592595, 0.43492157, 0.89576459 },
                DecimalMember = (decimal)0.936117451142574m,
                DecimalArrayMember = new decimal[] { 0.217008802209519m, 0.430357326953838m, 0.639145404863705m, 0.740935995122854m, 0.0100918142172004m, 0.743076100360172m, 0.761692472622587m, 0.0711910953145433m, 0.912716728128826m, 0.0311302510235134m },
                NullableDecimalMember = (decimal?)0.185642619238069m,
                NullableDecimalArrayMember = new decimal?[] { 0.214591689507752m, 0.0893134237682975m, null, null, 0.214140350098787m, 0.992375820871618m, 0.685149243420525m, 0.00265859905754151m, 0.658360024289396m, 0.970385286943235m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-90964609),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(270204758), DateTime.Now.AddSeconds(234013593), DateTime.Now.AddSeconds(181698576), DateTime.Now.AddSeconds(76134087), DateTime.Now.AddSeconds(-44982944), DateTime.Now.AddSeconds(212714058), DateTime.Now.AddSeconds(92445976), DateTime.Now.AddSeconds(-253385400), DateTime.Now.AddSeconds(-279406864), DateTime.Now.AddSeconds(-191912530) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-11878399),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-88027704), null, DateTime.Now.AddSeconds(-201985515), DateTime.Now.AddSeconds(-57348365), DateTime.Now.AddSeconds(-325614004), null, DateTime.Now.AddSeconds(126345343), DateTime.Now.AddSeconds(162041377), DateTime.Now.AddSeconds(155078908), DateTime.Now.AddSeconds(205853432) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"mEanbhTac;",
                StringArrayMember = new string[] { "n%pzsZA,I3", "lQh,ReLIS6", "i5lXaQM9U1", "UY,iiE.9bZ", "FUdVBVulV$", "WLItf4kRZb", "MNqeiMc dN", "rZ3'0NcPVE", "tyoXfJQRC0", "rsjAN'1 9?" },
                CharMember = (char)'C',
                CharArrayMember = new char[] { '!', 'O', 'v', 'Q', 'q', '@', '5', 'k', ',', 'J' },
                NullableCharMember = (char?)'P',
                NullableCharArrayMember = new char?[] { 'g', null, 'N', 'r', 'T', null, 'U', 'C', 'x', 'w' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, false, true, false, true, true, false, true, false, true },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { null, true, false, true, false, false, true, null, true, false },
                ByteMember = (byte)125,
                ByteArrayMember = new byte[] { 239, 57, 97, 243, 195, 246, 204, 179, 239, 138 },
                NullableByteMember = (byte?)50,
                NullableByteArrayMember = new byte?[] { 179, 24, 181, 137, 41, 218, null, null, null, 224 },
                SByteMember = (sbyte)-43,
                SByteArrayMember = new sbyte[] { -112, 30, 45, -24, 115, -83, 51, 48, -21, -114 },
                NullableSByteMember = (sbyte?)-39,
                NullableSByteArrayMember = new sbyte?[] { -97, -6, -24, null, -4, -49, null, -11, 82, null },
                Int16Member = (short)-5713,
                Int16ArrayMember = new short[] { 27675, -30629, -5537, 7049, -3184, -16386, -25713, -30260, 18588, 2982 },
                NullableInt16Member = (short?)21629,
                NullableInt16ArrayMember = new short?[] { null, 15874, 1826, -12483, null, -4836, null, null, null, -7947 },
                UInt16Member = (ushort)63490,
                UInt16ArrayMember = new ushort[] { 65357, 53446, 21844, 38266, 2650, 25814, 53934, 2498, 23948, 65346 },
                NullableUInt16Member = (ushort?)41170,
                NullableUInt16ArrayMember = new ushort?[] { null, null, null, 7255, null, null, 28721, null, null, null },
                Int32Member = (int)-1193340035,
                Int32ArrayMember = new int[] { 89862712, 1913851147, -768850458, -1158897335, -1148471366, -263744839, 746454921, -1934883147, 2043439747, 477970839 },
                NullableInt32Member = (int?)-253848928,
                NullableInt32ArrayMember = new int?[] { 703751471, -14448699, 192703466, -1730101785, null, 578016478, 417427257, null, 399544831, null },
                UInt32Member = (uint)2463234332,
                UInt32ArrayMember = new uint[] { 1528646960, 636681081, 1948281225, 3403940203, 396789636, 4267692367, 336837826, 3312312149, 3318250746, 3462918066 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 3788740971, null, 2494699357, 3982377913, 3723017625, null, 4000996347, null, null, 4225179797 },
                Int64Member = (long)-8851866077386916516,
                Int64ArrayMember = new long[] { -4372122780058590979, -3873641731868472685, 2708687889581314650, -8053796664466567973, -7311662682616387165, 5536725966578105722, -3730003088274146433, 7505691723035644233, -7403125156725110386, 8373896635541768970 },
                NullableInt64Member = (long?)7874151538139022065,
                NullableInt64ArrayMember = new long?[] { 2995994086695007779, null, -6262617052617891390, 5757917741469871536, null, null, null, -8397813847187699875, -7815552187977337353, -7212460118754851804 },
                UInt64Member = (ulong)3710953693253558344,
                UInt64ArrayMember = new ulong[] { 3822181597422533190, 13985597822806160766, 17949289714969034035, 18336894775734913934, 14118902026240470357, 8449648142020259363, 10902660159532993353, 14019930733300051361, 14423800592728250363, 6900228795057605196 },
                NullableUInt64Member = (ulong?)16090614895755326959,
                NullableUInt64ArrayMember = new ulong?[] { 7322384646484470226, null, 1958779276199023244, null, null, 14660860318965541655, 18036075171626964095, 12877431131525421054, null, null },
                SingleMember = (float)0.1520356F,
                SingleArrayMember = new float[] { 0.7816843F, 0.9770879F, 0.3756814F, 0.4057971F, 0.2060994F, 0.3464458F, 0.7491456F, 0.6262839F, 0.5929585F, 0.6182675F },
                NullableSingleMember = (float?)0.8959266F,
                NullableSingleArrayMember = new float?[] { 0.6443428F, 0.4427071F, 0.6489575F, 0.1304682F, 0.6673554F, 0.01355909F, null, null, null, null },
                DoubleMember = (double)0.61439965,
                DoubleArrayMember = new double[] { 0.56074345, 0.61200663, 0.93781934, 0.49840636, 0.60562212, 0.65433315, 0.98289037, 0.08846192, 0.91811055, 0.91886028 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { null, 0.19025013, 0.09693795, 0.53601657, 0.98713553, 0.46390942, 0.20986355, null, 0.52405037, 0.67247994 },
                DecimalMember = (decimal)0.995566192080996m,
                DecimalArrayMember = new decimal[] { 0.0436511864157632m, 0.187025291932293m, 0.281433468349945m, 0.625900836952916m, 0.189844091045598m, 0.100558888679631m, 0.42685105345531m, 0.494016460373074m, 0.611845919681641m, 0.721690588966799m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.730228531514401m, 0.113445501827377m, 0.130474838023295m, 0.0666539082614025m, null, 0.157503264098197m, 0.259122145948523m, 0.415169360309452m, 0.828006158037114m, 0.989155401470678m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-289111424),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-11231281), DateTime.Now.AddSeconds(-44293767), DateTime.Now.AddSeconds(3905763), DateTime.Now.AddSeconds(-90644791), DateTime.Now.AddSeconds(49195113), DateTime.Now.AddSeconds(161487071), DateTime.Now.AddSeconds(-148440571), DateTime.Now.AddSeconds(309630555), DateTime.Now.AddSeconds(-296963079), DateTime.Now.AddSeconds(-164100542) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(326413849),
                NullableDateTimeArrayMember = new DateTime?[] { null, null, DateTime.Now.AddSeconds(-86285441), null, DateTime.Now.AddSeconds(-260943285), null, DateTime.Now.AddSeconds(-50558451), DateTime.Now.AddSeconds(-115350605), null, DateTime.Now.AddSeconds(-294646768) },
            },
        };
    }
}
