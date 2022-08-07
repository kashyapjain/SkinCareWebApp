using SkinCareWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace SkinCareWebApp.Services
{
    public class TrendsService
    {
        private SkinCareDBEntities Entities = new SkinCareDBEntities();
        private double Lat { get; set; }
        private double Lon { get; set; }
        private string CityName { get; set; }

        private int DataGap = 7;
    
        public TrendsService(double Lat,double Lon, string CityName)
        {
            this.Lat = Lat;
            this.Lon = Lon;
            this.CityName = CityName;
        }


        public List<TrendsArchiveData> dataForTrends()
        {
            int DayDiffInUnix = 86400;
            DateTime CurrentDate = DateTime.UtcNow.Date;
            long CurrentDateEpoch = GetUnixTime(CurrentDate);
            long EndDateEpoch = CurrentDateEpoch;               //GetUnixTime(CurrentDate.AddDays(-1).Date);
            long StartDateEpoch = GetUnixTime(CurrentDate.AddDays(-DataGap));

            var LastRecord = Entities.TrendsMapDatas.ToList();
            if (LastRecord == null || LastRecord.Count==0)
            {
                try
                {
                    var ResponseString = getArchiveDataResponseStringFromApi(StartDateEpoch, EndDateEpoch);
                    CustomWeatherModel ResultObject = JsonConvert.DeserializeObject<CustomWeatherModel>(ResponseString);

                    if (ResultObject == null)
                    {
                        return null;
                    }
                    List<TrendsArchiveData> RealData = new List<TrendsArchiveData>();
                    foreach (CustomWeatherData item in ResultObject.days)
                    {
                        RealData.Add(new TrendsArchiveData()
                        {
                            EpochDate = item.datetimeEpoch,
                            Temp = item.temp,
                            Humidity = item.humidity,
                            SolarRadiation = item.solarradiation,
                            SolarEnergy = item.solarenergy,
                            UvIndex = item.uvindex
                        });
                    }
                    SaveTrendsDataToDatabase(RealData);

                    SaveTrendsMapToDatabase(new TrendsMapData()
                    {
                        EntryDate = DateTime.UtcNow,
                        TrendStartDate = StartDateEpoch,
                        TrendEndDate = EndDateEpoch,
                        Lat = this.Lat,
                        Long = this.Lon,
                        LocationCityName = this.CityName
                    });

                    return RealData;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                var lastRecord = LastRecord.Last();
                long? tempdiff = EndDateEpoch - lastRecord.TrendEndDate;
                if (tempdiff == 0)
                {
                    var data = GetTrendsDataToDatabase(StartDateEpoch, EndDateEpoch);
                    return data.ToList();
                }
                else
                {
                    int NoOfReq = (int)(tempdiff/DayDiffInUnix);
                    long Temp_EndDateEpoch = EndDateEpoch;
                    long Temp_StartDateEpoch = EndDateEpoch - NoOfReq*DayDiffInUnix;
                    try
                    {
                        var ResponseString = getArchiveDataResponseStringFromApi(Temp_StartDateEpoch, Temp_EndDateEpoch);
                        CustomWeatherModel ResultObject = JsonConvert.DeserializeObject<CustomWeatherModel>(ResponseString);

                        if (ResultObject == null)
                        {
                            return null;
                        }
                        List<TrendsArchiveData> RealData = new List<TrendsArchiveData>();
                        foreach (CustomWeatherData item in ResultObject.days)
                        {
                            RealData.Add(new TrendsArchiveData()
                            {
                                EpochDate = item.datetimeEpoch,
                                Temp = item.temp,
                                Humidity = item.humidity,
                                SolarRadiation = item.solarradiation,
                                SolarEnergy = item.solarenergy,
                                UvIndex = item.uvindex
                            });
                        }
                        SaveTrendsDataToDatabase(RealData);

                        SaveTrendsMapToDatabase(new TrendsMapData()
                        {
                            EntryDate = DateTime.UtcNow,
                            TrendStartDate = Temp_StartDateEpoch,
                            TrendEndDate = Temp_EndDateEpoch,
                            Lat = this.Lat,
                            Long = this.Lon,
                            LocationCityName = this.CityName
                        });

                        return GetTrendsDataToDatabase(StartDateEpoch,EndDateEpoch).ToList();

                    }
                    catch (Exception ex)
                    {
                        return null;
                    }

                }
            }
            return null;

        }
   
        private string getArchiveDataResponseStringFromApi(long StartDateEpoch,long EndDateEpoch)
        {
            string apiKey = "EBY8VHU22SUAUN9GCRBUU42EF";
            string url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{this.Lat},{this.Lon}/{StartDateEpoch}/{EndDateEpoch}?unitGroup=metric&elements=datetime%2CdatetimeEpoch%2Cname%2Caddress%2Ctemp%2Cfeelslike%2Chumidity%2Cprecip%2Cwindspeed%2Cpressure%2Csolarradiation%2Csolarenergy%2Cuvindex&include=days%2Ccurrent%2Cobs&key={apiKey}&contentType=json";
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

        private bool SaveTrendsDataToDatabase(List<TrendsArchiveData>data)
        {
            try
            {
                var result = Entities.TrendsArchiveDatas.AddRange(data);
                var t = Entities.SaveChanges();

                int c = 9;
            }
            catch(Exception ex)
            {
                return false;
            }

            return false;
        }

        private bool SaveTrendsMapToDatabase(TrendsMapData data)
        {
            try
            {
                Entities.TrendsMapDatas.Add(data);
                Entities.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        private IEnumerable<TrendsArchiveData> GetTrendsDataToDatabase(long startDate, long endDate)
        {
            try
            {
                var data = Entities.TrendsArchiveDatas.Where(i => (i.EpochDate >= startDate && i.EpochDate <= endDate));

                return data.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
      
        private long GetUnixTime(DateTime date)
        {
            var offset_date = new DateTimeOffset(date);
            var epoch = offset_date.ToUnixTimeSeconds();

            return epoch;
        }
   

    }
}