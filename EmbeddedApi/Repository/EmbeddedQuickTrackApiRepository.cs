using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using QuickTrackWeb.EmbeddedApi.Entities;

namespace QuickTrackWeb.EmbeddedApi.Repository
{
    public class EmbeddedQuickTrackApiRepository : IQuickTrackEmbeddedApiRepository
    {
        ApplicationDbContext _context;
        public EmbeddedQuickTrackApiRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        //METHODS BELOW
        public IEnumerable<ClassEntity> GetAllClassEntities()
        {
            return _context.ClassEntities.OrderBy(ce => ce.Name).ToList();
        }

        public bool ClassEntityExists(string loggedInUserName)
        {
            return _context.ClassEntities.Any(ce => ce.OwnerIdentityName == loggedInUserName);
        }
        public ClassEntity GetClassEntity(string ownerIdentityName)
        {
            var result = _context.ClassEntities.Where(c => c.OwnerIdentityName == ownerIdentityName)
                .Include(y => y.Students)
                .Include(y => y.ProgressRecords)
                .Include(y => y.Weeks)
                .Include(y => y.TrackedItems)
                .FirstOrDefault();

            if (result == null)
            {
                return ClassEntity.GetNullObject();
            }
            else
            {
                return result;
            }
        }

        public void AddClassEntity(ClassEntity newClassEntity)
        {
            _context.ClassEntities.Add(newClassEntity);
        }

        public void UpdateClassEntity(ClassEntity updatedClassEntity)
        {
            var foundClassEntity = _context.ClassEntities.FirstOrDefault(e => e.Id == updatedClassEntity.Id);

            if (foundClassEntity != null)
            {
                foundClassEntity.Id = updatedClassEntity.Id;
                foundClassEntity.Name = updatedClassEntity.Name;
                foundClassEntity.OwnerIdentityName = updatedClassEntity.OwnerIdentityName;
                foundClassEntity.Weeks = updatedClassEntity.Weeks;
                foundClassEntity.Students = updatedClassEntity.Students;
                foundClassEntity.TrackedItems = updatedClassEntity.TrackedItems;
                foundClassEntity.ProgressRecords = updatedClassEntity.ProgressRecords;
            }
        }
    
        public void DeleteClassEntity(string loggedInUserName)
        {
            //This maintains the cascase delete. 
            var foundClassEntity = _context.ClassEntities.Where(e => e.OwnerIdentityName == loggedInUserName)
                .Include(y => y.Students)
                .Include(y => y.ProgressRecords)
                .Include(y => y.Weeks)
                .Include(y => y.TrackedItems)
                .FirstOrDefault();
          
            if (foundClassEntity == null) return;

            _context.ClassEntities.Remove(foundClassEntity);

        }

        public bool Save()
        {
            //This returns true when the number of entities saved is more than 0
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Student> GetAllStudentsFromClass(string classEntityOwnerIdentityName)
        {
            var classEntity = GetClassEntity(classEntityOwnerIdentityName);
            return _context.Students.Where(s => s.ClassEntityId == classEntity.Id);
        }
        public Student GetStudent(int studentId)
        {
            return _context.Students.Where(s =>  s.Id == studentId).FirstOrDefault();
        }
        public void AddStudent(string ownerIdentityName, Student newStudent)
        { 


            var classEntity = GetClassEntity(ownerIdentityName);
             classEntity.Students.Add(newStudent);
        }

        public void DeleteStudent(Student studentToDelete)
        {
            _context.Students.Remove(studentToDelete);
        }
    }
}

