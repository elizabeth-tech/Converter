using System;
using System.Globalization;

namespace Converter
{
    class DecimalToP1: Converter
    {
        // Преобразование целой части числа в новую систему счисления
        protected override string ConvertIntegerPart(string number, byte basis)
        {
            UInt64 integerPart = UInt64.Parse(number);
            string ipcn = "";
            do
            {
                ipcn = CharOfDigit((byte)(integerPart % basis)) + ipcn;
                integerPart /= basis;
            }
            while (integerPart > 0);
            return ipcn;
        }

        // Преобразование дробной части числа в новую систему счисления
        protected override string ConvertFractionPart(string number, byte basis)
        {
            double tmp;
            UInt64 tmp3;
            number = "0" + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator + number;
            double fraction_part = double.Parse(number);
            string result = String.Empty;

            int k = ((number.Length) * 10) / basis;
            double kk = ((number.Length) * 10.0) / basis;
            if (kk > 0 && kk < 1)
                k = 1;
            while (k > 0)
            {
                tmp = fraction_part * basis;
                tmp3 = System.Convert.ToUInt64(tmp);
                if (tmp3 > tmp)
                    tmp3--;
                fraction_part = tmp - tmp3;
                result += CharOfDigit((byte)tmp3);
                k--;
            }
            while (result.Length > 0 && result[result.Length - 1] == '0')
                result = result.Substring(0, result.Length - 1);

            return result;
        }


    }

}
