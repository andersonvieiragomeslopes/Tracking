using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class RouteResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }

        [JsonProperty("waypoints")]
        public List<Waypoint> Waypoints { get; set; }
    }
    public class Route
    {
        [JsonProperty("geometry")]
        public string Geometry { get; set; }

        [JsonProperty("legs")]
        public List<Leg> Legs { get; set; }

        [JsonProperty("weight_name")]
        public string WeightName { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }
    }
    public class Intersection
    {
        [JsonProperty("out")]
        public int Out { get; set; }

        [JsonProperty("entry")]
        public List<bool> Entry { get; set; }

        [JsonProperty("bearings")]
        public List<int> Bearings { get; set; }

        [JsonProperty("location")]
        public List<double> Location { get; set; }

        [JsonProperty("in")]
        public int? In { get; set; }
    }

    public class Leg
    {
        [JsonProperty("steps")]
        public List<Step> Steps { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }
    }
    public class Step
    {
        [JsonProperty("geometry")]
        public string Geometry { get; set; }

        [JsonProperty("maneuver")]
        public Maneuver Maneuver { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("driving_side")]
        public string DrivingSide { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("intersections")]
        public List<Intersection> Intersections { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("destinations")]
        public string Destinations { get; set; }
    }
    public class Maneuver
    {
        [JsonProperty("bearing_after")]
        public int BearingAfter { get; set; }

        [JsonProperty("bearing_before")]
        public int BearingBefore { get; set; }

        [JsonProperty("location")]
        public List<double> Location { get; set; }

        [JsonProperty("modifier")]
        public string Modifier { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("exit")]
        public int? Exit { get; set; }
    }
    public class Waypoint
    {
        [JsonProperty("hint")]
        public string Hint { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public List<double> Location { get; set; }
    }
}
