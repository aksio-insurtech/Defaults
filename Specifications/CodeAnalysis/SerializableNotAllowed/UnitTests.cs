// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.SerializableNotAllowed;

public class UnitTests : CodeFixVerifier
{
    [Fact]
    public void WithoutSerializable()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class MyClass
                    {

                    }
                }       
            ";

        VerifyCSharpDiagnostic(content);
    }

    [Fact]
    public void WithSerializable()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    [Serializable]
                    public class MyClass
                    {

                    }
                }       
            ";

        var expected = new DiagnosticResult
        {
            Id = Analyzer.Rule.Id,
            Message = (string)Analyzer.Rule.MessageFormat,
            Severity = Analyzer.Rule.DefaultSeverity,
            Locations = new[]
            {
                    new DiagnosticResultLocation("Test0.cs", 6, 22)
                }
        };

        VerifyCSharpDiagnostic(content, expected);
    }

    protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
    {
        return new Analyzer();
    }
}
