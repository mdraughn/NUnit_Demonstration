using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
{
    [TestFixture]
    public class TypeConstraints
    {
        // Test hierarchy.
        public interface IFood { }
        public class Life { }
        public class Plant : Life { }
        public class Fruit : Plant, IFood { }
        public class Flower : Plant { }
        public class Animal : Life { }
        public class Cow : Animal, IFood { }
        public class Cat : Animal { }

        [Test]
        public void IsInstanceOf()
        {
            // For testing types.

            var plant = new Plant();

            // Can pass in a type.
            Assert.That(plant, Is.InstanceOf(typeof(Plant)));

            // Can concretize the generic (2.5)
            Assert.That(plant, Is.InstanceOf<Plant>());
        }

        [Test]
        public void IsInstanceOfSuperclass()
        {
            // Accepts superclasses
            var plant = new Plant();
            Assert.That(plant,Is.InstanceOf(typeof(Life)));
            Assert.That(plant,Is.InstanceOf<Life>());
        }

        [Test]
        public void IsNotInstanceOfSubclass()
        {
            // Does not accept subclasses
            var plant = new Plant();
            Assert.That(plant, Is.Not.InstanceOf(typeof(Fruit)));
            Assert.That(plant, Is.Not.InstanceOf<Fruit>());
        }

        [Test]
        public void IsInstanceOfInterface()
        {
            // Works as expected with interfaces.
            Assert.That(new Fruit(), Is.InstanceOf(typeof(IFood)));
            Assert.That(new Fruit(), Is.InstanceOf<IFood>());
            Assert.That(new Cow(), Is.InstanceOf(typeof(IFood)));
            Assert.That(new Cow(), Is.InstanceOf<IFood>());
            Assert.That(new Flower(), Is.Not.InstanceOf(typeof(IFood)));
            Assert.That(new Flower(), Is.Not.InstanceOf<IFood>());
            Assert.That(new Cat(), Is.Not.InstanceOf(typeof(IFood)));
            Assert.That(new Cat(), Is.Not.InstanceOf<IFood>());
        }

        [Test]
        public void IsAssignableFrom()
        {
            var plant = new Plant();

            // It's the same as IsInstanceOf for exact types.
            // E.g. plant can receive an object of type Plant
            Assert.That(plant, Is.AssignableFrom(typeof(Plant)));
            Assert.That(plant, Is.AssignableFrom<Plant>());

            // For inheritance it works backwards
            // E.g. plant cannot receive an object of type Life
            Assert.IsNotAssignableFrom(typeof(Life), plant);
            Assert.IsNotAssignableFrom<Life>(plant);
            Assert.That(plant, Is.Not.AssignableFrom(typeof(Life)));
            Assert.That(plant, Is.Not.AssignableFrom<Life>());

            // But plant can receive an object of type fruit.
            Assert.That(plant, Is.AssignableFrom(typeof(Fruit)));
            Assert.That(plant, Is.AssignableFrom<Fruit>());
        }

        [Test]
        public void TypeOf()
        {
            // Use TypeOf to get exact comparisons.
            var plant = new Plant();
            Assert.That(plant, Is.TypeOf(typeof(Plant)));
            Assert.That(plant, Is.TypeOf<Plant>());
            Assert.That(plant, Is.Not.TypeOf(typeof(Life)));
            Assert.That(plant, Is.Not.TypeOf<Life>());
        }
    }
}
