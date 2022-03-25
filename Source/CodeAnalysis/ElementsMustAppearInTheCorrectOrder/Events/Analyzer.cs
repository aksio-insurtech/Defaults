// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder.Events;

/// <summary>
/// Represents a <see cref="ElementsMustAppearInTheCorrectOrderAnalyzer"/> that requires events in a specific order.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Analyzer : ElementsMustAppearInTheCorrectOrderAnalyzer
{
    /// <inheritdoc/>
    public override DiagnosticDescriptor Rule => new(
        id: "AS0012",
        title: "EventElementsMustAppearInTheCorrectOrder",
        messageFormat: "Events must come after fields/properties/delegates and before constructors/finalizers/indexers/methods",
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
    protected override SyntaxKind KindToCheckFor { get; } = SyntaxKind.EventFieldDeclaration;
}
