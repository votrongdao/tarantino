using System;
using System.Collections;
using System.Collections.Generic;
using Tarantino.Core;
using Tarantino.Core.Commons.Model;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using NHibernate;
using NHibernate.Expression;
using StructureMap;
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Infrastructure.Commons.DataAccess.Repositories
{
	[Pluggable(Keys.Default)]
	public class PersistentObjectRepository : IPersistentObjectRepository
	{
		private IObjectMapper _mapper;

		public string ConnectionStringKey
		{
			get { return _mapper.ConnectionStringKey; }
			set { _mapper.ConnectionStringKey = value; }
		}

		public PersistentObjectRepository(IObjectMapper mapper)
		{
			_mapper = mapper;
		}

		public IEnumerable<T> GetAll<T>()
		{
			T[] properties = _mapper.LoadAll<T>();
			return properties;
		}

		public T GetById<T>(Guid id) where T : PersistentObject
		{
			T property = _mapper.Load<T>(id);
			return property;
		}

		public void Delete(PersistentObject persistentObject)
		{
			_mapper.Delete(persistentObject);
		}

		public void Save(PersistentObject persistentObject)
		{
			_mapper.SaveOrUpdate(persistentObject);
		}

		public void Revert(PersistentObject persistentObject)
		{
			_mapper.Refresh(persistentObject);
		}

		public IEnumerable<T> FindAll<T>(CriterionSet criterionSet)
		{
			List<T> objects = (List<T>) _mapper.Run(delegate(ISession session, object[] arguments)
      	{
					List<T> persistentObjects = new List<T>();

					ICriteria criteria = session.CreateCriteria(typeof(T));
					criteria.SetCacheable(true);

					foreach (Criterion criterion in criterionSet.GetCriteria())
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

					IList list = criteria.List();
					foreach (T entity in list)
					{
						persistentObjects.Add(entity);
					}

      		return persistentObjects;

      	}, false);

			return objects;
		}

		public T FindFirst<T>(CriterionSet criterionSet) where T : class
		{
			List<T> persistentObjects = new List<T>(FindAll<T>(criterionSet));

			T persistentObject = (persistentObjects.Count > 0) ? persistentObjects[0] : null;

			return persistentObject;
		}
	}
}