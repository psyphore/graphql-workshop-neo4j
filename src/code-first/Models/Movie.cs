using HotChocolate.Data.Neo4J;

namespace MoviesAPI.Models;

public class Movie
{
    public string? Title { get; set; }

    [Neo4JRelationship("ACTED_IN", RelationshipDirection.Incoming)]
    public List<Person> Actors { get; set; } = new();
}
