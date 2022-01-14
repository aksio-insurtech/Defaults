namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Indexers
{
    /// <summary>
    /// Represents a <see cref="ElementsMustAppearInTheCorrectOrderAnalyzer"/> that requires indexers in a specific order.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : ElementsMustAppearInTheCorrectOrderAnalyzer
    {
        /// <inheritdoc/>
        public override DiagnosticDescriptor Rule => new(
            id: "AS0015",
            title: "IndexerElementsMustAppearInTheCorrectOrder",
            messageFormat: "Indexers must after fields/properties/delegates/events/constructors/finalizers and before methods",
            category: "Exceptions",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: null,
            helpLinkUri: string.Empty,
            customTags: Array.Empty<string>()
        );

        /// <inheritdoc/>
        protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.IndexerDeclaration;
    }
}