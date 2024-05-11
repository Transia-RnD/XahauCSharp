using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Xahau.Utils
{
	public class HookUtilss
	{
		//public static readonly List<string> TRANSACTION_TYPES = TRANSACTION_TYPE_MAP.Keys.ToList();

		//public static string CalculateHookOn(List<string> transactionTypes)
		//{
		//	BigInteger hash = BigInteger.Parse("ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffbfffff", System.Globalization.NumberStyles.HexNumber);
		//	foreach (var nth in transactionTypes)
		//	{
		//		if (!TRANSACTION_TYPES.Contains(nth))
		//		{
		//			throw new XrplError($"Invalid transaction type '{nth}' in HookOn array");
		//		}

		//		int index = TRANSACTION_TYPE_MAP[nth];
		//		hash ^= BigInteger.One << index;
		//	}

		//	string hashHex = hash.ToString("X").PadLeft(64, '0');
		//	return hashHex;
		//}

		//public static bool IsHex(string value)
		//{
		//	return System.Text.RegularExpressions.Regex.IsMatch(value, "^[0-9A-F]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		//}

		//public static string HexValue(string value)
		//{
		//	var bytes = Encoding.UTF8.GetBytes(value);
		//	return BitConverter.ToString(bytes).Replace("-", string.Empty).ToUpper();
		//}

		//public static List<HookParameter> HexHookParameters(List<HookParameter> data)
		//{
		//	var hookParameters = new List<HookParameter>();
		//	foreach (var parameter in data)
		//	{
		//		string hookPName = parameter.HookParameterName;
		//		string hookPValue = parameter.HookParameterValue;

		//		if (!IsHex(hookPName))
		//		{
		//			hookPName = HexValue(hookPName);
		//		}

		//		if (!IsHex(hookPValue))
		//		{
		//			hookPValue = HexValue(hookPValue);
		//		}

		//		hookParameters.Add(new HookParameter
		//		{
		//			HookParameterName = hookPName,
		//			HookParameterValue = hookPValue
		//		});
		//	}
		//	return hookParameters;
		//}
	}
}

