namespace AVS.SpotifyMusic.Domain.Core.Utils
{
    public static class StringExtension
    {
        public static string ToOnlyNumber(this string str, string input) 
        {  
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
