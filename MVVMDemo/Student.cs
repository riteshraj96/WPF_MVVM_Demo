using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMDemo
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }
        public string CourseName { get; set; }
        public string CourseID { get; set; }

        //private string courseId;
        //public string CourseID
        //{
        //    get { return courseId; }
        //    set { courseId = value; }
        //}

        //private string courseName;
        //public string CourseName
        //{
        //    get { return courseName; }
        //    set { courseName = value; }
        //}
        public DateTime JoiningDate { get; set; }
    }
}
