namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Constructors
{
#error meh Fields/Properties
#error meh Delegates
#error meh Events
#error meh Constructors
#error meh Finalizers(Destructors)
#error meh Indexers
#error meh Methods
#error meh 
#error meh * de under her ønsker vi vel ikke i klasser?
#error meh Interfaces
#error meh Enums
#error meh Structs
#error meh Classes* (inkl.records)

#error testingen og dokumentasjon må ordnes, akkurat nå er det mange kopier av samme fil! Kan jeg generalisere her?

    public class UnitTests: CodeFixVerifier
    {
        [Fact]
        public void CorrectOrder()
        {
            const string content = @"
                class Blabla
                {
                    string _someBacking;
                    public string BackedField => _someBacking.Replace(""a"", ""b"");
                    public int Teller { get; private set; }
                    public delegate void SomethingHappenedEventHandler(object sender, object args);
                    public event EventHandler SomethingHappened;

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
        public void ConstructorBeforeFields()
        {
            const string content = @"
                class Blabla
                {
                    public Blabla() { }

                    public int _teller = 0;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void ConstructorBeforeProperties()
        {
            const string content = @"
                class Blabla
                {
                    public Blabla() { }

                    public int Teller { get; private set; }
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void ConstructorBeforeEvents()
        {
            const string content = @"
                class Blabla
                {
                    public Blabla() { }

                    public event EventHandler SomethingHappened;
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure());
        }

        [Fact]
        public void ConstructorAfterFinalizer()
        {
            const string content = @"
                class Blabla
                {
                    ~Blabla() { }

                    public Blabla() { }
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure(6));
        }

        [Fact]
        public void ConstructorAfterIndexers()
        {
            const string content = @"
                class Blabla
                {
                    public int this[int i] => 42;

                    public Blabla() { }
                }
            ";

            VerifyCSharpDiagnostic(content, GetExpectedFailure(6));
        }

        [Fact]
        public void ConstructorAfterMethods()
        {
            const string content = @"
                class Blabla
                {
                    void ØkTeller() => throw new NotImplementedException();

                    public Blabla() { }
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