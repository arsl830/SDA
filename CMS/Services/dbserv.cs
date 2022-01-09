using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Models;
namespace CMS.Services
{
    public class dbserv
    {
        public Table Login(Table T1)

        {
            cmsContext db = new cmsContext();
            var credentials = db.Tables.Where(model => model.Email == T1.Email && model.Password == T1.Password).FirstOrDefault();
            return credentials;
        }

        public List<Table> getallstudents()
        {
            List<Table> listAllstudents;
            using (cmsContext dbb = new cmsContext())
            {
                listAllstudents = dbb.Tables.ToList();

            }
            return listAllstudents;

        }

        public void addnewstudent(Teacher T)
        {
            using (cmsContext db = new cmsContext())
            {
                T.Name = "PUCIT STUDENT";
                db.Teachers.Add(T);
                db.SaveChanges();
            }
        }

        public List<Course> getlistofsubjects()
        {
            List<Course> listofsubjects;
            using (cmsContext dbb = new cmsContext())
            {
                listofsubjects = dbb.Courses.ToList();
            }
            return listofsubjects;
        }

        public List<Teacher>getlist()
        {
            List<Teacher> listofstudents;

            using (cmsContext dbb = new cmsContext())
            {
                listofstudents = dbb.Teachers.ToList();
            }
            return listofstudents;
        }

        public void assigncourse(Course C)
        {
            using (cmsContext db = new cmsContext())
            {

                db.Courses.Add(C);
                db.SaveChanges();
            }
        }
        public void deletestudent(int id)
        {
            using (cmsContext db = new cmsContext())
            {
                Teacher obj = db.Teachers.Find(id);
                db.Teachers.Remove(obj);
                db.SaveChanges();
            }
        }
    }
}
