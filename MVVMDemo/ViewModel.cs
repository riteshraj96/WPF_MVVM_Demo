using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;


namespace MVVMDemo
{
    public class ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private Student _student;
        private ObservableCollection<Student> _students;
        private ICommand _SubmitCommand;
        private ObservableCollection<Student> _fillCourseId = new ObservableCollection<Student>();
        static String connectionString = @"Data Source=RITESH-PC\SQLEXPRESS;Initial Catalog=SIT_Ritesh_DB;Integrated Security=True;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;
        SqlDataReader reader;

        public ObservableCollection<Student> FillCourseId
        {
            get { return _fillCourseId; }
            set
            {
                _fillCourseId = value;
                OnPropertyChanged("SystemStatusData");
            }
        }



        public Student Student
        {
            get
            {
                return _student;
            }
            set
            {
                _student = value;
                NotifyPropertyChanged("Student");
            }
        }
        public ObservableCollection<Student> Students
        {
            get
            {
                return _students;
            }
            set
            {
                _students = value;
                NotifyPropertyChanged("Students");
            }
        }

        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new RelayCommand(param => this.Submit(),
                        null);
                }
                return _SubmitCommand;
            }
        }

        //********************************************* Functions*******************************************// 

        public void GetCourseIdFromDB()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select * from dev_Course", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
               // Student Student = new Student();

                for (int i = 0; i < dt.Rows.Count; ++i)
                    FillCourseId.Add(new Student
                    {
                        CourseID = dt.Rows[i][0].ToString(),
                        CourseName =dt.Rows[i][1].ToString()
                    });

            }
            catch (Exception ex)
            {

            }
            
        }
        public ViewModel()
        {
            Student = new Student();
            Students = new ObservableCollection<Student>();
            Students.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Students_CollectionChanged);
            GetCourseIdFromDB();
        }

        void Students_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Students");
        }
        
        private void Submit()
        {
            Student.JoiningDate = DateTime.Today.Date;
            Students.Add(Student);
            Student = new Student();
        }
        // Property Changed Event 
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyname)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}

