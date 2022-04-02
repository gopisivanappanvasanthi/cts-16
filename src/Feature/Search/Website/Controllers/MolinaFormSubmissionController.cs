using Cts.Feature.Search.Models;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Configuration;
using Sitecore.Publishing;
using System.Web.Http;
using Sitecore.Data.Items;

namespace Cts.Feature.Search.Controllers
{
    public class MolinaFormSubmissionController : ApiController
    {
        // GET: MolinaFormSubmission
        [Route("molinahcapi/prescriptionform")]
        [HttpPost]
        public IHttpActionResult PostPrescriptionForm(PrescriptionFormData inputParam)
        {
            //parent item
            ID parentItemId = new ID(Templates.PrescriptionParent.PrescriptionParentItemId);
            Database masterDatabase = Factory.GetDatabase(Constants.masterDB);
            Database webDatabase = Factory.GetDatabase(Constants.webDB);
            Item parentItem = masterDatabase.GetItem(parentItemId);

            ID prescriptionTemplateIdObj = new ID(Templates.Prescription.PrescriptionTemplateId);
            TemplateID prescriptionTemplateId = new TemplateID(prescriptionTemplateIdObj);
            string result = string.Empty;
            try
            {
                using (new SecurityDisabler())
                {
                    var newlycreatedItem = parentItem.Add(inputParam.diagnosis, prescriptionTemplateId);
                    newlycreatedItem.Editing.BeginEdit();
                    newlycreatedItem.Fields["diagnosis"].Value = inputParam.diagnosis;
                    newlycreatedItem.Fields["patientBPlevel"].Value = inputParam.patientBPlevel;
                    newlycreatedItem.Fields["patientHeight"].Value = inputParam.patientHeight;
                    newlycreatedItem.Fields["patientWeight"].Value = inputParam.patientWeight;
                    newlycreatedItem.Editing.EndEdit();
                    
                    PublishOptions publishOptions = new PublishOptions(masterDatabase, webDatabase, PublishMode.SingleItem, Sitecore.Context.Language, DateTime.Now);
                    Publisher publisher = new Publisher(publishOptions);
                    publisher.Options.RootItem = newlycreatedItem;
                    publisher.Options.Deep = true;
                    publisher.Publish();
                    result = "Published Successfully";
                }
            }catch(Exception ex)
            {
                result = ex.ToString();
            }

            return Json(result);
        }
    }
}