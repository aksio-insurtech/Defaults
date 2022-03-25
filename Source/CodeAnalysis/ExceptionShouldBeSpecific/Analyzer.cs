// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.CodeAnalysis.ExceptionShouldBeSpecific;

/// <summary>
/// Represents a <see cref="DiagnosticAnalyzer"/> that requires one to now throw a generic system exception.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Analyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
    /// </summary>
    public static readonly DiagnosticDescriptor Rule = new(
         id: "AS0008",
         title: "ExceptionShouldBeSpecific",
         messageFormat: "Throwing a generic system exception is not a allowed - you should create a specific exception",
         category: "Exceptions",
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

        context.RegisterOperationAction(HandleThrow, ImmutableArray.Create(
            OperationKind.Throw));
    }

    void HandleThrow(OperationAnalysisContext context)
    {
        if (context.Operation is IThrowOperation throwOperation)
        {
            var exceptionOperation = throwOperation.Exception;
            if (exceptionOperation is IConversionOperation conversionOperation)
            {
                exceptionOperation = conversionOperation.Operand;
            }

            if (exceptionOperation is IObjectCreationOperation exception &&
                exception.Constructor.ContainingNamespace.Name.StartsWith("System", StringComparison.InvariantCulture))
            {
                var fullName = $"{exception.Constructor.ContainingNamespace.Name}.{exception.Constructor.ContainingType.Name}";
                if (fullName != typeof(NotImplementedException).FullName)
                {
                    var diagnostic = Diagnostic.Create(Rule, throwOperation.Syntax.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
