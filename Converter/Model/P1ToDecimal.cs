using System;

namespace Converter
{
    class P1ToDecimal: Converter
    {

        // Преобразование целой части числа в новую систему счисления
        protected override string ConvertIntegerPart(string number, byte scale)
        {
            UInt64 sum = 0, pr = 1;
            for (int i = number.Length - 1; i >= 0; i--, pr *= scale)
                sum += (UInt64)DigitOfChar(number[i]) * pr;
            return sum.ToString();
        }

        // Преобразование дробной части числа в новую систему счисления
        protected override string ConvertFractionPart(string number, byte scale)
        {
            double sum = 0, pr = 1.0 / scale;
            for (int i = 0; i < number.Length; i++, pr /= scale)
                sum += (UInt64)DigitOfChar(number[i]) * pr;
            return sum.ToString().Substring(2); // cut "0," at the beginning
        }
    }
}
