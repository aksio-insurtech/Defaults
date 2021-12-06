namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Constructors
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
        protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.ConstructorDeclaration;

        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
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
    }
}