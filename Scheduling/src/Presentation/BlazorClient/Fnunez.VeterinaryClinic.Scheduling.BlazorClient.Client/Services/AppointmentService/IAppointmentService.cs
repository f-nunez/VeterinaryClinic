using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterPatient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface IAppointmentService
{
    public Task<CreateAppointmentResponse> CreateAppointmentAsync(CreateAppointmentRequest request);
    public Task<DataGridResponse<AppointmentDto>> DataGridAsync(GetAppointmentsRequest request);
    public Task<DataGridResponse<AppointmentTypeFilterValueDto>> DataGridFilterAppointmentTypeAsync(GetAppointmentsFilterAppointmentTypeRequest request);
    public Task<DataGridResponse<ClientFilterValueDto>> DataGridFilterClientAsync(GetAppointmentsFilterClientRequest request);
    public Task<DataGridResponse<ClinicFilterValueDto>> DataGridFilterClinicAsync(GetAppointmentsFilterClinicRequest request);
    public Task<DataGridResponse<DoctorFilterValueDto>> DataGridFilterDoctorAsync(GetAppointmentsFilterDoctorRequest request);
    public Task<List<PatientFilterValueDto>> DataGridFilterPatientAsync(GetAppointmentsFilterPatientRequest request);
    public Task<DataGridResponse<RoomFilterValueDto>> DataGridFilterRoomAsync(GetAppointmentsFilterRoomRequest request);
    public Task<DeleteAppointmentResponse> DeleteAppointmentAsync(DeleteAppointmentRequest request);
    public Task<GetAppointmentDetailResponse> GetAppointmentDetailAsync(GetAppointmentDetailRequest request);
    public Task<GetAppointmentEditResponse> GetAppointmentEditAsync(GetAppointmentEditRequest request);
    public Task<UpdateAppointmentResponse> UpdateAppointmentAsync(UpdateAppointmentRequest request);
}