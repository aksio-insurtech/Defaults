namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Fields
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
        protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.FieldDeclaration;

        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        public override DiagnosticDescriptor Rule => new(
            id: "AS0009",
            title: "FieldsMustAppearInTheCorrectOrder",
            messageFormat: "Fields must before delegates/events/constructors/finalizers/indexers/methods",
            category: "Exceptions",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: null,
            helpLinkUri: string.Empty,
            customTags: Array.Empty<string>()
        );
    }
}