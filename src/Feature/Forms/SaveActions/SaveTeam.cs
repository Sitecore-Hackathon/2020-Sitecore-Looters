using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hackathon.Boilerplate.Feature.Forms.Models;
using Hackathon.Boilerplate.Feature.Forms.ReadOnly;
using Hackathon.Boilerplate.Foundation.ContentManagement.Interfaces;
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
        private IItemManagement _ItemManagement => DependencyResolver.Current.GetService<IItemManagement>();
        public SaveTeam(ISubmitActionData submitActionData) : base(submitActionData)
        {
         
        }

        protected override bool TryParse(string value, out string target)
        {
            target = string.Empty;
            return true;
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
            try
            {


                var fields = formSubmitContext.Fields;

                #region FormFields
                var teamNameField = GetFieldById(Guid.Parse(SaveTeamFields.TeamName), fields);
                var emailField = GetFieldById(Guid.Parse(SaveTeamFields.Email), fields);
                var countryField = GetFieldById(Guid.Parse(SaveTeamFields.Country), fields);
                var firstMemberNameField = GetFieldById(Guid.Parse(SaveTeamFields.FullNameOne), fields);
                var firstMemberGithubField = GetFieldById(Guid.Parse(SaveTeamFields.GithubOne), fields);
                var firstMemberTwitterField = GetFieldById(Guid.Parse(SaveTeamFields.TwitterOne), fields);
                var firstMemberLinkedInField = GetFieldById(Guid.Parse(SaveTeamFields.LinkedInOne), fields);
                var secondMemberNameField = GetFieldById(Guid.Parse(SaveTeamFields.FullNameTwo), fields);
                var secondMemberGithubField = GetFieldById(Guid.Parse(SaveTeamFields.GithubTwo), fields);
                var secondMemberTwitterField = GetFieldById(Guid.Parse(SaveTeamFields.TwitterTwo), fields);
                var secondMemberLinkedInField = GetFieldById(Guid.Parse(SaveTeamFields.LinkedInTwo), fields);
                var thirdMemberNameField = GetFieldById(Guid.Parse(SaveTeamFields.FullNameThree), fields);
                var thirdMemberGithubField = GetFieldById(Guid.Parse(SaveTeamFields.GithubThree), fields);
                var thirdMemberTwitterField = GetFieldById(Guid.Parse(SaveTeamFields.TwitterThree), fields);
                var thirdMemberLinkedInField = GetFieldById(Guid.Parse(SaveTeamFields.LinkedInThree), fields);
                #endregion

                #region FieldsValues
                var newTeamName = GetValue(teamNameField);
                var teamEmail = GetValue(emailField);
                var county = GetValue(countryField);
                var firstMemberName = GetValue(firstMemberNameField);
                var firstMemberGithub = GetValue(firstMemberGithubField);
                var firstMemberTwitter = GetValue(firstMemberTwitterField);
                var firstMemberLinkedIn = GetValue(firstMemberLinkedInField);
                var secondMemberName = GetValue(secondMemberNameField);
                var secondMemberGithub = GetValue(secondMemberGithubField);
                var secondMemberTwitter = GetValue(secondMemberTwitterField);
                var secondMemberLinkedIn = GetValue(secondMemberLinkedInField);
                var thirdMemberName = GetValue(thirdMemberNameField);
                var thirdMemberGithub = GetValue(thirdMemberGithubField);
                var thirdMemberTwitter = GetValue(thirdMemberTwitterField);
                var thirdMemberLinkedIn = GetValue(thirdMemberLinkedInField);
                #endregion


                var baseTeamPath = Settings.GetSetting("SaveTeam.TeamsPath");
                var currentYearPath = baseTeamPath + "/" + DateTime.Today.ToString("yyyy");
                var teamPath = currentYearPath + "/" + newTeamName;

                var teamItem = _ItemManagement.GetItemByPath<TeamInfo>(teamPath);
                if (teamItem != null)
                {
                    SubmitActionData.ErrorMessage = $"The Team name \"{newTeamName}\" already exists";
                }

                var thisYearTeams = _ItemManagement.GetItemByPath<TeamFolder>(currentYearPath);



                if (thisYearTeams != null)
                {
                    foreach (var team in thisYearTeams.Teams)
                    {
                        if (team.Email == teamEmail)
                        {
                            SubmitActionData.ErrorMessage = $"A team is already registered with the email \"{teamEmail}\"";
                            return false;
                        }
                    }
                }
                else
                {
                    var newTeamsFolder = new TeamFolder
                    {
                        Name = DateTime.Today.ToString("yyyy")
                    };
                    _ItemManagement.CreateSitecoreItemUsingParentPath(newTeamsFolder, baseTeamPath);
                }
                var newTeam = new TeamInfo
                {
                    Name = newTeamName,
                    TeamName = newTeamName,
                    Email = teamEmail,
                    Country = county,
                    FirstMemberName = firstMemberName,
                    FirstMemberGithub = firstMemberGithub,
                    FirstMemberTwitter = firstMemberTwitter,
                    FirstMemberLinkedIn = firstMemberLinkedIn,
                    SecondMemberName = secondMemberName,
                    SecondMemberGithub = secondMemberGithub,
                    SecondMemberTwitter = secondMemberTwitter,
                    SecondMemberLinkedIn = secondMemberLinkedIn,
                    ThirdMemberName = thirdMemberName,
                    ThirdMemberGithub = thirdMemberGithub,
                    ThirdMemberTwitter = thirdMemberTwitter,
                    ThirdMemberLinkedIn = thirdMemberLinkedIn

                };
                _ItemManagement.CreateSitecoreItemUsingParentPath(newTeam, currentYearPath);
                HttpContext.Current.Items["NextFormPage"] = Guid.Parse(Settings.GetSetting("SaveTeam.TeamAddedPage"));
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Hackathon.Boilerplate.Feature.Forms", ex, this);
                HttpContext.Current.Items["NextFormPage"] = Guid.Parse(Settings.GetSetting("SaveTeam.ErrorPage"));
                return false;
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
