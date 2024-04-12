using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LOLVR.Helper
{
    public class Map<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>
    {
        private readonly List<T1> list1 = new();
        private readonly List<T2> list2 = new();

        public Map() {}

        public Map(Dictionary<T1, T2> dict)
        {
            list1 = dict.Keys.ToList();

            foreach (T1 t1 in list1)
            {
                list2.Add(dict[t1]);
            }
        }

        public void Add(T1 t1, T2 t2)
        {
            list1.Add(t1);
            list2.Add(t2);
        }

        public int IndexOf(T1 t1) => list1.IndexOf(t1);

        public int IndexOf(T2 t2) => list2.IndexOf(t2);

        public int Count => list1.Count;

        public T2 this[T1 t1]
        {
            get => list2[IndexOf(t1)];
            set => list2[IndexOf(t1)] = value;
        }

        public T1 this[T2 t2]
        {
            get => list1[IndexOf(t2)];
            set => list1[IndexOf(t2)] = value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<KeyValuePair<T1, T2>>
        {
            private readonly Map<T1, T2> map;
            private int index;

            public Enumerator(Map<T1, T2> map)
            {
                this.map = map;
                index = 0;
                Current = new KeyValuePair<T1, T2>();
            }

            public KeyValuePair<T1, T2> Current { get; private set; }
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                for (; (uint) index < (uint) map.Count; ++index)
                {
                    Current = new KeyValuePair<T1, T2>(map.list1[index], map.list2[index]);
                        ++index;
                        return true;
                }
                index = map.Count + 1;
                Current = new KeyValuePair<T1, T2>();
                return false;
            }

            public void Reset()
            {
                index = 0;
                Current = new KeyValuePair<T1, T2>();
            }

            public void Dispose()
            {
            }
        }

    }
}
