using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Data_Structures_and_Algorithms {
    public class SquareMatrix<T> {
        T[,] data;
        int size;
        int capacity;

        public SquareMatrix()
            : this(4) {
        }
        public SquareMatrix(int capacity) {
            this.size = 0;
            this.capacity = Math.Max(4, capacity);
            this.data = new T[Capacity, Capacity];
        }

        public void EnsureSize(int size) {
            Guard.IsPositive(size, nameof(size));
            if(size > Capacity) {
                int _capacity = this.capacity * 2;
                if(size > _capacity)
                    _capacity = size * 2;
                T[,] _data = new T[_capacity, _capacity];
                for(int i = 0; i < Capacity; i++) {
                    for(int j = 0; j < Capacity; j++) {
                        _data[i, j] = data[i, j];
                    }
                }
                this.data = _data;
                this.capacity = _capacity;
            }
            this.size = size;
        }
        public T this[int x, int y] {
            get {
                Guard.IsInRange(x, 0, Size - 1, nameof(x));
                Guard.IsInRange(y, 0, Size - 1, nameof(y));
                return data[x, y];
            }
            set {
                Guard.IsInRange(x, 0, Size - 1, nameof(x));
                Guard.IsInRange(y, 0, Size - 1, nameof(y));
                data[x, y] = value;
            }
        }
        
        internal int Capacity {
            get { return capacity; }
        }

        public int Size { get { return size; } }
    }
}
