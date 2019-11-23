namespace Project
{
    public static class ContextSeed
    {
        public static void Seed(this Context context)
        {
            context.Add(new UserEntity(default, "Ada", "Lovelace", "ada.lovelace@email.com"));
            context.Add(new UserEntity(default, "Albert", "Einstein", "albert.einstein@email.com"));
            context.Add(new UserEntity(default, "Galileu", "Galilei", "galileu.galilei@email.com"));
            context.Add(new UserEntity(default, "Isaac", "Newton", "isaac.newton@email.com"));
            context.Add(new UserEntity(default, "Marie", "Curie", "marie.curie@email.com"));
            context.Add(new UserEntity(default, "Nikola", "Tesla", "nikola.tesla@email.com"));
            context.Add(new UserEntity(default, "Stephen", "Hawking", "stephen.hawking@email.com"));

            context.SaveChanges();
        }
    }
}
