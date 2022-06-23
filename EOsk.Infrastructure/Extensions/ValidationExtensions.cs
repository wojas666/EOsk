using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace EOsk.Infrastructure.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBePesel<T>(this IRuleBuilder<T, string> pesel)
        {
            bool isPesel(string input) => IsPesel(input);

            return pesel.Must(isPesel);
        }

        /// <summary>
        /// This method checks if the string entered is a pesel number.The method uses the following formula:
        /// 1*a + 3*b + 7*c + 9*d + 1*e + 3*f + 7*g + 9*h + 1*i + 3*j, where the letters a through j stand for consecutive digits of the PESEL number.
        /// From the result, the last digit is taken and subtracted from the number 10.
        /// </summary>
        /// <param name="input">string entered ( pesel number )</param>
        /// <returns>true if the string entered is a valid identity number, false if not.</returns>
        private static bool IsPesel(string input)
        {
            const int peselLength = 11;
            int peselControl = 0;

            int[] peselCheckValues = new int[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

            if (string.IsNullOrEmpty(input))
                return false;

            if (!long.TryParse(input, out _))
                return false;

            if (input.Length != peselLength)
                return false;

            for(int i = 0; i < peselCheckValues.Length; i++)
            {
                var currentNumber = (short)Char.GetNumericValue(input, i);
                peselControl += currentNumber * peselCheckValues[i];
            }

            var stringPeselControl = peselControl.ToString();
            var lastNumber = Char.GetNumericValue(stringPeselControl, stringPeselControl.Length - 1);
            lastNumber = 10 - lastNumber;

            if (lastNumber == Char.GetNumericValue(input, peselLength-1))
                return true;
            else
                return false;
        }
    }
}
