namespace Aksio.CodeAnalysis
{
    /// <summary>
    /// General extension methods for code analysis.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Check if a <see cref="ClassDeclarationSyntax"/> inherits a System Exception.
        /// </summary>
        /// <param name="classDeclaration"><see cref="ClassDeclarationSyntax"/> to check.</param>
        /// <param name="model"><see cref="SemanticModel"/> to use.</param>
        /// <returns>true if it inherits, false if not.</returns>
        public static bool InheritsASystemException(this ClassDeclarationSyntax classDeclaration, SemanticModel model)
        {
            var classSymbol = model.GetDeclaredSymbol(classDeclaration);
            return classSymbol.BaseType.ContainingNamespace.Name.StartsWith("System", StringComparison.InvariantCulture) &&
                classSymbol.BaseType.Name.EndsWith("Exception", StringComparison.InvariantCulture);
        }
    }
}