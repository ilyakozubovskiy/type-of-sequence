using System;

namespace Numbers
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Obtains formalized information in the form of an enum <see cref="ComparisonSigns"/>
        /// about the relationship of the order of two adjacent digits for all digits of a given number. 
        /// </summary>
        /// <param name="number">Source number.</param>
        /// <returns>Information in the form of an enum <see cref="ComparisonSigns"/>
        /// about the relationship of the order of two adjacent digits for all digits of a given number
        /// or null if the information is not defined.</returns>
        public static ComparisonSigns? GetTypeComparisonSigns(this long number)
        {
            ComparisonSigns comparisonSigns = 0;

            if (number == long.MinValue)
            {
                number = long.MaxValue;
            }

            int length = 1;
            long tmp = number;

            while ((tmp /= 10) >= 1)
            {
                length++;
            }

            long[] digitArray = new long[length];
            for (int i = 1; number > 0; number /= 10, i++)
            {
                digitArray[^i] = number % 10;
            }

            for (int i = 0; i < digitArray.Length - 1; i++)
            {
                long nextDigit = digitArray[i + 1];
                switch (digitArray[i])
                {   
                    case long digit when digit < nextDigit:
                        comparisonSigns |= ComparisonSigns.LessThan;
                        break;
                    case long digit when digit > nextDigit:
                        comparisonSigns |= ComparisonSigns.MoreThan;
                        break;
                    case long digit when digit == nextDigit:
                        comparisonSigns |= ComparisonSigns.Equals;
                        break;
                }
            }

            if (comparisonSigns > 0)
            {
                return comparisonSigns;
            }

            return null;
        }

        /// <summary>
        /// Gets information in the form of a string about the type of sequence that the digit of a given number represents.
        /// </summary>
        /// <param name="number">Source number.</param>
        /// <returns>The information in the form of a string about the type of sequence that the digit of a given number represents.</returns>
        public static string GetTypeOfDigitsSequence(this long number)
        {
            return GetTypeComparisonSigns(number) switch
            {
                null => "One digit number.",
                ComparisonSigns.LessThan => "Strictly Increasing.",
                ComparisonSigns.MoreThan => "Strictly Decreasing.",
                ComparisonSigns.Equals => "Monotonous.",
                ComparisonSigns.LessThan | ComparisonSigns.Equals => "Increasing.",
                ComparisonSigns.MoreThan | ComparisonSigns.Equals => "Decreasing.",
                ComparisonSigns.LessThan | ComparisonSigns.MoreThan => "Unordered.",
                ComparisonSigns.LessThan | ComparisonSigns.MoreThan | ComparisonSigns.Equals => "Unordered.",
                _ => null
            };
        }
    }
}
