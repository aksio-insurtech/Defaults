namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Fields
{
    /// <summary>
    /// Represents a <see cref="ElementsMustAppearInTheCorrectOrderAnalyzer"/> that requires fields in a specific order.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : ElementsMustAppearInTheCorrectOrderAnalyzer
    {
        /// <inheritdoc/>
        public override DiagnosticDescriptor Rule => new(
            id: "AS0009",
            title: "FieldsMustAppearInTheCorrectOrder",
            messageFormat: "Fields must come before delegates/events/constructors/finalizers/indexers/methods",
            category: "Exceptions",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: null,
            helpLinkUri: string.Empty,
            customTags: Array.Empty<string>()
        );

        /// <summary>
        /// Gets the element kind we are checking the ordering for.
        /// </summary>
        protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.FieldDeclaration;
    }
}