using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using StreamFlix.Models;

namespace StreamFlix.Services
{
    public class DynamoDBService
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private const string TableName = "Movies";

        public DynamoDBService(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task SaveMovieAsync(Movie movie)
        {
            var request = new PutItemRequest
            {
                TableName = TableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { "MovieId", new AttributeValue { S = movie.MovieId } },
                    { "Title", new AttributeValue { S = movie.Title } },
                    { "Genre", new AttributeValue { S = movie.Genre } },
                    { "Director", new AttributeValue { S = movie.Director } },
                    { "ReleaseDate", new AttributeValue { S = movie.ReleaseDate.ToString() } },
                    { "Rating", new AttributeValue { N = movie.Rating.ToString() } }
                }
            };
            await _dynamoDbClient.PutItemAsync(request);
        }

        public async Task DeleteMovieAsync(string movieId)
        {
            var request = new DeleteItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue> { { "MovieId", new AttributeValue { S = movieId } } }
            };
            await _dynamoDbClient.DeleteItemAsync(request);
        }
    }
}
