using TriviaRoyaleGame.Infrastructure.Models.Classes;
using System.Security.Cryptography;
using System.Text;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.FakeData
{
    public static class UserFakeDataSeed
    {
        public static IList<User> FakeDataUserSeed()
        {
            return [
                new () {
                     Id = 1,
                     MemberId = 1,
                     Email = "adrien@mail.com",
                     Password = CreateHashPassword("123456"),
                     Role = Role.User,
                     CreateDate = new DateTime(2025,06,25,14,08,11),
                     CreatedBy = "Administrator",
                     UpdateDate = new DateTime(2025,06,25,14,08,11),
                     UpdatedBy = "Administrator"
                },

                new () {
                     Id = 2,
                     MemberId = 2,
                     Email = "ahmed@mail.com",
                     Password = CreateHashPassword("123456"),
                     Role = Role.User,
                     CreateDate = new DateTime(2025,06,25,14,08,11),
                     CreatedBy = "Administrator",
                     UpdateDate = new DateTime(2025,06,25,14,08,11),
                     UpdatedBy = "Administrator"
                },

                new () {
                     Id = 3,
                     MemberId = 3,
                     Email = "iliad@mail.com",
                     Password = CreateHashPassword("123456"),
                     Role = Role.User,
                     CreateDate = new DateTime(2025,06,25,14,08,11),
                     CreatedBy = "Administrator",
                     UpdateDate = new DateTime(2025,06,25,14,08,11),
                     UpdatedBy = "Administrator"
                },

                new () {
                     Id = 4,
                     MemberId = 4,
                     Email = "marouane@mail.com",
                     Password = CreateHashPassword("123456"),
                     Role = Role.SuperAdmin,
                     CreateDate = new DateTime(2025,06,25,14,08,11),
                     CreatedBy = "Administrator",
                     UpdateDate = new DateTime(2025,06,25,14,08,11),
                     UpdatedBy = "Administrator"
                }
            ];
        }

        public static string? CreateHashPassword(string? password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            using (var sha512Hash = SHA512.Create())
            {
                var bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(password.Trim()));
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}