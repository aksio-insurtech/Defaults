// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder;

/// <summary>
/// Extension methods for working with members and filtering.
/// </summary>
public static class MemberFiltersExtensions
{
    /// <summary>
    /// Filter members for any of the supported modifiers.
    /// </summary>
    /// <param name="members">Enumerable to filter.</param>
    /// <param name="getModifiers">Callback for getting modifiers.</param>
    /// <returns>Filtered members.</returns>
    public static IEnumerable<MemberDeclarationSyntax> OnlyWithSupportedModifiersFor(this SyntaxList<MemberDeclarationSyntax> members, Func<MemberDeclarationSyntax, IEnumerable<SyntaxKind>> getModifiers)
    {
        return members.Where(_ =>
        {
            var modifiers = getModifiers(_);
            if (!modifiers.Any())
            {
                return true;
            }

            var contains = false;
            foreach (var modifier in modifiers)
            {
                contains |= _.Modifiers.Any(modifier);
            }

            return contains;
        });
    }
}
