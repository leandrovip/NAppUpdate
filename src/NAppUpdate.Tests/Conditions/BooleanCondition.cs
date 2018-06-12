using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAppUpdate.Framework.Conditions;
using NAppUpdate.Framework.Tasks;

namespace NAppUpdate.Tests.Conditions
{
	internal class MockCondition : IUpdateCondition
	{
		private readonly bool _isMet;

		internal MockCondition(bool isMet)
		{
			_isMet = isMet;
		}

		public bool IsMet(IUpdateTask task)
		{
			return _isMet;
		}
	}

	[TestClass]
	public class BooleanConditionTests
	{
		[TestMethod]
		public void ShortCircuitOR()
		{
			var bc = new BooleanCondition();
			bc.AddCondition(new MockCondition(true), BooleanCondition.ConditionType.OR);
			bc.AddCondition(new MockCondition(true), BooleanCondition.ConditionType.OR);
			bc.AddCondition(new MockCondition(false), BooleanCondition.ConditionType.OR);

			var isMet = bc.IsMet(null);

			Assert.IsTrue(isMet, "Expected the second or to short circuit the condition list");
		}

		[TestMethod]
		public void MultipleAND()
		{
			var bc = new BooleanCondition();
			bc.AddCondition(new MockCondition(true), BooleanCondition.ConditionType.AND);
			bc.AddCondition(new MockCondition(true), BooleanCondition.ConditionType.AND);

			var isMet = bc.IsMet(null);

			Assert.IsTrue(isMet);
		}

		[TestMethod]
		public void MultipleANDFail()
		{
			var bc = new BooleanCondition();
			bc.AddCondition(new MockCondition(false), BooleanCondition.ConditionType.AND);
			bc.AddCondition(new MockCondition(true), BooleanCondition.ConditionType.AND);

			var isMet = bc.IsMet(null);

			Assert.IsFalse(isMet);
		}

		[TestMethod]
		public void MultipleANDFail2()
		{
			var bc = new BooleanCondition();
			bc.AddCondition(new MockCondition(true), BooleanCondition.ConditionType.AND);
			bc.AddCondition(new MockCondition(false), BooleanCondition.ConditionType.AND);

			var isMet = bc.IsMet(null);

			Assert.IsFalse(isMet);
		}

		[TestMethod]
		public void LastORPass()
		{
			var bc = new BooleanCondition();
			bc.AddCondition(new MockCondition(false), BooleanCondition.ConditionType.AND);
			bc.AddCondition(new MockCondition(false), BooleanCondition.ConditionType.AND);
			bc.AddCondition(new MockCondition(true), BooleanCondition.ConditionType.OR);

			var isMet = bc.IsMet(null);

			Assert.IsTrue(isMet);
		}

		[TestMethod]
		public void MiddleORFail()
		{
			var bc = new BooleanCondition();
			bc.AddCondition(new MockCondition(false), BooleanCondition.ConditionType.AND);
			bc.AddCondition(new MockCondition(true), BooleanCondition.ConditionType.OR);
			bc.AddCondition(new MockCondition(false), BooleanCondition.ConditionType.AND);

			var isMet = bc.IsMet(null);

			Assert.IsFalse(isMet);
		}

		[TestMethod]
		public void Not()
		{
			var bc = new BooleanCondition();
			bc.AddCondition(new MockCondition(false), BooleanCondition.ConditionType.AND | BooleanCondition.ConditionType.NOT);

			var isMet = bc.IsMet(null);

			Assert.IsTrue(isMet);
		}
	}
}
