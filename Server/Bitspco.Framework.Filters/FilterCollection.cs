using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters
{
    public class FilterCollection<T> : IList<T> where T : IFilter
    {
        private List<T> filters = new List<T>();
        public FilterContext Context { get; } = new FilterContext();
        public int Count => filters.Count;

        public bool IsReadOnly => false;

        public T this[int index] { get { return filters[index]; } set { filters[index] = value; } }

        public FilterCollection() {}
        public void BeginExecute(int frameBack = 1, object[] arguments = null)
        {
            StackTrace stackTrace = new StackTrace();
            Context .Method = stackTrace.GetFrame(frameBack).GetMethod();
            Context.Parameters.Clear();
            if(arguments?.Length > 0)
                foreach (var item in Context.Method.GetParameters())
                    if (arguments.Length > item.Position)
                        Context.Parameters[item.Name] = arguments[item.Position];
            foreach (var item in filters) item.BeginExecute(Context);
        }
        public void EndExecute()
        {
            foreach (var item in filters) item.EndExecute(Context);
        }
        public void Add(T filter)
        {
            filters.Add(filter);
        }

        public int IndexOf(T item)
        {
            return filters.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            filters.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            throw new Exception("You Can't Remove Filters");
        }

        public void Clear()
        {
            throw new Exception("You Can't Clear Filters");
        }

        public bool Contains(T item)
        {
            return filters.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            filters.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            throw new Exception("You Can't Remove Filter");
        }

        public IEnumerator<T> GetEnumerator()
        {
            return filters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return filters.GetEnumerator();
        }
    }
}
