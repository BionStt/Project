namespace Project
{
    public sealed class UserModel
    {
        public UserModel() { }

        public UserModel
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

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
    }
}
