using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Data_Structures_and_Algorithms {
    public class BitMatrix {
        byte[] data;
        int size;
        int capacity;

        public BitMatrix()
            : this(Granularity) {
        }
        public BitMatrix(int capacity) {
            this.size = 0;
            this.capacity = CheckCapacity(capacity);
            this.data = CreateDataBlock(Capacity);
        }
        public bool this[int x, int y] {
            get {
                Guard.IsInRange(x, 0, Size - 1, nameof(x));
                Guard.IsInRange(y, 0, Size - 1, nameof(y));
                byte slot = GetSlot(x, y, false);
                return ((slot >> (y % 8)) & 0x1) == 1;
            }
            set {
                Guard.IsInRange(x, 0, Size - 1, nameof(x));
                Guard.IsInRange(y, 0, Size - 1, nameof(y));
                byte slot = GetSlot(x, y, false);
                if(value) {
                    slot |= (byte)(0x1 << (y % 8));
                }
                else {
                    slot &= (byte)(~(0x1 << (y % 8)));
                }
                SetSlot(x, y, slot, false);
            }
        }
        public void EnsureSize(int size) {
            Guard.IsPositive(size, nameof(size));
            if(size > Capacity) {
                int _capacity = this.capacity * 2;
                if(size > _capacity)
                    _capacity = CheckCapacity(size * 2);
                byte[] _data = CreateDataBlock(_capacity);
                for(int i = 0; i < Capacity; i++) {
                    Array.Copy(data, i * Capacity / 8, _data, i * _capacity / 8, Capacity / 8);
                }
                this.data = _data;
                this.capacity = _capacity;
            }
            this.size = size;
        }
        public int Size { get { return size; } }

        internal void SetSlot(int x, int y, byte value, bool checkArgs) {
            if(checkArgs) {
                Guard.IsInRange(x, 0, Size - 1, nameof(x));
                Guard.IsInRange(y, 0, Size - 1, nameof(y));
            }
            data[GetSlotIndex(x, y, Capacity)] = value;
        }
        internal byte GetSlot(int x, int y, bool checkArgs) {
            if(checkArgs) {
                Guard.IsInRange(x, 0, Size - 1, nameof(x));
                Guard.IsInRange(y, 0, Size - 1, nameof(y));
            }
            return this.data[GetSlotIndex(x, y, Capacity)];
        }
        static int GetSlotIndex(int x, int y, int matrixSize) {
            return x * matrixSize / 8 + y / 8;
        }

        static byte[] CreateDataBlock(int capacity) {
            int sz = capacity * capacity / 8;
            return new byte[sz];
        }

        internal int Capacity {
            get { return capacity; }
        }
        internal static int CheckCapacity(int capacity) {
            Guard.IsPositive(capacity, nameof(capacity));
            if(capacity <= Granularity) return Granularity;
            if(Utils.IsPowOfTwo(capacity)) return capacity;
            return Utils.GetPowOfTwo(capacity) * 2;
        }
        internal static readonly int Granularity = 8;
    }
}
