namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Events
{
    public class UnitTests: CodeFixVerifier
    {
        [Fact]
        public void CorrectOrder()
        {
            VerifyCSharpDiagnostic(Common.ValidOrder);
        }

        [Fact]
        public void EventsBeforeFields()
        {
            const string content = @"
                class Blabla
                {
                    public event EventHandler SomethingHappened;

                    public int _teller = 0;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void EventsBeforeProperties()
        {
            const string content = @"
                class Blabla
                {
                    public event EventHandler SomethingHappened;

                    public int Teller { get; private set; }
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void EventsBeforeDelegates()
        {
            const string content = @"
                class Blabla
                {
                    public event EventHandler SomethingHappened;

                    public delegate void SomethingHappenedEventHandler(object sender, object args);
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void EventsAfterConstructor()
        {
            const string content = @"
                class Blabla
                {
                    public Blabla() { }
                    
                    public event EventHandler SomethingHappened;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure(6));
        }

        [Fact]
        public void EventsAfterFinalizer()
        {
            const string content = @"
                class Blabla
                {
                    ~Blabla() { }

                    public event EventHandler SomethingHappened;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure(6));
        }

        [Fact]
        public void EventsAfterIndexers()
        {
            const string content = @"
                class Blabla
                {
                    public int this[int i] => 42;

                    public event EventHandler SomethingHappened;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure(6));
        }

        [Fact]
        public void EventsAfterMethods()
        {
            const string content = @"
                class Blabla
                {
                    void ØkTeller() => throw new NotImplementedException();

                    public event EventHandler SomethingHappened;
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