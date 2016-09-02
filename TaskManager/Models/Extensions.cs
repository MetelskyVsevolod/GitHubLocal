using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace TaskManager
{
    public static class Extensions
    {
        public static void createNewRow(this TableRow row, int cellCount, params string[] cellsTexts)
        {
            for (int i = 0; i < cellCount; i++)
            {
                TableCell tc = new TableCell();
                row.Cells.Add(tc);
                tc.HorizontalAlign = HorizontalAlign.Center;

                if (i < cellsTexts.Length)
                    tc.Text = cellsTexts[i];
                else
                    tc.Text = String.Empty;
            }
        }

        public static void addButtonToCell(this TableRow row, int cellIndex, Button button, string buttonText, EventHandler action)
        {
            button.Text = buttonText;
            button.Click += action;
            row.Cells[cellIndex].Controls.Add(button);
        }

        public static bool IsValidEmail(this string strIn)
        {
            if (String.IsNullOrEmpty(strIn))
                return false;

            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}