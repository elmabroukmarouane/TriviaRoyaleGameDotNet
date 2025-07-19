using Bogus;
using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.FakeData
{
    public static class MemberFakeDataSeed
    {
        public static IList<Member> FakeDataMemberSeed()
        {
            return [
                new () {
                    Id = 1,
                    FirstName = "Adrien",
                    LastName = "RODRIGUES",
                    CreateDate = new DateTime(2025,06,25,14,08,11),
                    CreatedBy = "Administrator",
                    UpdateDate = new DateTime(2025,06,25,14,08,11),
                    UpdatedBy = "Administrat"
                },

                new () {
                    Id = 2,
                    FirstName = "Ahmed",
                    LastName = "NACIRI",
                    CreateDate = new DateTime(2025,06,25,14,08,11),
                    CreatedBy = "Administrator",
                    UpdateDate = new DateTime(2025,06,25,14,08,11),
                    UpdatedBy = "Administrator"
                },

                new () {
                    Id = 3,
                    FirstName = "Ilias",
                    LastName = "KADI",
                    CreateDate = new DateTime(2025,06,25,14,08,11),
                    CreatedBy = "Administrator",
                    UpdateDate = new DateTime(2025,06,25,14,08,11),
                    UpdatedBy = "Administrator"
                },

                new () {
                    Id = 4,
                    FirstName = "Marouane",
                    LastName = "EL MABROUK",
                    CreateDate = new DateTime(2025,06,25,14,08,11),
                    CreatedBy = "Administrator",
                    UpdateDate = new DateTime(2025,06,25,14,08,11),
                    UpdatedBy = "Administrator"
                }
            ];
        }
    }
}
