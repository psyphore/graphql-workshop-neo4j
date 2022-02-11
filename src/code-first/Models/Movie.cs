using HotChocolate.Data.Neo4J;

using System.Collections.Generic;

namespace MoviesAPI.Models
{
    public class Movie
    {
        public string Title { get; set; }

        [Neo4JRelationship("ACTED_IN", RelationshipDirection.Incoming)]
        public List<Actor> Actors { get; set; }
    }
}