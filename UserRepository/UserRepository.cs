
public class UserRepository(UserDbContext userDbContext) : IUserRepository
{
    public bool CreateUser(UserCreateDTO userCreateDTO)
    {
        try
        {
            bool isExisted = userDbContext.Users.Any(x => x.Name.ToLower() == userCreateDTO.Name.ToLower() && x.IsDeleted == false);
            if (isExisted) return false;

            int maxId = userDbContext.Users.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
            maxId++;
            userDbContext.Users.Add(new()
            {
                Id = maxId,
                Name = userCreateDTO.Name,
                Email = userCreateDTO.Email
            });

            userDbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeleteUser(int id)
    {
        try
        {
            User? existingUser = userDbContext.Users.FirstOrDefault(x => x.Id == id);
            if (existingUser == null) return false;
            existingUser.IsDeleted = true;
            existingUser.DeletedAt = DateTime.UtcNow;
            userDbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public PaginationResponse<IEnumerable<UserReadDTO>> GetAllUsers(UserFilter userFilter)
    {
        int totalRecords =  userDbContext.Users.Count();
        try
        {
            IQueryable<User> users = userDbContext.Users;
            if(userFilter.Name!=null)
            users = users.Where(x=>x.Name.ToLower().Contains(userFilter.Name!.ToLower()));

            IQueryable<UserReadDTO> userReadDTOs = users.Skip((userFilter.PageNumber-1)*userFilter.PageSize).Take(userFilter.PageSize).Select(x=>
            new UserReadDTO
            {
                Id=x.Id,
                Name=x.Name,
                Email=x.Email
            }       
            );
            return PaginationResponse<IEnumerable<UserReadDTO>>.Create(userFilter.PageNumber, userFilter.PageSize, totalRecords, userReadDTOs);
                 
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw new Exception();
        }
    }

    public UserReadDTO? GetUserById(int id)
    {
        try
        {
            User? user = userDbContext.Users.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            if (user == null) return new UserReadDTO();
            return new UserReadDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return new UserReadDTO();
        }
    }

    public bool UpdateUser(UserUpdateDTO userUpdateDTO)
    {
        try
        {
            User? user = userDbContext.Users.FirstOrDefault(x => x.Id == userUpdateDTO.Id && x.IsDeleted==false);
            if(user==null) return false;
            user.Email=userUpdateDTO.Email;
            user.Name=userUpdateDTO.Name;
            user.UpdatedAt=DateTime.UtcNow;
            user.Version+=1;
            userDbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }
}