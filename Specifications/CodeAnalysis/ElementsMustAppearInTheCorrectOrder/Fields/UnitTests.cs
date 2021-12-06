namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Fields
{
    public class UnitTests: CodeFixVerifier
    {
        [Fact]
        public void FieldsBeforeConstructor()
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
        public void FieldsAfterDelegates()
        {
            const string content = @"
                class Blabla
                {
                    public delegate void SomethingHappenedEventHandler(object sender, object args);

                    string _someBacking;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailures());
        }

        [Fact]
        public void FieldsAfterEvents()
        {
            const string content = @"
                class Blabla
                {
                    public event EventHandler SomethingHappened;

                    string _someBacking;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailures());
        }

        [Fact]
        public void FieldsAfterConstructor()
        {
            const string content = @"
                class Blabla
                {
                    public Blabla() {}

                    string _someBacking;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailures());
        }

        [Fact]
        public void FieldsAfterFinalizer()
        {
            const string content = @"
                class Blabla
                {
                    ~Blabla() {}

                    string _someBacking;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailures());
        }

        [Fact]
        public void FieldsAfterIndexers()
        {
            const string content = @"
                class Blabla
                {
                    public int this[int i] => Teller;

                    string _someBacking;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailures());
        }

        [Fact]
        public void FieldsAfterMethods()
        {
            const string content = @"
                class Blabla
                {
                    void ØkTeller() => ++Teller;

                    string _someBacking;
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

        DiagnosticResult[] GetExpectedFailures(int firstFailLine = 6)
        {
            var analyzer = new Analyzer();
            var firstFailure = new DiagnosticResult
            {
                Id = analyzer.Rule.Id,
                Message = (string)analyzer.Rule.MessageFormat,
                Severity = analyzer.Rule.DefaultSeverity,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", firstFailLine, 21) }
            };

            return new[] { firstFailure };
        }
    }
}