using BookMachine.Core.Interfaces.Persistence.Mappings;
using BookMachine.Core.Models;
using BookMachine.Persistence.Entities;

namespace BookMachine.Persistence.Mappings
{
    public class UserMapping : IUserMapping
    {
        public static User FromUserEntityToUser(UserEntity userEntity)
        {
            User user = User.Create(userEntity.UserId, userEntity.UserName, userEntity.Email, userEntity.PasswordHash).User;

            return user;
        }

        public static UserEntity FromUserToUserEntity(User user)
        {
            UserEntity userEntity = new()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
            };

            return userEntity;
        }
    }
}
