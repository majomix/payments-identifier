/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

namespace PaymentsIdentifier.Model
{
    internal enum IdentifyStatus
    {
        Unidentified,
        IdentifiedByExtraction,
        IdentifiedByMatching,
        IdentifiedByMatchingWithMultipleSameValues,
        IdentifiedByInput,
        PartiallyIdentifiedByExtraction,
        IdentifiedByExtractionWithUnknownCustomer
        
    }
}
