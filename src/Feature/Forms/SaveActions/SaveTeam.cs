using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using Hackathon.Boilerplate.Feature.Forms.Models;
using Hackathon.Boilerplate.Feature.Forms.ReadOnly;
using Hackathon.Boilerplate.Foundation.ContentManagement.Interfaces;
using Microsoft.Extensions.DependencyModel;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using Sitecore.XConnect;
using Sitecore.XConnect.Client.Configuration;
using Sitecore.XConnect.Collection.Model;

namespace Hackathon.Boilerplate.Feature.Forms.SaveActions
{
    public class SaveTeam : SubmitActionBase<string>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveTeam"/> class.
        /// </summary>
        /// <param name="submitActionData">The submit action data.</param>
        private IItemManagement _ItemManagement;
        public SaveTeam(ISubmitActionData submitActionData) : base(submitActionData)
        {
            _ItemManagement = DependencyResolver.Current.GetService<IItemManagement>();
        }


        /// <summary>
        /// Executes the action with the specified <paramref name="data" />.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="formSubmitContext">The form submit context.</param>
        /// <returns><c>true</c> if the action is executed correctly; otherwise <c>false</c></returns>

        protected override bool Execute(string data, FormSubmitContext formSubmitContext)
        {
            Assert.ArgumentNotNull(data, nameof(data));
            Assert.ArgumentNotNull(formSubmitContext, nameof(formSubmitContext));
            var fields = formSubmitContext.Fields;
            var baseTeamPath = Settings.GetSetting("SaveTeam.TeamsPath");
            var currentYearPath = baseTeamPath +"/"+ DateTime.Today.ToString("yyyy");
            var teamNameField = GetFieldById(Guid.Parse(SaveTeamFields.TeamName), fields);
            var teamPath = currentYearPath+"/"+GetValue(teamNameField);

            var teamItem = _ItemManagement.GetItemByPath<TeamInfo>(teamPath);
            if (teamItem != null)
            {
                SubmitActionData.ErrorMessage = $"The Team name \"{GetValue(teamNameField)}\" already exists";
            }

            return true;
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
