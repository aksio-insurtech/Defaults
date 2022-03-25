// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable SA1629, SA1116, SA1117, CA2000, SA1124, SA1515, SA1202, SA1513, CA1825, SA1005, SA1202, SA1028, SA1202, CA1829, CA1815

namespace Aksio.CodeAnalysis;

/// <summary>
/// Struct that stores information about a Diagnostic appearing in a source
/// </summary>
public struct DiagnosticResult
{
    DiagnosticResultLocation[] locations;

    public DiagnosticResultLocation[] Locations
    {
        get
        {
            if (locations == null)
            {
                locations = new DiagnosticResultLocation[] { };
            }
            return locations;
        }

        set
        {
            locations = value;
        }
    }

    public DiagnosticSeverity Severity { get; set; }

    public string Id { get; set; }

    public string Message { get; set; }

    public string Path => Locations.Length > 0 ? Locations[0].Path : "";

    public int Line => Locations.Length > 0 ? Locations[0].Line : -1;

    public int Column => Locations.Length > 0 ? Locations[0].Column : -1;
}
