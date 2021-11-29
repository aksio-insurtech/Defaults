namespace Aksio.CodeAnalysis.MethodsAfterOtherParts
{
    public class UnitTests: CodeFixVerifier
    {
        [Fact]
        public void MethodsAfterOtherParts()
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
                        _someBacking = $""{ØkTeller()}"";
                    }

                    void ØkTeller() => ++Teller;
                }   
            ";

            VerifyCSharpDiagnostic(content);
        }

        [Fact]
        public void MethodsBeforeFields()
        {
            const string content = @"
                class Blabla
                {
                    void ØkTeller() => ++_teller;

                    public int _teller = 0;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void MethodsBeforeProperties()
        {
            const string content = @"
                class Blabla
                {
                    void ØkTeller() => 42;

                    public int Teller { get; private set; }
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void MethodsBeforeEvents()
        {
            const string content = @"
                class Blabla
                {
                    void ØkTeller() => 42;

                    public event EventHandler SomethingHappened;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void MethodsBeforeDelegates()
        {
            const string content = @"
                class Blabla
                {
                    void ØkTeller() => 42;

                    public delegate void SomethingHappenedEventHandler(object sender, object args);
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void MethodsBeforeConstructor()
        {
            const string content = @"
                class Blabla
                {
                    void ØkTeller() => 42;

                    public Blabla() { }
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void MethodsBeforeFinalizer()
        {
            const string content = @"
                class Blabla
                {
                    void ØkTeller() => 42;

                    ~Blabla() { }
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void MethodsBeforeIndexers()
        {
            const string content = @"
                class Blabla
                {
                    void ØkTeller() => ++Teller;

                    public int this[int i] => 42;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
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

        DiagnosticResult GetExpectedFailure(int failLine = 4)
        {
            return new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", failLine, 21) }
            };
        }
    }
}