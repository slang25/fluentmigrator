#region License
// 
// Copyright (c) 2007-2009, Sean Chambers <schambers80@gmail.com>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System.Collections.Generic;
using FluentMigrator.Builders.Delete;
using FluentMigrator.Builders.Delete.Column;
using FluentMigrator.Builders.Delete.ForeignKey;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;
using Moq;
using NUnit.Framework;
using NUnit.Should;

namespace FluentMigrator.Tests.Unit.Builders.Delete
{
	[TestFixture]
	public class DeleteExpressionRootTests
	{
		[Test]
		public void CallingTableAddsDeleteTableExpressionToContextWithSpecifiedName()
		{
			var collectionMock = new Mock<ICollection<IMigrationExpression>>();
            collectionMock.Verify(x => x.Add(It.Is<DeleteTableExpression>(e => e.TableName.Equals("Bacon"))), Times.AtMostOnce());

			var contextMock = new Mock<IMigrationContext>();
			contextMock.SetupGet(x => x.Expressions).Returns(collectionMock.Object).AtMostOnce();

			var root = new DeleteExpressionRoot(contextMock.Object);
			root.Table("Bacon");

			collectionMock.VerifyAll();
			contextMock.VerifyAll();
		}

		[Test]
		public void CallingColumnAddsDeleteColumnExpressionToContextWithSpecifiedName()
		{
			var collectionMock = new Mock<ICollection<IMigrationExpression>>();
            collectionMock.Verify(x => x.Add(It.Is<DeleteColumnExpression>(e => e.ColumnName.Equals("Bacon"))), Times.AtMostOnce());

			var contextMock = new Mock<IMigrationContext>();
			contextMock.SetupGet(x => x.Expressions).Returns(collectionMock.Object).AtMostOnce();

			var root = new DeleteExpressionRoot(contextMock.Object);
			root.Column("Bacon");

			collectionMock.VerifyAll();
			contextMock.VerifyAll();
		}

		[Test]
		public void CallingColumnReturnsDeleteColumnExpressionBuilder()
		{
			var collectionMock = new Mock<ICollection<IMigrationExpression>>();
			var contextMock = new Mock<IMigrationContext>();
			contextMock.SetupGet(x => x.Expressions).Returns(collectionMock.Object).AtMostOnce();

			var root = new DeleteExpressionRoot(contextMock.Object);
			var builder = root.Column("Bacon");

			builder.ShouldBeOfType<DeleteColumnExpressionBuilder>();
			contextMock.VerifyAll();
		}

		[Test]
		public void CallingForeignKeyAddsDeleteForeignKeyExpressionToContext()
		{
			var collectionMock = new Mock<ICollection<IMigrationExpression>>();
            collectionMock.Verify(x => x.Add(It.IsAny<DeleteForeignKeyExpression>()), Times.AtMostOnce());

			var contextMock = new Mock<IMigrationContext>();
			contextMock.SetupGet(x => x.Expressions).Returns(collectionMock.Object).AtMostOnce();

			var root = new DeleteExpressionRoot(contextMock.Object);
			root.ForeignKey();

			collectionMock.VerifyAll();
			contextMock.VerifyAll();
		}

		[Test]
		public void CallingForeignKeyReturnsDeleteForeignKeyExpressionBuilder()
		{
			var collectionMock = new Mock<ICollection<IMigrationExpression>>();
			var contextMock = new Mock<IMigrationContext>();
			contextMock.SetupGet(x => x.Expressions).Returns(collectionMock.Object).AtMostOnce();

			var root = new DeleteExpressionRoot(contextMock.Object);
			var builder = root.ForeignKey();

			builder.ShouldBeOfType<DeleteForeignKeyExpressionBuilder>();
			contextMock.VerifyAll();
		}

		[Test]
		public void CallingForeignKeyWithNameAddsDeleteForeignKeyExpressionToContextWithSpecifiedName()
		{
			var collectionMock = new Mock<ICollection<IMigrationExpression>>();
            collectionMock.Verify(x => x.Add(It.Is<DeleteForeignKeyExpression>(e => e.ForeignKey.Name.Equals("FK_Bacon"))), Times.AtMostOnce());

			var contextMock = new Mock<IMigrationContext>();
			contextMock.SetupGet(x => x.Expressions).Returns(collectionMock.Object).AtMostOnce();

			var root = new DeleteExpressionRoot(contextMock.Object);
			root.ForeignKey("FK_Bacon");

			collectionMock.VerifyAll();
			contextMock.VerifyAll();
		}
	}
}