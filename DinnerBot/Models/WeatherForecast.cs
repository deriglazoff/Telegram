namespace DinnerBot.Models
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public static class Menu
    {
        public static Dictionary<string, string> Soup = new()
        {
            {"Окрошка","Soup.1"},
            { "Борщ", "Soup.2" }
        };
    }
}