using System;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Common.Extensions;
using iTechArt.Common.Pagination;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Foundation.Services.Answers;
using iTechArt.Surveys.Foundation.Services.Files;
using iTechArt.Surveys.Foundation.Services.Surveys;
using iTechArt.Surveys.Foundation.Services.Users;
using iTechArt.Surveys.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Surveys.WebApp.Controllers
{
    [Authorize]
    public class SurveysController : Controller
    {
        private readonly ISurveyService _surveyService;
        private readonly IUserService _userService;

        public SurveysController(ISurveyService surveyService, IUserService userService)
        {
            _surveyService = surveyService;
            _userService = userService;
        }


        public async Task<IActionResult> Index(string nameSearchTerm = "", SortOrder sortOrder = SortOrder.Ascending,
            int? page = 1)
        {
            const int pageSize = 5;

            var surveys = await _surveyService
                .GetSurveysAsync(page.Value, pageSize, sortOrder, nameSearchTerm);

            var surveysWithLink = surveys.MapPagedResult(survey
                => new SurveyWithLinksViewModel()
                {
                    SurveyId = survey.Id,
                    UpdateDate = survey.UpdateDate ?? survey.CreationDate,
                    Name = survey.Name,
                    ResultsLink = "#",
                    SurveyLink = Url.Action("Answer", "SurveyAnswer", new {id = survey.Id})
                });

            var pageResponse = new PageResponseViewModel<SurveyWithLinksViewModel>()
            {
                Items = surveysWithLink,
                NameSearchTerm = nameSearchTerm,
                SortOrder = sortOrder
            };

            if (surveys.TotalPages < surveys.CurrentPage && surveys.TotalPages > 0)
            {
                return RedirectToAction(nameof(Index),
                    new
                    {
                        searchRequest = pageResponse.NameSearchTerm,
                        sortOrder = pageResponse.SortOrder,
                        page = surveys.TotalPages
                    });
            }

            return View(pageResponse);
        }

        public IActionResult AddSurvey()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSurvey([FromBody] SurveyAddOrEditViewModel surveyModel)
        {
            var authorId = User.GetUserId();
            var author = await _userService.GetUserByIdAsync(authorId);

            if (author == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var survey = CreateFromSurveyAddOrEdit(surveyModel);

            await _surveyService.AddSurveyAsync(survey, author);

            return Ok();
        }

        public async Task<IActionResult> EditSurvey(Guid surveyId)
        {
            var survey = await _surveyService.GetSurveyByIdAsync(surveyId);

            var surveyView = CreateFromSurvey(survey);

            return View(surveyView);
        }

        [HttpPost]
        public async Task<IActionResult> EditConfirm([FromBody] SurveyAddOrEditViewModel surveyModel)
        {
            var survey = await _surveyService.GetSurveyByIdAsync(surveyModel.Id);

            if (survey == null)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            if (userId != survey.AuthorId)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            survey.Name = surveyModel.Name;
            survey.Questions = surveyModel.Questions.Select(question => new Question()
            {
                Title = question.Title,
                Options = question.Options.Select(answer => new QuestionOption() {Text = answer}).ToList(),
                Type = question.Type
            }).ToList();

            await _surveyService.UpdateSurveyAsync(survey);

            return Ok();
        }

        public IActionResult DeleteSurvey(SurveyDeleteViewModel surveyModel)
        {
            return View(surveyModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(SurveyDeleteViewModel surveyModel)
        {
            var survey = await _surveyService.GetSurveyByIdAsync(surveyModel.Id);

            if (survey == null)
            {
                ModelState.AddModelError(string.Empty, $"Survey don't exist");

                return View(nameof(DeleteSurvey), surveyModel);
            }

            await _surveyService.DeleteSurveyAsync(survey);

            return Redirect(surveyModel.ReturnUrl);
        }


        private Survey CreateFromSurveyAddOrEdit(SurveyAddOrEditViewModel surveyModel)
        {
            var survey = new Survey()
            {
                Name = surveyModel.Name,
                Questions = surveyModel.Questions.Select(question => new Question()
                {
                    Title = question.Title,
                    Type = question.Type,
                    Options = question.Options?.Select(answer => new QuestionOption() {Text = answer}).ToList()
                }).ToList()
            };

            return survey;
        }

        
        private SurveyAddOrEditViewModel CreateFromSurvey(Survey survey)
        {
            var surveyView = new SurveyAddOrEditViewModel()
            {
                Id = survey.Id,
                Name = survey.Name,
                Questions = survey.Questions.Select(question => new QuestionCreateOrEditViewModel()
                {
                    Title = question.Title,
                    Options = question.Options.Select(answer => answer.Text).ToList(),
                    Type = question.Type
                }).ToList()
            };
            
            return surveyView;
        }
    }
}