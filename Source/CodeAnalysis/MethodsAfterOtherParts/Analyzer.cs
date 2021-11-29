namespace Aksio.CodeAnalysis.MethodsAfterOtherParts
{

#error må lage nye analyzere og tester, der vi blander BeforeOther og AfterOther, så vi kan si at den og den typen skal være mellom definerte sett av deklarasjoner

    // TESTKLASSEN SOM ER OK
    //    class Blabla
    //    {
    //        string _someBacking;
    //        public string BackedField => _someBacking.Replace(""a"", ""b"");
    //        public int Teller { get; private set; }
    //        public event EventHandler SomethingHappened;
    //        public delegate void SomethingHappenedEventHandler(object sender, object args);
    //
    //        public Blabla()
    //        {
    //            Teller = 0;
    //            _someBacking = ""meh"";
    //        }
    //
    //        ~Blabla()
    //        {
    //            Teller = int.MinValue;
    //        }
    //
    //        public int this[int i] => Teller;
    //
    //        public void GjørNoeRart()
    //        {
    //            _someBacking = $""{ ØkTeller()}
    //            "";
    //        }
    //        void ØkTeller() => ++Teller;
    //    }


    // rekkefølgen vi skal teste for
    //     Fields/Properties
    //     Delegates
    //     Events
    //     Constructors
    //     Finalizers(Destructors)
    //     Indexers
    //     Methods
    // 
    //         * de under her ønsker vi vel ikke i klasser?
    //         Interfaces
    //     Enums
    //         Structs
    //     Classes* (inkl.records)



    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of the 'sealed' keyword.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer: DiagnosticAnalyzer
    {
        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        public static readonly DiagnosticDescriptor Rule = new(
            id: "AS0010",
            title: "MethodsAfterConstructor",
            messageFormat: "Methods must be after the constructor(s)",
            category: "Exceptions",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: null,
            helpLinkUri: string.Empty,
            customTags: Array.Empty<string>()
        );

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

            context.RegisterSyntaxNodeAction(
                FieldsAndPropertiesBeforeConstructor,
                ImmutableArray.Create(SyntaxKind.ClassDeclaration)
            );
        }

        void FieldsAndPropertiesBeforeConstructor(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = context.Node as ClassDeclarationSyntax;

            var typesThatShouldBeDeclaredAfterFieldsAndProperties = new List<SyntaxKind>
            {
                SyntaxKind.FieldDeclaration,
                SyntaxKind.PropertyDeclaration,
                SyntaxKind.DelegateDeclaration,
                SyntaxKind.EventFieldDeclaration,
                SyntaxKind.EventDeclaration,
                SyntaxKind.ConstructorDeclaration,
                SyntaxKind.DestructorDeclaration,
                SyntaxKind.IndexerDeclaration
            };

            var firstOtherPosition = classDeclaration.Members.Select((m, pos) => (Member: m, Position: pos))
                .LastOrDefault(_ => typesThatShouldBeDeclaredAfterFieldsAndProperties.Contains(_.Member.Kind()))
                .Position;

            foreach (var property in classDeclaration.Members.Take(firstOtherPosition)
                         .Where(_ => _.IsKind(SyntaxKind.MethodDeclaration)))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, property.GetLocation()));
            }
        }
    }
}