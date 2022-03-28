using System.Text.Json.Serialization;

namespace Asteradar.API.Client;

public class Response
{
    public class Kilometers
    {
        [JsonPropertyName("estimated_diameter_min")]
        public double EstimatedDiameterMin { get; set; }

        [JsonPropertyName("estimated_diameter_max")]
        public double EstimatedDiameterMax { get; set; }
    }

    public class EstimatedDiameter
    {
        [JsonPropertyName("kilometers")]
        public Kilometers Kilometers { get; set; }
    }

    public class RelativeVelocity
    {
        [JsonPropertyName("kilometers_per_hour")]
        public string KilometersPerHour { get; set; }
    }

    public class CloseApproachData
    {
        [JsonPropertyName("close_approach_date")]
        public string CloseApproachDate { get; set; }

        [JsonPropertyName("relative_velocity")]
        public RelativeVelocity RelativeVelocity { get; set; }

        [JsonPropertyName("orbiting_body")]
        public string OrbitingBody { get; set; }
    }
    public class NearEarthObject
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("estimated_diameter")]
        public EstimatedDiameter EstimatedDiameter { get; set; }

        [JsonPropertyName("is_potentially_hazardous_asteroid")]
        public bool IsPotentiallyHazardousAsteroid { get; set; }

        [JsonPropertyName("close_approach_data")]
        public List<CloseApproachData> CloseApproachData { get; set; }
    }

    public class NearAsteroidsResponse
    {
        [JsonPropertyName("near_earth_objects")]
        public Dictionary<string,List<NearEarthObject>> NearEarthObjects { get; set; }
    }
}