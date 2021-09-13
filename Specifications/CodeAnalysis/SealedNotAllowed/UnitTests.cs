namespace Aksio.CodeAnalysis.SealedNotAllowed
{
    public class UnitTests : CodeFixVerifier
    {
        [Fact]
        public void WithoutSealed()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class MyClass
                    {

                    }
                }       
            ";

            VerifyCSharpDiagnostic(content);
        }

        [Fact]
        public void WithSealed()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public sealed class MyClass
                    {

                    }
                }       
            ";

            var expected = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 6, 28)
                }
            };

            VerifyCSharpDiagnostic(content, expected);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new Analyzer();
        }
    }
}