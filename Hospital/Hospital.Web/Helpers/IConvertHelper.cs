using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Helpers
{
    public interface IConvertHelper
    {
        public User ToUser(UserDTO dto);
        public MedicalSpe ToMedicalSpe(MedicalSpeDTO dto);
        public MedicalOrder ToMedicalOrder(MedicalOrderDTO dto);
        public MedicalHistory ToMedicalHistory(MedicalHistoryDTO dto);
        public Appoiment ToAppoiment(AppoimentDTO dto);
        public Status ToStatus(StatusDTO dto);
        public RolesPermission ToRP(RolPermissionsDTO dto);
   }

    public class ConvertHelper : IConvertHelper
    {
        public User ToUser(UserDTO dto)
        {
            return new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Birth = dto.Birth,
                UserName = dto.UserName,
                Password = dto.Password,
                RolId = dto.RolId,
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
                UserDoctorId= dto.UserDoctorId,
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

        public RolesPermission ToRP(RolPermissionsDTO dto)
        {
            return new RolesPermission
            {
                PermisosId = dto.PermisosId,
                rolId = dto.rolId
            };
        }
    }
}