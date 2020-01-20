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
                StringMember = (string)"AI6$g3c#q ",
                StringArrayMember = new string[] { "deYXls3!f9", "Bo 3jG7X26", "ivUQnIHHIy", "SFeqz$l7Fb", "Cw@AziICla", "@ER6n4KI7c", "lv83r#JgEm", "sARa490TnE", " Et53N,uC!", ".d 0?d,Og3" },
                CharMember = (char)'\'',
                CharArrayMember = new char[] { 'b', 'g', 'P', '6', 'r', 'W', '5', 'J', '0', 'j' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { 'b', '3', 'k', 'x', 'n', null, 'Y', 'E', 't', '3' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { true, true, true, true, false, true, false, false, true, true },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { null, null, null, false, true, false, false, false, true, true },
                ByteMember = (byte)234,
                ByteArrayMember = new byte[] { 210, 163, 243, 71, 95, 36, 127, 137, 145, 148 },
                NullableByteMember = (byte?)184,
                NullableByteArrayMember = new byte?[] { null, null, 45, 30, 90, 175, 167, 38, 73, 149 },
                SByteMember = (sbyte)-41,
                SByteArrayMember = new sbyte[] { -73, -111, -123, 16, 50, 84, 21, -124, -104, 108 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { -38, null, -102, null, -37, 49, 9, null, null, 117 },
                Int16Member = (short)-12423,
                Int16ArrayMember = new short[] { -4616, 29531, 21576, -11003, 18446, 31868, 15943, -9955, 26309, -5469 },
                NullableInt16Member = (short?)-23715,
                NullableInt16ArrayMember = new short?[] { -12088, null, -6109, null, null, -9278, 27045, -13900, 4477, null },
                UInt16Member = (ushort)12979,
                UInt16ArrayMember = new ushort[] { 11680, 39616, 29223, 40670, 57975, 50437, 52695, 26546, 51144, 17073 },
                NullableUInt16Member = (ushort?)43150,
                NullableUInt16ArrayMember = new ushort?[] { 44832, null, 7070, null, 18673, null, 47023, null, null, null },
                Int32Member = (int)-258637828,
                Int32ArrayMember = new int[] { 810376438, 1679191170, 1195235052, -1549113495, -944847136, -1285411429, -1693905096, 1196166492, -1443081791, 138315468 },
                NullableInt32Member = (int?)-1576421850,
                NullableInt32ArrayMember = new int?[] { null, 1132943512, -917123376, 1668478874, -175448568, -687772904, 1963894151, null, 1906268432, 1997982421 },
                UInt32Member = (uint)2853217874,
                UInt32ArrayMember = new uint[] { 1493747038, 1556213409, 288247677, 1016299111, 2563212961, 13182049, 2166922463, 812909969, 2307980975, 2505414721 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 3261836115, 2925902943, 961050847, null, 1086719097, 1660869495, 167381742, 3507515697, 3371900308, 1353209388 },
                Int64Member = (long)7412917309289649398,
                Int64ArrayMember = new long[] { 906006989125013105, 5622900573841933994, -26537918252191204, -6900853266646589937, -1053201014501840547, 7485847653861010222, -5080212687512846644, 7748276419391574700, -3166837558681170165, -200385288793780848 },
                NullableInt64Member = (long?)-1519922912922542841,
                NullableInt64ArrayMember = new long?[] { -6403056986637177032, null, -1404691861886270048, 743713382751147331, -7068059919594233206, 6629464660975678749, -6436423797719124828, 2153489477929522594, null, -692538858814959337 },
                UInt64Member = (ulong)988727417371534153,
                UInt64ArrayMember = new ulong[] { 7090159340138828661, 11410947366258545285, 5788179793148626226, 16402114514909875140, 18073702038582905434, 8459028847569706275, 13201147767072325704, 9233112417326633791, 9394651584734012018, 4784247295613844722 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 5014324908998246274, 15180643169143599018, 3936224276140328159, null, 7140682784052228134, 15183296336392663652, null, 7629769691667677734, 7574949289519870879, 10839433224222514161 },
                SingleMember = (float)0.0042F,
                SingleArrayMember = new float[] { 0.884F, 0.4758F, 0.6759F, 0.2536F, 0.4407F, 0.7747F, 0.7071F, 0.2313F, 0.602F, 0.0601F },
                NullableSingleMember = (float?)0.9288F,
                NullableSingleArrayMember = new float?[] { 0.3862F, null, 0.443F, 0.5044F, 0.2622F, 0.6476F, 0.7948F, 0.888F, 0.1413F, null },
                DoubleMember = (double)0.9555,
                DoubleArrayMember = new double[] { 0.0193, 0.2804, 0.034, 0.5752, 0.6965, 0.5494, 0.2035, 0.8276, 0.8579, 0.146 },
                NullableDoubleMember = (double?)0.0572,
                NullableDoubleArrayMember = new double?[] { null, null, null, null, 0.4509, null, 0.6289, null, null, null },
                DecimalMember = (decimal)0.280816619415216m,
                DecimalArrayMember = new decimal[] { 0.966926272011794m, 0.477604157513754m, 0.576707818813952m, 0.324577629251675m, 0.749231527442686m, 0.66972575274749m, 0.676837577334064m, 0.500664677238401m, 0.327480664163586m, 0.82036485328356m },
                NullableDecimalMember = (decimal?)0.446088868401055m,
                NullableDecimalArrayMember = new decimal?[] { 0.326663383900497m, 0.527087062377058m, 0.88295900583405m, 0.884631805533838m, 0.213352186704684m, 0.0859072362472803m, 0.863165471173434m, 0.620184243479829m, 0.945733226344796m, 0.502769163578176m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-32140545),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-309761246), DateTime.Now.AddSeconds(-250663768), DateTime.Now.AddSeconds(-316946496), DateTime.Now.AddSeconds(327080169), DateTime.Now.AddSeconds(96941860), DateTime.Now.AddSeconds(70837600), DateTime.Now.AddSeconds(289143665), DateTime.Now.AddSeconds(97921055), DateTime.Now.AddSeconds(125870305), DateTime.Now.AddSeconds(-330944739) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(53293167),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-103447975), DateTime.Now.AddSeconds(191988812), DateTime.Now.AddSeconds(82704589), DateTime.Now.AddSeconds(-115329183), DateTime.Now.AddSeconds(-7307040), DateTime.Now.AddSeconds(-172764567), null, DateTime.Now.AddSeconds(-281460771), DateTime.Now.AddSeconds(154345890), null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"Zz% 4!9:C1",
                StringArrayMember = new string[] { "E:r2dhCDAe", "IldJyOBokb", "$l972WFuA8", "a$sX;@9TWf", "a7OBv.V,Xp", "\"Vdx3pE6AG", "cfCOEK\"GT3", "c4RFANX8L'", "BQWh'Fq!qX", ":CTQC@5A$k" },
                CharMember = (char)'P',
                CharArrayMember = new char[] { 'T', '!', '8', 'O', '3', 'B', 'S', '4', 'I', 'r' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { 'N', '\'', 'B', null, null, 'W', null, null, null, 'j' },
                BooleanMember = (bool)false,
                BooleanArrayMember = new bool[] { false, true, true, false, false, true, false, false, true, true },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { true, true, false, null, null, true, true, false, null, true },
                ByteMember = (byte)123,
                ByteArrayMember = new byte[] { 58, 21, 60, 129, 47, 231, 251, 7, 125, 166 },
                NullableByteMember = (byte?)74,
                NullableByteArrayMember = new byte?[] { null, 130, 248, 104, null, 28, 155, 168, 41, 11 },
                SByteMember = (sbyte)36,
                SByteArrayMember = new sbyte[] { 40, -59, 28, 56, -109, -36, -58, 1, 102, 3 },
                NullableSByteMember = (sbyte?)null,
                NullableSByteArrayMember = new sbyte?[] { -70, -72, null, 93, 92, -30, -120, -3, null, null },
                Int16Member = (short)-32663,
                Int16ArrayMember = new short[] { 32721, -23057, 18483, 27023, 19098, -21659, -26760, -20207, 8229, -13434 },
                NullableInt16Member = (short?)-31646,
                NullableInt16ArrayMember = new short?[] { null, null, 8431, null, 26770, -5297, 17203, null, null, null },
                UInt16Member = (ushort)38646,
                UInt16ArrayMember = new ushort[] { 44739, 20714, 34499, 26572, 9027, 47985, 34066, 31074, 23416, 65345 },
                NullableUInt16Member = (ushort?)56904,
                NullableUInt16ArrayMember = new ushort?[] { 6286, 4279, 65108, null, null, 14000, 5715, 22452, null, 41356 },
                Int32Member = (int)-1355613197,
                Int32ArrayMember = new int[] { -1513320654, 2128026941, 743922093, -1377193897, 1407172394, -761362017, 338835953, 479362419, -895200967, 1473380283 },
                NullableInt32Member = (int?)null,
                NullableInt32ArrayMember = new int?[] { 1162346595, 1282007333, -1475355109, -352013062, 1002843915, -232912123, null, 633648264, 223259786, 6172682 },
                UInt32Member = (uint)2354492726,
                UInt32ArrayMember = new uint[] { 3478134329, 3504113382, 1899454573, 2611309234, 1748502189, 156988382, 547257341, 3022761267, 3938483607, 2307613408 },
                NullableUInt32Member = (uint?)3283953486,
                NullableUInt32ArrayMember = new uint?[] { 212975890, 2934865200, 1792165571, null, null, 3649254591, 1270594217, 1284535214, 3714622790, 922421798 },
                Int64Member = (long)-5873533283843167292,
                Int64ArrayMember = new long[] { 8740601968393792597, -2362777164972767161, -4791279742441147911, -9062189531428994236, 4368601506872924097, 740707134114149572, 7005587536953686517, -6496493100482661461, -7641625243087478388, -4645826418152896111 },
                NullableInt64Member = (long?)6910988946091115999,
                NullableInt64ArrayMember = new long?[] { -3252457328688072693, -2994535896764684204, 1364368503970494457, null, 8823202272522450698, null, -3722733660543849538, null, null, null },
                UInt64Member = (ulong)11560868245817408178,
                UInt64ArrayMember = new ulong[] { 7225280214622876688, 8298426541475480669, 9055531847070529917, 9166192341209351631, 10910662645399820540, 14167890216480929109, 6513573485814585104, 9297010361614254985, 618328784457355663, 6441933770986506703 },
                NullableUInt64Member = (ulong?)12979632530537224427,
                NullableUInt64ArrayMember = new ulong?[] { 10516108461653170914, 7014640441601456964, null, 341894009787037469, 8852928066115439368, null, null, 16656807461940975237, null, 13668737776808469023 },
                SingleMember = (float)0.8379F,
                SingleArrayMember = new float[] { 0.7166F, 0.9885F, 0.0035F, 0.4938F, 0.6626F, 0.2927F, 0.2109F, 0.8831F, 0.9503F, 0.7026F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { null, 0.0904F, null, 0.7742F, 0.3662F, null, 0.2046F, 0.742F, 0.8282F, null },
                DoubleMember = (double)0.0777,
                DoubleArrayMember = new double[] { 0.1484, 0.2271, 0.3923, 0.9941, 0.7665, 0.9013, 0.8038, 0.9657, 0.0612, 0.1511 },
                NullableDoubleMember = (double?)0.5239,
                NullableDoubleArrayMember = new double?[] { 0.5193, null, 0.9615, null, 0.8451, null, 0.3375, null, 0.5259, null },
                DecimalMember = (decimal)0.039132131281836m,
                DecimalArrayMember = new decimal[] { 0.738958620344735m, 0.502394606127588m, 0.459616193296209m, 0.467675999956055m, 0.84961245295108m, 0.707749818315613m, 0.66367648526266m, 0.740680984100644m, 0.781509966953429m, 0.131658810717826m },
                NullableDecimalMember = (decimal?)0.528227610759543m,
                NullableDecimalArrayMember = new decimal?[] { null, 0.454078452407419m, 0.14301421313687m, null, 0.267818994944831m, 0.0264923120972199m, 0.191729945219927m, null, 0.886066056269252m, null },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(221630015),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-220242785), DateTime.Now.AddSeconds(-179032258), DateTime.Now.AddSeconds(-128982254), DateTime.Now.AddSeconds(-25419408), DateTime.Now.AddSeconds(138972471), DateTime.Now.AddSeconds(-329949206), DateTime.Now.AddSeconds(-50186775), DateTime.Now.AddSeconds(323896524), DateTime.Now.AddSeconds(-263905496), DateTime.Now.AddSeconds(-300418552) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-98954973),
                NullableDateTimeArrayMember = new DateTime?[] { null, null, DateTime.Now.AddSeconds(86626108), DateTime.Now.AddSeconds(20790100), DateTime.Now.AddSeconds(-47108284), DateTime.Now.AddSeconds(230461750), null, null, null, null },
            },
            new DataConvertersTestClass {
                StringMember = (string)"\"PsqNLDXxq",
                StringArrayMember = new string[] { "STuIFLIrV%", "r%m@?akBCF", "eX.y%8v3U3", "CSU EueLL:", "9N1%\"!zw19", "yBVrM@f#JT", "mcXo#Y1m:R", "#Z?KSQs,5D", "J BiFA$z.J", "OhB'ch;MCr" },
                CharMember = (char)':',
                CharArrayMember = new char[] { 'z', 'm', 'M', 'M', '?', '$', 'U', 'Z', 'd', 'p' },
                NullableCharMember = (char?)'A',
                NullableCharArrayMember = new char?[] { 'S', 'o', 'N', '6', 'N', 'J', '3', null, '"', null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, true, false, false, true, true, false, true, false, false },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { false, null, false, false, false, true, false, true, null, true },
                ByteMember = (byte)53,
                ByteArrayMember = new byte[] { 142, 250, 25, 16, 162, 93, 235, 254, 69, 9 },
                NullableByteMember = (byte?)185,
                NullableByteArrayMember = new byte?[] { 67, null, null, 155, null, 68, null, 80, 53, null },
                SByteMember = (sbyte)6,
                SByteArrayMember = new sbyte[] { -9, 117, -10, 94, 111, -37, 81, -48, 102, -109 },
                NullableSByteMember = (sbyte?)-95,
                NullableSByteArrayMember = new sbyte?[] { null, 86, null, -15, null, null, 56, 0, 16, -45 },
                Int16Member = (short)16473,
                Int16ArrayMember = new short[] { 16462, 11709, -18660, 22361, 1776, -866, -1170, 18620, 22898, 3695 },
                NullableInt16Member = (short?)null,
                NullableInt16ArrayMember = new short?[] { 19964, -25172, 10977, 10922, -3531, -11374, null, null, null, -16287 },
                UInt16Member = (ushort)19937,
                UInt16ArrayMember = new ushort[] { 62269, 513, 15094, 53371, 49430, 15018, 22037, 53223, 15474, 41243 },
                NullableUInt16Member = (ushort?)6467,
                NullableUInt16ArrayMember = new ushort?[] { null, 42797, 21466, null, null, 53866, 64594, null, 57448, 43165 },
                Int32Member = (int)-637845158,
                Int32ArrayMember = new int[] { 1808550849, 959697115, 451170828, 1159886786, -2092226157, -1240961197, -33517346, -1013433752, -550484721, -187098079 },
                NullableInt32Member = (int?)1767538020,
                NullableInt32ArrayMember = new int?[] { 403608432, null, null, 1129416312, 902777741, 186759634, 1982088006, -219246250, null, -261283861 },
                UInt32Member = (uint)670748329,
                UInt32ArrayMember = new uint[] { 2720108697, 2288496953, 400391744, 3042464247, 2032040911, 19660836, 3044428810, 1714441273, 3122762415, 3728345945 },
                NullableUInt32Member = (uint?)2334524694,
                NullableUInt32ArrayMember = new uint?[] { 1186124920, null, 2550366384, 1005361183, null, null, 1684502350, null, null, null },
                Int64Member = (long)-8424791347406439127,
                Int64ArrayMember = new long[] { 3929329577498297401, -578347741816129995, -1719284640617164019, 3445328553583921725, -2200778599677694372, -2497235263120344083, 6471837476735489540, -1141999067171434591, -3931113202172610650, 3685860663702476829 },
                NullableInt64Member = (long?)2217540660816950531,
                NullableInt64ArrayMember = new long?[] { 3518473472665204598, 3970104643408322954, null, 1721272496051952400, 4137342128859523726, -3343044703519875637, 991844443954123395, null, -8889639436751429618, null },
                UInt64Member = (ulong)6789045260081116807,
                UInt64ArrayMember = new ulong[] { 17180849434940445600, 15456050707645712152, 14885619721354902206, 9846506150364691498, 8951978274292876125, 11133485932650017332, 5195552669660698808, 5170891532593220094, 17079877047359970594, 1393800242443610943 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { 17629638607773618947, null, null, 15662019920198175087, null, 6451577122288325070, null, 3797497583700447800, 3630411921919097294, null },
                SingleMember = (float)0.4588F,
                SingleArrayMember = new float[] { 0.6266F, 0.8867F, 0.748F, 0.09F, 0.939F, 0.2223F, 0.5768F, 0.9403F, 0.84F, 0.8924F },
                NullableSingleMember = (float?)0.3946F,
                NullableSingleArrayMember = new float?[] { 0.7906F, null, null, 0.0652F, 0.1679F, 0.0666F, 0.9477F, 0.069F, 0.512F, 0.2615F },
                DoubleMember = (double)0.3743,
                DoubleArrayMember = new double[] { 0.1168, 0.3266, 0.512, 0.6974, 0.528, 0.4976, 0.548, 0.3222, 0.8111, 0.8335 },
                NullableDoubleMember = (double?)0.2447,
                NullableDoubleArrayMember = new double?[] { 0.0915, null, null, 0.0068, 0.9317, 0.3127, 0.0839, 0.6609, 0.2435, 0.9298 },
                DecimalMember = (decimal)0.0566533338542345m,
                DecimalArrayMember = new decimal[] { 0.799071838985696m, 0.38174743781879m, 0.0993954875037984m, 0.825214074377536m, 0.406956737119219m, 0.246525182969181m, 0.824268777772909m, 0.235198198461532m, 0.0594110624210029m, 0.307854269308902m },
                NullableDecimalMember = (decimal?)0.301048271963861m,
                NullableDecimalArrayMember = new decimal?[] { 0.472814340364567m, 0.273219763894202m, null, 0.0150189688499174m, 0.290582821374099m, null, null, null, 0.95291257507769m, 0.748820520820478m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { null, Guid.NewGuid(), null, null, null, Guid.NewGuid(), null, null, Guid.NewGuid(), null },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(205818071),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-20515423), DateTime.Now.AddSeconds(36028194), DateTime.Now.AddSeconds(2298370), DateTime.Now.AddSeconds(56953859), DateTime.Now.AddSeconds(293575165), DateTime.Now.AddSeconds(-265134537), DateTime.Now.AddSeconds(-202574755), DateTime.Now.AddSeconds(148227732), DateTime.Now.AddSeconds(-117674575), DateTime.Now.AddSeconds(170307491) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-262921287),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-23581592), null, null, DateTime.Now.AddSeconds(252928032), DateTime.Now.AddSeconds(-40617983), DateTime.Now.AddSeconds(170838326), null, DateTime.Now.AddSeconds(211399704), null, DateTime.Now.AddSeconds(324572325) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"QM5j?YLKZ@",
                StringArrayMember = new string[] { "ZqNFJna,i#", "bCz5ClX$58", "af9O.#Ros4", ",9Z:6vU#5?", "GB0GPOk hb", "BObos\"4 SN", "V8T1Hoz7%e", "RbO\"1Y23%v", "jRHR3wh8W?", ";U0sqV5 %y" },
                CharMember = (char)'h',
                CharArrayMember = new char[] { 'O', 'T', 'K', 'p', 'R', '#', 'f', 'k', 'R', '"' },
                NullableCharMember = (char?)'m',
                NullableCharArrayMember = new char?[] { null, 'b', null, 'Y', 'S', '9', '3', null, null, '!' },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, true, false, false, true, true, false, false, true, false },
                NullableBooleanMember = (bool?)null,
                NullableBooleanArrayMember = new bool?[] { null, false, true, null, null, null, null, false, true, true },
                ByteMember = (byte)93,
                ByteArrayMember = new byte[] { 133, 155, 210, 205, 113, 113, 124, 78, 59, 176 },
                NullableByteMember = (byte?)83,
                NullableByteArrayMember = new byte?[] { 4, 143, 98, 88, 3, null, null, 235, 251, 199 },
                SByteMember = (sbyte)19,
                SByteArrayMember = new sbyte[] { 116, 21, 54, 101, 121, -18, 13, 121, -59, -55 },
                NullableSByteMember = (sbyte?)-26,
                NullableSByteArrayMember = new sbyte?[] { 107, 60, 34, -101, null, null, -77, -4, -71, 68 },
                Int16Member = (short)32083,
                Int16ArrayMember = new short[] { 30584, -31749, -20141, -31157, 17302, 1392, 6512, 30172, 11461, -11789 },
                NullableInt16Member = (short?)-13883,
                NullableInt16ArrayMember = new short?[] { -31265, 12558, -29286, null, 1573, -31065, null, -24933, -29394, 9294 },
                UInt16Member = (ushort)48348,
                UInt16ArrayMember = new ushort[] { 28509, 3636, 34384, 39743, 55802, 55545, 37577, 62858, 49090, 53300 },
                NullableUInt16Member = (ushort?)32388,
                NullableUInt16ArrayMember = new ushort?[] { 45941, 53561, 24700, 19057, 17830, 10991, null, 22927, 10943, 24298 },
                Int32Member = (int)-865612464,
                Int32ArrayMember = new int[] { -1663123818, 613322507, -1599368259, 1290987336, -520686390, 1603790752, 732023188, 75514978, -1099672495, -417814681 },
                NullableInt32Member = (int?)1180423217,
                NullableInt32ArrayMember = new int?[] { 539755268, null, -1372813354, -314439809, -1508738878, null, 602157939, null, null, null },
                UInt32Member = (uint)2487173315,
                UInt32ArrayMember = new uint[] { 1980103181, 44160068, 3036800499, 1451855662, 2739761978, 3625109554, 2769509651, 3986942484, 2650139505, 408201310 },
                NullableUInt32Member = (uint?)1276009391,
                NullableUInt32ArrayMember = new uint?[] { 1185338425, null, 1164090355, null, null, 4009476326, null, 2571322388, null, null },
                Int64Member = (long)3344810491866627457,
                Int64ArrayMember = new long[] { 6897657063091873424, -3192738560691873915, -1422739155517953081, 2515782448270343834, 4215631939575647960, 7163993758047123303, 2803702798761451787, -6083801149262388764, -2816215507876961811, -319912908693498197 },
                NullableInt64Member = (long?)null,
                NullableInt64ArrayMember = new long?[] { 911161212864621742, -1956771813665876793, -4497942677601793903, null, null, null, 4882645614543445906, -956728898410840588, null, null },
                UInt64Member = (ulong)9382308840231213260,
                UInt64ArrayMember = new ulong[] { 10618532585098344403, 2736418371819723852, 15215024104574893482, 3847480306152826472, 2210630259657108347, 1630128483061198583, 3797582835591731231, 5665311792839647056, 17123737052061824376, 548369896132711311 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { null, null, 11739586494551978861, null, null, 17728149734516674492, 2067384459512095285, null, null, null },
                SingleMember = (float)0.2961F,
                SingleArrayMember = new float[] { 0.8345F, 0.8751F, 0.752F, 0.2565F, 0.0636F, 0.9546F, 0.3291F, 0.6821F, 0.7109F, 0.5135F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { 0.8818F, 0.9716F, null, null, null, 0.899F, 0.1137F, 0.1413F, null, 0.8004F },
                DoubleMember = (double)0.8538,
                DoubleArrayMember = new double[] { 0.2407, 0.3439, 0.4295, 0.8489, 0.7347, 0.6391, 0.6006, 0.9959, 0.7507, 0.9713 },
                NullableDoubleMember = (double?)0.9372,
                NullableDoubleArrayMember = new double?[] { 0.3321, 0.539, 0.3079, 0.3551, 0.5892, null, 0.8041, null, null, null },
                DecimalMember = (decimal)0.267886248076282m,
                DecimalArrayMember = new decimal[] { 0.976875417389383m, 0.699951651366405m, 0.821683762046361m, 0.199326036590769m, 0.68402765024641m, 0.982438127967733m, 0.749911372899036m, 0.913258960895826m, 0.703013536382007m, 0.967422096509217m },
                NullableDecimalMember = (decimal?)null,
                NullableDecimalArrayMember = new decimal?[] { null, 0.0836031507158666m, 0.884894891588434m, 0.942268734770952m, null, 0.918510181325725m, 0.34912280754611m, 0.713053597003712m, 0.240493213869861m, 0.438530200830908m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, null, null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-100704536),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(95903588), DateTime.Now.AddSeconds(229711181), DateTime.Now.AddSeconds(277034798), DateTime.Now.AddSeconds(-116250321), DateTime.Now.AddSeconds(-185767278), DateTime.Now.AddSeconds(320328647), DateTime.Now.AddSeconds(-62131671), DateTime.Now.AddSeconds(83796133), DateTime.Now.AddSeconds(-7372412), DateTime.Now.AddSeconds(-219950308) },
                NullableDateTimeMember = (DateTime?)DateTime.Now.AddSeconds(-262693824),
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(-55568161), DateTime.Now.AddSeconds(-311650990), null, null, DateTime.Now.AddSeconds(-211750374), DateTime.Now.AddSeconds(-110025216), null, null, DateTime.Now.AddSeconds(340015971), DateTime.Now.AddSeconds(-288259363) },
            },
            new DataConvertersTestClass {
                StringMember = (string)"m0ACJw i.U",
                StringArrayMember = new string[] { "OABu1E1B%h", "G;lJn#xsZg", "p35XJ6QFHG", "x %Bx;TG:m", "q4AnrvO:lM", "%dId2iS0k@", ".w!'7H:jjx", "0r1.?82R3k", "rZq!HT;rWO", "ups#y!9I2j" },
                CharMember = (char)'J',
                CharArrayMember = new char[] { 'Z', 'c', 'f', 'A', 'I', '!', '3', '\'', '0', '2' },
                NullableCharMember = (char?)null,
                NullableCharArrayMember = new char?[] { '0', ';', 'x', 'Q', 'T', 'q', 'J', 'c', null, null },
                BooleanMember = (bool)true,
                BooleanArrayMember = new bool[] { true, true, false, false, false, true, false, true, true, true },
                NullableBooleanMember = (bool?)true,
                NullableBooleanArrayMember = new bool?[] { null, true, true, null, true, true, false, true, null, false },
                ByteMember = (byte)241,
                ByteArrayMember = new byte[] { 109, 188, 141, 121, 169, 54, 72, 71, 64, 198 },
                NullableByteMember = (byte?)104,
                NullableByteArrayMember = new byte?[] { 188, 18, 110, null, 186, 71, null, null, 69, 133 },
                SByteMember = (sbyte)-33,
                SByteArrayMember = new sbyte[] { -108, -2, 59, 112, -3, -57, -70, -10, 68, -56 },
                NullableSByteMember = (sbyte?)111,
                NullableSByteArrayMember = new sbyte?[] { 11, 90, 112, -35, -105, -80, null, null, null, -3 },
                Int16Member = (short)-9454,
                Int16ArrayMember = new short[] { -13259, 12418, -20814, -23727, -21811, 21564, -22400, 12771, -3007, -28715 },
                NullableInt16Member = (short?)-1115,
                NullableInt16ArrayMember = new short?[] { null, null, 31514, null, -5704, 25915, -687, null, 1173, -23731 },
                UInt16Member = (ushort)29918,
                UInt16ArrayMember = new ushort[] { 4364, 61913, 39096, 10396, 51718, 24023, 35481, 18519, 20233, 3196 },
                NullableUInt16Member = (ushort?)56485,
                NullableUInt16ArrayMember = new ushort?[] { null, null, null, 13300, 50406, 16875, null, null, null, 5549 },
                Int32Member = (int)423367534,
                Int32ArrayMember = new int[] { -23149434, 774119233, -1748686902, -86597656, 1213951582, -1877950561, -2147402759, -1682085690, 2105566698, 1484874831 },
                NullableInt32Member = (int?)-1379105672,
                NullableInt32ArrayMember = new int?[] { 352617468, 2046080768, 84217267, null, null, -1389794719, -89434081, null, 1144727386, 2071871325 },
                UInt32Member = (uint)154476433,
                UInt32ArrayMember = new uint[] { 2332257410, 4265550929, 4183017933, 3048245461, 1466778928, 3444752797, 1005944119, 814698170, 3638530494, 3239158244 },
                NullableUInt32Member = (uint?)null,
                NullableUInt32ArrayMember = new uint?[] { 451755808, 3342264762, null, null, null, 1412831681, 2135163695, 2822978339, null, null },
                Int64Member = (long)-762108023256413425,
                Int64ArrayMember = new long[] { 3859246555807225296, 7250819048525133493, -3541213774895920227, -5246494284098505825, 1274897249455943652, -439726616884548296, -4890642185800912918, -2631563624072391, 4907078771467633350, -2822039778721953511 },
                NullableInt64Member = (long?)-8074427251437863625,
                NullableInt64ArrayMember = new long?[] { null, null, -5333299938292573860, -5670623988519742446, -6437503922756837129, -1690680623305366891, -413732904859816173, null, -3225044348306759047, null },
                UInt64Member = (ulong)16110561165880539544,
                UInt64ArrayMember = new ulong[] { 10504087700166987963, 16689917181875108278, 6831876953899471307, 825826469815419312, 6865006682485500021, 1851548896765559152, 647122911250193306, 14539100147986112769, 7183817837906382404, 12282841999460662476 },
                NullableUInt64Member = (ulong?)null,
                NullableUInt64ArrayMember = new ulong?[] { null, null, 10560997392053666263, 9122638288294695338, 6408867627034774997, null, null, null, 1954213635468964044, null },
                SingleMember = (float)0.3149F,
                SingleArrayMember = new float[] { 0.8701F, 0.5351F, 0.0648F, 0.2714F, 0.5786F, 0.5963F, 0.9199F, 0.5196F, 0.4801F, 0.5341F },
                NullableSingleMember = (float?)null,
                NullableSingleArrayMember = new float?[] { null, null, null, 0.6544F, 0.159F, 0.6906F, 0.7071F, 0.8616F, 0.3625F, 0.7207F },
                DoubleMember = (double)0.8671,
                DoubleArrayMember = new double[] { 0.8249, 0.3045, 0.4326, 0.8813, 0.9797, 0.8507, 0.9008, 0.8766, 0.2426, 0.8624 },
                NullableDoubleMember = (double?)0.5068,
                NullableDoubleArrayMember = new double?[] { 0.186, 0.0732, null, null, 0.1766, 0.3592, 0.0177, null, 0.6514, 0.1882 },
                DecimalMember = (decimal)0.590263905744191m,
                DecimalArrayMember = new decimal[] { 0.454044162041528m, 0.534058874721666m, 0.0754482569524312m, 0.96731195038525m, 0.0825356450316196m, 0.607902617942496m, 0.353525425472076m, 0.101722235373092m, 0.459923171186784m, 0.490376212396834m },
                NullableDecimalMember = (decimal?)0.359960802998376m,
                NullableDecimalArrayMember = new decimal?[] { 0.833115897529347m, null, 0.196179597729901m, null, 0.775970395084457m, 0.0106438403067383m, 0.076939811500227m, null, 0.350828843820295m, 0.486272439587057m },
                GuidMember = (Guid)Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = (Guid?)Guid.NewGuid(),
                NullableGuidArrayMember = new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = (DateTime)DateTime.Now.AddSeconds(-105920416),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(195554155), DateTime.Now.AddSeconds(-22103789), DateTime.Now.AddSeconds(346752337), DateTime.Now.AddSeconds(-177147081), DateTime.Now.AddSeconds(-101067733), DateTime.Now.AddSeconds(136421836), DateTime.Now.AddSeconds(290596309), DateTime.Now.AddSeconds(344936634), DateTime.Now.AddSeconds(203329034), DateTime.Now.AddSeconds(7705295) },
                NullableDateTimeMember = (DateTime?)null,
                NullableDateTimeArrayMember = new DateTime?[] { DateTime.Now.AddSeconds(309543888), DateTime.Now.AddSeconds(122878340), DateTime.Now.AddSeconds(-127924160), DateTime.Now.AddSeconds(-123593514), null, DateTime.Now.AddSeconds(-72311682), DateTime.Now.AddSeconds(-238360182), DateTime.Now.AddSeconds(-287826265), DateTime.Now.AddSeconds(-208667351), null },
            },
        };
    }
}
