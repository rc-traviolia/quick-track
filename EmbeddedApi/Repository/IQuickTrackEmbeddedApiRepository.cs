﻿using QuickTrackWeb.EmbeddedApi.Entities;
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

        IEnumerable<Student> GetAllStudentsFromClass(string classEntityOwnerIdentityName);
        Student GetStudent(int studentId);
        void AddStudent(string classEntityOwnerIdentityName, Student newStudent);
        void DeleteStudent(Student studentToDelete);

        bool Save();
    }
}
