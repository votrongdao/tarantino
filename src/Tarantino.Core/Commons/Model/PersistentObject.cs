using System;

namespace Tarantino.Commons.Core.Model
{
	public abstract class PersistentObject
	{
		public const string ID = "Id";
	
		protected Guid _id;

		public Guid Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public bool IsPersistent
		{
			get { return isPersistentObject(); }
		}

		public override bool Equals(object obj)
		{
			if (isPersistentObject())
			{
				PersistentObject persistentObject = obj as PersistentObject;
				return (persistentObject != null) && (Id == persistentObject.Id);
			}
			else
				return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return isPersistentObject() ? _id.GetHashCode() : base.GetHashCode();
		}

		private bool isPersistentObject()
		{
			return (_id != Guid.Empty);
		}
	}
}