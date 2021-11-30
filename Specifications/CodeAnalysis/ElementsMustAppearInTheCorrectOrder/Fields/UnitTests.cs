namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Fields
{
    public class UnitTests: CodeFixVerifier
    {
        [Fact]
        public void FieldsAndPropertiesBeforeConstructor()
        {
            const string content = @"
                class Blabla
                {
                    string _someBacking;
                    public string BackedField => _someBacking.Replace(""a"", ""b"");
                    public int Teller { get; private set; }
                    public event EventHandler SomethingHappened;
                    public delegate void SomethingHappenedEventHandler(object sender, object args);

                    public Blabla()
                    {
                        Teller = 0;
                        _someBacking = ""meh"";
                    }

                    ~Blabla()
                    {
                        Teller = int.MinValue;
                    }

                    public int this[int i] => Teller;

                    public void GjørNoeRart()
                    {
                        _someBacking = $""{ ØkTeller()}
                        "";
                    }
                    void ØkTeller() => ++Teller;
                }
            ";

            VerifyCSharpDiagnostic(content);
        }

        [Fact]
        public void FieldsAndPropertiesAfterEvents()
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
        public void FieldsAndPropertiesAfterDelegates()
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
        public void FieldsAndPropertiesAfterConstructor()
        {
            const string content = @"
                class Blabla
                {
                    public Blabla()
                    {
                        Teller = 0;
                        _someBacking = ""meh"";
                    }

                    string _someBacking;
                    public string BackedField => _someBacking.Replace(""a"", ""b"");
                    public int Teller { get; private set; }
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailures(10, 11, 12));
        }

        [Fact]
        public void FieldsAndPropertiesAfterIndexers()
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
        public void FieldsAndPropertiesAfterMethods()
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

        DiagnosticResult[] GetExpectedFailures(int firstFailLine = 6, int secondFailLine = 7, int thirdFailLine = 8)
        {
            var firstFailure = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", firstFailLine, 21) }
            };

            var secondFailure = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", secondFailLine, 21) }
            };

            var thirdFailure = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", thirdFailLine, 21) }
            };

            return new[] { firstFailure, secondFailure, thirdFailure };
        }
    }
}