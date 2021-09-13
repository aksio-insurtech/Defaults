namespace Aksio.CodeAnalysis.ExceptionShouldOnlyHaveOneConstructor
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of the 'sealed' keyword.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        public static readonly DiagnosticDescriptor Rule = new (
             id: "AS0005",
             title: "ExceptionShouldOnlyHaveOneConstructor",
             messageFormat: "An exception should not have more than one constructor and typically not a generic one taking a message",
             category: "Exceptions",
             defaultSeverity: DiagnosticSeverity.Error,
             isEnabledByDefault: true,
             description: null,
             helpLinkUri: string.Empty,
             customTags: Array.Empty<string>());

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(
                HandleClassDeclaration,
                ImmutableArray.Create(
                    SyntaxKind.ClassDeclaration));
        }

        void HandleClassDeclaration(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = context.Node as ClassDeclarationSyntax;
            if (classDeclaration?.BaseList == null || classDeclaration?.BaseList?.Types == null) return;

            if (classDeclaration.InheritsASystemException(context.SemanticModel))
            {
                var constructors = classDeclaration.Members.Where(_ => _.IsKind(SyntaxKind.ConstructorDeclaration)).ToArray();
                if (constructors.Length > 1)
                {
                    foreach (var constructor in constructors)
                    {
                        var diagnostic = Diagnostic.Create(Rule, constructor.GetLocation());
                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }
        }
    }
}
