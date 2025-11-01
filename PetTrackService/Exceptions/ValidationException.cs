namespace PetTrackService.Exceptions;

/// <summary>
/// Ýþ kuralý veya doðrulama hatasý oluþtuðunda fýrlatýlýr.
/// HTTP 400 - Bad Request ile eþleþir.
/// </summary>
public class ValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationException(string message) : base(message)
 {
        Errors = new Dictionary<string, string[]>();
    }

  public ValidationException(Dictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }
}
