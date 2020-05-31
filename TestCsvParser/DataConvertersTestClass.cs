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
                StringMember = (string)"3!jY!nhni:",
                StringArrayMember = new string[] { "9Cl1??DY.#", "Gh\"w'aD2LN", "c.h6d.?1r;", "O.@Zn9'Tn0", "CGDU0VB.fq", "k1bVl%s2XB", " Z3$5SmqIG", "i6PiKW7MGI", "9BWAcjix.o", "QN@l00ZGmh" },
                CharMember = (char)'1',
                CharArrayMember = new char[] { 'N', 'P', '4', '2', 'V', '2', 'j', 's', 'y', '0' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { null, 'A', null, null, 't', 'W', 'y', '1', null, 'R' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, true, false, false, true, false, false, true, true, true },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { false, null, true, true, false, false, true, true, null, true },
                ByteMember = (byte)68,
                ByteArrayMember = new byte[] { 92, 150, 131, 53, 16, 219, 238, 225, 160, 120 },
                NullableByteMember = (byte?)208,
                NullableByteArrayMember = new byte?[] { 127, 202, 92, 110, 45, 235, 19, null, null, null },
                SByteMember = (sbyte)83,
                SByteArrayMember = new sbyte[] { -86, 18, 113, -46, -85, -44, -56, -47, -49, -53 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { -104, 33, -19, -59, -110, 120, null, null, null, 96 },
                Int16Member = (short)-16361,
                Int16ArrayMember = new short[] { 11677, -27412, 414, -308, -30762, 2458, -25998, 25002, -20123, -9373 },
                NullableInt16Member = (short?)2140,
                NullableInt16ArrayMember = new short?[] { null, -24709, null, null, 23851, -19093, -31649, null, -570, null },
                UInt16Member = (ushort)27412,
                UInt16ArrayMember = new ushort[] { 58468, 60807, 22195, 52176, 25657, 54603, 6168, 62947, 62994, 63167 },
                NullableUInt16Member = (ushort?)15023,
                NullableUInt16ArrayMember = new ushort?[] { 48570, 9526, 62210, null, 46700, null, 62179, null, 1877, 55259 },
                Int32Member = (int)-2058317622,
                Int32ArrayMember = new int[] { 171051696, 732305783, 871507381, -1617869450, 437828996, -686910326, 1536680073, -1266059024, -1655373585, -1823692564 },
                NullableInt32Member = (int?)-1711952759,
                NullableInt32ArrayMember = new int?[] { null, -1431080871, 486159991, null, 1250006912, null, 1984118172, 1893446644, 434569549, null },
                UInt32Member = (uint)1675300796,
                UInt32ArrayMember = new uint[] { 1341820063, 584899186, 1303413018, 344858896, 2275223390, 4240038472, 1563332010, 2990937248, 93433387, 3696823229 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 4250480763, 1357794571, null, 3552910164, null, 2189405156, 1646394938, 3461568392, 2287939108, 254001836 },
                Int64Member = (long)8870365571741440651,
                Int64ArrayMember = new long[] { 8329545136450217872, -1102720283126864516, -8188120988704065679, -6793952253119102252, 2943113674785702865, 3561178684701089313, -7195565282724573401, 8289057208602338121, 7468502166171613485, -330721392896952609 },
                NullableInt64Member = (long?)2276888409724930989,
                NullableInt64ArrayMember = new long?[] { null, null, 3181188501903945576, 8451258322517232625, 3231878720268156918, -36735162988701893, -6483427660195084628, null, -5287452303668065039, null },
                UInt64Member = (ulong)11347508658714652857,
                UInt64ArrayMember = new ulong[] { 17613106159874126480, 8720679566899485793, 6203479623944148968, 4197057121729814655, 16785466475613864911, 11945184467635117378, 7044964501547442730, 8123460835846861723, 17628630330562499294, 12491557406661847205 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { null, 17424565571232856149, 2975602687234594432, null, 9890532340906439107, 8603957227516451245, 14948579145110177915, 5813784488988138125, 4951696585195189528, null },
                SingleMember = (float)0.6327F,
                SingleArrayMember = new float[] { 0.6719F, 0.5569F, 0.1049F, 0.172F, 0.2258F, 0.3003F, 0.8182F, 0.9124F, 0.0957F, 0.9471F },
                NullableSingleMember = (float?)0.4933F,
                NullableSingleArrayMember = new float?[] { 0.1242F, 0.1253F, 0.116F, 0.7434F, 0.4785F, 0.5252F, null, null, 0.6186F, null },
                DoubleMember = (double)0.0843,
                DoubleArrayMember = new double[] { 0.4616, 0.1959, 0.6999, 0.5102, 0.0824, 0.981, 0.2503, 0.3708, 0.1239, 0.7999 },
                NullableDoubleMember = (double?)0.6011,
                NullableDoubleArrayMember = new double?[] { null, 0.7069, 0.4589, 0.6399, 0.874, 0.1864, 0.4651, null, null, 0.5802 },
                DecimalMember = (decimal)0.676399444544874m,
                DecimalArrayMember = new decimal[] { 0.386494331241816m, 0.424527428776271m, 0.0330283781667372m, 0.0640088394582313m, 0.486724272596987m, 0.446567281357277m, 0.474197676626126m, 0.175616849761278m, 0.9661831403925m, 0.477391026670761m },
                NullableDecimalMember = (decimal?)0.171869956968292m,
                NullableDecimalArrayMember = new decimal?[] { null, null, 0.648547286004083m, 0.796946221868017m, null, null, 0.478682091216874m, 0.101106345700615m, 0.663508819259474m, 0.0351708489633961m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(147794841),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(178107201), DateTime.Now.AddSeconds(-329432010), DateTime.Now.AddSeconds(40401232), DateTime.Now.AddSeconds(105595955), DateTime.Now.AddSeconds(-87309406), DateTime.Now.AddSeconds(-311651688), DateTime.Now.AddSeconds(6342960), DateTime.Now.AddSeconds(-325363981), DateTime.Now.AddSeconds(-1793296), DateTime.Now.AddSeconds(101136960) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-177573424), DateTime.Now.AddSeconds(141699321), DateTime.Now.AddSeconds(-101875775), DateTime.Now.AddSeconds(-55640483), DateTime.Now.AddSeconds(49969718), null, DateTime.Now.AddSeconds(130826500), null, DateTime.Now.AddSeconds(-162912035), DateTime.Now.AddSeconds(-21509638) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"5h2YI1m'KP",
                StringArrayMember = new string[] { "OgwPP$D3rV", "7?3q;U,05g", "$0xvZcW$:y", "#pmvZ6o3W2", "xtlQx\"1Lh.", "?8K0Rk6A?.", "xF,\"YRZHRG", "3HeC6bPg%g", "xLnD8Ke8d,", "An;iEr4s:g" },
                CharMember = (char)'#',
                CharArrayMember = new char[] { 'W', 'A', 'z', 'v', 'D', 'o', 'm', '8', ' ', 'v' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { ';', null, 'y', 'w', '2', 'h', '%', null, '"', null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, false, true, true, true, true, true, false, true, false },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { true, true, false, true, false, true, false, null, null, null },
                ByteMember = (byte)107,
                ByteArrayMember = new byte[] { 156, 162, 171, 146, 216, 248, 51, 204, 24, 163 },
                NullableByteMember = (byte?)31,
                NullableByteArrayMember = new byte?[] { null, null, 117, 108, 182, 239, 208, 47, null, null },
                SByteMember = (sbyte)-32,
                SByteArrayMember = new sbyte[] { 31, 90, 119, -118, 4, -104, 15, -71, -47, 82 },
                NullableSByteMember = (sbyte?)-100,
                NullableSByteArrayMember = new sbyte?[] { 45, 116, -37, 22, 31, -22, null, -74, null, null },
                Int16Member = (short)6923,
                Int16ArrayMember = new short[] { -22742, 28279, -16293, -19536, -9412, 9281, -17604, 24867, -6553, 23387 },
                NullableInt16Member = (short?)1669,
                NullableInt16ArrayMember = new short?[] { 256, 11108, 25831, -20844, null, null, null, 31674, -28601, 9093 },
                UInt16Member = (ushort)28193,
                UInt16ArrayMember = new ushort[] { 52229, 25789, 23326, 43445, 13198, 52214, 42394, 25046, 6743, 44217 },
                NullableUInt16Member = (ushort?)52679,
                NullableUInt16ArrayMember = new ushort?[] { 18893, 13145, 58847, 8392, null, null, 1180, 40973, 9965, null },
                Int32Member = (int)-364997325,
                Int32ArrayMember = new int[] { 1031589437, -1590098503, 313862033, 1483689346, 1775506011, -1680998276, 888793330, 38608097, 1862164547, 1888092192 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { 1941681185, -473859917, null, -12816684, null, -1073476400, 583971802, null, null, -1285257125 },
                UInt32Member = (uint)1085457586,
                UInt32ArrayMember = new uint[] { 852202455, 812934847, 4134901731, 2070469467, 3321665221, 1709977520, 586701642, 1087336327, 3477781896, 3986183620 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { null, null, 3511128809, 3340454568, 1705420687, null, 1940410734, null, 2576391789, 1796210287 },
                Int64Member = (long)6190582425112419521,
                Int64ArrayMember = new long[] { -7154342476772633174, 5615039295668341551, -1141619636661372222, 7941996861135392711, -6880127872332404741, 2373498658654338132, -8982508429698511824, -8463772859591914900, -2150790684395717485, 7744996403643715509 },
                NullableInt64Member = (long?)-8051801128095653347,
                NullableInt64ArrayMember = new long?[] { null, null, -5200695828973148047, -2549606952651662503, -4125210139503449262, 8108793165673954059, null, 4739315974975983402, null, -8144612615589163319 },
                UInt64Member = (ulong)12484822014458959291,
                UInt64ArrayMember = new ulong[] { 16353572138997911855, 3643992262897577653, 6495872228104687143, 15123375114935615043, 7412947658115285856, 12330357077248129310, 3912866652208798211, 12634771612561135596, 1338683479338612301, 715050753367481198 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 5779549146090631222, null, 17832400616325965615, null, 2842724350686596387, null, 10811567666886138731, 10053972035357896596, null, null },
                SingleMember = (float)0.5121F,
                SingleArrayMember = new float[] { 0.4606F, 0.7001F, 0.7168F, 0.5726F, 0.7483F, 0.8551F, 0.8775F, 0.1956F, 0.7044F, 0.516F },
                NullableSingleMember = (float?)0.102F,
                NullableSingleArrayMember = new float?[] { 0.9238F, null, 0.8591F, 0.4952F, null, null, null, 0.9806F, 0.1934F, null },
                DoubleMember = (double)0.5223,
                DoubleArrayMember = new double[] { 0.3589, 0.6441, 0.5974, 0.7476, 0.7797, 0.4392, 0.5845, 0.7799, 0.2366, 0.9191 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { null, null, 0.5028, 0.3024, 0.4264, 0.4493, null, 0.2427, 0.1478, 0.493 },
                DecimalMember = (decimal)0.933185165251226m,
                DecimalArrayMember = new decimal[] { 0.523573649359668m, 0.515358106938823m, 0.509515653601622m, 0.471776678912238m, 0.393159369655493m, 0.463093003008092m, 0.109379594265194m, 0.257792169813901m, 0.392536428939801m, 0.583089872069233m },
                NullableDecimalMember = (decimal?)0.84830669166907m,
                NullableDecimalArrayMember = new decimal?[] { null, 0.0497161294565146m, null, 0.137425151717581m, 0.130558026549666m, null, null, 0.292794890838114m, 0.632259364999952m, 0.119284804965968m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(59878740),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(154800820), DateTime.Now.AddSeconds(250548434), DateTime.Now.AddSeconds(-293914706), DateTime.Now.AddSeconds(-244680269), DateTime.Now.AddSeconds(340009813), DateTime.Now.AddSeconds(-169128401), DateTime.Now.AddSeconds(70228438), DateTime.Now.AddSeconds(-150712925), DateTime.Now.AddSeconds(-33790586), DateTime.Now.AddSeconds(-54467197) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(204804147), DateTime.Now.AddSeconds(-103235233), null, DateTime.Now.AddSeconds(183630338), null, DateTime.Now.AddSeconds(-17576723), DateTime.Now.AddSeconds(145456816), null, DateTime.Now.AddSeconds(-184611936), DateTime.Now.AddSeconds(259574731) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"qt!aH5eI\";",
                StringArrayMember = new string[] { "VZCgMmp7HO", "9n1cGQQ@$P", "ttpo?ERM#D", "3W:mqxlhWp", "C#l0tC:oU:", "oiK1Tz7krH", "Uc,EhmvO8f", "16vfMxI,@9", "Nz'FlVLNS;", ";B?PUwHhTH" },
                CharMember = (char)'d',
                CharArrayMember = new char[] { 'C', 'd', 'N', 'l', 'm', 'W', 'y', 'A', '4', 'W' },
                NullableCharMember = (char?)'l',
                NullableCharArrayMember = new char?[] { null, null, null, ';', null, null, null, 'f', '"', null },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, true, true, true, true, false, false, true, false },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { false, true, null, null, false, true, null, true, null, false },
                ByteMember = (byte)16,
                ByteArrayMember = new byte[] { 251, 55, 85, 131, 52, 218, 22, 36, 124, 166 },
                NullableByteMember = (byte?)150,
                NullableByteArrayMember = new byte?[] { 67, 161, 173, null, 174, 55, null, null, null, 180 },
                SByteMember = (sbyte)70,
                SByteArrayMember = new sbyte[] { 47, -67, -26, 111, 47, -17, 18, 113, 19, -80 },
                NullableSByteMember = (sbyte?)108,
                NullableSByteArrayMember = new sbyte?[] { -109, null, -45, 99, null, null, -77, -18, 77, null },
                Int16Member = (short)10696,
                Int16ArrayMember = new short[] { 24533, -25577, -8528, -24156, 28306, -8103, -30645, -3033, 10204, 3495 },
                NullableInt16Member = (short?)null,
                NullableInt16ArrayMember = new short?[] { 10864, -27936, null, 31600, 15127, -12107, -18868, null, -30570, null },
                UInt16Member = (ushort)32329,
                UInt16ArrayMember = new ushort[] { 16037, 7872, 17012, 65086, 38826, 14549, 30299, 21547, 24848, 34001 },
                NullableUInt16Member = (ushort?)51228,
                NullableUInt16ArrayMember = new ushort?[] { null, 7925, 9404, 55349, 9358, 62919, null, 39574, 52167, 29847 },
                Int32Member = (int)-535110103,
                Int32ArrayMember = new int[] { -1136321153, 1093417248, 1874981230, 1673114687, -643001679, 1672061960, -1752725129, 1581523291, -1295253275, 1000294598 },
                NullableInt32Member = (int?)-1926209423,
                NullableInt32ArrayMember = new int?[] { null, 16895910, -623361999, -1315486494, 1672302054, -1677268929, null, 1150692209, -510597418, 766320730 },
                UInt32Member = (uint)2086006048,
                UInt32ArrayMember = new uint[] { 3357938872, 2041849311, 698494364, 2134142558, 3486377736, 1084761268, 484176320, 3111092408, 1311697232, 1321830727 },
                NullableUInt32Member = (uint?)4000371131,
                NullableUInt32ArrayMember = new uint?[] { 2412314712, null, 2496103967, 1240336358, null, null, null, null, 3862918360, 3162447487 },
                Int64Member = (long)4379650495923678062,
                Int64ArrayMember = new long[] { 4714016041371391200, 970125818456672177, -1870200183594407887, 2459873007456352832, 4395358005215725607, -2791039922193800670, 3054648535963129460, 8703709352161937736, -4666849587286939293, 354544127693209384 },
                NullableInt64Member = (long?)2584073737190059304,
                NullableInt64ArrayMember = new long?[] { 4366618832498214696, -7277928667743607151, 7128872332439384047, -2548299922017026776, 6523963079645842472, -1328344762167700173, 8195512754693109819, null, -3614237024847598175, null },
                UInt64Member = (ulong)16801217340153062517,
                UInt64ArrayMember = new ulong[] { 15790458184284633430, 15340208637637758804, 15435643222837889532, 16046432638900233215, 4459111400969895513, 1642285287118630946, 16946589092374454017, 17466217304334408643, 766534061761554526, 7859555256875986216 },
                NullableUInt64Member = (ulong?)5834659143542614218,
                NullableUInt64ArrayMember = new ulong?[] { 11172209275765200601, 17614559707690567899, 5625334618578150331, 7846022135943990797, 3938188137922654906, 4435656894847999562, null, null, 5516344230632757773, null },
                SingleMember = (float)0.5796F,
                SingleArrayMember = new float[] { 0.5317F, 0.7187F, 0.5512F, 0.1995F, 0.4544F, 0.5836F, 0.4864F, 0.5422F, 0.7765F, 0.6595F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { 0.2478F, null, null, null, null, 0.4233F, 0.1003F, 0.6479F, 0.1234F, 0.1411F },
                DoubleMember = (double)0.0452,
                DoubleArrayMember = new double[] { 0.7066, 0.5898, 0.5127, 0.7907, 0.6016, 0.441, 0.4689, 0.0037, 0.596, 0.2406 },
                NullableDoubleMember = (double?)0.6787,
                NullableDoubleArrayMember = new double?[] { 0.7009, 0.4348, 0.8761, 0.7042, null, 0.6786, 0.8861, 0.0098, 0.3547, null },
                DecimalMember = (decimal)0.931922032466122m,
                DecimalArrayMember = new decimal[] { 0.989391411649711m, 0.823215453337513m, 0.14241873153598m, 0.43902682766273m, 0.242431684510052m, 0.442609491032832m, 0.962032919731938m, 0.207077479552048m, 0.700093278056054m, 0.17444667228239m },
                NullableDecimalMember = (decimal?)0.772722425299102m,
                NullableDecimalArrayMember = new decimal?[] { null, 0.213856349798784m, 0.623019132587602m, null, null, 0.541726743123367m, 0.526083667541893m, 0.90461282241373m, null, 0.827987673146644m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), null, Guid.NewGuid(), null, null, null, null, null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-293004552),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-22112151), DateTime.Now.AddSeconds(201260347), DateTime.Now.AddSeconds(-75982883), DateTime.Now.AddSeconds(46145657), DateTime.Now.AddSeconds(288327964), DateTime.Now.AddSeconds(296336140), DateTime.Now.AddSeconds(162798216), DateTime.Now.AddSeconds(-317321050), DateTime.Now.AddSeconds(-282893155), DateTime.Now.AddSeconds(-328333038) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-301162851), DateTime.Now.AddSeconds(348826714), DateTime.Now.AddSeconds(210031296), DateTime.Now.AddSeconds(290344660), null, DateTime.Now.AddSeconds(280366573), null, null, DateTime.Now.AddSeconds(-272544537), null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"dz2,M592pt",
                StringArrayMember = new string[] { "r@p5!rk6pL", "bi@@p0\"gKn", "pxs,fk#E'3", "ifuwW!iHU0", "LY:H2@37Vj", ",csBD\"Z1Zh", "ug;80@.Wvv", "R9MgbQ6lIk", "3M5g'F.ttc", "r45.F1iFd7" },
                CharMember = (char)',',
                CharArrayMember = new char[] { ',', '#', '1', 'P', '5', 'v', ' ', 'e', '#', 'S' },
                NullableCharMember = (char?)'2',
                NullableCharArrayMember = new char?[] { null, 'u', null, null, null, 'Q', null, 'u', 'o', 'n' },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, false, false, false, true, true, true, true, true, true },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { null, null, null, true, true, false, null, false, true, false },
                ByteMember = (byte)218,
                ByteArrayMember = new byte[] { 125, 168, 82, 154, 188, 27, 108, 118, 154, 74 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { 80, 197, null, 228, 201, 3, null, 111, null, 0 },
                SByteMember = (sbyte)13,
                SByteArrayMember = new sbyte[] { 66, 96, 118, 56, 10, -93, 21, 52, 77, 125 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { null, 71, 5, 3, 119, -19, 51, -79, null, -99 },
                Int16Member = (short)12249,
                Int16ArrayMember = new short[] { 7969, -26468, 31838, -9797, -4452, -1299, 19663, 11110, -3551, -13957 },
                NullableInt16Member = (short?)null,
                NullableInt16ArrayMember = new short?[] { 9329, null, null, 7428, -14360, 29104, null, 7384, 24923, -7481 },
                UInt16Member = (ushort)56508,
                UInt16ArrayMember = new ushort[] { 16836, 15812, 30089, 26945, 9087, 16919, 61318, 24735, 59265, 13574 },
                NullableUInt16Member = (ushort?)19564,
                NullableUInt16ArrayMember = new ushort?[] { null, 53377, null, null, 45703, 57927, 40211, 56750, 24949, 5204 },
                Int32Member = (int)1055991716,
                Int32ArrayMember = new int[] { -1979186204, -2112713959, -1112013014, -1550359352, -1774119744, 106826747, 747359440, 417674751, 1706543351, 185788394 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { 601415339, 1812629749, -666739738, -11749119, -1608387453, null, -642240084, 1610966069, null, -1533686612 },
                UInt32Member = (uint)1379422969,
                UInt32ArrayMember = new uint[] { 1406834768, 2570460712, 3801752265, 81428832, 2981559163, 3236206341, 451106787, 1104071072, 1454806067, 4254549103 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 61166094, 1098532093, 3108800156, 1003581465, 1481834745, 2508442572, 3056187747, 2162105233, 3498583766, null },
                Int64Member = (long)989517872451687405,
                Int64ArrayMember = new long[] { 7451992139074052317, -8689055847127889565, -6674005733556569986, 5001057003733235191, 2469041021810262450, -6404840579562223494, -5555071352124679256, 1264748209428226239, -5563042412228828190, -8631987633145033355 },
                NullableInt64Member = (long?)6360838442149902168,
                NullableInt64ArrayMember = new long?[] { null, null, 5294296072218712452, null, -8315691013254508991, null, 461495620865212331, -658881301243616411, -1650859788247245535, null },
                UInt64Member = (ulong)14375974823497572984,
                UInt64ArrayMember = new ulong[] { 17277725518548804799, 2972186058992399330, 12988700955974336407, 13396977254407103322, 5349004139810375196, 2691218470900000221, 11318868676728807395, 4835070374599202915, 2034632891380523847, 4234358005169149251 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 16191119752639837764, 9490152312984598995, null, 14105886023408701350, null, 6531161858823836909, null, 17183101095685390502, 15598043765723592449, 9184649898692136782 },
                SingleMember = (float)0.9789F,
                SingleArrayMember = new float[] { 0.6204F, 0.7362F, 0.25F, 0.5549F, 0.2501F, 0.2979F, 0.6639F, 0.9856F, 0.9648F, 0.967F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { null, null, 0.5341F, 0.4333F, 0.0958F, 0.0701F, null, null, 0.7701F, null },
                DoubleMember = (double)0.6319,
                DoubleArrayMember = new double[] { 0.4297, 0.0916, 0.7347, 0.53, 0.3293, 0.9483, 0.8173, 0.836, 0.4297, 0.0339 },
                NullableDoubleMember = (double?)0.7065,
                NullableDoubleArrayMember = new double?[] { null, 0.0153, 0.6481, 0.8078, 0.2717, 0.7888, null, null, 0.2015, 0.5286 },
                DecimalMember = (decimal)0.216720762763508m,
                DecimalArrayMember = new decimal[] { 0.507984621686854m, 0.760900004655542m, 0.555280762983151m, 0.990094789299227m, 0.325870137347779m, 0.40314501729009m, 0.75987441873172m, 0.79681884581075m, 0.40927986400634m, 0.263135147869184m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.3523021230252m, null, 0.126314640569647m, null, 0.759424841385067m, null, null, null, 0.503339668038925m, 0.390195289342755m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-244908382),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(68122317), DateTime.Now.AddSeconds(-342505392), DateTime.Now.AddSeconds(174621748), DateTime.Now.AddSeconds(-319583264), DateTime.Now.AddSeconds(160653707), DateTime.Now.AddSeconds(-207708374), DateTime.Now.AddSeconds(338321565), DateTime.Now.AddSeconds(94272456), DateTime.Now.AddSeconds(-318371594), DateTime.Now.AddSeconds(-206207236) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(333432327),
                NullableDateTimeArrayMember = new DateTime?[] { null, null, DateTime.Now.AddSeconds(161963375), DateTime.Now.AddSeconds(51163627), null, DateTime.Now.AddSeconds(146630102), null, null, null, DateTime.Now.AddSeconds(25512998) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"HhMxRCQLw9",
                StringArrayMember = new string[] { "K,t?1iyg\"u", "IRzSDjDYbw", "50HXdV IP?", "?y#6S3L'Jt", "DCM6z?5\"u4", "%:7 Eh3Vjf", "iqQD5gU\"AW", "YLP5t2oYun", "nGkh:$:#28", "A?g0Kk8IBS" },
                CharMember = (char)'J',
                CharArrayMember = new char[] { 'T', 'u', '0', 'T', ' ', 'x', 'h', '1', 'V', 'o' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { null, null, 'u', 'F', 'F', '5', null, 'a', 'V', null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, false, true, false, true, true, false, true, true, false },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { true, true, true, false, true, false, false, false, null, false },
                ByteMember = (byte)209,
                ByteArrayMember = new byte[] { 220, 202, 46, 96, 73, 253, 252, 100, 250, 96 },
                NullableByteMember = (byte?)182,
                NullableByteArrayMember = new byte?[] { 117, null, 123, null, 113, 133, null, 234, 121, 51 },
                SByteMember = (sbyte)1,
                SByteArrayMember = new sbyte[] { 47, 32, -123, -29, 79, -49, -102, -77, 16, 111 },
                NullableSByteMember = (sbyte?)-113,
                NullableSByteArrayMember = new sbyte?[] { null, -65, 16, null, null, -22, null, 21, -44, -61 },
                Int16Member = (short)32205,
                Int16ArrayMember = new short[] { -23702, 29702, -6251, 7952, -20753, -31625, -29821, -17226, 1785, 12837 },
                NullableInt16Member = (short?)18394,
                NullableInt16ArrayMember = new short?[] { -10935, -20627, 16810, null, null, 5729, -8110, -32367, 21346, -23338 },
                UInt16Member = (ushort)9991,
                UInt16ArrayMember = new ushort[] { 55914, 28335, 37767, 62551, 37410, 41992, 32650, 39909, 16996, 3390 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 58083, 60248, 55274, 51308, 49022, null, 46201, null, 2801, 21001 },
                Int32Member = (int)827705845,
                Int32ArrayMember = new int[] { -1059158629, 214248937, -1128893877, 1241472312, -1919725226, 2017261232, -2146577627, 730743145, 2049601228, -1412468275 },
                NullableInt32Member = (int?)-1449977559,
                NullableInt32ArrayMember = new int?[] { 1288458229, -2117585501, null, 538286694, -1948055134, null, null, -2115667067, null, null },
                UInt32Member = (uint)2734507948,
                UInt32ArrayMember = new uint[] { 3267810389, 4262352236, 839537740, 2584339648, 815160802, 1351679521, 2010269013, 2214769538, 1665196237, 1623062429 },
                NullableUInt32Member = (uint?)1972031123,
                NullableUInt32ArrayMember = new uint?[] { 914243861, 2177675599, 644642115, 1385790358, 2795822948, 3056967915, null, null, 2435883995, 3551719500 },
                Int64Member = (long)3374733017728796595,
                Int64ArrayMember = new long[] { 7094214680891328452, -4307474124734235555, 6636893799443950325, -4298765673830309655, 2771477470504404148, -1381509905526650600, -3931740759086547570, -6413913110935664787, -5714535362608479707, 309796833781527284 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { -6893870664315328593, 6179093595948830603, -1554578545390486380, -8846408391295355591, null, -2223090753280533712, 5795991200509604749, 7717900085362137861, null, null },
                UInt64Member = (ulong)14679016968129570738,
                UInt64ArrayMember = new ulong[] { 15255370592994174315, 11818181678297859090, 9625318726602346624, 2295088282522937756, 6311934639178902332, 1576678187156573082, 7667441363497363301, 5129781419970450705, 110415017966164587, 2238409331196474808 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 5945318662895696809, 13496121451224744775, 10817192336031100351, null, 3890031219270001661, 4485754056252982888, 15735425030999109219, 6156891547393711241, 5938460064594470275, null },
                SingleMember = (float)0.4793F,
                SingleArrayMember = new float[] { 0.2019F, 0.0709F, 0.6114F, 0.0761F, 0.3399F, 0.4424F, 0.5076F, 0.9218F, 0.0403F, 0.4095F },
                NullableSingleMember = (float?)0.0051F,
                NullableSingleArrayMember = new float?[] { 0.8559F, null, null, null, null, 0.5906F, 0.7957F, null, null, 0.0187F },
                DoubleMember = (double)0.4312,
                DoubleArrayMember = new double[] { 0.5712, 0.649, 0.0587, 0.6086, 0.8381, 0.5871, 0.9444, 0.171, 0.3915, 0.4866 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.886, 0.5173, null, 0.1526, 0.3027, null, 0.5469, null, null, 0.3028 },
                DecimalMember = (decimal)0.989481734107007m,
                DecimalArrayMember = new decimal[] { 0.29713178579562m, 0.918891679457804m, 0.534414005249m, 0.707481613712144m, 0.355546925382478m, 0.134045246585293m, 0.99035671678854m, 0.1921086563692m, 0.413546667626848m, 0.399026094190323m },
                NullableDecimalMember = (decimal?)0.0648849788424023m,
                NullableDecimalArrayMember = new decimal?[] { 0.453981931067063m, 0.213739268581262m, 0.77287254378799m, 0.189424620098166m, null, null, 0.546777221163165m, null, 0.35447358915325m, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-302013542),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(47521275), DateTime.Now.AddSeconds(-152197281), DateTime.Now.AddSeconds(302806894), DateTime.Now.AddSeconds(273448766), DateTime.Now.AddSeconds(210872501), DateTime.Now.AddSeconds(-165715814), DateTime.Now.AddSeconds(57706598), DateTime.Now.AddSeconds(-35443367), DateTime.Now.AddSeconds(-290387764), DateTime.Now.AddSeconds(-152954786) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { null, DateTime.Now.AddSeconds(-185849426), null, DateTime.Now.AddSeconds(66238532), DateTime.Now.AddSeconds(306912333), DateTime.Now.AddSeconds(2096422), DateTime.Now.AddSeconds(191494251), DateTime.Now.AddSeconds(-278466737), DateTime.Now.AddSeconds(221775225), null },
            },
        };
    }
}
