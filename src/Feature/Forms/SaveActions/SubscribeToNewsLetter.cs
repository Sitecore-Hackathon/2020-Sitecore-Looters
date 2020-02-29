using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using Hackathon.Boilerplate.Feature.Forms.Models;
using Sitecore.Analytics;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using Sitecore.ListManagement.XConnect.Web;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Configuration;
using Sitecore.XConnect.Collection.Model;
using Sitecore.Configuration;

namespace Hackathon.Boilerplate.Feature.Forms.SaveActions
{
    public class SubscribeToNewsletter : SubmitActionBase<NewsLetterContactData>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscribeToNewsletter"/> class.
        /// </summary>
        /// <param name="submitActionData">The submit action data.</param>

        public SubscribeToNewsletter(ISubmitActionData submitActionData) : base(submitActionData)
        {
        }

        /// <summary>
        /// Gets the current tracker.
        /// </summary>     
        protected virtual ITracker CurrentTracker => Tracker.Current;

        /// <summary>
        /// Executes the action with the specified <paramref name="data" />.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="formSubmitContext">The form submit context.</param>
        /// <returns><c>true</c> if the action is executed correctly; otherwise <c>false</c></returns>

        protected override bool Execute(NewsLetterContactData data, FormSubmitContext formSubmitContext)
        {
            Assert.ArgumentNotNull(data, nameof(data));
            Assert.ArgumentNotNull(formSubmitContext, nameof(formSubmitContext));
            var firstNameField = GetFieldById(data.FirstNameFieldId, formSubmitContext.Fields);
            var lastNameField = GetFieldById(data.LastNameFieldId, formSubmitContext.Fields);
            var emailField = GetFieldById(data.EmailFieldId, formSubmitContext.Fields);
            if (firstNameField == null && lastNameField == null && emailField == null)
            {
                return false;
            }
            using (var client = CreateClient())
            {
                try
                {
                    var source = "Subscribe.Form";
                    var id = GetValue(emailField);//to sure that email address is unique 
                    CurrentTracker.Session.IdentifyAs(source, id);
                    var trackerIdentifier = new IdentifiedContactReference(source, id);
                    var expandOptions = new ContactExpandOptions(
                        CollectionModel.FacetKeys.PersonalInformation,
                        CollectionModel.FacetKeys.EmailAddressList);
                    Contact contact = client.Get(trackerIdentifier, expandOptions);
                    SetPersonalInformation(GetValue(firstNameField), GetValue(lastNameField), contact, client);
                    if (contact.Personal() == null)//new Contact
                    {
                        HttpContext.Current.Items["NextFormPage"] = Guid.Parse(Settings.GetSetting("NewsLetter.ContactAddedPage"));
                    }
                    else //Update Contact
                    {
                        HttpContext.Current.Items["NextFormPage"] = Guid.Parse(Settings.GetSetting("NewsLetter.ContactUpdatedPage"));
                    }
                    SetEmail(GetValue(emailField), contact, client);
                    client.Submit(); // submit to ExperienceProfile
                    #region Save to contact to Contact list
                    SubscriptionService service = (SubscriptionService)ServiceLocator.ServiceProvider.GetService(typeof(ISubscriptionService));
                    Guid listId = new Guid(Settings.GetSetting("NewsLetter.ListID"));// Get your Contact list ID
                    Guid contactId = new Guid(contact.Id.ToString());
                    service.Subscribe(listId, contactId);
                    #endregion
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Error("Hackathon.Boilerplate.Feature.Forms", ex,this);
                    HttpContext.Current.Items["NextFormPage"] = Guid.Parse(Settings.GetSetting("NewsLetter.ErrorPage"));
                    return false;
                }
            }
        }

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <returns>The <see cref="IXdbContext"/> instance.</returns>

        protected virtual IXdbContext CreateClient()
        {
            return SitecoreXConnectClientConfiguration.GetClient();
        }

        /// <summary>
        /// Gets the field by <paramref name="id" />.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fields">The fields.</param>
        /// <returns>The field with the specified <paramref name="id" />.</returns>

        private static IViewModel GetFieldById(Guid id, IList<IViewModel> fields)
        {
            return fields.FirstOrDefault(f => Guid.Parse(f.ItemId) == id);
        }

        /// <summary>
        /// Gets the <paramref name="field" /> value.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The field value.</returns>

        private static string GetValue(object field)
        {
            return field?.GetType().GetProperty("Value")?.GetValue(field, null)?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Sets the <see cref="PersonalInformation"/> facet of the specified <paramref name="contact" />.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="contact">The contact.</param>
        /// <param name="client">The client.</param>

        private static void SetPersonalInformation(string firstName, string lastName, Contact contact, IXdbContext client)
        {
            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                return;
            }
            PersonalInformation personalInfoFacet = contact.Personal() ?? new PersonalInformation();
            if (personalInfoFacet.FirstName == firstName && personalInfoFacet.LastName == lastName)
            {
                return;
            }
            personalInfoFacet.FirstName = firstName;
            personalInfoFacet.LastName = lastName;
            client.SetPersonal(contact, personalInfoFacet);
        }

        /// <summary>
        /// Sets the <see cref="EmailAddressList"/> facet of the specified <paramref name="contact" />.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <param name="contact">The contact.</param>
        /// <param name="client">The client.</param>

        private static void SetEmail(string email, Contact contact, IXdbContext client)
        {
            if (string.IsNullOrEmpty(email))
            {
                return;
            }
            EmailAddressList emailFacet = contact.Emails();
            if (emailFacet == null)
            {
                emailFacet = new EmailAddressList(new EmailAddress(email, false), "Preferred");
            }
            else
            {
                if (emailFacet.PreferredEmail?.SmtpAddress == email)
                {
                    return;
                }
                emailFacet.PreferredEmail = new EmailAddress(email, false);
            }
            client.SetEmails(contact, emailFacet);
        }
    }
}
