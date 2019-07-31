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
            (typeof(string), (string)"2gdKGt'6;s"),
            (typeof(string[]), new string[] { ".ZTEsw?jBs", "xRu,@g'2z,", "NeuICscI#n", "DfR1jMPFHr", "9,Ikqk9pOw", "0NHCfg1LC ", "U4nkpS 'b5", "R9uIBYWKoo", "69\"W9?YPWD", "iEDIWFYu@Y" }),
            (typeof(char), (char)'Q'),
            (typeof(char?), (char?)'U'),
            (typeof(char[]), new char[] { 'q', 'W', 'j', 'l', '0', 'L', '\'', '$', 'A', 'u' }),
            (typeof(char?[]), new char?[] { null, 'J', 'a', 'W', null, '#', null, ' ', 'r', 'Y' }),
            (typeof(bool), (bool)true),
            (typeof(bool?), (bool?)false),
            (typeof(bool[]), new bool[] { false, false, false, false, true, false, true, true, true, true }),
            (typeof(bool?[]), new bool?[] { false, null, null, null, false, null, null, null, null, true }),
            (typeof(byte), (byte)22),
            (typeof(byte?), (byte?)null),
            (typeof(byte[]), new byte[] { 26, 123, 91, 17, 47, 181, 174, 190, 121, 61 }),
            (typeof(byte?[]), new byte?[] { 60, null, null, 81, 234, null, null, 122, null, null }),
            (typeof(sbyte), (sbyte)123),
            (typeof(sbyte?), (sbyte?)-106),
            (typeof(sbyte[]), new sbyte[] { -103, 23, -63, 124, 22, -52, 47, -49, -120, -101 }),
            (typeof(sbyte?[]), new sbyte?[] { null, null, null, 56, -86, -69, -39, -109, 112, -92 }),
            (typeof(short), (short)-22795),
            (typeof(short?), (short?)-22275),
            (typeof(short[]), new short[] { -7222, -11432, -32134, -21514, -23878, 6140, -24950, -32562, -16731, -18989 }),
            (typeof(short?[]), new short?[] { null, null, -14521, null, 19669, -30713, -13868, null, -3761, -17344 }),
            (typeof(ushort), (ushort)4394),
            (typeof(ushort?), (ushort?)null),
            (typeof(ushort[]), new ushort[] { 56091, 59955, 51417, 13325, 58755, 21783, 52938, 50528, 58271, 12187 }),
            (typeof(ushort?[]), new ushort?[] { null, null, 38462, null, null, 49442, 26315, 50990, null, null }),
            (typeof(int), (int)852753653),
            (typeof(int?), (int?)null),
            (typeof(int[]), new int[] { -1528580662, 738477909, 1417696671, -898524343, 289345279, 1818386499, 31679234, 2049437920, 397594740, -2089048839 }),
            (typeof(int?[]), new int?[] { -1776826917, 524931609, null, null, 1382647585, null, null, -1370815901, null, null }),
            (typeof(uint), (uint)3329407967),
            (typeof(uint?), (uint?)2239492802),
            (typeof(uint[]), new uint[] { 2785425297, 20619169, 1705616380, 1481811228, 117063842, 1541564637, 3529470808, 1350666952, 1336698830, 3958992154 }),
            (typeof(uint?[]), new uint?[] { 905326689, 1830753987, null, 682063352, null, null, null, null, null, 1495429433 }),
            (typeof(long), (long)4707423350908916626),
            (typeof(long?), (long?)-8979951111139428553),
            (typeof(long[]), new long[] { -1056514891554703911, 2877199767649092895, 4289708647302108122, -3204278088702710322, -7978793060336371788, 6427884363160604151, -4811268879609954039, 63944515168615724, 4119595816188820476, 6878665288007076814 }),
            (typeof(long?[]), new long?[] { 4450509232353910772, 8712717461980135169, null, null, null, null, null, 4547437674153688022, null, 6510353311077383719 }),
            (typeof(ulong), (ulong)4500763256299587463),
            (typeof(ulong?), (ulong?)8257011937960906434),
            (typeof(ulong[]), new ulong[] { 16647907630064180898, 12828167851228211700, 15932696022294678893, 16914200801014568396, 14393105802690875318, 17526474735970483924, 6112391457488674759, 11881521824346054786, 15580993248791667054, 4752240908918742926 }),
            (typeof(ulong?[]), new ulong?[] { null, null, 13163173039299072355, 5211997489412098659, null, null, null, 2911070714918463077, 6629881942558857713, 18427818679905821157 }),
            (typeof(float), (float)0.9311963F),
            (typeof(float?), (float?)null),
            (typeof(float[]), new float[] { 0.8397835F, 0.6195897F, 0.6591357F, 0.08543294F, 0.7795863F, 0.9814671F, 0.3652757F, 0.6495939F, 0.01480083F, 0.7505451F }),
            (typeof(float?[]), new float?[] { 0.04725996F, null, null, 0.004487643F, 0.3195684F, null, 0.2307771F, 0.07762374F, 0.2240213F, null }),
            (typeof(double), (double)0.15717002),
            (typeof(double?), (double?)0.01368775),
            (typeof(double[]), new double[] { 0.39624933, 0.95154269, 0.22662825, 0.58909164, 0.80807276, 0.89739918, 0.63404452, 0.99230192, 0.15248474, 0.45375909 }),
            (typeof(double?[]), new double?[] { null, null, 0.9073727, null, 0.37049671, null, null, 0.72889676, 0.81392752, null }),
            (typeof(decimal), (decimal)0.0315667111573586m),
            (typeof(decimal?), (decimal?)0.570939674773691m),
            (typeof(decimal[]), new decimal[] { 0.209563108724339m, 0.326703857316963m, 0.842516636402587m, 0.574360266595315m, 0.826370823116214m, 0.913410787430318m, 0.591996436282991m, 0.0533801885570307m, 0.531763281920814m, 0.757928947339733m }),
            (typeof(decimal?[]), new decimal?[] { 0.494950563877332m, 0.986708741163234m, null, 0.809956161216812m, null, null, 0.487591173261214m, null, null, null }),
            (typeof(Guid), (Guid)Guid.NewGuid()),
            (typeof(Guid?), (Guid?)null),
            (typeof(Guid[]), new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }),
            (typeof(Guid?[]), new Guid?[] { Guid.NewGuid(), null, Guid.NewGuid(), null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null }),
            (typeof(DateTime), (DateTime)DateTime.Now.AddSeconds(-91981)),
            (typeof(DateTime?), (DateTime?)null),
            (typeof(DateTime[]), new DateTime[] { DateTime.Now.AddSeconds(-200013124), DateTime.Now.AddSeconds(-293429738), DateTime.Now.AddSeconds(-302131070), DateTime.Now.AddSeconds(-15005230), DateTime.Now.AddSeconds(121472269), DateTime.Now.AddSeconds(-129519103), DateTime.Now.AddSeconds(-208212357), DateTime.Now.AddSeconds(75911259), DateTime.Now.AddSeconds(166948933), DateTime.Now.AddSeconds(136395407) }),
            (typeof(DateTime?[]), new DateTime?[] { DateTime.Now.AddSeconds(-237285326), DateTime.Now.AddSeconds(221262296), null, null, DateTime.Now.AddSeconds(168670674), DateTime.Now.AddSeconds(-249928790), DateTime.Now.AddSeconds(20819676), null, DateTime.Now.AddSeconds(-298648707), null }),
        };
    }
}
