namespace Project
{
    public sealed class UserEntity
    {
        public UserEntity
        (
            int id,
            string name,
            string surname,
            string email
        )
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string Email { get; private set; }

        public void UpdateName(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }
    }
}
