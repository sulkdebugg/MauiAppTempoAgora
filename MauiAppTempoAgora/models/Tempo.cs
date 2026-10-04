namespace MauiAppTempoAgora.models
{
    public class Tempo
    {
        public double? lon { get; set; }
        public double? lat { get; set; }
        public int? humidity { get; set; }
        public double? temp_min { get; set; }
        public double? temp_max { get; set; }
        public double? temp { get; set; }
        public double? feels_like { get; set; }
        public int? sea_level { get; set; }
        public string? sunrise { get; set; }
        public string? sunset { get; set; }
        public string description { get; set; }
        public double? speed { get; set; }
        public int? timezone { get; set; }
        public string? main { get; set; }
    }
}
