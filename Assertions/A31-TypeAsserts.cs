using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Assertions
{
    [TestFixture]
    public class TypeAsserts
    {
        // Test hierarchy.
        public interface IFood { }
        public class Life { }
            public class Plant : Life {}
                public class Fruit : Plant, IFood {}
                public class Flower : Plant {}
            public class Animal : Life {}
                public class Cow : Animal, IFood {}
                public class Cat : Animal {}

        [Test]
        public void IsInstanceOf()
        {
            // For testing types.

            var plant = new Plant();
            
            // Can pass in a type.
            Assert.IsInstanceOf(typeof(Plant), plant);

            // Can concretize the generic (2.5)
            Assert.IsInstanceOf<Plant>(plant);
        }

        [Test]
        public void IsInstanceOfSuperclass()
        {
            // Accepts superclasses
            var plant = new Plant();
            Assert.IsInstanceOf(typeof(Life), plant);
            Assert.IsInstanceOf<Life>(plant);
        }

        [Test]
        public void IsNotInstanceOfSubclass()
        {
            // Does not accept subclasses
            var plant = new Plant();
            Assert.IsNotInstanceOf(typeof(Fruit), plant);
            Assert.IsNotInstanceOf<Fruit>(plant);
        }

        [Test]
        public void IsInstanceOfInterface()
        {
            // Works as expected with interfaces.
            Assert.IsInstanceOf(typeof(IFood), new Fruit());
            Assert.IsInstanceOf<IFood>(new Fruit());
            Assert.IsInstanceOf(typeof(IFood), new Cow());
            Assert.IsInstanceOf<IFood>(new Cow());
            Assert.IsNotInstanceOf(typeof(IFood), new Flower());
            Assert.IsNotInstanceOf<IFood>(new Flower());
            Assert.IsNotInstanceOf(typeof(IFood), new Cat());
            Assert.IsNotInstanceOf<IFood>(new Cat());
        }

        [Test]
        public void IsAssignableFrom()
        {
            var plant = new Plant();

            // It's the same as IsInstanceOf for exact types.
            // E.g. plant can receive an object of type Plant
            Assert.IsAssignableFrom(typeof(Plant), plant);
            Assert.IsAssignableFrom<Plant>(plant);

            // For inheritance it works backwards
            // E.g. plant cannot receive an object of type Life
            Assert.IsNotAssignableFrom(typeof(Life), plant);
            Assert.IsNotAssignableFrom<Life>(plant);

            // But plant can receive an object of type fruit.
            Assert.IsAssignableFrom(typeof(Fruit), plant);
            Assert.IsAssignableFrom<Fruit>(plant);
        }
    }
}
