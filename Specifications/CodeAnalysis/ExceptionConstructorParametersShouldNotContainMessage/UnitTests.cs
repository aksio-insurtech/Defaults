namespace Aksio.CodeAnalysis.ExceptionConstructorParametersShouldNotContainMessage
{
    public class UnitTests : CodeFixVerifier
    {
        [Fact]
        public void WithoutMessageInParameters()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {
                        public SomethingWentWrong(string first, string second)
                        {
                        }
                    }
                }       
            ";

            VerifyCSharpDiagnostic(content);
        }

        [Fact]
        public void WithMessageInConstructorParameters()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {
                        public SomethingWentWrong(string firstMessage, string messageForSecond)
                        {
                        }
                    }
                }       
            ";

            var firstFailure = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 8, 58)
                }
            };

            var secondFailure = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 8, 79)
                }
            };

            VerifyCSharpDiagnostic(content, firstFailure, secondFailure);
        }

        [Fact]
        public void WithMessageNotBeingStringInConstructorParameters()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {
                        public SomethingWentWrong(Type firstMessage, Type messageForSecond)
                        {
                        }
                    }
                }       
            ";

            VerifyCSharpDiagnostic(content);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new Analyzer();
        }
    }
}