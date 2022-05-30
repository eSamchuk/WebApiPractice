using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using MediatR;
using NmsDisplayData;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.Data.Entities;

namespace NoMansSkyRecipies.CQRS.Queries
{
    public class GetResourceByCategoryQuery : IRequest<List<DisplayedResource>>
    {
        public Expression<Func<RawResource, bool>> PredicateExpression { get; }

        public GetResourceByCategoryQuery(Expression<Func<RawResource, bool>> predicateExpression)
        {
            this.PredicateExpression = predicateExpression;
        }
    }
}
