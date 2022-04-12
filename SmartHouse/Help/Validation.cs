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
            int s = 7;
            while (s < passw.Length)
            {
                if (passw[s] >= 'А' && passw[s] <= 'Я' || passw[s] >= 'а' && passw[s] <= 'я' || ((passw[s] == '-' || passw[s] == ' ') && (passw[s] >= 'А' && passw[s] <= 'Я' || passw[s] >= 'а' && passw[s] <= 'я')))
                    s++;
                else return false;
            }
            return true;
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
