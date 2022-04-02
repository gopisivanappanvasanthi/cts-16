using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.WebApi;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.Schema;
using Sitecore.Xdb.Common.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Cts.Feature.Search.Controllers
{
    public class ContactCreationController : ApiController
    {
        // From <xConnect instance>\App_Config\AppSettings.config
        const string CERTIFICATE_OPTIONS =
            "StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=9EED3BED7191FE8343EDBF798BFA8DF1090AE675";
        // From your installation
        const string XCONNECT_URL = "https://ctsxconnect.dev.local";

        [Route("altudoapi/CreateCustomContact")]
        [HttpGet]
        public IHttpActionResult CreateCustomContact()
        {
            CustomContactCreation().ConfigureAwait(false).GetAwaiter().GetResult();
            return Json("result");
        }

        private static async Task CustomContactCreation()
        {
            CertificateHttpClientHandlerModifierOptions options = CertificateHttpClientHandlerModifierOptions.Parse(CERTIFICATE_OPTIONS);
            var certificateModifier = new CertificateHttpClientHandlerModifier(options);

            List<IHttpClientModifier> clientModifiers = new List<IHttpClientModifier>();
            var timeoutClientModifier = new TimeoutHttpClientModifier(new TimeSpan(0, 0, 20));
            clientModifiers.Add(timeoutClientModifier);

            var collectionClient = new CollectionWebApiClient(
               new Uri(XCONNECT_URL + "/odata"),
               clientModifiers,
               new[] { certificateModifier }
           );

            var searchClient = new SearchWebApiClient(
                new Uri(XCONNECT_URL + "/odata"),
                clientModifiers,
                new[] { certificateModifier }
            );

            var configurationClient = new ConfigurationWebApiClient(
                new Uri(XCONNECT_URL + "/configuration"),
                clientModifiers,
                new[] { certificateModifier }
            );

            var cfg = new XConnectClientConfiguration(
              new XdbRuntimeModel(CollectionModel.Model),
              collectionClient,
              searchClient,
              configurationClient
            );

            try
            {
                cfg.Initialize();
            }
            catch (XdbModelConflictException ce)
            {
                return;
            }

            using (var client = new XConnectClient(cfg))
            {
                try
                {
                    var homepagegoal = Guid.Parse("E16BF05E-D50C-4C35-A699-A7CF55081F3F"); // "Watched demo" goal
                    var covidgoal = Guid.Parse("692E8F8E-E913-4824-B7B2-C628D4782B1B");
                    var channelId = Guid.Parse("5947F9D0-D220-4444-8A89-82C5FC197855"); // "Other event" channel
                    //creating my contact
                    // Identifier for a 'known' contact
                    var identifier = new ContactIdentifier[]
                    {
                        new ContactIdentifier(
                            "twitter",
                            "sivagopi123" + Guid.NewGuid().ToString("N"),
                            ContactIdentifierType.Known
                        )
                    };

                    // Create a new contact with the identifier
                    Contact knownContact = new Contact(identifier);

                    PersonalInformation personalInfoFacet = new PersonalInformation();

                    personalInfoFacet.FirstName = "Gopi";
                    personalInfoFacet.LastName = "S V";
                    personalInfoFacet.JobTitle = "Programmer Writer";

                    client.SetFacet<PersonalInformation>(
                        knownContact,
                        PersonalInformation.DefaultFacetKey,
                        personalInfoFacet
                    );

                    client.AddContact(knownContact);

                    // adding interaction for the contact

                    // Create a new interaction for that contact
                    Interaction interaction = new Interaction(knownContact, InteractionInitiator.Brand, channelId, "");

                    var xConnectEvent = new Goal(homepagegoal, DateTime.UtcNow);
                    interaction.Events.Add(xConnectEvent);
                    var xConnectCovidEvent = new Goal(covidgoal, DateTime.UtcNow);
                    interaction.Events.Add(xConnectCovidEvent);

                    IpInfo ipInfo = new IpInfo("127.0.0.1");
                    ipInfo.BusinessName = "Home";
                    client.SetFacet<IpInfo>(interaction, IpInfo.DefaultFacetKey, ipInfo);

                    // Add the contact and interaction
                    client.AddInteraction(interaction);

                    client.Submit();

                }
                catch (XdbExecutionException ex)
                {

                }
            }
        }
    }
}
