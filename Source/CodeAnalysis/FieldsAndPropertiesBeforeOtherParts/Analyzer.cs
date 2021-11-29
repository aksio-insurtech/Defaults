namespace Aksio.CodeAnalysis.FieldsAndPropertiesBeforeOtherParts
{
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
            id: "AS0009",
            title: "FieldsAndPropertiesBeforeOtherParts",
            messageFormat: "Fields and properties must before delegates, events, constructors, finalizers, indexers and methods",
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
            context.RegisterSyntaxNodeAction(ValidateRule, ImmutableArray.Create(SyntaxKind.ClassDeclaration));
        }

        void ValidateRule(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = context.Node as ClassDeclarationSyntax;

            var typesThatShouldBeDeclaredAfterFieldsAndProperties = new List<SyntaxKind>
            {
                SyntaxKind.DelegateDeclaration,
                SyntaxKind.EventFieldDeclaration,
                SyntaxKind.EventDeclaration,
                SyntaxKind.ConstructorDeclaration,
                SyntaxKind.DestructorDeclaration,
                SyntaxKind.IndexerDeclaration,
                SyntaxKind.MethodDeclaration
            };

            var firstOtherPosition = classDeclaration.Members.Select((m, pos) => (Member: m, Position: pos))
                .FirstOrDefault(_ => typesThatShouldBeDeclaredAfterFieldsAndProperties.Contains(_.Member.Kind()))
                .Position;

            foreach (var property in classDeclaration.Members.Skip(firstOtherPosition)
                         .Where(_ => _.IsKind(SyntaxKind.PropertyDeclaration)))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, property.GetLocation()));
            }

            foreach (var field in classDeclaration.Members.Skip(firstOtherPosition)
                         .Where(_ => _.IsKind(SyntaxKind.FieldDeclaration)))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, field.GetLocation()));
            }
        }
    }
}