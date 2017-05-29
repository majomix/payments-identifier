/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using PaymentsIdentifier.Commands;
using PaymentsIdentifier.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace PaymentsIdentifier.ViewModel
{
    [Export]
    internal class MainWindowViewModel : BindableBase
    {
        private string myDailyReportFilePath;
        private string myUnallocatedCashReportFilePath;
        private string myOutputFilePath;
        private double myTolerance;
        private int myAging;
        private int myCurrentProgress;
        private string myCurrentStatus;
        private Dictionary<string, object> myAvailableRegions;
        private Dictionary<string, object> mySelectedRegions;
        private bool myCloseCurrentWindow;

        public string DailyReportFilePath
        {
            get { return myDailyReportFilePath; }
            set
            {
                if (myDailyReportFilePath != value)
                {
                    myDailyReportFilePath = value;
                    OnPropertyChanged("DailyReportFilePath");
                }
            }
        }
        public string UnallocatedCashReportFilePath
        {
            get { return myUnallocatedCashReportFilePath; }
            set
            {
                if (myUnallocatedCashReportFilePath != value)
                {
                    myUnallocatedCashReportFilePath = value;
                    OnPropertyChanged("UnallocatedCashReportFilePath");
                }
            }
        }
        public string OutputFilePath
        {
            get { return myOutputFilePath; }
            set
            {
                if (myOutputFilePath != value)
                {
                    myOutputFilePath = value;
                    OnPropertyChanged("OutputFilePath");
                }
            }
        }
        public double Tolerance
        {
            get { return myTolerance; }
            set
            {
                if (myTolerance != value)
                {
                    myTolerance = value;
                    OnPropertyChanged("Tolerance");
                }
            }
        }
        public int Aging
        {
            get { return myAging; }
            set
            {
                if (myAging != value)
                {
                    myAging = value;
                    OnPropertyChanged("Aging");
                }
            }
        }
        public string CurrentStatus
        {
            get { return myCurrentStatus; }
            private set
            {
                if (myCurrentStatus != value)
                {
                    myCurrentStatus = value;
                    OnPropertyChanged("CurrentStatus");
                }
            }
        }
        public int CurrentProgress
        {
            get { return myCurrentProgress; }
            protected set
            {
                if (myCurrentProgress != value)
                {
                    myCurrentProgress = value;
                    OnPropertyChanged("CurrentProgress");
                }
            }
        }
        public Dictionary<string, object> AvailableRegions
        {
            get
            {
                return myAvailableRegions;
            }
            set
            {
                myAvailableRegions = value;
                OnPropertyChanged("AvailableRegions");
            }
        }
        public Dictionary<string, object> SelectedRegions
        {
            get
            {
                return mySelectedRegions;
            }
            set
            {
                mySelectedRegions = value;
                OnPropertyChanged("SelectedRegions");
            }
        }
        public IProgress<int> Progress { get; private set; }
        public bool CloseCurrentWindow
        {
            get { return myCloseCurrentWindow; }
            protected set
            {
                if (myCloseCurrentWindow != value)
                {
                    myCloseCurrentWindow = value;
                    OnPropertyChanged("CloseCurrentWindow");
                }
            }
        }

        [Import(typeof(LoadFileNameCommand))]
        public ICommand LoadFileNameCommand { get; private set; }

        [Import(typeof(MatchInvoicesCommand))]
        public IAsynchronousCommand MatchInvoicesCommand { get; private set; }

        [Import(typeof(LoadExcelFilesCommand))]
        public IAsynchronousCommand LoadExcelFilesCommand { get; private set; }

        [ImportingConstructor]
        public MainWindowViewModel(IFilePathProvider filePathProvider)
        {
            CurrentStatus = "Ready...";
            Progress = new Progress<int>(UpdateProgressBar);
            AvailableRegions = new Dictionary<string, object>();
            SelectedRegions = new Dictionary<string, object>();

            foreach (Country country in ReportMappings.SupportedCountries())
            {
                AvailableRegions.Add(country.SheetName, country.SheetName);
            }
        }

        private void UpdateProgressBar(int progress)
        {
            if (CurrentProgress != 100)
            {
                if (progress != 0) CurrentProgress = progress;
                else CurrentProgress++;
            }
        }
    }
}
