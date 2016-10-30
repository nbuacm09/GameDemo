public delegate void BaseDelegate (); 
public delegate void BaseDelegateV<T> (T val); 
public delegate void BaseDelegateV<T, U> (T val0, U val1); 
public delegate void BaseDelegateV<T, U, V> (T val0, U val1, V val2); 
public delegate T BaseDelegate<T, U> (U val); 
public delegate void DataProcessDelegate<T> (ref T val);
public delegate void DataProcessDelegate<T, U> (ref T val, U extraInfo);
