namespace Aksio.CodeAnalysis.SealedNotAllowed
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
             id: "DL0003",
             title: "SealedNotAllowed",
             messageFormat: "The keyword 'sealed' unnecessarily locks down code from inheritance - very rare occasions is this a problem",
             category: "Openness",
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
                AnalyzeSyntaxNode,
                ImmutableArray.Create(
                    SyntaxKind.ClassDeclaration));
        }

        void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = context.Node as ClassDeclarationSyntax;
            var sealedKeyword = classDeclaration.Modifiers.SingleOrDefault(_ => _.IsKind(SyntaxKind.SealedKeyword));
            if (sealedKeyword == default) return;

            var diagnostic = Diagnostic.Create(Rule, sealedKeyword.GetLocation());
            context.ReportDiagnostic(diagnostic);
        }
    }
}
