using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Helpers
{
    public interface IConverterHelper
    {
        public User ToUser(UserDTO dto);
        public MedicalSpe ToMedicalSpe(MedicalSpeDTO dto);
        public MedicalOrder ToMedicalOrder(MedicalOrderDTO dto);
        public MedicalHistory ToMedicalHistory(MedicalHistoryDTO dto);
        public Appoiment ToAppoiment(AppoimentDTO dto);
        public Status ToStatus(StatusDTO dto);

        public Task<UserDTO> ToUserDTOAsync(User user, bool isNew = true);
    }

    public class ConverterHelper : IConverterHelper
    {
        private readonly ICombosHelpers _combosHelper;

        public ConverterHelper(ICombosHelpers combosHelper)
        {
            _combosHelper = combosHelper;
        }
        public User ToUser(UserDTO dto)
        {
            return new User
            {
                Id = dto.Id.ToString(),
                Document = dto.Document,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email,
                HospitalRoleId = dto.HospitalRoleId,
                PhoneNumber = dto.PhoneNumber,
            };
        }

        public async Task<UserDTO> ToUserDTOAsync(User user, bool isNew = true)
        {
            return new UserDTO
            {
                Id = isNew ? Guid.NewGuid() : Guid.Parse(user.Id),
                Document = user.Document,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                HospitalRoles = await _combosHelper.GetComboHospitalRolesAsync(),
                HospitalRoleId = user.HospitalRoleId,
                PhoneNumber = user.PhoneNumber
            };
        }

        public MedicalSpe ToMedicalSpe(MedicalSpeDTO dto)
        {
            return new MedicalSpe
            {
                Name = dto.Name,
                UserDoctorId = dto.UserDoctorId,
            };
        }
        public MedicalOrder ToMedicalOrder(MedicalOrderDTO medicalOrderdto)
        {
            return new MedicalOrder
            {
                Diagnosis = medicalOrderdto.Diagnosis,
                Description = medicalOrderdto.Description,
                AppoimentId = medicalOrderdto.AppoimentId,
                MedicationId = medicalOrderdto.MedicationId
            };
        }

        public MedicalHistory ToMedicalHistory(MedicalHistoryDTO dto)
        
        {
            return new MedicalHistory
            {

                NamePatient = dto.NamePatient,
                Description = dto.Description,
                AppoimentId = dto.AppoimentId
            };
        
        }

        public Appoiment ToAppoiment(AppoimentDTO dto)
        {
            return new Appoiment
            {
                Date = dto.Date,
                Time = dto.Time,
                UserDoctorId= (Guid)dto.UserDoctorId,
                UserPatientId= dto.UserPatientId
            };
        }

        public Status ToStatus(StatusDTO dto)
        {
            return new Status
            {
                AppoimentId = dto.AppoimentId,
                StatusAppoiment = dto.StatusAppoiment
            };
        }
       
    }
}