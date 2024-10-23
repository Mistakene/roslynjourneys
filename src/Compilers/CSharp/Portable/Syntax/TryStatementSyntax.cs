// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
    public partial class TryStatementSyntax : StatementSyntax
    {
        public TryStatementSyntax Update(SyntaxToken tryKeyword, BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax @finally)
            => Update(AttributeLists, tryKeyword, block, catches, @finally);

        private SyntaxNode? attributeLists;
        private BlockSyntax? block;
        private SyntaxNode? catches;
        private FinallyClauseSyntax? @finally;

        internal TryStatementSyntax(InternalSyntax.CSharpSyntaxNode green, SyntaxNode? parent, int position)
          : base(green, parent, position)
        {
        }

        public override SyntaxList<AttributeListSyntax> AttributeLists => new SyntaxList<AttributeListSyntax>(GetRed(ref this.attributeLists, 0));

        public SyntaxToken TryKeyword => new SyntaxToken(this, ((InternalSyntax.TryStatementSyntax)this.Green).tryKeyword, GetChildPosition(1), GetChildIndex(1));

        public BlockSyntax Block => GetRed(ref this.block, 2)!;

        public SyntaxList<CatchClauseSyntax> Catches => new SyntaxList<CatchClauseSyntax>(GetRed(ref this.catches, 3));

        public FinallyClauseSyntax? Finally => GetRed(ref this.@finally, 4);

        internal override SyntaxNode? GetNodeSlot(int index)
            => index switch
            {
                0 => GetRedAtZero(ref this.attributeLists)!,
                2 => GetRed(ref this.block, 2)!,
                3 => GetRed(ref this.catches, 3)!,
                4 => GetRed(ref this.@finally, 4),
                _ => null,
            };

        internal override SyntaxNode? GetCachedSlot(int index)
            => index switch
            {
                0 => this.attributeLists,
                2 => this.block,
                3 => this.catches,
                4 => this.@finally,
                _ => null,
            };

        public override void Accept(CSharpSyntaxVisitor visitor) => visitor.VisitTryStatement(this);
        public override TResult? Accept<TResult>(CSharpSyntaxVisitor<TResult> visitor) where TResult : default => visitor.VisitTryStatement(this);

        public TryStatementSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken tryKeyword, BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax? @finally)
        {
            if (attributeLists != this.AttributeLists || tryKeyword != this.TryKeyword || block != this.Block || catches != this.Catches || @finally != this.Finally)
            {
                var newNode = SyntaxFactory.TryStatement(attributeLists, tryKeyword, block, catches, @finally);
                var annotations = GetAnnotations();
                return annotations?.Length > 0 ? newNode.WithAnnotations(annotations) : newNode;
            }

            return this;
        }

        internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists) => WithAttributeLists(attributeLists);
        public new TryStatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists) => Update(attributeLists, this.TryKeyword, this.Block, this.Catches, this.Finally);
        public TryStatementSyntax WithTryKeyword(SyntaxToken tryKeyword) => Update(this.AttributeLists, tryKeyword, this.Block, this.Catches, this.Finally);
        public TryStatementSyntax WithBlock(BlockSyntax block) => Update(this.AttributeLists, this.TryKeyword, block, this.Catches, this.Finally);
        public TryStatementSyntax WithCatches(SyntaxList<CatchClauseSyntax> catches) => Update(this.AttributeLists, this.TryKeyword, this.Block, catches, this.Finally);
        public TryStatementSyntax WithFinally(FinallyClauseSyntax? @finally) => Update(this.AttributeLists, this.TryKeyword, this.Block, this.Catches, @finally);

        internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items) => AddAttributeLists(items);
        public new TryStatementSyntax AddAttributeLists(params AttributeListSyntax[] items) => WithAttributeLists(this.AttributeLists.AddRange(items));
        public TryStatementSyntax AddBlockAttributeLists(params AttributeListSyntax[] items) => WithBlock(this.Block.WithAttributeLists(this.Block.AttributeLists.AddRange(items)));
        public TryStatementSyntax AddBlockStatements(params StatementSyntax[] items) => WithBlock(this.Block.WithStatements(this.Block.Statements.AddRange(items)));
        public TryStatementSyntax AddCatches(params CatchClauseSyntax[] items) => WithCatches(this.Catches.AddRange(items));
    }

    public sealed partial class CatchClauseSyntax : CSharpSyntaxNode
    {
        private CatchDeclarationSyntax? declaration;
        private CatchFilterClauseSyntax? filter;
        private BlockSyntax? block;

        internal CatchClauseSyntax(InternalSyntax.CSharpSyntaxNode green, SyntaxNode? parent, int position)
          : base(green, parent, position)
        {
        }

        public SyntaxToken CatchKeyword => new SyntaxToken(this, ((InternalSyntax.CatchClauseSyntax)this.Green).catchKeyword, Position, 0);

        public CatchDeclarationSyntax? Declaration => GetRed(ref this.declaration, 1);

        public CatchFilterClauseSyntax? Filter => GetRed(ref this.filter, 2);

        public BlockSyntax Block => GetRed(ref this.block, 3)!;

        internal override SyntaxNode? GetNodeSlot(int index)
            => index switch
            {
                1 => GetRed(ref this.declaration, 1),
                2 => GetRed(ref this.filter, 2),
                3 => GetRed(ref this.block, 3)!,
                _ => null,
            };

        internal override SyntaxNode? GetCachedSlot(int index)
            => index switch
            {
                1 => this.declaration,
                2 => this.filter,
                3 => this.block,
                _ => null,
            };

        public override void Accept(CSharpSyntaxVisitor visitor) => visitor.VisitCatchClause(this);
        public override TResult? Accept<TResult>(CSharpSyntaxVisitor<TResult> visitor) where TResult : default => visitor.VisitCatchClause(this);

        public CatchClauseSyntax Update(SyntaxToken catchKeyword, CatchDeclarationSyntax? declaration, CatchFilterClauseSyntax? filter, BlockSyntax block)
        {
            if (catchKeyword != this.CatchKeyword || declaration != this.Declaration || filter != this.Filter || block != this.Block)
            {
                var newNode = SyntaxFactory.CatchClause(catchKeyword, declaration, filter, block);
                var annotations = GetAnnotations();
                return annotations?.Length > 0 ? newNode.WithAnnotations(annotations) : newNode;
            }

            return this;
        }

        public CatchClauseSyntax WithCatchKeyword(SyntaxToken catchKeyword) => Update(catchKeyword, this.Declaration, this.Filter, this.Block);
        public CatchClauseSyntax WithDeclaration(CatchDeclarationSyntax? declaration) => Update(this.CatchKeyword, declaration, this.Filter, this.Block);
        public CatchClauseSyntax WithFilter(CatchFilterClauseSyntax? filter) => Update(this.CatchKeyword, this.Declaration, filter, this.Block);
        public CatchClauseSyntax WithBlock(BlockSyntax block) => Update(this.CatchKeyword, this.Declaration, this.Filter, block);

        public CatchClauseSyntax AddBlockAttributeLists(params AttributeListSyntax[] items) => WithBlock(this.Block.WithAttributeLists(this.Block.AttributeLists.AddRange(items)));
        public CatchClauseSyntax AddBlockStatements(params StatementSyntax[] items) => WithBlock(this.Block.WithStatements(this.Block.Statements.AddRange(items)));
    }
}

namespace Microsoft.CodeAnalysis.CSharp
{
    public partial class SyntaxFactory
    {
        public static TryStatementSyntax TryStatement(BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax? @finally)
            => TryStatement(attributeLists: default, block, catches, @finally);

        public static TryStatementSyntax TryStatement(SyntaxToken tryKeyword, BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax? @finally)
            => TryStatement(attributeLists: default, tryKeyword, block, catches, @finally);
    }
}
