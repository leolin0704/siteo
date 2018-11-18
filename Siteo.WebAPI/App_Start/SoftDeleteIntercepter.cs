﻿using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace Siteo.WebAPI.App_Start
{
    public class SoftDeleteInterceptor : IDbCommandTreeInterceptor
    {
        public const string IsDeletedColumnName = "IsDeleted";

        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
            if (interceptionContext.OriginalResult.DataSpace != DataSpace.SSpace)
            {
                return;
            }

            var queryCommand = interceptionContext.Result as DbQueryCommandTree;
            if (queryCommand != null)
            {
                interceptionContext.Result = HandleQueryCommand(queryCommand);
            }

            var deleteCommand = interceptionContext.OriginalResult as DbDeleteCommandTree;
            if (deleteCommand != null)
            {
                interceptionContext.Result = HandleDeleteCommand(deleteCommand);
            }
        }

        private static DbCommandTree HandleDeleteCommand(DbDeleteCommandTree deleteCommand)
        {
            var setClauses = new List<DbModificationClause>();
            var table = (EntityType)deleteCommand.Target.VariableType.EdmType;

            if (table.Properties.All(p => p.Name != IsDeletedColumnName))
            {
                return deleteCommand;
            }

            setClauses.Add(DbExpressionBuilder.SetClause(
                deleteCommand.Target.VariableType.Variable(deleteCommand.Target.VariableName).Property(IsDeletedColumnName),
                DbExpression.FromInt32(1)));

            return new DbUpdateCommandTree(
                deleteCommand.MetadataWorkspace,
                deleteCommand.DataSpace,
                deleteCommand.Target,
                deleteCommand.Predicate,
                setClauses.AsReadOnly(), null);
        }

        private static DbCommandTree HandleQueryCommand(DbQueryCommandTree queryCommand)
        {
            var newQuery = queryCommand.Query.Accept(new SoftDeleteQueryVisitor());
            return new DbQueryCommandTree(
                queryCommand.MetadataWorkspace,
                queryCommand.DataSpace,
                newQuery);
        }

        public class SoftDeleteQueryVisitor : DefaultExpressionVisitor
        {
            public override DbExpression Visit(DbScanExpression expression)
            {
                var table = (EntityType)expression.Target.ElementType;
                if (table.Properties.All(p => p.Name != IsDeletedColumnName))
                {
                    return base.Visit(expression);
                }

                var binding = expression.Bind();
                return binding.Filter(
                    binding.VariableType
                        .Variable(binding.VariableName)
                        .Property(IsDeletedColumnName)
                        .NotEqual(DbExpression.FromInt32(1)));
            }
        }

    }
}
