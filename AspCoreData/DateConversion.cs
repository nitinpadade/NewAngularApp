using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreData
{
    public static class DateConversion
    {
        public static DateTime? ToDate(this string value)
        {

            DateTime? convertedDate = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        string[] dateParts = value.Split('/');
                        convertedDate = new DateTime(int.Parse(dateParts[2]), int.Parse(dateParts[1]), int.Parse(dateParts[0]));
                    }
                    return convertedDate;
                }
                catch (Exception ex)
                {
                    return Convert.ToDateTime(value);
                }
            }
            catch
            {
                return convertedDate;
            }
        }
    }
}
