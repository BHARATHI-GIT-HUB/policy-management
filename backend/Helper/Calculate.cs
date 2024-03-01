using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace RepositryAssignement.Helper
{
    public class Calculate
    {
        public double agePercentage(int age) {

            double premiumFactor = 0.0;

            if (age > 60)
            {
                premiumFactor = 0.5;
            }
            else if (age > 50)
            {
                premiumFactor = 0.6;
            }
            else if (age > 40)
            {
                premiumFactor = 0.7;
            }
            else if (age > 30)
            {
                premiumFactor = 0.8;
            }
            else if (age > 25)
            {
                premiumFactor = 0.9;
            }
            else if (age > 20)
            {
                premiumFactor = 1;
            }
            else
            {
                return premiumFactor;
            }
            return premiumFactor;
        }

        public int TimePeriod(String  timePeriod)
        {
            if (timePeriod.ToLower() == "yearly")
            {
                return 1;
            }
            else if(timePeriod.ToLower() =="half yearly")
            {
                return 2;
            }
            else if(timePeriod.ToLower() == "quartly")
            {
                return 4;
            }
            else if(timePeriod.ToLower() == "monthly"){ return 12; }
            else
            {
                return 0;
            }
        }
        public int TotalTimePreriod(int age)
        {
            int timePeriod = 0;

            if (age > 60)
            {
                timePeriod = 2;
            }
            else if (age > 50)
            {
                timePeriod = 2;
            }
            else if (age > 40)
            {
                timePeriod = 2;
            }
            else if (age > 30)
            {
                timePeriod = 3;
            }
            else if (age > 25)
            {
                timePeriod = 4;
            }
            else if (age > 20)
            {
                timePeriod = 5;
            }
            else
            {
                return timePeriod;
            }
            return timePeriod;  

        }

        public string ActualFrequency(int frequencyValue)
        {
            string actualFrequency = "";

            if (frequencyValue == 1)
            {
                actualFrequency = "Yearly";
            }
            else if (frequencyValue == 2)
            {
                actualFrequency = "Half Yearly";
            }
            else if (frequencyValue == 4)
            {
                actualFrequency = "Quartly";

            }
            else
            {
                actualFrequency = "Monthly";
            }

            return actualFrequency;
        }


    }
}
