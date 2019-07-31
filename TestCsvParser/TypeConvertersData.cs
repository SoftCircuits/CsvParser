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
            (typeof(string), (string)"G.ktoTQolm"),
            (typeof(string[]), new string[] { "X?kDIn7uSM", "Qx#FlpvP@:", "@jJo:8:%6S", "c klrGkRt%", "O@Vkof;fH%", "uBtuq.3@MW", "w9,A7QMile", "Ees#z NvAi", "!i;7vac;@3", "aoFXZcHVzX" }),
            (typeof(char), (char)'u'),
            (typeof(char?), (char?)'"'),
            (typeof(char[]), new char[] { '5', 'e', 'k', 'f', '9', 'n', 'z', 'D', 'B', 'Z' }),
            (typeof(char?[]), new char?[] { 'y', null, null, null, 'q', ',', '1', 'E', 'v', 'X' }),
            (typeof(bool), (bool)false),
            (typeof(bool?), (bool?)null),
            (typeof(bool[]), new bool[] { false, false, false, true, true, false, true, true, false, false }),
            (typeof(bool?[]), new bool?[] { false, null, null, false, true, null, false, true, null, false }),
            (typeof(byte), (byte)140),
            (typeof(byte?), (byte?)null),
            (typeof(byte[]), new byte[] { 76, 20, 229, 51, 11, 219, 169, 101, 97, 207 }),
            (typeof(byte?[]), new byte?[] { null, null, null, null, null, null, null, 218, 140, null }),
            (typeof(sbyte), (sbyte)-17),
            (typeof(sbyte?), (sbyte?)-105),
            (typeof(sbyte[]), new sbyte[] { 35, -48, 44, 58, 118, -108, -81, 47, -2, -21 }),
            (typeof(sbyte?[]), new sbyte?[] { null, null, 69, null, -128, null, 40, null, null, -87 }),
            (typeof(short), (short)-21431),
            (typeof(short?), (short?)-21970),
            (typeof(short[]), new short[] { -12638, 7126, 32107, -3758, 14729, 27325, 13668, -7256, -26182, -18151 }),
            (typeof(short?[]), new short?[] { null, null, null, null, null, 27291, 18647, 11330, 24399, 385 }),
            (typeof(ushort), (ushort)59368),
            (typeof(ushort?), (ushort?)16615),
            (typeof(ushort[]), new ushort[] { 64545, 46973, 44830, 10078, 32069, 38376, 59959, 46940, 22554, 3473 }),
            (typeof(ushort?[]), new ushort?[] { 38292, null, null, null, null, 44712, null, 15261, 20219, null }),
            (typeof(int), (int)-1896724598),
            (typeof(int?), (int?)null),
            (typeof(int[]), new int[] { 713185608, 600683313, -2004419112, -1229236220, 1526075142, -106806240, -148522446, 1672762889, 1259750936, -530078968 }),
            (typeof(int?[]), new int?[] { -48197198, 2008308900, null, null, null, -2001337395, 450268790, null, 1102933794, 363291566 }),
            (typeof(uint), (uint)463795365),
            (typeof(uint?), (uint?)null),
            (typeof(uint[]), new uint[] { 604168461, 1249621422, 884078093, 589504070, 1729855818, 2385455855, 2473318646, 2089094557, 4132136909, 2627736744 }),
            (typeof(uint?[]), new uint?[] { null, 3097070759, 1543345438, null, null, null, null, null, null, null }),
            (typeof(long), (long)2331757504480316597),
            (typeof(long?), (long?)null),
            (typeof(long[]), new long[] { -3949700335694036518, -3097133786759062514, -5032287215716591012, 5367123063620196766, 6318966133289612117, -4072673445708110765, 2434368101521606625, 3876674538883972119, -2189601184877309408, -6294367263937060247 }),
            (typeof(long?[]), new long?[] { -6072990280402430436, 3537750553972915187, null, null, 7836701894275265715, -7628861093228106290, null, -2926952703926147925, -6400933780942392430, -4142345956753063669 }),
            (typeof(ulong), (ulong)12389655739463255248),
            (typeof(ulong?), (ulong?)null),
            (typeof(ulong[]), new ulong[] { 15177830893018219266, 13014494235454727226, 4418492842652570042, 9652022814951621699, 6195335646326882641, 9098046443541349711, 6815109546701669498, 6572177182097084594, 8845751518043447281, 9382088022343390359 }),
            (typeof(ulong?[]), new ulong?[] { null, 4858590694360078359, null, 16825710506844190316, 16821714313647315638, null, null, 5909107508663964124, null, 18055529104357187615 }),
            (typeof(float), (float)0.4525963F),
            (typeof(float?), (float?)null),
            (typeof(float[]), new float[] { 0.082617F, 0.1949135F, 0.1207908F, 0.3303964F, 0.1621427F, 0.5128187F, 0.406981F, 0.6217217F, 0.8782194F, 0.1747007F }),
            (typeof(float?[]), new float?[] { null, 0.8087016F, 0.3633276F, null, 0.09385387F, null, null, 0.06398255F, 0.5515006F, 0.5570879F }),
            (typeof(double), (double)0.38750396),
            (typeof(double?), (double?)0.8795522),
            (typeof(double[]), new double[] { 0.00745974, 0.26341828, 0.39985666, 0.71361447, 0.58459503, 0.5002051, 0.61088499, 0.82719991, 0.5700627, 0.13259998 }),
            (typeof(double?[]), new double?[] { 0.98223503, 0.94453018, 0.14225111, null, null, 0.48817326, 0.29712772, null, null, 0.92797417 }),
            (typeof(decimal), (decimal)0.0172072760840912m),
            (typeof(decimal?), (decimal?)0.122389577851812m),
            (typeof(decimal[]), new decimal[] { 0.331033324976933m, 0.0983533305573991m, 0.244846773913524m, 0.601154125575514m, 0.277385964653169m, 0.406822616889525m, 0.540731963953344m, 0.159165459293484m, 0.319135564528003m, 0.350334417703717m }),
            (typeof(decimal?[]), new decimal?[] { null, 0.74448295205109m, 0.0240967706889365m, null, 0.465495508846592m, null, 0.423269347950476m, null, 0.144569429636267m, 0.452338761860662m }),
            (typeof(Guid), (Guid)Guid.NewGuid()),
            (typeof(Guid?), (Guid?)Guid.NewGuid()),
            (typeof(Guid[]), new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }),
            (typeof(Guid?[]), new Guid?[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null }),
            (typeof(DateTime), (DateTime)DateTime.Now.AddSeconds(-79538062)),
            (typeof(DateTime?), (DateTime?)DateTime.Now.AddSeconds(320281461)),
            (typeof(DateTime[]), new DateTime[] { DateTime.Now.AddSeconds(-106486551), DateTime.Now.AddSeconds(122226813), DateTime.Now.AddSeconds(-258872232), DateTime.Now.AddSeconds(-194185897), DateTime.Now.AddSeconds(223376659), DateTime.Now.AddSeconds(288868401), DateTime.Now.AddSeconds(-214174255), DateTime.Now.AddSeconds(-176497592), DateTime.Now.AddSeconds(-95604031), DateTime.Now.AddSeconds(4015605) }),
            (typeof(DateTime?[]), new DateTime?[] { null, null, null, DateTime.Now.AddSeconds(-30040353), DateTime.Now.AddSeconds(-47880234), DateTime.Now.AddSeconds(234947793), DateTime.Now.AddSeconds(281943980), null, DateTime.Now.AddSeconds(-290799620), null }),
        };
    }
}
