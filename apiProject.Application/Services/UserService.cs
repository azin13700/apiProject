using apiProject.Application.Dtos;
using apiProject.Application.Dtos.Responses;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserUnitRepository _userUnitRepository;
        public UserService(IUserRepository repository, IUserRoleRepository userRoleRepository, IUserUnitRepository userUnitRepository, IUnitRepository unitRepository)
        {
            _repository = repository;
            _userRoleRepository = userRoleRepository;
            _userUnitRepository = userUnitRepository;
            _unitRepository = unitRepository;
        }

        public async Task<bool?> ChangeStatus(ChangeStatusUserDto dto)
        {
            var employee = await _repository.GetUserByIdAsync(dto.UserId);
            if (employee == null)
            {
                return null;
            }
            employee.IsActive = !employee.IsActive;
            await _repository.UpdateUserAsync(employee);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<int> CreateUserAsync(CreateUserDto dto)
        {
            var existingUser = await _repository.GetByUserNameOrEmailAsync(dto.UserName, dto.Email);
            if (existingUser == true)
            {
                throw new Exception("نام کاربری یا ایمیل قبلاً ثبت شده است");
            }
            var user = new User
            {
                Name = dto.Name,
                Family = dto.Family,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
                IsActive = true,
                NationalNo = dto.NationalNo,
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                UserName = dto.UserName,
            };
            await _repository.AddUserAsync(user);

            await _repository.SaveChangesAsync();


            if (dto.Photo != null)
            {
                using var ms = new MemoryStream();

                await dto.Photo.CopyToAsync(ms);

                var photo = new UserPhoto
                {
                    UserId = user.Id,
                    ImageData = ms.ToArray()
                };

                await _repository.AddPhotoAsync(photo);

                await _repository.SaveChangesAsync();
            }

            if (dto.RoleId.Length >= 0)
            {

                foreach (var roleId in dto.RoleId)
                {
                    var userRole = new UserRole
                    {
                        RoleId = roleId,
                        UserId = user.Id,
                        Status = dto.Status,
                    };

                    await _userRoleRepository.AddAsync(userRole);
                    await _userRoleRepository.SaveChangesAsync();
                }


            }

            if (dto.UnitId.Length >= 0)
            {
                foreach (var unitId in dto.UnitId)
                {
                    var userRole = new UserUnit
                    {
                        UnitId = unitId,
                        UserId = user.Id
                    };

                    await _userUnitRepository.AddAsync(userRole);
                    await _userUnitRepository.SaveChangesAsync();
                }
            }

            return user.Id;

        }

        public async Task<List<UserResponseDto>> GetAllUserAsync()
        {
            var users = await _repository.GetAllAsync();

            return users.Select(user => new UserResponseDto
            {
                FullName = user.Name + " " + user.Family,
                createdAt = user.CreatedAt,
                Email = user.Email,
                UserId = user.Id,
                UserName = user.UserName,
                IsActive = user.IsActive,
                Unit = user.UserUnits
                     .Where(x => x.Unit != null)
                     .Select(x => x.Unit.Name)
                    .ToList() ?? new List<string>(),
                Photo = user.UserPhoto == null
                 ? null
                 : user.UserPhoto.ImageData,

                Role = user.UserRoles?
                 .Where(x => x.Role != null)
                 .Select(x => x.Role.Name)
                 .ToList() ?? new List<string>(),

                Status = user.UserRoles?
                 .Select(x => x.Status)
                 .FirstOrDefault()
            }).ToList();
        }

        public async Task<GetUserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);

            if (user == null)
                return null;
            return new GetUserResponseDto
            {
                Name = user.Name,
                Family = user.Family,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                NationalNo = user.NationalNo.ToString(),
                Password = user.Password,
                PhoneNumber = user.PhoneNumber.ToString(),
                Photo = user.UserPhoto?.ImageData,
                IsActive = user.IsActive,
                UserId = user.Id,
                UserName = user.UserName,
                UserPhotoId = user.UserPhoto.UserId,
                Status = user.UserRoles.Select(x => x.Status).FirstOrDefault(),
                RoleId = user.UserRoles.Select(x => x.RoleId).ToArray(),
                Unitid = user.UserUnits.Select(x => x.UnitId).ToArray()

            };

        }

        //public async Task<List<UserResponseDto>> SearchAsync(UserResponseDto dto)
        //{
        //    var employees = await _repository.GetAllAsync();
        //    var query = employees.AsQueryable();


        //    if (!string.IsNullOrWhiteSpace(dto.FullName))
        //    {
        //        var term = dto.FullName.Trim().ToLower();
        //        query = query.Where(e => e.Name.ToLower().Contains(term) || e.Family.ToLower().Contains(term));
        //    }

        //    if (!string.IsNullOrWhiteSpace(dto.UserName))
        //    {
        //        var term = dto.UserName.Trim().ToLower();
        //        query = query.Where(e => e.UserName.ToLower().Contains(term));
        //    }

        //    if (dto.Role.Any())
        //    {
        //      //  query = query.Where(e => e.UserRoles.Any(x=>x.RoleId == dto.Role.));
        //    }
        //    if (!string.IsNullOrWhiteSpace(dto.Email))
        //    {
        //        query = query.Where(e => e.Email == dto.Email);
        //    }


        //    if (!string.IsNullOrWhiteSpace(dto.Status))
        //    {
        //        query = query.Where(e => e.UserRoles.Any(x=>x.Status == dto.Status));
        //    }

        //    //if (!string.IsNullOrWhiteSpace(dto.CompanyName))
        //    //{
        //    //    query = query.Where(x => x.WorkExperiences
        //    //              .Any(x => x.CompanyName == dto.CompanyName));
        //    //}
        //    //if (!string.IsNullOrWhiteSpace(dto.FromYear))
        //    //{
        //    //    query = query.Where(x => x.WorkExperiences
        //    //            .Any(x => x.FromYear == dto.FromYear));
        //    //}

        //    //if (!string.IsNullOrWhiteSpace(dto.ToYear))
        //    //{
        //    //    query = query.Where(x => x.WorkExperiences
        //    //    .Any(x => x.ToYear == dto.ToYear));

        //    //}

        //    var result = query
        //        .OrderByDescending(e => e.Id)
        //        .ToList();

        //    Console.WriteLine($"جستجو انجام شد - تعداد نتیجه: {result.Count}");

        //    return result.Select(user => new UserResponseDto
        //    {
        //        FullName = user.Name + " " + user.Family,
        //        createdAt = user.CreatedAt,
        //        Email = user.Email,
        //        UserId = user.Id,
        //        UserName = user.UserName,

        //        Photo = user.UserPhoto == null
        //              ? null
        //              : user.UserPhoto.ImageData,

        //        Role = user.UserRoles?
        //              .Where(x => x.Role != null)
        //              .Select(x => x.Role.Name)
        //              .ToList() ?? new List<string>(),

        //        Status = user.UserRoles?
        //              .Select(x => x.Status)
        //              .FirstOrDefault()
        //    }).ToList();
        //}


        public async Task<List<UserResponseDto>> SearchAsync(SearchUserDto dto)
        {
            var users = await _repository.GetAllAsync();
            var query = users.AsQueryable();

            // ✅ جستجو
            if (!string.IsNullOrWhiteSpace(dto.FullName) || !string.IsNullOrWhiteSpace(dto.UserName) || !string.IsNullOrWhiteSpace(dto.Email))
            {
                var term = dto.FullName.Trim().ToLower();
                var username = dto.UserName.Trim().ToLower();
                var email = dto.Email.Trim().ToLower();
                query = query.Where(e =>
                    (e.Name != null && e.Name.ToLower().Contains(term)) ||
                    (e.Family != null && e.Family.ToLower().Contains(term)) ||
                    (e.Email != null && e.Email.ToLower().Contains(email)) ||
                    ((e.Name ?? "") + " " + (e.Family ?? "")).ToLower().Contains(term)||
                        (e.UserName != null && e.UserName.ToLower().Contains(username))
                );
            }

            //if (!string.IsNullOrWhiteSpace(dto.UserName))
            //{
            //    var term = dto.UserName.Trim().ToLower();
            //    query = query.Where(e => e.UserName != null && e.UserName.ToLower().Contains(term));
            //}

       

            if (dto.IsActive.HasValue)
            {
                query = query.Where(e => e.IsActive == dto.IsActive.Value);
            }

            if (dto.Role != null && dto.Role.Any())
            {
                query = query.Where(e => e.UserRoles != null &&
                    e.UserRoles.Any(ur => ur.Role != null && dto.Role.Contains(ur.Role.Name)));
            }

            if (dto.Unit != null && dto.Unit.Any())
            {
                query = query.Where(e => e.UserUnits != null &&
                    e.UserUnits.Any(uu => dto.Unit.Contains(uu.UnitId)));
            }

            var result = query
                .OrderByDescending(e => e.Id)
                .ToList();

            // ✅ لاگ برای دیباگ
            Console.WriteLine($"🔍 تعداد نتایج قبل از Select: {result.Count}");

            var response = result.Select(user => new UserResponseDto
            {
                FullName = user.Name + " " + user.Family,
                createdAt = user.CreatedAt,
                Email = user.Email,
                UserId = user.Id,
                UserName = user.UserName,
                IsActive = user.IsActive,
                Unit = user.UserUnits
                     .Where(x => x.Unit != null)
                     .Select(x => x.Unit.Name)
                    .ToList() ?? new List<string>(),
                Photo = user.UserPhoto == null
                 ? null
                 : user.UserPhoto.ImageData,

                Role = user.UserRoles?
                 .Where(x => x.Role != null)
                 .Select(x => x.Role.Name)
                 .ToList() ?? new List<string>(),

                Status = user.UserRoles?
                 .Select(x => x.Status)
                 .FirstOrDefault()
            }).ToList();

            Console.WriteLine($"🔍 تعداد نتایج نهایی: {response.Count}");

            return response;
        }
        public async   Task UpdateAsync(UpdateUserDto dto)
        {
            var user = await _repository.GetUserByIdAsync(dto.UserId);
            if (user == null)
            {
                throw new Exception("کاربر یافت نشد");
            }
            //var existingUser = await _repository.GetByUserNameOrEmailAsync(dto.UserName, dto.Email);
            //if (existingUser == true)
            //{
            //    throw new Exception("نام کاربری یا ایمیل قبلاً ثبت شده است");
            //}
      

            user.Name = dto.Name;
            user.Family = dto.Family;
            user.DateOfBirth = dto.DateOfBirth;
            user.NationalNo = dto.NationalNo;
            user.Password = dto.Password;
            user.PhoneNumber = dto.PhoneNumber;
            user.UserName = user.UserName;
            user.Email = dto.Email;
            await _repository.UpdateUserAsync(user);

            await _repository.SaveChangesAsync();

            if (dto.Photo != null)
            {
                // var user = await _repository.GetUserByIdAsync(dto.UserId);
                await _repository.DeleteUserPhotoAsync(user.Id);
                using var ms = new MemoryStream();

                await dto.Photo.CopyToAsync(ms);

                var photo = new UserPhoto
                {
                    UserId = user.Id,
                    ImageData = ms.ToArray()
                };

                await _repository.AddPhotoAsync(photo);

                await _repository.SaveChangesAsync();
            }

            if (dto.RoleId != null && dto.RoleId.Any())
            {
                await _userRoleRepository.DeleteByUserIdAsync(user.Id);

                foreach (var roleId in dto.RoleId)
                {
                    var userRole = new UserRole
                    {
                        RoleId = roleId,
                        UserId = user.Id,
                        Status = dto.Status ?? "Active"
                    };

                    await _userRoleRepository.AddAsync(userRole);
                }
                await _userRoleRepository.SaveChangesAsync();
            }


            if (dto.UnitId != null && dto.UnitId.Any())
            {
                await _unitRepository.DeleteByUserIdAsync(user.Id);

                foreach (var unitId in dto.UnitId)
                {
                    var userRole = new UserUnit
                    {
                        UserId = user.Id,
                       UnitId = unitId
                    };

                    await _unitRepository.AddUserUnitAsync(userRole);
                }
                await _unitRepository.SaveChangesAsync();
            }


        }



    }
}
