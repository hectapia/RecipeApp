using RecipeApp.Models;

namespace RecipeApp.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext db)
        {
            if (!db.Users.Any())
            {
                var demoUser = new User
                {
                    Email = "demo@example.com",
                    DisplayName = "Demo",
                    PasswordHash = "placeholder"
                };
                db.Users.Add(demoUser);
                db.SaveChanges();

                db.Recipes.Add(new Recipe
                {
                    Title = "Simple Guacamole",
                    Instructions = "Mash avocados, add lime, salt, onion, cilantro, tomato. Serve.",
                    OwnerUserId = demoUser.Id
                });
                db.SaveChanges();
            }

        }
    }
}
