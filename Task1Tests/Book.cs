using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1Tests
{
    class Book : IEquatable<Book>
    {
        public string Name { get; }
        public int Price { get; }

        public Book(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public bool Equals(Book other)
        {
            if (Name == other.Name)
                return true;
            return false;
        }

        public override bool Equals(object obj) => this.Equals((Book)obj);

        public override int GetHashCode() => Name.GetHashCode() ^ 9 + Price.GetHashCode() ^ 9;
    }

    class ComparerByPrice : IEqualityComparer<Book>
    {
        public bool Equals(Book x, Book y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                throw new ArgumentNullException();

            if (x.Price == y.Price)
                return true;
            else return false;
        }

        public int GetHashCode(Book obj)
        {
            if (ReferenceEquals(obj, null))
                throw new ArgumentNullException();

            return obj.Name.GetHashCode() ^ 9 + obj.Price.GetHashCode() ^ 9;
        }
    }
}
