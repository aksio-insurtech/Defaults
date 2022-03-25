// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.PrivateNotAllowed;

public class UnitTests : CodeFixVerifier
{
    [Fact]
    public void PublicClassWithPublicMethodsPropertiesAndFieldsAllAllowed()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class MyClass
                    {
                        public int MyField;

                        public int MyProperty { get; set; }

                        public void DoStuff()
                        {
                        }
                    }
                }       
            ";

        VerifyCSharpDiagnostic(content);
    }

    [Fact]
    public void PrivatePropertySetterAllowed()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class MyClass
                    {
                        public int MyProperty { get; private set; }
                    }
                }       
            ";

        VerifyCSharpDiagnostic(content);
    }

    [Fact]
    public void PrivateClassNotAllowed()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    private class MyClass
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
                    new DiagnosticResultLocation("Test0.cs", 6, 21)
                }
        };

        VerifyCSharpDiagnostic(content, expected);
    }

    [Fact]
    public void PrivateMethodNotAllowed()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class MyClass
                    {
                        private void DoStuff()
                        {

                        }
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
                    new DiagnosticResultLocation("Test0.cs", 8, 25)
                }
        };

        VerifyCSharpDiagnostic(content, expected);
    }

    [Fact]
    public void PrivateFieldNotAllowed()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class MyClass
                    {
                        private int MyField;
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
                    new DiagnosticResultLocation("Test0.cs", 8, 25)
                }
        };

        VerifyCSharpDiagnostic(content, expected);
    }

    [Fact]
    public void PrivatePropertyNotAllowed()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class MyClass
                    {
                        private int MyProperty { get; set; };
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
                    new DiagnosticResultLocation("Test0.cs", 8, 25)
                }
        };

        VerifyCSharpDiagnostic(content, expected);
    }

    [Fact]
    public void PrivateEventNotAllowed()
    {
        const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class MyClass
                    {
                        private event MyEvent;
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
                    new DiagnosticResultLocation("Test0.cs", 8, 25)
                }
        };

        VerifyCSharpDiagnostic(content, expected);
    }

    protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
    {
        return new Analyzer();
    }
}
