using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public class Team
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("team_name")]
        [BsonRequired]
        [JsonPropertyName("team_name")]
        public string TeamName { get; set; }

        [BsonElement("wins")]
        [JsonPropertyName("wins")]
        public int Wins { get; set; }

        [BsonElement("losses")]
        [JsonPropertyName("losses")]
        public int Losses { get; set; }

        [BsonElement("ties")]
        [JsonPropertyName("ties")]
        public int Ties { get; set; }

        [BsonElement("score")]
        [JsonPropertyName("score")]
        public int Score { get; set; }

        internal void UpdateScore()
        {
            Score = (Wins * 3) + (Ties * 1);
        }
    }
}