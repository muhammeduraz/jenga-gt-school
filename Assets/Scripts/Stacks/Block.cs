using System;

namespace Assets.Scripts.Stacks
{
    [Serializable]
    public class Block
    {
        #region Variables

        public int id;
        public string subject;
        public string grade;
        public int mastery;
        public string domainId;
        public string domain;
        public string cluster;
        public string standardId;
        public string standardDescription;

        #endregion Variables

        #region Functions

        public override string ToString()
        {
            return
                $"id: {id}\n" +
                $"subject: {subject}\n" +
                $"grade: {grade}\n" +
                $"mastery: {mastery}\n" +
                $"domainid: {domainId}\n" +
                $"domain: {domain}\n" +
                $"cluster: {cluster}\n" +
                $"standardid: {standardId}\n" +
                $"standarddescription: {standardDescription}\n";
        }

        #endregion Functions
    }
}