const compose = (... funcs) => (comp) => {
    return funcs.reducerRight(
        (wrapped, f) = f(wrapped), comp);
    };

 export default compos;