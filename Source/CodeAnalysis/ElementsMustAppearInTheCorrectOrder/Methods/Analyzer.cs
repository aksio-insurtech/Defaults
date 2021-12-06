namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Methods
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of the 'sealed' keyword.
    /// </summary>
    public class Analyzer: ElementsMustAppearInTheCorrectOrderAnalyzer
    {
        /// <summary>
        /// The element kind we are checking the ordering for.
        /// </summary>
        protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.MethodDeclaration;

        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        public override DiagnosticDescriptor Rule => new(
            id: "AS0016",
            title: "MethodElementsMustAppearInTheCorrectOrder",
            messageFormat: "Methods must after fields/properties/delegates/events/constructors/indexers",
            category: "Exceptions",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: null,
            helpLinkUri: string.Empty,
            customTags: Array.Empty<string>()
        );
    }
}