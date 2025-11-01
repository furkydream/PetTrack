namespace PetTrackService.DTOs.Pet;

/// <summary>
/// Pet'in tüm detaylarýný (iliþkiler dahil) dönerken kullanýlýr.
/// </summary>
public record PetDetailDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Species { get; init; } = string.Empty;
    public string Breed { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public int Age { get; init; }
    public string OwnerName { get; init; } = string.Empty;
    public DateTime CreatedDate { get; init; }
    
    // Ýliþkili veriler
    public TrackerDeviceDto? TrackerDevice { get; init; }
    public List<HealthRecordDto> HealthRecords { get; init; } = new();
    public List<VetAppointmentDto> UpcomingAppointments { get; init; } = new();
    public List<ActivityLogDto> RecentActivities { get; init; } = new();
    public List<AlertDto> ActiveAlerts { get; init; } = new();
}

public record TrackerDeviceDto
{
    public int Id { get; init; }
    public string SerialNumber { get; init; } = string.Empty;
    public double BatteryLevel { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}

public record HealthRecordDto
{
    public int Id { get; init; }
    public DateTime RecordDate { get; init; }
    public string Description { get; init; } = string.Empty;
    public string RecordType { get; init; } = string.Empty;
    public string? VaccineName { get; init; }
}

public record VetAppointmentDto
{
    public int Id { get; init; }
    public DateTime AppointmentDate { get; init; }
    public string Description { get; init; } = string.Empty;
}

public record ActivityLogDto
{
    public int Id { get; init; }
    public DateTime ActivityDate { get; init; }
    public string ActivityType { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public double Distance { get; init; }
    public int Duration { get; init; }
}

public record AlertDto
{
    public int Id { get; init; }
    public DateTime AlertDate { get; init; }
    public string AlertType { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}
