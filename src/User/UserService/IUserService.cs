using System.Collections.Generic;

namespace Project
{
    public interface IUserService
    {
        void Add(UserModel model);

        void Delete(int id);

        UserModel Get(int id);

        IEnumerable<UserModel> List();

        void Update(UserModel model);

        void UpdateEmail(UserModel model);
    }
}
