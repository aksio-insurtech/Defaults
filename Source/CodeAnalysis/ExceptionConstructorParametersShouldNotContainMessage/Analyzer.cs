namespace Aksio.CodeAnalysis.ExceptionConstructorParametersShouldNotContainMessage
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
             id: "DL0006",
             title: "ExceptionConstructorParametersShouldNotContainMessage",
             messageFormat: "An argument of an exception with the name 'message' in it indicates its a generic exception and output string ownership is wrong",
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
                var constructors = classDeclaration.Members
                                                        .Where(_ => _.IsKind(SyntaxKind.ConstructorDeclaration))
                                                        .Select(_ => _ as ConstructorDeclarationSyntax);

                foreach (var constructor in constructors)
                {
                    var wronglyNamedParameters = constructor.ParameterList.Parameters
                                                    .Where(_ =>
                                                        _.Type.ToString().EndsWith("string", StringComparison.InvariantCultureIgnoreCase) &&
                                                        _.Identifier.Text.Contains("message", StringComparison.InvariantCultureIgnoreCase));
                    foreach (var parameter in wronglyNamedParameters)
                    {
                        var diagnostic = Diagnostic.Create(Rule, parameter.Identifier.GetLocation());
                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }
        }
    }
}
