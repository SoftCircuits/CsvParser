// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;

namespace CsvParserTests
{
    public static class TypeConvertersTestData
    {
        public static List<(Type Type, object Data)> TestItems = new List<(Type Type, object Data)>
        {
            (typeof(string), (string)"xFZwZ:ftYZ"),
            (typeof(string[]), new string[] { "%hSirXvyQ$", ";zB$@$; ue", "5r!ov#AR?y", "MEPI?DER@N", "L5,wa$Eb1c", "Af%!rdn1.v", "We9o78k\"I:", "\"vEhS 3UOy", ":wmkx3GVoi", "nZeX:4#X?o" }),
            (typeof(char), (char)';'),
            (typeof(char?), (char?)null),
            (typeof(char[]), new char[] { '@', 'k', 'q', 'h', 'n', 'H', '?', 'J', 'l', 'X' }),
            (typeof(char?[]), new char?[] { 'Z', 'y', '\'', null, 'C', 'l', 'Z', 'Q', 't', '#' }),
            (typeof(bool), (bool)true),
            (typeof(bool?), (bool?)null),
            (typeof(bool[]), new bool[] { true, true, false, true, false, false, false, true, true, true }),
            (typeof(bool?[]), new bool?[] { null, false, true, true, false, null, false, null, true, false }),
            (typeof(byte), (byte)151),
            (typeof(byte?), (byte?)null),
            (typeof(byte[]), new byte[] { 70, 220, 77, 194, 32, 114, 129, 23, 44, 154 }),
            (typeof(byte?[]), new byte?[] { 141, null, null, null, null, null, null, 156, null, null }),
            (typeof(sbyte), (sbyte)-65),
            (typeof(sbyte?), (sbyte?)null),
            (typeof(sbyte[]), new sbyte[] { 0, -117, 45, -58, 103, 77, -57, -21, -95, 6 }),
            (typeof(sbyte?[]), new sbyte?[] { -119, null, -56, null, -27, null, null, null, null, null }),
            (typeof(short), (short)8820),
            (typeof(short?), (short?)null),
            (typeof(short[]), new short[] { 28069, -29841, 5629, -13680, 15431, 6149, 20757, -15868, -14960, 3929 }),
            (typeof(short?[]), new short?[] { 27042, 24511, null, null, null, 22033, null, null, null, null }),
            (typeof(ushort), (ushort)44072),
            (typeof(ushort?), (ushort?)null),
            (typeof(ushort[]), new ushort[] { 22143, 50906, 34748, 63317, 17435, 672, 40061, 4888, 6839, 57311 }),
            (typeof(ushort?[]), new ushort?[] { null, null, 39002, null, null, null, 24915, null, null, null }),
            (typeof(int), (int)1998334988),
            (typeof(int?), (int?)null),
            (typeof(int[]), new int[] { -489974010, -2127142130, -1899821720, -726541981, -1986477059, -1570009544, -150470415, -465669865, -324793676, -600410293 }),
            (typeof(int?[]), new int?[] { -2088372228, null, null, 1745815019, 746080493, 1046154993, -639645961, null, null, null }),
            (typeof(uint), (uint)2331143781),
            (typeof(uint?), (uint?)331002325),
            (typeof(uint[]), new uint[] { 1665294549, 1969566793, 1001755564, 4078820150, 476036571, 1397538900, 4189341343, 2698539321, 1232383889, 402323236 }),
            (typeof(uint?[]), new uint?[] { null, 1806521289, null, null, null, 2639337457, 3304002580, null, null, 347814460 }),
            (typeof(long), (long)8332067795970879699),
            (typeof(long?), (long?)-4346818580823709199),
            (typeof(long[]), new long[] { 1606923999256625939, 5262581071390270534, 5325043030899773096, -8074771337583098889, 69315869537590621, 9219239763158880260, -5091795968843471520, -6857241609594637091, 7412506471042406771, -6516472138644753576 }),
            (typeof(long?[]), new long?[] { -8786799162514818214, -7135156559785625945, -2239476637094408866, -4916118922369123340, -2665789351971239364, 3547852037679594041, null, -3811473728956342729, 5890251116951442080, null }),
            (typeof(ulong), (ulong)6501060745138737789),
            (typeof(ulong?), (ulong?)5771609996197977449),
            (typeof(ulong[]), new ulong[] { 6820907690283483849, 13310338452571881213, 13280793511810618415, 9328358833910186402, 6203387686892914129, 10864707002124163986, 17148160470728163494, 103055177869843059, 5943834006601089673, 8161899718843265470 }),
            (typeof(ulong?[]), new ulong?[] { 14650325671527506434, 5687045995811758469, 2581201299865327665, 2389933755400781949, null, null, 12077308599098399222, null, null, 18011578915293969933 }),
            (typeof(float), (float)0.04278442F),
            (typeof(float?), (float?)0.1648707F),
            (typeof(float[]), new float[] { 0.9849914F, 0.1539859F, 0.9560515F, 0.4642398F, 0.4673147F, 0.8343465F, 0.8509492F, 0.09908468F, 0.966545F, 0.4795527F }),
            (typeof(float?[]), new float?[] { null, 0.247147F, null, 0.03175093F, 0.6783224F, null, 0.6383852F, null, null, null }),
            (typeof(double), (double)0.82890755),
            (typeof(double?), (double?)0.22998475),
            (typeof(double[]), new double[] { 0.68502826, 0.05440055, 0.2658899, 0.18039361, 0.87553952, 0.9964546, 0.62053535, 0.5284682, 0.09925322, 0.91014865 }),
            (typeof(double?[]), new double?[] { 0.21467578, null, 0.28265449, null, null, null, 0.23117989, null, null, 0.59563939 }),
            (typeof(decimal), (decimal)0.431209469880541m),
            (typeof(decimal?), (decimal?)0.449844123539442m),
            (typeof(decimal[]), new decimal[] { 0.525472503865823m, 0.00489820773010059m, 0.458065046210804m, 0.433965642672947m, 0.241309247557684m, 0.588172160828566m, 0.689373691421642m, 0.599446365423243m, 0.503362755059899m, 0.507751017579693m }),
            (typeof(decimal?[]), new decimal?[] { null, 0.718117636962849m, 0.325389110169089m, null, 0.296993987307415m, null, 0.0325955366867574m, null, null, 0.789776739100822m }),
            (typeof(Guid), (Guid)Guid.NewGuid()),
            (typeof(Guid?), (Guid?)null),
            (typeof(Guid[]), new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }),
            (typeof(Guid?[]), new Guid?[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null }),
            (typeof(DateTime), (DateTime)DateTime.Now.AddSeconds(-233003922)),
            (typeof(DateTime?), (DateTime?)DateTime.Now.AddSeconds(231190383)),
            (typeof(DateTime[]), new DateTime[] { DateTime.Now.AddSeconds(-229663832), DateTime.Now.AddSeconds(84748424), DateTime.Now.AddSeconds(129490740), DateTime.Now.AddSeconds(-246721597), DateTime.Now.AddSeconds(-302706290), DateTime.Now.AddSeconds(-271024695), DateTime.Now.AddSeconds(293719327), DateTime.Now.AddSeconds(-256508647), DateTime.Now.AddSeconds(159106432), DateTime.Now.AddSeconds(-39937550) }),
            (typeof(DateTime?[]), new DateTime?[] { null, DateTime.Now.AddSeconds(-238036671), null, DateTime.Now.AddSeconds(-305813346), null, DateTime.Now.AddSeconds(348926776), DateTime.Now.AddSeconds(-297840644), DateTime.Now.AddSeconds(-222904874), null, null }),
        };
    }
}
