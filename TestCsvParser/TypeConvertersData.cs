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
            (typeof(string), (string)"UjKYiH%rPG"),
            (typeof(string[]), new string[] { "6IM9Cxsphp", "rOP;aZuydd", "q5B'@1Bz T", "5Yk .bql73", "ytqUkGUU$J", "kXO9nAGT:Y", "w'pN7T0QC%", "6I;w1$O3f,", "g41;6hJyj.", "UWtjH6EdkR" }),
            (typeof(char), (char)'U'),
            (typeof(char?), (char?)'z'),
            (typeof(char[]), new char[] { 'G', '8', '9', ';', 'c', '"', 'K', 'q', 'k', 'S' }),
            (typeof(char?[]), new char?[] { null, 'O', 'y', null, null, null, 'G', null, '8', null }),
            (typeof(bool), (bool)true),
            (typeof(bool?), (bool?)false),
            (typeof(bool[]), new bool[] { true, true, true, true, false, false, true, true, true, false }),
            (typeof(bool?[]), new bool?[] { null, null, false, null, null, null, true, null, null, true }),
            (typeof(byte), (byte)68),
            (typeof(byte?), (byte?)59),
            (typeof(byte[]), new byte[] { 175, 97, 222, 113, 66, 237, 54, 39, 191, 204 }),
            (typeof(byte?[]), new byte?[] { null, 210, null, 42, null, null, 20, null, null, null }),
            (typeof(sbyte), (sbyte)-36),
            (typeof(sbyte?), (sbyte?)null),
            (typeof(sbyte[]), new sbyte[] { -79, -98, 50, 112, 0, -123, -66, -50, 112, 20 }),
            (typeof(sbyte?[]), new sbyte?[] { null, -75, 98, 123, -128, null, 104, 10, null, 81 }),
            (typeof(short), (short)32358),
            (typeof(short?), (short?)4985),
            (typeof(short[]), new short[] { -6373, 2019, 27946, 21220, 9339, 26850, -28554, 24989, -3515, -7315 }),
            (typeof(short?[]), new short?[] { null, null, null, 11286, -30733, 19090, null, null, 29070, -1843 }),
            (typeof(ushort), (ushort)4252),
            (typeof(ushort?), (ushort?)14650),
            (typeof(ushort[]), new ushort[] { 3385, 15099, 16450, 58359, 14219, 11659, 37031, 23917, 55037, 13877 }),
            (typeof(ushort?[]), new ushort?[] { null, null, null, null, 28127, 17570, null, 22607, null, 7183 }),
            (typeof(int), (int)1876529347),
            (typeof(int?), (int?)null),
            (typeof(int[]), new int[] { 381212501, -232750363, 1367577954, 185877407, 2040682519, 545703283, -486332545, 49692262, 2052246665, 1196609727 }),
            (typeof(int?[]), new int?[] { -1312341184, -1553139171, null, null, null, -1756262294, -1958969411, -741834337, null, 1527072587 }),
            (typeof(uint), (uint)4063538333),
            (typeof(uint?), (uint?)null),
            (typeof(uint[]), new uint[] { 3438109945, 2434255440, 2656837586, 2516406586, 1215670506, 3372536377, 3899773882, 3393346834, 2685084659, 3825126997 }),
            (typeof(uint?[]), new uint?[] { 785037897, 3786734188, 1168752132, 2081563744, 3698621999, null, null, null, 2853978204, 957935565 }),
            (typeof(long), (long)4431405050458675138),
            (typeof(long?), (long?)null),
            (typeof(long[]), new long[] { 6911215425048014099, 4372184742823624732, 8394797801715763213, -1635041890163195460, 4670509788655429046, -511356546605185812, -4328318778004933432, -986682354580079764, 1320509428581103665, 8180052364306628279 }),
            (typeof(long?[]), new long?[] { -5188734645233219849, 1564529869430140379, 4848865398154733941, null, null, null, 2608168749473871114, null, null, 4944446830882147454 }),
            (typeof(ulong), (ulong)8755515616175635764),
            (typeof(ulong?), (ulong?)null),
            (typeof(ulong[]), new ulong[] { 14992255743054199531, 14579664823518707568, 5979153051555667051, 87521868222971267, 9082885785891235673, 6794747220958248807, 12860304531839558707, 9233265094286523199, 9676988619935716617, 13382744737226173711 }),
            (typeof(ulong?[]), new ulong?[] { 7953123275499419172, 5249862152775091332, null, 12153841389699614725, null, 1976950469305643074, 11026796603253320333, null, null, 1488344793354086187 }),
            (typeof(float), (float)0.7107176F),
            (typeof(float?), (float?)0.1908861F),
            (typeof(float[]), new float[] { 0.1317251F, 0.2090623F, 0.9146225F, 0.4848171F, 0.1515551F, 0.3601714F, 0.429855F, 0.4041181F, 0.3114151F, 0.2518592F }),
            (typeof(float?[]), new float?[] { null, null, null, null, 0.4423443F, 0.5539647F, 0.3589965F, null, 0.3533548F, 0.6066683F }),
            (typeof(double), (double)0.01666766),
            (typeof(double?), (double?)0.00350381),
            (typeof(double[]), new double[] { 0.24453125, 0.25512222, 0.88517569, 0.35002527, 0.13963387, 0.83326042, 0.72313154, 0.69722354, 0.24013307, 0.25039339 }),
            (typeof(double?[]), new double?[] { null, 0.17365696, null, 0.82468947, 0.08393678, null, null, null, null, null }),
            (typeof(decimal), (decimal)0.78886223341751m),
            (typeof(decimal?), (decimal?)0.392541152607902m),
            (typeof(decimal[]), new decimal[] { 0.132045519599712m, 0.523709212208031m, 0.0723028830589274m, 0.817747185387531m, 0.396536429131653m, 0.313443687890397m, 0.940473652417061m, 0.867141369668367m, 0.924277329316492m, 0.690142766428246m }),
            (typeof(decimal?[]), new decimal?[] { 0.911462185397494m, 0.632112003691547m, null, 0.583210337247332m, 0.254717209960668m, null, null, 0.149107534507805m, 0.394088133421768m, null }),
            (typeof(Guid), (Guid)Guid.NewGuid()),
            (typeof(Guid?), (Guid?)Guid.NewGuid()),
            (typeof(Guid[]), new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }),
            (typeof(Guid?[]), new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), null }),
            (typeof(DateTime), (DateTime)DateTime.Now.AddSeconds(14678885)),
            (typeof(DateTime?), (DateTime?)DateTime.Now.AddSeconds(-327482035)),
            (typeof(DateTime[]), new DateTime[] { DateTime.Now.AddSeconds(130786847), DateTime.Now.AddSeconds(15898689), DateTime.Now.AddSeconds(249212674), DateTime.Now.AddSeconds(-156980052), DateTime.Now.AddSeconds(-125439486), DateTime.Now.AddSeconds(109782582), DateTime.Now.AddSeconds(18283314), DateTime.Now.AddSeconds(275548838), DateTime.Now.AddSeconds(159167791), DateTime.Now.AddSeconds(-280493979) }),
            (typeof(DateTime?[]), new DateTime?[] { null, null, null, null, DateTime.Now.AddSeconds(-54152234), null, null, DateTime.Now.AddSeconds(97747187), DateTime.Now.AddSeconds(76844432), null }),
        };
    }
}
