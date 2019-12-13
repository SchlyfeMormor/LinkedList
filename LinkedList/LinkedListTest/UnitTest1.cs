using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#pragma warning disable IDE0044

namespace LinkedListUnitTest
{
    [TestClass]
    public class LinkedListUnitTest
    {
        int[] values = { 10, 25, 99, 233 };

        [TestMethod]
        public void TestAdd()
        {
            // Arrange
            LinkedList<int> list = new LinkedList<int>();
            Node<int> returnValue;
            Type expectedType = new Node<int>().GetType();

            // Act
            returnValue = list.AddFirst(values[0]);
            for (int i = 1; i < values.Length; i++)
                list.AddFirst(values[i]);

            foreach (int i in values)
                list.AddLast(i);

            // Assert
            Assert.AreSame(
                returnValue.GetType(),
                expectedType,
                "Unexpected return type.");

            int actualData = list[0];
            Assert.AreEqual(
                values[3],
                actualData,
                "Unexpected value of first element.");

            int actualCount = list.Count;
            Assert.AreEqual(
                values.Length * 2,
                actualCount,
                "Unexpected Count.");

            Node<int> foundNode = list.Find(values[0]);
            Assert.AreEqual(
                foundNode.GetType(),
                expectedType,
                "Unexpected return type.");

            Assert.AreEqual(
                foundNode.data,
                values[0],
                "Unexpected value of returned node.");

            Assert.AreEqual(
                list.Find(int.MaxValue),
                null,
                "Expected null.");
        }

        [TestMethod]
        public void TestRemove()
        {
            // Arrange
            LinkedList<int> list = new LinkedList<int>();

            // Act
            foreach (int i in values)
                list.AddLast(i);

            // Assert
            int actualCount = list.Count;
            Assert.AreEqual(
                values.Length,
                actualCount,
                "Unexpected Count before removal.");

            bool returnValue = list.Remove(values[0]);
            Assert.AreEqual(
                returnValue,
                true,
                "Unexpected return.");

            actualCount = list.Count;
            Assert.AreEqual(
                values.Length - 1,
                actualCount,
                "Unexpected Count after removal.");

            int actualData = list[0];
            Assert.AreEqual(
                values[1],
                actualData,
                "Unexpected value of first element.");

            returnValue = list.Remove(100);
            Assert.AreEqual(
                returnValue,
                false,
                "Unexpected return.");

            Assert.AreEqual(
                values.Length - 1,
                actualCount,
                "Unexpected Count after failed removal.");
        }

        [TestMethod]
        public void TestClear()
        {
            // Arrange
            LinkedList<int> list = new LinkedList<int>();

            // Act
            foreach (int i in values)
                list.AddLast(i);

            // Assert
            int actualCount = list.Count;
            Assert.AreEqual(
                values.Length,
                actualCount,
                "Unexpected Count before clear.");

            list.Clear();

            actualCount = list.Count;
            Assert.AreEqual(
                0,
                actualCount,
                "Unexpected Count after clear.");
        }

        [TestMethod]
        public void TestContains()
        {
            // Arrange
            LinkedList<int> list = new LinkedList<int>();

            Assert.AreEqual(
                false,
                list.Contains(values[0]),
                "Unexpected return value.");

            // Act
            foreach (int i in values)
                list.AddLast(i);

            // Assert
            Assert.AreEqual(
                true,
                list.Contains(values[3]),
                "Unexpected return value.");

            Assert.AreEqual(
                false,
                list.Contains(100),
                "Unexpected return value.");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException),
            "Accessing elements out of range didn't throw appropriate exception.")]
        public void TestIteration()
        {
            // Arrange
            LinkedList<int> list = new LinkedList<int>();
            int index = 0;

            // Act
            foreach (int i in values)
                list.AddLast(i);

            // Assert
            foreach (int i in list)
            {
                Assert.AreEqual(
                    i,
                    values[index++],
                    "Unexpected value in list");
            }

            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(
                    list[0],
                    values[index++],
                    "Unexpected value in list");
            }

            list = new LinkedList<int>();
            _ = list[0];
        }

        [TestMethod]
        //[ExpectedException(typeof(InvalidOperationException),
        //    "Accessing elements out of range through enumerator " +
        //    "didn't throw appropriate exception.")]
        public void TestEnumerator()
        {
            // Arrange
            LinkedList<int> list = new LinkedList<int>();
            System.Collections.IEnumerator it;

            // Act
            foreach (int i in values)
                list.AddLast(i);

            it = list.GetEnumerator();

            // Assert
            Assert.AreEqual(
                list[3],
                values[3],
                "Unexpected value.");

            it = list.GetEnumerator();

            Assert.AreEqual(
                it.MoveNext(),
                true,
                "Unexpected return value.");
            var current = it.Current;

            Assert.AreEqual(
                current.GetType(),
                typeof(int),
                "Unexpected return type.");

            Assert.AreEqual(
                current,
                values[0],
                "Unexpected value.");

            Assert.AreEqual(
               it.MoveNext(),
               true,
               "Unexpected return value.");

            Assert.AreEqual(
               it.MoveNext(),
               true,
               "Unexpected return value.");

            Assert.AreEqual(
               it.MoveNext(),
               true,
               "Unexpected return value.");

            Assert.AreEqual(
               it.MoveNext(),
               false,
               "Unexpected return value.");

            _ = it.Current;
        }

        [TestMethod]
        public void LargeTest()
        {
            // Arrange
            LinkedList<int> list = new LinkedList<int>();

            // Act
            for (int i = 0; i < 100; i++)
                foreach (int number in values)
                    list.AddLast(number);

            for (int i = 0; i < 100; i++)
                foreach (int number in values)
                    list.AddFirst(number);

            // Assert
            Assert.AreEqual(
                800,
                list.Count,
                "Unexpected Count.");

            for (int i = 0; i < 100; i++)
                foreach (int number in values)
                    list.Remove(number);

            Assert.AreEqual(
                400,
                list.Count,
               "Unexpected Count.");

        }
    }
}