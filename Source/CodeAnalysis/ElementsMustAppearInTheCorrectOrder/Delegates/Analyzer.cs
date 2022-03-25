// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Delegates;

/// <summary>
/// Represents a <see cref="ElementsMustAppearInTheCorrectOrderAnalyzer"/> that requires delegates in a specific order.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Analyzer : ElementsMustAppearInTheCorrectOrderAnalyzer
{
    /// <inheritdoc/>
    public override DiagnosticDescriptor Rule => new(
        id: "AS0011",
        title: "DelegateElementsMustAppearInTheCorrectOrder",
        messageFormat: "Delegates must come after fields/properties and before events/constructors/finalizers/indexers/methods",
        category: "Exceptions",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: null,
        helpLinkUri: string.Empty,
        customTags: Array.Empty<string>()
    );

    /// <summary>
    /// The element kind we are checking the ordering for.
    /// </summary>
    protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.DelegateDeclaration;
}
