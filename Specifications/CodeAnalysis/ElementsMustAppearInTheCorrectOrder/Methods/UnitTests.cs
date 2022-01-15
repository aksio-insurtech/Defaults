namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Methods
{
    public class UnitTests : CodeFixVerifier
    {
        [Fact]
        public void CorrectOrder()
        {
            VerifyCSharpDiagnostic(Common.ValidOrder);
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
            var analyzer = new Analyzer();
            return new DiagnosticResult
            {
                Id = analyzer.Rule.Id,
                Message = (string)analyzer.Rule.MessageFormat,
                Severity = analyzer.Rule.DefaultSeverity,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", failLine, 21) }
            };
        }
    }
}