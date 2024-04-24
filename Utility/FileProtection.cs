using System.IO;
using System.Text;
using System.Security.Cryptography;

public class FileProtection
{
    // Encrypting file name
    public string MD5HashFunc(string str)
    {
        StringBuilder MD5Str = new StringBuilder();
        byte[] byteArr = Encoding.ASCII.GetBytes(str);
        byte[] resultArr = (new MD5CryptoServiceProvider()).ComputeHash(byteArr);
    
        for (int cnti = 0; cnti < resultArr.Length; cnti++)
        {
            MD5Str.Append(resultArr[cnti].ToString("X2"));
        }
      
        return MD5Str.ToString();
    }
    
    // Encryption
    public byte[] AESEncrypt256(byte[] encryptData, String password)
    {
        AESEncrypt aes = new AESEncrypt();
        return aes.AESEncrypt256(encryptData, password);
    }
    
    // Decryption
    public byte[] AESDecrypt256(byte[] decryptData, String password)
    {
        AESEncrypt aes = new AESEncrypt();
        return aes.AESDecrypt256(decryptData, password);
    }
  
  
    // Example Using
    public void Save(string path, string saltKey, Data data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
      
        if (useEncrypt)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
            byte[] encryptData = UtilityManager.Instance.AESEncrypt256(byteData, saltKey);
            File.WriteAllBytes(path, encryptData);
        }
        else
        {
            File.WriteAllText(path, jsonData);
        }
    }
}

class AESEncrypt
{
    private SHA256Managed sha256Managed = new SHA256Managed();
    private RijndaelManaged aes = new RijndaelManaged();

    public AESEncrypt()
    {
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
    }

    // AES_256 Encryption
    public byte[] AESEncrypt256(byte[] encryptData, String password)
    {
        // Salt는 비밀번호의 길이를 SHA256 해쉬값으로 한다.
        var salt = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(password.Length.ToString()));

        // PBKDF2(Password-Based Key Derivation Function)
        // 반복은 2번
        var PBKDF2Key = new Rfc2898DeriveBytes(password, salt, 2);
        var secretKey = PBKDF2Key.GetBytes(aes.KeySize / 8);
        var iv = PBKDF2Key.GetBytes(aes.BlockSize / 8);

        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(secretKey, iv), CryptoStreamMode.Write))
            {
                cs.Write(encryptData, 0, encryptData.Length);
            }
            xBuff = ms.ToArray();
        }
        return xBuff;
    }

    // AES_256 Decryption
    public byte[] AESDecrypt256(byte[] decryptData, String password)
    {
        // Salt는 비밀번호의 길이를 SHA256 해쉬값으로 한다.
        var salt = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(password.Length.ToString()));

        // PBKDF2(Password-Based Key Derivation Function)
        // 반복은 2번
        var PBKDF2Key = new Rfc2898DeriveBytes(password, salt, 2);
        var secretKey = PBKDF2Key.GetBytes(aes.KeySize / 8);
        var iv = PBKDF2Key.GetBytes(aes.BlockSize / 8);

        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(secretKey, iv), CryptoStreamMode.Write))
            {
                cs.Write(decryptData, 0, decryptData.Length);
            }
            xBuff = ms.ToArray();
        }
        return xBuff;
    }
}
