using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissorsGenericGame;

internal class HmacCalculatorService
{
    private readonly byte[] _cryptoKey = [];
    private byte[] _hmac = [];
    public HmacCalculatorService(int numberOfBits = 256)
    {
        if (numberOfBits % 8 != 0)
            throw new ArgumentException("Number of bits must be divisible by 8.");

        int numberOfBytes = numberOfBits / 8;
        this._cryptoKey = new byte[numberOfBytes];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(_cryptoKey);
        }
    }

    public void CalculateHmacForMessage(string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);

        using (var hmac = new HMACSHA256(_cryptoKey))
        {
            _hmac =  hmac.ComputeHash(messageBytes);
        }
    }

    public string GetSecretKeyAsString()
    {
        return BitConverter.ToString(_cryptoKey).Replace("-", "");
    }

    public string GetHmacAsString()
    {
        return BitConverter.ToString(_hmac).Replace("-", "");
    }
}
