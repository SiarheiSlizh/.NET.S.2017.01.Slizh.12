using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    /// <summary>
    /// Generic Queue
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    public class MyQueue<T>
    {
        #region fields
        private int head;
        private int tail;
        private int count;
        private T[] arr;
        IEqualityComparer<T> comparer;
        #endregion

        #region prop
        /// <summary>
        /// Capacity of queue
        /// </summary>
        public int Capacity { get { return arr.Length; } }

        /// <summary>
        /// Amount of elements in queue
        /// </summary>
        public int Count
        {
            get { return count; }
            private set { count = value; }
        }

        /// <summary>
        /// readonly property returns first element in queue
        /// </summary>
        public T Peek {
            get
            {
                if (Count == 0)
                    throw new ArgumentOutOfRangeException();
                return arr[head];
            }
        }
        #endregion

        #region ctor
        public MyQueue() : this(2)
        {
        }

        public MyQueue(int capacity)
        {
            arr = new T[capacity];
            head = tail = count = 0;
            comparer = EqualityComparer<T>.Default;
        }

        public MyQueue(IEqualityComparer<T> eq) : this(2,eq)
        {
        }

        public MyQueue(int capacity, IEqualityComparer<T> eq)
        {
            arr = new T[capacity];
            head = tail = count = 0;
            comparer = eq;
        }
        #endregion

        #region public methods
        /// <summary>
        /// add element at the end of queue
        /// </summary>
        /// <param name="element">element which is added at the end of queue</param>
        public void Enqueue(T element)
        {
            if (count == Capacity)
                SetCapacity(Capacity * 2);

            tail = tail % arr.Length;
            arr[tail] = element;
            tail++;
            Count++;
        }

        /// <summary>
        /// remove element from head of queue
        /// </summary>
        /// <returns>removed element</returns>
        public T Dequeue()
        {
            if (Count == 0)
                throw new ArgumentOutOfRangeException();

            T buf = arr[head];
            arr[head] = default(T);
            head = (head + 1) % arr.Length;
            Count--;
            return buf;
        }

        /// <summary>
        /// Checks if there is paticular element in the queue
        /// </summary>
        /// <param name="element">particular element</param>
        /// <returns></returns>
        public bool Contains(T element)
        {
            int first = head;
            int size = Count;
            while (size-- > 0)
            {
                if (comparer.Equals(element, arr[first]))
                    return true;
                first = (first + 1) % arr.Length;
            }
            return false;
        }

        /// <summary>
        /// set default values towards all elements in the queue
        /// </summary>
        public void Clear()
        {
            if (tail <= head)
            {
                Array.Clear(arr, head, arr.Length - head);
                Array.Clear(arr, 0, tail);
            }
            else
                Array.Clear(arr, head, Count);
        }

        /// <summary>
        /// Transformes queue into array
        /// </summary>
        /// <returns>array</returns>
        public T[] ToArray()
        {
            T[] toArr = new T[Count];

            if (tail <= head)
            {
                Array.Copy(arr, head, toArr, 0, arr.Length - head);
                Array.Copy(arr, 0, toArr, arr.Length - head, tail);
            }
            else
                Array.Copy(arr, head, toArr, 0, Count);

            return toArr;
        }

        /// <summary>
        /// Copies queue into array
        /// </summary>
        /// <param name="array">array in which elements is copied</param>
        /// <param name="index"></param>
        public void CopyTo(T[] array, int index)
        {
            if (ReferenceEquals(array, null))
                throw new ArgumentNullException(nameof(array));

            if (index >= count || index < 0)
                throw new ArgumentException(nameof(index));

            if (arr.Length < Count - index)
                throw new ArgumentOutOfRangeException(nameof(array));

            if (tail <= head)
            {
                Array.Copy(arr, head, array, 0, arr.Length - head);
                Array.Copy(arr, 0, array, arr.Length - head, tail);
            }
            else
                Array.Copy(arr, head, array, 0, Count);
        }

        /// <summary>
        /// Corrects the length of queue in according to amount of elements there
        /// </summary>
        public void TrimExcess()
        {
            int length = (int)(arr.Length * 0.9);
            if (Count < length)
                SetCapacity(Count);
        }

        /// <summary>
        /// creates iterator which can enumerate th queue
        /// </summary>
        /// <returns>iterator</returns>
        public MyQueueEnumerator GetEnumerator() => new MyQueueEnumerator(this);
        #endregion

        #region private methods
        /// <summary>
        /// set the length of queue
        /// </summary>
        /// <param name="capacity">size of queue</param>
        private void SetCapacity(int capacity)
        {
            T[] newArr = new T[capacity];

            if (tail <= head)
            {
                Array.Copy(arr, head, newArr, 0, arr.Length - head);
                Array.Copy(arr, 0, newArr, arr.Length - head, tail);
            }
            else 
                Array.Copy(arr, head, newArr, 0, Count);
            head = 0;
            tail = Count;
            arr = newArr;
        }
        #endregion

        #region iterator
        /// <summary>
        /// Iterator for Queue
        /// </summary>
        public struct MyQueueEnumerator
        {
            #region fields
            private int currentIndex;
            private readonly MyQueue<T> queue;
            private int count;
            #endregion

            #region prop
            /// <summary>
            /// readonly propertu which returns current index in the queue
            /// </summary>
            public T Current
            {
                get
                {
                    if (currentIndex == -1 || currentIndex >= queue.Count)
                        throw new InvalidOperationException();
                    return queue.arr[currentIndex];
                }
            }
            #endregion

            #region ctor
            internal MyQueueEnumerator(MyQueue<T> queue)
            {
                this.queue = queue;
                currentIndex = queue.head - 1;
                count = default(int);
            }
            #endregion

            #region public methods
            /// <summary>
            /// moves to the next element in the queue
            /// </summary>
            /// <returns>true in case existing next element else false</returns>
            public bool MoveNext()
            {
                if (count++ < queue.Count)
                {
                    currentIndex = (currentIndex + 1) % queue.Capacity;
                    return true;
                }
                this.Reset();
                return false;
            }

            /// <summary>
            /// return to initial state
            /// </summary>
            public void Reset() => currentIndex = queue.head - 1;
            #endregion
        }
        #endregion
    }
}