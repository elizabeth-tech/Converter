namespace Converter
{
    class Control
    {
        public static string DoConversion(string str, byte p1, byte p2)
        {
            bool is_negative = false;
            if (str[0] == '-') // проверка на знак
            { 
                str = str.Substring(1); // отрезать первый символ
                is_negative = true;
            }

            // конвертация двумя проходами
            if (p1 != 10)
            {
                var toDecimal = new P1ToDecimal();
                str = toDecimal.Convert(str, p1);
            }

            if (p2 != 10)
            {
                var fromDecimal = new DecimalToP1();
                str = fromDecimal.Convert(str, p2);
            }

            // вывод результата
            if (is_negative)
                str = "-" + str;
            return str;
        }

    }
}
