/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

namespace PaymentsIdentifier.ViewModel
{
    internal interface IFilePathProvider
    {
        string GetOpenFilePath();
        string GetSaveFilePath();
    }
}
