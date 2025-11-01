namespace PetTrackService.Exceptions;

/// <summary>
/// Ýþ mantýðý hatasý (örn: Pet zaten bir cihaza sahip)
/// HTTP 400 - Bad Request ile eþleþir.
/// </summary>
public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
    }
}
