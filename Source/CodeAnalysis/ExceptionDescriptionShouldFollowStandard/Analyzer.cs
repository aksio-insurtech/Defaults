namespace Aksio.CodeAnalysis.ExceptionDescriptionShouldFollowStandard
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that requires a specific phrase for Exception API documentation.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        /// <summary>
        /// The expected phrase.
        /// </summary>
        public const string Phrase = "Exception that gets thrown when";

        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        public static readonly DiagnosticDescriptor Rule = new (
             id: "AS0007",
             title: "ExceptionDescriptionShouldFollowStandard",
             messageFormat: $"Exception description for API documentation should start with '{Phrase}'",
             category: "Naming",
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
                foreach (var trivia in classDeclaration.GetLeadingTrivia())
                {
                    if (trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) ||
                        trivia.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia))
                    {
                        var descendants = trivia.GetStructure().DescendantTokens();
                        var summaryTokenAndIndex = descendants
                                                    .Select((token, index) => new { Token = token, Index = index })
                                                    .FirstOrDefault(_ => _.Token.IsKind(SyntaxKind.IdentifierToken) && _.Token.Text.Equals("summary", StringComparison.InvariantCulture));

                        if (summaryTokenAndIndex == default) return;

                        var xmlTextLiteralToken = descendants
                                        .Skip(summaryTokenAndIndex.Index)
                                        .FirstOrDefault(_ => _.IsKind(SyntaxKind.XmlTextLiteralToken));

                        if (xmlTextLiteralToken == default) return;

                        if (!xmlTextLiteralToken.Text.Trim().StartsWith(Phrase, StringComparison.InvariantCulture))
                        {
                            var diagnostic = Diagnostic.Create(Rule, xmlTextLiteralToken.GetLocation());
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }
    }
}
