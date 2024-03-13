// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace BuildConverters
{
    public enum TypeVariation
    {
        Standard,
        Nullable,
        Array,
        NullableArray,
    }

    internal class TemplateSection
    {
        private static readonly string SectionTag = "{@Section}";

        public string Text { get; set; }
        public string Type { get; private set; }
        public TypeVariation Variation { get; private set; }
        public string Placeholder { get; private set; }

        public TemplateSection(string[] args)
        {
            if (args.Length != 3)
                throw new Exception($"Number of arguments to '{SectionTag}' should be 3. Found {args.Length}.");

            Type = args[0].Trim();
            if (!Enum.TryParse(args[1].Trim(), out TypeVariation mode))
                throw new Exception($"Template mode '{args[1]}' is not a recognized mode.");
            Variation = mode;
            Placeholder = args[2].Trim();
        }
    }
}
