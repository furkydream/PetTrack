namespace PetTrackService.Exceptions;

/// <summary>
/// Veritabanýnda aranýlan kayýt bulunamadýðýnda fýrlatýlan hata.
/// HTTP 404 - Not Found ile eþleþir.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
 }

    public NotFoundException(string entityName, object key)
        : base($"{entityName} with key '{key}' was not found.")
    {
    }
}
