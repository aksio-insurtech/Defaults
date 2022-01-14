namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Finalizers
{
    /// <summary>
    /// Represents a <see cref="ElementsMustAppearInTheCorrectOrderAnalyzer"/> that requires finalizers in a specific order.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : ElementsMustAppearInTheCorrectOrderAnalyzer
    {
        /// <inheritdoc/>
        public override DiagnosticDescriptor Rule => new(
            id: "AS0014",
            title: "FinalizerElementsMustAppearInTheCorrectOrder",
            messageFormat: "Finalizers must after fields/properties/delegates/events/constructors and before indexers/methods",
            category: "Exceptions",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: null,
            helpLinkUri: string.Empty,
            customTags: Array.Empty<string>()
        );

        /// <inheritdoc/>
        protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.DestructorDeclaration;
    }
}