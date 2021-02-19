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
                StringMember = "bbKcnFu;%$",
                StringArrayMember = new string[] { "3qgws7\"HGw", "Sr#S:QiyY0", ",vPrO!XFJO", ",wWvX2JYbY", "zx4VI7f:lW", "t#pUJHb,hx", "j.%:7tVY!M", "HKW BO;!je", "mfr9s'HBmP", "aV:0?,z;kl" },
                CharMember = 'M',
                CharArrayMember = new char[] { 'g', 'B', 'R', 'c', 'O', '1', 'f', '.', '6', '0' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, null, null, 'x', null, null, null, null, 'Z', 's' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, false, false, false, false, false, false, true, true, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, null, true, false, false, true, null, false, true },
                ByteMember = 83,
                ByteArrayMember = new byte[] { 215, 158, 175, 26, 71, 138, 191, 95, 27, 2 },
                NullableByteMember = 187,
                NullableByteArrayMember = new Nullable<byte>[] { null, 46, 182, 1, 206, 85, 104, 34, null, 164 },
                SByteMember = -19,
                SByteArrayMember = new sbyte[] { 80, -13, 86, 111, -79, -13, -73, -46, 90, 122 },
                NullableSByteMember = 98,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -25, 47, 17, null, null, null, 92, -119, -36, 49 },
                Int16Member = 8372,
                Int16ArrayMember = new short[] { 9992, 12472, 27284, 82, -25393, 4451, -16947, -18194, -15515, 24507 },
                NullableInt16Member = 2023,
                NullableInt16ArrayMember = new Nullable<short>[] { 26409, null, -29447, -14281, null, null, null, 7093, -16747, -13959 },
                UInt16Member = 16650,
                UInt16ArrayMember = new ushort[] { 29908, 59052, 39396, 38139, 38474, 34809, 53405, 42126, 46975, 36423 },
                NullableUInt16Member = 41055,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 30667, 58511, null, 48839, null, null, null, null, 60795, null },
                Int32Member = -302526511,
                Int32ArrayMember = new int[] { 1702195431, -1943922722, -2034930913, -1862126746, 705427281, -527942905, -313018162, 1334200873, -1466205910, 1193274577 },
                NullableInt32Member = -1228480475,
                NullableInt32ArrayMember = new Nullable<int>[] { -642389062, null, null, null, null, -1751848788, -714984256, -1746117531, null, -20654236 },
                UInt32Member = 1787625972,
                UInt32ArrayMember = new uint[] { 3039105593, 676981611, 151370955, 2634035380, 862536301, 2399127777, 1754034834, 125246835, 3366690419, 2631758896 },
                NullableUInt32Member = 1137882495,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 38095254, 2304350386, null, 1610163095, 4249136821, 1815643158, null, null, 739467727, 591584279 },
                Int64Member = -8729699300044038908,
                Int64ArrayMember = new long[] { 3000062661579200161, -5474650084146982574, 4434855283997038189, -6218702991244852181, 6676410223752446386, -2216278387808633202, -2655838483693555177, -7523888610319000885, 1079268058552218014, -454837741495350097 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -5630306918085467709, null, 6545334346280047114, -3067875398927627138, 3748364114195035232, 241910029539377234, null, 164438927196355316, null, 3760876895550608515 },
                UInt64Member = 1840682409077800292,
                UInt64ArrayMember = new ulong[] { 3150493286061853965, 3900364721833411041, 16939456339255633073, 8307403466422114767, 16746496598478334332, 9426408997104453313, 310728414524812893, 1061594817270516792, 3479073912588789048, 8782565819836836541 },
                NullableUInt64Member = 6525605395451465837,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 16594136305130201280, null, null, 1237648976857348205, null, null, 12653279569203319851, null, 11862182889936893961, 6528870467698951469 },
                SingleMember = 0.1676F,
                SingleArrayMember = new float[] { 0.8408F, 0.2344F, 0.2287F, 0.2949F, 0.4545F, 0.1037F, 0.9922F, 0.6916F, 0.5143F, 0.469F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.9194F, null, 0.1021F, 0.4227F, 0.7154F, null, 0.2081F, 0.4765F, 0.4758F, 0.7486F },
                DoubleMember = 0.008,
                DoubleArrayMember = new double[] { 0.5902, 0.516, 0.6172, 0.4099, 0.3059, 0.3293, 0.4857, 0.1569, 0.0488, 0.59 },
                NullableDoubleMember = 0.8851,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.8773, null, 0.0975, 0.6302, 0.3765, 0.2512, 0.9175, 0.9704, null, null },
                DecimalMember = 0.605865701383848m,
                DecimalArrayMember = new decimal[] { 0.0445682099296563m, 0.91318250117506m, 0.418317836438454m, 0.759284837990666m, 0.627133746923476m, 0.831938930243225m, 0.326517784188743m, 0.563517825940399m, 0.42205828773885m, 0.669425924154662m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, null, 0.612652772391519m, null, 0.867396588841172m, 0.0284411967864452m, 0.995924091896007m, null, null, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, null, null },
                DateTimeMember = DateTime.Now.AddSeconds(100637597),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(94905144), DateTime.Now.AddSeconds(-307812466), DateTime.Now.AddSeconds(-318036157), DateTime.Now.AddSeconds(-110250553), DateTime.Now.AddSeconds(-317584107), DateTime.Now.AddSeconds(-151320772), DateTime.Now.AddSeconds(197315065), DateTime.Now.AddSeconds(-258944258), DateTime.Now.AddSeconds(-189202399), DateTime.Now.AddSeconds(-56554548) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-328917187),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(121140098), null, DateTime.Now.AddSeconds(-309071212), DateTime.Now.AddSeconds(94163704), DateTime.Now.AddSeconds(-326743008), null, null, null, null, null },
            },
            new DataConvertersTestClass {
                StringMember = "mG$ZPjwpT!",
                StringArrayMember = new string[] { "RhbCLDTLW'", "lT9189TsKM", "mAg%'fiStI", "Sp4r%'\",p8", "snqsASTTnd", "QEeGfQAdK2", "sApNw#lS2'", "'eCYr%h,$w", "LO1Q'OKyL5", ":n1.Pr$c@D" },
                CharMember = 'm',
                CharArrayMember = new char[] { 'S', '!', 'q', ',', '7', 'i', 'o', 'y', 'r', 'v' },
                NullableCharMember = 'K',
                NullableCharArrayMember = new Nullable<char>[] { 'W', 's', null, null, 'G', '#', 'z', null, 'v', 'C' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { false, true, false, false, false, false, false, false, false, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, null, null, null, null, false, false, true, null },
                ByteMember = 115,
                ByteArrayMember = new byte[] { 203, 56, 126, 66, 17, 52, 55, 180, 153, 11 },
                NullableByteMember = 157,
                NullableByteArrayMember = new Nullable<byte>[] { 80, null, 54, 113, 222, 209, null, 72, null, 0 },
                SByteMember = 20,
                SByteArrayMember = new sbyte[] { 86, -9, 110, -67, 26, 63, -60, 86, -70, 118 },
                NullableSByteMember = 123,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, null, -128, -71, 45, null, null, 50, 98, null },
                Int16Member = -161,
                Int16ArrayMember = new short[] { -16319, -32510, -19557, -9899, 8225, 2035, -8403, 1885, 23874, 18476 },
                NullableInt16Member = -14175,
                NullableInt16ArrayMember = new Nullable<short>[] { null, null, null, -30519, -1139, 18791, null, null, -31581, -1358 },
                UInt16Member = 457,
                UInt16ArrayMember = new ushort[] { 28157, 9809, 2442, 7806, 52612, 37244, 54132, 7026, 45933, 45164 },
                NullableUInt16Member = 12640,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 65349, 40477, 17753, 41566, 20857, 26245, null, 16890, 40028, 3781 },
                Int32Member = 717380322,
                Int32ArrayMember = new int[] { 929824272, -1968411824, -1058040739, -2030480067, -536099850, 1405968720, 24844636, 1359138559, 679897637, 1779563860 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { -1733863425, 138337806, null, 816831201, -1410386213, -1684106610, -114175537, 1651878049, 1821885356, 48416193 },
                UInt32Member = 1875201649,
                UInt32ArrayMember = new uint[] { 3040482329, 1021530265, 2382875250, 3296715846, 1289131276, 1562872523, 487345382, 1598344732, 24940158, 2815656778 },
                NullableUInt32Member = 194469264,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, null, null, 2027624061, 3723333539, 533021773, null, 1756893096, null, 3392062088 },
                Int64Member = -2773598668571130449,
                Int64ArrayMember = new long[] { 1196429026498550647, -2737020804287478047, -7978970903931516414, -7618628176778680771, -1058998366605837121, -8565634584712432391, 8547204506402649920, -5906819191451297962, 6071230983228686592, 9067979802388204220 },
                NullableInt64Member = -2379484331848395062,
                NullableInt64ArrayMember = new Nullable<long>[] { -4959248564894514858, null, 3165584372143261710, 3275154417110147222, null, -4935943942941793947, 9038477722818022998, 7370133319851727790, -1116690361999621696, -3113484096085569961 },
                UInt64Member = 13076379426309899333,
                UInt64ArrayMember = new ulong[] { 15725246982221838941, 10237721536115008647, 4177090231742435948, 1601629846987087647, 16188232841939694318, 10654616998774629066, 6648781854659960688, 7349048827252139202, 4939847984572426538, 5873893711266964074 },
                NullableUInt64Member = 7833791091424737985,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 7414255296782703731, 5894457179134488139, 3922085384320104896, 7695913059716125951, null, null, null, 13705407186288420797, null, 13543623375682481219 },
                SingleMember = 0.1443F,
                SingleArrayMember = new float[] { 0.5936F, 0.1779F, 0.0531F, 0.6391F, 0.3337F, 0.6777F, 0.4623F, 0.2225F, 0.2062F, 0.1706F },
                NullableSingleMember = 0.8657F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.889F, 0.2149F, 0.2036F, null, 0.6138F, 0.0362F, 0.6704F, 0.0263F, null, 0.2079F },
                DoubleMember = 0.9907,
                DoubleArrayMember = new double[] { 0.8932, 0.2166, 0.8538, 0.721, 0.0016, 0.9522, 0.9434, 0.5072, 0.6529, 0.8117 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.4276, null, 0.6344, 0.3817, 0.6051, 0.6984, null, 0.6315, null, 0.3297 },
                DecimalMember = 0.879537385366642m,
                DecimalArrayMember = new decimal[] { 0.264523793600744m, 0.923158793208729m, 0.126302649325832m, 0.620292797973516m, 0.0881824233048514m, 0.0438972637261717m, 0.819688603198011m, 0.603144239915136m, 0.19984476463862m, 0.208735790666535m },
                NullableDecimalMember = 0.594999840760138m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, null, 0.515720571631436m, 0.684250001182896m, 0.992275858760009m, null, null, 0.810622832184016m, 0.789143534744691m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-39982178),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-39837828), DateTime.Now.AddSeconds(29687008), DateTime.Now.AddSeconds(125560849), DateTime.Now.AddSeconds(-72654843), DateTime.Now.AddSeconds(15144871), DateTime.Now.AddSeconds(-286422522), DateTime.Now.AddSeconds(161761714), DateTime.Now.AddSeconds(264175389), DateTime.Now.AddSeconds(163844573), DateTime.Now.AddSeconds(-127708935) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-12357243), DateTime.Now.AddSeconds(267327878), DateTime.Now.AddSeconds(70938543), DateTime.Now.AddSeconds(87080025), DateTime.Now.AddSeconds(-76219489), DateTime.Now.AddSeconds(-280320059), DateTime.Now.AddSeconds(240147790), DateTime.Now.AddSeconds(184532238), null, DateTime.Now.AddSeconds(15451297) },
            },
            new DataConvertersTestClass {
                StringMember = "3Bnuk9BFlh",
                StringArrayMember = new string[] { "6MfkqdVYVi", "9.G$Ac0PJ ", "AE\"!\"KyH2G", "NEM1 2IRU9", "WwGjR\"!qtI", "I8#mg1Tkqf", "2OsPV4xc%V", "Uj!W0b%g0A", "PHku$bHxUU", "iux,GHD:Oj" },
                CharMember = 'K',
                CharArrayMember = new char[] { 'G', '#', 'v', '?', '@', '6', '?', ' ', '%', 'R' },
                NullableCharMember = '7',
                NullableCharArrayMember = new Nullable<char>[] { '?', null, null, 'Q', null, 't', 'C', null, 'W', ';' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, false, true, false, false, false, false, false, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, null, null, false, true, null, true, true, true, null },
                ByteMember = 104,
                ByteArrayMember = new byte[] { 53, 129, 117, 141, 13, 130, 242, 146, 29, 163 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 102, 180, 63, 218, null, 137, null, null, null, null },
                SByteMember = 54,
                SByteArrayMember = new sbyte[] { 101, -62, -106, -88, 31, -59, -93, -77, 114, 11 },
                NullableSByteMember = 18,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, -41, null, -19, null, null, -119, -93, -124, -43 },
                Int16Member = -28922,
                Int16ArrayMember = new short[] { -4435, 14087, 22199, -25316, -16874, -1889, -11826, -8460, 20875, -27949 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -26939, null, -17865, -28971, null, null, null, null, null, null },
                UInt16Member = 56378,
                UInt16ArrayMember = new ushort[] { 35865, 15261, 57480, 60043, 32608, 29511, 43575, 12310, 41381, 22299 },
                NullableUInt16Member = 48713,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 34615, 33186, 11737, 52584, null, 61841, null, null, 13829, 2694 },
                Int32Member = -136933565,
                Int32ArrayMember = new int[] { 2061181244, -982495347, 1051720713, 1846414349, 1298479533, -1625855046, -926316413, 1129880495, -671819272, 622471125 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { 770164386, null, 156529847, 181419400, null, 380077573, 877030564, null, null, 1547705990 },
                UInt32Member = 1368337723,
                UInt32ArrayMember = new uint[] { 3252242247, 3865622431, 351136262, 1669706860, 314142908, 3394015075, 411960029, 1651027273, 92769373, 3951035498 },
                NullableUInt32Member = 2077034945,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3409761294, 4001199050, 2648903675, null, 2340509853, 4169149664, 2717240816, 4084420222, 2644475356, null },
                Int64Member = -3949048972994757518,
                Int64ArrayMember = new long[] { -4867619972589610370, 6422561969344790289, 6460070259978602409, -2819495948440870686, -7301963043627763786, -1387513352924348068, -5252177853228602213, -1342945473340253580, 8218370016018686687, 5937346238970166365 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, 3013571374309834930, null, 797724169729369245, -5606255327157741584, -1321253868272454205, -4607012711048261350, -6160845870145567269, 7353719580600782867, -631354714284805665 },
                UInt64Member = 1864229547200841662,
                UInt64ArrayMember = new ulong[] { 134636707046010823, 8591646152679952642, 3435174451028775725, 13601530899123025269, 17243436944178487257, 1590946349756725838, 10818314810449869132, 12301567642551125250, 3453617753711024718, 5253741696259203591 },
                NullableUInt64Member = 18264836682988625828,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 5369235606256387201, null, 16035636344898817087, null, 12057248944378083162, 16530520738767363613, null, null, null, null },
                SingleMember = 0.6278F,
                SingleArrayMember = new float[] { 0.8536F, 0.943F, 0.37F, 0.8754F, 0.4013F, 0.0963F, 0.2803F, 0.1419F, 0.0933F, 0.6397F },
                NullableSingleMember = 0.2165F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.6674F, null, 0.319F, null, 0.9434F, 0.7602F, null, null, null, 0.9003F },
                DoubleMember = 0.4924,
                DoubleArrayMember = new double[] { 0.0079, 0.3589, 0.9102, 0.7526, 0.0581, 0.6968, 0.5526, 0.0313, 0.9731, 0.8756 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.5903, null, 0.589, 0.5098, null, 0.2831, null, null, 0.7991, 0.1948 },
                DecimalMember = 0.576917119127194m,
                DecimalArrayMember = new decimal[] { 0.711494621220741m, 0.0486441180336495m, 0.537374085065617m, 0.0316932490243079m, 0.339619300486343m, 0.747596361556834m, 0.567415136642482m, 0.403358990980014m, 0.437215176614567m, 0.801765489765334m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, 0.985046569716673m, null, null, 0.699717170884701m, null, null, 0.378650508531672m, 0.691558414926547m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(63618564),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(349830723), DateTime.Now.AddSeconds(-80804203), DateTime.Now.AddSeconds(238661406), DateTime.Now.AddSeconds(-112847108), DateTime.Now.AddSeconds(61368733), DateTime.Now.AddSeconds(35236752), DateTime.Now.AddSeconds(-333899706), DateTime.Now.AddSeconds(160671803), DateTime.Now.AddSeconds(307505335), DateTime.Now.AddSeconds(-177949612) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(220478959),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(61214454), DateTime.Now.AddSeconds(-14357884), DateTime.Now.AddSeconds(-48812954), DateTime.Now.AddSeconds(-170045112), DateTime.Now.AddSeconds(323765738), DateTime.Now.AddSeconds(-129626734), null, DateTime.Now.AddSeconds(37197366), DateTime.Now.AddSeconds(-120954760) },
            },
            new DataConvertersTestClass {
                StringMember = "SprY,w:x9E",
                StringArrayMember = new string[] { "3.7qQu0%S\"", "wAH09w9GPs", "YHRQ4xmM6X", "kh,Qov.Guj", "dEvU9Sh:lO", ".7hQ3vtq9q", "HoO!gPM:MO", "92i3OTi#em", "PvxsjzAuk'", "CTTEc?;1,u" },
                CharMember = 'O',
                CharArrayMember = new char[] { 'j', '9', 'u', 'w', 'g', 'o', '\'', 'l', '\'', 'a' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'f', null, 'G', null, 'T', 'K', null, null, null, 'o' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, false, false, true, false, false, false, true, true },
                NullableBooleanMember = false,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, null, null, false, false, false, null, true, false },
                ByteMember = 102,
                ByteArrayMember = new byte[] { 249, 79, 199, 141, 133, 179, 49, 213, 151, 39 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 10, 162, 205, null, 196, 205, 9, 213, null, null },
                SByteMember = -39,
                SByteArrayMember = new sbyte[] { -68, -70, 54, -87, 13, 111, 118, 95, 95, -36 },
                NullableSByteMember = -47,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -115, -21, null, -26, 14, null, null, null, 49, -117 },
                Int16Member = -11886,
                Int16ArrayMember = new short[] { 29175, -19618, -22103, 10300, -20678, -5978, 10081, -26765, 10351, 3440 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { -2719, null, null, 10550, null, -1776, null, -26079, -5092, -536 },
                UInt16Member = 17690,
                UInt16ArrayMember = new ushort[] { 28268, 61987, 63203, 52943, 1007, 24832, 27113, 42582, 21530, 56782 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 43952, 17483, null, 32177, 5468, 15029, 65138, 36373, 59151 },
                Int32Member = -1219442422,
                Int32ArrayMember = new int[] { 286552661, 424560443, -870016144, -1642778472, 573872713, -1646139525, -338764929, 120794345, -1818131270, 651859570 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { 1794349269, null, null, null, 781545490, -1837723539, 1127179128, -1894575588, 247444267, -1438052056 },
                UInt32Member = 164601567,
                UInt32ArrayMember = new uint[] { 3269664858, 1269834957, 2804855792, 3663188668, 827099301, 2842738812, 3105846127, 1104649444, 2543744605, 2099776269 },
                NullableUInt32Member = 3317718578,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 3276253728, 2375897430, 1498560944, 3679676407, null, null, 2597175348, null, 2942454108, 1903448924 },
                Int64Member = 8987953423572131258,
                Int64ArrayMember = new long[] { -1086991342158336681, 2060768358380115229, 1101511976819990003, -5760354729589974178, -6639927276133107760, 7498473990569187399, -347572743555077820, 147571703781436387, -5347428995048805242, 8048675780185984450 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { -6014096324984489122, 2260469462670209959, -2259939639349485712, -1690403079465205578, 2487672162755534513, null, 7781729243962623897, null, null, null },
                UInt64Member = 14125992766320064498,
                UInt64ArrayMember = new ulong[] { 7605078483229648411, 14715163078473947616, 1343517319980188363, 6373828070732213850, 6769800846343201609, 3485165341876027307, 18005860361764172161, 9046687400569260800, 11586565941801978277, 10279988541575701518 },
                NullableUInt64Member = 189079587855777336,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 9760251588369921397, 9483637167346748511, 763183257836387829, null, null, 7248062988934924995, null, 6731617325420998738, null, 1391495756653354632 },
                SingleMember = 0.9687F,
                SingleArrayMember = new float[] { 0.4105F, 0.6514F, 0.9925F, 0.4041F, 0.9983F, 0.5428F, 0.1944F, 0.9226F, 0.3401F, 0.0136F },
                NullableSingleMember = 0.1186F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.0616F, 0.5532F, null, 0.2812F, null, 0.7158F, 0.9061F, null, 0.3556F, 0.3874F },
                DoubleMember = 0.6263,
                DoubleArrayMember = new double[] { 0.8929, 0.0895, 0.3488, 0.6873, 0.6136, 0.0148, 0.645, 0.4528, 0.516, 0.5057 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.6283, null, 0.5061, 0.6736, null, 0.7717, null, null, 0.6253, 0.2093 },
                DecimalMember = 0.427389678744315m,
                DecimalArrayMember = new decimal[] { 0.226043171820251m, 0.748121574869436m, 0.711357934731691m, 0.315679817141816m, 0.500868195435436m, 0.29903888856016m, 0.41672411394153m, 0.242771484070817m, 0.891649618694396m, 0.15120064660497m },
                NullableDecimalMember = 0.254500209938036m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.718820923342752m, 0.296560256880038m, 0.87439216993488m, 0.468954373369438m, 0.668447643829718m, 0.807883876752054m, 0.419707431653378m, null, 0.2483951702008m, 0.728877685837856m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, null, null, Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-265618891),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(276922627), DateTime.Now.AddSeconds(-201606725), DateTime.Now.AddSeconds(158672500), DateTime.Now.AddSeconds(-94674603), DateTime.Now.AddSeconds(303939584), DateTime.Now.AddSeconds(98981350), DateTime.Now.AddSeconds(-22513156), DateTime.Now.AddSeconds(-144040856), DateTime.Now.AddSeconds(121228583), DateTime.Now.AddSeconds(54838248) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(-32596031), null, DateTime.Now.AddSeconds(62829743), DateTime.Now.AddSeconds(-171066665), DateTime.Now.AddSeconds(-151109879), null, null, null, null, DateTime.Now.AddSeconds(-54569942) },
            },
            new DataConvertersTestClass {
                StringMember = "Fq2XbHY1Bj",
                StringArrayMember = new string[] { "NwMXai:th3", "OpeTLpjebZ", "d3H7gv\"ZQb", "bWsA1cH#0\"", "$TTHfnMiL9", "v!ZzGgpb;1", "podFc'#do6", "im0hcJkAb\"", "jhOe2g#\"!V", ".2F af;N2m" },
                CharMember = 'U',
                CharArrayMember = new char[] { '"', 'J', 'u', 'w', '$', '%', '8', 'm', 'e', 'd' },
                NullableCharMember = 'g',
                NullableCharArrayMember = new Nullable<char>[] { 'r', '9', 'L', 'N', 'Q', null, 'P', null, null, ';' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, false, true, false, true, false, true, true, true, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, true, true, false, null, null, false, true, true, true },
                ByteMember = 171,
                ByteArrayMember = new byte[] { 22, 212, 248, 109, 28, 248, 67, 84, 21, 248 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 214, 30, 116, 138, 165, null, 208, 65, 162, 160 },
                SByteMember = 114,
                SByteArrayMember = new sbyte[] { -124, 27, -41, 77, 89, 53, -47, -106, -24, 92 },
                NullableSByteMember = -33,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -17, -36, -100, -117, 74, 6, 73, 36, null, 91 },
                Int16Member = 267,
                Int16ArrayMember = new short[] { 11756, 19838, -28147, 23696, 10884, 4056, 29032, -20360, -507, 29803 },
                NullableInt16Member = -7953,
                NullableInt16ArrayMember = new Nullable<short>[] { -943, -31988, 8637, null, null, 31235, 15132, null, 21876, null },
                UInt16Member = 44593,
                UInt16ArrayMember = new ushort[] { 20367, 15153, 61, 23104, 18291, 43612, 18315, 21163, 64642, 21644 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 6367, null, 5442, null, null, null, null, 63381, null, null },
                Int32Member = -512173548,
                Int32ArrayMember = new int[] { 1925323335, 36980876, -1384512516, 798106355, -957589290, -1319209337, 1528062598, -2055353273, 686326271, -880366939 },
                NullableInt32Member = 1152331648,
                NullableInt32ArrayMember = new Nullable<int>[] { 444532184, 1521675964, null, 858385169, null, -1376483793, 747202874, 1174891296, null, null },
                UInt32Member = 4018164512,
                UInt32ArrayMember = new uint[] { 3859081664, 2026813907, 3825912797, 1994050008, 4138661255, 927154562, 100029696, 4282812048, 1535769691, 3774535984 },
                NullableUInt32Member = 1467907927,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 4269314827, null, 2137885349, null, 3368254950, null, 3683191684, 4222502271, 3300993892, null },
                Int64Member = -5884045462232628164,
                Int64ArrayMember = new long[] { -4233192355803785192, 5185281154068199830, 2742511542772960414, 5824030954728223920, -1248007067941637075, 7755366376434859075, -4830502817739433969, 8681082149709169544, 7933335553669493350, 1766780990580618590 },
                NullableInt64Member = 9048023082084780553,
                NullableInt64ArrayMember = new Nullable<long>[] { -1152115879760434291, null, -3357520387100158171, null, 5160050620698110921, -6688600558604883624, 9003353231823838143, null, -2187691215319380173, -4218896779790748804 },
                UInt64Member = 1468504782989751516,
                UInt64ArrayMember = new ulong[] { 7782562170632958189, 12406253515876830626, 5472003845369388174, 7430514391931978747, 5815702262094574932, 18319481441651940445, 8544450398529217737, 2734938071517342847, 524439870615838575, 2016620251407941542 },
                NullableUInt64Member = 11625772130885682313,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 639907974703877048, 15946753822147647683, 12620567609013045102, null, 18126904013877466182, null, 15253506727917567005, 8436987444399371767, null },
                SingleMember = 0.8394F,
                SingleArrayMember = new float[] { 0.3203F, 0.2596F, 0.196F, 0.3116F, 0.7315F, 0.4419F, 0.6473F, 0.5507F, 0.2182F, 0.2122F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.8674F, 0.7699F, 0.9205F, 0.4752F, 0.8716F, 0.8605F, null, 0.6947F, 0.0864F, 0.7338F },
                DoubleMember = 0.5564,
                DoubleArrayMember = new double[] { 0.7702, 0.0734, 0.9535, 0.0165, 0.502, 0.9262, 0.1384, 0.8836, 0.5268, 0.9707 },
                NullableDoubleMember = 0.5755,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.3657, 0.6126, 0.7249, null, 0.5781, 0.7562, 0.683, 0.8546, null, null },
                DecimalMember = 0.84581567013907m,
                DecimalArrayMember = new decimal[] { 0.474075070337427m, 0.912229621742028m, 0.521553189736583m, 0.819293561773046m, 0.692001056248323m, 0.121535359472751m, 0.300897294329897m, 0.282204612289651m, 0.943327921416298m, 0.459903955208093m },
                NullableDecimalMember = 0.438448050729208m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.869254514979783m, null, 0.74191910808064m, 0.300145113514804m, null, null, 0.137789259263216m, null, 0.903474057979637m, null },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, null, Guid.NewGuid(), null, null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(202424095),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-333220374), DateTime.Now.AddSeconds(-248638334), DateTime.Now.AddSeconds(209172618), DateTime.Now.AddSeconds(-21097073), DateTime.Now.AddSeconds(-54925814), DateTime.Now.AddSeconds(78460095), DateTime.Now.AddSeconds(-262330721), DateTime.Now.AddSeconds(280042081), DateTime.Now.AddSeconds(72085684), DateTime.Now.AddSeconds(198177698) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(107274870),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(249969969), DateTime.Now.AddSeconds(-99040556), DateTime.Now.AddSeconds(-326735991), null, null, DateTime.Now.AddSeconds(247177323), null, DateTime.Now.AddSeconds(-177428691), DateTime.Now.AddSeconds(112929619) },
            },
            new DataConvertersTestClass {
                StringMember = "q6!baIR:X#",
                StringArrayMember = new string[] { "#oA$SIa Z@", "#DiT#2h1H#", "TWV,Z0m9Du", "PEs$YV2H3S", "F;.6qnXyfu", "yn7bzwTLvo", "sn#23:hl!y", "AZ$,;djlU9", "d,ef@zTRVz", "BqjPxp!HCz" },
                CharMember = 'f',
                CharArrayMember = new char[] { 'O', 'r', ':', 'U', '%', 'c', 'V', '0', 'c', '5' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { 'd', 'X', null, 'w', '6', null, '9', null, '0', ',' },
                BooleanMember = false,
                BooleanArrayMember = new bool[] { false, true, true, false, true, true, true, true, true, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, true, null, false, true, null, null, null, true, null },
                ByteMember = 175,
                ByteArrayMember = new byte[] { 34, 179, 41, 128, 81, 142, 212, 236, 234, 94 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { 138, null, null, 176, null, 119, 6, 5, null, 40 },
                SByteMember = -109,
                SByteArrayMember = new sbyte[] { 106, 108, 91, 63, 76, 3, -114, -42, 89, -43 },
                NullableSByteMember = -28,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 73, 113, null, -104, null, -128, -71, 79, null, 91 },
                Int16Member = 12562,
                Int16ArrayMember = new short[] { -23784, -6340, -28182, 27799, 19709, -973, -21232, -10109, 27747, -18250 },
                NullableInt16Member = 29981,
                NullableInt16ArrayMember = new Nullable<short>[] { -7652, -7735, 18570, -13110, null, -11464, -30785, 18358, null, null },
                UInt16Member = 10049,
                UInt16ArrayMember = new ushort[] { 54364, 16960, 57330, 29639, 27323, 28219, 23059, 62429, 28574, 57112 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 54570, null, 37053, 46671, 54593, null, null, 60846, 27540, 18858 },
                Int32Member = -602961370,
                Int32ArrayMember = new int[] { -1748960979, 1813391181, -1738752109, 704628098, -899239089, -341340139, -1957476659, -1614437107, 915274787, 1247763910 },
                NullableInt32Member = 1970140669,
                NullableInt32ArrayMember = new Nullable<int>[] { -2058993175, null, -1664662493, -754529579, -1124471202, null, -228406985, null, 983177126, 1158765893 },
                UInt32Member = 2372028981,
                UInt32ArrayMember = new uint[] { 1511759250, 3037368272, 494727779, 4180176840, 1395605836, 792377593, 3175163607, 850455777, 3109305184, 1287172201 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 2944318434, null, 4086614681, null, 3949124243, 3545058986, null, 445431113, 3800603536, null },
                Int64Member = 1310280112126069828,
                Int64ArrayMember = new long[] { -1714082103319690873, -7635727610421832958, -8395172364594773649, 4783309604176957596, 2432866139800741238, -3272779589440593860, 3232295788062175619, 5127989937693408003, 3006654730056099762, -2576385419024857538 },
                NullableInt64Member = 5335179587197037788,
                NullableInt64ArrayMember = new Nullable<long>[] { -687373485591781059, -7906851193802423451, -3397084126982945565, -8443193553107944354, -2818648495440637215, 2845991314450847347, 1664822302029975977, 7142763378066447571, 7478267081111326796, null },
                UInt64Member = 11151741190426912427,
                UInt64ArrayMember = new ulong[] { 18433871596734945279, 5878332358254965463, 1426905128156981380, 11327928971000055614, 12784385935539761838, 11810091465425169671, 11309478706482063954, 17554253937029708234, 4686695454114044797, 6806430900195444861 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 13546643676661102147, 330609723582094285, null, 17414892492210697141, 7420040352821968580, 11906725715687454182, 17034498659677018507, 17763998997522486893, null, 10168512407387112017 },
                SingleMember = 0.1872F,
                SingleArrayMember = new float[] { 0.101F, 0.5352F, 0.5006F, 0.6933F, 0.5342F, 0.9226F, 0.7113F, 0.3302F, 0.8431F, 0.9059F },
                NullableSingleMember = 0.6039F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.0125F, 0.8484F, null, 0.4139F, null, 0.6182F, null, 0.5873F, null, 0.3818F },
                DoubleMember = 0.2058,
                DoubleArrayMember = new double[] { 0.0621, 0.3906, 0.8054, 0.0886, 0.6827, 0.4426, 0.5522, 0.353, 0.4128, 0.702 },
                NullableDoubleMember = null,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.9793, 0.3557, 0.5206, null, 0.9007, 0.9004, 0.5293, 0.6876, 0.828, null },
                DecimalMember = 0.0811607637820583m,
                DecimalArrayMember = new decimal[] { 0.910874657757056m, 0.430823633647907m, 0.994376313870017m, 0.558805506005327m, 0.422322960767114m, 0.825546035461848m, 0.780425139600609m, 0.343150918531768m, 0.197267733606169m, 0.0800599996373337m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.573494875604983m, 0.52562645195314m, 0.575050692807441m, null, 0.817613592286414m, 0.706005850204269m, 0.216433478154444m, 0.283503423576943m, 0.385411265019985m, 0.96395766174605m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-109143088),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(88739671), DateTime.Now.AddSeconds(-74991176), DateTime.Now.AddSeconds(-291666627), DateTime.Now.AddSeconds(8509525), DateTime.Now.AddSeconds(246557254), DateTime.Now.AddSeconds(-39292783), DateTime.Now.AddSeconds(-245402522), DateTime.Now.AddSeconds(262055965), DateTime.Now.AddSeconds(-274064680), DateTime.Now.AddSeconds(-317355601) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(-120265310),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(147224261), DateTime.Now.AddSeconds(283276776), null, null, DateTime.Now.AddSeconds(71481520), DateTime.Now.AddSeconds(-191305841), null, DateTime.Now.AddSeconds(289068290), DateTime.Now.AddSeconds(-334283695), DateTime.Now.AddSeconds(-153167469) },
            },
            new DataConvertersTestClass {
                StringMember = "F934uZq#kW",
                StringArrayMember = new string[] { "N4;8L3Jn7E", "uQPSwh?5OY", "ibjs3rIx8t", "JX4%lTzxD\"", "fdX%E?tkHN", "j85L.8VVFr", "vj2O@XiR3c", "4O1U'FEkn:", "%upuJYosK0", "$esjV#xIC3" },
                CharMember = 'g',
                CharArrayMember = new char[] { 'e', 'X', '4', 'w', ' ', 'O', 'r', 'o', 'D', 't' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, '5', null, null, '.', 'i', null, null, '!', ';' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, true, true, true, false, true, true, false, true, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { true, false, false, true, false, false, true, false, true, null },
                ByteMember = 147,
                ByteArrayMember = new byte[] { 218, 204, 159, 149, 116, 73, 68, 129, 173, 155 },
                NullableByteMember = 39,
                NullableByteArrayMember = new Nullable<byte>[] { null, 158, 24, 144, null, 243, null, 107, 169, 127 },
                SByteMember = 43,
                SByteArrayMember = new sbyte[] { 64, -20, -127, -69, -12, 64, 19, -48, 15, 16 },
                NullableSByteMember = 36,
                NullableSByteArrayMember = new Nullable<sbyte>[] { -32, -36, null, null, 103, -48, -59, 106, 33, 123 },
                Int16Member = 9870,
                Int16ArrayMember = new short[] { 4571, 19689, -17319, 11771, -13818, 25095, -29747, -15997, -17174, -8123 },
                NullableInt16Member = 10897,
                NullableInt16ArrayMember = new Nullable<short>[] { -1753, -16699, -10704, 1443, -1119, null, 15312, null, 11724, -11269 },
                UInt16Member = 34897,
                UInt16ArrayMember = new ushort[] { 15684, 22690, 26245, 51297, 43422, 36459, 8518, 32055, 46187, 11368 },
                NullableUInt16Member = 23850,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 33519, 62027, 35712, 30755, 63315, null, null, 37388, 8207, 23575 },
                Int32Member = 2085783688,
                Int32ArrayMember = new int[] { -454019617, 1985581809, 1929879192, 174265273, 522855037, 45063687, -1450877160, 1708541634, 1351958439, 1601299527 },
                NullableInt32Member = -1820848731,
                NullableInt32ArrayMember = new Nullable<int>[] { 133837243, 1949523557, -1848840571, -934103889, null, -1038975367, 1646578353, -1538167242, null, null },
                UInt32Member = 2977945135,
                UInt32ArrayMember = new uint[] { 595954215, 398284567, 1467656272, 4262342013, 2037053160, 2685334068, 1811495210, 73724472, 542849763, 532425160 },
                NullableUInt32Member = 2699324722,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 904165997, 897320299, 1591605476, 1984126686, 904843587, 49564474, 638779433, 750540016, 2737225437 },
                Int64Member = 3006726739875777066,
                Int64ArrayMember = new long[] { 3173200377176355448, -6812963119623046130, 6737605769016259263, 5175112074559187112, -2855847687772092402, -6118495505946526439, -5734534557829213097, 6713105113643835122, -4469323701604728790, -1397425896708047148 },
                NullableInt64Member = -2434736587076078269,
                NullableInt64ArrayMember = new Nullable<long>[] { -2480537471765231173, 333647430081519361, 4986800639198783246, 773770753906642810, null, -3631490440002556164, 8514018763011744605, -3595678954549770719, null, -8788981748115882312 },
                UInt64Member = 2452718944922862696,
                UInt64ArrayMember = new ulong[] { 10265922714049154647, 18278467377207988364, 8124996295142372449, 16876333302808454442, 7959998485939279212, 4562336383968274315, 10236105235244515801, 14182977499650790349, 14450559284628693531, 9055587130155853308 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { null, 3062386691232072632, 11726669825924811404, 7680169514627310826, null, 4619707116151531493, 12843188107375185074, 11235026455782168845, null, null },
                SingleMember = 0.0368F,
                SingleArrayMember = new float[] { 0.4978F, 0.5609F, 0.7035F, 0.4843F, 0.0251F, 0.372F, 0.1737F, 0.3668F, 0.6122F, 0.2357F },
                NullableSingleMember = 0.2092F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.632F, 0.7633F, 0.7916F, null, null, 0.5423F, 0.4333F, null, 0.4828F, 0.5327F },
                DoubleMember = 0.5941,
                DoubleArrayMember = new double[] { 0.0716, 0.6174, 0.3074, 0.321, 0.9086, 0.9273, 0.7701, 0.6777, 0.683, 0.6304 },
                NullableDoubleMember = 0.7873,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.7764, 0.5884, 0.6463, 0.8998, null, null, 0.6755, 0.1617, null, 0.1104 },
                DecimalMember = 0.997925508300739m,
                DecimalArrayMember = new decimal[] { 0.788526998268686m, 0.492723649597132m, 0.219736530547839m, 0.371329954532594m, 0.51250428776839m, 0.465527641338076m, 0.53459144501695m, 0.0161250899620937m, 0.907009240662218m, 0.0888293753791737m },
                NullableDecimalMember = 0.484890646992666m,
                NullableDecimalArrayMember = new Nullable<decimal>[] { null, null, null, 0.767750026550028m, null, 0.982544835183092m, 0.828145457351648m, 0.105789962739586m, 0.186842261434925m, 0.526106506831062m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-229361104),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-227497124), DateTime.Now.AddSeconds(48304976), DateTime.Now.AddSeconds(-25260341), DateTime.Now.AddSeconds(39185168), DateTime.Now.AddSeconds(333346013), DateTime.Now.AddSeconds(-76831126), DateTime.Now.AddSeconds(-177886103), DateTime.Now.AddSeconds(-251234655), DateTime.Now.AddSeconds(110982341), DateTime.Now.AddSeconds(47448055) },
                NullableDateTimeMember = DateTime.Now.AddSeconds(267902498),
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(169591902), DateTime.Now.AddSeconds(-223372922), DateTime.Now.AddSeconds(-146160942), DateTime.Now.AddSeconds(79063310), DateTime.Now.AddSeconds(137084597), DateTime.Now.AddSeconds(-308551902), null, null, DateTime.Now.AddSeconds(234605358), null },
            },
            new DataConvertersTestClass {
                StringMember = "db31fUGkT5",
                StringArrayMember = new string[] { "384NtA\"HEu", "5;HDfasR #", "e@h6kt;,P,", "REZc#Mwe;U", "DqEf%mU0WF", "z,kXa7a$z ", "JF'Qd1o1HS", "0Ob2',:C8%", "ZNoJMcqCBL", "9y1;K%x,CT" },
                CharMember = '!',
                CharArrayMember = new char[] { '5', 'n', 'U', 'V', 'y', 'g', 'U', '%', 'h', ';' },
                NullableCharMember = null,
                NullableCharArrayMember = new Nullable<char>[] { null, null, 'I', 'D', null, 'c', null, null, '3', '$' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, true, true, true, true, true, true, true, false, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { null, false, false, null, null, null, false, true, null, null },
                ByteMember = 35,
                ByteArrayMember = new byte[] { 6, 152, 102, 65, 18, 29, 190, 247, 68, 65 },
                NullableByteMember = 13,
                NullableByteArrayMember = new Nullable<byte>[] { null, 191, 215, 103, 236, null, 179, 230, 137, 74 },
                SByteMember = 95,
                SByteArrayMember = new sbyte[] { 125, -6, -82, 20, 2, -29, 90, -43, -97, -34 },
                NullableSByteMember = -12,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, null, 78, -34, null, null, -90, 9, null, null },
                Int16Member = 23797,
                Int16ArrayMember = new short[] { -15056, 3245, -6583, 9552, -12705, 28470, -26089, -16080, -31446, 8218 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { 4151, null, -13124, null, -3342, -110, -26893, 20610, 16263, null },
                UInt16Member = 7150,
                UInt16ArrayMember = new ushort[] { 57590, 18980, 57114, 26922, 34562, 54550, 10599, 11488, 43298, 10724 },
                NullableUInt16Member = 64642,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 34711, 56271, 1006, 43657, 24025, null, null, 58670, null },
                Int32Member = 108035771,
                Int32ArrayMember = new int[] { -900412813, 1373575001, -1329860534, -1287456014, -1271970585, -1340993900, -310673585, -721944390, -1369990568, -2123806935 },
                NullableInt32Member = -730736008,
                NullableInt32ArrayMember = new Nullable<int>[] { -1604233604, 1924943478, null, null, -224667269, 1474820170, null, -610301549, -1607263262, -1960508471 },
                UInt32Member = 3235852771,
                UInt32ArrayMember = new uint[] { 2685359154, 2614141738, 4252479116, 2960724256, 3068519506, 2710135251, 2224479511, 2354203307, 2915207771, 1876330613 },
                NullableUInt32Member = 1168467183,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1967442808, 3962063871, 1300852426, null, 3931914992, 986468245, 3831461134, null, null, 1087071464 },
                Int64Member = -1793031712667338589,
                Int64ArrayMember = new long[] { -2988632247216494748, -2281681639137924375, 7613858764568217229, -2927936300872607763, 8756562744262534022, -3682908083063008584, -8668509219580702045, 532560252895230333, -183130647373361204, 4525318554913905600 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, null, 8247403117231831855, null, 7195851380612837311, null, 6073052575855391517, null, -8087813094238256893, null },
                UInt64Member = 17817494561519981319,
                UInt64ArrayMember = new ulong[] { 17250545757013275390, 4233170669221202715, 9902773872628319836, 2789836860882999830, 14060819634265666628, 14480986032217659844, 5087249455396265876, 11391683759874431876, 5495798920650383764, 5293645999199568804 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 17842058555508406381, 728458750415153434, 3061432182616153973, 17768733103464204373, 3153937540590403281, 15331875878704940702, 5054322021096151463, 5341946619586459012, null, 11807948041833309142 },
                SingleMember = 0.106F,
                SingleArrayMember = new float[] { 0.1351F, 0.1416F, 0.3294F, 0.2933F, 0.3454F, 0.3451F, 0.4047F, 0.1195F, 0.0439F, 0.6405F },
                NullableSingleMember = 0.4817F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.9006F, 0.1347F, 0.9861F, 0.2045F, null, null, null, 0.1906F, 0.6107F, 0.0945F },
                DoubleMember = 0.9951,
                DoubleArrayMember = new double[] { 0.1255, 0.7242, 0.0195, 0.8186, 0.6305, 0.8473, 0.6824, 0.6583, 0.2014, 0.4285 },
                NullableDoubleMember = 0.8429,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.6685, 0.2154, null, 0.314, null, null, 0.0581, null, 0.6229, null },
                DecimalMember = 0.138819537190171m,
                DecimalArrayMember = new decimal[] { 0.568282279916239m, 0.395013874580624m, 0.568710556052956m, 0.233310071860119m, 0.0821344913365946m, 0.816032202363029m, 0.464246385946985m, 0.353758345057144m, 0.44947193351084m, 0.0131221641847501m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.472180380705828m, 0.728784216907241m, 0.221115203211603m, null, 0.689523921203578m, null, null, 0.466347567488601m, 0.664862925496354m, 0.681525532007928m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = null,
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(13554666),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-55009088), DateTime.Now.AddSeconds(250413136), DateTime.Now.AddSeconds(65223433), DateTime.Now.AddSeconds(305296423), DateTime.Now.AddSeconds(260141523), DateTime.Now.AddSeconds(2809617), DateTime.Now.AddSeconds(254995030), DateTime.Now.AddSeconds(-252411447), DateTime.Now.AddSeconds(163878248), DateTime.Now.AddSeconds(-58516830) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { null, DateTime.Now.AddSeconds(143286792), DateTime.Now.AddSeconds(288204753), DateTime.Now.AddSeconds(211835471), DateTime.Now.AddSeconds(162441211), null, DateTime.Now.AddSeconds(-178340937), DateTime.Now.AddSeconds(-15966670), null, DateTime.Now.AddSeconds(-66708439) },
            },
            new DataConvertersTestClass {
                StringMember = "%psEbkk5em",
                StringArrayMember = new string[] { "\"XM1x@n6DA", "Km0@fCjks!", "ND o 6sk4y", "'7ulSlmAPO", "h:7BJi?xvZ", "B NjZ?Yu1:", "aDBbIZ:Oz!", "mjVdkVS%fo", "!!,fw'uLMc", "N'VY.d;f\"6" },
                CharMember = 'd',
                CharArrayMember = new char[] { ';', '7', 'W', '9', 'p', 'J', 'U', '8', 'A', '5' },
                NullableCharMember = '$',
                NullableCharArrayMember = new Nullable<char>[] { '\'', 'C', ';', null, '1', null, '%', null, 'k', null },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, true, false, false, true, true, false, true, true, true },
                NullableBooleanMember = true,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, null, null, false, true, true, true, true, false, null },
                ByteMember = 158,
                ByteArrayMember = new byte[] { 40, 1, 137, 149, 51, 192, 93, 207, 129, 42 },
                NullableByteMember = 158,
                NullableByteArrayMember = new Nullable<byte>[] { 91, 84, null, null, 80, null, 52, 111, null, 248 },
                SByteMember = 58,
                SByteArrayMember = new sbyte[] { -119, 9, -112, 112, -22, 19, 47, -113, -86, -126 },
                NullableSByteMember = -41,
                NullableSByteArrayMember = new Nullable<sbyte>[] { 19, 72, null, 91, null, 118, 84, -24, null, null },
                Int16Member = 10444,
                Int16ArrayMember = new short[] { -25645, -14484, -18058, 27861, -29094, 11933, 11000, 11140, -9129, -23229 },
                NullableInt16Member = 8090,
                NullableInt16ArrayMember = new Nullable<short>[] { -13933, 6607, 30455, -6104, 16525, -3016, -32091, null, null, null },
                UInt16Member = 10806,
                UInt16ArrayMember = new ushort[] { 12204, 34035, 30574, 42693, 58484, 6360, 22402, 39362, 49191, 53799 },
                NullableUInt16Member = null,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { null, 18671, null, 59135, 33303, 56172, 22492, 15990, null, 25797 },
                Int32Member = -2055129063,
                Int32ArrayMember = new int[] { -151902308, -443495291, 2039538143, -1289474323, -1691871538, 827840928, -709769193, 293431158, 800809334, -771514960 },
                NullableInt32Member = -1556479032,
                NullableInt32ArrayMember = new Nullable<int>[] { null, -1133322598, -1025186288, null, -1172652578, 41837432, null, 321979125, null, -1927424296 },
                UInt32Member = 1079179573,
                UInt32ArrayMember = new uint[] { 3824196914, 1653210887, 1210187531, 2095287643, 4052171384, 3540568534, 3902870246, 1220151873, 4151290122, 3392399710 },
                NullableUInt32Member = 3689239314,
                NullableUInt32ArrayMember = new Nullable<uint>[] { 1317034752, 1784771323, null, 1593179421, 3645177889, null, null, 126820075, 594920299, null },
                Int64Member = -904980864868028421,
                Int64ArrayMember = new long[] { -4424064188700133975, 4980808144157301371, 676592629898349103, 5939955547127829580, 8139323029535448499, -7390281760332070509, -1402327993504842288, -8815714461196945647, 9158421431525739499, -2727653733089456874 },
                NullableInt64Member = 6036288902302302097,
                NullableInt64ArrayMember = new Nullable<long>[] { -3019584345710381874, null, -3417653067253436665, -3830636690173359391, -1913728104537840611, -3185061385396140835, null, null, null, 7001451944609002585 },
                UInt64Member = 17236059313175728894,
                UInt64ArrayMember = new ulong[] { 6500234789292474397, 10889472322139682112, 2715682573865827511, 14057584121382038331, 6768672604267689626, 15078629537871717421, 1298885847402860531, 12471032874413933904, 10356025015974351455, 3276640838787243981 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 4048747026624051264, 6476548083936296346, 3104574542340560856, null, 11151755472305674647, 1304966005372213547, 10472450912548964502, null, 8692421087861845343, 12821097616129281 },
                SingleMember = 0.9828F,
                SingleArrayMember = new float[] { 0.8891F, 0.0234F, 0.834F, 0.5062F, 0.1006F, 0.0788F, 0.8798F, 0.5871F, 0.2867F, 0.7514F },
                NullableSingleMember = null,
                NullableSingleArrayMember = new Nullable<float>[] { 0.7477F, 0.4433F, null, 0.0496F, 0.2551F, 0.4967F, 0.3282F, null, null, 0.5473F },
                DoubleMember = 0.6319,
                DoubleArrayMember = new double[] { 0.118, 0.7469, 0.2813, 0.494, 0.5673, 0.6879, 0.5049, 0.3362, 0.7678, 0.4935 },
                NullableDoubleMember = 0.368,
                NullableDoubleArrayMember = new Nullable<double>[] { 0.4451, null, null, 0.5443, 0.5536, 0.9876, null, 0.962, 0.4109, null },
                DecimalMember = 0.113946567808253m,
                DecimalArrayMember = new decimal[] { 0.166461146979808m, 0.325662026333465m, 0.687584805622504m, 0.00848493818588785m, 0.877305552771923m, 0.338726960745979m, 0.693815500798549m, 0.532475156026182m, 0.251580575132547m, 0.579724617106712m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.518121041598786m, 0.549915189179552m, 0.942085587392601m, 0.659826097385877m, 0.231604873776252m, 0.604719997665249m, 0.836582585627484m, 0.0787685509206581m, null, 0.251897245297161m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(-345564492),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(53300501), DateTime.Now.AddSeconds(29587423), DateTime.Now.AddSeconds(-62491462), DateTime.Now.AddSeconds(29324110), DateTime.Now.AddSeconds(-252572326), DateTime.Now.AddSeconds(-247542858), DateTime.Now.AddSeconds(244959355), DateTime.Now.AddSeconds(-147320936), DateTime.Now.AddSeconds(-130272300), DateTime.Now.AddSeconds(259450379) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(85059232), null, null, DateTime.Now.AddSeconds(-186287554), DateTime.Now.AddSeconds(-303866522), DateTime.Now.AddSeconds(96544907), null, DateTime.Now.AddSeconds(33026760), DateTime.Now.AddSeconds(313199864), DateTime.Now.AddSeconds(78754072) },
            },
            new DataConvertersTestClass {
                StringMember = "qx$19p16 D",
                StringArrayMember = new string[] { "taON#gPt1P", "23CX3uN#qG", "f.Ew$U#KsH", "O.MJ6.0Hbe", "OU00D.vx$O", "b4Q'u6VpQz", "jEAuB'r'n\"", "\"GrM'LJbsJ", "vWNe2gtTl ", "pL:z!KTGyL" },
                CharMember = 'f',
                CharArrayMember = new char[] { '1', 'o', 'e', '0', 'D', 'd', 'N', 'L', 'D', '.' },
                NullableCharMember = 'R',
                NullableCharArrayMember = new Nullable<char>[] { 'I', '1', null, 'k', 'X', '?', null, 'z', null, 'X' },
                BooleanMember = true,
                BooleanArrayMember = new bool[] { true, false, true, false, true, true, true, false, false, true },
                NullableBooleanMember = null,
                NullableBooleanArrayMember = new Nullable<bool>[] { false, false, false, null, true, false, null, true, true, null },
                ByteMember = 43,
                ByteArrayMember = new byte[] { 201, 53, 206, 248, 162, 238, 11, 20, 126, 139 },
                NullableByteMember = null,
                NullableByteArrayMember = new Nullable<byte>[] { null, 229, 205, 217, null, 145, 50, 249, 116, 83 },
                SByteMember = 87,
                SByteArrayMember = new sbyte[] { 6, 35, -91, 119, 39, 81, 75, -113, -20, 71 },
                NullableSByteMember = 86,
                NullableSByteArrayMember = new Nullable<sbyte>[] { null, 76, -122, null, -71, null, null, null, null, 124 },
                Int16Member = -13224,
                Int16ArrayMember = new short[] { 12202, 22115, -23514, 31800, 13214, 8621, 21254, -27125, 7206, 16848 },
                NullableInt16Member = null,
                NullableInt16ArrayMember = new Nullable<short>[] { null, null, -20213, -25355, 1323, null, 8047, -5345, -19280, -12787 },
                UInt16Member = 56257,
                UInt16ArrayMember = new ushort[] { 26395, 2036, 41253, 57188, 64011, 15107, 1571, 49093, 52512, 7509 },
                NullableUInt16Member = 35195,
                NullableUInt16ArrayMember = new Nullable<ushort>[] { 26576, 54931, null, 34698, 38997, 62028, 36247, 55358, null, null },
                Int32Member = -149431824,
                Int32ArrayMember = new int[] { 24816131, 1610120592, -1203388689, -678733424, 660743205, -276074263, -334719558, -470928717, -848575741, 225606888 },
                NullableInt32Member = null,
                NullableInt32ArrayMember = new Nullable<int>[] { -1647480302, null, null, null, -277361816, null, -79197409, null, 1657847211, null },
                UInt32Member = 1732579658,
                UInt32ArrayMember = new uint[] { 3602858739, 1644510888, 1816063040, 2673078756, 1028611330, 4080611221, 4104648117, 3820168561, 3190700056, 1922368133 },
                NullableUInt32Member = null,
                NullableUInt32ArrayMember = new Nullable<uint>[] { null, 2253441186, 3975447330, 163828232, 295154209, 1398421786, null, 3338672533, null, 731271437 },
                Int64Member = 136718552118026336,
                Int64ArrayMember = new long[] { -8307969516055149982, 1919771518212919896, -1514371706087238437, -3259716550189087929, 3457025056116427645, -406473883283237248, -8931212056751630149, 3421106950242817863, 730062480444894094, 6756202566074209435 },
                NullableInt64Member = null,
                NullableInt64ArrayMember = new Nullable<long>[] { null, null, 2353673682873650055, 2881337558632663741, -1187437804868204795, null, null, 5477125375066379129, null, -4529201905678677496 },
                UInt64Member = 10739564472927722924,
                UInt64ArrayMember = new ulong[] { 11020914315509911509, 669698290595871352, 971334461185674930, 1541214183925325424, 15681742416171644692, 7850654745469416011, 17728769061054593745, 17975412569754234837, 17610460107336356311, 5704088127119898542 },
                NullableUInt64Member = null,
                NullableUInt64ArrayMember = new Nullable<ulong>[] { 2318746386799388833, 11959023815160263605, null, 13497568616901853982, 10222116610723272711, 9272737947082359842, 10027498044630283739, 1818640875903783612, null, 5729249908861053816 },
                SingleMember = 0.1027F,
                SingleArrayMember = new float[] { 0.3899F, 0.7343F, 0.1346F, 0.025F, 0.0611F, 0.7514F, 0.133F, 0.6477F, 0.2835F, 0.8F },
                NullableSingleMember = 0.2549F,
                NullableSingleArrayMember = new Nullable<float>[] { 0.6995F, null, 0.4484F, 0.6167F, null, 0.2029F, null, 0.8393F, 0.3924F, 0.5058F },
                DoubleMember = 0.6661,
                DoubleArrayMember = new double[] { 0.586, 0.6117, 0.0075, 0.4521, 0.0505, 0.5403, 0.4545, 0.1472, 0.519, 0.2756 },
                NullableDoubleMember = 0.1636,
                NullableDoubleArrayMember = new Nullable<double>[] { null, null, 0.8223, 0.4838, 0.226, 0.5468, null, 0.8417, 0.1304, 0.6036 },
                DecimalMember = 0.41727156584024m,
                DecimalArrayMember = new decimal[] { 0.855650661445991m, 0.782720853939057m, 0.239811777248891m, 0.283010786531032m, 0.687460137851285m, 0.676584017312426m, 0.031974309604603m, 0.594874827933905m, 0.829592399219792m, 0.0349687161086913m },
                NullableDecimalMember = null,
                NullableDecimalArrayMember = new Nullable<decimal>[] { 0.00239764666296432m, 0.810595229179876m, 0.171235635956393m, 0.522423595433321m, 0.803980723397797m, 0.599111631325964m, 0.182707542638624m, 0.698672956646734m, 0.150110052502765m, 0.121175537873607m },
                GuidMember = Guid.NewGuid(),
                GuidArrayMember = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                NullableGuidMember = Guid.NewGuid(),
                NullableGuidArrayMember = new Nullable<Guid>[] { Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid() },
                DateTimeMember = DateTime.Now.AddSeconds(297454211),
                DateTimeArrayMember = new DateTime[] { DateTime.Now.AddSeconds(-28736907), DateTime.Now.AddSeconds(-75276756), DateTime.Now.AddSeconds(143924912), DateTime.Now.AddSeconds(-28296532), DateTime.Now.AddSeconds(-256846956), DateTime.Now.AddSeconds(-239422254), DateTime.Now.AddSeconds(261895747), DateTime.Now.AddSeconds(-176345660), DateTime.Now.AddSeconds(293569008), DateTime.Now.AddSeconds(177076695) },
                NullableDateTimeMember = null,
                NullableDateTimeArrayMember = new Nullable<DateTime>[] { DateTime.Now.AddSeconds(85525688), null, DateTime.Now.AddSeconds(232238800), null, DateTime.Now.AddSeconds(-228083564), null, DateTime.Now.AddSeconds(-179679117), DateTime.Now.AddSeconds(162751227), DateTime.Now.AddSeconds(-92109399), DateTime.Now.AddSeconds(-2158556) },
            },
        };
    }
}
