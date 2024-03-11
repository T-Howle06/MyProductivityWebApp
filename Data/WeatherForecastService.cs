using MyProductivityWebApp.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProductivityWebApp.Data
{
    public class WeatherForecastService
    {
        private readonly MyProductivityWebAppContext _myProductivityWebAppContext;
        
        public  WeatherForecastService(MyProductivityWebAppContext productivityWebAppContext)
        {
            _myProductivityWebAppContext = productivityWebAppContext;
        }

        public async Task<List<WeatherForecast>> GetForecastsAsync(string strCurrenUser)
        {
            // Get Weather Forecasts
            return await _myProductivityWebAppContext.WeatherForecasts
                // Only get entries for the current logged in user
                .Where(u => u.UserName == strCurrenUser)
                // Use AsNoTracking to disable EF change tracking
                // Use ToListAsync to avoid blocking thread
                .AsNoTracking().ToListAsync();
        }

        public Task<WeatherForecast> CreateForecastAsync(WeatherForecast objWeatherForecast)
        {
            _myProductivityWebAppContext.WeatherForecasts.Add(objWeatherForecast);
            _myProductivityWebAppContext.SaveChanges();
            return Task.FromResult(objWeatherForecast);
        }

        public Task<bool> UpdateForecastAsync(WeatherForecast objWeatherForecast)
        {
            var ExistingWeatherForecast = _myProductivityWebAppContext.WeatherForecasts
                .Where(x => x.Id == objWeatherForecast.Id)
                .FirstOrDefault();
            if (ExistingWeatherForecast != null)
            {
                ExistingWeatherForecast.Date = objWeatherForecast.Date;
                ExistingWeatherForecast.Summary = objWeatherForecast.Summary;
                ExistingWeatherForecast.TemperatureC = objWeatherForecast.TemperatureC;
                ExistingWeatherForecast.TemperatureF = objWeatherForecast.TemperatureF;
                _myProductivityWebAppContext.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteForecastAsync(WeatherForecast objWeatherForecast)
        {
            var ExistingWeatherForecast = _myProductivityWebAppContext.WeatherForecasts
                .Where(x => x.Id == objWeatherForecast.Id)
                .FirstOrDefault();
            if (ExistingWeatherForecast != null)
            {
                _myProductivityWebAppContext.WeatherForecasts.Remove(ExistingWeatherForecast);
                _myProductivityWebAppContext.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
