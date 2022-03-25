// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.ExceptionShouldOnlyHaveOneConstructor;

public class UnitTests : CodeFixVerifier
{
    [Fact]
    public void WithoutConstructor()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {
                    }
                }       
            ";

        VerifyCSharpDiagnostic(content);
    }

    [Fact]
    public void WithSingleConstructor()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {
                        public SomethingWentWrong()
                        {
                        }
                    }
                }       
            ";

        VerifyCSharpDiagnostic(content);
    }

    [Fact]
    public void WithMultipleConstructors()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {
                        public SomethingWentWrong()
                        {
                        }

                        public SomethingWentWrong(string something)
                        {
                        }
                    }
                }       
            ";

        var firstFailure = new DiagnosticResult
        {
            Id = Analyzer.Rule.Id,
            Message = (string)Analyzer.Rule.MessageFormat,
            Severity = Analyzer.Rule.DefaultSeverity,
            Locations = new[]
            {
                    new DiagnosticResultLocation("Test0.cs", 8, 25)
                }
        };

        var secondFailure = new DiagnosticResult
        {
            Id = Analyzer.Rule.Id,
            Message = (string)Analyzer.Rule.MessageFormat,
            Severity = Analyzer.Rule.DefaultSeverity,
            Locations = new[]
            {
                    new DiagnosticResultLocation("Test0.cs", 12, 25)
                }
        };

        VerifyCSharpDiagnostic(content, firstFailure, secondFailure);
    }

    protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
    {
        return new Analyzer();
    }
}
