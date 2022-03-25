// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Properties;

public class UnitTests : CodeFixVerifier
{
    [Fact]
    public void CorrectOrder()
    {
        VerifyCSharpDiagnostic(Common.ValidOrder);
    }

    [Fact]
    public void PropertiesAfterDelegates()
    {
        const string content = @"
                class Blabla
                {
                    public delegate void SomethingHappenedEventHandler(object sender, object args);

                    string _someBacking;
                    public string BackedField => _someBacking.Replace(""a"", ""b"");
                    public int Teller { get; private set; }
                }
            ";

        VerifyCSharpDiagnostic(content, GetExpectedFailures());
    }

    [Fact]
    public void PropertiesAfterEvents()
    {
        const string content = @"
                class Blabla
                {
                    public event EventHandler SomethingHappened;

                    string _someBacking;
                    public string BackedField => _someBacking.Replace(""a"", ""b"");
                    public int Teller { get; private set; }
                }
            ";

        VerifyCSharpDiagnostic(content, GetExpectedFailures());
    }

    [Fact]
    public void PropertiesAfterConstructor()
    {
        const string content = @"
                class Blabla
                {
                    public Blabla() { }

                    string _someBacking;
                    public string BackedField => _someBacking.Replace(""a"", ""b"");
                    public int Teller { get; private set; }
                }
            ";

        VerifyCSharpDiagnostic(content, GetExpectedFailures());
    }

    [Fact]
    public void PropertiesAfterFinalizer()
    {
        const string content = @"
                class Blabla
                {
                    ~Blabla() { }

                    string _someBacking;
                    public string BackedField => _someBacking.Replace(""a"", ""b"");
                    public int Teller { get; private set; }
                }
            ";

        VerifyCSharpDiagnostic(content, GetExpectedFailures());
    }

    [Fact]
    public void PropertiesAfterIndexers()
    {
        const string content = @"
                class Blabla
                {
                    public int this[int i] => Teller;

                    string _someBacking;
                    public string BackedField => _someBacking.Replace(""a"", ""b"");
                    public int Teller { get; private set; }
                }
            ";

        VerifyCSharpDiagnostic(content, GetExpectedFailures());
    }

    [Fact]
    public void PropertiesAfterMethods()
    {
        const string content = @"
                class Blabla
                {
                    void ØkTeller() => ++Teller;

                    string _someBacking;
                    public string BackedField => _someBacking.Replace(""a"", ""b"");
                    public int Teller { get; private set; }
                }
            ";

        VerifyCSharpDiagnostic(content, GetExpectedFailures());
    }


    [Fact]
    public void AnalyzerDoesNotCrashOnEmptyClass()
    {
        const string content = @"
                class Blabla
                {
                }
            ";

        VerifyCSharpDiagnostic(content);
    }

    protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
    {
        return new Analyzer();
    }

    DiagnosticResult[] GetExpectedFailures(int firstFailLine = 7, int secondFailLine = 8)
    {
        var analyzer = new Analyzer();

        var firstFailure = new DiagnosticResult
        {
            Id = analyzer.Rule.Id,
            Message = (string)analyzer.Rule.MessageFormat,
            Severity = analyzer.Rule.DefaultSeverity,
            Locations = new[] { new DiagnosticResultLocation("Test0.cs", firstFailLine, 21) }
        };

        var secondFailure = new DiagnosticResult
        {
            Id = analyzer.Rule.Id,
            Message = (string)analyzer.Rule.MessageFormat,
            Severity = analyzer.Rule.DefaultSeverity,
            Locations = new[] { new DiagnosticResultLocation("Test0.cs", secondFailLine, 21) }
        };

        return new[] { firstFailure, secondFailure };
    }
}
