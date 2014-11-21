//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;

//namespace ScrumBoard.Business
//{
//    public class Member
//    {
//        public String Name { get; set; }
//        public Decimal FocusFactor { get; set; }
//        public Decimal AvailabilityFactor { get; set; }
//        public Decimal NormalHours { get; set; }
//        public List<DateTime> NonWorkingHours { get; set; }

//        public Member(String name, Decimal focusFactor, Decimal availabilityFactor, Decimal normalHours)
//        {
//            Name = name;
//            FocusFactor = focusFactor;
//            AvailabilityFactor = availabilityFactor;
//            NormalHours = normalHours;
//            Init();
//        }

//        public void StoreNonWorkingHours()
//        {
//            String fileName = String.Format("Member_{0}.dat", Name);
//            if (File.Exists(fileName))
//                File.Delete(fileName);
//            StreamWriter sw = File.CreateText(fileName);
//            sw.WriteLine(Name);
//            sw.WriteLine(FocusFactor.ToString());
//            sw.WriteLine(AvailabilityFactor.ToString());
//            sw.WriteLine(NormalHours.ToString());
//            foreach (DateTime d in NonWorkingHours)
//            {
//                sw.WriteLine(d.ToFileTimeUtc().ToString());
//            }
//            sw.Close();
//        }

//        private void Init()
//        {
//            NonWorkingHours = new List<DateTime>();
//            String fileName = String.Format("NonWorkingHours_{0}.dat", Name);
//            if (File.Exists(fileName))
//            {
//                StreamReader sr = File.OpenText(fileName);
//                while (!sr.EndOfStream)
//                {
//                    NonWorkingHours.Add(DateTime.FromFileTimeUtc(long.Parse(sr.ReadLine())));
//                }
//                sr.Close();
//            }
//        }
//    }
//}
