using System;


namespace Assets.Scripts.Utils {
    public class Pointer<T> {

        private readonly Func<T> _getter;
        private readonly Action<T> _setter;


        public Pointer(Func<T> getter, Action<T> setter) {
            _getter = getter;
            _setter = setter;
        }


        public T Dereference { get { return _getter(); } set { _setter(value); } }
    }
}
