using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Persistence.Contexts;

public class ApplicationDbContextSeeder
{
    private readonly ILogger<ApplicationDbContextSeeder> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextSeeder(
        ILogger<ApplicationDbContextSeeder> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task MigrateAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedDataAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!await _context.AppointmentTypes.AnyAsync())
        {
            await _context.AppointmentTypes.AddRangeAsync(GetAppointmentTypes());
            await _context.SaveChangesAsync();
        }

        if (!await _context.Clinics.AnyAsync())
        {
            await _context.Clinics.AddRangeAsync(GetClinics());
            await _context.SaveChangesAsync();
        }

        if (!await _context.Doctors.AnyAsync())
        {
            await _context.Doctors.AddRangeAsync(GetDoctors());
            await _context.SaveChangesAsync();
        }

        if (!await _context.Rooms.AnyAsync())
        {
            await _context.Rooms.AddRangeAsync(GetRooms());
            await _context.SaveChangesAsync();
        }

        if (!await _context.Clients.AnyAsync())
        {
            await _context.Clients.AddRangeAsync(GetClients());
            await _context.SaveChangesAsync();
        }

        if (!await _context.Patients.AnyAsync())
        {
            await _context.Patients.AddRangeAsync(GetPatients());
            await _context.SaveChangesAsync();
        }
    }

    private List<AppointmentType> GetAppointmentTypes()
    {
        var appointmentTypes = new List<AppointmentType>
        {
            new AppointmentType("Wellness Exam", "WE", 60),
            new AppointmentType("Diagnostic Exam", "DE", 60),
            new AppointmentType("Nail Trim", "NT", 30),
            new AppointmentType("Regular Shower", "RS", 30),
            new AppointmentType("Premium Shower", "PS", 60),
            new AppointmentType("Hair Cut", "HC", 30),
            new AppointmentType("Hair Treatment", "HT", 60),
            new AppointmentType("Phsicology Treatment", "PT", 60),
            new AppointmentType("Surgery A", "SA", 60),
            new AppointmentType("Surgery B", "SB", 120),
            new AppointmentType("Surgery C", "SC", 180),
            new AppointmentType("Surgery D", "SD", 240)
        };

        appointmentTypes.ForEach(x => x.SetCreatedBy("9f79b45e-1ebe-4bb2-9d6f-e00da51b0848"));

        return appointmentTypes;
    }

    private List<Clinic> GetClinics()
    {
        var clinics = new List<Clinic>
        {
            new Clinic("Flores Magon #8665 Tijuana", "floresmagon@vc.com", "Clinic Flores Magon"),
            new Clinic("Lagos #311 Tijuana", "lagos@vc.com", "Clinic Lagos"),
            new Clinic("San Rafael #8080 Tijuana", "sanrafael@vc.com", "Clinic San Rafael"),
            new Clinic("Valle ST #456 Pomona", "valle@vc.com", "Clinic Valle"),
            new Clinic("Revolucion #87 Tepic", "sandocan@vc.com", "Clinic San Docan"),
            new Clinic("Mission ST #7895 Pomona", "mission@vc.com", "Clinic Mission"),
            new Clinic("Thomas Guardado #789 Guadalajara", "thomas@vc.com", "Clinic Thomas"),
            new Clinic("Insurgentes #155 Tepic", "partida@vc.com", "Clinic Partida"),
            new Clinic("Niños Heroes #21 Tepic", "rincon@vc.com", "Clinic Rincon"),
            new Clinic("Ing. Aguayo #1415 Guadalajara", "aguayo@vc.com", "Clinic Aguayo"),
            new Clinic("Ing. Plutarco Elias Calles #99", "happypet@vc.com", "Clinic Happy Pet"),
        };

        clinics.ForEach(x => x.SetCreatedBy("9f79b45e-1ebe-4bb2-9d6f-e00da51b0848"));

        return clinics;
    }

    private List<Doctor> GetDoctors()
    {
        var doctors = new List<Doctor>
        {
            new Doctor("Francisco Nuñez"),
            new Doctor("Sherpard D. Monkey"),
            new Doctor("Sarah Cortez"),
            new Doctor("Jayden James"),
            new Doctor("Nina Hartley"),
            new Doctor("Britney Evans"),
            new Doctor("Juan Wick"),
            new Doctor("Peter Lopez"),
            new Doctor("Miguel Cruz"),
            new Doctor("Esperanza Partida"),
            new Doctor("Elizabeth Gonzalez"),
            new Doctor("Muhammed Baha"),
        };

        doctors.ForEach(x => x.SetCreatedBy("9f79b45e-1ebe-4bb2-9d6f-e00da51b0848"));

        return doctors;
    }

    private List<Room> GetRooms()
    {
        var rooms = new List<Room>
        {
            new Room("A-1"),
            new Room("B-1"),
            new Room("C-1"),
            new Room("D-1"),
            new Room("E-1"),
            new Room("F-1"),
            new Room("G-1"),
            new Room("H-1"),
            new Room("I-1"),
            new Room("J-1"),
            new Room("K-1"),
            new Room("L-1"),
            new Room("M-1"),
            new Room("N-1")
        };

        rooms.ForEach(x => x.SetCreatedBy("9f79b45e-1ebe-4bb2-9d6f-e00da51b0848"));

        return rooms;
    }

    private List<Client> GetClients()
    {
        var clients = new List<Client>
        {
            new Client(
                "Christian Nuñez",
                "Chris",
                "Mister",
                "christian.demo@hotmail.com",
                null
            )
        };

        clients.ForEach(x => x.SetCreatedBy("9f79b45e-1ebe-4bb2-9d6f-e00da51b0848"));

        return clients;
    }

    private List<Patient> GetPatients()
    {
        var patients = new List<Patient>
        {
            new Patient(
                1,
                "Booster",
                AnimalSex.Male,
                new AnimalType("Dobermann", "Dog"),
                new Photo("booster.png", "F8F90EB9-E0B4-44C6-9E33-18606796E537.png"),
                null
            ),
            new Patient(
                1,
                "Vina",
                AnimalSex.Female,
                new AnimalType("Albino", "Ferret"),
                new Photo("vina.jpg", "34D6B187-A8A3-4704-9B7E-945CBC553591.jpg"),
                null
            ),
            new Patient(
                1,
                "Mata",
                AnimalSex.Male,
                new AnimalType("Domestic Shorthair", "Cat"),
                new Photo("mata.jpg", "C1AC842C-3DBE-4C9E-96EA-426D21F29689.jpg"),
                null
            )
        };

        patients.ForEach(x => x.SetCreatedBy("9f79b45e-1ebe-4bb2-9d6f-e00da51b0848"));

        return patients;
    }
}