using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using TouristAgency.Model;

namespace TouristAgency.RequestModel
{
    public class UpdateTouristPackageStatusRequest
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TouristPackageStatus Status { get; set; }
    }
}
