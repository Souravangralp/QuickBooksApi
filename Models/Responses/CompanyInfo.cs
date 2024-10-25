namespace QuickBookApi.Models.Responses;

public class CompanyInfo
{
    public string CompanyName { get; set; }
    public string LegalName { get; set; }
    public CompanyAddr CompanyAddr { get; set; }
    public CustomerCommunicationAddr CustomerCommunicationAddr { get; set; }
    public LegalAddr LegalAddr { get; set; }
    public PrimaryPhone PrimaryPhone { get; set; }
    public string CompanyStartDate { get; set; }
    public string FiscalYearStartMonth { get; set; }
    public string Country { get; set; }
    public Email Email { get; set; }
    public WebAddr WebAddr { get; set; }
    public string SupportedLanguages { get; set; }
    public IList<NameValue> NameValue { get; set; }
    public string domain { get; set; }
    public bool sparse { get; set; }
    public string Id { get; set; }
    public string SyncToken { get; set; }
    public MetaData MetaData { get; set; }

}
public class CompanyAddr
{
    public string Id { get; set; }
    public string Line1 { get; set; }
    public string City { get; set; }
    public string CountrySubDivisionCode { get; set; }
    public string PostalCode { get; set; }
    public string Lat { get; set; }
    public string Long { get; set; }

}
public class CustomerCommunicationAddr
{
    public string Id { get; set; }
    public string Line1 { get; set; }
    public string City { get; set; }
    public string CountrySubDivisionCode { get; set; }
    public string PostalCode { get; set; }
    public string Lat { get; set; }
    public string Long { get; set; }

}
public class LegalAddr
{
    public string Id { get; set; }
    public string Line1 { get; set; }
    public string City { get; set; }
    public string CountrySubDivisionCode { get; set; }
    public string PostalCode { get; set; }
    public string Lat { get; set; }
    public string Long { get; set; }

}
public class PrimaryPhone
{

}
public class Email
{
    public string Address { get; set; }

}
public class WebAddr
{

}
public class NameValue
{
    public string Name { get; set; }
    public string Value { get; set; }

}
public class MetaData
{
    public string CreateTime { get; set; }
    public DateTime LastUpdatedTime { get; set; }

}
public class Application
{
    public CompanyInfo CompanyInfo { get; set; }
    public string time { get; set; }

}
