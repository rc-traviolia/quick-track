using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using QuickTrackWeb.EmbeddedApi.Entities;
using QuickTrackWeb.EmbeddedApi.Services;

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
            return _context.Students.Where(s => s.ClassEntityId == classEntity.Id).OrderBy(s => s.Name);
        }
        public Student GetStudent(int studentId)
        {
            var result = _context.Students.Where(s => s.Id == studentId)
               .Include(y => y.ProgressRecords)
               .FirstOrDefault();

            if (result == null)
            {
                return Student.GetNullObject();
            }
            else
            {
                return result;
            }
           //OLD:  return _context.Students.Where(s =>  s.Id == studentId).FirstOrDefault();
        }
        public void AddStudent(string classEntityOwnerIdentityName, Student newStudent)
        { 
             var classEntity = GetClassEntity(classEntityOwnerIdentityName);
             classEntity.Students.Add(newStudent);
        }

        public void DeleteStudent(Student studentToDelete)
        {
            _context.Students.Remove(studentToDelete);
        }

        public IEnumerable<TrackedItem> GetAllTrackedItemsFromClass(string classEntityOwnerIdentityName)
        {
            var classEntity = GetClassEntity(classEntityOwnerIdentityName);
            return _context.TrackedItems.Where(s => s.ClassEntityId == classEntity.Id).OrderBy(ti => ti.Name);
        }

        public TrackedItem GetTrackedItem(int trackedItemId)
        {
            var result = _context.TrackedItems.Where(ti => ti.Id == trackedItemId)
               .Include(y => y.ProgressRecords)
               .FirstOrDefault();

            if (result == null)
            {
                return TrackedItem.GetNullObject();
            }
            else
            {
                return result;
            }
        }

        public void AddTrackedItem(string classEntityOwnerIdentityName, TrackedItem newTrackedItem)
        {
            var classEntity = GetClassEntity(classEntityOwnerIdentityName);
            classEntity.TrackedItems.Add(newTrackedItem);
        }

        public void DeleteTrackedItem(TrackedItem trackedItemToDelete)
        {
            _context.TrackedItems.Remove(trackedItemToDelete);
        }

        public IEnumerable<Week> GetAllWeeksFromClass(string classEntityOwnerIdentityName)
        {
            var classEntity = GetClassEntity(classEntityOwnerIdentityName);
            return _context.Weeks.Where(s => s.ClassEntityId == classEntity.Id).OrderBy(w => w.Number);
        }

        public Week GetWeek(int weekId)
        {
            var result = _context.Weeks.Where(w => w.Id == weekId)
               .Include(y => y.ProgressRecords)
               .FirstOrDefault();

            if (result == null)
            {
                return Week.GetNullObject();
            }
            else
            {
                return result;
            }
        }

        public void AddWeek(string classEntityOwnerIdentityName, Week newWeek)
        {
            var classEntity = GetClassEntity(classEntityOwnerIdentityName);
            classEntity.Weeks.Add(newWeek);
        }

        public void DeleteWeek(Week weekToDelete)
        {
            _context.Weeks.Remove(weekToDelete);
        }

        public ProgressRecord GetProgressRecord(int progressRecordId)
        {
            var result = _context.ProgressRecords.Where(pr => pr.Id == progressRecordId)
                .FirstOrDefault();

            if (result == null)
            {
                return ProgressRecord.GetNullObject();
            }
            else
            {
                return result;
            }
        }

        public void AddProgressRecord(ProgressRecord newProgressRecord)
        { 
            _context.ProgressRecords.Add(newProgressRecord);
        }

        public void DeleteProgressRecord(ProgressRecord progressRecordToDelete)
        {
            _context.ProgressRecords.Remove(progressRecordToDelete);
        }

        public bool StudentExists(int studentId)
        {
            return _context.Students.Any(s => s.Id == studentId);
        }

        public bool TrackedItemExists(int trackedItemId)
        {
            return _context.TrackedItems.Any(ti => ti.Id == trackedItemId);
        }

        public bool WeekExists(int weekId)
        {
            return _context.Weeks.Any(w => w.Id == weekId);
        }

        public bool ProgressRecordExists(int progressRecordId)
        {
            return _context.ProgressRecords.Any(pr => pr.Id == progressRecordId);
        }
        public bool ProgressRecordIsDuplicate(ProgressRecord progressRecord)
        {
            if(_context.ProgressRecords.Any(pr => 
            pr.ClassEntityId == progressRecord.ClassEntityId
            && pr.StudentId == progressRecord.StudentId
            && pr.WeekId == progressRecord.WeekId
            && pr.TrackedItemId == progressRecord.TrackedItemId))
            {
                return true;
            }
            return false;
        }

        public ProgressRecord GetProgressRecordByForeignKeys(int ClassEntityId, int StudentId, int TrackedItemId, int WeekId)
        {
            return _context.ProgressRecords.Where(pr =>
            pr.ClassEntityId == ClassEntityId
            && pr.StudentId == StudentId
            && pr.WeekId == WeekId
            && pr.TrackedItemId == TrackedItemId).FirstOrDefault();
        }

        public IEnumerable<ProgressRecord> GetAllProgressRecordsFromClassAndWeek(string classEntityOwnerIdentityName, int weekId)
        {
            //this is lazy and shouldn't need to happen, but my site is, at this point,
            //needing to be completed... RT 4/24/2020
            var classEntity = GetClassEntity(classEntityOwnerIdentityName);

            return _context.ProgressRecords.Where(pr =>
                pr.ClassEntityId == classEntity.Id &&
                pr.WeekId == weekId);
        }

        public IEnumerable<ProgressRecord> GetAllProgressRecordsFromClassEntity(string classEntityOwnerIdentityName)
        {
            //this is lazy and shouldn't need to happen, but my site is, at this point,
            //needing to be completed... 
            //copied the code and am leaving the comment... RT 4/29/2020
            var classEntity = GetClassEntity(classEntityOwnerIdentityName);

            return _context.ProgressRecords.Where(pr =>
                pr.ClassEntityId == classEntity.Id);
        }
    }
}

