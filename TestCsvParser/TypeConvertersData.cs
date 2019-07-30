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
            (typeof(string), (string)"CthEIhx8o2"),
            (typeof(string[]), new string[] { "xFm'un07ev", "YJh,$1L8#X", "e0uA;oTV\"m", "sy0RngO0Un", "CdgDD\"LsGe", "W,,RY#imCG", "Uf::DwfQYn", "mhngUAUq##", "cyOlQ39i'q", "%yxOTumoAd" }),
            (typeof(char), (char)'d'),
            (typeof(char?), (char?)'2'),
            (typeof(char[]), new char[] { 'U', 'L', 'w', ';', 'D', 'Y', 's', '9', '9', 'c' }),
            (typeof(char?[]), new char?[] { null, 'y', null, 'R', 't', null, 'C', null, 'B', 'h' }),
            (typeof(bool), (bool)false),
            (typeof(bool?), (bool?)null),
            (typeof(bool[]), new bool[] { true, false, false, true, true, true, true, false, false, false }),
            (typeof(bool?[]), new bool?[] { null, false, null, true, false, null, null, true, false, null }),
            (typeof(byte), (byte)0),
            (typeof(byte?), (byte?)187),
            (typeof(byte[]), new byte[] { 226, 150, 74, 147, 166, 33, 118, 202, 208, 136 }),
            (typeof(byte?[]), new byte?[] { 118, null, 66, 186, 113, null, null, null, 141, null }),
            (typeof(sbyte), (sbyte)-124),
            (typeof(sbyte?), (sbyte?)77),
            (typeof(sbyte[]), new sbyte[] { -44, -99, 81, -121, 74, -116, 78, 61, -18, -23 }),
            (typeof(sbyte?[]), new sbyte?[] { null, null, -110, null, null, -16, null, 95, 125, null }),
            (typeof(short), (short)-15841),
            (typeof(short?), (short?)12617),
            (typeof(short[]), new short[] { -4896, -19620, 22652, -29591, -11163, -11651, 23072, -26481, 20998, 21985 }),
            (typeof(short?[]), new short?[] { 20830, 6260, -2909, -27678, null, null, null, null, -6443, null }),
            (typeof(ushort), (ushort)45487),
            (typeof(ushort?), (ushort?)35853),
            (typeof(ushort[]), new ushort[] { 50235, 14663, 17794, 35629, 58419, 3612, 5165, 35371, 33236, 34825 }),
            (typeof(ushort?[]), new ushort?[] { 30152, 53158, 10134, 46785, null, 14918, 20605, 36413, null, 37731 }),
            (typeof(int), (int)-816079939),
            (typeof(int?), (int?)null),
            (typeof(int[]), new int[] { 421733219, 2038097917, -648960129, 652260305, -1616817899, -461232173, -1366855036, 1301975936, 1202303928, -2114496689 }),
            (typeof(int?[]), new int?[] { 895741156, null, null, null, null, null, null, 995242170, null, null }),
            (typeof(uint), (uint)3275535084),
            (typeof(uint?), (uint?)null),
            (typeof(uint[]), new uint[] { 217146227, 1104822614, 3511114978, 96601181, 2899988808, 2143664653, 3022999029, 980229104, 2984019415, 3325484949 }),
            (typeof(uint?[]), new uint?[] { 1781974395, null, null, null, null, 392323235, 376544696, 3327206673, 668530956, null }),
            (typeof(long), (long)7841140640275529833),
            (typeof(long?), (long?)null),
            (typeof(long[]), new long[] { 1640018715990742705, 2920450158913966512, -3440546272092062475, -5393399686590051198, 6707434402298556399, -326734670005039407, -529883065323349767, -1979819757519647082, 6708780455167236011, 5567510364999993959 }),
            (typeof(long?[]), new long?[] { -3930080820786199779, null, -3484088959795144129, null, null, -3505527363788236365, null, 6557539851048349578, 6474631294881085917, -1555325802517199154 }),
            (typeof(ulong), (ulong)16034771076717449931),
            (typeof(ulong?), (ulong?)null),
            (typeof(ulong[]), new ulong[] { 14024237752666486474, 811777168293859885, 10328611346398704430, 7177111355902575858, 14256877803029476261, 10286878804117011863, 3578400574450210351, 17582632504563842976, 10680537522065541623, 11369671191095567385 }),
            (typeof(ulong?[]), new ulong?[] { 9987865332366795376, null, 3453903060365006355, 18007523668263235170, null, 15613041097615765628, null, null, 13432193851565623893, null }),
            (typeof(float), (float)0.6711579F),
            (typeof(float?), (float?)null),
            (typeof(float[]), new float[] { 0.2124667F, 0.1966921F, 0.05988806F, 0.987845F, 0.6821361F, 0.7787361F, 0.4234388F, 0.0784585F, 0.8821264F, 0.1866634F }),
            (typeof(float?[]), new float?[] { 0.8982522F, 0.7668799F, 0.3124983F, null, null, 0.4197188F, 0.9733576F, 0.6321994F, 0.6023122F, null }),
            (typeof(double), (double)0.09735717),
            (typeof(double?), (double?)null),
            (typeof(double[]), new double[] { 0.56170563, 0.41543468, 0.93513814, 0.56505375, 0.05607313, 0.38810477, 0.87377332, 0.89206756, 0.22952879, 0.6119009 }),
            (typeof(double?[]), new double?[] { null, null, null, 0.9794108, null, 0.05329562, 0.77227746, 0.63827794, 0.10570243, 0.86447466 }),
            (typeof(decimal), (decimal)0.871095393258657m),
            (typeof(decimal?), (decimal?)0.654781929522186m),
            (typeof(decimal[]), new decimal[] { 0.667826322683984m, 0.295322307523024m, 0.74810641852585m, 0.964440135734361m, 0.889828535676854m, 0.0633319961202014m, 0.235180909389249m, 0.0389756914409696m, 0.382755537229942m, 0.277977259027761m }),
            (typeof(decimal?[]), new decimal?[] { 0.977439159982576m, 0.975476394861693m, null, null, null, 0.239149879309884m, 0.567197057682647m, 0.391472407333307m, null, null }),
            (typeof(Guid), (Guid)Guid.NewGuid()),
            (typeof(Guid?), (Guid?)null),
            (typeof(Guid[]), new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }),
            (typeof(Guid?[]), new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), null, null, null, Guid.NewGuid(), Guid.NewGuid() }),
            (typeof(DateTime), (DateTime)DateTime.Now.AddSeconds(-79100156)),
            (typeof(DateTime?), (DateTime?)null),
            (typeof(DateTime[]), new DateTime[] { DateTime.Now.AddSeconds(-105047671), DateTime.Now.AddSeconds(-342198230), DateTime.Now.AddSeconds(192887979), DateTime.Now.AddSeconds(-88202627), DateTime.Now.AddSeconds(-67510084), DateTime.Now.AddSeconds(97065840), DateTime.Now.AddSeconds(-274267519), DateTime.Now.AddSeconds(163954181), DateTime.Now.AddSeconds(301219283), DateTime.Now.AddSeconds(-177080511) }),
            (typeof(DateTime?[]), new DateTime?[] { null, null, null, DateTime.Now.AddSeconds(-76494257), null, null, null, DateTime.Now.AddSeconds(-66361438), null, null }),
        };
    }
}
