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
                StringMember = (string)"9W6O;6CS1t",
                StringArrayMember = new string[] { "!SA1,vfYBb", "ryqVh:lhS:", "\"4RDRND#Cb", "I.1lO#T5%?", "Qp8B1L2k3 ", "Ur 9zNA#jy", "rjWwT3$FHl", "u2jdyb@PYG", "hpPDHo.:8B", "@WVXU3TM5%" },
                CharMember = (char)'G',
                CharArrayMember = new char[] { 'r', '9', 'e', 'E', '%', 'M', 'u', 'f', 'U', 'U' },
                NullableCharMember = (char?)'o',
                NullableCharArrayMember = new char?[] { 'Q', 'K', 'C', 'V', 'k', '3', '4', '5', 'U', '6' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, false, true, false, false, true, false, true, false, false },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { true, false, null, false, true, true, true, false, true, true },
                ByteMember = (byte)36,
                ByteArrayMember = new byte[] { 4, 120, 78, 26, 55, 82, 57, 64, 16, 103 },
                NullableByteMember = (byte?)253,
                NullableByteArrayMember = new byte?[] { null, null, null, null, 223, null, 148, null, 20, 13 },
                SByteMember = (sbyte)70,
                SByteArrayMember = new sbyte[] { -2, 24, 87, 53, -72, 116, 73, -58, 15, 110 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { null, 41, 50, null, null, 30, 76, 28, null, -59 },
                Int16Member = (short)21622,
                Int16ArrayMember = new short[] { -4451, -29720, -7401, -4368, -14676, -22750, 24233, 387, 20451, -24716 },
                NullableInt16Member = (short?)null,
                NullableInt16ArrayMember = new short?[] { -6337, -32495, -21340, -31401, null, 18826, null, -28571, 9569, null },
                UInt16Member = (ushort)43676,
                UInt16ArrayMember = new ushort[] { 49737, 26512, 37810, 39451, 62464, 2534, 17039, 57215, 5451, 40138 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { null, 11321, 21257, 40045, 56492, 44511, 45859, null, 61018, 44720 },
                Int32Member = (int)1237122970,
                Int32ArrayMember = new int[] { 2020534969, -2011200460, 2016512589, -704375294, -1093877474, -456743245, -1481152579, 1674613747, 1212807788, -1080316962 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { null, 32663021, -644061264, null, null, null, -860190172, -1162177160, -1127479540, 537732467 },
                UInt32Member = (uint)2275944309,
                UInt32ArrayMember = new uint[] { 189115790, 25395039, 2664080449, 4163559472, 561264781, 1620015925, 4084266061, 581572141, 1059513511, 3239806526 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { null, 3734135254, null, 2990320547, null, 3202620946, null, null, 2818190035, 3119168076 },
                Int64Member = (long)-1087403103251343545,
                Int64ArrayMember = new long[] { -8686286910002351811, -3418899702419456414, 4027633630995663330, -6505129780362135574, 2630129221940955079, -6366052641089420370, 3926351469496515402, 4582487842300557132, -3875089688722476091, -5882305183600949944 },
                NullableInt64Member = (long?)-3921567789391617013,
                NullableInt64ArrayMember = new long?[] { null, 4985179577369574047, null, 3248663106106176025, null, -3898323568849610127, -6660654990081370328, null, -8873931451258344700, 424561277029141907 },
                UInt64Member = (ulong)1675702141556567766,
                UInt64ArrayMember = new ulong[] { 12420111453338087223, 7569640598664674312, 4174726766897292557, 14525046558297457832, 6447312700352302063, 1227159094356091911, 11256194286508797218, 12954911938173059135, 11001220113362976488, 16899795671143473387 },
                NullableUInt64Member = (ulong?)15412929539154977346,
                NullableUInt64ArrayMember = new ulong?[] { null, 16521555218042597373, 3451880205007326941, 17690858804387576954, 12579298283157268354, 924712739702590589, null, 14148634969834435632, 13746682583706555589, 16314888962890394594 },
                SingleMember = (float)0.8387F,
                SingleArrayMember = new float[] { 0.4127F, 0.2209F, 0.6469F, 0.0001F, 0.8804F, 0.6426F, 0.6563F, 0.2295F, 0.4496F, 0.0571F },
                NullableSingleMember = (float?)0.9778F,
                NullableSingleArrayMember = new float?[] { 0.7029F, 0.314F, null, null, 0.7333F, 0.3753F, 0.7034F, null, null, 0.2522F },
                DoubleMember = (double)0.5836,
                DoubleArrayMember = new double[] { 0.6502, 0.1058, 0.8779, 0.01, 0.4748, 0.2974, 0.5169, 0.3507, 0.479, 0.0206 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { null, 0.326, null, 0.3131, null, null, 0.7996, null, null, 0.4968 },
                DecimalMember = (decimal)0.67380387693355m,
                DecimalArrayMember = new decimal[] { 0.512653538264638m, 0.664892729681401m, 0.310011254767893m, 0.139843080257924m, 0.844023912141111m, 0.796975144556246m, 0.43960020758193m, 0.567602997910046m, 0.829288154295314m, 0.852509494336559m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.30295739942368m, 0.298930469573909m, 0.255750062528881m, 0.68463812707208m, 0.100316103594525m, 0.0593425194077857m, 0.834958026574439m, 0.0644740294965329m, 0.240782244708753m, 0.159450964610768m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-9516842),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(29464721), DateTime.Now.AddSeconds(287519255), DateTime.Now.AddSeconds(-12518144), DateTime.Now.AddSeconds(-307644194), DateTime.Now.AddSeconds(-7588225), DateTime.Now.AddSeconds(-28426734), DateTime.Now.AddSeconds(102673458), DateTime.Now.AddSeconds(-39899643), DateTime.Now.AddSeconds(195356749), DateTime.Now.AddSeconds(229641547) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(114206491), null, null, null, DateTime.Now.AddSeconds(24320992), null, DateTime.Now.AddSeconds(-296802379), DateTime.Now.AddSeconds(195991507), DateTime.Now.AddSeconds(-130980157), null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"OxdSboItLX",
                StringArrayMember = new string[] { "R8vdQCUSp1", "\"FTFqXhLI!", "I6ga:7M#yp", "fQ2k6Jy'HS", "jAuDEIeXyE", "H$lRPibUlf", "?Xr Wza5Wh", "wpD?iHLdV6", "c,Vuii2lVX", "5ddycLgu4c" },
                CharMember = (char)'P',
                CharArrayMember = new char[] { 'g', 'N', 'x', '$', ' ', 'k', 'N', ';', 'a', 'J' },
                NullableCharMember = (char?)'h',
                NullableCharArrayMember = new char?[] { 'A', '8', null, null, 'R', 'D', 'I', '"', 'Q', 'd' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, true, false, true, false, false, true, false, false, true },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { null, null, true, true, null, false, true, false, false, false },
                ByteMember = (byte)33,
                ByteArrayMember = new byte[] { 217, 60, 120, 34, 43, 24, 172, 48, 220, 234 },
                NullableByteMember = (byte?)123,
                NullableByteArrayMember = new byte?[] { null, 222, 171, 187, null, null, 43, 51, 161, null },
                SByteMember = (sbyte)38,
                SByteArrayMember = new sbyte[] { -74, 21, 28, 17, -116, 32, 106, 21, 113, 66 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { -1, null, -47, null, 30, 112, 124, null, 41, 49 },
                Int16Member = (short)-30398,
                Int16ArrayMember = new short[] { -6505, -7778, 10196, 10455, -20685, -1023, 24838, 18781, -9985, 8651 },
                NullableInt16Member = (short?)-1884,
                NullableInt16ArrayMember = new short?[] { null, 3765, null, null, null, 19956, -21270, 6603, -1362, 18395 },
                UInt16Member = (ushort)25539,
                UInt16ArrayMember = new ushort[] { 7201, 53722, 62464, 33625, 14934, 35383, 25031, 22418, 23635, 36274 },
                NullableUInt16Member = (ushort?)60978,
                NullableUInt16ArrayMember = new ushort?[] { null, 44855, 14416, null, 61509, 49745, 59308, null, 8790, 40233 },
                Int32Member = (int)2144950998,
                Int32ArrayMember = new int[] { -145463861, 821491595, 607544429, -221318829, 848381733, -1762007658, -1289130252, -1195385564, 1743719242, -1039564681 },
                NullableInt32Member = (int?)-1784438883,
                NullableInt32ArrayMember = new int?[] { 441876901, -391722865, null, null, 1137581059, -646011761, null, 1034189671, 1447491950, 1554499932 },
                UInt32Member = (uint)951196934,
                UInt32ArrayMember = new uint[] { 4290272281, 599004602, 1886219146, 554930464, 2997864538, 3706996568, 9542585, 530901733, 3040366335, 1426318437 },
                NullableUInt32Member = (uint?)2361346412,
                NullableUInt32ArrayMember = new uint?[] { 3383147478, null, null, 1758584832, null, 1051951902, 3513863878, null, 1522824891, null },
                Int64Member = (long)-4801007609742403318,
                Int64ArrayMember = new long[] { 715317430686143994, -8874955339675432559, 2921131312103634651, 432506845778056110, 4775705837859617077, 3954449814876629855, 669565403226101380, -118601244811032373, 2620167486731397769, -6484946944812059664 },
                NullableInt64Member = (long?)8029590697576809551,
                NullableInt64ArrayMember = new long?[] { null, 2942347797144311967, 4385672035221862239, -2343240184810377854, null, null, null, null, null, 8538117355811397402 },
                UInt64Member = (ulong)16870288915719133592,
                UInt64ArrayMember = new ulong[] { 16269798712301822148, 10401703506914568466, 9557150311436150911, 9683759675567482735, 17560222049110403008, 5955955038370931746, 17534936827505415615, 8269321932296247294, 1055516938066464134, 11670647994560034741 },
                NullableUInt64Member = (ulong?)14853825299695042848,
                NullableUInt64ArrayMember = new ulong?[] { 15736028407283225447, null, 17024822036255577073, null, 13404782954050258287, null, null, 13841385824371754424, null, 11004228393141393065 },
                SingleMember = (float)0.6247F,
                SingleArrayMember = new float[] { 0.7057F, 0.4932F, 0.5601F, 0.7989F, 0.8648F, 0.5804F, 0.1686F, 0.8448F, 0.481F, 0.1224F },
                NullableSingleMember = (float?)0.0741F,
                NullableSingleArrayMember = new float?[] { 0.9834F, 0.3646F, 0.2064F, 0.2753F, null, 0.4234F, 0.1452F, 0.0374F, 0.9435F, null },
                DoubleMember = (double)0.229,
                DoubleArrayMember = new double[] { 0.6601, 0.1672, 0.4846, 0.0231, 0.4029, 0.0223, 0.4313, 0.2242, 0.4097, 0.966 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.3317, null, 0.4949, 0.1547, 0.5726, 0.7705, null, 0.0316, null, 0.4356 },
                DecimalMember = (decimal)0.320309004429872m,
                DecimalArrayMember = new decimal[] { 0.384580304093929m, 0.51859371527964m, 0.731411833656678m, 0.384218888536198m, 0.408309037987287m, 0.730744895399895m, 0.689406335674881m, 0.652359505953435m, 0.422231666940372m, 0.340238339891768m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { 0.63277123106307m, 0.537104655772962m, 0.0732903499497521m, 0.206464302822232m, 0.442969011349123m, null, null, null, null, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), null, null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(261507974),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-302366105), DateTime.Now.AddSeconds(61747826), DateTime.Now.AddSeconds(261893523), DateTime.Now.AddSeconds(-347914414), DateTime.Now.AddSeconds(-63068801), DateTime.Now.AddSeconds(27872202), DateTime.Now.AddSeconds(95156947), DateTime.Now.AddSeconds(-321582723), DateTime.Now.AddSeconds(-303077483), DateTime.Now.AddSeconds(186388497) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(346735593), DateTime.Now.AddSeconds(-48749244), DateTime.Now.AddSeconds(51428748), null, null, DateTime.Now.AddSeconds(133783664), DateTime.Now.AddSeconds(-131851392), null, DateTime.Now.AddSeconds(-6814348), DateTime.Now.AddSeconds(-62714463) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"5vARfp\"v6H",
                StringArrayMember = new string[] { "X% 2CBC#o,", "#?k ySLcjW", "yov\"B4.DP'", "pG1NA6eyvb", "JjG\"f'LBpM", "dnMkg5ZF0V", "N%4vV;KW%'", ",k3INOi2lI", "s8lCmB!R4R", "gY$ZNK1hup" },
                CharMember = (char)' ',
                CharArrayMember = new char[] { 'W', 'Z', 'Y', 'j', 'C', 'q', 'N', '0', 'p', '!' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { 'a', 'V', '3', '$', '0', 'o', 'd', 'o', 'G', '#' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, false, true, true, true, false, false, true, false, false },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { true, false, null, null, true, false, true, false, null, false },
                ByteMember = (byte)148,
                ByteArrayMember = new byte[] { 38, 58, 106, 42, 49, 95, 4, 150, 69, 25 },
                NullableByteMember = (byte?)168,
                NullableByteArrayMember = new byte?[] { null, 112, 99, 2, 185, null, null, 156, null, 17 },
                SByteMember = (sbyte)88,
                SByteArrayMember = new sbyte[] { -69, -41, -20, 47, -94, -112, 120, -46, 77, -16 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { -12, 52, -111, 6, -19, 23, -9, 117, null, 94 },
                Int16Member = (short)-27854,
                Int16ArrayMember = new short[] { 13060, 24930, 10060, 544, -4621, 11494, 6174, -22329, -27350, -20979 },
                NullableInt16Member = (short?)null,
                NullableInt16ArrayMember = new short?[] { 17584, null, null, -29325, null, -32145, null, 7388, -13107, -2171 },
                UInt16Member = (ushort)51364,
                UInt16ArrayMember = new ushort[] { 44438, 64339, 49974, 11758, 47847, 17864, 12995, 48670, 17514, 19159 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { null, 51888, null, 52443, null, null, null, 47821, 53545, null },
                Int32Member = (int)-282009079,
                Int32ArrayMember = new int[] { -83287388, -185827911, -15756928, 2136784406, 1364631535, -1628489735, 1403436053, -1409096153, 624201588, 666987768 },
                NullableInt32Member = (int?)929558234,
                NullableInt32ArrayMember = new int?[] { 661429738, null, 1844829835, null, -1713963547, -1973252198, -2105067638, 1226768431, null, null },
                UInt32Member = (uint)1328938732,
                UInt32ArrayMember = new uint[] { 1507247490, 2976287515, 1298624254, 3521420937, 4188548775, 1644480062, 2045251145, 3539183891, 1505987634, 337795501 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 284255090, 2961760215, 3155246166, 2735542388, 2491045277, 613079804, null, 1403034004, 282867825, 2083911819 },
                Int64Member = (long)6842246816298348358,
                Int64ArrayMember = new long[] { 1684745305521280346, -6138059053194327007, -4657427087510373338, -1159342080130675952, -5760850801018788886, 839537056348218877, -2620668498294056739, -1039736936336401673, -3606872047190275230, 7943833058526099566 },
                NullableInt64Member = (long?)-9122590611819698533,
                NullableInt64ArrayMember = new long?[] { null, -3424966755059201826, -1563220318585095759, -5393205270068541816, 1424709966580561634, -4771746753532589246, -480157974534989684, -7589643139684024003, 868724444092114807, null },
                UInt64Member = (ulong)3666726026722288878,
                UInt64ArrayMember = new ulong[] { 15457665254411701078, 3111660088544349753, 8910244227863403893, 18113706622927905360, 28485754433101993, 1746120586770751313, 14647731344569879285, 7540023473570226147, 4793981410565519085, 15640871850649502839 },
                NullableUInt64Member = (ulong?)10986511883622155050,
                NullableUInt64ArrayMember = new ulong?[] { 1079673804691670439, 10317680025729425909, 4295704588754287828, 18183533211493337531, 1827061012753913425, 11719714434152842025, null, 306312249747207246, 18004618714579387501, null },
                SingleMember = (float)0.2633F,
                SingleArrayMember = new float[] { 0.8577F, 0.4041F, 0.6047F, 0.7439F, 0.5639F, 0.3982F, 0.1789F, 0.2373F, 0.1225F, 0.9671F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { 0.2618F, 0.6593F, 0.3919F, 0.3389F, 0.4107F, null, 0.0824F, 0.0698F, 0.6537F, 0.543F },
                DoubleMember = (double)0.784,
                DoubleArrayMember = new double[] { 0.2592, 0.4985, 0.186, 0.5132, 0.2458, 0.6373, 0.3496, 0.8661, 0.4559, 0.6177 },
                NullableDoubleMember = (double?)null,
                NullableDoubleArrayMember = new double?[] { 0.7284, 0.6598, null, 0.2441, 0.8944, 0.6135, null, null, 0.3922, 0.8292 },
                DecimalMember = (decimal)0.304885053683484m,
                DecimalArrayMember = new decimal[] { 0.0770004638829271m, 0.732501115059714m, 0.526218987315064m, 0.326820753201293m, 0.585587526478613m, 0.693058529725791m, 0.266297775910375m, 0.396296037545566m, 0.78436368414404m, 0.317792635559008m },
                NullableDecimalMember = (decimal?)0.721436322071327m,
                NullableDecimalArrayMember = new decimal?[] { null, null, 0.882045587469845m, 0.564269030263773m, 0.194984765814145m, 0.229918792485222m, 0.840090870317114m, 0.0372363249944692m, 0.877339993080748m, 0.945105941940614m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, null, null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(214975618),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-258472253), DateTime.Now.AddSeconds(15588319), DateTime.Now.AddSeconds(173335963), DateTime.Now.AddSeconds(268822497), DateTime.Now.AddSeconds(60065292), DateTime.Now.AddSeconds(55039562), DateTime.Now.AddSeconds(248352530), DateTime.Now.AddSeconds(283463226), DateTime.Now.AddSeconds(227555961), DateTime.Now.AddSeconds(-205866091) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-24399139), DateTime.Now.AddSeconds(107830879), null, DateTime.Now.AddSeconds(-102699967), null, null, null, DateTime.Now.AddSeconds(59195349), DateTime.Now.AddSeconds(287105485), DateTime.Now.AddSeconds(327142985) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"5,CxY\"7DWR",
                StringArrayMember = new string[] { "Xurr0ck2JU", "@'p4immd!C", "jAH#a36.0s", "R;!m!93cw8", "x Yat?WVO8", "ddJU4XmvR#", "twBnCF%C9v", "6J4Jbky4Zk", "hkw'P,4x5u", "XILzPB$8Yo" },
                CharMember = (char)'B',
                CharArrayMember = new char[] { '$', 'J', 'g', 'H', '6', 'n', 'w', 'S', 'M', ';' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { 'v', null, 'L', null, 'l', ':', 'W', '8', 'G', 'B' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, true, true, true, true, false, true, true, true },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { true, true, null, true, null, false, true, false, null, false },
                ByteMember = (byte)119,
                ByteArrayMember = new byte[] { 237, 252, 117, 30, 154, 182, 176, 124, 81, 89 },
                NullableByteMember = (byte?)null,
                NullableByteArrayMember = new byte?[] { 29, null, null, 198, 8, 43, 69, null, 218, null },
                SByteMember = (sbyte)-31,
                SByteArrayMember = new sbyte[] { 116, -72, 59, 54, -21, 125, -87, -36, 3, 84 },
                NullableSByteMember = (sbyte?)30,
                NullableSByteArrayMember = new sbyte?[] { null, -114, null, null, 10, null, null, 50, null, 63 },
                Int16Member = (short)7871,
                Int16ArrayMember = new short[] { -1122, -6632, 23377, 14918, 5507, -31860, 11550, -29298, 30125, -12075 },
                NullableInt16Member = (short?)-598,
                NullableInt16ArrayMember = new short?[] { null, null, -14204, -1918, 9713, -32317, null, null, null, -460 },
                UInt16Member = (ushort)10284,
                UInt16ArrayMember = new ushort[] { 55843, 58119, 38818, 14377, 33123, 50660, 34905, 6912, 12707, 32517 },
                NullableUInt16Member = (ushort?)null,
                NullableUInt16ArrayMember = new ushort?[] { 52449, null, 30966, 39317, null, 7202, 52362, 6809, null, null },
                Int32Member = (int)-435563842,
                Int32ArrayMember = new int[] { 546863359, -238577003, 1355056781, -665580105, 308530132, -1930780875, 618061404, 707199916, 567129036, -319471113 },
                NullableInt32Member = (int?)436009541,
                NullableInt32ArrayMember = new int?[] { -1359670968, null, null, -1923043082, null, null, -1991924796, 1074037657, null, null },
                UInt32Member = (uint)2567404534,
                UInt32ArrayMember = new uint[] { 1897154616, 3894214514, 4248171869, 2992792405, 1752212161, 836239902, 1544639378, 2149848901, 82637267, 2703631350 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 329728532, 3145897512, 914071051, 3978288867, null, null, 1663677649, 2074000222, 2515428402, 3337162367 },
                Int64Member = (long)-7272850690854452354,
                Int64ArrayMember = new long[] { -562611456979853219, 457227806873342344, -5949237435650715457, -9048353479070367024, -893329626572641634, 1772864576728039581, 5514787548443124778, -4718157928685144017, 9121280832184818532, 6250132416992551605 },
                NullableInt64Member = (long?)-6762027395557930050,
                NullableInt64ArrayMember = new long?[] { 70898257822873564, null, 7840262889016426630, 8186965993055189750, null, null, -5348976253175542745, null, 7871772974721749142, null },
                UInt64Member = (ulong)8679686042492644412,
                UInt64ArrayMember = new ulong[] { 13062544362974058197, 76379543690546606, 5972440184345832384, 1800404050603574737, 5298996422050899664, 16236786266096949394, 8558006134948736924, 6684562388122372019, 17282112709676085264, 2971441960993942050 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { null, null, null, 15610592248908667697, null, 17253331288787096636, 413884116827196633, 15338774905089830607, 9202533088308953026, 7429696657913514350 },
                SingleMember = (float)0.9871F,
                SingleArrayMember = new float[] { 0.9013F, 0.8521F, 0.8591F, 0.0156F, 0.67F, 0.2421F, 0.2395F, 0.7533F, 0.9664F, 0.1043F },
                NullableSingleMember = (float?)0.0486F,
                NullableSingleArrayMember = new float?[] { 0.0731F, 0.1616F, null, null, 0.6455F, null, null, null, 0.244F, null },
                DoubleMember = (double)0.4064,
                DoubleArrayMember = new double[] { 0.9916, 0.0065, 0.1885, 0.9957, 0.4402, 0.1505, 0.4675, 0.2313, 0.8141, 0.0859 },
                NullableDoubleMember = (double?)0.5414,
                NullableDoubleArrayMember = new double?[] { null, 0.537, null, null, 0.8542, 0.5573, 0.1524, 0.5412, null, null },
                DecimalMember = (decimal)0.198784278332621m,
                DecimalArrayMember = new decimal[] { 0.778668678262582m, 0.926053780096608m, 0.42585984171641m, 0.612405871791954m, 0.919858390893721m, 0.896012679625308m, 0.013417586690475m, 0.830169426198196m, 0.621937563932472m, 0.366473455152695m },
                NullableDecimalMember = (decimal?)0.752740971628922m,
                NullableDecimalArrayMember = new decimal?[] { 0.158578768912041m, null, 0.903530742928167m, null, null, 0.968358525060284m, 0.287270492076534m, null, 0.756308520564953m, 0.298225344297581m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, null, Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), null, null, null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-156400783),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-81550246), DateTime.Now.AddSeconds(-145860336), DateTime.Now.AddSeconds(293745546), DateTime.Now.AddSeconds(188109074), DateTime.Now.AddSeconds(-192128590), DateTime.Now.AddSeconds(-186558342), DateTime.Now.AddSeconds(305394770), DateTime.Now.AddSeconds(-179199823), DateTime.Now.AddSeconds(285027282), DateTime.Now.AddSeconds(273150719) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(52898225), DateTime.Now.AddSeconds(173872787), DateTime.Now.AddSeconds(-261737504), DateTime.Now.AddSeconds(-191817197), DateTime.Now.AddSeconds(110317729), DateTime.Now.AddSeconds(315598145), DateTime.Now.AddSeconds(-156566493), DateTime.Now.AddSeconds(-226184782), DateTime.Now.AddSeconds(306392970), DateTime.Now.AddSeconds(331717011) },
            },
            new DataConvertersTestClass {
                StringMember = (string)":TiqCTv 'T",
                StringArrayMember = new string[] { "v4V3mIAJXO", "XYUPXx%E!a", "m?g8tOBF?@", "P44l%p9eNG", "A,kJQp%TSb", "VqFo46YkJY", "G!Uc%qq6R4", ",q:.ZybWrD", "P5.kwvyP4P", "RYV, GqIKx" },
                CharMember = (char)':',
                CharArrayMember = new char[] { 'u', 'W', '%', 'd', '2', 'Q', 'n', 'N', 'T', 'P' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { 'n', 'm', 'o', 'a', null, 'P', '%', 'S', 'I', 't' },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, true, true, false, true, true, false, true, false, false },
                NullableBooleanMember = (bool?)false,
                NullableBooleanArrayMember = new bool?[] { true, null, true, null, true, null, null, null, true, true },
                ByteMember = (byte)219,
                ByteArrayMember = new byte[] { 119, 140, 144, 79, 241, 31, 86, 27, 112, 138 },
                NullableByteMember = (byte?)10,
                NullableByteArrayMember = new byte?[] { 88, 141, null, 155, 191, 213, null, 86, 30, 213 },
                SByteMember = (sbyte)-64,
                SByteArrayMember = new sbyte[] { -73, -20, -102, -32, -45, 110, -114, 20, 5, 56 },
                NullableSByteMember = (sbyte?)-56,
                NullableSByteArrayMember = new sbyte?[] { 102, -126, -18, null, -106, null, -10, -13, 85, 5 },
                Int16Member = (short)32268,
                Int16ArrayMember = new short[] { -679, 15993, 32319, 8464, -22546, -524, -1269, -10299, -1411, 7168 },
                NullableInt16Member = (short?)-1646,
                NullableInt16ArrayMember = new short?[] { 3018, 5177, -21694, null, null, null, -30454, -6140, 32002, 30405 },
                UInt16Member = (ushort)2180,
                UInt16ArrayMember = new ushort[] { 45524, 47428, 34158, 2464, 5549, 63938, 46394, 41320, 42161, 59598 },
                NullableUInt16Member = (ushort?)14106,
                NullableUInt16ArrayMember = new ushort?[] { null, 54929, 25707, 23801, 34905, null, 3878, 64812, 49498, null },
                Int32Member = (int)1333188243,
                Int32ArrayMember = new int[] { 1945019631, 801412733, -1154190851, -1821572395, 675184107, -1653473504, -784115073, 486526402, -1144936456, 541497503 },
                NullableInt32Member = (int?)706234483,
                NullableInt32ArrayMember = new int?[] { -1613626706, 793614358, 586371123, null, 410070892, -189644498, -1406591690, -1398705319, -371776514, -2016380757 },
                UInt32Member = (uint)1976523714,
                UInt32ArrayMember = new uint[] { 2478142772, 3003598099, 1909331830, 2984307186, 3625764663, 1192717372, 291396059, 3940547076, 4009049601, 1504234177 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 589319612, null, 4220401709, 1590180343, null, 2935416505, 3010454327, 510456050, 3400540259, null },
                Int64Member = (long)4294895465625230049,
                Int64ArrayMember = new long[] { 3360628447192419976, 7068805268064831674, 6830377704956190810, -8229172593236216572, -589499789188766763, 8129278609628477484, 2874748872357684481, -7057630365582910652, -5957573787035050693, 327543732721529230 },
                NullableInt64Member = (long?)9055804823122435669,
                NullableInt64ArrayMember = new long?[] { 8586932257426601058, null, 3295694099872236391, -2000256399357387576, -2517478042764395816, -7091542329401848821, null, 4086393796086802639, 2999281925234415972, -2256236937987935108 },
                UInt64Member = (ulong)6408960611191457993,
                UInt64ArrayMember = new ulong[] { 10467776281392380937, 11290865835069910350, 17973526859732787945, 14702729179527191527, 7761187666804923004, 10646600388040738371, 4531876738939481558, 13278562814774062478, 11713217397003133603, 10760044085524358380 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 14912834446808734848, 7997266307718313569, 18314367684632507141, null, null, 8857013236084202034, 3291053017462687032, 9252241711484841698, null, 520912595032197770 },
                SingleMember = (float)0.5224F,
                SingleArrayMember = new float[] { 0.7983F, 0.4941F, 0.6761F, 0.5252F, 0.4059F, 0.5796F, 0.98F, 0.568F, 0.1265F, 0.4027F },
                NullableSingleMember = (float?)0.9893F,
                NullableSingleArrayMember = new float?[] { 0.1525F, 0.3725F, 0.4417F, null, 0.0092F, 0.9083F, 0.1678F, 0.8975F, null, 0.3112F },
                DoubleMember = (double)0.0459,
                DoubleArrayMember = new double[] { 0.666, 0.589, 0.8536, 0.7478, 0.5204, 0.4597, 0.4644, 0.6112, 0.3866, 0.1478 },
                NullableDoubleMember = (double?)0.7658,
                NullableDoubleArrayMember = new double?[] { 0.2896, 0.1582, 0.353, 0.2919, 0.9233, null, 0.0353, 0.849, 0.8541, null },
                DecimalMember = (decimal)0.548351201484143m,
                DecimalArrayMember = new decimal[] { 0.838341503328803m, 0.388957782829626m, 0.559739999268083m, 0.839257742203426m, 0.119008357226387m, 0.136042902775129m, 0.795382541974719m, 0.0991453254125758m, 0.788797323493658m, 0.112420230224924m },
                NullableDecimalMember = (decimal?)0.746660177012282m,
                NullableDecimalArrayMember = new decimal?[] { 0.122408861817051m, 0.104433356367253m, null, 0.681512101405073m, null, null, 0.46664960285027m, 0.131695932304345m, 0.91746489373849m, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)null,
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-67659019),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(77896577), DateTime.Now.AddSeconds(54753312), DateTime.Now.AddSeconds(15013471), DateTime.Now.AddSeconds(23416296), DateTime.Now.AddSeconds(-149611638), DateTime.Now.AddSeconds(165998396), DateTime.Now.AddSeconds(211178133), DateTime.Now.AddSeconds(-187840798), DateTime.Now.AddSeconds(-103585410), DateTime.Now.AddSeconds(-62969299) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(310629732), null, DateTime.Now.AddSeconds(-48705077), null, null, null, DateTime.Now.AddSeconds(319541507), DateTime.Now.AddSeconds(83281488), DateTime.Now.AddSeconds(-111458481), DateTime.Now.AddSeconds(-5324433) },
            },
        };
    }
}
