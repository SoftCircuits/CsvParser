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
        public string Placeholder { get; private set; }
        public string Type { get; private set; }
        public TemplateMode Mode { get; private set; }

        public Section(string[] args)
        {
            if (args.Length != 3)
                throw new Exception($"Number of arguments to '{SectionTag}' should be 3. Found {args.Length}.");

            Placeholder = args[0].Trim();
            Type = args[1].Trim();

            if (!Enum.TryParse(args[2].Trim(), out TemplateMode mode))
                throw new Exception($"Template mode '{args[2]}' is not a recognized mode.");
            Mode = mode;
        }
    }
}
