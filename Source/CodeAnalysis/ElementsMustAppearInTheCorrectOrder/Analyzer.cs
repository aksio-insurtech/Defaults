namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of the 'sealed' keyword.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public abstract class ElementsMustAppearInTheCorrectOrderAnalyzer: DiagnosticAnalyzer
    {
        abstract SyntaxKind KindToCheckFor;

        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        static virtual readonly DiagnosticDescriptor Rule;

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(ValidateRule, ImmutableArray.Create(SyntaxKind.ClassDeclaration));
        }

        void ValidateRule(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is not ClassDeclarationSyntax classDeclaration)
            {
                return;
            }

            var kindToCheckFor = SyntaxKind.DelegateDeclaration;
            var elementToCheckFor = KindToElementOrder(kindToCheckFor);

            var currentKindList = classDeclaration.Members.Where(_ => _.Kind() == kindToCheckFor)
                .Select((m, pos) => (Member: m, Position: pos))
                .ToList();

            // Each instance of the element before the end of the types that should be before it generates an error
            var lastBeforeElementPosition = LastBeforeElementPosition(classDeclaration, elementToCheckFor);
            foreach (var (member, _) in currentKindList.Where(_ => _.Position < lastBeforeElementPosition))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, member.GetLocation()));
            }

            // Each instance of the element after the start of the types that should be after it generates an error
            var firstAfterElementPosition = FirstAfterElementPosition(classDeclaration, elementToCheckFor);
            foreach (var (member, _) in currentKindList.Where(_ => _.Position > firstAfterElementPosition))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, member.GetLocation()));
            }
        }

        int LastBeforeElementPosition(ClassDeclarationSyntax classDeclaration, ElementOrderList elementOrderToCheckFor)
        {
            var kindAndPosition = classDeclaration.Members
                .Select((m, pos) => (ElementOrder: KindToElementOrder(m.Kind()), Position: pos))
                .Where(_ => _.ElementOrder != elementOrderToCheckFor)
                .ToList();

            return kindAndPosition.Where(_ => _.ElementOrder > elementOrderToCheckFor).Min(_ => _.Position);
        }

        int FirstAfterElementPosition(ClassDeclarationSyntax classDeclaration, ElementOrderList elementOrderToCheckFor)
        {
            var kindAndPosition = classDeclaration.Members
                .Select((m, pos) => (ElementOrder: KindToElementOrder(m.Kind()), Position: pos))
                .Where(_ => _.ElementOrder != elementOrderToCheckFor)
                .ToList();

            return kindAndPosition.Where(_ => _.ElementOrder < elementOrderToCheckFor).Max(_ => _.Position);
        }

        ElementOrderList KindToElementOrder(SyntaxKind syntaxKind)
        {
            return syntaxKind switch
            {
                SyntaxKind.FieldDeclaration => ElementOrderList.FieldsAndProperties,
                SyntaxKind.PropertyDeclaration => ElementOrderList.FieldsAndProperties,
                SyntaxKind.DelegateDeclaration => ElementOrderList.Delegates,
                SyntaxKind.EventFieldDeclaration => ElementOrderList.Events,
                SyntaxKind.EventDeclaration => ElementOrderList.Events,
                SyntaxKind.ConstructorDeclaration => ElementOrderList.Constructors,
                SyntaxKind.DestructorDeclaration => ElementOrderList.Destructors,
                SyntaxKind.IndexerDeclaration => ElementOrderList.Indexers,
                SyntaxKind.MethodDeclaration => ElementOrderList.Methods,
                _ => ElementOrderList.Undefined
            };
        }
    }
}