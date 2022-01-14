namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Constructors
{
    /// <summary>
    /// Represents a <see cref="ElementsMustAppearInTheCorrectOrderAnalyzer"/> that requires constructor in a specific order.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : ElementsMustAppearInTheCorrectOrderAnalyzer
    {
        /// <inheritdoc/>
        public override DiagnosticDescriptor Rule => new(
            id: "AS0013",
            title: "ConstructorsMustAppearInTheCorrectOrder",
            messageFormat: "Constructors must after fields/properties/delegates/events, and before finalizers/indexers/methods",
            category: "Exceptions",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: null,
            helpLinkUri: string.Empty,
            customTags: Array.Empty<string>()
        );

        /// <inheritdoc/>
        protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.ConstructorDeclaration;
    }
}