using System.Collections;
using System.Collections.Generic;

var key = "Faridun";
var value = new Person("Faridun", "Berdiev");

Hashtable<string, Person> hashtable = new Hashtable<string, Person>();
hashtable[key] = value;

var person = hashtable[key];

Console.WriteLine(person);

record Person(string Name, string Surname);

class HashtableKeyValuePair<K, V>
{
    public K Key { get; set; }
    public V Value { get; set; }

    public HashtableKeyValuePair(K key, V value)
    {
        Key = key;
        Value = value;
    }
}

class Hashtable<K, V> : IEnumerable<HashtableKeyValuePair<K, V>>
{
    private readonly int _maxCount = 0;
    private readonly HashtableKeyValuePair<K, V>[] _keyValuePairArray;

    public Hashtable(int maxCount = 1000)
    {
        _maxCount = maxCount;
        _keyValuePairArray = new HashtableKeyValuePair<K, V>[_maxCount];
    }

    public V this[K key]
    {
        get => Get(key);
        set => Put(key, value);
    }

    public IEnumerator<HashtableKeyValuePair<K, V>> GetEnumerator()
    {
        foreach(var pair in _keyValuePairArray)
            yield return pair;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public V Get(K key)
    {
        if(_keyValuePairArray is null || _maxCount == 0)
            return default;

        var hashCodeIndex = GetKeyHashCode(key);
        return _keyValuePairArray[hashCodeIndex].Value;
    }

    public void Put(K key, V value)
    {
        if(_keyValuePairArray is null || _maxCount == 0)
            return;
        
        var hashCodeIndex = GetKeyHashCode(key);
        var hashtableKeyValuePair = _keyValuePairArray[hashCodeIndex];
        if(hashtableKeyValuePair is null)
        {
            _keyValuePairArray[hashCodeIndex] = new HashtableKeyValuePair<K, V>(key, value);
            return;
        }

        if(EqualityComparer<K>.Default.Equals(hashtableKeyValuePair.Key, key))
        {
            hashtableKeyValuePair.Value = value;
        }
    }

    private int GetKeyHashCode(K key) => Math.Abs(key.GetHashCode() % _maxCount);   
}
