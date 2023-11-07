using System.Linq;
using TMPro;
using UnityEngine;

namespace LOLVR.Helper
{
    public static class Extension
    {
        public static string ToFirstUpper(this string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : $"{input[0].ToString().ToUpper()}{input[1..]}";
        }

        public static string GetSelectedText(this TMP_Dropdown dropdown) => dropdown.options[dropdown.value].text;

        public static void SetSelectedByText(this TMP_Dropdown dropdown, string value)
        {
            TMP_Dropdown.OptionData optionData = dropdown.options.FirstOrDefault(op => op.text == value);

            if (optionData != null)
            {
                dropdown.value = dropdown.options.IndexOf(optionData);
            }
            else
            {
                Debug.LogError($"Couldn't find {value} inside options of {dropdown}");
            }
        }
    }
}
