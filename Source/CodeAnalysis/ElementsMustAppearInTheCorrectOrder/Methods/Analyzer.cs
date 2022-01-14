namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Methods
{
    /// <summary>
    /// Represents a <see cref="ElementsMustAppearInTheCorrectOrderAnalyzer"/> that requires methods in a specific order.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : ElementsMustAppearInTheCorrectOrderAnalyzer
    {
        /// <inheritdoc/>
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

        /// <summary>
        /// The element kind we are checking the ordering for.
        /// </summary>
        protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.MethodDeclaration;
    }
}