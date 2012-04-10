using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace ViewModel.Tests
{
    [TestFixture]
    public class ConcurrentCollectionTests
    {
        private ConcurrentObservableCollection<int> collection;
        private Mock<ICollectionRunner> runner;

        [TestFixtureSetUp]
        public void TestSetUp()
        {
            runner = new Mock<ICollectionRunner>();
            collection = new ConcurrentObservableCollection<int>(runner.Object);
            runner.Setup(r => r.Run(It.IsAny<Action>())).Callback<Action>(t => t()).Verifiable("ICollectionRunner.Run was not called!");
        }

        [Test]
        public void Add_CallsRunner_AddItem()
        {
            int count = collection.Count;
            collection.Add(123);
            runner.Verify(r => r.Run(It.IsAny<Action>()), Times.Once());
            Assert.That(collection.Count == count + 1);
        }

        [Test]
        public void Remove_CallsRunner_RemovesItem()
        {
            int currentCount = collection.Count;
            collection.Remove(5);
            runner.Verify(r => r.Run(It.IsAny<Action>()));
            Assert.AreEqual(currentCount - 1, collection.Count);
        }

        [Test]
        public void Insert_CallsRunner_InsertsItem()
        {
            int currentCount = collection.Count;
            collection.Insert(RandomIndex, 5);
            runner.Verify(r => r.Run(It.IsAny<Action>()));
            Assert.AreEqual(currentCount + 1, collection.Count);
        }

        [Test]
        public void Clear_CallsRunner_Clears()
        {
            collection.Clear();
            runner.Verify(r => r.Run(It.IsAny<Action>()));
            Assert.That(!collection.Any());
        }

        [Test]
        public void SetItem_CallsRunner_DoesntChangeCollectionSize()
        {
            collection.Add(153);
            int count = collection.Count;
            collection[RandomIndex] = 10;
            runner.Verify(r => r.Run(It.IsAny<Action>()), Times.AtLeastOnce());
            Assert.AreEqual(collection.Count, count);
        }

        private int RandomIndex
        {
            get { return It.IsInRange(0, collection.Count, Range.Inclusive); }
        }

        [Test]
        public void MoveItem_CallsRunner_MovesItem()
        {
            int oldIndex = RandomIndex;
            int old = collection[oldIndex];
            int newIndex = RandomIndex;
            int @new = collection[newIndex];
            int count = collection.Count;
            collection.Move(oldIndex, newIndex);
            runner.Verify(r => r.Run(It.IsAny<Action>()));
            Assert.AreEqual(collection.Count, count);
            Assert.That(collection[newIndex] == old && collection[oldIndex] == @new);
        }
    }
}
