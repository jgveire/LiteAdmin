﻿namespace LiteAdmin.Core
{
    using System;

    public interface IColumn
    {
        Type DataType { get; }
        object DefaultValue { get; }
        bool IsNullable { get; }
        int MaxLength { get; }
        string Name { get; }
    }
}
