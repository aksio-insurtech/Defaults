namespace Aksio.CodeAnalysis.SerializableNotAllowed
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of <see cref="SerializableAttribute"/>.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        public static readonly DiagnosticDescriptor Rule = new (
             id: "AS0001",
             title: "SerializableNotAllowed",
             messageFormat: "The use of the [Serializable] attribute is considered an old .NET desktop framework practice for interop/marshalling between AppDomains and is not necessary",
             category: "Legacy",
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
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.Attribute);
        }

        void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            var attribute = context.Node as AttributeSyntax;
            if (attribute?.Name.ToString() == "Serializable")
            {
                var diagnostic = Diagnostic.Create(Rule, context.Node.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
