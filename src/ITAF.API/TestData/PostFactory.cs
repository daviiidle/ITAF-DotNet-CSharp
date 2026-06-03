using Bogus;
using ITAF.API.Models;

namespace ITAF.API.TestData;

public static class PostFactory
{
    private static readonly Faker Faker = new();

    public static Post Create()
    {
        return new Post(
            UserId: Faker.Random.Int(1, 20),
            Id: 0,
            Title: Faker.Lorem.Sentence(4),
            Body: Faker.Lorem.Paragraph());
    }
}

