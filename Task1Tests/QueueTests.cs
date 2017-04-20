using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;

namespace Task1Tests
{
    [TestFixture]
    public class QueueTests
    {
        MyQueue<string> q;

        private void Initial()
        {
            q = new MyQueue<string>();
            for (int i = 0; i < 7; i++)
                q.Enqueue(i.ToString());
        }

        [Test]
        public void Queue_Contains_PositiveTests()
        {
            Initial();
            Assert.AreEqual(!default(bool), q.Contains("4"));
        }

        [Test]
        public void Queue_foreach_PositiveTests()
        {
            string expected = "" , actual = "";
            Initial();
            foreach (var item in q)
                actual += item.ToString();
            expected += "0123456";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Queue_EqualityComaparer_PositiveTests()
        {
            MyQueue<Book> qb = new MyQueue<Book>(new ComparerByPrice());
            qb.Enqueue(new Book("Palanik", 120));
            qb.Enqueue(new Book("King", 140));

            Assert.AreEqual(!default(bool), qb.Contains(new Book("Pushkin", 140)));
        }

        [Test]
        public void Queue_EqualityDefault_PositiveTests()
        {
            MyQueue<Book> qb = new MyQueue<Book>();
            qb.Enqueue(new Book("Palanik", 120));
            qb.Enqueue(new Book("King", 140));

            Assert.AreEqual(default(bool), qb.Contains(new Book("Pushkin", 140)));
        }

        [Test]
        public void Queue_Peek_PositiveTests()
        {
            Initial();
            q.Dequeue();
            q.Dequeue();
            Assert.AreEqual("2", q.Peek.ToString());
        }
    }
}
