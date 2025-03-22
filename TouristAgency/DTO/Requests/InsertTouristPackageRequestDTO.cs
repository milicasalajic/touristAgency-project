﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using TouristAgency.Model;

namespace TouristAgency.DTO.Requests
{
    public class InsertTouristPackageRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Capacity { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public string Schedule { get; set; }
        public string PriceIncludes { get; set; }
        public string PriceDoesNotIncludes { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<Tourist> Tourists { get; set; }
        public Destination Destination { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Transportation Transportation { get; set; }
        public double BasePrice { get; set; } 
        // Cena po vrsti sobe (index: 0 - dvokrevetna, 1 - trokrevetna, 2 - četvorokrevetna)
        public List<double> RoomPrices { get; set; } = new List<double>(); 
        public TouristPackageStatus Status { get; set; }
        public double GetRoomPrice(int bedCount)
        {
            if (RoomPrices.Count > 0 && bedCount == 2)
                return RoomPrices[0]; // Dvokrevetna

            if (RoomPrices.Count > 1 && bedCount == 3)
                return RoomPrices[1]; // Trokrevetna
            
            if (RoomPrices.Count > 2 && bedCount == 4)
                return RoomPrices[2]; // Četvorokrevetna

            return BasePrice;
        }
    }
}
