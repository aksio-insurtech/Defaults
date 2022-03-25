// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder;

/// <summary>
/// ElementOrderList.
/// </summary>
public enum ElementOrderList
{
    /// <summary>
    /// Undefined
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// Fields
    /// </summary>
    FieldsAndProperties = 1,

    /// <summary>
    /// Delegates
    /// </summary>
    Delegates = 2,

    /// <summary>
    /// Events
    /// </summary>
    Events = 3,

    /// <summary>
    /// Constructors
    /// </summary>
    Constructors = 4,

    /// <summary>
    /// Destructors
    /// </summary>
    Destructors = 5,

    /// <summary>
    /// Indexers
    /// </summary>
    Indexers = 6,

    /// <summary>
    /// Methods
    /// </summary>
    Methods = 7
}
