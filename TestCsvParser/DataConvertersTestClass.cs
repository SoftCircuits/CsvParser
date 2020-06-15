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
                StringMember = (string)"?Mjc$N\"$?4",
                StringArrayMember = new string[] { "CE5Ms8rbj,", "A Vw@SEY n", "kn?dU y;4K", "'xLnNiP##4", "3rLnG?:.g1", "Jr$ZTlPc:t", "K1uY#a,4ik", "DHcS7\"cYWf", "H.gCAXz9Uz", "S9FZuC$9r3" },
                CharMember = (char)'$',
                CharArrayMember = new char[] { 'E', 'e', 'R', 'l', ',', 'i', 'U', 'l', 'Z', 'J' },
                NullableCharMember = (char?)'u',
                NullableCharArrayMember = new char?[] { 'u', 'F', 'y', 'W', 'A', 'n', 's', 'L', null, '8' },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, true, true, false, false, true, true, true, false, false },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { true, true, false, null, true, true, null, false, null, true },
                ByteMember = (byte)82,
                ByteArrayMember = new byte[] { 214, 192, 135, 107, 99, 235, 24, 242, 237, 28 },
                NullableByteMember = (byte?)37,
                NullableByteArrayMember = new byte?[] { 13, 92, null, 159, 36, null, null, null, 61, 80 },
                SByteMember = (sbyte)92,
                SByteArrayMember = new sbyte[] { 112, -106, -88, -15, 111, 65, 24, -109, 31, 86 },
                NullableSByteMember = (sbyte?)4,
                NullableSByteArrayMember = new sbyte?[] { -37, -71, 119, -54, null, null, -107, -53, -112, -55 },
                Int16Member = (short)-23286,
                Int16ArrayMember = new short[] { -21695, -28897, 30051, -16600, 8106, -4151, -19564, 1592, -3997, 16978 },
                NullableInt16Member = (short?)-28264,
                NullableInt16ArrayMember = new short?[] { -31283, 4945, 16511, -17436, null, -28729, null, -18788, -9131, null },
                UInt16Member = (ushort)43307,
                UInt16ArrayMember = new ushort[] { 53987, 46249, 44246, 23906, 34282, 49736, 40072, 23259, 34287, 35361 },
                NullableUInt16Member = (ushort?)7790,
                NullableUInt16ArrayMember = new ushort?[] { 7071, null, null, 9418, 41986, 30042, 7020, 63139, 45516, 38527 },
                Int32Member = (int)777893731,
                Int32ArrayMember = new int[] { -1198987373, 1636011534, 885699885, 1758895083, -1365712291, -830878760, 1992345225, -402115622, -1395767148, -421797266 },
                NullableInt32Member = (int?)691809958,
                NullableInt32ArrayMember = new int?[] { 1778186810, 1454251905, 1194310253, null, -508757626, -114700894, 843624966, -1933466564, null, -913861519 },
                UInt32Member = (uint)1436346500,
                UInt32ArrayMember = new uint[] { 3851163824, 1796212050, 436192371, 2095736926, 1432594579, 113400645, 600853043, 822656789, 1302573018, 1015967611 },
                NullableUInt32Member = (uint?)2568199674,
                NullableUInt32ArrayMember = new uint?[] { 3822951273, 256345564, 145917269, 3149092588, null, 10339502, 1265509913, 3777064367, null, null },
                Int64Member = (long)9053755377891870209,
                Int64ArrayMember = new long[] { -8612721916284939365, -1683075836859244259, -1892347880132254805, -789652349214557381, -3065803083169833730, 9089442882252384530, -7720128373712511390, -8779709798298765418, -7525004644681646057, 846680215177030511 },
                NullableInt64Member = (long?)-9194432473583262844,
                NullableInt64ArrayMember = new long?[] { null, -6749486411924983394, null, -7009507732053927920, 7444713217906348648, -1876995697674276939, 5076010194735340298, 6997405344012481361, -1300245555348743513, -5859756948609729432 },
                UInt64Member = (ulong)8875207982935095828,
                UInt64ArrayMember = new ulong[] { 10158900645948120772, 7187329907549269745, 14299548283139961467, 241934317731064893, 1043771714446778218, 10204736738465974706, 10454388523526990844, 7823050877479449580, 4866883024121102227, 13602928678786173750 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 11622373416791101423, 10319990453249261413, 17058082471502579043, 2272791950259913272, null, 298493981353177897, null, 10234809243124884579, 10493124950304442925, 893562334611555074 },
                SingleMember = (float)0.6591F,
                SingleArrayMember = new float[] { 0.521F, 0.2509F, 0.3645F, 0.5289F, 0.358F, 0.7027F, 0.5673F, 0.2706F, 0.2661F, 0.6759F },
                NullableSingleMember = (float?)0.2175F,
                NullableSingleArrayMember = new float?[] { null, 0.5304F, null, null, 0.4243F, 0.2029F, 0.3354F, 0.9351F, 0.3894F, 0.3099F },
                DoubleMember = (double)0.6578,
                DoubleArrayMember = new double[] { 0.8432, 0.4049, 0.0164, 0.3607, 0.9207, 0.0315, 0.0023, 0.2822, 0.9919, 0.0427 },
                NullableDoubleMember = (double?)0.4341,
                NullableDoubleArrayMember = new double?[] { null, null, 0.4263, 0.4595, 0.6763, null, 0.7179, null, 0.2695, 0.1926 },
                DecimalMember = (decimal)0.127706090979141m,
                DecimalArrayMember = new decimal[] { 0.818142530423655m, 0.66653647677346m, 0.15159828641992m, 0.96714494841506m, 0.0349767212918851m, 0.375027870468343m, 0.346736667839222m, 0.0335591044433224m, 0.394069489740799m, 0.580773416711378m },
                NullableDecimalMember = (decimal?)0.0902753412212596m,
                NullableDecimalArrayMember = new decimal?[] { 0.4923173284588m, 0.804464420212649m, 0.113761836715863m, 0.0456453487489584m, 0.249974899110373m, 0.591755128275489m, 0.224203495878821m, 0.0849268874548966m, null, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(115873893),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-152564363), DateTime.Now.AddSeconds(80803294), DateTime.Now.AddSeconds(55434981), DateTime.Now.AddSeconds(159007631), DateTime.Now.AddSeconds(203173471), DateTime.Now.AddSeconds(-140262291), DateTime.Now.AddSeconds(-115062856), DateTime.Now.AddSeconds(349546309), DateTime.Now.AddSeconds(82953904), DateTime.Now.AddSeconds(-153232380) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(92466094),
                NullableDateTimeArrayMember = new DateTime?[] { null, DateTime.Now.AddSeconds(-102750739), DateTime.Now.AddSeconds(-253358173), null, DateTime.Now.AddSeconds(63516893), DateTime.Now.AddSeconds(-314698666), DateTime.Now.AddSeconds(247402828), DateTime.Now.AddSeconds(-121356488), null, DateTime.Now.AddSeconds(-296529017) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"BKb#xCP2pa",
                StringArrayMember = new string[] { "J;FjlWUrYv", "xCj6RQBCv5", "@p4qe#jkc%", "I$U!pb4s.X", "4cl@F%xr1g", "AMH7eqJmFp", ".7Pamz5pQ;", "jfOXApE8kf", "W:6@$#;jn ", "SSnVj$NLgc" },
                CharMember = (char)'G',
                CharArrayMember = new char[] { 'w', 'Z', 'o', 'G', '6', '6', '5', 'd', 'D', 'm' },
                NullableCharMember = (char?)'P',
                NullableCharArrayMember = new char?[] { 'R', 'g', null, null, null, ':', null, 'D', '0', 'S' },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, true, false, false, false, true, false, false, false, true },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { null, false, null, true, true, false, false, false, null, false },
                ByteMember = (byte)77,
                ByteArrayMember = new byte[] { 205, 160, 172, 65, 247, 147, 19, 239, 3, 74 },
                NullableByteMember = (byte?)185,
                NullableByteArrayMember = new byte?[] { 5, 196, null, 175, 24, 252, 221, 127, 7, null },
                SByteMember = (sbyte)-55,
                SByteArrayMember = new sbyte[] { -17, 63, -59, -60, -59, -92, 38, -105, 30, -125 },
                NullableSByteMember = (sbyte?)-59,
                NullableSByteArrayMember = new sbyte?[] { -61, -5, null, 49, null, -10, -9, null, -109, 35 },
                Int16Member = (short)-30993,
                Int16ArrayMember = new short[] { 21626, -30565, -6713, 6825, -16083, -9763, -21290, -24668, 12169, -1872 },
                NullableInt16Member = (short?)-25439,
                NullableInt16ArrayMember = new short?[] { null, 9349, -25417, -21229, -20072, -1864, -17647, null, 16562, null },
                UInt16Member = (ushort)14988,
                UInt16ArrayMember = new ushort[] { 11120, 27296, 45940, 5231, 36026, 9772, 24863, 49112, 56803, 58497 },
                NullableUInt16Member = (ushort?)1561,
                NullableUInt16ArrayMember = new ushort?[] { 30875, 61037, 40864, 45968, 14946, 1702, 36530, 54908, null, 48625 },
                Int32Member = (int)1057485542,
                Int32ArrayMember = new int[] { 395555508, -1875566760, 764880967, 2049208274, 706962797, -1150413520, 1695912713, -1330112476, -935007547, 317692531 },
                NullableInt32Member = (int?)-174563924,
                NullableInt32ArrayMember = new int?[] { 225444263, 249417345, null, 163909336, -428855445, 123415272, null, -1199324656, 351039941, -499663849 },
                UInt32Member = (uint)2871800625,
                UInt32ArrayMember = new uint[] { 4149125267, 109493750, 1491707130, 3619345098, 3278430783, 2529417310, 646977702, 2149885441, 246162062, 1695196789 },
                NullableUInt32Member = (uint?)3567743775,
                NullableUInt32ArrayMember = new uint?[] { 1562791586, null, 3125635275, 769544375, null, null, 2814994816, 3633930667, 476397104, 4289872 },
                Int64Member = (long)-4326862641414607230,
                Int64ArrayMember = new long[] { -398454108054768641, -2171705228212352026, 5952159118923507571, 8380487308507703161, -10310279891079756, -858558693382815229, -5867019490495057902, 7938521384837498393, 1443113409170052862, -5699362261772596532 },
                NullableInt64Member = (long?)1768675068971194961,
                NullableInt64ArrayMember = new long?[] { null, -3410677678323643884, null, null, null, 8460119454161545499, 7184381813179966687, null, null, 6128886315798899486 },
                UInt64Member = (ulong)11303040547331618622,
                UInt64ArrayMember = new ulong[] { 18197889291414885384, 1350123130447435143, 14244567996075961061, 15253611696390519642, 2293938548459668178, 11905243863645434935, 2989479160281003770, 15057285940307486373, 3467207755191191525, 7894262465081500308 },
                NullableUInt64Member = (ulong?)364613022583248756,
                NullableUInt64ArrayMember = new ulong?[] { 3187983755162816023, 9136495206036948655, null, null, 7557697292180428614, 18307105801930943048, null, 16473929797768490467, null, 13150250822372302320 },
                SingleMember = (float)0.4891F,
                SingleArrayMember = new float[] { 0.3826F, 0.0803F, 0.0524F, 0.2858F, 0.0503F, 0.458F, 0.4039F, 0.772F, 0.1996F, 0.9427F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { 0.9477F, null, 0.0325F, null, 0.9842F, 0.8206F, 0.6332F, 0.4562F, 0.9155F, null },
                DoubleMember = (double)0.5568,
                DoubleArrayMember = new double[] { 0.6381, 0.7453, 0.255, 0.6524, 0.6243, 0.1593, 0.7815, 0.509, 0.2771, 0.405 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.24, 0.7571, null, 0.0682, null, 0.5424, 0.7086, 0.6635, 0.9726, 0.4694 },
                DecimalMember = (decimal)0.210180691075595m,
                DecimalArrayMember = new decimal[] { 0.613377295720101m, 0.233925889355096m, 0.249221176956418m, 0.33715162022838m, 0.00556018250321978m, 0.838675910997519m, 0.988129324274198m, 0.227207371605191m, 0.118553945384246m, 0.832257701471568m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { null, null, null, 0.76065730432079m, 0.555373898500285m, null, null, 0.424208687815912m, 0.415364561330231m, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(327886245),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(258316601), DateTime.Now.AddSeconds(278277280), DateTime.Now.AddSeconds(-77059362), DateTime.Now.AddSeconds(-135378099), DateTime.Now.AddSeconds(-125294072), DateTime.Now.AddSeconds(79133976), DateTime.Now.AddSeconds(260880736), DateTime.Now.AddSeconds(-139680981), DateTime.Now.AddSeconds(-309492009), DateTime.Now.AddSeconds(45800614) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { null, DateTime.Now.AddSeconds(219320228), DateTime.Now.AddSeconds(204770186), DateTime.Now.AddSeconds(77803419), null, null, DateTime.Now.AddSeconds(-294744569), DateTime.Now.AddSeconds(-318287743), DateTime.Now.AddSeconds(-187865698), null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"QIa,#r%66,",
                StringArrayMember = new string[] { "lXBxT@BTcq", "9VCRuR'',D", "b\"cIONn!%c", "%H0yk@Q;Co", "#!5;q@GJQ2", "zF@2js!'iF", "eGUAs1ra3z", "AB;WHhLlpT", "He1T0'M$O'", "AXf#J?w4V," },
                CharMember = (char)'U',
                CharArrayMember = new char[] { 'J', '?', 'R', 'Y', 'K', '%', ':', 'G', ';', 'W' },
                NullableCharMember = (char?)'\'',
                NullableCharArrayMember = new char?[] { 'T', null, ':', 'm', null, 'm', 'v', 'O', 'c', null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, true, false, true, true, false, true, true, false, true },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { true, false, null, false, false, false, false, null, false, false },
                ByteMember = (byte)37,
                ByteArrayMember = new byte[] { 168, 198, 164, 31, 122, 79, 226, 53, 120, 163 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { null, null, 101, 80, 186, 107, null, null, 214, null },
                SByteMember = (sbyte)84,
                SByteArrayMember = new sbyte[] { 8, -103, -43, 13, -10, 67, 57, -106, -18, -108 },
                NullableSByteMember = (sbyte?)96,
                NullableSByteArrayMember = new sbyte?[] { -58, null, null, 80, null, -52, -58, null, null, 114 },
                Int16Member = (short)4592,
                Int16ArrayMember = new short[] { -13510, -15699, 9718, -18472, 20282, -16318, 25542, -11888, 11466, 4875 },
                NullableInt16Member = (short?)-11846,
                NullableInt16ArrayMember = new short?[] { -6223, -27030, -24584, null, -11908, null, -23114, -21767, 16054, -22696 },
                UInt16Member = (ushort)5282,
                UInt16ArrayMember = new ushort[] { 11943, 9303, 52930, 56315, 9729, 60223, 31629, 44431, 23955, 37544 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { null, 30566, null, 62114, 48380, null, null, 23394, 27552, 1483 },
                Int32Member = (int)-2069505048,
                Int32ArrayMember = new int[] { 153783450, 674054551, -692179186, 193529108, -410379331, -644017554, 1918098420, 209650744, 1164760709, 1110050635 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { 1043153335, 606876177, -2028552623, -306441136, -1297151248, null, -1245756519, 1539333192, null, 1078131638 },
                UInt32Member = (uint)3033833380,
                UInt32ArrayMember = new uint[] { 854709541, 925652413, 656349747, 687574378, 1986853754, 1410218184, 1744893375, 3026280428, 3533944235, 1151564084 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 4098172156, null, null, 866682137, null, 1922877421, 3544957781, 3937762411, 676856307, 267009021 },
                Int64Member = (long)8430558363281259661,
                Int64ArrayMember = new long[] { 3854636731140125454, -2923690712672580634, -6681967766975130378, 8445461290925101906, -5538167816220993231, -7526491189732459092, 3295807569802476431, -7500413661130316945, 9016413506573276311, -3854959243924659409 },
                NullableInt64Member = (long?)-2584238953119825584,
                NullableInt64ArrayMember = new long?[] { null, null, -8197029800528803422, -3791565098723358884, 6443580224258048047, -2698363337469418439, null, 660788836777928389, null, -1135109307393722738 },
                UInt64Member = (ulong)9543716764399961629,
                UInt64ArrayMember = new ulong[] { 474791424971544239, 8072119012245431549, 12095094511490052998, 11445836385884330344, 2588240545058347488, 3403659755085939591, 5126301387584737025, 3853309881623456956, 4566927606416798220, 3324865127129481543 },
                NullableUInt64Member = (ulong?)3862023825952464817,
                NullableUInt64ArrayMember = new ulong?[] { 18035458795644970060, null, 17463645640507071956, null, 8995160510752583695, 11780201965144590295, null, null, 15778360579712498770, 15914631110415932125 },
                SingleMember = (float)0.5938F,
                SingleArrayMember = new float[] { 0.2613F, 0.3133F, 0.6434F, 0.9606F, 0.777F, 0.2933F, 0.7471F, 0.1651F, 0.2791F, 0.8827F },
                NullableSingleMember = (float?)0.062F,
                NullableSingleArrayMember = new float?[] { 0.8037F, null, 0.9305F, 0.896F, 0.9026F, null, null, 0.2209F, 0.7121F, null },
                DoubleMember = (double)0.2264,
                DoubleArrayMember = new double[] { 0.6512, 0.353, 0.8096, 0.7254, 0.3538, 0.2974, 0.5122, 0.7818, 0.9212, 0.4187 },
                NullableDoubleMember = (double?)0.9173,
                NullableDoubleArrayMember = new double?[] { 0.0545, 0.7466, 0.1987, 0.0767, 0.2014, 0.4984, 0.4249, 0.3152, 0.4999, 0.0946 },
                DecimalMember = (decimal)0.844435845894942m,
                DecimalArrayMember = new decimal[] { 0.559843158144431m, 0.770346783925941m, 0.810773225413064m, 0.25409212627173m, 0.261809780384325m, 0.48247088700648m, 0.830723093278111m, 0.370592725635782m, 0.43607471810471m, 0.856349451866164m },
                NullableDecimalMember = (decimal?)0.15516570823042m,
                NullableDecimalArrayMember = new decimal?[] { 0.96578850921513m, 0.886554465110672m, 0.21338764727739m, 0.911057308274814m, 0.971689347630222m, 0.46354619155803m, 0.529552988954612m, 0.0534380809652796m, 0.189168607904189m, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(158032152),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(83235098), DateTime.Now.AddSeconds(-25470042), DateTime.Now.AddSeconds(270364545), DateTime.Now.AddSeconds(-6928267), DateTime.Now.AddSeconds(296075326), DateTime.Now.AddSeconds(105067658), DateTime.Now.AddSeconds(-61548982), DateTime.Now.AddSeconds(-270893346), DateTime.Now.AddSeconds(-180474545), DateTime.Now.AddSeconds(-160346283) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(168115285),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(236322808), null, null, DateTime.Now.AddSeconds(-55700679), DateTime.Now.AddSeconds(-220429993), DateTime.Now.AddSeconds(-128997655), null, DateTime.Now.AddSeconds(-112701185), DateTime.Now.AddSeconds(239577599), DateTime.Now.AddSeconds(-248860698) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"a2co1xOvvC",
                StringArrayMember = new string[] { "gfaO%AbBXz", "puL,y0t\"NR", "7dQ0'bBMpC", "1nYMir$Q%J", "kvhtV3qo?B", "EUieRDPwVt", "%OiV$BV dH", "3d@D3L#ufM", "7kXn$gF!@e", "cX!cLWMMvT" },
                CharMember = (char)'5',
                CharArrayMember = new char[] { 'F', 'C', 'm', 'I', '"', 'x', 'k', ':', 'x', 'T' },
                NullableCharMember = (char?)'a',
                NullableCharArrayMember = new char?[] { 'L', null, '@', 'i', 'g', 'R', 'm', null, null, null },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, true, true, true, false, true, true, true, true },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { true, false, false, null, true, true, false, false, true, true },
                ByteMember = (byte)114,
                ByteArrayMember = new byte[] { 99, 201, 19, 243, 231, 94, 37, 40, 241, 128 },
                NullableByteMember = (byte?)55,
                NullableByteArrayMember = new byte?[] { null, null, null, 28, 112, null, null, null, 47, 76 },
                SByteMember = (sbyte)76,
                SByteArrayMember = new sbyte[] { 110, -64, -111, 105, -14, 73, -6, 79, 110, -79 },
                NullableSByteMember = (sbyte?)12,
                NullableSByteArrayMember = new sbyte?[] { 9, 28, null, -85, null, 126, -63, null, 8, null },
                Int16Member = (short)-6092,
                Int16ArrayMember = new short[] { -9708, 7901, -1409, 4470, 32271, 8302, 22156, -29278, -6410, -29249 },
                NullableInt16Member = (short?)25973,
                NullableInt16ArrayMember = new short?[] { 4345, 29312, -15539, null, null, null, null, null, -10499, null },
                UInt16Member = (ushort)29351,
                UInt16ArrayMember = new ushort[] { 62115, 18177, 24887, 5268, 14864, 61640, 44272, 44900, 59481, 10888 },
                NullableUInt16Member = (ushort?)1004,
                NullableUInt16ArrayMember = new ushort?[] { 21037, 15805, 55567, 54516, 23325, 26647, 20718, null, 29546, 63163 },
                Int32Member = (int)-1145905397,
                Int32ArrayMember = new int[] { 1457897671, 1416491771, -1850950193, 2023878010, -596742674, 743792904, -1067539309, 719616683, 2106193916, -1010504268 },
                NullableInt32Member = (int?)-1358721549,
                NullableInt32ArrayMember = new int?[] { -916862927, -1294569314, 1472379243, null, -2116528727, -1004247934, null, -736798293, null, 1737913033 },
                UInt32Member = (uint)3997015242,
                UInt32ArrayMember = new uint[] { 2159663571, 196528460, 4007265943, 1797816297, 3634591961, 1334060369, 2985202698, 732401116, 4202222615, 3945786111 },
                NullableUInt32Member = (uint?)2130336541,
                NullableUInt32ArrayMember = new uint?[] { null, 897454718, null, null, 1767222685, 772070072, null, null, null, 1798178324 },
                Int64Member = (long)-1583607381483489898,
                Int64ArrayMember = new long[] { 7926834579735775488, 3019791852929719302, -8198280761215261571, -3770442021886190532, -4042095456575617239, 2468094411763949493, 2653889068841358221, 6014787660510060751, -1833027872291835153, -3619560895483670403 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { null, 5940866554510319137, 1697291527807923836, -5333501459444578921, null, null, null, -911638800992627636, -1600459665937569419, null },
                UInt64Member = (ulong)5308046099934357534,
                UInt64ArrayMember = new ulong[] { 2886722369487057415, 4887290104060720595, 2683540165433339642, 5009361559248740306, 3033883000607688992, 18043730518182236106, 3367193689876248917, 18400841855890837236, 8556564217241898416, 11301720292062329491 },
                NullableUInt64Member = (ulong?)10336731351370419594,
                NullableUInt64ArrayMember = new ulong?[] { null, null, 13890825507810575872, 8851697025919428414, null, 3334950312730851118, 10193205260447322650, 7131709593462819924, 12226944206867350810, null },
                SingleMember = (float)0.9553F,
                SingleArrayMember = new float[] { 0.8296F, 0.2094F, 0.5425F, 0.8285F, 0.1782F, 0.9383F, 0.6294F, 0.7002F, 0.3118F, 0.1707F },
                NullableSingleMember = (float?)0.2911F,
                NullableSingleArrayMember = new float?[] { 0.2806F, 0.9118F, 0.0348F, 0.1516F, null, null, 0.0196F, 0.6912F, 0.8404F, 0.7397F },
                DoubleMember = (double)0.0396,
                DoubleArrayMember = new double[] { 0.0507, 0.7538, 0.9874, 0.1201, 0.5146, 0.3181, 0.4598, 0.9582, 0.2516, 0.1469 },
                NullableDoubleMember = (double?)0.5203,
                NullableDoubleArrayMember = new double?[] { null, null, null, 0.9169, 0.7739, 0.3927, 0.4498, 0.3112, 0.929, 0.1969 },
                DecimalMember = (decimal)0.833856615626186m,
                DecimalArrayMember = new decimal[] { 0.768983988915097m, 0.161124336608278m, 0.278832752853088m, 0.217782540348257m, 0.416434146192127m, 0.864461819112516m, 0.570447921087243m, 0.10838847472723m, 0.508194497557448m, 0.295802323285398m },
                NullableDecimalMember = (decimal?)0.16203238310387m,
                NullableDecimalArrayMember = new decimal?[] { 0.846318163837454m, null, null, 0.292157973764538m, 0.618871941053714m, 0.657110323038469m, 0.588153810514209m, null, null, 0.732256791429714m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-108771620),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-29242287), DateTime.Now.AddSeconds(-287428375), DateTime.Now.AddSeconds(347220917), DateTime.Now.AddSeconds(-247151417), DateTime.Now.AddSeconds(127083920), DateTime.Now.AddSeconds(65978033), DateTime.Now.AddSeconds(125436068), DateTime.Now.AddSeconds(324306960), DateTime.Now.AddSeconds(-18364944), DateTime.Now.AddSeconds(-125772567) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(35244731), null, null, DateTime.Now.AddSeconds(20890597), DateTime.Now.AddSeconds(78921379), DateTime.Now.AddSeconds(285574057), null, DateTime.Now.AddSeconds(-47656950), DateTime.Now.AddSeconds(-137767247), DateTime.Now.AddSeconds(-291205199) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"hrL5VyZexF",
                StringArrayMember = new string[] { "HrRJ8l:@Xo", "@aD%1giq'h", " 6 L1QZpSW", "GyT?X7F8qw", "eR,GXHnMD?", "P6lQGXsGes", "MZzBiUHNx!", "ax SC4@\".u", "zMldGWNC6A", "P d%kEp9uD" },
                CharMember = (char)'o',
                CharArrayMember = new char[] { '#', '1', 'l', 't', 'H', 'Q', '?', 'c', '9', 'Z' },
                NullableCharMember = (char?)'8',
                NullableCharArrayMember = new char?[] { 'F', 'L', null, 'D', 'y', 'J', '1', null, '7', 't' },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, false, false, true, true, true, false, false, false, true },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { null, false, null, false, false, null, null, null, false, true },
                ByteMember = (byte)83,
                ByteArrayMember = new byte[] { 185, 22, 55, 112, 72, 214, 219, 127, 107, 100 },
                NullableByteMember = (byte?)132,
                NullableByteArrayMember = new byte?[] { 79, null, null, null, null, 69, 205, null, null, null },
                SByteMember = (sbyte)81,
                SByteArrayMember = new sbyte[] { 45, 126, -3, 79, 107, -89, -82, -125, -71, -19 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { 41, null, -123, -114, -72, null, 51, -20, 111, null },
                Int16Member = (short)-22721,
                Int16ArrayMember = new short[] { 21030, 7640, 4882, 21680, -6287, 28582, 29854, 9609, -28049, -24843 },
                NullableInt16Member = (short?)-21967,
                NullableInt16ArrayMember = new short?[] { 29127, 17206, null, 14784, null, 8192, null, -16019, null, -17445 },
                UInt16Member = (ushort)31970,
                UInt16ArrayMember = new ushort[] { 61309, 61366, 6794, 61413, 55255, 10957, 23278, 55292, 37626, 11773 },
                NullableUInt16Member = (ushort?)56001,
                NullableUInt16ArrayMember = new ushort?[] { 15282, 65511, 19586, 6492, null, null, 16787, 36903, null, null },
                Int32Member = (int)-1169872671,
                Int32ArrayMember = new int[] { 304235813, 795618042, 725043669, 1672426797, -179305164, -1835172412, -453493735, 768034301, 1030670227, -674688446 },
                NullableInt32Member = (int?)-1664384133,
                NullableInt32ArrayMember = new int?[] { 868455503, null, null, -1482099377, null, null, -1875736783, -966355190, null, -1183302878 },
                UInt32Member = (uint)2763475182,
                UInt32ArrayMember = new uint[] { 3515468340, 2658186193, 844453726, 2764781490, 293041302, 2622549806, 2009883343, 155587555, 3798581314, 77895628 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 2064167632, 3565829084, 1148626641, null, 4175851265, null, 4075834384, null, 1926299351, null },
                Int64Member = (long)-2460130036944195956,
                Int64ArrayMember = new long[] { -7188070100186694289, 2608930328817263152, 3499935096769290950, -9056943978732220213, -7988807192435918508, -4251901737788949963, 3130150182109284601, -1572501967506926997, -5119228265908706797, -7688790206935316224 },
                NullableInt64Member = (long?)634024227697519433,
                NullableInt64ArrayMember = new long?[] { 5696169278070889967, -6807310542858365310, -9209702605551354519, -1627824130757368762, -3936784726253002545, 7190290917273274474, -4148581417362230918, -5171198519045545941, -4120656080989182410, -1296140486110219357 },
                UInt64Member = (ulong)11924110053429815322,
                UInt64ArrayMember = new ulong[] { 15707735737618703005, 4676136504615824677, 127817678901030562, 11953281993814035843, 3434939103317029252, 2403932442560020955, 9522025825048401988, 10319040577850537948, 5212510479249454603, 589935566852176258 },
                NullableUInt64Member = (ulong?)5439394134757220409,
                NullableUInt64ArrayMember = new ulong?[] { 4939395957482287993, null, null, 8164809330625181549, 12139313370591919166, null, 8614508766515375514, 13979797306468282964, 9046857588195174601, 69015124214733092 },
                SingleMember = (float)0.1085F,
                SingleArrayMember = new float[] { 0.5812F, 0.3001F, 0.2098F, 0.4495F, 0.6733F, 0.5299F, 0.2334F, 0.6076F, 0.8654F, 0.8522F },
                NullableSingleMember = (float?)0.6188F,
                NullableSingleArrayMember = new float?[] { null, 0.4598F, 0.2563F, null, 0.1938F, null, 0.2954F, null, 0.1008F, null },
                DoubleMember = (double)0.7929,
                DoubleArrayMember = new double[] { 0.4214, 0.232, 0.0794, 0.8777, 0.7267, 0.3007, 0.7269, 0.6265, 0.7787, 0.8806 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.8493, 0.302, 0.3892, 0.3827, 0.072, 0.4128, null, null, 0.304, 0.8088 },
                DecimalMember = (decimal)0.286744058731359m,
                DecimalArrayMember = new decimal[] { 0.777285012312832m, 0.339124473435397m, 0.246906110200522m, 0.454683150842173m, 0.151704546134781m, 0.386181719315323m, 0.0466304179498136m, 0.686955756362041m, 0.582122175294031m, 0.470120318918545m },
                NullableDecimalMember = (decimal?)0.642939726190148m,
                NullableDecimalArrayMember = new decimal?[] { 0.402862810717366m, 0.787698285089665m, 0.688450048998208m, 0.181933845012418m, 0.34343448111016m, 0.877218958864556m, 0.539974222211155m, 0.472394072670673m, 0.625852085941402m, 0.729821564969524m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-244227082),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-203367046), DateTime.Now.AddSeconds(215490059), DateTime.Now.AddSeconds(85389848), DateTime.Now.AddSeconds(327967351), DateTime.Now.AddSeconds(278297074), DateTime.Now.AddSeconds(218896003), DateTime.Now.AddSeconds(250026263), DateTime.Now.AddSeconds(170894367), DateTime.Now.AddSeconds(130289429), DateTime.Now.AddSeconds(-240761112) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(228664603),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(312664265), DateTime.Now.AddSeconds(198700797), DateTime.Now.AddSeconds(185757614), DateTime.Now.AddSeconds(-253635786), null, DateTime.Now.AddSeconds(37591761), DateTime.Now.AddSeconds(278101996), null, DateTime.Now.AddSeconds(306443565), null },
            },
            new DataConvertersTestClass {
                StringMember = (string)" amJlp7SNZ",
                StringArrayMember = new string[] { "bzS6' .W@L", "HEex1bUA5%", "PIV7vZwc:o", "m!?hvn4tFk", "QT:j#@@?eK", "U6kPJTdhLq", "aiedwMP$BO", "ux@U4@Q8,z", "d7$4T3gwXc", "JWhlK5?k.6" },
                CharMember = (char)'W',
                CharArrayMember = new char[] { 'A', ';', 'Z', '0', 'o', '5', '4', 'x', '!', '1' },
                NullableCharMember = (char?)'I',
                NullableCharArrayMember = new char?[] { 'v', 'g', '$', null, '?', null, null, '0', null, null },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, false, true, false, false, true, true, true, false, false },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { null, false, true, null, false, null, true, true, false, false },
                ByteMember = (byte)102,
                ByteArrayMember = new byte[] { 28, 5, 233, 153, 227, 196, 155, 11, 106, 18 },
                NullableByteMember = (byte?)15,
                NullableByteArrayMember = new byte?[] { null, null, null, 48, 108, 71, 199, 177, 120, 145 },
                SByteMember = (sbyte)78,
                SByteArrayMember = new sbyte[] { 19, 30, -64, 93, 125, -63, -78, -50, 60, 84 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { null, null, -21, null, -46, null, null, -2, 69, null },
                Int16Member = (short)17750,
                Int16ArrayMember = new short[] { -13233, -29300, -10173, 13729, -22539, 22042, 13472, 4218, -376, 13798 },
                NullableInt16Member = (short?)null,
                NullableInt16ArrayMember = new short?[] { 1161, -30916, 21803, null, null, -29140, 23479, -26648, null, 1369 },
                UInt16Member = (ushort)19493,
                UInt16ArrayMember = new ushort[] { 28183, 16795, 63529, 2997, 5563, 15543, 24142, 61438, 21845, 65152 },
                NullableUInt16Member = (ushort?)63297,
                NullableUInt16ArrayMember = new ushort?[] { null, 48382, null, null, 56330, null, 30883, null, null, 30292 },
                Int32Member = (int)-153945527,
                Int32ArrayMember = new int[] { 560262387, 1424691913, -1782132413, -1509287247, 1223837876, -823554415, -1609559471, -1381198497, -771450155, 2104059597 },
                NullableInt32Member = (int?)-7056203,
                NullableInt32ArrayMember = new int?[] { null, 97661192, 1701087496, 45600661, 421899765, -667478761, -609127201, -1569271980, -1416590225, -811649460 },
                UInt32Member = (uint)2476613576,
                UInt32ArrayMember = new uint[] { 1022311763, 952909401, 1763986076, 490051295, 2709891954, 221350844, 2849419858, 632160094, 833509851, 207666166 },
                NullableUInt32Member = (uint?)2863158291,
                NullableUInt32ArrayMember = new uint?[] { null, null, null, null, 4237892916, null, 2817188108, null, 4271676633, 637790342 },
                Int64Member = (long)5791863567803613560,
                Int64ArrayMember = new long[] { -4172704695091189710, -8586023531626851830, 1407603606983102312, 5907753222012823894, 1137077899957703606, 2799075075743064028, 7010852327259827752, 8900481574311127455, -5605429699900740970, 513414207538379143 },
                NullableInt64Member = (long?)-4023890329813708392,
                NullableInt64ArrayMember = new long?[] { -3162935163667813963, 7158394250023227656, 4410627439296898455, -3542297892624075215, 1663423980146492853, -7929805211316660805, -1642408914511193686, null, -2476294980057269149, 7456549422700534176 },
                UInt64Member = (ulong)703202418126014883,
                UInt64ArrayMember = new ulong[] { 13703995938408510492, 5962239571187719692, 15082424665025066903, 7055672949404308494, 14157492385716519720, 9346089375818986666, 10643734202103186955, 4869788453629946852, 1297614155495311860, 1444330341760541757 },
                NullableUInt64Member = (ulong?)10089615412659760401,
                NullableUInt64ArrayMember = new ulong?[] { 8908319037425706188, 16931076079348230331, 13419835596252682287, 4744679425284604848, 5708963980809920887, null, 17642089405938151362, 17865756890265650225, null, null },
                SingleMember = (float)0.004F,
                SingleArrayMember = new float[] { 0.487F, 0.214F, 0.7891F, 0.9965F, 0.572F, 0.0622F, 0.5649F, 0.991F, 0.6576F, 0.8311F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { null, 0.4272F, 0.2995F, 0.9068F, null, 0.3647F, null, null, null, 0.5459F },
                DoubleMember = (double)0.8036,
                DoubleArrayMember = new double[] { 0.3891, 0.1992, 0.7612, 0.4587, 0.7256, 0.7469, 0.4409, 0.7474, 0.709, 0.3533 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.0524, 0.2253, 0.2762, null, 0.3332, 0.9399, 0.1072, 0.7526, 0.7111, 0.8015 },
                DecimalMember = (decimal)0.838191129657529m,
                DecimalArrayMember = new decimal[] { 0.821644415995872m, 0.730909485244616m, 0.956677849850002m, 0.937084952340035m, 0.707349232261698m, 0.623391596425041m, 0.460136444522131m, 0.30098872599238m, 0.70261712358455m, 0.127881424561088m },
                NullableDecimalMember = (decimal?)0.0641524293758685m,
                NullableDecimalArrayMember = new decimal?[] { 0.280788700227062m, 0.828827005265666m, 0.575546507991639m, null, 0.550322946417296m, null, null, null, null, 0.611730323923626m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), null, null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-149129788),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-85148834), DateTime.Now.AddSeconds(134642508), DateTime.Now.AddSeconds(66641267), DateTime.Now.AddSeconds(218534042), DateTime.Now.AddSeconds(-164997899), DateTime.Now.AddSeconds(8737374), DateTime.Now.AddSeconds(195479360), DateTime.Now.AddSeconds(284275040), DateTime.Now.AddSeconds(106947656), DateTime.Now.AddSeconds(126899356) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-334443057),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-257253051), DateTime.Now.AddSeconds(-55627298), DateTime.Now.AddSeconds(-13099399), null, null, DateTime.Now.AddSeconds(26475138), DateTime.Now.AddSeconds(312430586), null, DateTime.Now.AddSeconds(-172101703), null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"2%.Qmt.QiN",
                StringArrayMember = new string[] { "CstiNqkwK\"", "2pwwEXQgJ%", "G3%E$f#4Rw", "'QNJZoz%Yr", "UPa!DMZOLO", "deHkHWtol4", "t3ekAmcnXm", "uRHo1;cEB7", "G5sDTlJ0CC", "@Ljq%n8;\"o" },
                CharMember = (char)':',
                CharArrayMember = new char[] { '5', 'I', 'E', 'y', 'Q', 'D', '.', '!', 'x', 'b' },
                NullableCharMember = (char?)'d',
                NullableCharArrayMember = new char?[] { null, 'I', null, 'J', null, '5', 'P', 'D', null, null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { false, true, false, false, true, true, true, false, false, false },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { null, null, true, false, true, true, false, true, true, null },
                ByteMember = (byte)234,
                ByteArrayMember = new byte[] { 185, 195, 231, 225, 185, 213, 148, 209, 127, 233 },
                NullableByteMember = (byte?)29,
                NullableByteArrayMember = new byte?[] { 145, 109, 85, 175, null, null, 96, 228, 228, 32 },
                SByteMember = (sbyte)-88,
                SByteArrayMember = new sbyte[] { 2, -54, 47, -29, -100, -74, 34, -76, 36, 78 },
                NullableSByteMember = (sbyte?)108,
                NullableSByteArrayMember = new sbyte?[] { -92, 53, -71, null, -112, -56, 49, null, null, 17 },
                Int16Member = (short)-19045,
                Int16ArrayMember = new short[] { 18232, 21302, -10787, 29728, 17177, 4232, 20487, 2501, -9824, -6888 },
                NullableInt16Member = (short?)4262,
                NullableInt16ArrayMember = new short?[] { 22828, null, -5063, -7682, null, -2924, null, null, -18352, null },
                UInt16Member = (ushort)23889,
                UInt16ArrayMember = new ushort[] { 26351, 17726, 55266, 15160, 41339, 47654, 30422, 49890, 15146, 49758 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 45019, 34235, 64533, 16520, null, 16466, 32730, 36597, null, 52532 },
                Int32Member = (int)2075235895,
                Int32ArrayMember = new int[] { 1873199180, -229106714, 565639929, 730388371, 358205102, 493476602, -512527239, -1214224267, 459602222, 1541989779 },
                NullableInt32Member = (int?)-644712939,
                NullableInt32ArrayMember = new int?[] { 1957524753, -2137310095, 1928694103, null, null, -218146415, -929600142, null, -698030645, null },
                UInt32Member = (uint)1568230070,
                UInt32ArrayMember = new uint[] { 1669871529, 225294802, 2358299401, 619086558, 2746863893, 213334304, 2155016619, 3531419287, 2418908704, 3977739907 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 1913442768, 299761923, null, 2137996757, null, null, 3681938759, 1430285245, null, 2848766358 },
                Int64Member = (long)3195097991296489309,
                Int64ArrayMember = new long[] { -3575081291744564555, -8490525640658144083, 5651615614845853903, 4085203650141984080, -2499177595810103742, -4144057390241126601, 2573541464127127404, 2214100772275690555, -6101419308605374206, -681185141526031409 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { -286484467105279003, 8843570105865779482, 4666115056710078587, 3088012987811326377, 4170186086449734297, null, -4072438821291955680, null, null, null },
                UInt64Member = (ulong)10162513463020498524,
                UInt64ArrayMember = new ulong[] { 17871946948541946607, 5010244944770240665, 6381130561015093263, 5222613483992848731, 15813775579853209038, 7276555024221750298, 11177167707452486054, 2786879737040215947, 4479583070022496917, 15500174962087941411 },
                NullableUInt64Member = (ulong?)11495572106706051626,
                NullableUInt64ArrayMember = new ulong?[] { 3141011707129625166, null, 17972016764213755385, 11110617964294740721, 3485442917994703199, 16019415842534610084, null, 12659030861482038121, null, 15033239880212719264 },
                SingleMember = (float)0.4747F,
                SingleArrayMember = new float[] { 0.736F, 0.0047F, 0.5894F, 0.1455F, 0.5724F, 0.0069F, 0.2294F, 0.1206F, 0.6515F, 0.0631F },
                NullableSingleMember = (float?)0.0467F,
                NullableSingleArrayMember = new float?[] { null, 0.4687F, null, null, 0.6459F, null, 0.8439F, 0.3474F, null, null },
                DoubleMember = (double)0.9967,
                DoubleArrayMember = new double[] { 0.8116, 0.8177, 0.9751, 0.4914, 0.3018, 0.3573, 0.3906, 0.3289, 0.5551, 0.5372 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.0294, null, 0.6164, null, 0.3726, null, 0.1451, null, null, null },
                DecimalMember = (decimal)0.274308053438695m,
                DecimalArrayMember = new decimal[] { 0.57423634527914m, 0.996276814023162m, 0.956270089352629m, 0.400913206581452m, 0.51797565469424m, 0.804748596532619m, 0.191449277192098m, 0.67195332547275m, 0.903225408821937m, 0.85061333274963m },
                NullableDecimalMember = (decimal?)0.0650584525731664m,
                NullableDecimalArrayMember = new decimal?[] { 0.880945119020038m, 0.215553056549073m, 0.813066529488688m, 0.259185521984094m, 0.643567067405007m, 0.0503055779497631m, null, 0.287868183240233m, null, 0.356863654850453m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-300134645),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-27357249), DateTime.Now.AddSeconds(197703156), DateTime.Now.AddSeconds(293582698), DateTime.Now.AddSeconds(-223983500), DateTime.Now.AddSeconds(-70580783), DateTime.Now.AddSeconds(194387721), DateTime.Now.AddSeconds(-224286490), DateTime.Now.AddSeconds(-187976665), DateTime.Now.AddSeconds(-96702803), DateTime.Now.AddSeconds(127662163) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-346778563),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-271190042), null, DateTime.Now.AddSeconds(35290561), null, null, null, null, DateTime.Now.AddSeconds(34292245), null, DateTime.Now.AddSeconds(-57296770) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"78;vqLWp6Q",
                StringArrayMember = new string[] { "!on1y,LbQm", "dykUrj5oqN", "FlLj U:#jI", "Pr!9Idv5:J", "chwdLJYtEH", "d5%sKfdD1e", "#P'IX83yLO", ", wg7v?4Y;", "@U'b1gsYy0", "ArDkNNpoGZ" },
                CharMember = (char)'d',
                CharArrayMember = new char[] { 'Y', 'j', 'A', '#', 'N', '?', '\'', '9', 'x', 'D' },
                NullableCharMember = (char?)'3',
                NullableCharArrayMember = new char?[] { 'C', 'n', 'U', null, 'l', '"', null, 'W', '?', 'R' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, false, true, true, true, true, false, false, true, true },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { true, true, false, true, true, false, true, null, false, true },
                ByteMember = (byte)75,
                ByteArrayMember = new byte[] { 149, 120, 127, 87, 246, 39, 186, 165, 193, 123 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { 129, 42, 53, 68, 34, null, 77, 188, null, 212 },
                SByteMember = (sbyte)-108,
                SByteArrayMember = new sbyte[] { 88, -6, -6, 84, 62, 103, 30, -39, 55, -86 },
                NullableSByteMember = (sbyte?)-28,
                NullableSByteArrayMember = new sbyte?[] { null, null, null, 38, null, null, null, null, null, null },
                Int16Member = (short)11814,
                Int16ArrayMember = new short[] { -25013, -11625, -22386, -4737, 16565, 14226, 9949, -10621, 14978, 4909 },
                NullableInt16Member = (short?)32559,
                NullableInt16ArrayMember = new short?[] { null, null, -6243, -17515, 10986, null, null, -21813, 4272, null },
                UInt16Member = (ushort)33026,
                UInt16ArrayMember = new ushort[] { 11646, 6169, 54159, 35812, 58767, 21031, 9899, 30335, 31390, 28402 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 24945, 38320, 18591, null, 2690, 4558, 4273, null, 54192, 25900 },
                Int32Member = (int)872309487,
                Int32ArrayMember = new int[] { 2066695681, -990742382, -1284051190, 971282642, 41457626, -1806050241, 228315198, 268846464, 1249592233, 543196201 },
                NullableInt32Member = (int?)1725467817,
                NullableInt32ArrayMember = new int?[] { -1652601351, -201594537, null, -574473061, 1726596419, 261758095, null, null, -2142267774, -1907501069 },
                UInt32Member = (uint)4293224714,
                UInt32ArrayMember = new uint[] { 2121155385, 1100487865, 280911947, 2771830279, 2230624269, 2926446158, 729497004, 87239876, 2642246810, 3216797401 },
                NullableUInt32Member = (uint?)2547982460,
                NullableUInt32ArrayMember = new uint?[] { 646107033, 2975954026, null, null, null, 2087494011, 2323036205, 3818835096, 2414268879, 2829820775 },
                Int64Member = (long)-8557240217694347482,
                Int64ArrayMember = new long[] { -6586477320807607994, 2258110680334019281, 1660693320921860114, -1744736423502479543, -2406067854120275807, -4957129556794661153, 5675498883399320841, 6714046459881324677, 2600684469669325921, -8347019154679493716 },
                NullableInt64Member = (long?)-1426307699348244041,
                NullableInt64ArrayMember = new long?[] { 5546405247879678036, 7493829856773355777, -577825421941267590, null, null, -1653058140770521172, -7398808146381756325, 6758137355138204652, -8124848675804867535, 4868134321159543787 },
                UInt64Member = (ulong)11944950110339440630,
                UInt64ArrayMember = new ulong[] { 6428693525810142740, 1742921068190334512, 688495506775620332, 6574638952092303556, 5855519453610301886, 15877012520827090798, 982565563612885870, 6921248014920235727, 17306454053431520065, 5351410722797485816 },
                NullableUInt64Member = (ulong?)3960968241376432907,
                NullableUInt64ArrayMember = new ulong?[] { 17503584008960291199, null, 12512052300486259987, null, 3865031901667772505, 12104375610477804703, 8373928668310886290, 7971302629532012805, 12012424660580163771, 1925593728284248740 },
                SingleMember = (float)0.2532F,
                SingleArrayMember = new float[] { 0.6394F, 0.8311F, 0.7459F, 0.3877F, 0.3207F, 0.0261F, 0.5875F, 0.829F, 0.9864F, 0.0682F },
                NullableSingleMember = (float?)0.7881F,
                NullableSingleArrayMember = new float?[] { null, null, 0.3499F, 0.2075F, 0.0475F, 0.8959F, 0.0429F, 0.0299F, 0.2682F, 0.0088F },
                DoubleMember = (double)0.3213,
                DoubleArrayMember = new double[] { 0.654, 0.7379, 0.973, 0.9276, 0.7298, 0.5165, 0.9075, 0.3566, 0.6869, 0.4727 },
                NullableDoubleMember = (double?)0.4019,
                NullableDoubleArrayMember = new double?[] { 0.9169, 0.0665, 0.796, 0.6598, 0.5159, 0.1362, 0.5682, 0.1524, 0.6665, null },
                DecimalMember = (decimal)0.56739780100407m,
                DecimalArrayMember = new decimal[] { 0.0354604413898012m, 0.0116612725014152m, 0.185828391083436m, 0.726752380713239m, 0.706340309561389m, 0.171611335208458m, 0.421124448264541m, 0.546176774681628m, 0.171183618330948m, 0.0893394230349639m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.949842192674914m, 0.593718532283659m, 0.669377827397258m, null, 0.682541571409694m, null, 0.352544801939532m, null, 0.106053397108826m, 0.990483079101184m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(238376028),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(250706057), DateTime.Now.AddSeconds(104210680), DateTime.Now.AddSeconds(-186391372), DateTime.Now.AddSeconds(-183079079), DateTime.Now.AddSeconds(-169160924), DateTime.Now.AddSeconds(-115770698), DateTime.Now.AddSeconds(234409684), DateTime.Now.AddSeconds(-215695096), DateTime.Now.AddSeconds(279196199), DateTime.Now.AddSeconds(271596568) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-38818050),
                NullableDateTimeArrayMember = new DateTime?[] { null, null, DateTime.Now.AddSeconds(-89554381), DateTime.Now.AddSeconds(-248692185), null, DateTime.Now.AddSeconds(-52334820), null, null, DateTime.Now.AddSeconds(130130574), DateTime.Now.AddSeconds(-344324372) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"cwi;TdIo7m",
                StringArrayMember = new string[] { "qv2,izXnd;", "Qe iNT@LTu", "FXx kV9BIX", "XDoLKb?PBl", "?BKQJ$xAz ", "h$VNgS%2aN", "?VeFpM;.Y9", "e3K54ENh?n", "v9ZJ;a5b2:", "dJ?,vrtEx#" },
                CharMember = (char)'v',
                CharArrayMember = new char[] { '2', 't', 'g', 's', 'N', 'a', '6', 'Q', '7', 'O' },
                NullableCharMember = (char?)'z',
                NullableCharArrayMember = new char?[] { 'R', null, null, 'K', 'r', 'C', '\'', 'q', null, ';' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, true, false, false, true, true, true, false, false },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { true, false, null, true, null, null, null, null, null, false },
                ByteMember = (byte)152,
                ByteArrayMember = new byte[] { 174, 234, 95, 157, 82, 163, 100, 57, 191, 70 },
                NullableByteMember = (byte?)174,
                NullableByteArrayMember = new byte?[] { 51, 56, null, 242, null, 150, null, 41, 149, 39 },
                SByteMember = (sbyte)47,
                SByteArrayMember = new sbyte[] { -104, 27, -127, 11, 48, 31, 94, 42, 33, 15 },
                NullableSByteMember = (sbyte?)68,
                NullableSByteArrayMember = new sbyte?[] { null, -27, -105, 1, 80, -57, 56, 84, -50, 48 },
                Int16Member = (short)-17338,
                Int16ArrayMember = new short[] { 5981, 28380, -12714, 18301, -23278, 7707, -23914, 2937, 920, -31456 },
                NullableInt16Member = (short?)-19891,
                NullableInt16ArrayMember = new short?[] { 28615, -5744, null, null, 17464, -15875, -14444, null, -31529, null },
                UInt16Member = (ushort)4047,
                UInt16ArrayMember = new ushort[] { 51397, 55994, 61843, 48379, 14776, 16094, 60264, 47687, 53305, 36769 },
                NullableUInt16Member = (ushort?)11876,
                NullableUInt16ArrayMember = new ushort?[] { 19170, 40657, 19378, 55916, 4977, 39704, 13913, null, null, 27099 },
                Int32Member = (int)648994018,
                Int32ArrayMember = new int[] { 1565872045, 1134380942, -288922214, 812269568, 538086885, -698582894, -347574676, -396339568, 1063010363, 243350727 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { -1853410841, -1474532173, 882085322, null, null, null, 1152561919, null, 719569075, -2029843288 },
                UInt32Member = (uint)2445906077,
                UInt32ArrayMember = new uint[] { 3377187640, 490839064, 2969698120, 2249182943, 2264750825, 1272942910, 3614091589, 2396963829, 3191678564, 28561405 },
                NullableUInt32Member = (uint?)3855779607,
                NullableUInt32ArrayMember = new uint?[] { 613718071, 1380094531, 1854144492, 562309225, 1351727181, null, 652541094, null, 3530647654, 4221175249 },
                Int64Member = (long)4350991467448007761,
                Int64ArrayMember = new long[] { 237726583173855152, -4329654538528825621, 4117565632464508322, 4720149946981538493, 6395163697760566135, 6206118897519204302, 2818376421808946066, -7807835297166490477, 8233394294971863900, 3240237748324405141 },
                NullableInt64Member = (long?)-2336009042391811822,
                NullableInt64ArrayMember = new long?[] { -1949268993738146958, null, null, 3629273276551715321, -9131584263029985513, -1925445902152909260, -4674092652716170527, 4488789714215905547, -1196144921249441938, null },
                UInt64Member = (ulong)16614959114989630942,
                UInt64ArrayMember = new ulong[] { 1065855553795433373, 895303739328905130, 5712492154355551771, 12642080062877886276, 17843831107741002352, 2024601464168647808, 14162812162458487560, 17958280306056006237, 1599441742261917646, 8070797253375793900 },
                NullableUInt64Member = (ulong?)2163433948666601670,
                NullableUInt64ArrayMember = new ulong?[] { null, 4434027888145049679, 418786767698837856, null, 17113682182847144301, null, null, null, null, null },
                SingleMember = (float)0.242F,
                SingleArrayMember = new float[] { 0.7483F, 0.0185F, 0.7538F, 0.59F, 0.2895F, 0.9055F, 0.6754F, 0.7448F, 0.0206F, 0.9119F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { null, 0.7395F, 0.2709F, 0.8999F, null, 0.14F, null, null, 0.7434F, null },
                DoubleMember = (double)0.4933,
                DoubleArrayMember = new double[] { 0.0422, 0.9805, 0.7033, 0.6527, 0.3559, 0.6275, 0.8074, 0.1253, 0.3725, 0.6727 },
                NullableDoubleMember = (double?)0.309,
                NullableDoubleArrayMember = new double?[] { null, 0.2194, null, 0.8421, 0.6797, 0.7894, null, 0.7987, 0.6278, 0.6583 },
                DecimalMember = (decimal)0.649660635576425m,
                DecimalArrayMember = new decimal[] { 0.516880413292386m, 0.850394971599055m, 0.374894371430806m, 0.546361753971484m, 0.28321553174556m, 0.273574272763717m, 0.0533971800717512m, 0.627168872685716m, 0.154192738306798m, 0.39719651518259m },
                NullableDecimalMember = (decimal?)0.496148175325314m,
                NullableDecimalArrayMember = new decimal?[] { 0.635678526775762m, 0.275331966707172m, 0.940381224239423m, 0.816711595196608m, 0.618490355842975m, 0.0787614793883457m, 0.010777179156792m, 0.659116470561883m, 0.796083102839106m, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(66136062),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(180731245), DateTime.Now.AddSeconds(-186589089), DateTime.Now.AddSeconds(-88511616), DateTime.Now.AddSeconds(-165219114), DateTime.Now.AddSeconds(336715986), DateTime.Now.AddSeconds(306011693), DateTime.Now.AddSeconds(-124992548), DateTime.Now.AddSeconds(57812654), DateTime.Now.AddSeconds(-260311431), DateTime.Now.AddSeconds(-39258759) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(118992134),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(294505053), null, null, DateTime.Now.AddSeconds(276768655), DateTime.Now.AddSeconds(262722578), DateTime.Now.AddSeconds(-118534115), DateTime.Now.AddSeconds(-69213502), DateTime.Now.AddSeconds(94072597), null, null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"G9y;r19mfc",
                StringArrayMember = new string[] { "Fn;9oD@VPO", "dmIgfU:WKN", "hI6\"g9KioL", "oT@Z8;TCWr", "vY,jqq0ShX", ".mcS@#3#d2", "\"k3EwKq6up", "A,ApoITXZ?", "I'ARYC;0d?", "JWwryLr79g" },
                CharMember = (char)'3',
                CharArrayMember = new char[] { 'D', '"', 'f', 'e', 'M', 'J', 'b', 'g', 's', 'M' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { 'y', null, 'n', 'Y', null, 'd', 'Q', null, '0', 'o' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, false, true, false, true, true, true, false, false },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { true, true, null, null, null, false, false, true, null, null },
                ByteMember = (byte)58,
                ByteArrayMember = new byte[] { 159, 246, 208, 40, 204, 115, 240, 226, 235, 133 },
                NullableByteMember = (byte?)250,
                NullableByteArrayMember = new byte?[] { null, 226, null, null, 14, 56, null, 65, null, 72 },
                SByteMember = (sbyte)-52,
                SByteArrayMember = new sbyte[] { -108, 74, 60, -94, 101, -110, 39, -105, -53, -74 },
                NullableSByteMember = (sbyte?)-114,
                NullableSByteArrayMember = new sbyte?[] { null, null, null, -115, null, null, null, -80, -20, -44 },
                Int16Member = (short)-12132,
                Int16ArrayMember = new short[] { 12329, 5944, 2573, 23745, 25872, -22889, 5862, 24301, 11695, -6633 },
                NullableInt16Member = (short?)31950,
                NullableInt16ArrayMember = new short?[] { null, 22639, null, null, null, null, -25045, -20385, null, 30457 },
                UInt16Member = (ushort)31001,
                UInt16ArrayMember = new ushort[] { 50268, 24127, 39265, 63472, 10568, 7722, 34569, 30867, 4603, 18631 },
                NullableUInt16Member = (ushort?)35041,
                NullableUInt16ArrayMember = new ushort?[] { null, null, 63224, 60560, 13444, null, 3718, null, null, null },
                Int32Member = (int)1311478142,
                Int32ArrayMember = new int[] { 2123645403, 504081779, 1277734062, 258034504, 765682150, -814948696, 1224717122, -1558968634, -569036345, -1774242710 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { -574942460, -1594643619, -39452947, null, -1012023493, null, 377848983, 565355650, 1507452724, -1650290076 },
                UInt32Member = (uint)3533099809,
                UInt32ArrayMember = new uint[] { 3477425149, 2969825080, 125800305, 2644810307, 2620153494, 4246444254, 2136352351, 3828119430, 3465248250, 917253217 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 3404912885, 1711104054, 1710487975, 2568056903, 2590243371, 1020661702, null, 2743244277, 539464714, 1030230408 },
                Int64Member = (long)8986910727917908504,
                Int64ArrayMember = new long[] { -2396386402592096945, -1586235425830578118, -4240883646127548177, 6446848471038777743, -4311613548564055380, -8527814198962604469, -742895898223327051, 7806903410101331672, -2551660289965367072, 8185588149720123821 },
                NullableInt64Member = (long?)5402133304981804928,
                NullableInt64ArrayMember = new long?[] { -2242905339065193355, 3202395815907359307, -4019340016921142276, 4061686300397464728, null, 8524985536947785324, 3457400509760819059, -916975444534583126, null, -8809103776055076914 },
                UInt64Member = (ulong)8775947617388701846,
                UInt64ArrayMember = new ulong[] { 17470434424897928910, 14308717628347836400, 3253098559027399101, 10948926292589513202, 10428326546864260597, 15013074805501196812, 8832239755064253717, 9907196491890204120, 16215051197089805045, 14641190362342922513 },
                NullableUInt64Member = (ulong?)3641815468984496106,
                NullableUInt64ArrayMember = new ulong?[] { 13437790037471010890, 130346719765321611, 14831287530269943065, 2595171544583230246, 11797836116407704354, 8190321378374271472, 6205409304182482872, null, 12010306915626652269, null },
                SingleMember = (float)0.7031F,
                SingleArrayMember = new float[] { 0.0649F, 0.2907F, 0.8804F, 0.3193F, 0.9369F, 0.9776F, 0.8262F, 0.9374F, 0.7696F, 0.6212F },
                NullableSingleMember = (float?)0.0172F,
                NullableSingleArrayMember = new float?[] { null, null, null, 0.1356F, 0.4136F, 0.3033F, 0.759F, 0.3682F, null, 0.102F },
                DoubleMember = (double)0.7551,
                DoubleArrayMember = new double[] { 0.2685, 0.0204, 0.1307, 0.5841, 0.3597, 0.9431, 0.2136, 0.2329, 0.7106, 0.5875 },
                NullableDoubleMember = (double?)0.5547,
                NullableDoubleArrayMember = new double?[] { 0.1758, null, null, null, 0.3488, null, null, 0.7364, null, 0.2528 },
                DecimalMember = (decimal)0.265646823805592m,
                DecimalArrayMember = new decimal[] { 0.430641369163357m, 0.896516139105203m, 0.654303205038562m, 0.238944451435909m, 0.207399815417547m, 0.379239842472244m, 0.666626660929353m, 0.992937521074404m, 0.540321352211908m, 0.42134040939684m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.174355753312984m, 0.289730867040218m, 0.182698702990403m, 0.212703806912854m, 0.122170727291224m, 0.829854898541167m, 0.886393951664862m, 0.872039670530725m, 0.103140477139103m, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null, null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-327034959),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-206026225), DateTime.Now.AddSeconds(36790346), DateTime.Now.AddSeconds(-52214410), DateTime.Now.AddSeconds(-121446327), DateTime.Now.AddSeconds(-181726378), DateTime.Now.AddSeconds(-46426341), DateTime.Now.AddSeconds(115785288), DateTime.Now.AddSeconds(10108184), DateTime.Now.AddSeconds(54759556), DateTime.Now.AddSeconds(-25534037) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(74536712), DateTime.Now.AddSeconds(-101502804), DateTime.Now.AddSeconds(331428680), null, null, DateTime.Now.AddSeconds(-228342740), DateTime.Now.AddSeconds(203361918), DateTime.Now.AddSeconds(-257046950), DateTime.Now.AddSeconds(-50734641), null },
            },
        };
    }
}
