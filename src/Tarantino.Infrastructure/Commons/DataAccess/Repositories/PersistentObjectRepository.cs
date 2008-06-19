using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using Tarantino.Core.Commons.Model;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Infrastructure.Commons.DataAccess.Repositories
{
	public class PersistentObjectRepository : RepositoryBase, IPersistentObjectRepository
	{
		public string ConfigurationFile { get; set; }

		public PersistentObjectRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
		{
		}

		public IEnumerable<T> GetAll<T>()
		{
			var session = CreateSession();
			var criteria = session.CreateCriteria(typeof (T));
			criteria.SetCacheable(true);
			var list = criteria.List<T>();
			return list;
		}

		public T GetById<T>(Guid id) where T : PersistentObject
		{
			return CreateSession().Load<T>(id);
		}

		public void Delete(PersistentObject persistentObject)
		{
			CreateSession().Delete(persistentObject);
		}

		public void Save(PersistentObject persistentObject)
		{
			CreateSession().SaveOrUpdate(persistentObject);
		}

		public void Revert(PersistentObject persistentObject)
		{
			CreateSession().Refresh(persistentObject);
		}

		public IEnumerable<T> FindAll<T>(CriterionSet criterionSet)
		{
			var session = CreateSession();

			var persistentObjects = new List<T>();

			var criteria = session.CreateCriteria(typeof (T));
			criteria.SetCacheable(true);

			foreach (var criterion in criterionSet.GetCriteria())
			{
				ICriterion expression;

				if (criterion.Operator == ComparisonOperator.GreaterThan)
				{
					expression = Expression.Gt(criterion.Attribute, criterion.Value);
				}
				else if (criterion.Operator == ComparisonOperator.LessThan)
				{
					expression = Expression.Lt(criterion.Attribute, criterion.Value);
				}
				else if (criterion.Operator == ComparisonOperator.NotEqual)
				{
					if (criterion.Value == null)
					{
						expression = Expression.IsNotNull(criterion.Attribute);
					}
					else
					{
						expression = Expression.Not(Expression.Eq(criterion.Attribute, criterion.Value));
					}
				}
				else
				{
					if (criterion.Value == null)
					{
						expression = Expression.IsNull(criterion.Attribute);
					}
					else
					{
						expression = Expression.Eq(criterion.Attribute, criterion.Value);
					}
				}

				criteria.Add(expression);
			}

			if (criterionSet.OrderBy != null)
			{
				if (criterionSet.SortOrder == SortOrder.Descending)
				{
					criteria.AddOrder(Order.Desc(criterionSet.OrderBy));
				}
				else
				{
					criteria.AddOrder(Order.Asc(criterionSet.OrderBy));
				}
			}

			var list = criteria.List();
			foreach (T entity in list)
			{
				persistentObjects.Add(entity);
			}

			return persistentObjects;
		}

		public T FindFirst<T>(CriterionSet criterionSet) where T : class
		{
			var persistentObjects = new List<T>(FindAll<T>(criterionSet));

			var persistentObject = (persistentObjects.Count > 0) ? persistentObjects[0] : null;

			return persistentObject;
		}

		private ISession CreateSession()
		{
			return GetSession(ConfigurationFile);
		}
	}
}