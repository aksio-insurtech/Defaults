// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.ExceptionDescriptionShouldFollowStandard;

public class UnitTests : CodeFixVerifier
{
    [Fact]
    public void WithDescriptionFollowingStandard()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    /// <summary>
                    /// Exception that gets thrown when
                    /// </summary>
                    public class SomethingWentWrong : Exception
                    {

                    }
                }       
            ";

        VerifyCSharpDiagnostic(content);
    }

    [Fact]
    public void WithoutDescriptionFollowingStandard()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    /// <summary>
                    /// Some kinda exception
                    /// </summary>
                    public class SomethingWentWrong : Exception
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
                    new DiagnosticResultLocation("Test0.cs", 7, 24)
                }
        };

        VerifyCSharpDiagnostic(content, expected);
    }

    protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
    {
        return new Analyzer();
    }
}
