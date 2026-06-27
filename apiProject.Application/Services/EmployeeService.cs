using apiProject.Application.Dtos.Dependancy;
using apiProject.Application.Dtos.Employee;
using apiProject.Application.Dtos.Responses;
using apiProject.Application.Dtos.User;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace apiProject.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IWorkExperienceRepository _workExperienceRepository;
        private readonly IDependantRepository _dependantRepository;

        public EmployeeService(IEmployeeRepository repository, IWorkExperienceRepository workExperienceRepository, IDependantRepository dependantRepository)
        {
            _repository = repository;
            _workExperienceRepository = workExperienceRepository;
            _dependantRepository = dependantRepository;
        }



        public async Task<EmployeeResponseDto?> GetByIdAsync(int id)
        {
            var employee = await _repository.GetWithDetailsAsync(id);

            if (employee == null)
                return null;

            return new EmployeeResponseDto
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Family = employee.Family,
                Gender = employee.Gender,
                DateOfEmployment = employee.DateOfEmployment,
                TypeOfEmployment = employee.TypeOfEmployment,
                NationalCode = employee.NationalCode,
                IsDelete = employee.IsDelete,

                Dependants = employee.Dependants.Select(d =>
                    new DependantResponseDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Family = d.Family,
                        DateOfBirth = d.DateOfBirth,
                        RelationType = d.relationType
                    }).ToList(),

                WorkExperiences = employee.WorkExperiences.Select(w =>
                    new WorkExperienceResponseDto
                    {
                        WorkExperienceId = w.Id,
                        CompanyName = w.CompanyName,
                        FromYear = w.FromYear,
                        ToYear = w.ToYear,
                        RelationType = w.relationType
                    }).ToList(),

            };
        }

        public async Task<int> CreateAsync(CreateEmployeeDto dto)
        {

            var employee = new Employee
            {
                Name = dto.Name,
                Family = dto.Family,
                NationalCode = dto.NationalCode,
                Gender = dto.Gender,
                DateOfEmployment = dto.DateOfEmployment,
                TypeOfEmployment = dto.TypeOfEmployment
            };

            await _repository.AddAsync(employee);

            await _repository.SaveChangesAsync();

            if (dto.Photo != null)
            {
                using var ms = new MemoryStream();

                await dto.Photo.CopyToAsync(ms);

                var photo = new Photo
                {
                    EmployeeId = employee.EmployeeId,
                    ImageData = ms.ToArray()
                };

                await _repository.AddPhotoAsync(photo);

                await _repository.SaveChangesAsync();
            }

            return employee.EmployeeId;
        }

        public async Task UpdateAsync(UpdateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(dto.EmployeeId);

            if (employee == null)
                return;

            employee.Name = dto.Name;
            employee.Family = dto.Family;
            employee.Gender = dto.Gender;
            employee.DateOfEmployment = dto.DateOfEmployment;
            employee.TypeOfEmployment = dto.TypeOfEmployment;
            employee.NationalCode = dto.NationalCode;

            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee != null)
            {
                employee.IsDelete = true;
                await _repository.UpdateAsync(employee);
                await _repository.SaveChangesAsync();
              //  await _repository.DeleteAsync(employee);

              //  await _repository.SaveChangesAsync();
            }
        }

        public async Task<List<EmployeeResponseDto>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();
              
          

            return employees.Select(employee => new EmployeeResponseDto
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Family = employee.Family,
                Gender = employee.Gender,
                DateOfEmployment = employee.DateOfEmployment,
                TypeOfEmployment = employee.TypeOfEmployment,
                NationalCode = employee.NationalCode,
                IsDelete = employee.IsDelete,
                Photo = employee.Photo?.ImageData,
                Dependants = employee.Dependants.Select(d =>
                    new DependantResponseDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Family = d.Family,
                        DateOfBirth = d.DateOfBirth,
                        RelationType = d.relationType
                    }).ToList(),

                WorkExperiences = employee.WorkExperiences.Select(w =>
                    new WorkExperienceResponseDto
                    {
                        WorkExperienceId = w.Id,
                        CompanyName = w.CompanyName,
                        FromYear = w.FromYear,
                        ToYear = w.ToYear,
                        RelationType = w.relationType
                    }).ToList()

            }).ToList();
        }

        public async Task<int> CreateDetailsAsync(CreateEmployeeDetailsDto dto)
        {
            var employee = await _repository.GetWithDetailsAsync(dto.EmployeeId);
            if (employee == null)
                throw new Exception("پرسنل یافت نشد");

            if (dto.Dependants != null && dto.Dependants.Any())
            {
                foreach (var dep in dto.Dependants)
                {
                    var dependantEntity = new Dependant
                    {
                        Name = dep.Name,
                        Family = dep.Family,
                        DateOfBirth = dep.DateOfBirth,
                        relationType = dep.RelationType,
                        EmployeeId = dto.EmployeeId
                    };

                    await _dependantRepository.AddAsync(dependantEntity);
                    await _dependantRepository.SaveChangesAsync();
                }
            }
            if (dto.WorkExperiences != null && dto.WorkExperiences.Any())
            {
                foreach (var exp in dto.WorkExperiences)
                {
                    var workExpEntity = new WorkExperience
                    {
                        CompanyName = exp.CompanyName,
                        FromYear = exp.FromYear,
                        ToYear = exp.ToYear,
                        relationType = exp.RelationType,
                        EmployeeId = dto.EmployeeId
                    };

                    await _workExperienceRepository.AddAsync(workExpEntity);
                    await _workExperienceRepository.SaveChangesAsync();
                }
            }

            await _repository.SaveChangesAsync();

            return employee.EmployeeId;
        }

        public async Task<List<EmployeeResponseDto>> SearchAsync(SearchEmployeeDto dto)
        {
            var employees = await _repository.GetAllAsync();
            var query = employees.AsQueryable();


            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                var term = dto.Name.Trim().ToLower();
                query = query.Where(e => e.Name.ToLower().Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(dto.Family))
            {
                var term = dto.Family.Trim().ToLower();
                query = query.Where(e => e.Family.ToLower().Contains(term));
            }

            if (dto.Gender.HasValue)
            {
                query = query.Where(e => e.Gender == dto.Gender.Value.ToString());
            }
            if (!string.IsNullOrWhiteSpace(dto.NationalCode))
            {
                query = query.Where(e => e.NationalCode == dto.NationalCode);
            }


            if (!string.IsNullOrWhiteSpace(dto.TypeOfEmployment))
            {
                query = query.Where(e => e.TypeOfEmployment == dto.TypeOfEmployment);
            }

            if (!string.IsNullOrWhiteSpace(dto.CompanyName))
            {
              query = query.Where(x => x.WorkExperiences
                        .Any(x => x.CompanyName == dto.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(dto.FromYear))
            {
                query = query.Where(x => x.WorkExperiences
                        .Any(x => x.FromYear == dto.FromYear));
            }

            if (!string.IsNullOrWhiteSpace(dto.ToYear))
            {
                query = query.Where(x => x.WorkExperiences
                .Any(x => x.ToYear == dto.ToYear));
                    
            }

            var result = query
                .OrderByDescending(e => e.EmployeeId)
                .ToList();

            Console.WriteLine($"جستجو انجام شد - تعداد نتیجه: {result.Count}");

               return result.Select(employee => new EmployeeResponseDto
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Family = employee.Family,
                Gender = employee.Gender,
                DateOfEmployment = employee.DateOfEmployment,
                TypeOfEmployment = employee.TypeOfEmployment,
                NationalCode = employee.NationalCode,

                Dependants = employee.Dependants.Select(d =>
                    new DependantResponseDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Family = d.Family,
                        DateOfBirth = d.DateOfBirth,
                        RelationType = d.relationType
                    }).ToList(),

                WorkExperiences = employee.WorkExperiences.Select(w =>
                    new WorkExperienceResponseDto
                    {
                        WorkExperienceId = w.Id,
                        CompanyName = w.CompanyName,
                        FromYear = w.FromYear,
                        ToYear = w.ToYear,
                        RelationType = w.relationType
                    }).ToList()

            }).ToList();
        }

        public async Task<int> UpdateDetailsAsync(UpdateEmployeeDetailsDto dto)
        {
            var employee = await _repository.GetWithDetailsAsync(dto.EmployeeId);
            if (employee?.Dependants != null && employee.Dependants.Any())
            {
                var existingDependantIds = dto.Dependants
               .Where(d => d.Id > 0)
               .Select(d => d.Id)
               .ToList();

                var dependantsToDelete = employee.Dependants
                .Where(d => !existingDependantIds.Contains(d.Id))
                .ToList();
                foreach (var dep in dependantsToDelete)
                {
                    employee.Dependants.Remove(dep);
                }


                foreach (var DependantDto in dto.Dependants)
                {
                    if (DependantDto.Id > 0)
                    {
                        var existingDependant = employee.Dependants
                                                .FirstOrDefault(x => x.Id == DependantDto.Id);

                        if (existingDependant != null)
                        {
                            existingDependant.relationType = DependantDto.RelationType;
                            existingDependant.DateOfBirth = DependantDto.DateOfBirth;
                            existingDependant.Name = DependantDto.Name;
                            existingDependant.Family = DependantDto.Family;

                        }
                    }
                    else
                    {
                        employee.Dependants.Add(
                            new Dependant
                            {
                                DateOfBirth = DependantDto.DateOfBirth,
                                Name = DependantDto.Name,
                                Family = DependantDto.Family,
                                relationType = DependantDto.RelationType,
                                IsDelete = false,
                                EmployeeId = dto.EmployeeId

                            });


                    }


                }
            }
            else
            {
                foreach (var dep in dto.Dependants)
                {
                    var dependantEntity = new Dependant
                    {
                        Name = dep.Name,
                        Family = dep.Family,
                        DateOfBirth = dep.DateOfBirth,
                        relationType = dep.RelationType,
                        EmployeeId = dto.EmployeeId
                    };

                    await _dependantRepository.AddAsync(dependantEntity);
                    await _dependantRepository.SaveChangesAsync();
                }
            }

            if (dto.WorkExperiences != null)
            {
                var existingWorkIds = dto.WorkExperiences
                    .Where(w => w.WorkExperienceId > 0)
                    .Select(w => w.WorkExperienceId)
                    .ToList();

                // حذف سوابقی که حذف شده‌اند
                var worksToDelete = employee.WorkExperiences
                    .Where(w => !existingWorkIds.Contains(w.Id))
                    .ToList();

                foreach (var work in worksToDelete)
                {
                    employee.WorkExperiences.Remove(work);
                }

                // به‌روزرسانی یا افزودن سوابق جدید
                foreach (var workDto in dto.WorkExperiences)
                {
                    if (workDto.WorkExperienceId > 0)
                    {
                        var existingWork = employee.WorkExperiences
                            .FirstOrDefault(w => w.Id == workDto.WorkExperienceId);

                        if (existingWork != null)
                        {
                            existingWork.CompanyName = workDto.CompanyName;
                            existingWork.FromYear = workDto.FromYear;
                            existingWork.ToYear = workDto.ToYear;
                            existingWork.relationType = workDto.RelationType;
                        }
                    }
                    else
                    {
                        employee.WorkExperiences.Add(new WorkExperience
                        {
                            CompanyName = workDto.CompanyName,
                            FromYear = workDto.FromYear,
                            ToYear = workDto.ToYear,
                            relationType = workDto.RelationType,
                            EmployeeId = dto.EmployeeId
                        });
                    }
                }
            }

            else
            {
                foreach (var exp in dto.WorkExperiences)
                {
                    var workExpEntity = new WorkExperience
                    {
                        CompanyName = exp.CompanyName,
                        FromYear = exp.FromYear,
                        ToYear = exp.ToYear,
                        relationType = exp.RelationType,
                        EmployeeId = dto.EmployeeId
                    };

                    await _workExperienceRepository.AddAsync(workExpEntity);
                     await _workExperienceRepository.SaveChangesAsync();
                }
            }


                await _dependantRepository.SaveChangesAsync();
            return await _workExperienceRepository.SaveChangesAsync();
        }

        public async Task<bool?> ChangeStatus(ChangeStatusDto dto)
        {
            var employee = await _repository.GetByIdAsync(dto.EmployeeId);
            if (employee == null)
            {
                return null;
            }
            employee.IsDelete = !employee.IsDelete;
            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();
            return true;
          
        }
        public async Task<bool> UploadPhotoAsync(PhotoUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return false;

            using var memoryStream = new MemoryStream();
            await dto.File.CopyToAsync(memoryStream);

            var photo = new Photo
            {
                EmployeeId = dto.EmployeeId,
                ImageData = memoryStream.ToArray(),
              
            };

            await _repository.AddPhotoAsync(photo);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}

