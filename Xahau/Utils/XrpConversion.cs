﻿using System;
using System.Numerics;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Globalization;
using Xahau.Client.Exceptions;


// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/utils/xrpConversion.ts

namespace Xahau.Utils
{
    public static partial class BigIntegerExtensions
    {
        // this have to be used for extend BigInteger
        public static String ToRadixString(this BigInteger value, int radix)
        {
            if (radix <= 1 || radix > 36)
                throw new ArgumentOutOfRangeException(nameof(radix));
            if (value == 0)
                return "0";

            bool negative = value < 0;

            if (negative)
                value = -value;

            StringBuilder sb = new StringBuilder();

            for (; value > 0; value /= radix)
            {
                int d = (int)(value % radix);

                sb.Append((char)(d < 10 ? '0' + d : 'A' - 10 + d));
            }

            return (negative ? "-" : "") + string.Concat(sb.ToString().Reverse());
        }
        // this have to be used for extend BigInteger
        public static String ToRadixString(this double value, int radix)
        {
            if (radix <= 1 || radix > 36)
                throw new ArgumentOutOfRangeException(nameof(radix));
            if (value == 0)
                return "0";

            bool negative = value < 0;

            if (negative)
                value = -value;

            StringBuilder sb = new StringBuilder();

            for (; value > 0; value /= radix)
            {
                int d = (int)(value % radix);

                sb.Append((char)(d < 10 ? '0' + d : 'A' - 10 + d));
            }

            return (negative ? "-" : "") + string.Concat(sb.ToString().Reverse());
        }
    }

    // XRP Conversion
    public static class XrpConversion
    {

        private static double DROPS_PER_XRP = 1000000.0;
        private static int MAX_FRACTION_LENGTH = 6;
        private static int BASE_TEN = 10;
        private static string SANITY_CHECK = "/ ^-?[0 - 9.] +$/u";

        /// <summary>
        /// Convert Drops to XRP.
        /// </summary>
        /// <param name="dropsToConvert"> Drops to convert to XRP. This can be a string, number, or BigNumber.</param>
        /// <returns
        public static string DropsToXrp(double dropsToConvert)
        {
            return DropsToXrp(dropsToConvert.ToString());
        }

        /// <summary>
        /// Convert Drops to XRP.
        /// </summary>
        /// <param name="dropsToConvert"> Drops to convert to XRP. This can be a string, number, or BigNumber.</param>
        /// <returns
        public static string DropsToXrp(string dropsToConvert)
        {
            /*
            * Converting to BigNumber and then back to string should remove any
            * decimal point followed by zeros, e.g. '1.00'.
            * Important: specify base BASE_10 to avoid exponential notation, e.g. '1e-7'.
            */
            string drops = BigInteger.Parse(dropsToConvert, NumberStyles.AllowLeadingSign
                    | (NumberStyles.AllowLeadingSign & NumberStyles.AllowDecimalPoint)
                    | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent)
                    | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                    | (NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                    | NumberStyles.AllowExponent
                    | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture).ToRadixString(BASE_TEN);
            // check that the value is valid and actually a number
            if (!(dropsToConvert is string) && drops != null)
            {
                throw new ValidationException($"dropsToXrp: invalid value '{dropsToConvert}', should be a BigNumber or string - encoded number.");
            }

            // drops are only whole units
            if (drops.Contains("."))
            {
                throw new ValidationException("dropsToXrp: value '${drops}' has too many decimal places.");
            }

            /*
             * This should never happen; the value has already been
             * validated above. This just ensures BigNumber did not do
             * something unexpected.
             */
            //if (!SANITY_CHECK.exec(drops))
            //{
            //    throw new ValidationException(
            //        "dropsToXrp: failed sanity check -" +
            //        " value '${drops}'," +
            //        " does not match(^-?[0 - 9] +$)."
            //    );
            //}

            // TODO: SHOULD BE BASE 10
            return ((decimal)BigInteger.Parse(dropsToConvert, NumberStyles.AllowLeadingSign
                                                              | (NumberStyles.AllowLeadingSign & NumberStyles.AllowDecimalPoint)
                                                              | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent)
                                                              | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                                                              | (NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                                                              | NumberStyles.AllowExponent
                                                              | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) / (decimal)new BigInteger(DROPS_PER_XRP)).ToString();
            //return ((decimal)BigInteger.Parse(dropsToConvert, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) / (decimal)new BigInteger(DROPS_PER_XRP)).ToString("F"+BASE_TEN).TrimEnd('0');
        }

        /// <summary>
        /// Convert XRP to Drops.
        /// </summary>
        /// <param name="xrpToConvert"> XRP to convert to Drops. This can be a string, number, or BigNumber.</param>
        /// <returns
        public static string XrpToDrops(double xrpToConvert)
        {
            return XrpToDrops(xrpToConvert.ToString());
        }

        /// <summary>
        /// Convert an amount in XRP to an amount in drops.
        /// </summary>
        /// <param name="xrpToConvert">Amount in XRP.</param>
        /// <returns>Amount in drops.</returns>
        public static string XrpToDrops(string xrpToConvert)
        {
            // Important: specify base BASE_TEN to avoid exponential notation, e.g. '1e-7'.
            // TODO: SHOULD BE BASE 10
            string xrp = decimal.Parse(xrpToConvert, NumberStyles.AllowLeadingSign
                                                     | (NumberStyles.AllowLeadingSign & NumberStyles.AllowDecimalPoint)
                                                     | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent)
                                                     | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                                                     | (NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                                                     | NumberStyles.AllowExponent
                                                     | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture).ToString();
            // check that the value is valid and actually a number
            if (!(xrpToConvert is string) && xrp != null)
            {
                throw new ValidationException($"xrpToConvert: invalid value '{xrpToConvert}', should be a BigInteger or string - encoded number.");
            }

            // drops are only whole units

            string[] components = xrp.TrimEnd('0').Split('.');
            if (components.Length > 2)
            {
                throw new ValidationException("xrpToDrops: failed sanity check - value '${xrp}' has too many decimal points.");
            }
            string fraction = components.Length > 1 && components[1] != null ? components[1] : "0";
            if (fraction.Length > MAX_FRACTION_LENGTH)
            {
                throw new ValidationException($"xrpToDrops: value '{xrp}' has too many decimal places.");
            }
            // TODO: SHOULD BE BASE 10
            return new BigInteger(decimal.Parse(xrpToConvert, NumberStyles.AllowLeadingSign
                                                              | (NumberStyles.AllowLeadingSign & NumberStyles.AllowDecimalPoint)
                                                              | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent)
                                                              | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                                                              | (NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                                                              | NumberStyles.AllowExponent
                                                              | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * (decimal)new BigInteger(DROPS_PER_XRP)).ToString();
        }
    }
}