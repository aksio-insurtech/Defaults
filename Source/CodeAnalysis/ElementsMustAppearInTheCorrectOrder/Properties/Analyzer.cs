namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Properties
{
    /// <summary>
    /// Represents a <see cref="ElementsMustAppearInTheCorrectOrderAnalyzer"/> that requires properties in a specific order.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : ElementsMustAppearInTheCorrectOrderAnalyzer
    {
        /// <inheritdoc/>
        public override DiagnosticDescriptor Rule => new(
            id: "AS0010",
            title: "PropertiesMustAppearInTheCorrectOrder",
            messageFormat: "Properties must come before delegates, events, constructors, finalizers, indexers and methods",
            category: "Exceptions",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: null,
            helpLinkUri: string.Empty,
            customTags: Array.Empty<string>()
        );

        /// <inheritdoc/>
        protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.PropertyDeclaration;
    }
}