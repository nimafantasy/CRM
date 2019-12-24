using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Mvc;

namespace SohaCRM.Models.Training
{
    public class TrainingListModel
    {
        public int Customer_ID { get; set; }
        public int Training_ID { get; set; }
        public string CustomerName { get; set; }
        public string LastName { get; set; }
        public string ScoreOfPeople { get; set; }
        public string NamesOfPeople { get; set; }
        public string TrainingDate { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public int IsActive_ID { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
        public string CustomerNameInSearch { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string SubscriptionCode { get; set; }
        public string CustomerConnector { get; set; }
    }
}