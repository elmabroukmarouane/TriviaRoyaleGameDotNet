using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.FakeData
{
    public static class QuestionFakeDataSeed
    {
        public static IList<Question> FakeDataQuestionSeed()
        {
            return [
                new() {
                    Id = 1,
                    CategoryId = 1,
                    Text = "Capital of France ?",
                    Choices = [ "London", "Paris", "Berlin", "Rome" ],
                    CorrectChoiceIndex = 1
                },
                new() {
                    Id = 2,
                    CategoryId = 3,
                    Text = "2 + 2 = ?",
                    Choices = [ "3", "4", "5", "6" ],
                    CorrectChoiceIndex = 1
                },
                new() {
                    Id = 3,
                    CategoryId = 4,
                    Text = "Blazor is a ___ framework ?",
                    Choices = [ "Python", "C#", "Java", "PHP" ],
                    CorrectChoiceIndex = 1
                },
                new() {
                    Id = 4,
                    CategoryId = 2,
                    Text = "What is the gravity G of earth ?",
                    Choices = [ "5.12", "9.8", "2", "10" ],
                    CorrectChoiceIndex = 1
                }
            ];
        }
    }
}