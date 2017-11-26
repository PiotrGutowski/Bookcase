using System;
using System.Threading.Tasks;
using Bookcase.Core.Domain;
using Bookcase.Core.Repositories;
using Bookcase.Infrastructure.DTO;
using Bookcase.Infrastructure.Extensions;
using System.Collections.Generic;
using AutoMapper;


namespace Bookcase.Infrastructure.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
    }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var user = await _userRepository.GetAllUserAsync();
            return _mapper.Map<IEnumerable<UserDto>>(user);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);

            return _mapper.Map<UserDto>(user);

        }
       

        public async Task<IEnumerable<UserDto>> GetUserByNameAsync(string name = null)
        {
            var user = await _userRepository.GetByNameAsync(name);
            return _mapper.Map<IEnumerable<UserDto>>(user);

        }

        public async Task AddUserAsync(Guid userId, string email, string name)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
            {
                throw new Exception( "Email already exists");
            }
            user = new User(userId, email, name);
            await _userRepository.AddAsync(user);
        }
        public async Task EditUserAsync(Guid userId, string email, string name)
        {

            var user = await _userRepository.GetOrFailAsync(userId);

            user.SetName(name);
            user.SetEmail(email);
            await _userRepository.UpdateAsync(user);
        }
        public async Task DeleteUserAsync(Guid userId)
        {
            var user= await _userRepository.GetOrFailAsync(userId);
            await _userRepository.DeleteAsync(user);

        }
    }
}
