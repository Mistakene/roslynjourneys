// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
    internal partial class SyntaxToken
    {
        internal class SyntaxIdentifier : SyntaxToken
        {
            protected readonly bool missing;
            protected readonly string TextField;

            internal SyntaxIdentifier(string text, bool missing = false)
                : base(SyntaxKind.IdentifierToken, text.Length)
            {
                this.missing = missing;
                this.TextField = text;
                if (missing)
                    ClearFlags(NodeFlags.IsNotMissing);
            }

            internal SyntaxIdentifier(string text, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
                : base(SyntaxKind.IdentifierToken, text.Length, diagnostics, annotations)
            {
                this.TextField = text;
            }

            public override string Text
            {
                get { return missing ? string.Empty : this.TextField; }
            }

            public override object Value
            {
                get 
                {
                    /*if (missing)
                        switch (this.Kind)
                        {
                            case SyntaxKind.IdentifierToken:
                                return string.Empty;
                            default:
                                return null;
                        }*/
                    return this.TextField;
                }
            }

            public override string ValueText
            {
                get { return this.TextField; }
            }

            public override SyntaxToken TokenWithLeadingTrivia(GreenNode trivia)
            {
                if (missing)
                    return new MissingTokenWithTrivia(this.Kind, trivia, null, this.GetDiagnostics(), this.GetAnnotations());
                else
                    return new SyntaxIdentifierWithTrivia(this.Kind, this.TextField, this.TextField, trivia, null, this.GetDiagnostics(), this.GetAnnotations());
            }

            public override SyntaxToken TokenWithTrailingTrivia(GreenNode trivia)
            {
                if (missing)
                    return new MissingTokenWithTrivia(this.Kind, null, trivia, this.GetDiagnostics(), this.GetAnnotations());
                else
                    return new SyntaxIdentifierWithTrivia(this.Kind, this.TextField, this.TextField, null, trivia, this.GetDiagnostics(), this.GetAnnotations());
            }

            internal override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
            {
                if (missing)
                    return new MissingTokenWithTrivia(this.Kind, null, null, diagnostics, this.GetAnnotations());
                else
                    return new SyntaxIdentifier(this.Text, diagnostics, this.GetAnnotations());
            }

            internal override GreenNode SetAnnotations(SyntaxAnnotation[] annotations)
            {

                if (missing)
                    return new MissingTokenWithTrivia(this.Kind, null, null, this.GetDiagnostics(), annotations);
                else
                    return new SyntaxIdentifier(this.Text, this.GetDiagnostics(), annotations);
            }
        }
    }
}
