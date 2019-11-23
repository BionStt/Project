namespace Project
{
    public sealed class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(Context context) : base(context) { }
    }
}
