using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ProductStore.Domain.Data.Extensions
{
	public static class ConfigurationSeedExtensions
	{
		public static T AddIfNotExists<T>(this IDbSet<T> set, Expression<Func<T, bool>> predicate, T entity) where T : class, new()
		{
			return !set.Any(predicate) ? set.Add(entity) : null;
		}

		public static void AddIfNotExists<TEntity>(this IDbSet<TEntity> set, Expression<Func<TEntity, object>> identifierExpression, params TEntity[] entities) where TEntity : class
		{
			var identifyingProperties = GetProperties<TEntity>(identifierExpression).ToList();

			var parameter = Expression.Parameter(typeof(TEntity));
			foreach (var entity in entities)
			{
				var matches = identifyingProperties.Select(pi => Expression.Equal(Expression.Property(parameter, pi.Name), Expression.Constant(pi.GetValue(entity, null))));
				var matchExpression = matches.Aggregate<BinaryExpression, Expression>(null, (agg, v) => (agg == null) ? v : Expression.AndAlso(agg, v));

				var predicate = Expression.Lambda<Func<TEntity, bool>>(matchExpression, new[] { parameter });
				if (!set.Any(predicate))
				{
					set.Add(entity);
				}
			}
		}

		private static IEnumerable<PropertyInfo> GetProperties<T>(Expression<Func<T, object>> exp) where T : class
		{
			var type = typeof(T);
			var properties = new List<PropertyInfo>();

			if (exp.Body.NodeType == ExpressionType.MemberAccess)
			{
				var memExp = exp.Body as MemberExpression;
				if (memExp != null && memExp.Member != null)
					properties.Add(type.GetProperty(memExp.Member.Name));
			}
			else if (exp.Body.NodeType == ExpressionType.Convert)
			{
				var unaryExp = exp.Body as UnaryExpression;
				if (unaryExp != null)
				{
					var propExp = unaryExp.Operand as MemberExpression;
					if (propExp != null && propExp.Member != null)
						properties.Add(type.GetProperty(propExp.Member.Name));
				}
			}
			else if (exp.Body.NodeType == ExpressionType.New)
			{
				var newExp = exp.Body as NewExpression;
				if (newExp != null)
					properties.AddRange(newExp.Members.Select(x => type.GetProperty(x.Name)));
			}

			return properties.OfType<PropertyInfo>();
		}
	}
}