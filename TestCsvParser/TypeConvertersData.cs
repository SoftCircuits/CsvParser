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
            (typeof(string), (string)"9zN%Zoqe8l"),
            (typeof(string[]), new string[] { "b c%4vblKv", "AZL#Jf;8:h", "hDPuJ:87xh", "j'cFu#49uZ", "\"FohcknR?!", "y5mbDY75,?", ".r.0MH$GYM", "xDNoncl4Rw", "aq!260cABW", "M %4mZR,VU" }),
            (typeof(char), (char)'j'),
            (typeof(char?), (char?)null),
            (typeof(char[]), new char[] { ':', ',', '4', 't', 'V', 'f', 'H', 'D', 'o', '5' }),
            (typeof(char?[]), new char?[] { 'b', null, null, ';', 'S', null, 'h', '2', 'B', null }),
            (typeof(bool), (bool)true),
            (typeof(bool?), (bool?)null),
            (typeof(bool[]), new bool[] { false, false, false, true, true, false, true, false, true, true }),
            (typeof(bool?[]), new bool?[] { true, false, false, false, true, true, false, true, false, false }),
            (typeof(byte), (byte)238),
            (typeof(byte?), (byte?)null),
            (typeof(byte[]), new byte[] { 32, 78, 208, 241, 218, 72, 253, 27, 192, 111 }),
            (typeof(byte?[]), new byte?[] { 209, null, 42, null, null, 2, 52, null, null, null }),
            (typeof(sbyte), (sbyte)-65),
            (typeof(sbyte?), (sbyte?)null),
            (typeof(sbyte[]), new sbyte[] { -60, 14, 52, -120, -44, 97, -34, -103, -67, -39 }),
            (typeof(sbyte?[]), new sbyte?[] { null, -64, null, null, -80, null, null, null, null, -104 }),
            (typeof(short), (short)-28204),
            (typeof(short?), (short?)null),
            (typeof(short[]), new short[] { -13738, -12348, 25465, -16851, -13056, -2594, -3854, 4948, 27432, 28611 }),
            (typeof(short?[]), new short?[] { 4243, null, null, null, null, -12589, -3647, null, null, -13448 }),
            (typeof(ushort), (ushort)4682),
            (typeof(ushort?), (ushort?)248),
            (typeof(ushort[]), new ushort[] { 3361, 16672, 52876, 18872, 26485, 55139, 13908, 39522, 40763, 45424 }),
            (typeof(ushort?[]), new ushort?[] { 8308, 26019, 56563, null, 6781, null, null, null, 61657, null }),
            (typeof(int), (int)899625961),
            (typeof(int?), (int?)null),
            (typeof(int[]), new int[] { 599456746, -1851592613, -1934112560, -261617343, 690572527, 506651323, -27317320, -1120111618, 2099459156, -2081113522 }),
            (typeof(int?[]), new int?[] { -430978903, 1211271840, null, null, null, null, null, 795581385, null, -108241517 }),
            (typeof(uint), (uint)2530456649),
            (typeof(uint?), (uint?)251358035),
            (typeof(uint[]), new uint[] { 72815505, 2647793763, 2495650042, 540192597, 4152212774, 436703117, 1006492493, 1541163448, 4087861372, 2005503068 }),
            (typeof(uint?[]), new uint?[] { null, 2888350141, 3648430340, null, null, 1024197248, null, null, 3165709719, 1040745495 }),
            (typeof(long), (long)5173179954128953076),
            (typeof(long?), (long?)-1159801424051184178),
            (typeof(long[]), new long[] { -2028425485773176385, -2808067275585465724, -1818130896818254487, -3881399820508262894, -8633574412200369235, 1258326695761318190, 890266393608754646, 321539250736151869, 507064906468592176, -7462390416976022755 }),
            (typeof(long?[]), new long?[] { -3904558548434571962, 4187176993443844693, null, null, null, null, null, -461804783056683892, null, null }),
            (typeof(ulong), (ulong)16721625927019067232),
            (typeof(ulong?), (ulong?)null),
            (typeof(ulong[]), new ulong[] { 3215169128111526812, 5587462868937597203, 18045055714029315642, 8779512009833115174, 5867288026429617745, 5800834934202452087, 12769462197251488099, 18123724270703470951, 8065339355437808650, 8167267042000602938 }),
            (typeof(ulong?[]), new ulong?[] { null, null, null, 8524060926557706019, null, null, null, 10153109016502064323, 10480043377233380740, null }),
            (typeof(float), (float)0.585166F),
            (typeof(float?), (float?)null),
            (typeof(float[]), new float[] { 0.5762754F, 0.8177987F, 0.9755152F, 0.1645326F, 0.587315F, 0.3652382F, 0.6775184F, 0.05985177F, 0.9172666F, 0.2083651F }),
            (typeof(float?[]), new float?[] { 0.2388613F, 0.2771752F, 0.2650009F, 0.6938699F, 0.5428373F, 0.8836665F, 0.1819181F, 0.08569411F, null, null }),
            (typeof(double), (double)0.28136068),
            (typeof(double?), (double?)null),
            (typeof(double[]), new double[] { 0.85636947, 0.57049559, 0.96675197, 0.62545153, 0.85637452, 0.05928564, 0.68843523, 0.9783771, 0.34622172, 0.05292539 }),
            (typeof(double?[]), new double?[] { 0.50371801, null, 0.97143267, null, null, null, null, null, null, 0.08615886 }),
            (typeof(decimal), (decimal)0.944451941151382m),
            (typeof(decimal?), (decimal?)0.602073340957087m),
            (typeof(decimal[]), new decimal[] { 0.792393362984244m, 0.488261872198555m, 0.91849576678057m, 0.117391764706649m, 0.105689893991542m, 0.984492992975047m, 0.20675060116069m, 0.386967915290486m, 0.723364654333966m, 0.0484396121690234m }),
            (typeof(decimal?[]), new decimal?[] { 0.217260683522215m, null, null, null, null, null, 0.653911919171881m, null, 0.74321089486741m, null }),
            (typeof(Guid), (Guid)Guid.NewGuid()),
            (typeof(Guid?), (Guid?)Guid.NewGuid()),
            (typeof(Guid[]), new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }),
            (typeof(Guid?[]), new Guid?[] { null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, Guid.NewGuid(), Guid.NewGuid() }),
            (typeof(DateTime), (DateTime)DateTime.Now.AddSeconds(-7102591)),
            (typeof(DateTime?), (DateTime?)DateTime.Now.AddSeconds(-167945136)),
            (typeof(DateTime[]), new DateTime[] { DateTime.Now.AddSeconds(119190930), DateTime.Now.AddSeconds(-15134269), DateTime.Now.AddSeconds(-242068258), DateTime.Now.AddSeconds(88866432), DateTime.Now.AddSeconds(-194414262), DateTime.Now.AddSeconds(-178310505), DateTime.Now.AddSeconds(-341035826), DateTime.Now.AddSeconds(-145521500), DateTime.Now.AddSeconds(180126162), DateTime.Now.AddSeconds(-223845860) }),
            (typeof(DateTime?[]), new DateTime?[] { DateTime.Now.AddSeconds(-153533297), null, null, null, DateTime.Now.AddSeconds(182784615), DateTime.Now.AddSeconds(-55149009), DateTime.Now.AddSeconds(-92448093), DateTime.Now.AddSeconds(-265265976), DateTime.Now.AddSeconds(38844239), null }),
        };
    }
}
