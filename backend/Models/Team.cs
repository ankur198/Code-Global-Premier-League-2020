using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public class Team
    {
        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        // public string Id { get; set; }

        [BsonElement("team_name")]
        [BsonId]
        public string TeamName { get; set; }

        [BsonElement("wins")]
        public int Wins { get; set; }

        [BsonElement("losses")]
        public int Losses { get; set; }

        [BsonElement("ties")]
        public int Ties { get; set; }

        [BsonElement("score")]
        public int Score { get; set; }

        internal void UpdateScore()
        {
            Score = (Wins * 3) + (Ties * 1);
        }
    }
}