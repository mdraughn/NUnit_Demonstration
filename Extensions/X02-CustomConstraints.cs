using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace NUnit_Demonstration.Extensions
{
    // Since constraints are just classes, we can make our own.

    public class TaskCodeConstraint : Constraint
    {
        private static Regex TaskCodeRegex = new Regex("^[A-Z]{1,3}[1-9][0-9]{2}$");

        public TaskCodeConstraint()
        {
            Description = "Valid UTBMS task code format";
        }

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            return new ConstraintResult(this, actual,
                TaskCodeRegex.IsMatch(actual.ToString()) ? ConstraintStatus.Success : ConstraintStatus.Failure);
            throw new NotImplementedException();
        }
    }
    //public class TaskCodeConstraint2 : Constraint
    //{
    //    private static Regex TaskCodeRegex = new Regex("^[A-Z]{1,3}[1-9][0-9]{2}$");

    //    public override bool Matches(object actual)
    //    {
    //        // REQUIRED: Always save the actual object.
    //        this.actual = actual;
    //        return TaskCodeRegex.IsMatch(actual.ToString());
    //    }

    //    public override void WriteDescriptionTo(MessageWriter writer)
    //    {
    //        writer.WritePredicate("Valid UTBMS task code format");
    //    }
    //}

    [TestFixture]
    class TaskCodeConstraintTest {

        [Test]
        public void ValidTaskCode()
        {
            // You can invoke custom constraints by creating and passing them.
            Assert.That("PA123", new TaskCodeConstraint());

            // You can also use the Matches syntax which us useful in expressions.
            Assert.That("PA123", Is.Not.Null.And.Matches(new TaskCodeConstraint()));
        }

        // You can use it to compose constraints.
        [Test]
        public void InvalidValidTaskCode()
        {
            // Or you can compose it.
            Assert.That("XYZZY", new NotConstraint(new TaskCodeConstraint()));
        }
    }
    [TestFixture]
    class TaskCodeConstraintTest_ExpectedToFail
    {
        // And yes, it will fail like any other constraint.
        [Test, Category("ExpectedToFail")]
        public void InvalidValidTaskCode_ExpectedToFail()
        {
            Assert.That("XYZZY", new TaskCodeConstraint());
        }
    }

    // But what if you want the way-cool "Is" syntax?

    // First, create your own Is-like class in your own namespace.
    // In the grand tracition of Serengti class naming, I'll add a T.
    public static class Tis
    {
        // Since the task code constraint has no arguments, implement it as a getter.
        public static TaskCodeConstraint TaskCode
        {
            get { return new TaskCodeConstraint(); }
        }
    }

    // Now you can use the TIs syntax.

    [TestFixture]
    class TaskCodeConstraintIsTest
    {
        [Test]
        public void TisValidTaskCode()
        {
            // You can invoke custom constraints by creating and passing them.
            Assert.That("PA123", Tis.TaskCode);
        }
    }

    // You should also add an extension method for constraint expressions.

    public static class TIsExtension
    {
        public static TaskCodeConstraint TaskCode(this ConstraintExpression expression)
        {
            return new TaskCodeConstraint();
        }
    }

    // Now you can use it in expressions

    [TestFixture]
    class TaskCodeConstraintExpressionTest
    {
        [Test]
        public void TisValidTaskCode()
        {
            // It has to be a method, though, so there are always ().
            Assert.That("PA123", Is.Not.Null.And.TaskCode());
        }
    }

}