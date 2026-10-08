using System.Collections.Generic;

namespace Travelling
{
    public class RouteManager<T>
    {
        private List<T> items = new List<T>();
        public void AddItem(T item)
        {
            items.Add(item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override string ToString()
        {
            return string.Join(" -> ", items);
        }
    }
}