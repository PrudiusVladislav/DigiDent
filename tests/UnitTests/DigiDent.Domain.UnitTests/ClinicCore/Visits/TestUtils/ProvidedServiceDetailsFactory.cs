namespace DigiDent.Domain.UnitTests.ClinicCore.Visits.TestUtils;

public class ProvidedServiceDetailsFactory
{
    public static (string, string) ValidProvidedServiceDetails =>
        ("Dentistry", "Dental care services");
    
    public static HashSet<(string, string)> InvalidProvidedServiceDetails =>
    [
        ("", "Description"),
        ("Service", ""),
        ("", ""),
        ("A", "Description"),
        ("".PadLeft(101, 'A'), "Description"),
        ("Service", "".PadLeft(501, 'A'))
    ];
}