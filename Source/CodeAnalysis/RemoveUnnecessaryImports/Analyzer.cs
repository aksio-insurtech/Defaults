#pragma warning disable RS1025, RS1026, RS1029

namespace Aksio.CodeAnalysis.RemoveUnnecessaryImports
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the unnecessary import statements.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        public static readonly DiagnosticDescriptor Rule = new (
             id: "CS8019",
             title: "RemoveUnnecessaryImports",
             messageFormat: "Unnecessary using directive",
             category: "Style",
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

            context.RegisterSemanticModelAction(AnalyzeSemanticModel);
        }

        void AnalyzeSemanticModel(SemanticModelAnalysisContext context)
        {
            var cancellationToken = context.CancellationToken;
            var model = context.SemanticModel;
            var root = model.SyntaxTree.GetRoot(cancellationToken);

            var diagnostics = model.GetDiagnostics(cancellationToken: cancellationToken);
            if (!diagnostics.Any()) return;

            foreach (var diagnostic in diagnostics)
            {
                if (diagnostic.Id == "CS8019" && root.FindNode(diagnostic.Location.SourceSpan) is UsingDirectiveSyntax node)
                {
                    var diagnosticWrapper = Diagnostic.Create(Rule, node.GetLocation());
                    context.ReportDiagnostic(diagnosticWrapper);
                }
            }
        }
    }
}