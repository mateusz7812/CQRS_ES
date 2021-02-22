using System;

namespace Optionals
{
    public class Optional<T>
    {
        private readonly T _item;

        public T Item
        {
            get => Code.CodesNumbers == CodesNumbers.Success
                ? _item
                : throw new Exception(Code.CodeMessage);
            init => _item = value;
        }

        public Code Code { get; init; }

        public static implicit operator T(Optional<T> optional)
        {
            return optional.Item;
        }

        public static implicit operator Optional<T>(T item)
        {
            return new() {Item = item, Code = Codes.Success};
        }

        public static implicit operator Optional<T>(Code code)
        {
            if (code == Codes.Success) 
                throw new NotImplementedException();
            return new Optional<T> {Code = code};
        }
    }
}