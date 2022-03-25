// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.ExceptionShouldNotBeSuffixed;

/// <summary>
/// Represents a <see cref="DiagnosticAnalyzer"/> that does requires one to now add a suffix of 'Exception' to an exception type - indicating possibly bad naming.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Analyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
    /// </summary>
    public static readonly DiagnosticDescriptor Rule = new(
         id: "AS0004",
         title: "ExceptionShouldNotBeSuffixed",
         messageFormat: "The use of the word 'Exception' should not be added as a suffix - create a well understood and self explanatory name for the exception",
         category: "Naming",
         defaultSeverity: DiagnosticSeverity.Error,
         isEnabledByDefault: true,
         description: null,
         helpLinkUri: string.Empty,
         customTags: Array.Empty<string>());

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterSyntaxNodeAction(
            HandleClassDeclaration,
            ImmutableArray.Create(
                SyntaxKind.ClassDeclaration));
    }

    void HandleClassDeclaration(SyntaxNodeAnalysisContext context)
    {
        var classDeclaration = context.Node as ClassDeclarationSyntax;
        if (classDeclaration?.BaseList == null || classDeclaration?.BaseList?.Types == null) return;

        if (classDeclaration.InheritsASystemException(context.SemanticModel) && classDeclaration.Identifier.Text.EndsWith("Exception", StringComparison.InvariantCulture))
        {
            var diagnostic = Diagnostic.Create(Rule, classDeclaration.Identifier.GetLocation());
            context.ReportDiagnostic(diagnostic);
        }
    }
}
