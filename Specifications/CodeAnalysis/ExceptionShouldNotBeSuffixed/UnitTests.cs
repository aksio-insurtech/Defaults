namespace Aksio.CodeAnalysis.ExceptionShouldNotBeSuffixed
{
    public class UnitTests : CodeFixVerifier
    {
        [Fact]
        public void WithoutSuffix()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {

                    }
                }       
            ";

            VerifyCSharpDiagnostic(content);
        }

        [Fact]
        public void WithSuffix()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class MyException : Exception
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
                    new DiagnosticResultLocation("Test0.cs", 6, 34)
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