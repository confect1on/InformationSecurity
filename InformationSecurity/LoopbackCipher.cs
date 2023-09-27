namespace InformationSecurity;

public class LoopbackCipher
{
    private readonly GronsfeldCipher _gronsfeldCipher;

    public LoopbackCipher(GronsfeldCipher gronsfeldCipher)
    {
        _gronsfeldCipher = gronsfeldCipher;
    }

    public string Encode(string source, IEnumerable<int[]> keys)
    {
        return keys.Aggregate(source, (current, key) => _gronsfeldCipher.Encode(current, key));
    }

    public string Decode(string cipher, IEnumerable<int[]> keys)
    {
        return keys.Aggregate(cipher, (current, key) => _gronsfeldCipher.Decode(current, key));
    }
}