/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Model;
using PaymentsIdentifier.ViewModel;
using Prism.Events;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using System.Windows.Input;
using System.Linq;

namespace PaymentsIdentifier.Commands
{
    [Export]
    internal class SaveCustomerDatabaseCommand : ICommand
    {
        private IEventAggregator myEventAggregator;
        private IdentifierFacade myFacade;
        private IFilePathProvider myFilePathProvider;

        [ImportingConstructor]
        public SaveCustomerDatabaseCommand(IEventAggregator eventAggregator, IdentifierFacade facade, IFilePathProvider filePathProvider)
        {
            myEventAggregator = eventAggregator;
            myFacade = facade;
            myFilePathProvider = filePathProvider;
        }

        public bool CanExecute(object parameter)
        {
            var p = parameter as object[];
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            var parameters = (object[])parameter;

            string filePath = myFilePathProvider.GetSaveFilePath();
            if (!String.IsNullOrWhiteSpace(filePath))
            {
                using (FileStream fileStream = File.Open(filePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream, Encoding.Unicode))
                    {
                        foreach(string countryName in myFacade.CustomerDatabase.Select(_ => _.Country.Name).Distinct())
                        {
                            writer.WriteLine("[{0}]", countryName);
                            foreach (Customer customer in myFacade.CustomerDatabase.Where(_ => _.Country.Name == countryName))
                            {
                                writer.WriteLine("Name={0}\nId={1}", customer.Name, string.Join(",", customer.Identifiers));
                            }
                            writer.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
