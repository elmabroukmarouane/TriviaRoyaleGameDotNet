using Bogus;
using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.FakeData
{
    public static class CategoryFakeDataSeed
    {
        public static IList<Category> FakeDataCategorySeed()
        {
            return [
                new () {
                    Id = 1,
                    Name = "Geography",
                    CreateDate = new DateTime(2025,06,25,14,08,11),
                    CreatedBy = "Administrator",
                    UpdateDate = new DateTime(2025,06,25,14,08,11),
                    UpdatedBy = "Administrat"
                },

                new () {
                    Id = 2,
                    Name = "Science",
                    CreateDate = new DateTime(2025,06,25,14,08,11),
                    CreatedBy = "Administrator",
                    UpdateDate = new DateTime(2025,06,25,14,08,11),
                    UpdatedBy = "Administrator"
                },

                new () {
                    Id = 3,
                    Name = "Math",
                    CreateDate = new DateTime(2025,06,25,14,08,11),
                    CreatedBy = "Administrator",
                    UpdateDate = new DateTime(2025,06,25,14,08,11),
                    UpdatedBy = "Administrator"
                },

                new () {
                    Id = 4,
                    Name = "Computer Science",
                    CreateDate = new DateTime(2025,06,25,14,08,11),
                    CreatedBy = "Administrator",
                    UpdateDate = new DateTime(2025,06,25,14,08,11),
                    UpdatedBy = "Administrator"
                }
            ];
        }
    }
}
