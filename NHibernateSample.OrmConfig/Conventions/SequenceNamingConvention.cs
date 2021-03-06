﻿using System;
using System.Text.RegularExpressions;

namespace NHibernateSample.OrmConfig.Conventions
{
    public class SequenceNamingConvention
    {
        private const string PascalCaseRegex = @"(?<!_)([A-Z])";

        public static string GetSequenceName(Type type)
        {
            var typeName = type.Name.Replace("Map", "");            
            var sequenceName = Regex.Replace(typeName, PascalCaseRegex, "_$1").Remove(0, 1).ToLower();
            return $"sq_{sequenceName}";
        }
    }
}
