using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkinCareWebApp.Models;

namespace SkinCareWebApp.Services
{
    public class AssessmentService
    {
        private SkinCareDBEntities Entities = new SkinCareDBEntities();

        public double getPrecentage(int correct)
        {
            if (Entities.AssessmentResponses.Count() < 1)
            {
                return 100;
            }
            return ((double)Entities.AssessmentResponses.Where(x => x.correct < correct).Count() / (double)Entities.AssessmentResponses.Count()) * 100;
        }

        public void saveResponse(AssessmentRespons response)
        {
            Entities.AssessmentResponses.Add(response);
            Entities.SaveChanges();
            return;
        }
    }
}