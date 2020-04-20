using QuickTrackWeb.EmbeddedApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.EmbeddedApi.Repository
{
    public interface IQuickTrackEmbeddedApiRepository
    {
        IEnumerable<ClassEntity> GetAllClassEntities();
        bool ClassEntityExists(string loggedInUserName);
        ClassEntity GetClassEntity(string loggedInUserName);
        void AddClassEntity(ClassEntity newClassEntity);
        void UpdateClassEntity(ClassEntity updatedClassEntity);
        void DeleteClassEntity(string loggedInUserName);

        bool StudentExists(int studentId);
        IEnumerable<Student> GetAllStudentsFromClass(string classEntityOwnerIdentityName);
        Student GetStudent(int studentId);
        void AddStudent(string classEntityOwnerIdentityName, Student newStudent);
        void DeleteStudent(Student studentToDelete);

        bool TrackedItemExists(int trackedItemId);
        IEnumerable<TrackedItem> GetAllTrackedItemsFromClass(string classEntityOwnerIdentityName);
        TrackedItem GetTrackedItem(int trackedItemId);
        void AddTrackedItem(string classEntityOwnerIdentityName, TrackedItem newTrackedItem);
        void DeleteTrackedItem(TrackedItem trackedItemToDelete);

        bool WeekExists(int weekId);
        IEnumerable<Week> GetAllWeeksFromClass(string classEntityOwnerIdentityName);
        Week GetWeek(int weekId);
        void AddWeek(string classEntityOwnerIdentityName, Week newWeek);
        void DeleteWeek(Week weekToDelete);

        bool ProgressRecordExists(int progressRecordId);
        ProgressRecord GetProgressRecordByForeignKeys(int ClassEntityId, int StudentId, int TrackedItemId,int WeekId);
        bool ProgressRecordIsDuplicate(ProgressRecord progressRecordId);
        ProgressRecord GetProgressRecord(int progressRecordId);
        void AddProgressRecord(ProgressRecord newProgressRecord); //TESTING! How do we do this!?!
        void DeleteProgressRecord(ProgressRecord progressRecordToDelete);


        bool Save();
    }
}
