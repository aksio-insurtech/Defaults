namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Delegates
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of the 'sealed' keyword.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
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
            id: "AS0011",
            title: "DelegateElementsMustAppearInTheCorrectOrder",
            messageFormat: "Delegates must after fields/properties and before events/constructors/finalizers/indexers/methods",
            category: "Exceptions",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: null,
            helpLinkUri: string.Empty,
            customTags: Array.Empty<string>()
        );
    }
}