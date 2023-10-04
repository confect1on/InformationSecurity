namespace InformationSecurity.Ciphers;

public class LoopbackCipher
{
    private readonly GronsfeldCipher _gronsfeldCipher;

    public LoopbackCipher(GronsfeldCipher gronsfeldCipher)
    {
        _gronsfeldCipher = gronsfeldCipher;
    }

    public string Enciphering(string source, IEnumerable<int[]> keys)
    {
        return keys.Aggregate(source, (current, key) => _gronsfeldCipher.Enciphering(current, key));
    }

    public string Deciphering(string cipher, IEnumerable<int[]> keys)
    {
        return keys.Aggregate(cipher, (current, key) => _gronsfeldCipher.Deciphering(current, key));
    }
}