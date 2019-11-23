namespace Project
{
    public static class UserFactory
    {
        public static UserEntity Create(UserModel model)
        {
            if (model == default) return default;

            return new UserEntity(model.Id, model.Name, model.Surname, model.Email);
        }

        public static UserModel Create(UserEntity entity)
        {
            if (entity == default) return default;

            return new UserModel(entity.Id, entity.Name, entity.Surname, entity.Email);
        }
    }
}
