using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SmartHouse.Help
{
    class Validation
    {
        
        public static bool ValidatePassw(string passw)
        {
            int t = 0;
            int s = 7;
            if (passw.Length >= t)
            {
                for (int i = 0; i < passw.Length; i++)
                    if ((passw[i] >= '0') && (passw[i] <= '9'))
                    {
                        for (int d = 0; d < passw.Length; d++)
                            for (int j = 0; j < passw.Length; j++)
                                if ((passw[j] >= 'a') && (passw[j] <= 'z'))
                                {
                                    for (int k = 0; k < passw.Length; k++)
                                    {
                                        if ((passw[k] >= 'A') && (passw[k] <= 'Z'))
                                            return true;
                                    }
                                    return false;
                                }
                        return false;
                    }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
