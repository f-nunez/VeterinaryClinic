using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientEdit;

public class GetPatientEditResponse : BaseResponse
{
    public PatientEditDto? Patient { get; set; }
    public List<PreferredDoctorFilterValueDto> PreferredDoctorFilterValues { get; set; } = new();

    public GetPatientEditResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}