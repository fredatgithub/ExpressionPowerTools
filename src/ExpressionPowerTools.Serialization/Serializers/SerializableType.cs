﻿// Copyright (c) Jeremy Likness. All rights reserved.
// Licensed under the MIT License. See LICENSE in the repository root for license information.

using System;

namespace ExpressionPowerTools.Serialization.Serializers
{
    /// <summary>
    /// Represents a serializable type. Handles recursive generic arguments.
    /// </summary>
    [Serializable]
    public struct SerializableType
    {
        /// <summary>
        /// Gets or sets the full name of the type.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the list of generic type arguments for the type.
        /// </summary>
        public SerializableType[] GenericTypeArguments { get; set; }
    }
}