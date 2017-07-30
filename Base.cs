using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public abstract class EnumerableBase<T> : IEnumerable<T> {
        public EnumerableBase() {
        }

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return GetEnumerator();
        }
        #endregion

        protected abstract IEnumerator<T> GetEnumerator();
    }

    public class SimpleSet<T> : EnumerableBase<T> {
        int setSize;
        readonly T[] setCore;

        public SimpleSet(int maximalSize) {
            this.setSize = 0;
            this.setCore = new T[maximalSize];
        }
        public int AddItem(T item) {
            this.setCore[this.setSize] = item;
            return this.setSize++;
        }
        public void UpdateItem(int itemIndex, T item) {
            this.setCore[itemIndex] = item;
        }
        public void Clear() {
            this.setSize = 0;
        }
        public int Size {
            get { return setSize; }
        }
        #region IEnumerable
        protected override IEnumerator<T> GetEnumerator() {
            for(int n = 0; n < Size; n++)
                yield return setCore[n];
        }
        #endregion
        protected T[] SetCore { get { return setCore; } }
    }
}
