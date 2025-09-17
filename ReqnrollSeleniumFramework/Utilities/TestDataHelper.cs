namespace ReqnrollSeleniumFramework.Utilities
{
    /**
    * Utility class for generating test data dynamically.
    */
    public class TestDataHelper
    {
        /** Generate dynamic data*/
        public static string GenerateLargePayload(int lengthA, int length1, int lengthSpecial, int lengthHtml)
        {
            return new string('A', lengthA) +
                   new string('1', length1) +
                   "!@#$%^&*()_+|}{:?>".PadRight(lengthSpecial, '!') +
                   "<select>alert('x')".PadRight(lengthHtml, '<');
        }
    }
}
