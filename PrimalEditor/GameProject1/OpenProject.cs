using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.GameProject1
{
    [DataContract]
    public class ProjectData {
        [DataMember]
        public string ProjectName { get; set; }
        [DataMember]
        public string ProjectPath { get; set; }
        [DataMember]
        public DateTime Date { get; set; }

    }
    [DataContract]
    public class ProjectDataList {
        [DataMember]
        public List<ProjectData> Projects { get; set; }
    }


    class OpenProject
    {
/*        private static readonly string _applicationDataPath = $@"{Environment}";


        static OpenProject() {
            try
            {
                if (!Directory.Exists()) ;
            }
            catch (Exception)
            {

                throw;
            }
        }*/
    }
}
