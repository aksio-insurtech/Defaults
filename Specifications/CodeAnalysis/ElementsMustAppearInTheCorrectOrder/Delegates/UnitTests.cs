namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Delegates
{
    public class UnitTests: CodeFixVerifier
    {
        [Fact]
        public void CorrectOrder()
        {
            VerifyCSharpDiagnostic(Common.ValidOrder);
        }

        [Fact]
        public void DelegatesBeforeFields()
        {
            const string content = @"
                class Blabla
                {
                    public delegate void SomethingHappenedEventHandler(object sender, object args);

                    public int _teller = 0;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void DelegatesBeforeProperties()
        {
            const string content = @"
                class Blabla
                {
                    public delegate void SomethingHappenedEventHandler(object sender, object args);

                    public int Teller { get; private set; }
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void DelegatesAfterEvents()
        {
            const string content = @"
                class Blabla
                {
                    public event EventHandler SomethingHappened;

                    public delegate void SomethingHappenedEventHandler(object sender, object args);
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure(6));
        }

        [Fact]
        public void DelegatesAfterConstructor()
        {
            const string content = @"
                class Blabla
                {
                    public Blabla() { }
                    
                    public delegate void SomethingHappenedEventHandler(object sender, object args);
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure(6));
        }

        [Fact]
        public void DelegatesAfterFinalizer()
        {
            const string content = @"
                class Blabla
                {
                    ~Blabla() { }

                    public delegate void SomethingHappenedEventHandler(object sender, object args);
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure(6));
        }

        [Fact]
        public void DelegatesAfterIndexers()
        {
            const string content = @"
                class Blabla
                {
                    public int this[int i] => 42;

                    public delegate void SomethingHappenedEventHandler(object sender, object args);
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure(6));
        }

        [Fact]
        public void DelegatesAfterMethods()
        {
            const string content = @"
                class Blabla
                {
                    void ØkTeller() => throw new NotImplementedException();

                    public delegate void SomethingHappenedEventHandler(object sender, object args);
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure(6));
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