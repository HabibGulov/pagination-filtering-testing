public interface IUserRepository
{
    PaginationResponse<IEnumerable<UserReadDTO>> GetAllUsers(UserFilter userFilter);
    UserReadDTO? GetUserById(int id);
    bool CreateUser(UserCreateDTO userCreateDTO);
    bool DeleteUser(int id);
    bool UpdateUser(UserUpdateDTO userUpdateDTO);
}