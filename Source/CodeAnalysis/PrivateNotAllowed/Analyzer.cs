// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.PrivateNotAllowed;

/// <summary>
/// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of 'private' keyword.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Analyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
    /// </summary>
    public static readonly DiagnosticDescriptor Rule = new(
         id: "AS0002",
         title: "PrivateNotAllowed",
         messageFormat: "Private is implicit in C# and is not needed",
         category: "Style",
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
            HandleDeclarations,
            ImmutableArray.Create(
                SyntaxKind.ClassDeclaration,
                SyntaxKind.MethodDeclaration,
                SyntaxKind.FieldDeclaration));

        context.RegisterSyntaxNodeAction(
            HandleEventDeclaration,
            ImmutableArray.Create(SyntaxKind.EventDeclaration));

        context.RegisterSyntaxNodeAction(
            HandlePropertyDeclaration,
            ImmutableArray.Create(SyntaxKind.PropertyDeclaration));
    }

    void HandleDeclarations(SyntaxNodeAnalysisContext context)
    {
        var childTokens = context.Node?.ChildTokens();
        if (childTokens == null) return;
        ReportErrorIfModifierIsPrivate(context, childTokens);
    }

    void HandleEventDeclaration(SyntaxNodeAnalysisContext context)
    {
        var eventDeclaration = context.Node as EventDeclarationSyntax;
        ReportErrorIfModifierIsPrivate(context, eventDeclaration.Modifiers);
    }

    void HandlePropertyDeclaration(SyntaxNodeAnalysisContext context)
    {
        var propertyDeclaration = context.Node as PropertyDeclarationSyntax;
        ReportErrorIfModifierIsPrivate(context, propertyDeclaration.Modifiers);
    }

    void ReportErrorIfModifierIsPrivate(SyntaxNodeAnalysisContext context, IEnumerable<SyntaxToken> tokens)
    {
        var privateKeyword = tokens.SingleOrDefault(_ => _.IsKind(SyntaxKind.PrivateKeyword));
        if (privateKeyword == default) return;

        var diagnostic = Diagnostic.Create(Rule, privateKeyword.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}
