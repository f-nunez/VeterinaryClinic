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
        if (!_context.AppointmentTypes.Any())
        {
            _context.AppointmentTypes.AddRange(GetAppointmentTypes());
            await _context.SaveChangesAsync();
        }

        if (!_context.Clinics.Any())
        {
            _context.Clinics.AddRange(GetClinics());
            await _context.SaveChangesAsync();
        }

        if (!_context.Doctors.Any())
        {
            _context.Doctors.AddRange(GetDoctors());
            await _context.SaveChangesAsync();
        }

        if (!_context.Rooms.Any())
        {
            _context.Rooms.AddRange(GetRooms());
            await _context.SaveChangesAsync();
        }

        if (!_context.Clients.Any())
        {
            _context.Clients.AddRange(GetClients());
            await _context.SaveChangesAsync();
        }

        if (!_context.Patients.Any())
        {
            _context.Patients.AddRange(GetPatients());
            await _context.SaveChangesAsync();
        }
    }

    private List<AppointmentType> GetAppointmentTypes()
    {
        return new List<AppointmentType>
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
    }

    private List<Clinic> GetClinics()
    {
        return new List<Clinic>
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
    }

    private List<Doctor> GetDoctors()
    {
        return new List<Doctor>
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
    }

    private List<Room> GetRooms()
    {
        return new List<Room>
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
    }

    private List<Client> GetClients()
    {
        var client1 = new Client(
            "Christian Nuñez",
            "Chris",
            "Mister",
            "christian.demo@hotmail.com",
            null
        );

        return new List<Client>
        {
            client1
        };
    }

    private List<Patient> GetPatients()
    {
        var pet1 = new Patient(
            1,
            "Booster",
            AnimalSex.Male,
            new AnimalType("Dobermann", "Dog"),
            null
        );

        var pet2 = new Patient(
            1,
            "Vina",
            AnimalSex.Female,
            new AnimalType("Albino", "Ferret"),
            null
        );

        var pet3 = new Patient(
            1,
            "Mata",
            AnimalSex.Male,
            new AnimalType("Domestic Shorthair", "Cat"),
            null
        );

        return new List<Patient>
        {
            pet1,
            pet2,
            pet3
        };
    }
}