using System;
using System.Collections.Generic;

namespace ConsoleApp.Model
{
    public class SortByDatum : IComparer<WeatherModel>
    {
        public int Compare(WeatherModel x, WeatherModel y)
        {
            if (x.Datum == y.Datum)
                if (x.Plats == "Inne")
                    return 1;
                else
                    return -1;
            else
                return x.Datum.CompareTo(y.Datum);
        }
    }
    public class SortByMogel : IComparer<WeatherModel>
    {
        public int Compare(WeatherModel x, WeatherModel y)
        {
            if (x.Temperatur == y.Temperatur)
            {
                if (x.Datum == y.Datum)
                    if (x.Plats == "Inne")
                        return 1;
                    else
                        return -1;
                else
                    return x.Datum.CompareTo(y.Datum);
            }
            else
            {
                float mogelRiskx = (float)(((decimal)5 / (decimal)104400) * (x.Temperatur * x.Temperatur * x.Temperatur * x.Temperatur) - (decimal)0.0045 * (x.Temperatur * x.Temperatur * x.Temperatur) + (decimal)0.1652 * (x.Temperatur * x.Temperatur) - (decimal)2.9381 * x.Temperatur + 97);
                if (x.Luftfuktighet < 75 && x.Plats == "Inne")
                    mogelRiskx = 0;
                else if (x.Luftfuktighet < 85)
                    mogelRiskx = 0;
                float mogelRisky = (float)(((decimal)5 / (decimal)104400) * (y.Temperatur * y.Temperatur * y.Temperatur * y.Temperatur) - (decimal)0.0045 * (y.Temperatur * y.Temperatur * y.Temperatur) + (decimal)0.1652 * (y.Temperatur * y.Temperatur) - (decimal)2.9381 * y.Temperatur + 97);
                if (y.Luftfuktighet < 75 && y.Plats == "Inne")
                    mogelRisky = 0;
                else if (y.Luftfuktighet < 85)
                    mogelRisky = 0;
                if (mogelRiskx == mogelRisky)
                    return 0;
                return mogelRiskx.CompareTo(mogelRisky);
            }
        }
    }
    public class SortByTemperatur : IComparer<WeatherModel>
    {
        public int Compare(WeatherModel x, WeatherModel y)
        {
            if (x.Temperatur == y.Temperatur)
            {
                if (x.Datum == y.Datum)
                    if (x.Plats == "Inne")
                        return 1;
                    else
                        return -1;
                else
                    return x.Datum.CompareTo(y.Datum);
            }
            else
                return x.Temperatur.CompareTo(y.Temperatur);
        }
    }
    public class SortByLuftfuktighet : IComparer<WeatherModel>
    {
        public int Compare(WeatherModel x, WeatherModel y)
        {
            if (x.Luftfuktighet == y.Luftfuktighet)
            {
                if (x.Datum == y.Datum)
                    if (x.Plats == "Inne")
                        return 1;
                    else
                        return -1;
                else
                    return x.Datum.CompareTo(y.Datum);
            }
            else
                return x.Luftfuktighet.CompareTo(y.Luftfuktighet);
        }
    }
    public partial class WeatherModel
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string Plats { get; set; }
        public decimal Temperatur { get; set; }
        public int Luftfuktighet { get; set; }
    }
}
