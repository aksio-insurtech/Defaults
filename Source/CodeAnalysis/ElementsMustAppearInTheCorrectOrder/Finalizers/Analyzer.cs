namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Finalizers
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of the 'sealed' keyword.
    /// </summary>
    public class Analyzer: ElementsMustAppearInTheCorrectOrderAnalyzer
    {
        /// <summary>
        /// The element kind we are checking the ordering for.
        /// </summary>
        protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.DelegateDeclaration;

        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
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
    }
}