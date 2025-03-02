﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Gma.DataStructures.StringSearch.Word
{
    
    
    internal class Node<T>
    {
        private readonly IDictionary<int, Edge<T>> _edges;
        private readonly HashSet<T> _data;

        public Node()
        {
            _edges = new IntDictionary<T>();
            Suffix = null;
            _data = new HashSet<T>();
        }

       

        public IEnumerable<T> GetData()
        {
            // TODO: Improve performance here
            var childData = _edges.Values.Select((e) => e.Target).SelectMany((t) => t.GetData());
            return _data.Concat(childData).Distinct();
        }

        public void AddRef(T value)
        {
            if (_data.Contains(value))
                return;

            _data.Add(value);
            //  add this reference to all the suffixes as well
            var iter = Suffix;
            while (iter != null)
            {
                if (iter._data.Contains(value))
                    break;

                iter.AddRef(value);
                iter = iter.Suffix;
            }
        }

        public void AddEdge(int ch, Edge<T> e)
        {
            _edges[ch] = e;
        }

        public Edge<T> GetEdge(int ch)
        {
            Edge<T> result;
            _edges.TryGetValue(ch, out result);
            return result;
        }

        public Node<T> Suffix { get; set; }
    }
}