// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace BuildConverters
{
    public enum TemplateMode
    {
        Standard,
        Nullable,
        Array,
        NullableArray,
    }

    internal class Section
    {
        private static readonly string SectionTag = "{@Section}";

        public string Text { get; set; }
        public string Type { get; private set; }
        public TemplateMode Mode { get; private set; }
        public string Placeholder { get; private set; }

        public Section(string[] args)
        {
            if (args.Length != 3)
                throw new Exception($"Number of arguments to '{SectionTag}' should be 3. Found {args.Length}.");

            Type = args[0].Trim();
            if (!Enum.TryParse(args[1].Trim(), out TemplateMode mode))
                throw new Exception($"Template mode '{args[1]}' is not a recognized mode.");
            Mode = mode;
            Placeholder = args[2].Trim();
        }
    }
}
