using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QuickTrackWeb.Services
{
    public class ServiceResponse
    {
        public bool Succeeded;
        public string Description;
        public string Origin;
        public string Parameters;

        public ServiceResponse([CallerMemberName] string methodName = null,
                                        [CallerFilePath] string sourceFile = null)
        {
            Succeeded = false;
            Description = "<<<default value from ProcedureResult constructor>>>";
            Origin = $"{Path.GetFileNameWithoutExtension(sourceFile)}.{methodName}";
            Parameters = "";
        }
        public void AddException(Exception ex)
        {
            if (Description == "new")
            {
                Description = $"ERROR: {ex}";
            }
            else
            {
                Description = $"{Description}\nERROR: {ex}";
            }

        }
        public void Success()
        {
            Succeeded = true;
            Description = "SUCCESS";
        }
        public override string ToString()
        {
            return $"{Description}: {Origin}({Parameters})"; //base.ToString();
        }
    }

    public class ServiceResponse<T> where T: new()
    {
        private T Data;
        public T Content
        {
            get { return this.Data; }
            set { this.Data = Content; }
        }

        public bool Succeeded;
        public string Description;
        public string Origin;
        public string Parameters;
        

        public ServiceResponse([CallerMemberName] string methodName = null,
                                        [CallerFilePath] string sourceFile = null)
        {
            this.Data = new T();
            Succeeded = false;
            Description = "new";
            Origin = $"{Path.GetFileNameWithoutExtension(sourceFile)}.{methodName}";
            Parameters = "";
        }
        public void AddException(Exception ex)
        {
            if(Description == "new")
            {
                Description = $"ERROR: {ex}";
            }
            else
            {
                Description = $"{Description}\nERROR: {ex}";
            }
            
        }
        public void Success()
        {
            Succeeded = true;
            Description = "SUCCESS";
        }
        public override string ToString()
        {
            return $"{Description}: {Origin}({Parameters})"; //base.ToString();
        }
    }
}
