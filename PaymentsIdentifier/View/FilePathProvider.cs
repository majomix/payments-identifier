/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using Microsoft.Win32;
using PaymentsIdentifier.ViewModel;
using System.ComponentModel.Composition;

namespace PaymentsIdentifier.View
{
    [Export(typeof(IFilePathProvider))]
    internal class FilePathProvider : IFilePathProvider
    {
        public string GetOpenFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            string filePath = null;
            bool? dialogResult = openFileDialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                filePath = openFileDialog.FileName;
            }

            return filePath;
        }

        public string GetSaveFilePath()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            string filePath = null;
            bool? dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                filePath = saveFileDialog.FileName;
            }

            return filePath;
        }
    }
}
