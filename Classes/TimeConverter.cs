using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            try
            {   
                int[] parsedTime = Array.ConvertAll(aTime.Split(':'), int.Parse);

                if (parsedTime.Length != 3)
                    throw new Exception("The time is not valid. It should follow the HH:mm:ss format.");

                string convertedTime;
                convertedTime = GetConvertedSeconds(parsedTime[2]) + Environment.NewLine;
                convertedTime += GetConvertedHours(parsedTime[0]) + Environment.NewLine;
                convertedTime += GetConvertedMinutes(parsedTime[1]);

                return convertedTime;
            }
            catch(Exception ex)
            {
                //Here the exception should be handled (logged, create an alert on SCOM or other data center monitoring system, etc)
                //Also, there should be more exception types defined (for invalid time format, invalid time values, etc)

                return String.Empty;
            }
        }

        private string GetConvertedMinutes(int minutes)
        {
            if (minutes < 0 || minutes > 59)
                throw new Exception("The minutes are invalid, they should be a number between 0 and 59.");

            string convertedMinutes;

            int firstYellowRow = minutes / 5;
            string lampsInFirstYellowRow = new string('Y', firstYellowRow) + new string('O', 11 - firstYellowRow);
            convertedMinutes = lampsInFirstYellowRow.Replace("YYY", "YYR") + Environment.NewLine;

            int secondYellowRow = minutes % 5;
            string lampsInSecondYellowRow = new string('Y', secondYellowRow) + new string('O', 4 - secondYellowRow);
            convertedMinutes += lampsInSecondYellowRow;

            return convertedMinutes;
        }

        private string GetConvertedHours(int hours)
        {
            if (hours < 0 || hours > 59)
                throw new Exception("The hours are invalid, they should be a number between 0 and 24.");

            string convertedHours;

            int firstRedRow = hours / 5;
            string lampsInFirstRedRow = new string('R', firstRedRow) + new string('O', 4 - firstRedRow);
            convertedHours = lampsInFirstRedRow + Environment.NewLine;

            int secondRedRow = hours % 5;
            string lampsInSecondRedRow = new string('R', secondRedRow) + new string('O', 4 - secondRedRow);
            convertedHours += lampsInSecondRedRow;

            return convertedHours;
        }

        private string GetConvertedSeconds(int seconds)
        {
            if (seconds < 0 || seconds > 59)
                throw new Exception("The seconds are invalid, they should be a number between 0 and 59.");

            return seconds % 2 == 0 ? "Y" : "O";
        }
    }
}
