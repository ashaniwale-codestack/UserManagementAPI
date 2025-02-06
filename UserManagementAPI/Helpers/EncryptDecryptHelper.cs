using System.Text;

namespace UserManagementAPI.Helpers;

public class EncryptDecryptHelper
{
    public static string EncryptData(string password)
    {
        byte[] encode = new byte[password.Length];
        encode = Encoding.UTF8.GetBytes(password);
        string strmsg = Convert.ToBase64String(encode);
        return strmsg;
    }

    public static string Decryptdata(string encryptpwd)
    {
        UTF8Encoding encodepwd = new UTF8Encoding();
        Decoder Decode = encodepwd.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
        int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        return new string(decoded_char);
    }
}
