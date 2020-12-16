using System;

namespace Converter
{
    abstract class Converter
    {
        // Конвертация целой части числа.
        abstract protected string ConvertIntegerPart(string number, byte basis);

        // Конвертация дробной части числа.
        abstract protected string ConvertFractionPart(string number, byte basis);

        // Возвращает десятичное значение цифры в какой-либо другой системе счисления.
        protected byte DigitOfChar(char c)
        {
            if (c >= '0' && c <= '9')
                return (byte)(c - '0');
            if (c >= 'A' && c <= 'F')
                return (byte)(c - 'A' + 10);
            return 0;
        }

        // Возвращает цифру по её десятичному значению.
        protected char CharOfDigit(byte digit)
        {
            if (digit < 10)
                return digit.ToString()[0];
            else
                return (char)(digit - 10 + 'A');
        }

        // Конвертация числа из р1 в десятичную систему
        public string Convert(string number, byte basis)
        {
            string integer, fraction;
            SplitParts(number, out integer, out fraction); // выделение дробной и целой части числа

            string converted_number = ConvertIntegerPart(integer, basis); // конвертация для целой части

            if (!String.IsNullOrEmpty(fraction)) // если есть дробная часть то конвертация для дробной части
                converted_number += "." + ConvertFractionPart(fraction, basis);

            return converted_number;
        }


        // Выделение дробной и целой части из исходного числа.
        protected void SplitParts(string number, out string integer, out string fraction)
        {
            string[] ret = number.Split(".".ToCharArray(), 2, StringSplitOptions.None);
            integer = ret[0];
            fraction = (ret.Length > 1 ? ret[1] : String.Empty);
        }



    }
}
