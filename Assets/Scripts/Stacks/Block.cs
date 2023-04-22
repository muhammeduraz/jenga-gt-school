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
        public string domainid;
        public string domain;
        public string cluster;
        public string standardid;
        public string standarddescription;

        #endregion Variables

        #region Functions

        public override string ToString()
        {
            return
                $"id: {id}\n" +
                $"subject: {subject}\n" +
                $"grade: {grade}\n" +
                $"mastery: {mastery}\n" +
                $"domainid: {domainid}\n" +
                $"domain: {domain}\n" +
                $"cluster: {cluster}\n" +
                $"standardid: {standardid}\n" +
                $"standarddescription: {standarddescription}\n";
        }

        #endregion Functions
    }
}