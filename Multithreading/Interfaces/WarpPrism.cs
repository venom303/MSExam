using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Interfaces
{
    public class WarpPrism : IEnumerable<ProtossUnit>
    {
        private ProtossUnit[] list = new ProtossUnit[8];

        private sbyte currentIndex = -1;
        private sbyte lastElementIndex = 0;

        public void Add(ProtossUnit unit)
        {
            if (unit.Size + currentIndex-1 > 8)
                throw new Exception("Not enough space");
            list[lastElementIndex] = unit;
            lastElementIndex += unit.Size;
        }

        public IEnumerator<ProtossUnit> GetEnumerator()
        {
            return new WarpPrismEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new WarpPrismEnumerator(this);
        }

        private class WarpPrismEnumerator : IEnumerator<ProtossUnit>
        {
            private WarpPrism _warpPrism;

            public WarpPrismEnumerator(WarpPrism warpPrism)
            {
                _warpPrism = warpPrism;
            }

            public ProtossUnit Current
            {
                get { return _warpPrism.list[_warpPrism.currentIndex]; }
            }

            public void Dispose()
            {
                _warpPrism.list = null;
            }

            public bool MoveNext()
            {
                if (_warpPrism.currentIndex >= 8)
                    return false;
                if (_warpPrism.currentIndex == -1)
                    _warpPrism.currentIndex = 0;
                else
                    _warpPrism.currentIndex += Current.Size;
                if (_warpPrism.currentIndex >= 8)
                    return false;
                return true;
            }

            public void Reset()
            {
                _warpPrism.currentIndex = 0;
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}
