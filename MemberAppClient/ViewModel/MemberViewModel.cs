using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MemberAppClient.ViewModel
{
    public class MemberViewModel : ViewModelBase
    {

        ServiceAgent serviceAgent;
        ObservableCollection<Member> _Employee; 

        public MemberViewModel()
        {
            CancelCommand = new AsyncRelayCommand(async ()=> await DoCancelAsync());
            SaveCommand = new AsyncRelayCommand(async () => await DoSave());
            AddUserCommand = new AsyncRelayCommand(async () => await AddUser());
            DeleteUserCommand = new AsyncRelayCommand(async () => await DeleteUser());


           
           // serviceAgent.EmployeeChanged += new EventHandler(personnel_EmployeeChanged);

        }

        void personnel_EmployeeChanged(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                RaisePropertyChanged("Employee");
            }));
        }

        public ObservableCollection<Member> Members
        {
            get
            {
                return _Employee;
            }
            set
            {
                _Employee = value;
                RaisePropertyChanged("Members");
            }
        }

        public AsyncRelayCommand CancelCommand { get; set; }
        public AsyncRelayCommand SaveCommand { get; set; }
        public AsyncRelayCommand AddUserCommand { get; set; }
        public AsyncRelayCommand DeleteUserCommand { get; set; }


        private async Task DoCancelAsync()
        {
//UpdateBindingGroup.CancelEdit();
            if(SelectedIndex == -1)    //This only closes if new - just to show you how CancelEdit returns old values to bindings
                SelectedEmployee = null;

            await Task.Delay(0);
        }

        public async Task DoSave()
        {
            var employee = SelectedEmployee as Member;
            if(SelectedIndex == -1)
            {
                RaisePropertyChanged("Employee"); // Update the list from the data source
            }
            else
                serviceAgent = new ServiceAgent();

            Members = await serviceAgent.GetAll();
            SelectedEmployee = null;
        }

        private async Task AddUser()
        {
            SelectedEmployee = null; // Unselects last selection. Essential, as assignment below won't clear other control's SelectedItems
            var employee = new Member();
            SelectedEmployee = employee;
        }

        private async Task DeleteUser()
        {
            var employee = SelectedEmployee as Member;
            if(SelectedIndex != -1)
            {
                //personnel.DeleteEmployee(employee);
                RaisePropertyChanged("Employee"); // Update the list from the data source
            }
            else
                SelectedEmployee = null; // Simply discard the new object
        }

        public int SelectedIndex { get; set; }
        object _SelectedEmployee;
        public object SelectedEmployee
        {
            get
            {
                return _SelectedEmployee;
            }
            set
            {
                if(_SelectedEmployee != value)
                {
                    _SelectedEmployee = value;
                    RaisePropertyChanged("SelectedEmployee");
                }
            }
        }
    }
}
