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
            (typeof(string), (string)"8Q3y%%Qqz7"),
            (typeof(string[]), new string[] { "851wXaRVq:", "qeoH2w4hpy", "D8E%S4HSEj", "bL66Pyh2qw", "?hjOFdyv@Y", "Q6AB3 13ep", "T RHmfv4'D", "PC;\"8sZ?',", "FiX2SrK5CP", "$1XBd;Qbyx" }),
            (typeof(char), (char)'A'),
            (typeof(char?), (char?)null),
            (typeof(char[]), new char[] { '"', 'k', 'r', 'y', '@', 'B', '2', '7', '8', 'V' }),
            (typeof(char?[]), new char?[] { null, null, null, null, null, null, '6', null, null, null }),
            (typeof(bool), (bool)false),
            (typeof(bool?), (bool?)null),
            (typeof(bool[]), new bool[] { true, true, true, true, false, true, true, false, false, true }),
            (typeof(bool?[]), new bool?[] { null, null, false, null, null, false, null, true, true, null }),
            (typeof(byte), (byte)156),
            (typeof(byte?), (byte?)null),
            (typeof(byte[]), new byte[] { 122, 104, 121, 231, 213, 235, 6, 131, 118, 45 }),
            (typeof(byte?[]), new byte?[] { 84, null, null, null, 21, 147, 218, null, 37, 14 }),
            (typeof(sbyte), (sbyte)98),
            (typeof(sbyte?), (sbyte?)null),
            (typeof(sbyte[]), new sbyte[] { 48, -74, 57, -56, 13, -49, 51, -21, -65, -86 }),
            (typeof(sbyte?[]), new sbyte?[] { -31, 104, null, 12, -38, null, -58, null, null, -120 }),
            (typeof(short), (short)-22258),
            (typeof(short?), (short?)29122),
            (typeof(short[]), new short[] { 2371, 472, -22132, 23138, 2023, 4518, 1427, -14075, -23751, 20558 }),
            (typeof(short?[]), new short?[] { 32215, -8383, null, 10216, -22164, -21161, null, -31873, -27154, 30686 }),
            (typeof(ushort), (ushort)35418),
            (typeof(ushort?), (ushort?)25146),
            (typeof(ushort[]), new ushort[] { 65370, 44839, 20201, 898, 34970, 3356, 64508, 34706, 44336, 43269 }),
            (typeof(ushort?[]), new ushort?[] { 7919, 34909, null, 33207, 12206, null, null, null, null, null }),
            (typeof(int), (int)1745497306),
            (typeof(int?), (int?)-223214879),
            (typeof(int[]), new int[] { -641422342, 657135241, -1689500692, 583183885, 1496092696, -1935601285, -738722098, -1041388297, -2112433016, 318895888 }),
            (typeof(int?[]), new int?[] { -1678823254, null, 1816479869, null, null, 923396471, null, null, 1446981720, 1083652371 }),
            (typeof(uint), (uint)3638583560),
            (typeof(uint?), (uint?)3687504319),
            (typeof(uint[]), new uint[] { 726682501, 3078474308, 1001490370, 200985281, 759854703, 512686716, 1511245735, 917579988, 3117822486, 3521159626 }),
            (typeof(uint?[]), new uint?[] { null, 3299180989, 1305273751, 2338299023, null, 582450307, 628807065, 3460382963, null, null }),
            (typeof(long), (long)-924448555573113328),
            (typeof(long?), (long?)null),
            (typeof(long[]), new long[] { 790769871525554012, -815034529701450824, -3596272395992784998, 931308526648247398, 669872857448726370, -6654531472238957853, -2034313269470768576, 927254678994259343, -4889748655793869890, 931116947253642579 }),
            (typeof(long?[]), new long?[] { 8842871578373419910, 9100540995373122049, -4640767958686375049, -5683621196345335996, null, null, null, -4204987531505123782, null, null }),
            (typeof(ulong), (ulong)11713660149828893140),
            (typeof(ulong?), (ulong?)5495638573902403444),
            (typeof(ulong[]), new ulong[] { 9527779768075935855, 4558665489661131650, 2827229012126792440, 10883392660493817267, 13806964695942809346, 15124065541961463545, 1908856567205864404, 17189662149153860260, 14267740737199635825, 4156102272065781598 }),
            (typeof(ulong?[]), new ulong?[] { null, null, 7848730042478172626, 18183959871372147429, null, 7345706144429678901, null, 10940855922853876092, 11971659546309364163, 15117848271400572911 }),
            (typeof(float), (float)0.3587325F),
            (typeof(float?), (float?)0.8278135F),
            (typeof(float[]), new float[] { 0.5570758F, 0.7936968F, 0.8432884F, 0.08027557F, 0.2607249F, 0.3263974F, 0.1164879F, 0.2763191F, 0.7554342F, 0.5559844F }),
            (typeof(float?[]), new float?[] { 0.4617017F, 0.1148363F, 0.9303576F, null, null, null, 0.7428161F, null, null, null }),
            (typeof(double), (double)0.3414045),
            (typeof(double?), (double?)null),
            (typeof(double[]), new double[] { 0.76221833, 0.84135761, 0.35769014, 0.71331424, 0.88959836, 0.96941813, 0.92390092, 0.37143384, 0.08294164, 0.7371138 }),
            (typeof(double?[]), new double?[] { null, 0.19665464, null, null, null, null, 0.72928439, 0.28796697, null, null }),
            (typeof(decimal), (decimal)0.614653583902239m),
            (typeof(decimal?), (decimal?)0.416554881453772m),
            (typeof(decimal[]), new decimal[] { 0.304790604070197m, 0.351819631807422m, 0.227237104544061m, 0.871691064849352m, 0.401666380652071m, 0.16747482687583m, 0.383336914416094m, 0.612477067677526m, 0.252620386543041m, 0.748224169829965m }),
            (typeof(decimal?[]), new decimal?[] { 0.371012617075356m, null, null, 0.202090021782597m, null, 0.66899929180229m, null, 0.858861260050378m, 0.375861096370901m, null }),
            (typeof(Guid), (Guid)Guid.NewGuid()),
            (typeof(Guid?), (Guid?)null),
            (typeof(Guid[]), new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }),
            (typeof(Guid?[]), new Guid?[] { null, Guid.NewGuid(), null, null, null, null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }),
            (typeof(DateTime), (DateTime)DateTime.Now.AddSeconds(-206736638)),
            (typeof(DateTime?), (DateTime?)null),
            (typeof(DateTime[]), new DateTime[] { DateTime.Now.AddSeconds(-176176579), DateTime.Now.AddSeconds(-269202042), DateTime.Now.AddSeconds(300726205), DateTime.Now.AddSeconds(54107636), DateTime.Now.AddSeconds(39102336), DateTime.Now.AddSeconds(-140403643), DateTime.Now.AddSeconds(149746959), DateTime.Now.AddSeconds(312243389), DateTime.Now.AddSeconds(14795474), DateTime.Now.AddSeconds(-51357900) }),
            (typeof(DateTime?[]), new DateTime?[] { null, DateTime.Now.AddSeconds(287639950), DateTime.Now.AddSeconds(-26573491), DateTime.Now.AddSeconds(3820366), DateTime.Now.AddSeconds(-166145815), null, null, null, null, DateTime.Now.AddSeconds(-242363347) }),
        };
    }
}
