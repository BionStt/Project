using System.Collections.Generic;
using System.Linq;

namespace Project
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(UserModel model)
        {
            var entity = UserFactory.Create(model);

            _userRepository.Add(entity);

            model.Id = entity.Id;
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public UserModel Get(int id)
        {
            var entity = _userRepository.Get(id);

            return UserFactory.Create(entity);
        }

        public IEnumerable<UserModel> List()
        {
            return _userRepository.List().Select(UserFactory.Create);
        }

        public void Update(UserModel model)
        {
            var entity = UserFactory.Create(model);

            _userRepository.Update(entity);
        }

        public void UpdateEmail(UserModel model)
        {
            _userRepository.Update(model.Id, new { model.Email });
        }
    }
}
